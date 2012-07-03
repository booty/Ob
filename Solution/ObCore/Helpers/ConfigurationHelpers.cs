using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ObCore.Helpers {
	public static class ConfigurationHelpers {
		/// <summary>
		/// If specified key doesn't contain a string that can be parsed to an int, return a default value
		/// We do this since the null coalescing operator (??) won't work for strings
		/// </summary>
		/// <param name="nv">Reference to AppSettings (presumably ConfigurationManager.AppSettings)</param>
		/// <param name="key">The key we're checking</param>
		/// <param name="defaultValue">Value to return if that key doesn't contain a string that can be parsed to an int</param>
		/// <returns>An integer!</returns>
		public static int ValueOrDefault(this NameValueCollection nv, string key, int defaultValue) {
			if (nv[key] == null) return defaultValue;
			string configVal = nv[key];
			int val;
			if (Int32.TryParse(configVal, out val)) return val;
			return defaultValue;	
		
		}

	}
}
