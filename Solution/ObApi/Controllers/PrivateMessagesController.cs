using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ObCore.Models; 

namespace ObApi.Controllers {
	public class PrivateMessagesController : ApiController {
		// GET api/privatemessages
		public HttpResponseMessage Get(string type="received", int skip=0, int take=25) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();

			if (type.ToLower().Equals("sent")) {
				return Request.CreateResponse<List<PrivateMessage>>(HttpStatusCode.OK, PrivateMessage.FindBySender(memberId.Value, skip, take)).WithObApiPrivateDefaults();
			}
			
			if (type.ToLower().Equals("received")) {
				return Request.CreateResponse<List<PrivateMessage>>(HttpStatusCode.OK, PrivateMessage.FindByRecipient(memberId.Value, skip, take)).WithObApiPrivateDefaults();
			}
			
			return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must supply a 'type' parameter that is either blank, 'sent', or 'received'.");
			
		}

		// GET api/privatemessages/5
		public HttpResponseMessage Get(int id) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			var pm = PrivateMessage.FindForMember(memberId.Value, id, true);
			if (pm == null) return Request.CreateResponse(HttpStatusCode.NotFound);
			return Request.CreateResponse<PrivateMessage>(HttpStatusCode.OK, pm);
		}

		// POST api/privatemessages
		public HttpResponseMessage Post([FromBody]string value) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			throw new NotImplementedException();
		}

		// PUT api/privatemessages/5
		public HttpResponseMessage Put(int id, [FromBody]string value) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			throw new NotImplementedException();
		}

		// DELETE api/privatemessages/5
		public HttpResponseMessage Delete(int id) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			throw new NotImplementedException();
		}
	}
}
