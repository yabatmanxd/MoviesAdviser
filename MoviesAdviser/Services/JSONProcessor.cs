using MoviesAdviser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAdviser.Services
{
    //Сериализатор/Десериализатор
    class JSONProcessor
    {
        private const string FileName = "favorites.json";
        public static void AddToFavorites(Movie movie)
        {
            List<Movie> favorites = GetFavorites();
            favorites.Add(movie);
            using (StreamWriter file = File.CreateText(FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, favorites);
            }
        }

        public static void DeleteFromFavorites(Movie movie)
        {
            List<Movie> favorites = GetFavorites();
            favorites.RemoveAll(m => m.Title == movie.Title);
            using (StreamWriter file = File.CreateText(FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, favorites);
            }
        }
        
        public static List<Movie> GetFavorites()
        {
            List<Movie> favorites;
            if (File.Exists(FileName))
            {
                using (StreamReader file = File.OpenText(FileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    favorites = (List<Movie>) serializer.Deserialize(file, typeof(List<Movie>));
                }
            }
            else
            {
                favorites = new List<Movie>();
            }
            return favorites;
        }

        public static bool CheckFavorite(Movie movie)
        {
            List<Movie> favorites = GetFavorites();
            return favorites.Find(m => m.Title == movie.Title) != null;     
        }
    }
}
