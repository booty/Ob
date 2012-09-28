using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using ObCore.Models;
using ObMobile.Helpers;

namespace ObMobile.Controllers {
	public class CommentTypeController : Controller {
		
		/// <summary>
		/// Assumes they want the adult comments types
		/// </summary>
		/// <returns>Comment types in Json</returns>
		///public string Index() {
		///	return Index(true);  
		///}

		/// <summary>
		/// Returns comment types
		/// </summary>
		/// <param name="includeAdult">Whether or not to include adult comments</param>
		/// <returns></returns>
		// todo: set caching options (should be fairly long TTL)
		// todo: don't return adult comments if user is not logged in, 
		public string Index() {
			if (Session.IsObLoggedIn()) {
				Response.AddHeader("Cache-Control", "private");
				Response.AddHeader("Expires", DateTime.UtcNow.AddMinutes(60).ToString("R"));
				
				// only include adult comments if they're an adult (duh)
				var commentTypes = CommentType.FindAll(Session.CurrentObMember().IsAdult);
				return JsonConvert.SerializeObject(commentTypes);
			}
			else {
				return String.Empty;
			}
		}

	}
}
