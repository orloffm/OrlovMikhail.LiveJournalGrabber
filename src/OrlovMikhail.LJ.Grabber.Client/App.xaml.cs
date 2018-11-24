using System.Windows;
using log4net;

namespace OrlovMikhail.LJ.Grabber.Client
{
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            Log.Info("Hello World");
            base.OnStartup(e);
        }
    }
}
