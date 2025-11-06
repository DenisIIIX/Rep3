using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;


namespace ElementsFilter
{
    internal class ElemFilter : ISelectionFilter
    {
        public bool AllowElement(Autodesk.Revit.DB.Element elem)
        {
            return elem is FamilyInstance;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
