using System.Collections.Generic;
using NUnit.Framework;
using WebApiExamples.Domain.CDCollection;
using WebApiExamples.Services.CDCollection;

namespace SpecsForDataServices.CDCollection
{
    [TestFixture]
    public class When_using_the_ArtistDataService
    {
        public static readonly ArtistDataService ArtistDataService = new ArtistDataService();
        public List<Artist> _artists;

        [SetUp]
        public void SetUp()
        {
            _artists = SaveAndGet4InitialArtists();
        }

        [TearDown]
        public void TearDown()
        {
            ClearOutAllArtists();
        }

        [Test]
        public void _001_there_should_be_four_artists_in_the_database()
        {
            Assert.That(ArtistDataService.TotalCount, Is.EqualTo(4));
        }

        [Test]
        public void _010_we_should_be_able_to_add_a_fifth_artists_into_the_database()
        {
            var prince = ArtistDataService.Create("Prince");
            Assert.That(ArtistDataService.TotalCount, Is.EqualTo(5));
        }

        [Test]
        public void _020_we_should_be_able_to_find_an_artist_named_Eminem_into_the_database()
        {
            var eminem = ArtistDataService.Get("Eminem");
            Assert.That(eminem, Is.Not.Null.Or.Empty);
        }

        [Test]
        public void _100_when_we_clear_out_the_database_the_count_should_be_zero()
        {
            ArtistDataService.ClearAll();
            Assert.That(ArtistDataService.TotalCount, Is.EqualTo(0));
        }

        private List<Artist> SaveAndGet4InitialArtists()
        {
            var michaelJackson = ArtistDataService.Create("Michael Jackson");
            var ladyGaga = ArtistDataService.Create("Lady Gaga");
            var eminem = ArtistDataService.Create("Eminem");
            var clevelandOrchestra = ArtistDataService.Create("Cleveland Orchestra");
            return new List<Artist>
                       {
                           michaelJackson,
                           ladyGaga,
                           eminem,
                           clevelandOrchestra
                       };
        }

        private void ClearOutAllArtists()
        {
            var artistDataService = new ArtistDataService();
            artistDataService.ClearAll();
        }
    }
}
