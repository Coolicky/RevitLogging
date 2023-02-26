using Autodesk.Revit.UI;
using Revit.DependencyInjection.Unity.Applications;
using Revit.DependencyInjection.Unity.Base;
using Revit.DependencyInjection.Unity.RibbonCommands;
using Revit.DependencyInjection.Unity.UI;
using Serilog;
using Unity;

namespace Coolicky.Revit.Logging.Test
{
    [ContainerProvider("3C244632-5957-42A7-8902-29DF61E902CC")]
    public class App : RevitApp
    {
        public override void OnCreateRibbon(IRibbonManager ribbonManager)
        {
            ribbonManager.AddRibbonCommands(config =>
            {
                config.TabName = "Coolicky";
                config.DefaultPanelName = "Logging Test";
            });
        }

        public override Result OnStartup(IUnityContainer container, UIControlledApplication application)
        {
            Log.Logger = new LoggerConfiguration()
                .CreateRevitConfiguration(application, "Test", "0.0.1")
                .WriteTo.File(@"PATH", shared: true)
                .WriteTo.Seq("URL", apiKey: "KEY")
                .CreateLogger();

            return Result.Succeeded;
        }

        public override Result OnShutdown(IUnityContainer container, UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}