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
using MoviesAdviser.Services;

namespace MoviesAdviser.Pages
{
    /// <summary>
    /// Логика взаимодействия для filmInfo.xaml
    /// </summary>
    public partial class filmInfo : Page
    {
        public Movie movieInfo { get; set; }
        public filmInfo(Movie movieObj, string type = "tmdb")
        {
            InitializeComponent();
            movieInfo = movieObj;
            switch (type)
            {
                case "tmdb": break;
                case "tvigle":
                    movieInfo = TvigleBrowser.parseMoreInfo(movieInfo);
                    break;
            }
            this.DataContext = movieInfo;
            //tb_header.Text = movieObj.Title;
            //tb_description.Text = movieObj.Description;
        }

        
    }
}
