using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiExamples.Domain.CDCollection;
using WebApiExamples.Services.CDCollection;

namespace WebApiExamples.Controllers
{
    public class ArtistController : ApiController
    {

        protected ArtistDataService ArtistDataService = new ArtistDataService();

        // GET api/artist
        public IEnumerable<Artist> GetAllArtists()
        {
            return ArtistDataService.GetAll();
        }

        // GET api/artist/5
        public Artist Get(int id)
        {
            var artists = ArtistDataService.Get(id);
            if (artists == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return artists;
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
