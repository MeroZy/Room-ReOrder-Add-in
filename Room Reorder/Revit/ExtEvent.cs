using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Room_Reorder.Revit
{
    public class ExtEventHan : IExternalEventHandler
    {
        public Request Request { get; set; }
        public ElementId IdToSelect { get; set; }
        public void Execute(UIApplication app)
        {
            switch (Request)
            {
                case Request.SelectPoint:
                    //RvtUtlis.Analysis(ExtCmd.doc, ExtCmd.uidoc, RvtData.start_range, RvtData.end_range);
                    break;


                case Request.SelectLevel:
                    using (var tns = new Transaction(ExtCmd.doc, "Clear Existing"))
                    {
                        tns.Start();
                        //Methods.VisualizeMethod.ClearExisting(ExtCmd.doc);
                        tns.Commit();
                    }
                    break;

                case Request.ReNumbering:
                    //RvtUtlis.SelectFloors(ExtCmd.uidoc);
                    break;

                case Request.SelectElement:
                    if (IdToSelect != null)
                    {
                        try
                        {
                            //select
                            ExtCmd.uidoc.Selection.SetElementIds(new List<ElementId> { IdToSelect });

                            //focus
                            ExtCmd.uidoc.ShowElements(IdToSelect);
                        }
                        catch { } 
                    }
                    break;
                case Request.TreeViewer:
                    Room_Reorder.Helpers.RoomTreeHelper.PopulateTreeView(ExtCmd.doc, ExtCmd.Mainform.treeViewRooms);
                    break;

            }
            
        }

        public string GetName()
        {
            return "Slope Analysis";
        }
    }
    public enum Request
    {
        SelectPoint,
        ReNumbering,
        SelectLevel,
        SelectElement,
        TreeViewer

    }
}
