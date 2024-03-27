using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace ButtonsStackTestProject
{
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	public class ExampleCommand : IExternalCommand
	{
		public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref String message, ElementSet elements)
		{
			try
			{
				Autodesk.Revit.DB.Document dbDoc = commandData.Application.ActiveUIDocument.Document;

				TaskDialog.Show("ExampleCommand", "Current document title: " + dbDoc.ProjectInformation.Name + "\nLocation: " + dbDoc.PathName);

			}
			catch(Exception e)
			{
				message = e.Message;
				return Autodesk.Revit.UI.Result.Failed;
			}

			return Autodesk.Revit.UI.Result.Succeeded;
		}
	}
}
