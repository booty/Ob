using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObCore;
using ObCore.Models;
using ObMobile.Helpers;

namespace ObMobile.Attributes {
	public class ObAuthorization : ActionFilterAttribute  {

		public ObCore.AuthorizationRequirement Requirement=AuthorizationRequirement.NoRequirement;

		public override void OnActionExecuting(ActionExecutingContext filterContext) { 
			// Nothing to do!
			if (Requirement == AuthorizationRequirement.NoRequirement) return;
		
			// Can be null, if they're not logged in
			Member member = filterContext.HttpContext.CurrentMember();

			bool authorized = Member.IsAuthorized(member, Requirement);
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext) { base.OnActionExecuted(filterContext); }

		public override void OnResultExecuting(ResultExecutingContext filterContext) { base.OnResultExecuting(filterContext); }

		public override void OnResultExecuted(ResultExecutedContext filterContext) { base.OnResultExecuted(filterContext); }

		public override bool Equals(object obj) { return base.Equals(obj); }

		public override bool Match(object obj) { return base.Match(obj); }

		public override int GetHashCode() { return base.GetHashCode(); }

		public override bool IsDefaultAttribute() { return base.IsDefaultAttribute(); }

		public override string ToString() { return base.ToString(); }

		public override object TypeId {
			get {
				return base.TypeId;
			}
		}
	}
}