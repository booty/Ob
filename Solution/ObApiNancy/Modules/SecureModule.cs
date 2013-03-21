using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
 using Nancy.Security;

namespace ObApiNancy {
	public class SecureModule : NancyModule {
		public SecureModule() : base("/secret") {
			
			this.RequiresAuthentication();

			Get["/"] = parameters => {
				this.Context.Trace.TraceLog.WriteLog(s=> s.AppendFormat("ApiKey: {0}\n", Request.Query.ApiKey.Value));
				//return "Hello World.... sssh, this is the secret hideout.";
				return this.Response.AsJson(new {
					SecureContent = "You're a penis",
					User = (OtakuBootyUserIdentity)this.Context.CurrentUser
				});
			};
		}
	}
}