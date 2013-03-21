using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using ObCore.Models;


namespace ObApiNancy {
	public class PhoneNumbersModule : Nancy.NancyModule {
		public PhoneNumbersModule()
			: base("/forums") {
				Get["/"] = p => {
					return "Hi!";
				};
		}
	}
}