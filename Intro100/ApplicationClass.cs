using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using System;
using System.Windows.Media.Imaging;

namespace Intro
{
    [Transaction(TransactionMode.Manual)]
    public class ApplicationClass : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            application.CreateRibbonTab("ПИК-Привет");
            var panel = application.CreateRibbonPanel("ПИК-Привет", "Общее");
            var button = new PushButtonData(
                "Hello",
                "Привет",
                "C:\\Users\\volkov_dv\\Documents\\SDK\\Software Development Kit\\Samples\\Reinforcement\\CS\\bin\\Debug\\Reinforcement.dll",
                "Revit.SDK.Samples.Reinforcement.CS.Command"
                );
            BitmapImage bitmapImage = new BitmapImage(new Uri("C:\\Users\\volkov_dv\\source\\repos\\DenisIIIX\\Rep3\\Intro100\\bin\\Debug\\png\\button.png", UriKind.Absolute));
            button.LargeImage = bitmapImage;
            panel.AddItem(button);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
