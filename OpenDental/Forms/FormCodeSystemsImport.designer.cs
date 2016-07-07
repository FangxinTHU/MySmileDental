namespace OpenDental{
	partial class FormCodeSystemsImport {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCodeSystemsImport));
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butCheckUpdates = new OpenDental.UI.Button();
			this.butDownload = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.EditableAcceptsCR = true;
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12, 42);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(857, 317);
			this.gridMain.TabIndex = 27;
			this.gridMain.Title = "Code Systems Available";
			this.gridMain.TranslationName = "";
			// 
			// butCheckUpdates
			// 
			this.butCheckUpdates.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCheckUpdates.Autosize = true;
			this.butCheckUpdates.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCheckUpdates.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCheckUpdates.CornerRadius = 4F;
			this.butCheckUpdates.Location = new System.Drawing.Point(12, 12);
			this.butCheckUpdates.Name = "butCheckUpdates";
			this.butCheckUpdates.Size = new System.Drawing.Size(106, 24);
			this.butCheckUpdates.TabIndex = 28;
			this.butCheckUpdates.Text = "Check for Updates";
			this.butCheckUpdates.Click += new System.EventHandler(this.butCheckUpdates_Click);
			// 
			// butDownload
			// 
			this.butDownload.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDownload.Autosize = true;
			this.butDownload.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDownload.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDownload.CornerRadius = 4F;
			this.butDownload.Enabled = false;
			this.butDownload.Location = new System.Drawing.Point(550, 370);
			this.butDownload.Name = "butDownload";
			this.butDownload.Size = new System.Drawing.Size(106, 24);
			this.butDownload.TabIndex = 3;
			this.butDownload.Text = "Download Updates";
			this.butDownload.Click += new System.EventHandler(this.butDownload_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(794, 370);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "Close";
			this.butCancel.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormCodeSystemsImport
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(881, 406);
			this.Controls.Add(this.butCheckUpdates);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butDownload);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormCodeSystemsImport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Import Code Systems";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCodeSystemsImport_FormClosing);
			this.Load += new System.EventHandler(this.FormCodeSystemsImport_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butDownload;
		private OpenDental.UI.Button butCancel;
		private UI.ODGrid gridMain;
		private UI.Button butCheckUpdates;
	}
}