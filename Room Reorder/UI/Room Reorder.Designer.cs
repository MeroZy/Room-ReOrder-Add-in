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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewRooms = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.separator1 = new ReaLTaiizor.Controls.Separator();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Updatelbl = new System.Windows.Forms.Label();
            this.VLbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.statusCircle = new Room_Reorder.UI.StatusCircle();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeViewRooms
            // 
            this.treeViewRooms.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewRooms.Location = new System.Drawing.Point(5, 30);
            this.treeViewRooms.Margin = new System.Windows.Forms.Padding(2);
            this.treeViewRooms.Name = "treeViewRooms";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Node0";
            this.treeViewRooms.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeViewRooms.Size = new System.Drawing.Size(470, 378);
            this.treeViewRooms.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(16, 484);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Version ";
            // 
            // separator1
            // 
            this.separator1.LineColor = System.Drawing.Color.Gray;
            this.separator1.Location = new System.Drawing.Point(2, 474);
            this.separator1.Margin = new System.Windows.Forms.Padding(2);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(480, 10);
            this.separator1.TabIndex = 3;
            this.separator1.Text = "separator1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(398, 484);
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
            this.label3.Location = new System.Drawing.Point(48, 484);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 4;
            // 
            // Updatelbl
            // 
            this.Updatelbl.AutoSize = true;
            this.Updatelbl.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Updatelbl.ForeColor = System.Drawing.Color.LimeGreen;
            this.Updatelbl.Location = new System.Drawing.Point(108, 484);
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
            this.VLbl.Location = new System.Drawing.Point(55, 484);
            this.VLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VLbl.Name = "VLbl";
            this.VLbl.Size = new System.Drawing.Size(25, 13);
            this.VLbl.TabIndex = 1;
            this.VLbl.Text = "1.1.0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 423);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 37);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusCircle
            // 
            this.statusCircle.IsGreen = false;
            this.statusCircle.Location = new System.Drawing.Point(4, 487);
            this.statusCircle.Margin = new System.Windows.Forms.Padding(2);
            this.statusCircle.Name = "statusCircle";
            this.statusCircle.Size = new System.Drawing.Size(10, 9);
            this.statusCircle.TabIndex = 5;
            this.statusCircle.Text = "statusCircle1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(197, 423);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 37);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Room_ReOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(486, 502);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusCircle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.separator1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Updatelbl);
            this.Controls.Add(this.VLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeViewRooms);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Room_ReOrder";
            this.Text = "Room ReOrder";
            this.Load += new System.EventHandler(this.Room_ReOrder_Load);
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
        private Button button1;
        private Button button2;
    }
}

