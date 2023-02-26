using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit.DependencyInjection.Unity.Commands;
using Revit.DependencyInjection.Unity.RibbonCommands.Attributes;
using Serilog;
using Unity;

namespace Coolicky.Revit.Logging.Test
{
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    [RibbonPushButton(
        PanelName = "Logging Test",
        FirstLine = "Test"
    )]
    public class Command : RevitAppCommand<App>
    {
        public override Result Execute(IUnityContainer container, ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Log.Logger.Activity(new Activity
            {
                Name = "Test",
                Description = "Description",
                Details = new[] { "First", "Second" }
            });
            
            Log.Logger.Error(new Exception("Whatever Men"), "Error Occured in testing");

            return Result.Succeeded;
        }
    }
}