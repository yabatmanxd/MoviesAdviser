using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesAdviser.Models;

namespace MoviesAdviser.Services
{
    //Поиск по 2hdkino.vip
    class HDKinoBrowser : BrowserInterface
    {
        public List<Movie> GetMoviesList(string genre, int year, string country)
        {
            var movieList = new List<Movie>();



            return movieList;
        }

        public bool IsAvailable()
        {
            throw new NotImplementedException();
        }

        //Жанры 2hdkino.vip
        public enum Genres
        {
            Биография,
            Боевики,
            Военные,
            Детективы,
            Документальные,
            Комедии,
            Мультфильмы,
            Ужасы,
            Фэнтези,
            Триллеры
        }
    }
}
