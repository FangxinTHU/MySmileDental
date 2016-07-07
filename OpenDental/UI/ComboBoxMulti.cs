using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace OpenDental.UI
{
	/// <summary>
	/// Summary description for ComboBoxMulti.
	/// </summary>
	public class ComboBoxMulti : System.Windows.Forms.UserControl
	{
		private ArrayList items;
		private System.Windows.Forms.PictureBox dropButton;
		private bool droppedDown;
		private System.Windows.Forms.TextBox textMain;
		private System.Windows.Forms.ContextMenu cMenu;
		private ArrayList selectedIndices;
		private bool useCommas;
		///<summary>Used to track user input for SelectionChangeCommitted.</summary>
		private bool _isUserChange=false;

		/// <summary></summary>
		public ComboBoxMulti()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			selectedIndices=new ArrayList();
			items=new ArrayList();
			UseCommas=true;//Required because we specified a default value of true for the designer.
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ComboBoxMulti));
			this.dropButton = new System.Windows.Forms.PictureBox();
			this.textMain = new System.Windows.Forms.TextBox();
			this.cMenu = new System.Windows.Forms.ContextMenu();
			this.SuspendLayout();
			// 
			// dropButton
			// 
			this.dropButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dropButton.Image = ((System.Drawing.Image)(resources.GetObject("dropButton.Image")));
			this.dropButton.Location = new System.Drawing.Point(102, 1);
			this.dropButton.Name = "dropButton";
			this.dropButton.Size = new System.Drawing.Size(17, 19);
			this.dropButton.TabIndex = 1;
			this.dropButton.TabStop = false;
			this.dropButton.Click += new System.EventHandler(this.dropButton_Click);
			// 
			// textMain
			// 
			this.textMain.BackColor = System.Drawing.Color.White;
			this.textMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textMain.Location = new System.Drawing.Point(3, 4);
			this.textMain.Name = "textMain";
			this.textMain.ReadOnly = true;
			this.textMain.Size = new System.Drawing.Size(95, 13);
			this.textMain.TabIndex = 2;
			this.textMain.Text = "";
			// 
			// ComboBoxMulti
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.textMain);
			this.Controls.Add(this.dropButton);
			this.Name = "ComboBoxMulti";
			this.Size = new System.Drawing.Size(120, 21);
			this.Load += new System.EventHandler(this.ComboBoxMulti_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ComboBoxMulti_Paint);
			this.Leave += new System.EventHandler(this.ComboBoxMulti_Leave);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ComboBoxMulti_Layout);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>The items to display in the combo box.</summary>
		[Category("Data"),
			Description("The text of the items to display in the dropdown section.")
		]
		public ArrayList Items{
			get{
				return items;
			}
			set{
				items=value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the combo box is displaying its drop-down portion.
		/// </summary>
		public bool DroppedDown{
			get{
				return droppedDown;
			}
			set{
				droppedDown=value;
			}
		}

		///<summary>The indices of selected items.</summary>
		public ArrayList SelectedIndices{
			get{
				return selectedIndices;
			}
			set{
				selectedIndices=value;
				if(SelectionChangeCommitted!=null && _isUserChange) {
					_isUserChange=false; 
					SelectionChangeCommitted(this,new EventArgs());
				}
			}
		}

		///<summary>The indices of selected items.</summary>
		public List<int> ListSelectedIndices {
			get {
				return new List<int>((int[])selectedIndices.ToArray(typeof(int)));
			}
		}

		///<summary>Use commas instead of OR in the display when muliple selected.</summary>
		[Category("Appearance"),Description("Use commas instead of OR in the display when muliple selected.")]
		[DefaultValue(true)]
		public bool UseCommas{
			get{
				return useCommas;
			}
			set{
				useCommas=value;
			}
		}

		///<summary>Force text being displayed to be refreshed.</summary>
		public void RefreshText() {
			FillText();
		}

		private void ComboBoxMulti_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(127,157,185))//blue
				,0,0,Width-1,Height-1);
		}

		private void dropButton_Click(object sender, System.EventArgs e) {
			textMain.Select();//this is critical to trigger the enter and leave events.
			if(droppedDown){
				droppedDown=false;
			}
			else{
				//show the list
				cMenu=new ContextMenu();
				MenuItem menuItem;
				for(int i=0;i<items.Count;i++){
					menuItem=new MenuItem(items[i].ToString(),new System.EventHandler(MenuItem_Click));
					menuItem.OwnerDraw=true;
					menuItem.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(MenuItem_MeasureItem);
					menuItem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(MenuItem_DrawItem);
					cMenu.MenuItems.Add(menuItem);
				}
        cMenu.Show(this,new Point(0,20));
				/*
				listBox2=new ListBox();
				listBox2.Click += new System.EventHandler(listBox2_Click);
				listBox2.SelectionMode=SelectionMode.MultiExtended;
				//MessageBox.Show(items.Count.ToString());
				listBox2.Size=new Size(Width,14*items.Count);
				listBox2.Location=new Point(0,21);
				Height=listBox2.Height+21;
				//MessageBox.Show(Height.ToString());
				for(int i=0;i<items.Count;i++){
					listBox2.Items.Add(items[i]);
					if(selectedIndices.Contains(i)){
						listBox2.SetSelected(i,true);
					}
				}
				Controls.Add(listBox2);
				this.BringToFront();*/
				//((Control)Container).Height;
				//this.Location;
				droppedDown=true;
			}
		}

		private void MenuItem_Click(object sender, System.EventArgs e){
			ArrayList selectedIndicesOld=new ArrayList(selectedIndices);//Makes a complete shallow copy of selectedIndicies.
			int index=((MenuItem)sender).Index;
			if(Control.ModifierKeys.HasFlag(Keys.Shift)) {//Extended Selection.  The user is holding the Shift key while clicking.
				if(selectedIndices.Count==0) {
					selectedIndices.Add(index);//Select only the item the user just clicked on.
				}
				else {
					int prevIdx=(int)selectedIndices[selectedIndices.Count-1];//Index of the most recently selected item.
					if(prevIdx < index) {//The previously selected index is before the current index.
						for(int i=prevIdx+1;i<=index;i++) {
							SetSelected(i,true);
						}
					}
					else if(prevIdx > index) {//The previously selected index is after the current index.
						for(int i=index;i<prevIdx;i++) {
							SetSelected(i,true);
						}
					}
				}
			}
			else if(Control.ModifierKeys.HasFlag(Keys.Control)) {//Extended Selection.  The user is holding the Ctrl key while clicking.
				if(selectedIndices.Contains(index)){
					//this item was already selected
					selectedIndices.Remove(index);
				}
				else{
					selectedIndices.Add(index);
				}
			}
			else {//Single Selection.  The user is NOT holding the Ctrl key nor the Shift key while clicking.
				selectedIndices.Clear();//Unselect any items already selected.
				selectedIndices.Add(index);//Select only the item the user just clicked on.
			}
			if(selectedIndicesOld.Count!=selectedIndices.Count) {
				_isUserChange=true;
			}
			else {
				for(int i=0;i<selectedIndices.Count;i++) {
					if(!selectedIndicesOld.Contains(selectedIndices[i])) {
						_isUserChange=true;
						break;
					}
				}
			}
			if(_isUserChange) {
				SelectedIndices=selectedIndices;//To trigger the SelectedIndicesChanged event
			}
			FillText();
			cMenu.Show(this,new Point(0,20));
		}

		private void MenuItem_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e){
			e.ItemWidth=Width-18;//not sure why I have to subtract 18 to make it the proper width.
			e.ItemHeight=14;
		}

		private void MenuItem_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e){
			string myCaption=items[e.Index].ToString();
			Brush brushBack;
			if(selectedIndices.Contains(e.Index))
				brushBack=SystemBrushes.Highlight;
			else
				brushBack=SystemBrushes.Window;
			Font myFont=new Font(FontFamily.GenericSansSerif,8);
			e.Graphics.FillRectangle(brushBack,e.Bounds);
			e.Graphics.DrawString(myCaption,e.Font,Brushes.Black,e.Bounds.X,e.Bounds.Y);
		}

		private void FillText(){
			textMain.Text="";
			if(useCommas) {
				for(int i=0;i<selectedIndices.Count;i++) {
					if(i>0) {
						textMain.Text+=", ";
					}
					textMain.Text+=items[(int)selectedIndices[i]];
				}
			}
			else {
				for(int i=0;i<selectedIndices.Count;i++) {
					if(i>0) {
						textMain.Text+=" OR ";
					}
					textMain.Text+=items[(int)selectedIndices[i]];
				}
			}
		}

		private void ComboBoxMulti_Layout(object sender, System.Windows.Forms.LayoutEventArgs e) {
			textMain.Width=Width-21;
		}

		//private void listBox2_Click(object sender, System.EventArgs e) {
		//	selectedIndices=new ArrayList();
		//	for(int i=0;i<listBox2.SelectedIndices.Count;i++){
		//		selectedIndices.Add(listBox2.SelectedIndices[i]);
		//	}
		//	FillText();
		//}

		private void ComboBoxMulti_Load(object sender, System.EventArgs e) {
			FillText();
		}

		private void ComboBoxMulti_Leave(object sender, System.EventArgs e) {
			droppedDown=false;
		}

		///<summary></summary>
		public void SetSelected(int index,bool setToValue){
			selectedIndices.Remove(index);//Remove the index if it is already present in the list in order to avoid duplicates.
			if(setToValue) {
				selectedIndices.Add(index);//The most recently added index must be last so that our Shift select will work.
			}
			FillText();//Since the selections probably changed, the text in the combobox display probably changed as well.
		}

		public delegate void SelectionChangeCommittedHandler(object sender,EventArgs e);

		///<summary>Occurs when one of the menu items is selected.  This line causes the event to show in the designer.</summary>
		public event SelectionChangeCommittedHandler SelectionChangeCommitted;

	}
}
















