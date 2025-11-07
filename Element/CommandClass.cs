using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ElementsFilter;
using System;
using System.Collections.Generic;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace ElGet

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

            try
            {
                IList<Reference> refs = uiDoc.Selection.PickObjects(ObjectType.Element, new ElemFilter(), "Выберете элемент'");

                Dictionary<string, int> elems = new Dictionary<string, int>();

                foreach (Reference r in refs)
                {
                    Element element = doc.GetElement(r);
                    string catName = element.Category?.Name ?? "Без категории";
                    if (elems.ContainsKey(catName))
                    {
                        elems[catName]++;
                    }
                    else
                    {
                        elems[catName] = 1;
                    }
                }

                TaskDialog.Show("Итог", $"Всего элементов: {refs.Count}");
                message+= "По категории: \n";
                foreach (var elem in elems)
                {
                    message += $"{elem.Key} = {elem.Value}\n";
                }

                TaskDialog.Show("Статистика элементов", message);
            }
            catch (OperationCanceledException)
            {
                TaskDialog.Show("Отмена", "Выбор отменен пользователем");
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Ошибка", $"Элементы не выбраны:{ex.Message}");
            }

            return Result.Succeeded;
        }

    }
}
