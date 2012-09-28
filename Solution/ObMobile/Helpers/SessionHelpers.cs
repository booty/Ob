using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ObCore;
using ObCore.Helpers;

namespace ObMobile.Helpers {
	public static class SessionHelpers {

		public static ObCore.Models.Member CurrentObMember(this HttpContext ctx) {
			if (ctx.Session == null) return null;
			return ctx.Session.CurrentObMember();
		}

		public static ObCore.Models.Member CurrentObMember(this HttpContextBase ctxBase) {
			if (ctxBase.Session == null) return null;
			return ctxBase.Session.CurrentObMember();
		}

		public static ObCore.Models.Member CurrentObMember(this HttpSessionStateBase hss) {
			return (ObCore.Models.Member)hss["CurrentObMember"];
		}

		public static ObCore.Models.Member CurrentObMember(this HttpSessionState hss) {
			return (ObCore.Models.Member)hss["CurrentObMember"];
		}

		public static bool IsObLoggedIn(this HttpSessionState httpSessionState) {
			return (httpSessionState["CurrentObMember"] != null);
		}

		public static bool IsObLoggedIn(this HttpSessionStateBase httpSessionState) {
			return (httpSessionState["CurrentObMember"] != null);
		}

		public static bool IsObLoggedIn(this HttpContext ctx) {
			return ctx.CurrentObMember() != null;
		}

		public static void MakeAuthenticatedAsFuck(this HttpContextBase ctx, AuthenticationResult ar, bool saveInCookies = true) {
			// Nothing to do; really... they shouldn't be here
			if (ar.AuthenticationResultCode != Security.AuthenticationResultCode.Success) {
				return;
			}

			ctx.Session[SessionVars.CurrentObMember] = ar.Member;

			if (saveInCookies) {
				ctx.Response.Cookies.Add(new HttpCookie("token", ar.Token));
				ctx.Response.Cookies["token"].Expires = DateTime.Now + new TimeSpan(ConfigurationManager.AppSettings.ValueOrDefault("LoginTokenCookieExpirationDays", 30), 0, 0, 0);
			}

		}
	}
}