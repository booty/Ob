using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

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
	}
}