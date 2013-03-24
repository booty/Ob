using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace ObApiNancy {
	/// <summary>
	/// Because having a class called "Utilities" is a code smell,
	/// this class is named "Helpers."  Therefore, everything is okay.
	/// </summary>
	public static class Helpers {
		public static int? GetNullableInt(DynamicDictionary dd, string key) {
			if (!dd.ContainsKey(key)) return null;
			try {
				return (int)dd[key];
			}
			catch {
				return null;
			}
		}
	}
}