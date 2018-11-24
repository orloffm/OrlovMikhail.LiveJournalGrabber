using System.Reflection;
using System.Windows;
using log4net;
using log4net.Config;

namespace OrlovMikhail.LJ.Grabber.Client
{
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetLogger(
            MethodBase.GetCurrentMethod()
                .DeclaringType
        );

        protected override void OnStartup(StartupEventArgs e)
        {
            XmlConfigurator.Configure();
            Log.Info("Hello World");
            base.OnStartup(e);
        }
    }
}