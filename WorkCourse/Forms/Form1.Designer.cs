﻿namespace WorkCourse
{
    partial class Form1
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCourseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.WaypointContexMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertWaypointBeforeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertWaypointAfterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteWaypointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panZoomBox1 = new PanZoomBox.PanZoomBox();
            this.MultiWaypointContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteWaypointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.WaypointContexMenu.SuspendLayout();
            this.MultiWaypointContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 406);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(950, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(950, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadMapToolStripMenuItem,
            this.loadCourseToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.loadMapToolStripMenuItem.Text = "Load Map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.loadMapToolStripMenuItem_Click);
            // 
            // loadCourseToolStripMenuItem
            // 
            this.loadCourseToolStripMenuItem.Name = "loadCourseToolStripMenuItem";
            this.loadCourseToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.loadCourseToolStripMenuItem.Text = "Load Course";
            this.loadCourseToolStripMenuItem.Click += new System.EventHandler(this.loadCourseToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.panZoomBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(950, 382);
            this.splitContainer1.SplitterDistance = 786;
            this.splitContainer1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(648, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(648, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(648, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            this.splitContainer2.Size = new System.Drawing.Size(160, 382);
            this.splitContainer2.SplitterDistance = 210;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(160, 210);
            this.treeView1.TabIndex = 0;
            // 
            // WaypointContexMenu
            // 
            this.WaypointContexMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertWaypointBeforeToolStripMenuItem,
            this.insertWaypointAfterToolStripMenuItem,
            this.deleteWaypointToolStripMenuItem});
            this.WaypointContexMenu.Name = "WaypointContexMenu";
            this.WaypointContexMenu.Size = new System.Drawing.Size(195, 70);
            // 
            // insertWaypointBeforeToolStripMenuItem
            // 
            this.insertWaypointBeforeToolStripMenuItem.Name = "insertWaypointBeforeToolStripMenuItem";
            this.insertWaypointBeforeToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.insertWaypointBeforeToolStripMenuItem.Text = "Insert Waypoint Before";
            this.insertWaypointBeforeToolStripMenuItem.Click += new System.EventHandler(this.insertWaypointBeforeToolStripMenuItem_Click);
            // 
            // insertWaypointAfterToolStripMenuItem
            // 
            this.insertWaypointAfterToolStripMenuItem.Name = "insertWaypointAfterToolStripMenuItem";
            this.insertWaypointAfterToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.insertWaypointAfterToolStripMenuItem.Text = "Insert Waypoint After";
            this.insertWaypointAfterToolStripMenuItem.Click += new System.EventHandler(this.insertWaypointAfterToolStripMenuItem_Click);
            // 
            // deleteWaypointToolStripMenuItem
            // 
            this.deleteWaypointToolStripMenuItem.Name = "deleteWaypointToolStripMenuItem";
            this.deleteWaypointToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteWaypointToolStripMenuItem.Text = "Delete Waypoint";
            this.deleteWaypointToolStripMenuItem.Click += new System.EventHandler(this.deleteWaypointToolStripMenuItem_Click);
            // 
            // panZoomBox1
            // 
            this.panZoomBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panZoomBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panZoomBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panZoomBox1.Image = null;
            this.panZoomBox1.Location = new System.Drawing.Point(0, 0);
            this.panZoomBox1.Name = "panZoomBox1";
            this.panZoomBox1.Size = new System.Drawing.Size(786, 382);
            this.panZoomBox1.TabIndex = 0;
            this.panZoomBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.panZoomBox1_Paint);
            this.panZoomBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.panZoomBox1_KeyDown);
            this.panZoomBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panZoomBox1_Click);
            this.panZoomBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panZoomBox1_MouseDown);
            this.panZoomBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panZoomBox1_MouseMove);
            this.panZoomBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panZoomBox1_MouseUp);
            this.panZoomBox1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.panZoomBox1_PreviewKeyDown);
            // 
            // MultiWaypointContextMenu
            // 
            this.MultiWaypointContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteWaypointsToolStripMenuItem});
            this.MultiWaypointContextMenu.Name = "MultiWaypointContextMenu";
            this.MultiWaypointContextMenu.Size = new System.Drawing.Size(167, 48);
            // 
            // deleteWaypointsToolStripMenuItem
            // 
            this.deleteWaypointsToolStripMenuItem.Name = "deleteWaypointsToolStripMenuItem";
            this.deleteWaypointsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.deleteWaypointsToolStripMenuItem.Text = "Delete Waypoints";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 428);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.WaypointContexMenu.ResumeLayout(false);
            this.MultiWaypointContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCourseToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private PanZoomBox.PanZoomBox panZoomBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip WaypointContexMenu;
        private System.Windows.Forms.ToolStripMenuItem insertWaypointBeforeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertWaypointAfterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteWaypointToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip MultiWaypointContextMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteWaypointsToolStripMenuItem;
    }
}

