using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ObCore.Helpers;

namespace ObMobile.Helpers {
	public static class HtmlHelpers {
		public enum HtmlDefinitionStyle {
			Plain, QuotedAnswer
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static IHtmlString ToFlashMessageHtml(this TempDataDictionary tdd, string key) {
			var obj = tdd[key];

			if (obj == null) return null;

			// If it's a just return a string (remember: string is IEnumerable, so we have to check this first!)
			if (obj is string) return ((string)obj).ToHtmlString();

			// If it's an array, convert it to a UL
			if (obj is IEnumerable) return ((IEnumerable)obj).ToHtmlUl().ToHtmlString();

			// Give up (maybe it's just a regular string?
			return obj.ToString().ToHtmlString();
		}

		public static string ToHtmlUl(this IEnumerable ie) { 
			var sb = new StringBuilder("<ul>"); 
			
			foreach (object obj in ie) sb.Append("<li>").Append(obj.ToString()).Append("</li>");
			sb.Append("</ul>");
			return sb.ToString();
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

		public static IHtmlString ToHtmlDefinition(this IHtmlString value, string label, HtmlDefinitionStyle hds = HtmlDefinitionStyle.Plain, bool encodeHtml = true) {
			return ToHtmlDefinition(value.ToString(), label, hds, encodeHtml);
		}

		public static String ToMemberPath(this int idMember, string login) {
			return String.Format("Member/{0}", idMember);
		}

		public static IHtmlString ToHtmlMemberA(this int idMember, string login) {
			return String.Format("<a href=\"Member/{0}\">{1}</a>", idMember, login).ToHtmlString();
		}

	}
}