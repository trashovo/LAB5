using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB;

public static class AudioLinqQueries
{
    public static List<Track> GetTracksLongerThan(IEnumerable<Track> tracks, long minDurationMs)
    {
        return tracks
            .Where(t => t.Duration > minDurationMs)
            .OrderByDescending(t => t.Duration)
            .ToList();
    }


    public static int CountTracksByGenre(IEnumerable<Track> tracks, IEnumerable<Genre> genres, string genreName)
    {
        return (from t in tracks
                join g in genres on t.GenreId equals g.Id
                where g.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase)
                select t).Count();
    }


    public static List<string> GetTopArtistsByTrackCount(
        IEnumerable<Artist> artists,
        IEnumerable<Album> albums,
        IEnumerable<Track> tracks,
        int topCount)
    {
        return (from ar in artists
                join al in albums on ar.Id equals al.ArtistId
                join t in tracks on al.Id equals t.AlbumId
                group t by ar.Name into grp
                orderby grp.Count() descending
                select $"{grp.Key} — {grp.Count()} треков")
            .Take(topCount)
            .ToList();
    }
    
    public static decimal GetMinArtistTotalPriceInGenre(
        IEnumerable<Track> tracks,
        IEnumerable<Album> albums,
        IEnumerable<Genre> genres,
        string genreName)
    {
        var totalsPerArtist = from t in tracks
            join g in genres on t.GenreId equals g.Id
            join al in albums on t.AlbumId equals al.Id
            
            where g.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase)
            group t by al.ArtistId into grp
            select grp.Sum(x => x.Price);

        if (!totalsPerArtist.Any())
            throw new InvalidOperationException($"Записей в жанре \"{genreName}\" не найдено.");

        return totalsPerArtist.Min();
    }
}
