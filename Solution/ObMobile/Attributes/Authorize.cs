using System.Configuration;
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
			filterContext.HttpContext.Response.Redirect(AuthorizationFailedUrl);
		}

		public ObAuthorizationRequired(ObCore.AuthorizationRequirement ar) {
			Requirement = ar;
			AuthorizationFailedUrl = ConfigurationManager.AppSettings["AuthorizationFailedUrl"] ?? "/Session/Create";
		}
	}
}