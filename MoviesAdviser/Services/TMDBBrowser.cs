using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MoviesAdviser.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoviesAdviser.Services
{
    //Поиск по The Movie Database
    public class TMDBBrowser : BrowserInterface
    {
        public bool IsAvailable()
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, string> Genres = new Dictionary<string, string>()
        {
            { "Боевики", "28" },            
            { "Приключения", "12" },            
            { "Военные", "10752" },
            { "Криминал", "80" },
            { "Документальные", "99" },
            { "Драмы", "18" },
            { "Семейные", "10751" },
            { "Комедии", "35" },
            { "Ужасы", "27" },
            { "Фантастика", "14" },
            { "Исторические", "36" },
            { "Мелодрамы", "10749" },
            { "Триллер","53" }
        };
        private Dictionary<string, string> SortMethods = new Dictionary<string, string>()
        {
            { "По рейтингу","vote_average" },
            { "По количеству комментариев","popularity" }
        };
        public List<Movie> GetMoviesList(string genre, int year, string country, string sortMethod)
        {
            List<Movie> movies = new List<Movie>();
            string URL = "https://api.themoviedb.org/3/discover/movie?api_key=b41296940c36d7ed60f4f56e9d17bf65&language=ru";
            //Установка параметров
            string urlParams = "";
            string genreID = Genres[genre];
            string sort = SortMethods[sortMethod];
            urlParams += "&with_genres=" + genreID;            
            urlParams += "&sort_by=" + sort + ".desc";
            urlParams += "&year=" + year;

            //Создание запроса
            string json = GetResponse(URL + urlParams, "GET");
            dynamic data = JsonConvert.DeserializeObject(json);
            dynamic results = data.results;
            Console.WriteLine(results[0]);
            foreach (dynamic res in results)
            {               
                DateTime date = Convert.ToDateTime(res.release_date);
                dynamic movie = JsonConvert.DeserializeObject(GetMovieInfo(res.id));
                Movie movieObj = new Movie((string)res.title, GetCountries(movie), date.Year, GetGenres(movie));
                movieObj.Rating = (int)movie.vote_average;
                movies.Add(movieObj);
            }

            return movies;
        }

        private string GetCountries(dynamic movie)
        {            
            dynamic countries = movie.production_countries;
            string res = "";
            foreach(dynamic mvCountry in countries)
            {
                res += mvCountry.name + ",";
            }
            if(res.Length > 0)
                res = res.Remove(res.Length - 1);
            return res;
        }
        private string GetGenres(dynamic movie)
        {           
            dynamic genres = movie.genres;
            string res = "";
            foreach (dynamic mvGenre in genres)
            {
                res += mvGenre.name + ",";
            }
            res = res.Remove(res.Length - 1);
            return res;

        }
        
             
        private string GetMovieInfo(dynamic id)
        {
            string movieURL = "https://api.themoviedb.org/3/movie/" + id + "?api_key=b41296940c36d7ed60f4f56e9d17bf65&language=ru";
            return  GetResponse(movieURL, "GET");    
        }
        private string GetResponse(string url, string method)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = method;
            HttpWebResponse response = null;
            response = (HttpWebResponse)request.GetResponse();
            string json = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                json = sr.ReadToEnd();
                sr.Close();
            }
            return json;
        }
    }
}
