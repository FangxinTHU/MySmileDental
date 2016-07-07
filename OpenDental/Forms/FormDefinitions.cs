using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormDefinitions : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textGuide;
		private System.Windows.Forms.GroupBox groupEdit;
		private OpenDental.TableDefs tbDefs;
		private System.Windows.Forms.ListBox listCategory;
		private System.Windows.Forms.Label label13;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butHide;
		//<summary>This is the index of the selected cat.</summary>
		//private int InitialCat;
		///<summary>this is (int)DefCat, not the index of the selected Cat.</summary>
		private int SelectedCat;
		private bool changed;
		///<summary>Gives the DefCat for each item in the list.</summary>
		private DefCat[] lookupCat;
		//private User user;
		private bool DefsIsSelected;
		private Def[] DefsList;
		private int DefsSelected;

		///<summary>Must check security before allowing this window to open.</summary>
		public FormDefinitions(DefCat selectedCat){
			InitializeComponent();// Required for Windows Form Designer support
			SelectedCat=(int)selectedCat;
			tbDefs.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbDefs_CellDoubleClicked);
			tbDefs.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbDefs_CellClicked);
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDefinitions));
			this.butClose = new OpenDental.UI.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.textGuide = new System.Windows.Forms.TextBox();
			this.groupEdit = new System.Windows.Forms.GroupBox();
			this.butHide = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.tbDefs = new OpenDental.TableDefs();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.groupEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(686, 638);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75, 24);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(92, 604);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 18);
			this.label14.TabIndex = 22;
			this.label14.Text = "Guidelines";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textGuide
			// 
			this.textGuide.Location = new System.Drawing.Point(198, 604);
			this.textGuide.Multiline = true;
			this.textGuide.Name = "textGuide";
			this.textGuide.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textGuide.Size = new System.Drawing.Size(460, 63);
			this.textGuide.TabIndex = 2;
			// 
			// groupEdit
			// 
			this.groupEdit.Controls.Add(this.butHide);
			this.groupEdit.Controls.Add(this.butDown);
			this.groupEdit.Controls.Add(this.butUp);
			this.groupEdit.Controls.Add(this.butAdd);
			this.groupEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupEdit.Location = new System.Drawing.Point(198, 549);
			this.groupEdit.Name = "groupEdit";
			this.groupEdit.Size = new System.Drawing.Size(460, 51);
			this.groupEdit.TabIndex = 1;
			this.groupEdit.TabStop = false;
			this.groupEdit.Text = "Edit Items";
			// 
			// butHide
			// 
			this.butHide.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butHide.Autosize = true;
			this.butHide.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHide.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHide.CornerRadius = 4F;
			this.butHide.Location = new System.Drawing.Point(138, 18);
			this.butHide.Name = "butHide";
			this.butHide.Size = new System.Drawing.Size(79, 24);
			this.butHide.TabIndex = 10;
			this.butHide.Text = "&Hide";
			this.butHide.Click += new System.EventHandler(this.butHide_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(346, 18);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(79, 24);
			this.butDown.TabIndex = 9;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0, 1);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(242, 18);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(79, 24);
			this.butUp.TabIndex = 8;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(34, 18);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79, 24);
			this.butAdd.TabIndex = 6;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// tbDefs
			// 
			this.tbDefs.BackColor = System.Drawing.SystemColors.Window;
			this.tbDefs.Location = new System.Drawing.Point(199, 6);
			this.tbDefs.Name = "tbDefs";
			this.tbDefs.ScrollValue = 1;
			this.tbDefs.SelectedIndices = new int[0];
			this.tbDefs.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbDefs.Size = new System.Drawing.Size(459, 538);
			this.tbDefs.TabIndex = 19;
			// 
			// listCategory
			// 
			this.listCategory.Items.AddRange(new object[] {
            "Account Colors",
            "Account Procs Quick Add",
            "Adj Types",
            "Appointment Colors",
            "Appt Confirmed",
            "Appt Procs Quick Add",
            "Billing Types",
            "Blockout Types",
            "Chart Graphic Colors",
            "Claim Custom Tracking",
            "Claim Payment Tracking",
            "Commlog Types",
            "Contact Categories",
            "Diagnosis",
            "Fee Colors",
            "Image Categories",
            "Insurance Payment Types",
            "Letter Merge Cats",
            "Misc Colors",
            "Payment Types",
            "PaySplit Unearned Types",
            "Proc Button Categories",
            "Proc Code Categories",
            "Prog Notes Colors",
            "Prognosis",
            "Provider Specialties",
            "Recall/Unsch Status",
            "Supply Categories",
            "Task Priorities",
            "Treat\' Plan Priorities"});
			this.listCategory.Location = new System.Drawing.Point(22, 36);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(147, 394);
			this.listCategory.TabIndex = 0;
			this.listCategory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listCategory_MouseDown);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(22, 18);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(162, 17);
			this.label13.TabIndex = 17;
			this.label13.Text = "Select Category:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormDefinitions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(789, 675);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.textGuide);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.groupEdit);
			this.Controls.Add(this.tbDefs);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.label13);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDefinitions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Definitions";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormDefinitions_Closing);
			this.Load += new System.EventHandler(this.FormDefinitions_Load);
			this.groupEdit.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDefinitions_Load(object sender, System.EventArgs e) {
			/*if(PermissionsOld.AuthorizationRequired("Definitions")){
				user=Users.Authenticate("Definitions");
				if(!UserPermissions.IsAuthorized("Definitions",user)){
					MsgBox.Show(this,"You do not have permission for this feature");
					DialogResult=DialogResult.Cancel;
					return;
				}	
			}*/
			lookupCat=new DefCat[listCategory.Items.Count];
			lookupCat[0]=DefCat.AccountColors;
			lookupCat[1]=DefCat.AccountQuickCharge;
			lookupCat[2]=DefCat.AdjTypes;
			lookupCat[3]=DefCat.AppointmentColors;
			lookupCat[4]=DefCat.ApptConfirmed;
			lookupCat[5]=DefCat.ApptProcsQuickAdd;
			lookupCat[6]=DefCat.BillingTypes;
			lookupCat[7]=DefCat.BlockoutTypes;
			lookupCat[8]=DefCat.ChartGraphicColors;
			lookupCat[9]=DefCat.ClaimCustomTracking;
			lookupCat[10]=DefCat.ClaimPaymentTracking;
			lookupCat[11]=DefCat.CommLogTypes;
			lookupCat[12]=DefCat.ContactCategories;
			lookupCat[13]=DefCat.Diagnosis;
			lookupCat[14]=DefCat.FeeColors;
			lookupCat[15]=DefCat.ImageCats;
			lookupCat[16]=DefCat.InsurancePaymentType;
			lookupCat[17]=DefCat.LetterMergeCats;
			lookupCat[18]=DefCat.MiscColors;
			lookupCat[19]=DefCat.PaymentTypes;
			lookupCat[20]=DefCat.PaySplitUnearnedType;
			lookupCat[21]=DefCat.ProcButtonCats;
			lookupCat[22]=DefCat.ProcCodeCats;
			lookupCat[23]=DefCat.ProgNoteColors;
			lookupCat[24]=DefCat.Prognosis;
			lookupCat[25]=DefCat.ProviderSpecialties;
			lookupCat[26]=DefCat.RecallUnschedStatus;
			lookupCat[27]=DefCat.SupplyCats;
			lookupCat[28]=DefCat.TaskPriorities;
			lookupCat[29]=DefCat.TxPriorities;			
			for(int i=0;i<listCategory.Items.Count;i++){
				listCategory.Items[i]=Lan.g(this,(string)listCategory.Items[i]);
				if((int)lookupCat[i]==SelectedCat){
					listCategory.SelectedIndex=i;
				}
			}
			FillCats();
		}

		private void listCategory_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			listCategory.SelectedIndex=listCategory.IndexFromPoint(e.X,e.Y);
			//test for -1 only necessary if there is whitespace, which there is not.
			SelectedCat=(int)lookupCat[listCategory.SelectedIndex];
			FillCats();
		}

		private void FillCats(){
			//a category is ALWAYS selected; never -1.
			DefsIsSelected=false;
			FormDefEdit.EnableColor=false;
			FormDefEdit.EnableValue=false;
			FormDefEdit.CanEditName=true;
			FormDefEdit.CanDelete=false;
			FormDefEdit.CanHide=true;
			tbDefs.Fields[1]="";
			FormDefEdit.ValueText="";
			butHide.Visible=true;
			switch(listCategory.SelectedIndex){
				case 0://"Account Colors":
					//SelectedCat=0;
					FormDefEdit.CanEditName=false;
					FormDefEdit.EnableColor=true;
					FormDefEdit.HelpText=Lan.g(this,"Changes the color of text for different types of entries in Account Module");
					break;
				case 1://"Acct Proc Quick Add":
					FormDefEdit.CanHide=true;
					FormDefEdit.CanDelete=true;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Procedure Codes");
					FormDefEdit.HelpText=Lan.g(this,"Account Proc Quick Add items.  Each entry can be a series of procedure codes separated by commas (e.g. D0180,D1101,D8220).  Used in the account module to quickly charge patients for items.");
					break;
				case 2://"Adj Types":
					//SelectedCat=1;
					FormDefEdit.ValueText=Lan.g(this,"+ or -");
					FormDefEdit.EnableValue=true;
					FormDefEdit.HelpText=Lan.g(this,"Plus increases the patient balance.  Minus decreases it.  Not allowed to change value after creating new type since changes affect all patient accounts.");
					break;
				case 3://"Appointment Colors":
					//SelectedCat=17;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText=Lan.g(this,"Changes colors of background in Appointments Module, and colors for completed appointments.");
					break;
				case 4://"Appt Confirmed":
					//SelectedCat=2;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Abbrev");
					FormDefEdit.EnableColor=true;
					//tbDefs.Fields[2]="Color";
					FormDefEdit.HelpText=Lan.g(this,"Color shows on each appointment if Appointment View is set to show ConfirmedColor.");
					break;
				case 5://"Appt Procs Quick Add":
					//SelectedCat=3;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"ADA Code(s)");
					if(Clinics.IsMedicalPracticeOrClinic(FormOpenDental.ClinicNum)) {
						FormDefEdit.HelpText=Lan.g(this,"These are the procedures that you can quickly add to the treatment plan from within the appointment editing window.  Multiple procedures may be separated by commas with no spaces. These definitions may be freely edited without affecting any patient records.");
					}
					else {
						FormDefEdit.HelpText=Lan.g(this,"These are the procedures that you can quickly add to the treatment plan from within the appointment editing window.  They must not require a tooth number. Multiple procedures may be separated by commas with no spaces. These definitions may be freely edited without affecting any patient records.");
					}	
					break;
				case 6://"Billing Types":
					//SelectedCat=4;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"E=Email bill");
					FormDefEdit.HelpText=Lan.g(this,"It is recommended to use as few billing types as possible.  They can be useful when running reports to separate delinquent accounts, but can cause 'forgotten accounts' if used without good office procedures. Changes affect all patients.");
					break;
				case 7://"Blockout Types":
					FormDefEdit.EnableColor=true;
					FormDefEdit.EnableValue=false;
					FormDefEdit.HelpText=Lan.g(this,"Blockout types are used in the appointments module.");
					break;
				case 8://"Chart Graphic Colors":
					//SelectedCat=22;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					if(Clinics.IsMedicalPracticeOrClinic(FormOpenDental.ClinicNum)) {
						FormDefEdit.HelpText=Lan.g(this,"These colors will be used to graphically display treatments.");
					}
					else {
						FormDefEdit.HelpText=Lan.g(this,"These colors will be used on the graphical tooth chart to draw restorations.");
					}	
					break;
				case 9://"Custom Tracking":
					butHide.Visible=false;
					FormDefEdit.CanDelete=true;
					FormDefEdit.CanHide=false;
					FormDefEdit.EnableColor=false;
					FormDefEdit.HelpText=Lan.g(this,"Some offices may set up claim tracking statuses such as 'review', 'hold', 'riskmanage', etc.");
					break;
				case 10://"Payment Tracking":
					FormDefEdit.EnableColor=false;
					FormDefEdit.ValueText=Lan.g(this,"Value");
					FormDefEdit.HelpText=Lan.g(this,"EOB adjudication method codes to be used for insurance payments.  Last entry cannot be hidden.");
					break;
				case 11://"Commlog Types"
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"APPT,FIN,RECALL,MISC");
					FormDefEdit.HelpText=Lan.g(this,"Changes affect all current commlog entries.  In the second column, you can optionally specify APPT,FIN,RECALL,or MISC. Only one of each. This helps automate new entries.");
					break;
				case 12://"Contact Categories":
					//SelectedCat=(int)DefCat.ContactCategories;
					FormDefEdit.HelpText=Lan.g(this,"You can add as many categories as you want.  Changes affect all current contact records.");
					break;
				case 13://"Diagnosis":
					//SelectedCat=16;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"1 or 2 letter abbreviation");
					FormDefEdit.HelpText=Lan.g(this,"The diagnosis list is shown when entering a procedure.  Ones that are less used should go lower on the list.  The abbreviation is shown in the progress notes.  BE VERY CAREFUL.  Changes affect all patients.");
					break;
				case 14://"Fee Colors":
					FormDefEdit.CanDelete=false;
					FormDefEdit.CanHide=false;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText=Lan.g(this,"These are the colors associated to fee types.");
					break;
				case 15://"Image Categories":
					//SelectedCat=18;
					//FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Usage");
					FormDefEdit.HelpText=Lan.g(this,"These are the categories that will be available in the image and chart modules.  If you hide a category, images in that category will be hidden, so only hide a category if you are certain it has never been used.  Multiple categories can be set to show in the Chart module, but only one category should be set for patient pictures, statements, and tooth charts. Selecting multiple categories for treatment plans will save the treatment plan in each category. Affects all patient records.");
					break;
				case 16://"Insurance Payment Types":
					butHide.Visible=false;
					FormDefEdit.CanDelete=true;
					FormDefEdit.CanHide=false;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"N=Not selected for deposit");
					FormDefEdit.EnableColor=false;
					FormDefEdit.HelpText=Lan.g(this,"These are claim payment types for insurance payments attached to claims.");
					break;
				case 17://"Letter Merge Cats"
					//SelectedCat=(int)DefCat.LetterMergeCats;
					FormDefEdit.HelpText=Lan.g(this,"Categories for Letter Merge.  You can safely make any changes you want.");
					break;
				case 18://"Misc Colors":
					//SelectedCat=21;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText="";
					break;
				case 19://"Payment Types":
					//SelectedCat=10;
					FormDefEdit.HelpText=Lan.g(this,"Types of payments that patients might make. Any changes will affect all patients.");
					break;
				case 20://paysplit unearned types
					FormDefEdit.HelpText=Lan.g(this,"Usually only used by offices that use accrual basis accounting instead of cash basis accounting. Any changes will affect all patients.");
					break;
				case 21://"Proc Button Categories":
					FormDefEdit.HelpText=Lan.g(this,"These are similar to the procedure code categories, but are only used for organizing and grouping the procedure buttons in the Chart module.");
					break;
				case 22://"Proc Code Categories":
					//SelectedCat=11;
					FormDefEdit.HelpText=Lan.g(this,"These are the categories for organizing procedure codes. They do not have to follow ADA categories.  There is no relationship to insurance categories which are setup in the Ins Categories section.  Does not affect any patient records.");
					break;
				case 23://"Prog Notes Colors":
					//SelectedCat=12;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText=Lan.g(this,"Changes color of text for different types of entries in the Chart Module Progress Notes.");
					break;
				case 24://"Prognosis":
					//Nothing special. Might add HelpText later.
					FormDefEdit.HelpText=Lan.g(this,"");
					break;
				case 25://"Provider Specialties":
					FormDefEdit.HelpText=Lan.g(this,"Provider specialties cannot be deleted.  Changes to provider specialties could affect e-claims.");
					break;
				case 26://"Recall/Unsch Status":
					//SelectedCat=13;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Abbreviation");
					FormDefEdit.HelpText=Lan.g(this,"Recall/Unsched Status.  Abbreviation must be 7 characters or less.  Changes affect all patients.");
					break;
				case 27://Supply Categories
					butHide.Visible=false;
					FormDefEdit.CanDelete=true;
					FormDefEdit.CanHide=false;
					FormDefEdit.HelpText=Lan.g(this,"The categories for inventory supplies.");
					break;
				case 28://Task Priorities
					FormDefEdit.CanDelete=false;
					FormDefEdit.CanHide=true;
					FormDefEdit.ValueText=Lan.g(this,"D = Default");
					FormDefEdit.EnableColor=true;
					FormDefEdit.EnableValue=true;
					FormDefEdit.HelpText=Lan.g(this,"Priorities available for selection within the task edit window.  Task lists are sorted using the order of these priorities.  They can have any description and color.  At least one priority should be Default (D).  If more than one priority is flagged as the default, the last default in the list will be used.  If no default is set, the last priority will be used.  Changes affect all tasks where the definition is used.");
					break;
				case 29://"Treat' Plan Priorities":
					//SelectedCat=20;
					FormDefEdit.EnableColor=true;
					FormDefEdit.HelpText=Lan.g(this,"Priorities available for selection in the Treatment Plan module.  They can be simple numbers or descriptive abbreviations 7 letters or less.  Changes affect all procedures where the definition is used.");
					break;
			}
			FillDefs();
		}

		private void FillDefs(){
			//Defs.IsSelected=false;
			int scroll=tbDefs.ScrollValue;
			DefsList=Defs.GetCatList(SelectedCat);
			tbDefs.ResetRows(DefsList.Length);
			tbDefs.SetBackGColor(Color.White);
			for(int i=0;i<DefsList.Length;i++){
				if(DefC.IsDefDeprecated(DefsList[i])) {
					DefsList[i].IsHidden=true;
				}
				if(FormDefEdit.CanEditName) {
					tbDefs.Cell[0,i]=DefsList[i].ItemName;
				}
				else {//Users cannot edit the item name so let them translate them.
					tbDefs.Cell[0,i]=Lan.g("FormDefinitions",DefsList[i].ItemName);//Doesn't use 'this' so that renaming the form doesn't change the translation
				}
				if(lookupCat[listCategory.SelectedIndex]==DefCat.ImageCats) {
					tbDefs.Cell[1,i]=GetItemDescForImages(DefsList[i].ItemValue);
				}
				else {
					tbDefs.Cell[1,i]=DefsList[i].ItemValue;
				}
				if(FormDefEdit.EnableColor){
					tbDefs.BackGColor[2,i]=DefsList[i].ItemColor;
				}
				if(DefsList[i].IsHidden){
					tbDefs.Cell[3,i]="X";
				}
				//else tbDefs.Cell[3,i]="";
			}
			if(DefsSelected>DefsList.Length-1){
				DefsSelected=-1;
				DefsIsSelected=false;
			}
			if(DefsIsSelected){
				tbDefs.BackGColor[0,DefsSelected]=Color.LightGray;
				tbDefs.BackGColor[1,DefsSelected]=Color.LightGray;
			}
			tbDefs.Fields[1]=FormDefEdit.ValueText;
			if(FormDefEdit.EnableColor){
				tbDefs.Fields[2]="Color";
			}
			else{
				tbDefs.Fields[2]="";
			}
			tbDefs.LayoutTables();
			tbDefs.ScrollValue=scroll;
			//the following do not require a refresh of the table:
			if(FormDefEdit.CanEditName){
				groupEdit.Enabled=true;
				groupEdit.Text="Edit Items";
			}
			else{
				groupEdit.Enabled=false;
				groupEdit.Text="Not allowed";
			}
			textGuide.Text=FormDefEdit.HelpText;
		}

		private string GetItemDescForImages(string itemValue) {
			string retVal="";
			if(itemValue.Contains("X")){
				retVal=Lan.g(this,"ChartModule");
			}
			if(itemValue.Contains("F")) {
				if(retVal!="") {
					retVal+=", ";
				}
				retVal+=Lan.g(this,"PatientForm");
			}
			if(itemValue.Contains("P")){
				if(retVal!=""){
					retVal+=", ";
				}
				retVal+=Lan.g(this,"PatientPic");
			}
			if(itemValue.Contains("S")){
				if(retVal!=""){
					retVal+=", ";
				}
				retVal+=Lan.g(this,"Statement");
			}
			if(itemValue.Contains("T")){
				if(retVal!=""){
					retVal+=", ";
				}
				retVal+=Lan.g(this,"ToothChart");
			}
			if(itemValue.Contains("R")) {
				if(retVal!="") {
					retVal+=", ";
				}
				retVal+=Lan.g(this,"TreatPlans");
			}
			if(itemValue.Contains("L")) {
				if(retVal!="") {
					retVal+=", ";
				}
				retVal+=Lan.g(this,"PatientPortal");
			}
			return retVal;
		}

		private void tbDefs_CellClicked(object sender, CellEventArgs e){
			if(tbDefs.Cell.GetLength(1)==0) {//Last row was deleted.
				return;
			}
			//Can't move this logic into the Table control because we never want to paint on col 3
			if(DefsIsSelected){
				if(DefsSelected==e.Row){
					tbDefs.BackGColor[0,e.Row]=Color.White;
					tbDefs.BackGColor[1,e.Row]=Color.White;
					DefsIsSelected=false;
				}
				else{
					tbDefs.BackGColor[0,DefsSelected]=Color.White;
					tbDefs.BackGColor[1,DefsSelected]=Color.White;
					tbDefs.BackGColor[0,e.Row]=Color.LightGray;
					tbDefs.BackGColor[1,e.Row]=Color.LightGray;
					DefsSelected=e.Row;
					DefsIsSelected=true;
				}
			}
			else{
				tbDefs.BackGColor[0,e.Row]=Color.LightGray;
				tbDefs.BackGColor[1,e.Row]=Color.LightGray;
				DefsSelected=e.Row;
				DefsIsSelected=true;
			}
			tbDefs.Refresh();
		}

		private void tbDefs_CellDoubleClicked(object sender, CellEventArgs e){
			tbDefs.BackGColor[0,e.Row]=SystemColors.Highlight;
			tbDefs.BackGColor[1,e.Row]=SystemColors.Highlight;
			tbDefs.Refresh();
			DefsIsSelected=true;
			DefsSelected=e.Row;
			if(lookupCat[listCategory.SelectedIndex]==DefCat.ImageCats) {
				FormDefEditImages FormDEI=new FormDefEditImages(DefsList[e.Row]);
				FormDEI.IsNew=false;
				FormDEI.ShowDialog();
			}
			else {
				FormDefEdit FormDefEdit2 = new FormDefEdit(DefsList[e.Row],DefsList);
				FormDefEdit2.IsNew=false;
				FormDefEdit2.ShowDialog();
				//Preferences2.GetCatList(listCategory.SelectedIndex);
			}
			changed=true;
			FillDefs();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Def DefCur=new Def();
			DefCur.ItemOrder=DefsList.Length;
			DefCur.Category=(DefCat)SelectedCat;
			DefCur.ItemName="";
			DefCur.ItemValue="";//necessary
			if(DefCur.Category==DefCat.InsurancePaymentType) {
				DefCur.ItemValue="N";
			}
			if(lookupCat[listCategory.SelectedIndex]==DefCat.ImageCats) {
				FormDefEditImages FormDEI=new FormDefEditImages(DefCur);
				FormDEI.IsNew=true;
				FormDEI.ShowDialog();
				if(FormDEI.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			else {
				FormDefEdit FormDE=new FormDefEdit(DefCur,DefsList);
				FormDE.IsNew=true;
				FormDE.ShowDialog();
				if(FormDE.DialogResult!=DialogResult.OK) {
					return;
				}
			}
			DefsSelected=DefsList.Length;//this is one more than allowed, but it's ok
			DefsIsSelected=true;
			changed=true;
			FillDefs();
		}

		private void butHide_Click(object sender, System.EventArgs e) {
			if(!DefsIsSelected){
				MessageBox.Show(Lan.g(this,"Please select item first,"));
				return;
			}
			//Warn the user if they are about to hide a billing type currently in use.
			if(DefsList[DefsSelected].Category==DefCat.BillingTypes && Patients.IsBillingTypeInUse(DefsList[DefsSelected].DefNum)) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Warning: Billing type is currently in use by patients.")) {
					return;
				}
			}
			if(DefsList[DefsSelected].Category==DefCat.ProviderSpecialties
				&& (Providers.IsSpecialtyInUse(DefsList[DefsSelected].DefNum)
				|| Referrals.IsSpecialtyInUse(DefsList[DefsSelected].DefNum)))
			{
				MsgBox.Show(this,"You cannot hide a specialty if it is in use by a provider or a referral source.");
				return;
			}
			if(Defs.IsDefinitionInUse(DefsList[DefsSelected])) {
				if(DefsList[DefsSelected].DefNum==PrefC.GetLong(PrefName.BrokenAppointmentAdjustmentType)
					|| DefsList[DefsSelected].DefNum==PrefC.GetLong(PrefName.AppointmentTimeArrivedTrigger)
					|| DefsList[DefsSelected].DefNum==PrefC.GetLong(PrefName.AppointmentTimeSeatedTrigger)
					|| DefsList[DefsSelected].DefNum==PrefC.GetLong(PrefName.AppointmentTimeDismissedTrigger)
					|| DefsList[DefsSelected].DefNum==PrefC.GetLong(PrefName.TreatPlanDiscountAdjustmentType)
					|| DefsList[DefsSelected].DefNum==PrefC.GetLong(PrefName.BillingChargeAdjustmentType)
					|| DefsList[DefsSelected].DefNum==PrefC.GetLong(PrefName.FinanceChargeAdjustmentType)) 
				{
					MsgBox.Show(this,"You cannot hide a definition if it is in use within Module Preferences.");
					return;
				}
				else {
					if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Warning: This definition is currently in use within the program.")) {
						return;
					}
				}
			}
			//Stop users from hiding the last definition in categories that must have at least one def in them.
			if(Defs.IsHidable(DefsList[DefsSelected].Category))	{
				int countShowing=0;
				for(int i=0;i<DefsList.Length;i++) {
					if(DefsList[i].DefNum==DefsList[DefsSelected].DefNum) {
						continue;
					}
					if(DefsList[i].IsHidden) {
						continue;
					}
					countShowing++;
				}
				if(countShowing==0) {
					MsgBox.Show(this,"You cannot hide the last definition in this category.");
					return;
				}
			}
			Defs.HideDef(DefsList[DefsSelected]);
			changed=true;
			FillDefs();
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			DefsSelected=DefL.MoveUp(DefsIsSelected,DefsSelected,DefsList);
			changed=true;
			FillDefs();
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			DefsSelected=DefL.MoveDown(DefsIsSelected,DefsSelected,DefsList);
			changed=true;
			FillDefs();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormDefinitions_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidType.Defs, InvalidType.Fees);
			}
			changed=false;
			//Correct the item orders of all definition categories.
			Def[][] arrayAllDefs=DefC.GetArrayLong();//Don't use DefsList because it will only be an array of defs for the selected category.
			for(int i=0;i<arrayAllDefs.Length;i++) {
				for(int j=0;j<arrayAllDefs[i].Length;j++) {
					if(arrayAllDefs[i][j].ItemOrder!=j) {
						arrayAllDefs[i][j].ItemOrder=j;
						Defs.Update(arrayAllDefs[i][j]);
						changed=true;
					}
				}
			}
			if(changed) {//This will only get fired if the item order was corrected
				DataValid.SetInvalid(InvalidType.Defs,InvalidType.Fees);
			}
			DefsIsSelected=false;
			//if(user!=null){
				//SecurityLogs.MakeLogEntry("Definitions","Altered Definitions",user);
			//}
		}



		



	}
}
