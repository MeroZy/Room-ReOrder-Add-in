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
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.separator1 = new ReaLTaiizor.Controls.Separator();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Updatelbl = new System.Windows.Forms.Label();
            this.VLbl = new System.Windows.Forms.Label();
            this.statusCircle = new Room_Reorder.UI.StatusCircle();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(69, 80);
            this.treeView1.Name = "treeView1";
            treeNode3.Name = "Node1";
            treeNode3.Text = "Node1";
            treeNode4.Name = "Node0";
            treeNode4.Text = "Node0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(121, 97);
            this.treeView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(25, 406);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Version ";
            // 
            // separator1
            // 
            this.separator1.LineColor = System.Drawing.Color.Gray;
            this.separator1.Location = new System.Drawing.Point(5, 393);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(678, 10);
            this.separator1.TabIndex = 3;
            this.separator1.Text = "separator1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(598, 406);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dev. Amr Khaled";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(73, 406);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 4;
            // 
            // Updatelbl
            // 
            this.Updatelbl.AutoSize = true;
            this.Updatelbl.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Updatelbl.ForeColor = System.Drawing.Color.LimeGreen;
            this.Updatelbl.Location = new System.Drawing.Point(93, 406);
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
            this.VLbl.Location = new System.Drawing.Point(63, 406);
            this.VLbl.Name = "VLbl";
            this.VLbl.Size = new System.Drawing.Size(25, 13);
            this.VLbl.TabIndex = 1;
            this.VLbl.Text = "1.1.0";
            // 
            // statusCircle
            // 
            this.statusCircle.IsGreen = true;
            this.statusCircle.Location = new System.Drawing.Point(8, 405);
            this.statusCircle.Name = "statusCircle";
            this.statusCircle.Size = new System.Drawing.Size(15, 15);
            this.statusCircle.TabIndex = 5;
            this.statusCircle.Text = "statusCircle1";
            // 
            // Room_ReOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(686, 429);
            this.Controls.Add(this.statusCircle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.separator1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Updatelbl);
            this.Controls.Add(this.VLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.HeaderColor = System.Drawing.SystemColors.MenuHighlight;
            this.Image = global::Room_Reorder.Properties.Resources.icons8_room_80;
            this.Name = "Room_ReOrder";
            this.Text = "Room ReOrder";
            this.Load += new System.EventHandler(this.Room_ReOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private ReaLTaiizor.Controls.Separator separator1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Updatelbl;
        private StatusCircle statusCircle;
        private Label VLbl;
    }
}

