using System.Windows.Input;

namespace OrlovMikhail.LJ.Grabber.Client.ViewModel
{
    public class DesignMainWindowVM : IMainWindowVM
    {
        public string BookRootLocation
        {
            get => "Base location";
            set { }
        }

        public string Cookie
        {
            get => "Cookie text";
            set { }
        }

        public bool IsEnabled => true;

        public string Log => "A\nB\nC\nD";

        public ICommand RunCommand => null;

        public ICommand SaveResultCommand => null;

        public string Subfolder
        {
            get => "Sub folder";
            set { }
        }

        public string URI
        {
            get => "http://galkovsky.livejournal.com/";
            set { }
        }

        public void SaveSettings()
        {
        }
    }
}