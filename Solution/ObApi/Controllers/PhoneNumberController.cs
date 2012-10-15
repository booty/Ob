using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using ObApi.Models;



namespace ObApi.Controllers {
	public class PhoneNumberController : ApiController {
		// GET api/phonenumber
		
		
		public IEnumerable<PhoneNumber> Get(string token, bool friendsOnly=false) {
			return PhoneNumber.Find(1238, friendsOnly);
		}


		// GET api/phonenumber/5
		public string Get(int id) {
			return "value";
		}

		// POST api/phonenumber
		public void Post([FromBody]string value) {

		}

		// PUT api/phonenumber/5
		public void Put(int id, [FromBody]string value) {

		}

		// DELETE api/phonenumber/5
		public void Delete(int id) {
			
		}

	}
}
