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
    public class AlbumController : ApiController
    {
        protected AlbumDataService  AlbumDataService = new AlbumDataService();

        // GET api/album/
        public IEnumerable<Album> GetAllAlbums()
        {
            return AlbumDataService.GetAll();
        }

        // GET api/album/5
        public Album Get(int id)
        {
            var album = AlbumDataService.Get(id);
            if (album == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return album;
        }

        // POST api/cd
        public void Post([FromBody]string value)
        {
        }

        // PUT api/cd/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/cd/5
        public void Delete(int id)
        {
        }
    }
}
