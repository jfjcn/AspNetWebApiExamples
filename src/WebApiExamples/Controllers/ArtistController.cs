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
            var allArtists = ArtistDataService.GetAll();
            return allArtists;
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

        public Artist GetByName(string artistName)
        {
            var matchingArtist = ArtistDataService.Get(artistName);
            return matchingArtist;
        }

        // POST api/artist
        public HttpResponseMessage PostProduct(Artist artist)
        {
            var createdArtist = ArtistDataService.Create(artist.Name);

            var response = Request.CreateResponse<Artist>(HttpStatusCode.Created, createdArtist);

            string uri = Url.Link("DefaultApi", new { id = createdArtist.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT api/artist/5
        public void Put(int id, Artist artistToUpdate)
        {
            artistToUpdate.Id = id;
            var successfullyUpdated = ArtistDataService.Update(artistToUpdate);
            if(!successfullyUpdated)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // DELETE api/artist/5
        public void DeleteArtist(int id)
        {
            ArtistDataService.Delete(id);
        }
    }
}
