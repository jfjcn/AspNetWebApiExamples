﻿using System;
using System.Collections.Generic;
using WebApiExamples.Domain.CDCollection;

namespace WebApiExamples.Services.CDCollection
{
    /// <summary>
    /// Artist data service serves the purpose of performing CRUD operations
    /// for Artists and is NOT thread-safe.  Used to showcase REST API built
    /// on top of these basic CRUD operations.
    /// </summary>
    public class ArtistDataService
    {
        private static Dictionary<int, Artist> ArtistDatabase = new Dictionary<int, Artist>();
        private static int NextId = 1;

        /// <summary>
        /// Creates a new Artist in the "DB" with a name of artistName and 
        /// assigns an Id to the Artist.
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        public Artist Create(string artistName)
        {
            var newArtist = 
                new Artist
                    {
                        Id = NextId++, 
                        Name = artistName
                    };
            AddArtistToDB(newArtist);
            return newArtist;
        }

        /// <summary>
        /// Updates artistToUpdate in the "DB"
        /// </summary>
        /// <param name="artistToUpdate"></param>
        /// <returns>true if artist exists, false otherwise</returns>
        public bool Update(Artist artistToUpdate)
        {
            if(!ArtistDatabase.ContainsKey(artistToUpdate.Id))
            {
                return false;
            }
            UpdateArtistFromDBWith(artistToUpdate);
            return true;
        }

        /// <summary>
        /// Get the Artist with artistId from the "DB"
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns>The Artist with Id artistId, otherwise null</returns>
        public Artist Get(int artistId)
        {
            if(!ArtistDatabase.ContainsKey(artistId))
            {
                return null;
            }
            return new Artist(ArtistDatabase[artistId]);
        }

        /// <summary>
        /// Get the Artist with artistName from the "DB"
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns>The Artist with Name artistName, otherwise null</returns>
        public Artist Get(string artistName)
        {
            foreach (var artist in ArtistDatabase.Values)
            {
                if(artistName.Equals(artist.Name, StringComparison.InvariantCulture))
                {
                    return new Artist(artist);
                }
            }
            return null;
        }

        /// <summary>
        /// Delete the Artist with Id artistId from the "DB"
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns>true if artistId is in the "DB," otherwise null</returns>
        public bool Delete(int artistId)
        {
            if (!ArtistDatabase.ContainsKey(artistId))
            {
                return false;
            }
            WipeOutArtistFromDBWithId(artistId);
            return true;
        }

        private static void AddArtistToDB(Artist newArtist)
        {
            ArtistDatabase.Add(newArtist.Id, newArtist);
        }

        private static Artist UpdateArtistFromDBWith(Artist artistToUpdate)
        {
            return ArtistDatabase[artistToUpdate.Id] = artistToUpdate;
        }

        private static void WipeOutArtistFromDBWithId(int artistId)
        {
            ArtistDatabase[artistId] = null;
        }
    }
}
