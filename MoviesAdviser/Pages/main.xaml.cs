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

namespace MoviesAdviser.Pages
{
    /// <summary>
    /// Логика взаимодействия для main.xaml
    /// </summary>
    public partial class main : Page
    {
        public HDKinoBrowser hdkinoBrowser;
        public TMDBBrowser tmdb;

        Thread testThread;

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
            tb_hint.Text = "";                        // вот эта хуета срабатывает только после того как получили
            gifLoad.Visibility = Visibility.Visible;  // весь список фильмов, а хотелось бы чтобы текстбокс сначала очистился, а потом уже подгрузился список фильмов
                                                      // ну и анимка загрузки соответственно должна сначала запустится перед неачалом загрузки и после окончания спрятать её


            var Genre = cb_genres.SelectedItem;
            var Country = cb_country.SelectedItem;
            var Year = cb_year.SelectedItem;
            var SortBy = ((TextBlock) cb_sortby.SelectedItem).Text;
            var Site = ((TextBlock)cb_site.SelectedItem).Text;
            Test_Conn();

            if (Site.Equals("tvigle.ru"))
            {

                tmpList = hdkinoBrowser.GetMoviesList((string)Genre, (int)Year, (string)Country, SortBy);
            }
            else
            {
                tmpList = tmdb.GetMoviesList((string)Genre, (int)Year, "", SortBy);
            }
            if (tmpList.Count <= 0)
            {
                tb_hint.Text = "К сожалению по данным критериям фильмов не найдено";
                lb_movies.ItemsSource = null;
            }
            else
            {
                lb_movies.ItemsSource = tmpList;
            }
            gifLoad.Visibility = Visibility.Hidden;
        }
    }
}
