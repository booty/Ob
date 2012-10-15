using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ObApi.Controllers {
	public class AuthenticationController : ApiController {
		// GET api/authentication
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/authentication/5
		public string Get(int id) {

			return "value";
		}

		// POST api/authentication
		public void Post([FromBody]string value) {

		}

		// PUT api/authentication/5
		public void Put(int id, [FromBody]string value) {

		}

		// DELETE api/authentication/5
		public void Delete(int id) {

		}

	}
}
