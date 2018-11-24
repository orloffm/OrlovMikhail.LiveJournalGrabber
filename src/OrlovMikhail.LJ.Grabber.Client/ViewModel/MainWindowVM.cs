using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using log4net;
using log4net.Repository.Hierarchy;
using OrlovMikhail.LJ.Grabber.Client.Properties;
using OrlovMikhail.LJ.Grabber.Extractor.Interfaces;

namespace OrlovMikhail.LJ.Grabber.Client.ViewModel
{
    public class MainWindowVM : ViewModelBase, IMainWindowVM
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindowVM));

        private readonly RelayCommand _runCommand;
        private readonly IWorker _w;

        public MainWindowVM(IWorker w)
        {
            _w = w;

            _runCommand = new RelayCommand(Run, CanRun);

            // Appender that allows us to log to a window.
            UIAppender app = new UIAppender();
            app.StringAdded += (sender, args) =>
            {
                string s = Log;
                string add = string.IsNullOrWhiteSpace(s) ? "" : Environment.NewLine;
                add += args.Value;

                Log += add;
            };

            // Set it.
            ((Hierarchy) LogManager.GetLoggerRepository()).Root.AddAppender(app);
            LoadSettings();

            IsEnabled = true;
        }

        public ICommand RunCommand => _runCommand;

        public void SaveSettings()
        {
            Settings.Default.URL = URI;
            Settings.Default.Cookie = Cookie;
            Settings.Default.RootFolder = BookRootLocation;
            Settings.Default.SubFolder = Subfolder;

            Settings.Default.Save();
        }

        private bool CanRun()
        {
            return !string.IsNullOrWhiteSpace(URI) && !string.IsNullOrWhiteSpace(Cookie) &&
                   !string.IsNullOrWhiteSpace(BookRootLocation) && !string.IsNullOrWhiteSpace(Subfolder);
        }

        private void LoadSettings()
        {
            URI = Settings.Default.URL;
            Cookie = Settings.Default.Cookie;
            BookRootLocation = Settings.Default.RootFolder;
            Subfolder = Settings.Default.SubFolder;
        }

        private void Run()
        {
            if (!CanRun())
            {
                return;
            }

            Task task = Task.Factory.StartNew(() => RunInternal());
            task.ContinueWith(
                t => t.Exception.Handle(
                    ex =>
                    {
                        log.Error("Error: " + ex.Message + Environment.NewLine + ex.StackTrace);
                        return true;
                    }
                )
                , TaskContinuationOptions.OnlyOnFaulted
            );
        }

        private void RunInternal()
        {
            try
            {
                IsEnabled = false;

                Log = string.Empty;
                SaveSettings();

                _w.WorkInGivenTarget(URI, BookRootLocation, Subfolder, Cookie);
            }
            finally
            {
                IsEnabled = true;
            }
        }

        #region properties

        private string _log;

        public string Log
        {
            get => _log;
            set
            {
                Set(() => Log, ref _log, value);
                _runCommand.RaiseCanExecuteChanged();
            }
        }

        private string _uri;

        public string URI
        {
            get => _uri;
            set
            {
                Set(() => URI, ref _uri, value);
                _runCommand.RaiseCanExecuteChanged();
            }
        }

        private string _cookie;

        public string Cookie
        {
            get => _cookie;
            set
            {
                Set(() => Cookie, ref _cookie, value);
                _runCommand.RaiseCanExecuteChanged();
            }
        }

        private string _bookRootLocation;

        public string BookRootLocation
        {
            get => _bookRootLocation;
            set
            {
                Set(() => BookRootLocation, ref _bookRootLocation, value);
                _runCommand.RaiseCanExecuteChanged();
            }
        }

        private string _subFolder;

        public string Subfolder
        {
            get => _subFolder;
            set
            {
                Set(() => Subfolder, ref _subFolder, value);
                _runCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set { Set(() => IsEnabled, ref _isEnabled, value); }
        }

        #endregion
    }
}