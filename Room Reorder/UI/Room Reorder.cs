using Autodesk.Revit.DB;
using Newtonsoft.Json;
using Room_Reorder.Revit;
using System;
using Room_Reorder.Helpers;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;


namespace Room_Reorder.UI
{
    public partial class Room_ReOrder : System.Windows.Forms.Form
    {
        // ========== Properties ==========
        public string Message { get; set; }
        private bool _isUpdatingSearch = false;
        private bool _targetRoomQueued = false;
        private bool _suppressSearchTextChanged = false;

        public Room_ReOrder()
        {
            InitializeComponent();
        }
        private void Room_ReOrder_Load(object sender, EventArgs e)
        {
            RefreshViewer();
            ServerConnect();
            ToolTipInfo();
        }

        // ========== online server connection ==========

        string url = "https://gist.githubusercontent.com/MeroZy/07e9b609484c2afd825e7367a486e84e/raw/Room%20Reorder.json";
        public string updatelink { get; set; }

        /// <summary>
        /// Represents the online configuration settings.
        /// </summary>
        public class OnlineConfig
        {
            public string Version { get; set; }
            public bool Online { get; set; }
            public string Update_Url { get; set; }
        }

        /// <summary>
        /// Fetches the online configuration from the specified URL.
        /// </summary>
        /// <param name="url">The URL to fetch the configuration from.</param>
        /// <returns>A task representing the asynchronous operation, with the online configuration as the result.</returns>
        public static async Task<OnlineConfig> GetOnlineConfig(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "RevitPlugin");

                    string cacheBuster = DateTime.Now.Ticks.ToString();
                    string finalUrl = url.Contains("?")
                        ? $"{url}&nocache={cacheBuster}"
                        : $"{url}?nocache={cacheBuster}";

                    string json = await client.GetStringAsync(finalUrl);

