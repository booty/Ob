using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ObApi.Controllers {
	public class FartController : ApiController {
		// GET api/fart
		public IEnumerable<string> Get() {
			return new string[] { "value1", "value2" };
		}

		// GET api/fart/5
		public string Get(int id) {
			return "value";
		}

		// POST api/fart
		public void Post([FromBody]string value) {
		}

		// PUT api/fart/5
		public void Put(int id, [FromBody]string value) {
		}

		// DELETE api/fart/5
		public void Delete(int id) {
		}
	}
}
