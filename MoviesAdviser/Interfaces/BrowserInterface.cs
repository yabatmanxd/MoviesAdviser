using MoviesAdviser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAdviser
{
    //Интерфейс, который будут реализовывать оба браузера. Через него удобно следить за функционалом сразу обоих браузеров.
    interface BrowserInterface
    {
        List<Movie> GetMoviesList(string genre, int year, string country, string sortMethod);
        bool IsAvailable();
    }
}
