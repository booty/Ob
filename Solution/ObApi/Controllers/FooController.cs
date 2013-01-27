using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ObApi.Controllers {
	public class FooController : ApiController {
		// GET api/foo
		public string Get() {
			return "Hi there! " + DateTime.Now.ToString();
			
		}

		// GET api/foo/5
		public string Get(int id) {
			return "value";
		}

		// POST api/foo
		public void Post([FromBody]string value) {
		}

		// PUT api/foo/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/foo/5
		public void Delete(int id) {
		}
	}
}
