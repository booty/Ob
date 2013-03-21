using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore.Models;


namespace ObApiNancy {
	public class MembersModule : Nancy.NancyModule {
		public MembersModule()
			: base("/forums") {
				Get["/"] = p => {
					return "Hi!";
				};
		}
	}
}