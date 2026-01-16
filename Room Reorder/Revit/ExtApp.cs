using Autodesk.Revit.UI;
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

        /// <summary>
        /// run the application to create ribbon tab, panel and button
        /// </summary>
        /// <param name="uicApp"></param>
        /// <returns></returns>
        public static Result RunApp(UIControlledApplication uicApp)
        {
            string tabName = "KAITECH-BD-R09"; //EasyRVT
            string panelName = "Architecture";

            try
            {
                uicApp.CreateRibbonTab(tabName);
            }
            catch (Exception) { }

            RibbonPanel panel = uicApp.GetRibbonPanels(tabName)
                                        .FirstOrDefault(p => p.Name == panelName);

            if (panel == null)
            {
                panel = uicApp.CreateRibbonPanel(tabName, panelName);
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            PushButtonData pb_Data = new PushButtonData(
                "Rooms ReOrder",
                "Rooms ReOrder",
                assembly.Location,
                "Room_Reorder.ExtCmd" //extcmd full name 
            );

            PushButton pb = panel.AddItem(pb_Data) as PushButton;
            pb.ToolTip = "This is Room ReOrder Add-in Developed By Amr Khaled";

            Bitmap originalBitmap = Properties.Resources.logoRenumbersq__1_;

            Bitmap resizedLarge = new Bitmap(originalBitmap, new Size(32, 32));
            pb.LargeImage = GetImageSource(resizedLarge);

            Bitmap resizedSmall = new Bitmap(originalBitmap, new Size(16, 16));
            pb.Image = GetImageSource(resizedSmall);

            return Result.Succeeded;
        }

        /// <summary>
        /// convert System.Drawing.Bitmap to ImageSource for Revit button icons
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static ImageSource GetImageSource(System.Drawing.Bitmap bitmap) //gpt help
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                PngBitmapDecoder decoder = new PngBitmapDecoder(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }
    }
        
}
