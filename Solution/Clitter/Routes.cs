using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Cookies;
using Nancy.Security;
using System.Diagnostics;

namespace Clitter {
	public class Routes : NancyModule {

		public Routes() {
			Get["/"] = p => {
				if (Context.CurrentUser == null) {
					Trace.WriteLine("No current user; showing login page");
					return View["authenticate.cshtml"];
				}
				Trace.WriteLine("Current user found");
				return View["dashboard.cshtml", Context.CurrentMember()];
			};
		}
	}
}