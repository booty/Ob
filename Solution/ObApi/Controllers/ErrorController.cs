using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ObApi.Controllers {
	public class ErrorController : ApiController {
		// GET api/error
		public string Get() {
			return "Penis";
		}

		// GET api/error/5
		public string Get(int id) {
			return "value";
		}

		// POST api/error
		public void Post([FromBody]string value) {
		}

		// PUT api/error/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/error/5
		public void Delete(int id) {
		}
	}
}
