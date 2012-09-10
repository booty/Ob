using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObCore;
using ObCore.Models;
using ObMobile.Attributes;
using ObMobile.Helpers;

namespace ObMobile.Controllers {
	public class MemberController : Controller {

		// GET: /Member/Details/5
		[ObAuthorizationRequired(AuthorizationRequirement.IsAuthenticated)]
		public ActionResult Details(int id) {
			if (Request.IsAuthenticated) {
				//User.Identity.Name
			}

			Member currentMember = Session.CurrentObMember();
			Member member = Member.Find(id);
			Relationship rel = Relationship.Find(Session.CurrentObMember().IdMember, id);

			// Get first five public pictures
			ViewBag.PublicPictures = member.PublicPictures;
			ViewBag.FriendsOnlyPictures = member.FriendsOnlyPicturesViewableBy(currentMember);
			
			return View(member);
		}


	}
}
