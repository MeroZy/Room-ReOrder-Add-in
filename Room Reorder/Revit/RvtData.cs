using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room_Reorder.Revit
{
    public class RvtData
    {
        //double start_range = double.Parse(Mainform.tb_start_range.Text);
        //double end_range = double.Parse(Mainform.tb_end_range.Text);

        public static string groundLevelName { get; set; }
        public static XYZ userStartPoint { get; set; }

    }
}
