using System.Drawing;
using System.Windows.Forms;

namespace Room_Reorder.UI
{
    partial class Room_ReOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Room_ReOrder));
            this.treeViewRooms = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.separator1 = new ReaLTaiizor.Controls.Separator();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Updatelbl = new System.Windows.Forms.Label();
            this.VLbl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SearchTextBox = new ReaLTaiizor.Controls.AloneTextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LevelList = new ReaLTaiizor.Controls.AloneComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.aloneButton1 = new ReaLTaiizor.Controls.AloneButton();
            this.StartReOrderingbtn = new ReaLTaiizor.Controls.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.StartingPointXYZ = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.statusCircle = new Room_Reorder.UI.StatusCircle();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewRooms
            // 
            this.treeViewRooms.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewRooms.Location = new System.Drawing.Point(11, 317);
            this.treeViewRooms.Margin = new System.Windows.Forms.Padding(2);
            this.treeViewRooms.Name = "treeViewRooms";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Node0";
            this.treeViewRooms.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeViewRooms.Size = new System.Drawing.Size(466, 220);
            this.treeViewRooms.TabIndex = 0;
            this.treeViewRooms.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewRooms_NodeMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(16, 551);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Version ";
            // 
            // separator1
            // 
            this.separator1.LineColor = System.Drawing.Color.Gray;
            this.separator1.Location = new System.Drawing.Point(2, 541);
            this.separator1.Margin = new System.Windows.Forms.Padding(2);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(480, 10);
            this.separator1.TabIndex = 3;
            this.separator1.Text = "separator1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(398, 551);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dev. Amr Khaled";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(48, 551);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 4;
            // 
            // Updatelbl
            // 
            this.Updatelbl.AutoSize = true;
            this.Updatelbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Updatelbl.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Updatelbl.ForeColor = System.Drawing.Color.LimeGreen;
            this.Updatelbl.Location = new System.Drawing.Point(108, 551);
            this.Updatelbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Updatelbl.Name = "Updatelbl";
            this.Updatelbl.Size = new System.Drawing.Size(238, 14);
            this.Updatelbl.TabIndex = 1;
            this.Updatelbl.Text = "New update available - Click here to install";
            this.Updatelbl.Visible = false;
            this.Updatelbl.Click += new System.EventHandler(this.Updatelbl_Click);
            // 
            // VLbl
            // 
            this.VLbl.AutoSize = true;
            this.VLbl.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLbl.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.VLbl.Location = new System.Drawing.Point(55, 551);
            this.VLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VLbl.Name = "VLbl";
            this.VLbl.Size = new System.Drawing.Size(25, 13);
            this.VLbl.TabIndex = 1;
            this.VLbl.Text = "1.1.0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.SearchTextBox);
            this.groupBox1.Location = new System.Drawing.Point(5, 258);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 284);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rooms Viewer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Search";
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.BackColor = System.Drawing.Color.Transparent;
            this.SearchTextBox.EnabledCalc = true;
            this.SearchTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SearchTextBox.ForeColor = System.Drawing.Color.Gray;
            this.SearchTextBox.Location = new System.Drawing.Point(56, 19);
            this.SearchTextBox.MaxLength = 32767;
            this.SearchTextBox.MultiLine = false;
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.ReadOnly = false;
            this.SearchTextBox.Size = new System.Drawing.Size(355, 29);
            this.SearchTextBox.TabIndex = 0;
            this.SearchTextBox.Text = "Search rooms...";
            this.SearchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SearchTextBox.UseSystemPasswordChar = false;
            this.SearchTextBox.TextChanged += new System.EventHandler(this.aloneTextBox1_TextChanged);
            this.SearchTextBox.Enter += new System.EventHandler(this.SearchTextBox_Enter);
            this.SearchTextBox.Leave += new System.EventHandler(this.SearchTextBox_Leave);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::Room_Reorder.Properties.Resources.refresh_page_option;
            this.pictureBox2.Location = new System.Drawing.Point(422, 278);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 28);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::Room_Reorder.Properties.Resources.target;
            this.pictureBox1.Location = new System.Drawing.Point(450, 276);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // LevelList
            // 
            this.LevelList.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.LevelList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LevelList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LevelList.EnabledCalc = true;
            this.LevelList.FormattingEnabled = true;
            this.LevelList.ItemHeight = 20;
            this.LevelList.Items.AddRange(new object[] {
            "Choose Ground Level..."});
            this.LevelList.Location = new System.Drawing.Point(12, 170);
            this.LevelList.Name = "LevelList";
            this.LevelList.Size = new System.Drawing.Size(316, 26);
            this.LevelList.TabIndex = 12;
            this.LevelList.Click += new System.EventHandler(this.LevelList_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Choose project (GROUND LEVEL)";
            // 
            // aloneButton1
            // 
            this.aloneButton1.BackColor = System.Drawing.Color.Transparent;
            this.aloneButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aloneButton1.EnabledCalc = true;
            this.aloneButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.aloneButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.aloneButton1.Location = new System.Drawing.Point(334, 170);
            this.aloneButton1.Name = "aloneButton1";
            this.aloneButton1.Size = new System.Drawing.Size(144, 26);
            this.aloneButton1.TabIndex = 14;
            this.aloneButton1.Text = "Choose starting point";
            this.aloneButton1.Click += new ReaLTaiizor.Controls.AloneButton.ClickEventHandler(this.aloneButton1_Click);
            // 
            // StartReOrderingbtn
            // 
            this.StartReOrderingbtn.BackColor = System.Drawing.Color.Transparent;
            this.StartReOrderingbtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.StartReOrderingbtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.StartReOrderingbtn.EnteredBorderColor = System.Drawing.Color.DeepSkyBlue;
            this.StartReOrderingbtn.EnteredColor = System.Drawing.Color.SkyBlue;
            this.StartReOrderingbtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartReOrderingbtn.Image = null;
            this.StartReOrderingbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StartReOrderingbtn.InactiveColor = System.Drawing.Color.DeepSkyBlue;
            this.StartReOrderingbtn.Location = new System.Drawing.Point(12, 220);
            this.StartReOrderingbtn.Name = "StartReOrderingbtn";
            this.StartReOrderingbtn.PressedBorderColor = System.Drawing.Color.LightSkyBlue;
            this.StartReOrderingbtn.PressedColor = System.Drawing.Color.DeepSkyBlue;
            this.StartReOrderingbtn.Size = new System.Drawing.Size(466, 32);
            this.StartReOrderingbtn.TabIndex = 15;
            this.StartReOrderingbtn.Text = "Start ReOrdering";
            this.StartReOrderingbtn.TextAlignment = System.Drawing.StringAlignment.Center;
            this.StartReOrderingbtn.Click += new System.EventHandler(this.StartReOrderingbtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label6.Location = new System.Drawing.Point(14, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Starting Point XYZ :";
            // 
            // StartingPointXYZ
            // 
            this.StartingPointXYZ.AutoSize = true;
            this.StartingPointXYZ.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.StartingPointXYZ.Location = new System.Drawing.Point(112, 201);
            this.StartingPointXYZ.Name = "StartingPointXYZ";
            this.StartingPointXYZ.Size = new System.Drawing.Size(25, 13);
            this.StartingPointXYZ.TabIndex = 17;
            this.StartingPointXYZ.Text = "0, 0";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(23, 25);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(433, 102);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            // 
            // statusCircle
            // 
            this.statusCircle.IsGreen = false;
            this.statusCircle.Location = new System.Drawing.Point(4, 554);
            this.statusCircle.Margin = new System.Windows.Forms.Padding(2);
            this.statusCircle.Name = "statusCircle";
            this.statusCircle.Size = new System.Drawing.Size(10, 9);
            this.statusCircle.TabIndex = 5;
            this.statusCircle.Text = "statusCircle1";
            // 
            // Room_ReOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(486, 570);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.StartingPointXYZ);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.StartReOrderingbtn);
            this.Controls.Add(this.aloneButton1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LevelList);
            this.Controls.Add(this.statusCircle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.separator1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Updatelbl);
            this.Controls.Add(this.VLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeViewRooms);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Room_ReOrder";
            this.Text = "Room ReOrder";
            this.Load += new System.EventHandler(this.Room_ReOrder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion

        public System.Windows.Forms.TreeView treeViewRooms;
        private System.Windows.Forms.Label label1;
        private ReaLTaiizor.Controls.Separator separator1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Updatelbl;
        private StatusCircle statusCircle;
        private Label VLbl;
        private PictureBox pictureBox1;
        private GroupBox groupBox1;
        private PictureBox pictureBox2;
        private ReaLTaiizor.Controls.AloneComboBox LevelList;
        private Label label5;
        private ReaLTaiizor.Controls.AloneButton aloneButton1;
        private ReaLTaiizor.Controls.Button StartReOrderingbtn;
        private Label label6;
        public Label StartingPointXYZ;
        private ToolTip toolTip1;
        private PictureBox pictureBox3;
        private Label label4;
        private ReaLTaiizor.Controls.AloneTextBox SearchTextBox;
    }
}

