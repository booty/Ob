using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ob {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			#region Forum Routes
			routes.MapRoute(
				"ForumSearch",
				"Forum/Search",
				new {
					controller = "Forum",
					action = "Search"
				}
			);

			routes.MapRoute(
				"ThreadsPostedIn",
				"Forum/ThreadsPostedIn/{idMember}/{pageNumber}",
				new {
					controller = "Forum",
					action = "ThreadsPostedIn",
					pageNumber = 1 //UrlParameter.Optional
				}
			);

			routes.MapRoute(
				"ThreadsCreatedBy",
				"Forum/ThreadsCreatedBy/{idMember}/{pageNumber}",
				new {
					controller = "Forum",
					action = "ThreadsCreatedBy",
					pageNumber = 1 //UrlParameter.Optional
				}
			);

			// Forum (all forums)
			routes.MapRoute(
				"ForumAllThreads", "Forum/All/{pageNumber}", new {
					controller = "Forum",
					action = "Index",
					pageNumber = 1
				});

			// Forum (per-forum thread list)
			routes.MapRoute(
				"ForumThreadList",
				"Forum/{id}/{page}",
				new { controller = "Forum", action = "Details", page = UrlParameter.Optional });

			#endregion

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}