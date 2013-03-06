using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ObApi {
	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
	
			routes.MapRoute(
				name: "Forums",
				url: "Forums/{id}",
				defaults: new {
					controller = "Forums",
					action="Get",
					id=UrlParameter.Optional,
					skip=0,
					take=25,
					includeSticky=false,
					includeAdult=false
				}
			);

			// I guess somebody is fucked if their login is also a valid integer! 
			routes.MapRoute(
				name: "Member (by id)",
				url: "Members/{idMember}",
				defaults: new {
					controller = "Members",
					action = "GetByIdMember"
				},
				constraints: new {
					id = @"\d+"
				}
			);

			routes.MapRoute(
				name: "Member (by login)",
				url: "Members/{login}",
				defaults: new {
					controller = "Members",
					action = "GetByLogin"
				}
			);

			routes.MapRoute(
				name: "Private Message (multiple)", 
				url: "PrivateMessages/", 
				defaults: new {
					controller = "PrivateMessages",
					action = "Get"
			});

			routes.MapRoute(
				name: "Private Message (single)",
				url: "PrivateMessages/{id}",
				defaults: new {
					controller="PrivateMessages",
					action="GetSingle"
				}
			);

			routes.MapRoute(
				 name: "Default",
				 url: "{controller}/{action}/{id}",
				 defaults: new {
					 controller = "Home",
					 action = "Index",
					 id = UrlParameter.Optional
				 }
			);
		}
	}
}