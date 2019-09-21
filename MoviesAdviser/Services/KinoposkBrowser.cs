using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesAdviser.Models;

namespace MoviesAdviser.Services
{
    //Поиск по kinopoisk.ru
    class KinoposkBrowser : BrowserInterface
    {
        public List<Movie> GetMoviesList(string genre, int year, string country)
        {
            throw new NotImplementedException();
        }

        public bool IsAvailable()
        {
            throw new NotImplementedException();
        }

        //Жанры кинопоиска
        public enum Genres 
        {
            Биография,
            Боевик,
            Военный,
            Детектив,         
            Документальный,
            Комедия,
            Мультфильм,
            Ужасы,
            Фэнтези,
            Триллер
            
        } 

    }
}
