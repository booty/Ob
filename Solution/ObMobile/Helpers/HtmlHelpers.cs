using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObCore.Helpers;

namespace ObMobile.Helpers {
	public static class HtmlHelpers {
		public static IHtmlString ToHtmlDefinition(this string value, string label) {
			if (String.IsNullOrEmpty(value)) return String.Empty.ToHtmlString();
			if (String.IsNullOrWhiteSpace(value)) return String.Empty.ToHtmlString();
			return String.Format("<dt>{0}</dt><dd>{1}</dd>", label, HttpUtility.HtmlEncode(value)).ToHtmlString();
		}
	}
}