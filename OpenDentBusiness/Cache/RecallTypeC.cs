﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class RecallTypeC {
		private static List<RecallType> _listt;
		private static object _lockObj=new object();

		///<summary>A list of all recall Types.</summary>
		public static List<RecallType> Listt {
			get {
				return GetListt();
			}
			set {
				lock(_lockObj) {
					_listt=value;
				}
			}
		}

		///<summary>A list of all recall Types.</summary>
		public static List<RecallType> GetListt() {
			bool isListNull=false;
			lock(_lockObj) {
				if(_listt==null) {
					isListNull=true;
				}
			}
			if(isListNull) {
				RecallTypes.RefreshCache();
			}
			List<RecallType> listRecallTypes=new List<RecallType>();
			lock(_lockObj) {
				for(int i=0;i<_listt.Count;i++) {
					listRecallTypes.Add(_listt[i].Copy());
				}
			}
			return listRecallTypes;
		}

	}
}
