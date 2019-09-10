using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WudFilmApp.Models
{
    public class MovieShowtimeViewModel
    {
        public IList<Movie> Movies { get; set; }
        public IList<Showtime> Showtimes { get; set;}
    

    }
}
