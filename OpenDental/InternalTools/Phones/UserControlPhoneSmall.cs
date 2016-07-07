﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class UserControlPhoneSmall:UserControl {
		private List<Phone> phoneList;
		private List<PhoneEmpDefault> phoneEmpDefaultList;
		///<summary>When the GoToChanged event fires, this tells us which patnum.</summary>
		public long GotoPatNum;
		///<summary></summary>
		[Category("Property Changed"),Description("Event raised when user wants to go to a patient or related object.")]
		public event EventHandler GoToChanged=null;
		public int Extension;

		///<summary>Set list of phones to display. Get/Set accessor won't work here because we require 2 seperate fields in order to update the control properly.</summary>
		public void SetPhoneList(List<PhoneEmpDefault> peds,List<Phone> phones) {
			//create a new list so our sorting doesn't affect this list elsewhere
			phoneList=new List<Phone>(phones);
			phoneList.Sort(new Phones.PhoneComparer(Phones.PhoneComparer.SortBy.ext));			
			phoneEmpDefaultList=peds;
			Invalidate();
		}

		///<summary>Set the phone which is linked to the extension at this desk. If phone==null then no phone info shown.</summary>
		public void SetPhone(Phone phone,PhoneEmpDefault phoneEmpDefault,bool isTriageOperator) {
			phoneTile.SetPhone(phone,phoneEmpDefault,isTriageOperator);
		}
		
		public UserControlPhoneSmall() {
			InitializeComponent();
			phoneTile.GoToChanged += new System.EventHandler(this.phoneTile_GoToChanged);
			phoneTile.MenuNumbers=menuNumbers;
			phoneTile.MenuStatus=menuStatus;
		}

		private void FillTile() {
			//Get the new phone list from the database and redraw control.
			SetPhoneList(PhoneEmpDefaults.Refresh(),Phones.GetPhoneList());
			//Set the currently selected phone accordingly.
			if(phoneList==null) {//No phone list. Shouldn't get here.
				phoneTile.SetPhone(null,null,false);
				return;
			}
			Phone phone=Phones.GetPhoneForExtension(phoneList,Extension);
			PhoneEmpDefault phoneEmpDefault=null;
			if(phone!=null) {
				phoneEmpDefault=PhoneEmpDefaults.GetEmpDefaultFromList(phone.EmployeeNum,phoneEmpDefaultList);
			}
			phoneTile.SetPhone(phone,phoneEmpDefault,PhoneEmpDefaults.IsTriageOperatorForExtension(Extension,phoneEmpDefaultList));	
		}

		private void UserControlPhoneSmall_Paint(object sender,PaintEventArgs e) {
			Graphics g=e.Graphics;
			g.FillRectangle(SystemBrushes.Control,this.Bounds);
			if(phoneList==null) {
				return;
			}
			int columns=9;
			int rows=1;
			//Dynamically figure out how many rows are needed if there are any phone tiles present.
			if(phoneList.Count > 0 && columns > 0) {
				rows=(phoneList.Count + columns - 1) / columns;//Rounds up the result of tile count / columns.
			}
			float boxWidth=((float)this.Width)/columns; //21.4f;
			float boxHeight=17f;
			float hTot=boxHeight*rows;
			float x=0f;
			float y=0f;
			//Create a white "background" rectangle so that any empty squares (no employees) will show as white boxes instead of no color.
			g.FillRectangle(new SolidBrush(Color.White),x,y,boxWidth*columns,boxHeight*rows);
			//Dynamically move the phone tile control down.
			int xTile=0;//Just in case this needs to be dynamic in the future as well.
			int yTile=(int)(boxHeight*rows) + 5;//The height of the lights plus a little padding.
			phoneTile.Location=new Point(xTile,yTile);
			//Dynamically resize the entire UserControlPhoneSmall.
			this.Size=new System.Drawing.Size(173,yTile+phoneTile.Height);
			for(int i=0;i<phoneList.Count;i++) {				
				//Draw the extension number if a person is available at that extension.
				if(phoneList[i].ClockStatus!=ClockStatusEnum.Home
					&& phoneList[i].ClockStatus!=ClockStatusEnum.None) {
					//Colors the box a color based on the corresponding phone's status.
					Color outerColor;
					Color innerColor;
					Color fontColor;
					bool isTriageOperatorOnTheClock=false;					
					//get the cubicle color and triage status
					PhoneEmpDefault ped=PhoneEmpDefaults.GetEmpDefaultFromList(phoneList[i].EmployeeNum,phoneEmpDefaultList);
					Phones.GetPhoneColor(phoneList[i],ped,false,out outerColor,out innerColor,out fontColor,out isTriageOperatorOnTheClock);
					using(Brush brush=new SolidBrush(outerColor)) {
						g.FillRectangle(brush,x*boxWidth,y*boxHeight,boxWidth,boxHeight);
					}
					Font baseFont=new Font("Arial",7);
					SizeF extSize=g.MeasureString(phoneList[i].Extension.ToString(),baseFont);
					float padX=(boxWidth-extSize.Width)/2;
					float padY=(boxHeight-extSize.Height)/2;
					using(Brush brush=new SolidBrush(Color.Black)) {
						g.DrawString(phoneList[i].Extension.ToString(),baseFont,brush,(x*boxWidth)+(padX),(y*boxHeight)+(padY));
					}
				}
				x++;
				if(x>=columns) {
					x=0f;
					y++;
				}
			}
			//horiz lines
			for(int i=0;i<rows+1;i++) {
				g.DrawLine(Pens.Black,0,i*boxHeight,Width,i*boxHeight);
			}
			//Very bottom
			g.DrawLine(Pens.Black,0,Height-1,Width,Height-1);
			//vert
			for(int i=0;i<columns;i++) {
				g.DrawLine(Pens.Black,i*boxWidth,0,i*boxWidth,hTot);
			}
			g.DrawLine(Pens.Black,Width-1,0,Width-1,hTot);
		}

		private void phoneTile_GoToChanged(object sender,EventArgs e) {
			if(phoneTile.PhoneCur==null) {
				return;
			}
			if(phoneTile.PhoneCur.PatNum==0) {
				return;
			}
			GotoPatNum=phoneTile.PhoneCur.PatNum;
			OnGoToChanged();
		}

		protected void OnGoToChanged() {
			if(GoToChanged!=null) {
				GoToChanged(this,new EventArgs());
			}
		}

		private void menuItemManage_Click(object sender,EventArgs e) {
			PhoneUI.Manage(phoneTile);
		}

		private void menuItemAdd_Click(object sender,EventArgs e) {
			PhoneUI.Add(phoneTile);
		}

		//Timecards-------------------------------------------------------------------------------------

		private void menuItemAvailable_Click(object sender,EventArgs e) {
			PhoneUI.Available(phoneTile);
			FillTile();
		}

		private void menuItemTraining_Click(object sender,EventArgs e) {
			PhoneUI.Training(phoneTile);
			FillTile();
		}

		private void menuItemTeamAssist_Click(object sender,EventArgs e) {
			PhoneUI.TeamAssist(phoneTile);
			FillTile();
		}

		private void menuItemNeedsHelp_Click(object sender,EventArgs e) {
			PhoneUI.NeedsHelp(phoneTile);
			FillTile();
		}

		private void menuItemWrapUp_Click(object sender,EventArgs e) {
			PhoneUI.WrapUp(phoneTile);
			FillTile();
		}

		private void menuItemOfflineAssist_Click(object sender,EventArgs e) {
			PhoneUI.OfflineAssist(phoneTile);
			FillTile();
		}

		private void menuItemUnavailable_Click(object sender,EventArgs e) {
			PhoneUI.Unavailable(phoneTile);
			FillTile();
		}

		private void menuItemBackup_Click(object sender,EventArgs e) {
			PhoneUI.Backup(phoneTile);
			FillTile();
		}		

		//RingGroups---------------------------------------------------

		private void menuItemRinggroupAll_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupAll(phoneTile);
		}

		private void menuItemRinggroupNone_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupNone(phoneTile);
		}

		private void menuItemRinggroupsDefault_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupsDefault(phoneTile);
		}

		private void menuItemRinggroupsBackup_Click(object sender,EventArgs e) {
			PhoneUI.RinggroupsBackup(phoneTile);
		}

		//Timecard---------------------------------------------------

		private void menuItemLunch_Click(object sender,EventArgs e) {
			PhoneUI.Lunch(phoneTile);
			FillTile();
		}

		private void menuItemHome_Click(object sender,EventArgs e) {
			PhoneUI.Home(phoneTile);
			FillTile();
		}

		private void menuItemBreak_Click(object sender,EventArgs e) {
			PhoneUI.Break(phoneTile);
			FillTile();
		}



	}
}
