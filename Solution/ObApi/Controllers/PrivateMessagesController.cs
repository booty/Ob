using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ObCore.Models; 

namespace ObApi.Controllers {
	
	//[HttpControllerConfiguration(ActionValueBinder=typeof(MvcActionValueBinder))]
	[MvcStyleBindingAttribute]
	public class PrivateMessagesController : ApiController {
		// GET api/privatemessages
		public HttpResponseMessage Get(string type="received", int skip=0, int take=25, int? id=null) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();

			if (id.HasValue) {
				var pm = PrivateMessage.FindForMember(memberId.Value, id.Value, true);
				if (pm == null) return Request.CreateResponse(HttpStatusCode.NotFound, "Message not found. Either that message doesn't exist, or you don't have permission to read it.");
				return Request.CreateResponse<PrivateMessage>(HttpStatusCode.OK, pm);
			}

			if (type.ToLower().Equals("sent")) {
				return Request.CreateResponse<List<PrivateMessage>>(HttpStatusCode.OK, PrivateMessage.FindBySender(memberId.Value, skip, take)).WithObApiPrivateDefaults();
			}
			
			if (type.ToLower().Equals("received")) {
				return Request.CreateResponse<List<PrivateMessage>>(HttpStatusCode.OK, PrivateMessage.FindByRecipient(memberId.Value, skip, take)).WithObApiPrivateDefaults();
			}
			
			return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "If you supply a 'type' parameter, it should have a value of 'sent' or 'received'.");
			
		}

		
		// GET api/privatemessages/5
		/*
		public HttpResponseMessage GetSingle(int id) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			var pm = PrivateMessage.FindForMember(memberId.Value, id, true);
			if (pm == null) return Request.CreateResponse(HttpStatusCode.NotFound);
			return Request.CreateResponse<PrivateMessage>(HttpStatusCode.OK, pm);
		}
		*/		

		// POST api/privatemessages
		public HttpResponseMessage Post([FromBody]string subject, [FromBody]string body, [FromBody]int idMemberTo, [FromBody]int? idMessageReplyTo) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			

			PrivateMessage pm;
			try {
				pm = Member.Find(memberId.Value).SendPrivateMessage(idMemberTo, subject, body, idMessageReplyTo);
			}
			catch (Exception e) {
				return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
			}
			return Request.CreateResponse<PrivateMessage>(HttpStatusCode.OK, pm);
		}

		// PUT api/privatemessages/5
		/// <summary>
		/// Marks a message as read.
		/// </summary>
		/// <param name="id">ID of the message you're marking as read</param>
		/// <returns></returns>
		public HttpResponseMessage Put(int id) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			PrivateMessage.MarkAsReadForMember(id, memberId.Value);
			return Request.CreateResponse(HttpStatusCode.OK);
		}

		// DELETE api/privatemessages/5
		public HttpResponseMessage Delete(int id) {
			int? memberId = HttpContext.Current.MemberId();
			if (!memberId.HasValue) return Request.CreateMissingAuthorizationTokenResponse();
			PrivateMessage.Delete(id, memberId.Value);
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
