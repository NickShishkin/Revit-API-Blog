using System;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using AW = Autodesk.Windows;

namespace ButtonsStackTestProject
{
    public class StackedButtonAppbyScottWilson: IExternalApplication
    {
        static AddInId addInId = new AddInId(new Guid("090CE0A9-93DF-4AFF-BBE8-9665B4B0BB26"));

        private readonly string assemblyLocation = Assembly.GetExecutingAssembly().Location;

        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "New Tab";
            application.CreateRibbonTab(tabName);

            string panelName = "Solution#1";
            Autodesk.Revit.UI.RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, panelName);

            // Set Image
            BitmapImage smallImage = new BitmapImage(new Uri(@"/ButtonsStackTestProject;component/Resources/wheel16.png", UriKind.RelativeOrAbsolute));
            BitmapImage midleImage = new BitmapImage(new Uri(@"/ButtonsStackTestProject;component/Resources/wheel24.png", UriKind.RelativeOrAbsolute));
            BitmapImage largeImage = new BitmapImage(new Uri(@"/ButtonsStackTestProject;component/Resources/wheel32.png", UriKind.RelativeOrAbsolute));

            // Set Button Name
            string buttonName1 = "ButtonTest1";
            string buttonName2 = "ButtonTest2";
            string buttonName3 = "ButtonTest3";
            string buttonName4 = "ButtonTest4";
            string buttonName5 = "ButtonTest5";

            // Create push buttons
            PushButtonData buttondata1 = new PushButtonData(buttonName1, "buttonTextTest1", assemblyLocation, typeof(ExampleCommand).FullName);
            buttondata1.Image = smallImage;
            buttondata1.LargeImage = midleImage;

            PushButtonData buttondata2 = new PushButtonData(buttonName2, "buttonTextTest2", assemblyLocation, typeof(ExampleCommand).FullName);
            buttondata2.Image = smallImage;
            buttondata2.LargeImage = largeImage;

            PushButtonData buttondata3 = new PushButtonData(buttonName3, "buttonTextTest3", assemblyLocation, typeof(ExampleCommand).FullName);
            buttondata3.Image = smallImage;

            PushButtonData buttondata4 = new PushButtonData(buttonName4, "buttonTextTest4", assemblyLocation, typeof(ExampleCommand).FullName);
            buttondata4.Image = smallImage;

            PushButtonData buttondata5 = new PushButtonData(buttonName5, "buttonTextTest5", assemblyLocation, typeof(ExampleCommand).FullName);
            buttondata5.Image = smallImage;

            // Create StackedItem
            IList<Autodesk.Revit.UI.RibbonItem> ribbonItems = ribbonPanel.AddStackedItems(buttondata1, buttondata2);
            ribbonPanel.AddSeparator();
            IList<Autodesk.Revit.UI.RibbonItem> ribbonItems2 = ribbonPanel.AddStackedItems(buttondata3, buttondata4, buttondata5);

            // Find Autodes.Windows.RibbonItems
            var btnTest1 = GetButton(tabName, panelName, buttonName1);
            var btnTest2 = GetButton(tabName, panelName, buttonName2);
            var btnTest3 = GetButton(tabName, panelName, buttonName3);
            var btnTest4 = GetButton(tabName, panelName, buttonName4);
            var btnTest5 = GetButton(tabName, panelName, buttonName5);

            // Set Size and Text Visibility
            btnTest1.Size = AW.RibbonItemSize.Large;
            btnTest1.ShowText = false;

            btnTest2.Size = AW.RibbonItemSize.Large;
            btnTest2.ShowText = false;

            btnTest3.ShowText = false;
            btnTest4.ShowText = false;
            btnTest5.ShowText = false;

            return Result.Succeeded;
        }
        public AW.RibbonItem GetButton(string tabName, string panelName, string itemName)
        {
            AW.RibbonControl ribbon = AW.ComponentManager.Ribbon;
            foreach (AW.RibbonTab tab in ribbon.Tabs)
            {
                if (tab.Name == tabName)
                {
                    foreach (AW.RibbonPanel panel in tab.Panels)
                    {
                        if (panel.Source.Title == panelName)
                        {
                            return panel.FindItem("CustomCtrl_%CustomCtrl_%"
                              + tabName + "%" + panelName + "%" + itemName,
                              true) as AW.RibbonItem;
                        }
                    }
                }
            }
            return null;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}