using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ObMobile.Helpers {
	public static class TempDataHelpers {
		private static void AddStringToObject(TempDataDictionary tdd, string key, string strongText, string msg) { 
			object obj=tdd.Peek(key);

			string s;
			if (String.IsNullOrEmpty(strongText))
				s = msg;
			else
				s = String.Format("<strong>{0}</strong> {1}", strongText, msg);

			if (obj==null) {
				tdd[key]=s;
				return;
			}

			if (obj is string) {
				var foo = new List<string>(2) {(string) obj, s};
				tdd[key] = foo;
				return;
			}

			if (tdd[key] is List<string>) {
				((List<string>)obj).Add(s);
				return;
			}
		
			throw new Exception("Was expecting this thing to be null, a string, or a List<string>, but it's a " + obj.GetType().Name);
		}

		public static void AddAlertMessage(this TempDataDictionary tdd, string strongText, string msg) {
			AddStringToObject(tdd,Flash.Alert, strongText, msg);
		}

		public static void AddErrorMessage(this TempDataDictionary tdd, string strongText, string msg) {
			AddStringToObject(tdd,Flash.Error,  strongText, msg);
		}

		public static void AddSuccessMessage(this TempDataDictionary tdd, string strongText, string msg) {
			AddStringToObject(tdd,Flash.Success,  strongText, msg);
		}

		public static void AddInfoMessage(this TempDataDictionary tdd, string strongText, string msg) {
			AddStringToObject(tdd,Flash.Info,  strongText, msg);
		}

		/*
		public static HtmlString ToHtml(this TempDataDictionary tdd, string key) {
			if (tdd.Peek(key) == null) return null;
			if (tdd[key] is IEnumerable) return ((IEnumerable)tdd[key]).T
		}
		 * */

	}
}