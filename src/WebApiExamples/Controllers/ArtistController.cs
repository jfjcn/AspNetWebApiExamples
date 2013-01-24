using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiExamples.Controllers
{
    public class ArtistController : ApiController
    {
        // GET api/artist
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/artist/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/artist
        public void Post([FromBody]string value)
        {
        }

        // PUT api/artist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/artist/5
        public void Delete(int id)
        {
        }
    }
}
