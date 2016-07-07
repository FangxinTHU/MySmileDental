using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormPopupsForFam:Form {
		public Patient PatCur;
		private List<Popup> PopupList;

		public FormPopupsForFam() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPopupsForFam_Load(object sender,EventArgs e) {
			gridMain.AllowSortingByColumn=true;
			FillGrid();
		}

		private void FillGrid() {
			if(checkDeleted.Checked) {
				PopupList=Popups.GetDeletedForFamily(PatCur);
			}
			else {
				PopupList=Popups.GetForFamily(PatCur);
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePopupsForFamily","Patient"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePopupsForFamily","Level"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePopupsForFamily","Disabled"),60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			if(checkDeleted.Checked) {
				col=new ODGridColumn(Lan.g("TablePopupsForFamily","Deleted"),60,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g("TablePopupsForFamily","Popup Message"),120);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<PopupList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(Patients.GetPat(PopupList[i].PatNum).GetNameLF());
				row.Cells.Add(Lan.g("enumEnumPopupLevel",PopupList[i].PopupLevel.ToString()));
				row.Cells.Add(PopupList[i].IsDisabled?"X":"");
				if(checkDeleted.Checked) {
					row.Cells.Add(PopupList[i].IsArchived?"X":"");
				}
				row.Cells.Add(PopupList[i].Description);
				row.Tag=i;
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormPopupEdit FormPE=new FormPopupEdit();
			int rowIndex=(int)gridMain.Rows[e.Row].Tag;
			FormPE.PopupCur=PopupList[rowIndex];
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK) {
				FillGrid();
			}
		}
		
		private void checkDeleted_CheckedChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormPopupEdit FormPE=new FormPopupEdit();
			Popup popup=new Popup();
			popup.PatNum=PatCur.PatNum;
			popup.PopupLevel=EnumPopupLevel.Patient;
			popup.IsNew=true;
			FormPE.PopupCur=popup;
			FormPE.ShowDialog();
			if(FormPE.DialogResult==DialogResult.OK) {
				FillGrid();
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}
	}
}