using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Cookies;
using Nancy.Security;
using System.Diagnostics;
using ObCore;
using ObCore.Models;

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

			// Process login
			Post["/"] = p => {
				// try authentication based on form 
				AuthenticationResult authResult = ObCore.Security.Authenticate(
					Request.Form.Login, 
					Request.Form.Password, 
					Request.UserHostAddress, "/");

				if (authResult.AuthenticationResultCode == Security.AuthenticationResultCode.Success) {
					Context.CurrentUser = authResult.Member;
					// Set cookie & send to dashboard
					return View["dashboard.cshtml", Context.CurrentMember()].WithCookie( 
						new NancyCookie("ObAuthenticationToken", authResult.AuthenticationToken.ToString())
					);
				}

				// if auth'd, set token in cookie, set current user & show dash 
				ViewBag.ErrorMessage = "Something got fucked up.";

				return View["authenticate.cshtml"];
			};
		}


	}
}