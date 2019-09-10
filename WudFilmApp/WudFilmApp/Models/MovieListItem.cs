using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;


namespace WudFilmApp.Models
{
    /// <summary>
    /// Displays a movie with it's latest showtime. 
    /// </summary>
    public class MovieListItem
    {
        /// <summary>
        /// Title of the movie
        /// </summary>
        public string Title => Movie?.Title;

        /// <summary>
        /// Url for posters.
        /// </summary>
        public string PosterUrl => Movie?.PosterUrl;

        /// <summary>
        /// The movie being displayed.
        /// </summary>
        public Movie Movie { get; set; }

        /// <summary>
        /// The next showtime for this movie.
        /// </summary>
        public Showtime Showtime { get; set; }

        /// <summary>
        /// String with following info:
        /// Runtime | Rating | ...
        /// 
        /// </summary>
        /// <example>
        /// 1hr 30min | PG-13 
        /// </example>
        public string AdditionalInfo { get; set; }

        public MovieListItem(Movie movie, Showtime showtime)
        {
            Movie = movie;
            Showtime = showtime;
            FillAdditionalInfo(movie, showtime);
        }

        private void FillAdditionalInfo(Movie m, Showtime s)
        {
            var hours = m.Runtime.ToString("hh");
            var minutes = m.Runtime.ToString("mm");
            var rating = "PG-13";
            AdditionalInfo = $"{hours} hrs {minutes} min | {rating}";
        }
    }
}
