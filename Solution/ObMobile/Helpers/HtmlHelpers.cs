using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObCore.Helpers;

namespace ObMobile.Helpers {
	public static class HtmlHelpers {
		public enum HtmlDefinitionStyle {
			Plain, QuotedAnswer
		}


		public static IHtmlString ToHtmlDefinition(this string value, string label, HtmlDefinitionStyle hds = HtmlDefinitionStyle.Plain, bool encodeHtml=true) {
			if (String.IsNullOrEmpty(value)) return String.Empty.ToHtmlString();
			if (String.IsNullOrWhiteSpace(value)) return String.Empty.ToHtmlString();
			switch (hds) {
				case HtmlDefinitionStyle.QuotedAnswer:
					return String.Format("<dt>{0}</dt><dd>&ldquo;{1}&rdquo;</dd>", label, (encodeHtml ? HttpUtility.HtmlEncode(value) : value.HtmlEntitiesToQuotes())).ToHtmlString();
				case HtmlDefinitionStyle.Plain:
				default:
					return String.Format("<dt>{0}</dt><dd>{1}</dd>", label, (encodeHtml ? HttpUtility.HtmlEncode(value) : value)).ToHtmlString();
			
			}
		}
	}
}