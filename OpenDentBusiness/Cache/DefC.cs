﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	public class DefC {
		private static Def[][] _short;
		private static Def[][] _long;
		private static object _lockObj=new object();

		///<summary>Stores all defs in a 2D array.</summary>
		public static Def[][] Long {
			get {
				return GetArrayLong();
			}
			set {
				lock(_lockObj) {
					_long=value;
				}
			}
		}

		///<summary>Stores all defs in a 2D array except the hidden ones.  The first dimension is the category, in int format.  The second dimension is the index of the definition in this category.  This is dependent on how it was refreshed, and not on what is in the database.  If you need to reference a specific def, then the DefNum is more effective.</summary>
		public static Def[][] Short {
			get {
				return GetArrayShort();
			}
			set {
				lock(_lockObj) {
					_short=value;
				}
			}
		}

		///<summary>Stores all defs in a 2D array.</summary>
		public static Def[][] GetArrayLong() {
			bool isArrayNull=false;
			lock(_lockObj) {
				if(_long==null) {
					isArrayNull=true;
				}
			}
			if(isArrayNull) {
				Defs.RefreshCache();
			}
			Def[][] arrayDefs=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			lock(_lockObj) {
				for(int i=0;i<arrayDefs.Length;i++) {
					Def[] arrayDefCopy=new Def[_long[i].Length];
					for(int j=0;j<_long[i].Length;j++) {
						arrayDefCopy[j]=_long[i][j].Copy();
					}
					arrayDefs[i]=arrayDefCopy;
				}
			}
			return arrayDefs;
		}

		///<summary>Stores all defs in a 2D array except the hidden ones.  The first dimension is the category, in int format.  The second dimension is the index of the definition in this category.  This is dependent on how it was refreshed, and not on what is in the database.  If you need to reference a specific def, then the DefNum is more effective.</summary>
		public static Def[][] GetArrayShort() {
			bool isArrayNull=false;
			lock(_lockObj) {
				if(_short==null) {
					isArrayNull=true;
				}
			}
			if(isArrayNull) {
				Defs.RefreshCache();
			}
			Def[][] arrayDefs=new Def[Enum.GetValues(typeof(DefCat)).Length][];
			lock(_lockObj) {
				for(int i=0;i<arrayDefs.Length;i++) {
					Def[] arrayDefCopy=new Def[_short[i].Length];
					for(int j=0;j<_short[i].Length;j++) {
						arrayDefCopy[j]=_short[i][j].Copy();
					}
					arrayDefs[i]=arrayDefCopy;
				}
			}
			return arrayDefs;
		}

		public static bool DefShortIsNull {
			get {
				bool isArrayNull=false;
				lock(_lockObj) {
					if(_short==null) {
						isArrayNull=true;
					}
				}
				return isArrayNull;
			}
		}

		///<summary>Gets a list of non-hidden defs for one category.</summary>
		public static Def[] GetList(DefCat defCat) {
			Def[][] arrayDefs=GetArrayShort();
			return arrayDefs[(int)defCat];
		}

		///<summary>Get one def from Long.  Returns null if not found.  Only used for very limited situations.  Other Get functions tend to be much more useful since they don't return null.  There is also BIG potential for silent bugs if you use this.ItemOrder instead of GetOrder().</summary>
		public static Def GetDef(DefCat myCat,long myDefNum) {
			Def[][] arrayDefs=GetArrayLong();
			for(int i=0;i<arrayDefs[(int)myCat].GetLength(0);i++) {
				if(arrayDefs[(int)myCat][i].DefNum==myDefNum) {
					return arrayDefs[(int)myCat][i].Copy();
				}
			}
			return null;
		}

		///<summary>Returns the Def with the exact itemName passed in.  Returns null if not found.
		///If itemName is blank, then it returns the first def in the category.</summary>
		public static Def GetDefByExactName(DefCat myCat,string itemName) {
			return GetDef(myCat,GetByExactName(myCat,itemName));
		}

		///<summary>Pass in an array of all defs to save from making deep copies of the cache if you are going to call this method repeatedly.</summary>
		public static string GetName(DefCat myCat,long myDefNum,Def[][] arrayDefs=null) {
			if(myDefNum==0) {
				return "";
			}
			if(arrayDefs==null) {
				arrayDefs=GetArrayLong();
			}
			for(int i=0;i<arrayDefs[(int)myCat].GetLength(0);i++) {
				if(arrayDefs[(int)myCat][i].DefNum==myDefNum) {
					return arrayDefs[(int)myCat][i].ItemName;
				}
			}
			return "";
		}

		///<summary>Returns 0 if it can't find the named def.  If the name is blank, then it returns the first def in the category.</summary>
		public static long GetByExactName(DefCat myCat,string itemName) {
			Def[][] arrayDefs=GetArrayLong();
			if(itemName=="") {
				return arrayDefs[(int)myCat][0].DefNum;//return the first one in the list
			}
			for(int i=0;i<arrayDefs[(int)myCat].GetLength(0);i++) {
				if(arrayDefs[(int)myCat][i].ItemName==itemName) {
					return arrayDefs[(int)myCat][i].DefNum;
				}
			}
			return 0;
		}

		///<summary>Returns the named def.  If it can't find the name, then it returns the first def in the category.</summary>
		public static long GetByExactNameNeverZero(DefCat myCat,string itemName) {
			Def[][] arrayDefs=GetArrayLong();
			if(itemName=="") {
				return arrayDefs[(int)myCat][0].DefNum;//return the first one in the list
			}
			for(int i=0;i<arrayDefs[(int)myCat].GetLength(0);i++) {
				if(arrayDefs[(int)myCat][i].ItemName==itemName) {
					return arrayDefs[(int)myCat][i].DefNum;
				}
			}
			if(arrayDefs[(int)myCat].Length==0) {
				Def def=new Def();
				def.Category=myCat;
				def.ItemOrder=0;
				def.ItemName=itemName;
				Defs.Insert(def);
				Defs.RefreshCache();
			}
			Def[][] arrayDefsUpdated=GetArrayLong();//The cache could have changed by this point.  Grab it again just in case.
			return arrayDefsUpdated[(int)myCat][0].DefNum;//return the first one in the list
		}

		///<summary>Gets the order of the def within Short or -1 if not found.</summary>
		public static int GetOrder(DefCat myCat,long myDefNum) {
			//gets the index in the list of unhidden (the Short list).
			Def[][] arrayDefs=GetArrayShort();
			for(int i=0;i<arrayDefs[(int)myCat].GetLength(0);i++) {
				if(arrayDefs[(int)myCat][i].DefNum==myDefNum) {
					return i;
				}
			}
			return -1;
		}

		///<summary></summary>
		public static string GetValue(DefCat myCat,long myDefNum) {
			string retStr="";
			Def[][] arrayDefs=GetArrayLong();
			for(int i=0;i<arrayDefs[(int)myCat].GetLength(0);i++) {
				if(arrayDefs[(int)myCat][i].DefNum==myDefNum) {
					retStr=arrayDefs[(int)myCat][i].ItemValue;
				}
			}
			return retStr;
		}

		///<summary></summary>
		public static Color GetColor(DefCat myCat,long myDefNum) {
			Color retCol=Color.White;
			Def[][] arrayDefs=GetArrayLong();
			for(int i=0;i<arrayDefs[(int)myCat].GetLength(0);i++) {
				if(arrayDefs[(int)myCat][i].DefNum==myDefNum) {
					retCol=arrayDefs[(int)myCat][i].ItemColor;
				}
			}
			return retCol;
		}

		///<summary></summary>
		public static bool GetHidden(DefCat myCat,long myDefNum) {
			Def[][] arrayDefs=GetArrayLong();
			for(int i=0;i<arrayDefs[(int)myCat].GetLength(0);i++) {
				if(arrayDefs[(int)myCat][i].DefNum==myDefNum) {
					return arrayDefs[(int)myCat][i].IsHidden;
				}
			}
			return false;
		}

		/*//<summary>Allowed types are blank, C, or A.  Only used in FormInsPlan.</summary>
		public static Def[] GetFeeSchedList(string type) {
			ArrayList AL=new ArrayList();
			for(int i=0;i<DefC.Short[(int)DefCat.FeeSchedNames].Length;i++) {
				if(DefC.Short[(int)DefCat.FeeSchedNames][i].ItemValue==type) {
					AL.Add(DefC.Short[(int)DefCat.FeeSchedNames][i]);
				}
			}
			Def[] retVal=new Def[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}*/

		///<summary>Returns defs from the AdjTypes that contain '+' in the ItemValue column.</summary>
		public static List<Def> GetPositiveAdjTypes() {
			List<Def> retVal=new List<Def>();
			Def[][] arrayDefs=GetArrayShort();
			for(int i=0;i<arrayDefs[(int)DefCat.AdjTypes].Length;i++) {
				if(arrayDefs[(int)DefCat.AdjTypes][i].ItemValue=="+") {
					retVal.Add(arrayDefs[(int)DefCat.AdjTypes][i]);
				}
			}
			return retVal;
		}

		///<summary>Returns defs from the AdjTypes that contain '-' in the ItemValue column.</summary>
		public static List<Def> GetNegativeAdjTypes() {
			List<Def> retVal=new List<Def>();
			Def[][] arrayDefs=GetArrayShort();
			for(int i=0;i<arrayDefs[(int)DefCat.AdjTypes].Length;i++) {
				if(arrayDefs[(int)DefCat.AdjTypes][i].ItemValue=="-") {
					retVal.Add(arrayDefs[(int)DefCat.AdjTypes][i]);
				}
			}
			return retVal;
		}

		///<summary>Returns a DefNum for the special image category specified.  Returns 0 if no match found.</summary>
		public static long GetImageCat(ImageCategorySpecial specialCat) {
			Def[] defs=DefC.GetList(DefCat.ImageCats);
			for(int i=0;i<defs.Length;i++) {
				if(defs[i].ItemValue.Contains(specialCat.ToString())) {
					return defs[i].DefNum;
				}
			}
			return 0;
		}

		///<summary>Returns true if the passed-in def is deprecated.  This method must be updated whenever another def is deprecated.</summary>
		public static bool IsDefDeprecated(Def def) {
			if(def.Category==DefCat.AccountColors && def.ItemName=="Received Pre-Auth") {
				return true;
			}
			return false;
		}

	}

	///<summary></summary>
	public enum ImageCategorySpecial {
		///<summary>Show in Chart module.</summary>
		X,
		///<summary>Show in patient forms.</summary>
		F,
		///<summary>Patient picture (only one)</summary>
		P,
		///<summary>Statements (only one)</summary>
		S,
		///<summary>Graphical tooth charts and perio charts (only one)</summary>
		T
	}
}
