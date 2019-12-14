using System;
using System.Collections.Generic;
using System.Globalization;
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
           
        public List<Movie> GetMoviesList(string genre, int year, string country, string sortMethod)
        {
            List<Movie> movies = new List<Movie>();            
            string URL = "https://api.themoviedb.org/3/discover/movie?api_key=b41296940c36d7ed60f4f56e9d17bf65&language=ru";
            //Установка параметров
            string urlParams = "";
            string genreID = Dictionaries.TMDBGenres[genre];
            string sort = Dictionaries.TMDBSortMethods[sortMethod];
            urlParams += "&with_genres=" + genreID;            
            urlParams += "&sort_by=" + sort + ".desc";
            urlParams += "&year=" + year;
            urlParams += "&page=";
            //Цикл если надо будет смотреть больше одной страницы
            for (int i = 1; i <=1 ; i++)
            {
                urlParams += i;
                //Создание запроса
                string json = GetResponse(URL + urlParams, "GET");
                dynamic data = JsonConvert.DeserializeObject(json);               
                dynamic results = data.results;
                foreach (dynamic res in results)
                {
                    DateTime date = Convert.ToDateTime(res.release_date);
                    if (date.Year == year)
                    {
                        dynamic movie = JsonConvert.DeserializeObject(GetMovieInfo(res.id));
                        Console.WriteLine(GetMovieInfo(res.id));
                        Movie movieObj = new Movie((string)res.title, GetCountries(movie), date.Year, GetGenres(movie));
                        movieObj.Rating = (int)movie.vote_average;
                        movieObj.Description = (string)movie.overview;                        
                        movieObj.Poster = "http://image.tmdb.org/t/p/w185/"+(string)movie.poster_path;
                        movies.Add(movieObj);
                    }
                }
                urlParams = urlParams.Remove(urlParams.Length - 1);
            }                       
            return movies;
        }

        private string GetCountries(dynamic movie)
        {            
            dynamic countries = movie.production_countries;
            string res = "";
            foreach(dynamic mvCountry in countries)
            {
                res += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(((string)mvCountry.name)) + ", ";
            }
            if(res.Length > 0)
                res = res.Remove(res.Length - 2);
            return res;
        }
        private string GetGenres(dynamic movie)
        {           
            dynamic genres = movie.genres;
            string res = "";
            foreach (dynamic mvGenre in genres)
            {
                res += CultureInfo.CurrentCulture.TextInfo.ToTitleCase((string)mvGenre.name) + ", ";
            }
            res = res.Remove(res.Length - 2);
            return res;

        }
        
             
        private string GetMovieInfo(dynamic id)
        {
            string movieURL = "https://api.themoviedb.org/3/movie/" + id + "?api_key=b41296940c36d7ed60f4f56e9d17bf65";
            return  GetResponse(movieURL, "GET");    
        }
        private string GetResponse(string url, string method)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = method;
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch(Exception e)
            {
                return GetResponse(url, method);
            }
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
