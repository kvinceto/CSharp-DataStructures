using System;
using System.Collections.Generic;

namespace Exam.RePlay
{
    using System.Linq;

    public class RePlayer : IRePlayer
    {
        private Dictionary<string, Track> tracksById;
        private Dictionary<string, Dictionary<string, Track>> tracksByAlbumsAndTitles;
        private Queue<Track> tracksQueue;
        private Dictionary<string, Track> removedTracks;
        private Dictionary<string, Dictionary<string, HashSet<Track>>> tracksByArtistAndAlbums;

        public RePlayer()
        {
            this.tracksById = new Dictionary<string, Track>();
            this.tracksByAlbumsAndTitles = new Dictionary<string, Dictionary<string, Track>>();
            this.tracksQueue = new Queue<Track>();
            this.removedTracks = new Dictionary<string, Track>();
            this.tracksByArtistAndAlbums = new Dictionary<string, Dictionary<string, HashSet<Track>>>();
        }

        public int Count => this.tracksById.Count;

        public void AddToQueue(string trackName, string albumName)
        {
            if (!this.tracksByAlbumsAndTitles.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }

            if (!this.tracksByAlbumsAndTitles[albumName].ContainsKey(trackName))
            {
                throw new ArgumentException();
            }

            this.tracksQueue.Enqueue(this.tracksByAlbumsAndTitles[albumName][trackName]);
        }

        public void AddTrack(Track track, string album)
        {
            if (!this.tracksByAlbumsAndTitles.ContainsKey(album))
            {
                this.tracksByAlbumsAndTitles.Add(album, new Dictionary<string, Track>());
            }

            track.Album = album;
            this.tracksByAlbumsAndTitles[album].Add(track.Title, track);
            this.tracksById.Add(track.Id, track);
            if (!this.tracksByArtistAndAlbums.ContainsKey(track.Artist))
            {
                this.tracksByArtistAndAlbums.Add(track.Artist, new Dictionary<string, HashSet<Track>>());
            }

            if (!this.tracksByArtistAndAlbums[track.Artist].ContainsKey(album))
            {
                this.tracksByArtistAndAlbums[track.Artist].Add(album, new HashSet<Track>());
            }

            this.tracksByArtistAndAlbums[track.Artist][album].Add(track);
        }

        public bool Contains(Track track)
        {
            return this.tracksById.ContainsKey(track.Id);
        }

        public IEnumerable<Track> GetAlbum(string albumName)
        {
            if (!this.tracksByAlbumsAndTitles.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }

            return this.tracksByAlbumsAndTitles[albumName].Values
                .OrderByDescending(t => t.Plays);
        }

        public Dictionary<string, List<Track>> GetDiscography(string artistName)
        {
            if (!this.tracksByArtistAndAlbums.ContainsKey(artistName))
            {
                throw new ArgumentException();
            }

            if (this.tracksByArtistAndAlbums[artistName].Count == 0)
            {
                throw new ArgumentException();
            }

            return this.tracksByArtistAndAlbums[artistName]
                .ToDictionary(t => t.Key, t => t.Value.ToList());
        }

        public Track GetTrack(string title, string albumName)
        {
            if (!this.tracksByAlbumsAndTitles.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }

            if (!this.tracksByAlbumsAndTitles[albumName].ContainsKey(title))
            {
                throw new ArgumentException();
            }

            return this.tracksByAlbumsAndTitles[albumName][title];
        }

        public IEnumerable<Track> GetTracksInDurationRangeOrderedByDurationThenByPlaysDescending(int lowerBound, int upperBound)
        {
            return this.tracksById.Values
                .Where(t => t.DurationInSeconds >= lowerBound &&
                            t.DurationInSeconds <= upperBound)
                .OrderBy(t => t.DurationInSeconds)
                .ThenByDescending(t => t.Plays);
        }

        public IEnumerable<Track> GetTracksOrderedByAlbumNameThenByPlaysDescendingThenByDurationDescending()
        {
            return this.tracksById.Values
                .OrderBy(t => t.Album)
                .ThenByDescending(t => t.Plays)
                .ThenByDescending(t => t.DurationInSeconds);
        }

        public Track Play()
        {
            bool trackIsValid = false;
            Track trackToPlay = null;

            while (!trackIsValid)
            {
                if (this.tracksQueue.Count == 0)
                {
                    throw new ArgumentException();
                }

                trackToPlay = this.tracksQueue.Dequeue();
                if (!this.removedTracks.ContainsKey(trackToPlay.Id))
                {
                    trackIsValid = true;
                }
            }

            trackToPlay.Plays++;

            return trackToPlay;
        }

        public void RemoveTrack(string trackTitle, string albumName)
        {
            if (!this.tracksByAlbumsAndTitles.ContainsKey(albumName))
            {
                throw new ArgumentException();
            }

            if (!this.tracksByAlbumsAndTitles[albumName].ContainsKey(trackTitle))
            {
                throw new ArgumentException();
            }

            Track track = this.tracksByAlbumsAndTitles[albumName][trackTitle];
            this.tracksByAlbumsAndTitles[albumName].Remove(trackTitle);
            this.tracksById.Remove(track.Id);
            this.tracksByArtistAndAlbums[track.Artist][albumName].Remove(track);
            this.removedTracks.Add(track.Id, track);
        }
    }
}
