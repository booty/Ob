using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObMobile.Helpers {
	public static class SessionHelpers {
		public static ObCore.Models.Member CurrentMember(this HttpContext ctx) {
			if (ctx.Session == null) return null;
			if (ctx.Session["CurrentMember"] != null) return (ObCore.Models.Member)ctx.Session["CurrentMember"];
			return null;
		}
		public static ObCore.Models.Member CurrentMember(this HttpContextBase ctxBase) {
			if (ctxBase.Session == null) return null;
			if (ctxBase.Session["CurrentMember"] != null) return (ObCore.Models.Member)ctxBase.Session["CurrentMember"];
			return null;
		}
	}
}