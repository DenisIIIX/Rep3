using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace Filter
{
    [Transaction(TransactionMode.Manual)]
    public class CommandClass : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Application application = uiApp.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Reference reference = uiDoc.Selection.PickObject(ObjectType.Element, "Выберете элемент");
            FamilyInstance familyInstance = doc.GetElement(reference) as FamilyInstance;

            var locationPoint = (familyInstance.Location as LocationPoint).Point;

            double number = 3;
            XYZ vector1 = new XYZ(2, 0.5, 0);
            XYZ vector2 = XYZ.BasisZ;
            XYZ vector3 = vector1.CrossProduct(vector2);

            using (var transaction = new Transaction(doc, "CreatePoint"))
            {
                transaction.Start();
                VisualizedAsVector(doc, vector1);
                //VisualizedAsVector(doc, vector2);
                //VisualizedAsVector(doc, vector3);
                XYZ point = XYZ.Zero;

                for (int i = 0; i< number; i++) 
                {
                    point += vector3;
                    VisualizedAsVector(doc, point);
                }

                transaction.Commit();
            }
            return Result.Succeeded;
        }
        private void VisualizedAsPoint(Document doc, XYZ point)
        {
            DirectShape directShape = DirectShape.CreateElement(doc, new ElementId(BuiltInCategory.OST_GenericModel));
            directShape.SetShape(new List<GeometryObject>() { Point.Create(point) });
        }

        private void VisualizedAsVector(Document doc, XYZ point)
        {
            DirectShape directShape = DirectShape.CreateElement(doc, new ElementId(BuiltInCategory.OST_GenericModel));
            directShape.SetShape(new List<GeometryObject>() { Line.CreateBound(XYZ.Zero, point) });
        }


    }

}
