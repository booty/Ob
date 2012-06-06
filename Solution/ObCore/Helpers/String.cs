using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ObCore.Helpers {
	public static class StringHelpers {
		/*
		public static bool IsCurrentViewAndController(ViewContext vc, string actionName, string controllerName) {
			if (!actionName.Equals(vc.RouteData.GetRequiredString("action"))) return false;
			if (!controllerName.Equals(vc.RouteData.GetRequiredString("controller"))) return false;
			return true;
		}

		public static bool IsCurrentController(ViewContext vc, string controllerName) {
			return (controllerName.Equals(vc.RouteData.GetRequiredString("controller")));
		}

		// from http://stackoverflow.com/questions/4728777/active-menu-item-asp-net-mvc3-master-page
		public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper,
			string linkText,
			string actionName,
			string controllerName) {


			if (IsCurrentViewAndController(htmlHelper.ViewContext, actionName, controllerName)) {
				return htmlHelper.ActionLink(
					 linkText,
					 actionName,
					 controllerName,
					 null,
					 new {
						 @class = "current"
					 });
			}
			return htmlHelper.ActionLink(linkText, actionName, controllerName);
		}


		public static string IifCurrentAction(this HtmlHelper htmlHelper, string actionName, string controllerName, string textIfCurrent, string textIfNotCurrent) {
			if (IsCurrentViewAndController(htmlHelper.ViewContext, actionName, controllerName))
				return textIfCurrent;
			return textIfNotCurrent;
		}

		public static string IifCurrentController(this HtmlHelper htmlHelper, string controllerName, string textIfCurrent, string textIfNotCurrent) {
			if (IsCurrentController(htmlHelper.ViewContext, controllerName))
				return textIfCurrent;
			return textIfNotCurrent;
		}

		public static string IfCurrentController(this HtmlHelper htmlHelper, string controllerName, string textIfCurrent) {
			if (IsCurrentController(htmlHelper.ViewContext, controllerName))
				return textIfCurrent;
			return String.Empty;
		}
		*/

		public static string Left(this string str, int length) {
			return str.Substring(0, Math.Min(length, str.Length));
		}

		public static string Right(this string str, int length) {
			return str.Substring(str.Length - length, length);
		}

		public static string TruncateFriendly(this string s, int maxLength) {
			if (s.Length <= maxLength) return s;
			s = s.Substring(0, maxLength);
			if (s.LastIndexOf(' ') == -1)
				return s + "&#8230;";
			else {
				return s.Substring(0, s.LastIndexOf(' ')) + "&#8230;";
			}
		}

		public static string ToOrdinal(this int i) {
			if ((i >= 4) && (i <= 20)) return i.ToString() + "th";

			string s = i.ToString();
			switch (s[s.Length - 1]) {
				case '1':
					return s + "st";
				case '2':
					return s + "nd";
				case '3':
					return s + "rd";
				default:
					return s + "th";

			}
		}

		public static string ToPossessive(this string s) {
			if (s.EndsWith("s", StringComparison.CurrentCultureIgnoreCase)) return s + "'";
			return s + "'s";
		}

		public static string ToPlural(this int i, string singularForm, string pluralForm, string zero) {
			if (i == 0) return zero + " " + pluralForm;
			return i.ToPlural(singularForm, pluralForm);
		}

		public static string ToPlural(this int i, string singularForm, string pluralForm) {
			if (i == 1) return "1 " + singularForm;
			return i + " " + pluralForm;
		}

		public static string ParenthesizeIfNotZero(this int i, bool leadingSpace = true) {
			if (i != 0) return System.String.Format("{0}({1})", leadingSpace ? " " : System.String.Empty, i);
			return System.String.Empty;
		}


		public static string ToCamelCase(this string s) {
			if (System.String.IsNullOrEmpty(s)) return System.String.Empty;

			s = s.Replace('_', ' ');
			if (string.IsNullOrWhiteSpace(s)) return System.String.Empty;
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