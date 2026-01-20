using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Room_Reorder.Revit
{
    public class ExtApp : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication uicApp)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication uicApp)
        {
            return RunApp(uicApp);
        }
        public static Result RunApp(UIControlledApplication uicApp)
        {
            CreatePluginUI(uicApp);

            return Result.Succeeded;
        }

        /// <summary>
        /// Tries to create the About button if this version is the final winner
        /// </summary>
        /// <param name="uicApp"></param>
        private static void CreatePluginUI(UIControlledApplication uicApp)
        {
            string tabName = "EasyRVT";
            string panelName = "Architecture";

            try { uicApp.CreateRibbonTab(tabName); } catch { }

            RibbonPanel panel =
                uicApp.GetRibbonPanels(tabName)
                    .FirstOrDefault(p => p.Name == panelName)
                ?? uicApp.CreateRibbonPanel(tabName, panelName);

            Assembly assembly = Assembly.GetExecutingAssembly();

            PushButtonData pbData = new PushButtonData(
                "Rooms_ReOrder",
                "Rooms ReOrder",
                assembly.Location,
                "Room_Reorder.ExtCmd"
            );

            PushButton pb = panel.AddItem(pbData) as PushButton;
            pb.ToolTip = "This is Room ReOrder Add-in Developed By Amr Khaled";

            Bitmap icon = Properties.Resources.icons8_room_80;
            pb.LargeImage = GetImageSource(new Bitmap(icon, new Size(32, 32)));
            pb.Image = GetImageSource(new Bitmap(icon, new Size(16, 16)));
        }

        // ================= Image Conversion =================
        public static ImageSource GetImageSource(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                return new PngBitmapDecoder(
                    ms,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.OnLoad
                ).Frames[0];
            }
        }
    }


}
