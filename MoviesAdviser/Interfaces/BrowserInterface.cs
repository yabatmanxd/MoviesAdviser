using MoviesAdviser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAdviser
{
    //Интерфейс, который будут реализовывать оба браузера. Через него удобно следить за функционалом сразу обоих браузеров.
    //Пока что на ум приходит только 2 очевидных метода, но по ходу работы их станет больше.
    //Все методы, общие для обоих браузеров, записываются в первую очередь сюда. Это важно.
    interface BrowserInterface
    {
        List<Movie> GetMoviesList(string genre, int year, string country);

        bool IsAvailable();
    }
}
