﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using MoviesAdviser.Models;

namespace MoviesAdviser.Services
{
    //Поиск по 2hdkino.vip
    public class HDKinoBrowser : BrowserInterface
    {
        public List<Movie> GetMoviesList(string genre, int year, string country)
        {
            var movieList = new List<Movie>();

            //string address = Encoding.ASCII.GetString(Convert.FromBase64String("aHR0cHM6Ly93d3cua2lub25ld3MucnUvdG9wMTAwLXRocmlsbGVyLw=="));
            string address = @"https://www.tvigle.ru/catalog/filmy/";
            string html;
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, ".NET Application");
                client.Encoding = Encoding.UTF8;
                html = client.DownloadString(address);
            }

            // AngleSharp
            var htmlDoc = new HtmlParser().ParseDocument(html);

            var items = htmlDoc.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("product-list__item"));

            foreach (var item in items)
            {
                var movieObj = new Movie();
                movieObj.Title = item.QuerySelectorAll("div").Where(t => t.ClassName != null && t.ClassName.Contains("product-list__item_name")).First().TextContent;
                string tempInfo = item.QuerySelectorAll("div").Where(t => t.ClassName != null && t.ClassName.Contains("product-list__item_info")).First().TextContent;
                tempInfo = tempInfo.Replace("\n", "");
                tempInfo = tempInfo.Trim();
                movieObj.Country = tempInfo.Substring(0, tempInfo.LastIndexOf(','));
                string yearStr = tempInfo.Substring(tempInfo.LastIndexOf(',')+1);
                movieObj.Year = Int32.Parse(yearStr);
                movieObj.Poster = "aaa";
                movieObj.Rating = 12;
                movieList.Add(movieObj);
            }

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
