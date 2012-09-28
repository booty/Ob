using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObMobile.Helpers {
	static public class StringHelpers {
		/// <summary>
		/// Naive; assumes that s is all numeric characters. If len!=10, does nothing
		/// </summary>
		/// <param name="s">Should be ten numeric characters</param>
		/// <returns>If it's 10 chars, a phone number. Otherwise, just returns s</returns>
		public static string ToPhoneNumber(this string s) {
			if (s.Length != 10) return s;
			return string.Format("({0}) {1}-{2}", s.Substring(0, 3), s.Substring(3, 3), s.Substring(6, 4));
		}
	}
}