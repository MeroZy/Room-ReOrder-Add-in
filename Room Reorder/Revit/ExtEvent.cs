using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room_Reorder.Revit
{
    public class ExtEventHan : IExternalEventHandler
    {
        public Request Request { get; set; }
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

                case Request.TreeRefresh:
                    if (ExtCmd.doc != null)
                    {
                        try
                        {
                            Room_Reorder.Helpers.RoomTreeHelper.PopulateTreeView(ExtCmd.doc, ExtCmd.Mainform.treeViewRooms);
                        }
                        catch (Exception ex)
                        {
                            TaskDialog.Show("Error", "Could not refresh tree: " + ex.Message);
                        }
                    }
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
        TreeRefresh
    }
}