                    return JsonConvert.DeserializeObject<OnlineConfig>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // TEMP debug
                return null;
            }
        }

        /// <summary>
        /// Connects to the online server and checks its status.
        /// </summary>
        private async void ServerConnect()
        {
            //label4.Text = "Checking server...";
            statusCircle.IsGreen = false;

            OnlineConfig config = await GetOnlineConfig(url);

            if (config == null)
            {
                //label4.Text = "Could not connect to server.";
                statusCircle.IsGreen = false;
                return;
            }

            statusCircle.IsGreen = config.Online;

            if (!config.Online)
            {
                //label4.Text = "Server offline";
                statusCircle.IsGreen = false;
                return;
            }

            //label4.Text = $"Version: {config.Version}";

            if (!string.IsNullOrWhiteSpace(config.Version) &&
                !string.Equals(config.Version.Trim(), VLbl.Text.Trim()))
            {
                Updatelbl.Visible = true;
                updatelink = config.Update_Url;
            }
            else
            {
                Updatelbl.Visible = false;
            }
        }

        // ========== Functions ===========
        /// <summary>
        /// Restores the full room tree from the cached nodes.
        /// </summary>
        private void RestoreFullTree()
        {
            if (RoomTreeHelper._allNodes == null)
                return;

            treeViewRooms.BeginUpdate();
            treeViewRooms.Nodes.Clear();

            foreach (TreeNode node in RoomTreeHelper._allNodes)
                treeViewRooms.Nodes.Add(RoomTreeHelper.CloneNode(node));

            // Expand all nodes and set top node
            treeViewRooms.ExpandAll();
            if (treeViewRooms.Nodes.Count > 0)
            {
                treeViewRooms.TopNode = treeViewRooms.Nodes[0];
            }

            treeViewRooms.EndUpdate();
        }

        /// <summary>
        /// Runs the search operation in a deferred manner to avoid UI blocking.
        /// </summary>
        private void RunSearchDeferred()
        {
            if (_isUpdatingSearch)
            {
                // Try again AFTER current UI work finishes
                BeginInvoke(new Action(RunSearchDeferred));
                return;
            }

            PerformSearch();
        }

        /// <summary>
        /// performs the search operation and updates the room tree view.
        /// </summary>
        private void PerformSearch()
        {
            if (RoomTreeHelper._allNodes == null)
                return;

            _isUpdatingSearch = true;

            try
            {
                string text = SearchTextBox.Text.ToLower();

                treeViewRooms.BeginUpdate();
                treeViewRooms.Nodes.Clear();

                if (string.IsNullOrWhiteSpace(text))
                {
                    foreach (TreeNode node in RoomTreeHelper._allNodes)
                        treeViewRooms.Nodes.Add(RoomTreeHelper.CloneNode(node));
                }
                else
                {
                    foreach (TreeNode node in RoomTreeHelper._allNodes)
                    {
                        TreeNode filtered = FilterNode(node, text);
                        if (filtered != null)
                            treeViewRooms.Nodes.Add(filtered);
                    }
                }

                treeViewRooms.ExpandAll();
            }
            finally
            {
                treeViewRooms.EndUpdate();
                _isUpdatingSearch = false;
            }
        }

        /// <summary>
        /// targets the selected room in the Revit model.
        /// </summary>
        private void TargetRoom()
        {
            if (_isUpdatingSearch)
            {
                if (!_targetRoomQueued)
                {
                    _targetRoomQueued = true;
                    BeginInvoke(new Action(() =>
                    {
                        _targetRoomQueued = false;
                        TargetRoom();
                    }));
                }
                return;
            }

            if (treeViewRooms.SelectedNode == null || treeViewRooms.SelectedNode.Tag == null)
            {
                MessageBox.Show("Please select a room in the tree first.");
                return;
            }

            ElementId targetId = null;

            if (treeViewRooms.SelectedNode.Tag is SpatialElement room)
                targetId = room.Id;
            else if (treeViewRooms.SelectedNode.Tag is ElementId id)
                targetId = id;

            if (targetId != null)
            {
                ExtCmd.ExtEventHan.IdToSelect = targetId;
                ExtCmd.ExtEventHan.Request = Request.SelectElement;
                ExtCmd.ExtEvent.Raise();
            }
        }

        /// <summary>
        /// refreshes the room tree viewer.
        /// </summary>
        public void RefreshViewer()
        {
            ExtCmd.ExtEventHan.Request = Request.TreeViewer;
            ExtCmd.ExtEvent.Raise();
        }

        /// <summary>
        /// renumbers the rooms based on the selected starting point and ground level.
        /// </summary>
        private void Renumbering()
        {
            ExtCmd.ExtEventHan.Request = Request.ReNumbering;
            ExtCmd.ExtEvent.Raise();
        }

        /// <summary>
        /// selects the starting point in the Revit model.
        /// </summary>
        private void SelectStartingPointXYZ()
        {
            ExtCmd.ExtEventHan.Request = Request.SelectPoint;
            ExtCmd.ExtEvent.Raise();
        }

        /// <summary>
        /// tooltip information for UI elements.
        /// </summary>
        private void ToolTipInfo()
        {
            toolTip1.SetToolTip(pictureBox1, "Target Selected Room");
            toolTip1.SetToolTip(pictureBox2, "Refresh Room Tree");
            toolTip1.SetToolTip(aloneButton1, "Select Starting Point");
            toolTip1.SetToolTip(StartReOrderingbtn, "Start Renumbering");
            toolTip1.SetToolTip(LevelList, "Select Ground Level");
        }

        /// <summary>
        /// filters a tree node and its children based on the search text.
        /// </summary>
        /// <param name="sourceNode"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        private TreeNode FilterNode(TreeNode sourceNode, string searchText)
        {
            bool isMatch = sourceNode.Text
                .ToLower()
                .Contains(searchText);

            TreeNode newNode = new TreeNode(sourceNode.Text)
            {
                Tag = sourceNode.Tag
            };

            foreach (TreeNode child in sourceNode.Nodes)
            {
                TreeNode filteredChild = FilterNode(child, searchText);
                if (filteredChild != null)
                    newNode.Nodes.Add(filteredChild);
            }

            // Keep node if:
            // - it matches
            // - OR it has matching children
            if (isMatch || newNode.Nodes.Count > 0)
                return newNode;

            return null;
        }

        // ========== Events ==========
        private void Updatelbl_Click(object sender, EventArgs e)
        {
            Process.Start(updatelink);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/amrkhaled2/");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TargetRoom();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RefreshViewer();
        }

        
        private void LevelList_Click(object sender, EventArgs e)
        {
            var oldLevel = LevelList.Text;
            var levels = RvtUtlis.GetLevelsNames(ExtCmd.doc);
            LevelList.Items.Clear();
            foreach (var level in levels)
            {
                LevelList.Items.Add(level);
            }
            LevelList.Text = oldLevel;
        }
        private void aloneButton1_Click(object sender, EventArgs e)
        {
            SelectStartingPointXYZ();
        }

        private void StartReOrderingbtn_Click(object sender, EventArgs e)
        {
            if (LevelList.Text == "")
            {
                MessageBox.Show("Please select a ground level from the list.");
                return;
            }
            else
            {
                RvtData.GroundLevelName = LevelList.Text;
                Renumbering();
            }
        }

        private void treeViewRooms_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Toggle expand/collapse on double-click
            treeViewRooms.BeginUpdate();
            e.Node.Toggle();
            treeViewRooms.EndUpdate();

            // Target the room
            TargetRoom();
        }

        private void aloneTextBox1_TextChanged(object sender, EventArgs e)
        {

            if (_suppressSearchTextChanged)
                return;

            if (SearchTextBox.ForeColor == Color.Gray)
                return;

            RunSearchDeferred();
            if (treeViewRooms.Nodes.Count > 0)
            {
                treeViewRooms.TopNode = treeViewRooms.Nodes[0];
            }
        }

        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            if (SearchTextBox.ForeColor == Color.Gray)
            {
                _suppressSearchTextChanged = true;

                SearchTextBox.Text = "";
                SearchTextBox.ForeColor = Color.Black;

                _suppressSearchTextChanged = false;
            }
        }

        private void SearchTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                _suppressSearchTextChanged = true;

                SearchTextBox.Text = "Search rooms...";
                SearchTextBox.ForeColor = Color.Gray;

                _suppressSearchTextChanged = false;

                RestoreFullTree();

            }
        }
    }
    
}
