using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ObCore.Helpers {
	public static class Helpers {
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

		public static string CurrentController(this HtmlHelper htmlHelper) {
			return htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
		}

		public static bool CurrentControllerIs(this HtmlHelper hh, string controllerName) {
			return hh.ViewContext.RouteData.GetRequiredString("controller").Equals(controllerName);
		}

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