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
                case "The Movie Database":                    
                    break;
                case "tvigle.ru":
                    movieInfo = TvigleBrowser.ParseMoreInfo(movieInfo);                    
                    break;                
            }
            this.DataContext = movieInfo;
            CheckFavorite();
            //tb_header.Text = movieObj.Title;
            //tb_description.Text = movieObj.Description;
        }

        private void DeleteFav(object sender, RoutedEventArgs e)
        {
            JSONProcessor.DeleteFromFavorites(movieInfo);
            setBtnFavToAdd();
        }

        private void AddFav_Click(object sender, RoutedEventArgs e)
        {
            JSONProcessor.AddToFavorites(movieInfo);
            setBtnFavToDelete();
        }
        private void CheckFavorite()
        {
            if (JSONProcessor.CheckFavorite(movieInfo))
            {
                setBtnFavToDelete();
            }
            else
            {
                setBtnFavToAdd();
            }
        }

        private void setBtnFavToAdd()
        {
            AddFav.Content = "Добавить в избранное";
            AddFav.Click += AddFav_Click;
        }
        private void setBtnFavToDelete()
        {
            AddFav.Content = "Удалить из избранного";
            AddFav.Click += DeleteFav;
        }
    }
}
