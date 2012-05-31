using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ob.Helpers {
	public static class Helpers {
		public static string UnderscoreToCamelCase(string s) {
			if (String.IsNullOrEmpty(s)) return String.Empty;

			s = s.Replace('_', ' ');
			if (string.IsNullOrWhiteSpace(s)) return String.Empty;
			s = s.Trim();

			var sb=new StringBuilder(s.Length);
			bool prevWhitespace=true, prevUpper=false;

			for (int i=0; i < s.Length; i++) {
				char c = s[i];
				bool currUpper;
				bool currWhitespace = Char.IsWhiteSpace(c);
				if (currWhitespace) {
					currUpper = false;
					prevUpper = false;
					prevWhitespace = true;
				}
				else {
					currUpper = (Char.IsUpper(c));
					if (prevWhitespace && !currUpper) {
						
						sb.Append(Char.ToUpper(c));
					}
					else {
						if (prevUpper && currUpper) {
							sb.Append(Char.ToLower(c));
						}
						else {
							sb.Append(c);
						}
					}
					prevUpper = currUpper;
					prevWhitespace = false;
				}
			}
			return sb.ToString();
		}
	}
}