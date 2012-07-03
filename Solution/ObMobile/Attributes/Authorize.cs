using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
using ObCore;
using ObCore.Models;
using ObMobile.Helpers;

namespace ObMobile.Attributes {
	public class ObAuthorizationRequired : ActionFilterAttribute  {
		public string AuthorizationFailedUrl;
		public ObCore.AuthorizationRequirement Requirement=AuthorizationRequirement.NoRequirement;

		public override void OnActionExecuting(ActionExecutingContext filterContext) { 
			// Nothing to do!
			if (Requirement == AuthorizationRequirement.NoRequirement) return;
		
			// fyi: Can return null, if they're not logged in
			Member member = filterContext.HttpContext.CurrentObMember();

			// if they're authorized, do nothing... just return
			if (Member.IsAuthorized(member, Requirement)) return;

			// they failed authorization; send them to the appropriate URL
			// todo: store currently requested route in session/tempdata so we can send the user back there after successful auth
			filterContext.Result = new RedirectResult("/Session/Create");
		}

		public ObAuthorizationRequired(ObCore.AuthorizationRequirement ar) {
			Requirement = ar;
			AuthorizationFailedUrl = ConfigurationManager.AppSettings["AuthorizationFailedUrl"] ?? "/Session/Create";
		}
	}

	public class CheckForLoginToken : AuthorizeAttribute {
		public override void OnAuthorization(AuthorizationContext filterContext) {
			if (filterContext.HttpContext.Session.IsObLoggedIn()) {
				Trace.Write("Already logged in; nothing to do", "CheckForLoginToken");
				return;
			}

			if (filterContext.HttpContext.Request.Cookies["token"] == null) {
				Trace.Write("There's no login token cookie; nothing to do", "CheckForLoginToken");
				return;
			}

			Trace.Write("Hey, there's a login token cookie. Should probably do something here!", "CheckForLoginToken");
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
			return;
			// base.HandleUnauthorizedRequest(filterContext);
		}

	}
}