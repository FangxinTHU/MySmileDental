namespace OpenDental{
	partial class FormWikiHistory {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWikiHistory));
			this.textNumbers = new System.Windows.Forms.TextBox();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butRevert = new OpenDental.UI.Button();
			this.textContent = new OpenDental.TextBoxWiki();
			this.webBrowserWiki = new System.Windows.Forms.WebBrowser();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textNumbers
			// 
			this.textNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.textNumbers.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textNumbers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(117)))), ((int)(((byte)(133)))));
			this.textNumbers.Location = new System.Drawing.Point(266, 12);
			this.textNumbers.Multiline = true;
			this.textNumbers.Name = "textNumbers";
			this.textNumbers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNumbers.Size = new System.Drawing.Size(46, 614);
			this.textNumbers.TabIndex = 83;
			this.textNumbers.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n188\r\n288";
			this.textNumbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12, 12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(248, 614);
			this.gridMain.TabIndex = 5;
			this.gridMain.Title = "Page History";
			this.gridMain.TranslationName = "TableWikiHistory";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.Click += new System.EventHandler(this.gridMain_Click);
			// 
			// butRevert
			// 
			this.butRevert.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butRevert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.butRevert.Autosize = true;
			this.butRevert.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butRevert.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butRevert.CornerRadius = 4F;
			this.butRevert.Location = new System.Drawing.Point(1067, 12);
			this.butRevert.Name = "butRevert";
			this.butRevert.Size = new System.Drawing.Size(75, 24);
			this.butRevert.TabIndex = 84;
			this.butRevert.Text = "Revert";
			this.butRevert.Click += new System.EventHandler(this.butRevert_Click);
			// 
			// textContent
			// 
			this.textContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.textContent.Location = new System.Drawing.Point(294, 12);
			this.textContent.Name = "textContent";
			this.textContent.ReadOnly = true;
			this.textContent.SelectedText = "";
			this.textContent.SelectionLength = 0;
			this.textContent.SelectionStart = 0;
			this.textContent.Size = new System.Drawing.Size(347, 614);
			this.textContent.TabIndex = 82;
			// 
			// webBrowserWiki
			// 
			this.webBrowserWiki.AllowWebBrowserDrop = false;
			this.webBrowserWiki.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowserWiki.IsWebBrowserContextMenuEnabled = false;
			this.webBrowserWiki.Location = new System.Drawing.Point(647, 12);
			this.webBrowserWiki.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowserWiki.Name = "webBrowserWiki";
			this.webBrowserWiki.Size = new System.Drawing.Size(414, 614);
			this.webBrowserWiki.TabIndex = 6;
			this.webBrowserWiki.WebBrowserShortcutsEnabled = false;
			this.webBrowserWiki.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowserWiki_Navigated);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(1067, 602);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 2;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormWikiHistory
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1154, 638);
			this.Controls.Add(this.butRevert);
			this.Controls.Add(this.textContent);
			this.Controls.Add(this.textNumbers);
			this.Controls.Add(this.webBrowserWiki);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormWikiHistory";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Wiki History";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FormWikiHistory_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butClose;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.WebBrowser webBrowserWiki;
		private TextBoxWiki textContent;
		private System.Windows.Forms.TextBox textNumbers;
		private UI.Button butRevert;
	}
}