using System;
using MoviesAdviser.Services;
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
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;
using System.Dynamic;

namespace MoviesAdviser.Pages
{
    /// <summary>
    /// Логика взаимодействия для main.xaml
    /// </summary>
    public partial class main : Page
    {
        public HDKinoBrowser hdkinoBrowser;
        public TMDBBrowser tmdb;

        Thread BrowsingThread;

        public static List<String> GenresList;

        public main()
        {
            InitializeComponent();
            GenresList = Dictionaries.hdkinoGenres.Select(t=>t.Key).ToList();
            cb_genres.ItemsSource = GenresList;
            cb_country.ItemsSource = Dictionaries.hdkinoCountries.Select(t => t.Key).ToList();
            for (int i = DateTime.Now.Year; i >= 1930; i--)
            {
                cb_year.Items.Add(i);
            }

            hdkinoBrowser = new HDKinoBrowser();
            tmdb = new TMDBBrowser();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Test_Conn();
            gifLoad.Visibility = Visibility.Hidden;
        }

        private void Test_Conn()
        {
            if (!ConnectionTester.TestConnection())
            {
                //new NoConnectionWindow().Show();
                var a = NavigationService.GetNavigationService(this);
                a.Navigate((Uri)(new Uri("Pages/waitConnection.xaml", UriKind.Relative)));

            }
        }

        private void Bt_search_Click(object sender, RoutedEventArgs e)
        {
            List<Movie> tmpList;
            tb_hint.Text = "";
            lb_movies.Visibility = Visibility.Hidden;
            gifLoad.Visibility = Visibility.Visible;  

            
            var Genre = cb_genres.SelectedItem;
            var Country = cb_country.SelectedItem;
            var Year = cb_year.SelectedItem;
            var SortBy = ((TextBlock) cb_sortby.SelectedItem).Text;
            var Site = ((TextBlock)cb_site.SelectedItem).Text;
            Test_Conn();
            BackgroundWorker bg = new BackgroundWorker();
            dynamic filters = new ExpandoObject();
            filters.Genre = Genre;
            filters.Country = Country;
            filters.Year = Year;
            filters.SortBy = SortBy;
            filters.Site = Site;
            if (Site.Equals("tvigle.ru"))
            {
                bg.DoWork += tvigle_work;                
                //tmpList = hdkinoBrowser.GetMoviesList((string)Genre, (int)Year, (string)Country, SortBy);
            }
            else
            {
                bg.DoWork += TMDB_work;
                
               // tmpList = tmdb.GetMoviesList((string)Genre, (int)Year, "", SortBy);
            }
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            bg.RunWorkerAsync(filters);
        }

        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<Movie> movies = (List<Movie>) e.Result;
            if (movies.Count <= 0)
            {
                tb_hint.Text = "К сожалению по данным критериям фильмов не найдено";
                lb_movies.ItemsSource = null;
            }
            else
            {
                lb_movies.ItemsSource = movies;
            }
            gifLoad.Visibility = Visibility.Hidden;
            lb_movies.Visibility = Visibility.Visible;
        }

        private void TMDB_work(object sender, DoWorkEventArgs e)
        {
            dynamic filters = e.Argument;
            List<Movie> movies = tmdb.GetMoviesList((string)filters.Genre, (int)filters.Year, (string)filters.Country, (string)filters.SortBy);
            e.Result = movies;
        }
        private void tvigle_work(object sender, DoWorkEventArgs e)
        {
            dynamic filters = e.Argument;
            List<Movie> movies = hdkinoBrowser.GetMoviesList((string)filters.Genre, (int)filters.Year, (string)filters.Country, (string)filters.SortBy);
            e.Result = movies;
        }
    }
}
