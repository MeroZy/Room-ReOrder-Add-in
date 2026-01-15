using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Diagnostics;

namespace Room_Reorder.UI
{
    public partial class Room_ReOrder : LostForm
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
                    client.Timeout = TimeSpan.FromSeconds(5);

                    string json = await client.GetStringAsync(url);

                    return JsonSerializer.Deserialize<OnlineConfig>(json);
                }
            }
            catch
            {
                return null; // offline / invalid / unreachable
            }
        }
        private void Room_ReOrder_Load(object sender, EventArgs e)
        {
            ServerConnect();
        }
        string url = "https://gist.github.com/MeroZy/07e9b609484c2afd825e7367a486e84e#file-room-reorder-json";
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

            if (config == null || !config.Online)
            {
                statusCircle.IsGreen = false;
                return;
            }

            if (config.Version != VLbl.Text)
            {
                Updatelbl.Visible = true;
                updatelink = (config.Update_Url);
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
    }
}
