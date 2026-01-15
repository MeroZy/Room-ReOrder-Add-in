
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ReaLTaiizor.Forms;
using Room_Reorder.Revit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Room_Reorder.UI
{
    public partial class Room_ReOrder : System.Windows.Forms.Form
    {

        public Room_ReOrder()
        {
            InitializeComponent();
        }
        bool isGreen = true; // true = green, false = red
        public static async Task<OnlineConfig> GetOnlineConfig(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "RevitPlugin");

                    // --- THE CACHE BUSTER FIX ---
                    // We add a unique timestamp to the end of the URL.
                    // Example: .../Room%20Reorder.json?t=63840921
                    string cacheBuster = DateTime.Now.Ticks.ToString();
                    string finalUrl = url.Contains("?")
                        ? $"{url}&nocache={cacheBuster}"
                        : $"{url}?nocache={cacheBuster}";

                    string json = await client.GetStringAsync(finalUrl);

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<OnlineConfig>(json, options);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private void Room_ReOrder_Load(object sender, EventArgs e)
        {
            ServerConnect();

            //ExtCmd.ExtEventHan.Request = Request.TreeRefresh;
            //ExtCmd.ExtEvent.Raise();
        }
        string url = "https://gist.githubusercontent.com/MeroZy/07e9b609484c2afd825e7367a486e84e/raw/Room%2520Reorder.json";
        public string updatelink { get; set; }

        public class OnlineConfig
        {
            public string Version { get; set; }
            public bool Online { get; set; }
            public string Update_Url { get; set; }
        }
        private async void ServerConnect()
        {
            OnlineConfig config = await GetOnlineConfig(url);

            // Check if config is null OR if deserialization failed (Version is null)
            if (config == null || string.IsNullOrEmpty(config.Version))
            {
                // Connection failed or JSON format is wrong
                statusCircle.IsGreen = false;
                return;
            }

            // Now check the Online status flag from the JSON
            if (!config.Online)
            {
                statusCircle.IsGreen = false;
                return;
            }

            // If we get here, we are Online and Valid
            statusCircle.IsGreen = true;

            // Check for Updates
            // Compare trimmed strings to avoid whitespace issues
            if (config.Version != VLbl.Text)
            {
                Updatelbl.Visible = true;
                updatelink = config.Update_Url;
            }
        }

        private void Updatelbl_Click(object sender, EventArgs e)
        {
            Process.Start(updatelink);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/amrkhaled2/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Room_Reorder.Helpers.RoomTreeHelper.PopulateTreeView(ExtCmd.doc, this.treeViewRooms);
            ExtCmd.ExtEventHan.Request = Request.TreeViewer;
            ExtCmd.ExtEvent.Raise();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (treeViewRooms.SelectedNode == null || treeViewRooms.SelectedNode.Tag == null)
            {
                MessageBox.Show("Please select a room in the tree first.");
                return;
            }

            ElementId targetId = null;


            if (treeViewRooms.SelectedNode.Tag is SpatialElement room)
            {
                targetId = room.Id;
            }
            else if (treeViewRooms.SelectedNode.Tag is ElementId id)
            {
                targetId = id; 
            }

            if (targetId != null)
            {
                // Pass the ID to the handler
                ExtCmd.ExtEventHan.IdToSelect = targetId;

                ExtCmd.ExtEventHan.Request = Request.SelectElement;
                ExtCmd.ExtEvent.Raise();
            }
        }
    }
}
