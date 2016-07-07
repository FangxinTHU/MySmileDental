﻿namespace OpenDental {
	partial class PhoneTile {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhoneTile));
			this.pictureWebCam = new System.Windows.Forms.PictureBox();
			this.labelExtensionName = new System.Windows.Forms.Label();
			this.labelStatusAndNote = new System.Windows.Forms.Label();
			this.pictureInUse = new System.Windows.Forms.PictureBox();
			this.labelCustomer = new System.Windows.Forms.Label();
			this.labelTime = new System.Windows.Forms.Label();
			this.service11 = new OpenDental.localhost.Service1();
			this.timerFlash = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pictureWebCam)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureInUse)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureWebCam
			// 
			this.pictureWebCam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureWebCam.Image = ((System.Drawing.Image)(resources.GetObject("pictureWebCam.Image")));
			this.pictureWebCam.Location = new System.Drawing.Point(4, 7);
			this.pictureWebCam.Name = "pictureWebCam";
			this.pictureWebCam.Size = new System.Drawing.Size(50, 37);
			this.pictureWebCam.TabIndex = 0;
			this.pictureWebCam.TabStop = false;
			this.pictureWebCam.Click += new System.EventHandler(this.phoneTile_Click);
			// 
			// labelExtensionName
			// 
			this.labelExtensionName.BackColor = System.Drawing.Color.Transparent;
			this.labelExtensionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelExtensionName.Location = new System.Drawing.Point(52, 9);
			this.labelExtensionName.Name = "labelExtensionName";
			this.labelExtensionName.Size = new System.Drawing.Size(105, 16);
			this.labelExtensionName.TabIndex = 1;
			this.labelExtensionName.Text = "104 - Jordan";
			this.labelExtensionName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelStatusAndNote
			// 
			this.labelStatusAndNote.BackColor = System.Drawing.Color.Transparent;
			this.labelStatusAndNote.Location = new System.Drawing.Point(80, 25);
			this.labelStatusAndNote.Name = "labelStatusAndNote";
			this.labelStatusAndNote.Size = new System.Drawing.Size(77, 16);
			this.labelStatusAndNote.TabIndex = 2;
			this.labelStatusAndNote.Text = "Available";
			this.labelStatusAndNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelStatusAndNote.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelStatusAndNote_MouseUp);
			// 
			// pictureInUse
			// 
			this.pictureInUse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureInUse.BackgroundImage")));
			this.pictureInUse.Location = new System.Drawing.Point(55, 25);
			this.pictureInUse.Name = "pictureInUse";
			this.pictureInUse.Size = new System.Drawing.Size(21, 17);
			this.pictureInUse.TabIndex = 3;
			this.pictureInUse.TabStop = false;
			// 
			// labelCustomer
			// 
			this.labelCustomer.BackColor = System.Drawing.Color.Transparent;
			this.labelCustomer.Location = new System.Drawing.Point(160, 27);
			this.labelCustomer.Name = "labelCustomer";
			this.labelCustomer.Size = new System.Drawing.Size(147, 16);
			this.labelCustomer.TabIndex = 4;
			this.labelCustomer.Text = "Customer phone #";
			this.labelCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelCustomer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelCustomer_MouseClick);
			this.labelCustomer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelCustomer_MouseUp);
			// 
			// labelTime
			// 
			this.labelTime.BackColor = System.Drawing.Color.Lime;
			this.labelTime.Location = new System.Drawing.Point(160, 11);
			this.labelTime.Name = "labelTime";
			this.labelTime.Size = new System.Drawing.Size(56, 16);
			this.labelTime.TabIndex = 5;
			this.labelTime.Text = "01:10:13";
			this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// service11
			// 
			this.service11.Url = "http://localhost:3824/Service1.asmx";
			this.service11.UseDefaultCredentials = true;
			// 
			// timerFlash
			// 
			this.timerFlash.Interval = 300;
			this.timerFlash.Tick += new System.EventHandler(this.timerFlash_Tick);
			// 
			// PhoneTile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pictureInUse);
			this.Controls.Add(this.pictureWebCam);
			this.Controls.Add(this.labelTime);
			this.Controls.Add(this.labelCustomer);
			this.Controls.Add(this.labelStatusAndNote);
			this.Controls.Add(this.labelExtensionName);
			this.DoubleBuffered = true;
			this.Name = "PhoneTile";
			this.Size = new System.Drawing.Size(310, 50);
			((System.ComponentModel.ISupportInitialize)(this.pictureWebCam)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureInUse)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureWebCam;
		private System.Windows.Forms.Label labelExtensionName;
		private System.Windows.Forms.Label labelStatusAndNote;
		private System.Windows.Forms.PictureBox pictureInUse;
		private System.Windows.Forms.Label labelCustomer;
		private System.Windows.Forms.Label labelTime;
		private localhost.Service1 service11;
		private System.Windows.Forms.Timer timerFlash;
	}
}
