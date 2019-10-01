using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoviesAdviser.Models;
using AngleSharp.Html.Parser;
using System.Net;

namespace MoviesAdviser.Pages
{
    /// <summary>
    /// Логика взаимодействия для filmInfo.xaml
    /// </summary>
    public partial class filmInfo : Page
    {
        public filmInfo(Movie movieObj, string type = "tmdb")
        {
            InitializeComponent();

            switch (type)
            {
                case "tmdb": break;
                case "tvigle":
                    movieObj = parseMoreInfo(movieObj);
                    break;
            }

            tb_header.Text = movieObj.Title;
            tb_description.Text = movieObj.Description;
        }

        private Movie parseMoreInfo(Movie mvObj)
        {
            string address = mvObj.Link;
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
                    return mvObj;
                }
            }
            var htmlObj = new HtmlParser().ParseDocument(html);
            var divs = htmlObj.QuerySelectorAll("div").Where(x => x.GetAttribute("itemprop") == "description");
            mvObj.Description = divs.First().ChildNodes[1].TextContent;


            return mvObj;
        }
    }
}
