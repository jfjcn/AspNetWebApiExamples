﻿using System.Collections.Generic;
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
        protected static readonly string ArtistRelativePath = @"artist";

        [TestFixtureSetUp]
        public static void SetUpForFixture()
        {
            _restClient = new RestClientForTesting<Artist>(BaseUrl);
        }

        [Test]
        public void _001_there_should_be_no_artists_in_the_database()
        {
            var response = _restClient.GetMany(ArtistRelativePath);
            var allArtists = response.ReturnedObject;
            Assert.That(allArtists, Is.Not.Null);
            Assert.That(allArtists.Count, Is.EqualTo(0));
        }

        [Test]
        public void _010_we_should_be_able_to_add_an_artist_named_Prince_into_our_API()
        {
            var prince = new Artist {Name = "Prince"};
            var response = _restClient.Post(ArtistRelativePath, prince);
            Assert.That(response.Success, Is.True);
            Assert.That(response.ResourceUri, Is.Not.Null);
        }

        [Test]
        public void _020_we_should_be_able_to_find_an_artist_named_Prince_into_our_API()
        {
            var princesName = "Prince";
            var response = _restClient.GetSingle(ArtistRelativePath + "?artistName=" + princesName);
            var prince = response.ReturnedObject;
            Assert.That(prince, Is.Not.Null);
            Assert.That(prince.Name, Is.EqualTo(princesName));
        }

        [Test]
        public void _030_we_should_be_able_to_rename_Prince_to_the_artist_in_our_API()
        {
            var princesName = "Prince";
            var response = _restClient.GetSingle(ArtistRelativePath + "?artistName=" + princesName);
            var prince = response.ReturnedObject;
            Assert.That(prince, Is.Not.Null);
            Assert.That(prince.Name, Is.EqualTo(princesName));

            var princesNewName = "The Artist";
            prince.Name = princesNewName;
            var secondResponse = _restClient.Put(ArtistRelativePath + "/" + prince.Id, prince);

            var thirdResponse = _restClient.GetSingle(ArtistRelativePath + "/" + prince.Id);
            var newlyUpdatedPrince = thirdResponse.ReturnedObject;
            Assert.That(newlyUpdatedPrince, Is.Not.Null);
            Assert.That(newlyUpdatedPrince.Name, Is.EqualTo(princesNewName));

        }

        [Test]
        public void _100_when_we_clear_out_the_database_the_count_should_be_zero()
        {
            ClearOutAllEntitiesInTheRemoteApi();
        }

        private static void ClearOutAllEntitiesInTheRemoteApi()
        {
            var response = _restClient.GetMany(ArtistRelativePath);
            var allArtists = response.ReturnedObject;
            if (allArtists == null)
            {
                return;
            }
            foreach (var artist in allArtists)
            {
                var deleteResponse = _restClient.Delete(ArtistRelativePath, artist.Id);
                Assert.That(deleteResponse, Is.Not.Null);
            }
        }
    }
}
