using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ObCore.Helpers {
	public static class Html {
		public static string CurrentController(this HtmlHelper htmlHelper) {
			return htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
		}

		public static bool CurrentControllerIs(this HtmlHelper hh, string controllerName) {
			return hh.ViewContext.RouteData.GetRequiredString("controller").Equals(controllerName);
		}
	}
}
