using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;
using System.Xml.Linq;

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

            FilteredElementCollector filteredElementCollector = new FilteredElementCollector(doc, doc.ActiveView.Id);
            var walls = filteredElementCollector
                .OfClass(typeof(Wall))
                .OfType<Wall>()
                .ToList();

            if (walls.Count == 0)
            {
                TaskDialog.Show("Предупреждение", "В текущем виде нет стен!");
                return Result.Succeeded;
            }
            using (Transaction t = new Transaction(doc, "Длина в комментарий"))
            {
                t.Start();

                int count = 0;
                double maxL = double.MinValue;
                double minL = double.MaxValue;

                foreach (Wall wall in walls)
                {
                    Parameter lengthParam = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                    if (lengthParam == null || !lengthParam.HasValue) continue;

                    double lengthM = UnitUtils.ConvertFromInternalUnits(
                        lengthParam.AsDouble(), DisplayUnitType.DUT_METERS);

                    if (lengthM > maxL) maxL = lengthM;
                    if (lengthM < minL) minL = lengthM;

                    count++;
                }

                foreach (Wall wall in walls)
                {
                    Parameter lengthParam = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                    if (lengthParam == null || !lengthParam.HasValue) continue;

                    Parameter commentParam = wall.LookupParameter("Комментарии");
                    if (commentParam == null || commentParam.IsReadOnly) continue;

                    double lengthM = UnitUtils.ConvertFromInternalUnits(
                         lengthParam.AsDouble(), DisplayUnitType.DUT_METERS);

                    if (lengthM == maxL)
                    {
                        commentParam.Set("Максимальная длина");
                    }
                    else if (lengthM == minL)
                    {
                        commentParam.Set("Минимальная длина");
                    }
                    else commentParam.Set("");
                    
                }
                t.Commit();
                TaskDialog.Show("Готово", $"Обработано стен: {count}\n"+$"Максимальная длина: {maxL:F2} м\n"+$"Минимальная длина: {minL:F2} м");
            }
            return Result.Succeeded;
        }
    }
}
