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
		public HttpResponseMessage Get(string type="received") {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();

			if (type.ToLower().Equals("sent")) {
				return Request.CreateResponse<List<PrivateMessage>>(HttpStatusCode.OK, PrivateMessage.FindBySender(memberId.Value));
			}
			else if (type.ToLower().Equals("received")) {
				return Request.CreateResponse<List<PrivateMessage>>(HttpStatusCode.OK, PrivateMessage.FindByRecipient(memberId.Value));
			}
			else {
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Must supply a 'type' parameter that is either blank, 'sent', or 'received'.");
			}
		}

		// GET api/privatemessages/5
		public string Get(int id) {
			return "value";
		}

		// POST api/privatemessages
		public void Post([FromBody]string value) {
		}

		// PUT api/privatemessages/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/privatemessages/5
		public void Delete(int id) {
		}
	}
}
