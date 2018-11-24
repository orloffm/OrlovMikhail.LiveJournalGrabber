using System.Windows.Input;

namespace OrlovMikhail.LJ.Grabber.Client.ViewModel
{
    public interface IMainWindowVM
    {
        string BookRootLocation { get; set; }

        string Cookie { get; set; }

        bool IsEnabled { get; }

        string Log { get; }

        ICommand RunCommand { get; }

        string Subfolder { get; set; }

        string URI { get; set; }

        void SaveSettings();
    }
}