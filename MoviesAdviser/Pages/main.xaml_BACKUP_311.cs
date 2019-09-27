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

namespace MoviesAdviser.Pages
{
    /// <summary>
    /// Логика взаимодействия для main.xaml
    /// </summary>
    public partial class main : Page
    {
        public Services.HDKinoBrowser hdkinoBrowser;
        public TMDBBrowser tmdb;

<<<<<<< HEAD
        public static List<String> GenresList = new List<String>
        {           
            "Боевик",
            "Военный",
            "Детектив",
            "Документальный",
            "Комедия",           
            "Ужасы",
            "Фэнтези",
            "Триллер"
        };
=======
        public static List<String> GenresList;
>>>>>>> 1a9d7bd9c687c3e93c6c82d33609f64945b11cad

        public static List<String> CountriesList = new List<String>
        {
            "Россия",
            "США",
            "СССР",
            "Австралия",
            "Бельгия",
            "Великобритания",
            "Германия",
            "Гонконг",
            "Дания",
            "Индия",
            "Ирландия",
            "Испания",
            "Италия",
            "Канада",
            "Китай",
            "Корея Южная",
            "Франция",
            "Швеция",
            "Япония"
        };

        public main()
        {
            InitializeComponent();
            GenresList = HDKinoBrowser.Genres.Select(t=>t.Key).ToList();
            cb_genres.ItemsSource = GenresList;
            cb_country.ItemsSource = CountriesList;
            for (int i = DateTime.Now.Year; i >= 1930; i--)
            {
                cb_year.Items.Add(i);
            }
            lb_movies.ItemsSource = listTest;

            hdkinoBrowser = new Services.HDKinoBrowser();
            tmdb = new TMDBBrowser();
        }

        public static List<Models.Movie> listTest = new List<Models.Movie>
        {
            new Models.Movie {Title = "Древнее говно мамонта", Poster="123", Country="CCАCP", Year = 3568, Rating = 123},
            new Models.Movie {Title = "Во все хуи", Poster="11223", Country="ФФаФафц", Year = 763, Rating = 234},
            new Models.Movie {Title = "Пососи бульбу", Poster="1423", Country="CCфцафцпCP", Year = 2356, Rating = 345},
            new Models.Movie {Title = "Неважно", Poster="1ыр23", Country="фцп", Year = 9583, Rating = 456}
        };

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Test_Conn();
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
            var Genre = cb_genres.SelectedItem;
            var Country = cb_country.SelectedItem;
            var Year = cb_year.SelectedItem;
            var SortBy = cb_sortby.SelectedItem;
            Test_Conn();
            //var listTest = hdkinoBrowser.GetMoviesList("", 228, "");
            //lb_movies.ItemsSource = listTest;
            var listTest = tmdb.GetMoviesList((string) Genre, 228, "");
            lb_movies.ItemsSource = listTest;
        }
    }
}
