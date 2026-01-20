using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Room_Reorder.Revit;
using Room_Reorder.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Room_Reorder.Methods;

namespace Room_Reorder.Revit
{
    static class RvtUtlis
    {
        /// <summary>
        /// pick a point in the Revit Model
        /// </summary>
        /// <param name="uidoc">The active UIDocument.</param>
        public static void PickPoint(UIDocument uidoc)
        {

            try
            {
                RvtData.StartingPoint = uidoc.Selection.PickPoint("Select the starting point for room reordering (Place it on the lobby or near the Entrance)");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                TaskDialog.Show("Canceled", "No Point Selected, Operation canceld by User");
                return;
            }
        }

        /// <summary>
        /// get all level names in the document
        /// </summary>
        /// <param name="doc">The Revit document.</param>
        /// <returns>A list of level names.</returns>
        public static List<string> GetLevelsNames(Document doc)
        {
            List<string> levelNames = new FilteredElementCollector(doc)
                                      .OfClass(typeof(Level))
                                      .Cast<Level>()
                                      .OrderBy(lvl => lvl.Elevation)
                                      .Select(lvl => lvl.Name)
                                      .ToList();
            return levelNames;
        }
    }
}
