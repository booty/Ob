using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ObCore;
using ObCore.Models;

namespace ObApi.Controllers {
	public class ThreadsController : ApiController {


		// GET api/threads/5
		public string Get(int id) {
			return "Hi! :-)";
		}

		// POST api/threads
		public void Post([FromBody]string value) {
		}

		// PUT api/threads/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/threads/5
		public void Delete(int id) {
		}
	}
}
