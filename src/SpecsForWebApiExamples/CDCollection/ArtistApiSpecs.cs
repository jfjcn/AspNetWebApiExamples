using System.Collections.Generic;
using NUnit.Framework;
using WebApiExamples.Domain.CDCollection;

namespace SpecsForWebApiExamples.CDCollection
{
    [TestFixture]
    public class When_using_the_artist_api
    {

        protected List<Artist> _artists;
        protected static RestClientForTesting<Artist> _restClient; 
        protected static readonly string BaseUrl = @"http://localhost:4848/api/";

        [TestFixtureSetUp]
        public static void SetUp()
        {
            _restClient = new RestClientForTesting<Artist>(BaseUrl);
        }

        [Test]
        public void _001_there_should_be_no_artists_in_the_database()
        {
            var response = _restClient.GetMany("artist");
            var allArtists = response.ReturnedObject;
            Assert.That(allArtists.Count, Is.EqualTo(0));
        }

        [Test]
        public void _010_we_should_be_able_to_add_an_artists_into_the_database()
        {
            var prince = new Artist {Name = "Prince"};
            var response = _restClient.Post("artist", prince);
            Assert.That(response.Success, Is.True);
            Assert.That(response.ResourceUri, Is.Not.Null);
        }

        [Test]
        public void _020_we_should_be_able_to_find_an_artist_named_Eminem_into_the_database()
        {
//            var eminem = ArtistDataService.Get("Eminem");
//            Assert.That(eminem, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void _100_when_we_clear_out_the_database_the_count_should_be_zero()
        {
//            ArtistDataService.ClearAll();
//            Assert.That(ArtistDataService.TotalCount, Is.EqualTo(0));
        }

//        private List<Artist> SaveAndGet4InitialArtists()
//        {
//            var michaelJackson = ArtistDataService.Create("Michael Jackson");
//            var ladyGaga = ArtistDataService.Create("Lady Gaga");
//            var eminem = ArtistDataService.Create("Eminem");
//            var clevelandOrchestra = ArtistDataService.Create("Cleveland Orchestra");
//            return new List<Artist>
//                       {
//                           michaelJackson,
//                           ladyGaga,
//                           eminem,
//                           clevelandOrchestra
//                       };
//        }

        private void ClearOutAllArtists()
        {
//            var artistDataService = new ArtistDataService();
//            artistDataService.ClearAll();
        }
    }
}
