using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ObApi.Controllers
{
    public class FopsController : ApiController
    {
        // GET api/fops
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/fops/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/fops
        public void Post([FromBody]string value)
        {
        }

        // PUT api/fops/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/fops/5
        public void Delete(int id)
        {
        }
    }
}
