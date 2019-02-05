using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetIdentity.WebApi.Controllers
{
    public class ProtectedController : ApiController
    {
        // GET: api/Protected
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Protected/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Protected
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Protected/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Protected/5
        public void Delete(int id)
        {
        }
    }
}
