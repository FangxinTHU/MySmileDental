namespace OpenDental{
	partial class FormTreatPlanCurEdit {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTreatPlanCurEdit));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.butRight = new OpenDental.UI.Button();
			this.butLeft = new OpenDental.UI.Button();
			this.gridTP = new OpenDental.UI.ODGrid();
			this.contextMenuProcs = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemGotToAppt = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemGoToPlanned = new System.Windows.Forms.ToolStripMenuItem();
			this.gridAll = new OpenDental.UI.ODGrid();
			this.butDelete = new OpenDental.UI.Button();
			this.butMakeActive = new OpenDental.UI.Button();
			this.textNote = new System.Windows.Forms.TextBox();
			this.textHeading = new System.Windows.Forms.TextBox();
			this.labelNote = new System.Windows.Forms.Label();
			this.labelHeading = new System.Windows.Forms.Label();
			this.contextMenuProcs.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(755, 604);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(836, 604);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butRight
			// 
			this.butRight.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRight.Autosize = true;
			this.butRight.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRight.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRight.CornerRadius = 4F;
			this.butRight.Image = global::OpenDental.Properties.Resources.Right;
			this.butRight.Location = new System.Drawing.Point(444, 312);
			this.butRight.Name = "butRight";
			this.butRight.Size = new System.Drawing.Size(35, 24);
			this.butRight.TabIndex = 58;
			this.butRight.Click += new System.EventHandler(this.butRight_Click);
			// 
			// butLeft
			// 
			this.butLeft.AdjustImageLocation = new System.Drawing.Point(-1, 0);
			this.butLeft.Autosize = true;
			this.butLeft.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butLeft.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butLeft.CornerRadius = 4F;
			this.butLeft.Image = global::OpenDental.Properties.Resources.Left;
			this.butLeft.Location = new System.Drawing.Point(444, 282);
			this.butLeft.Name = "butLeft";
			this.butLeft.Size = new System.Drawing.Size(35, 24);
			this.butLeft.TabIndex = 57;
			this.butLeft.Click += new System.EventHandler(this.butLeft_Click);
			// 
			// gridTP
			// 
			this.gridTP.ContextMenuStrip = this.contextMenuProcs;
			this.gridTP.HasMultilineHeaders = false;
			this.gridTP.HScrollVisible = false;
			this.gridTP.Location = new System.Drawing.Point(12, 129);
			this.gridTP.Name = "gridTP";
			this.gridTP.ScrollValue = 0;
			this.gridTP.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridTP.Size = new System.Drawing.Size(426, 469);
			this.gridTP.TabIndex = 56;
			this.gridTP.Title = "Treatment Planned Procedures";
			this.gridTP.TranslationName = "FormDisplayFields";
			this.gridTP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grids_MouseDown);
			// 
			// contextMenuProcs
			// 
			this.contextMenuProcs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemGotToAppt,
            this.menuItemGoToPlanned});
			this.contextMenuProcs.Name = "contextMenuProcs";
			this.contextMenuProcs.Size = new System.Drawing.Size(227, 48);
			// 
			// menuItemGotToAppt
			// 
			this.menuItemGotToAppt.Name = "menuItemGotToAppt";
			this.menuItemGotToAppt.Size = new System.Drawing.Size(226, 22);
			this.menuItemGotToAppt.Text = "Go To Appointment";
			this.menuItemGotToAppt.Click += new System.EventHandler(this.menuItemGotToAppt_Click);
			// 
			// menuItemGoToPlanned
			// 
			this.menuItemGoToPlanned.Name = "menuItemGoToPlanned";
			this.menuItemGoToPlanned.Size = new System.Drawing.Size(226, 22);
			this.menuItemGoToPlanned.Text = "Go To Planned Appointment";
			this.menuItemGoToPlanned.Click += new System.EventHandler(this.menuItemGoToPlanned_Click);
			// 
			// gridAll
			// 
			this.gridAll.ContextMenuStrip = this.contextMenuProcs;
			this.gridAll.HasMultilineHeaders = false;
			this.gridAll.HScrollVisible = false;
			this.gridAll.Location = new System.Drawing.Point(485, 129);
			this.gridAll.Name = "gridAll";
			this.gridAll.ScrollValue = 0;
			this.gridAll.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridAll.Size = new System.Drawing.Size(426, 469);
			this.gridAll.TabIndex = 59;
			this.gridAll.Title = "Available Procedures";
			this.gridAll.TranslationName = "FormDisplayFields";
			this.gridAll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grids_MouseDown);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(12, 604);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(86, 24);
			this.butDelete.TabIndex = 60;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butMakeActive
			// 
			this.butMakeActive.AdjustImageLocation = new System.Drawing.Point(1, 1);
			this.butMakeActive.Autosize = true;
			this.butMakeActive.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMakeActive.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMakeActive.CornerRadius = 4F;
			this.butMakeActive.Location = new System.Drawing.Point(274, 604);
			this.butMakeActive.Name = "butMakeActive";
			this.butMakeActive.Size = new System.Drawing.Size(164, 24);
			this.butMakeActive.TabIndex = 76;
			this.butMakeActive.TabStop = false;
			this.butMakeActive.Text = "Make Active Treatment Plan";
			this.butMakeActive.Click += new System.EventHandler(this.butMakeActive_Click);
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(103, 38);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(335, 85);
			this.textNote.TabIndex = 78;
			// 
			// textHeading
			// 
			this.textHeading.Location = new System.Drawing.Point(103, 12);
			this.textHeading.Name = "textHeading";
			this.textHeading.Size = new System.Drawing.Size(335, 20);
			this.textHeading.TabIndex = 77;
			// 
			// labelNote
			// 
			this.labelNote.Location = new System.Drawing.Point(12, 40);
			this.labelNote.Name = "labelNote";
			this.labelNote.Size = new System.Drawing.Size(89, 16);
			this.labelNote.TabIndex = 80;
			this.labelNote.Text = "Note";
			this.labelNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelHeading
			// 
			this.labelHeading.Location = new System.Drawing.Point(12, 14);
			this.labelHeading.Name = "labelHeading";
			this.labelHeading.Size = new System.Drawing.Size(89, 16);
			this.labelHeading.TabIndex = 79;
			this.labelHeading.Text = "Heading";
			this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormTreatPlanCurEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(923, 640);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.textHeading);
			this.Controls.Add(this.labelNote);
			this.Controls.Add(this.labelHeading);
			this.Controls.Add(this.butMakeActive);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.gridAll);
			this.Controls.Add(this.butRight);
			this.Controls.Add(this.butLeft);
			this.Controls.Add(this.gridTP);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(100, 100);
			this.Name = "FormTreatPlanCurEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Current Treatment Plan - {Active}";
			this.Load += new System.EventHandler(this.FormTreatPlanCurEdit_Load);
			this.contextMenuProcs.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.Button butRight;
		private UI.Button butLeft;
		private UI.ODGrid gridTP;
		private UI.ODGrid gridAll;
		private System.Windows.Forms.ContextMenuStrip contextMenuProcs;
		private System.Windows.Forms.ToolStripMenuItem menuItemGotToAppt;
		private System.Windows.Forms.ToolStripMenuItem menuItemGoToPlanned;
		private UI.Button butDelete;
		private UI.Button butMakeActive;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.TextBox textHeading;
		private System.Windows.Forms.Label labelNote;
		private System.Windows.Forms.Label labelHeading;
	}
}