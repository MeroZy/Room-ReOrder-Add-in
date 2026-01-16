using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Room_Reorder.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                    RvtUtlis.PickPoint(ExtCmd.uidoc);
                    ExtCmd.Mainform.StartingPointXYZ.Text = $"{RvtData.StartingPoint.X}, {RvtData.StartingPoint.Y}";
                    ExtCmd.Mainform.Focus();
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
                    RenumberingMethod.RenumberRooms(ExtCmd.doc, RvtData.GroundLevelName);
                    ExtCmd.Mainform. RefreshViewer();
                    ExtCmd.Mainform.Focus();
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
                    if (ExtCmd.Mainform.treeViewRooms.Nodes.Count > 0)
                    {
                        ExtCmd.Mainform.treeViewRooms.TopNode = ExtCmd.Mainform.treeViewRooms.Nodes[0];
                    }
                    Room_Reorder.Helpers.RoomTreeHelper.CacheFullTree();
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
