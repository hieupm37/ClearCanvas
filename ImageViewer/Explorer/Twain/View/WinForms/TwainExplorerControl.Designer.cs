namespace ClearCanvas.ImageViewer.Explorer.Twain.View.WinForms
{
    partial class TwainExplorerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TwainExplorerControl));
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._btnScan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._smallIconImageList = new System.Windows.Forms.ImageList(this.components);
            this._mediumIconImageList = new System.Windows.Forms.ImageList(this.components);
            this._largeIconImageList = new System.Windows.Forms.ImageList(this.components);
            this._pictureBox = new System.Windows.Forms.PictureBox();
            this._galleryView = new ClearCanvas.Desktop.View.WinForms.GalleryView();
            this._toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _toolStrip
            // 
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnScan,
            this.toolStripSeparator1});
            resources.ApplyResources(this._toolStrip, "_toolStrip");
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Stretch = true;
            // 
            // _btnScan
            // 
            this._btnScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this._btnScan, "_btnScan");
            this._btnScan.Name = "_btnScan";
            this._btnScan.Click += new System.EventHandler(this._btnScan_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // _smallIconImageList
            // 
            this._smallIconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this._smallIconImageList, "_smallIconImageList");
            this._smallIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _mediumIconImageList
            // 
            this._mediumIconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this._mediumIconImageList, "_mediumIconImageList");
            this._mediumIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _largeIconImageList
            // 
            this._largeIconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this._largeIconImageList, "_largeIconImageList");
            this._largeIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _pictureBox
            // 
            this._pictureBox.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this._pictureBox, "_pictureBox");
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.TabStop = false;
            // 
            // _galleryView
            // 
            this._galleryView.AllowRenaming = false;
            this._galleryView.DataSource = null;
            this._galleryView.DragOutside = false;
            this._galleryView.DragReorder = false;
            this._galleryView.HideSelection = true;
            this._galleryView.ImageSize = new System.Drawing.Size(100, 100);
            resources.ApplyResources(this._galleryView, "_galleryView");
            this._galleryView.MaxDescriptionLines = 0;
            this._galleryView.MultiSelect = true;
            this._galleryView.Name = "_galleryView";
            this._galleryView.TileMode = false;
            // 
            // TwainExplorerControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this._galleryView);
            this.Controls.Add(this._toolStrip);
            this.Name = "TwainExplorerControl";
            this.Resize += new System.EventHandler(this.TwainExplorerControl_Resize);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.ToolStripButton _btnScan;
        private System.Windows.Forms.ImageList _smallIconImageList;
        private System.Windows.Forms.ImageList _mediumIconImageList;
        private System.Windows.Forms.ImageList _largeIconImageList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Desktop.View.WinForms.GalleryView _galleryView;
        private System.Windows.Forms.PictureBox _pictureBox;

    }
}
