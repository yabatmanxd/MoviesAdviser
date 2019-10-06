using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using MoviesAdviser.Models;

namespace MoviesAdviser.Services
{

    //Поиск по tvigle.vip
    public class TvigleBrowser : BrowserInterface
    {
        public List<Movie> GetMoviesList(string genre, int year, string country, string sortMethod)
        {
            var movieList = new List<Movie>();

            //string address = Encoding.ASCII.GetString(Convert.FromBase64String("aHR0cHM6Ly93d3cua2lub25ld3MucnUvdG9wMTAwLXRocmlsbGVyLw=="));
            int idCountry = Dictionaries.tvigleCountries[country];
            int idGenre = Dictionaries.tvigleGenres[genre];
            string siteLink = @"https://www.tvigle.ru";
            string yearParamStr;
            if (year >= 2010 && year <= 2015)
                yearParamStr = "2010-2015";
            else
                if (year >= 2000 && year <= 2010)
                yearParamStr = "2000-2010";
            else
                    if (year >= 1990 && year <= 2000)
                yearParamStr = "1990-2000";
            else
                        if (year >= 1980 && year <= 1990)
                yearParamStr = "1980-1990";
            else
                            if (year > 2015)
                yearParamStr = year.ToString();
            else
                yearParamStr = "1980";

            string address = String.Format(siteLink + @"/catalog/filmy/?release_year={0}&category={1}&country={2}&o=", yearParamStr, idGenre, idCountry);
            string html;
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, ".NET Application");
                client.Encoding = Encoding.UTF8;
                try
                {
                    html = client.DownloadString(address);
                }
                catch
                {
                    MessageBox.Show("К сожалению, сервер в данный момент недоступен. Попробуйте позже.", "Movies Adviser - Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return movieList;
                }
            }

            // AngleSharp
            var htmlDoc = new HtmlParser().ParseDocument(html);

            var items = htmlDoc.QuerySelectorAll("a.product-list__item");

            foreach (var item in items)
            {
                var movieObj = new Movie();

                string tempInfo = item.QuerySelectorAll("div.product-list__item_info").First().TextContent;
                tempInfo = tempInfo.Replace("\n", "");
                tempInfo = tempInfo.Trim();
                movieObj.Country = tempInfo.Substring(0, tempInfo.LastIndexOf(','));
                string yearStr = tempInfo.Substring(tempInfo.LastIndexOf(',') + 1);
                movieObj.Year = Int32.Parse(yearStr);

                if (movieObj.Year == year)
                {
                    movieObj.Title = item.QuerySelectorAll("div.product-list__item_name").First().TextContent;
                    movieObj.Poster = "https://" + (item.QuerySelectorAll("img").First().GetAttribute("src")).Substring(2); // в ссылке сайта на картинку вначале 2 слеша, их нужно обрезать
                    movieObj.Link = siteLink + item.GetAttribute("href");

                    try
                    {
                        string rateImdb = item.QuerySelectorAll("span.meta-rate__imdb").First().LastChild.TextContent.Replace('.', ',');
                        string rateKinopoisk = item.QuerySelectorAll("span.meta-rate__kinopoisk").First().LastChild.TextContent.Replace('.', ',');
                        movieObj.Rating = Math.Round((Double.Parse(rateImdb) + Double.Parse(rateKinopoisk)) / 2, 2);
                    }
                    catch
                    {
                        movieObj.Rating = 0;
                    }

                    var listGenres = item.QuerySelectorAll("div.meta-labels").First().ChildNodes.Where(t => t.NodeName == "SPAN").Select(t => t.TextContent);
                    movieObj.Genre = String.Join(", ", listGenres);

                    movieList.Add(movieObj);
                }


            }
            if (sortMethod == "По рейтингу")
            {
                movieList = movieList.OrderByDescending(x => x.Rating).ToList();
            }
            return movieList;
        }

        public bool IsAvailable()
        {
            throw new NotImplementedException();
        }


    }
}
