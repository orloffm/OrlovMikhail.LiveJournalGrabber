using System.Windows;
using System.Windows.Controls;
using OrlovMikhail.LJ.Grabber.Client.ViewModel;

namespace OrlovMikhail.LJ.Grabber.Client.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtLog.ScrollToEnd();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IMainWindowVM vm = this.DataContext as IMainWindowVM;
            vm.SaveSettings();
        }
    }
}
