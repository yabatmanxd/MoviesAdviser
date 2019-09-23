using MoviesAdviser.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MoviesAdviser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread WatchConnection;
        public static List<String> GenresList = new List<String>
        {
            "Биография",
            "Боевик",
            "Военный",
            "Детектив",
            "Документальный",
            "Комедия",
            "Мультфильм",
            "Ужасы",
            "Фэнтези",
            "Триллер"
        };

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

        public MainWindow()
        {                       
            InitializeComponent();
            cb_genres.ItemsSource = GenresList;
            cb_country.ItemsSource = CountriesList;
            for (int i = DateTime.Now.Year; i >= 1930; i--)
            {
                cb_year.Items.Add(i);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Test_Conn();
        }

        private void Bt_search_Click(object sender, RoutedEventArgs e)
        {
            var Genre = cb_genres.SelectedItem;
            var Country = cb_country.SelectedItem;
            var Year = cb_year.SelectedItem;
            var SortBy = cb_sortby.SelectedItem;
            Test_Conn();
        }

        private void Test_Conn()
        {
            if (!ConnectionTester.TestConnection())
            {
                new NoConnectionWindow().Show();
                Close();
            }
        }
    }
}
