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

namespace Room_Reorder
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class ExtCmd : IExternalCommand
    {
        public static UIDocument uidoc { get; set; }
        public static Document doc { get; set; }
        public static Room_ReOrder Mainform { get; set; }
        public static ExtEventHan ExtEventHan { get; set; }
        public static ExternalEvent ExtEvent { get; set; }


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;

            ExtEventHan = new ExtEventHan();
            ExtEvent = ExternalEvent.Create(ExtEventHan);

            //MAIN
            Mainform = new Room_ReOrder();
            Mainform.Show();


            //RVTUtlis.Run();
            //TaskDialog.Show("Test",RVTUtlis.sb.ToString()); //for test
            //GetRoomData(doc);
            //RvtUtlis.PickPoint(uidoc);
            //RenumberingMethod.RenumberRooms(doc, "Level 1");

            return Result.Succeeded;
        }


        
    }
}
