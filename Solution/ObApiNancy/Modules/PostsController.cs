using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore.Models;


namespace ObApiNancy {
	public class PostsModule : Nancy.NancyModule {
		public PostsModule()
			: base("/forums") {
				Get["/"] = p => {
					return "Hi!";
				};
		}
	}
}