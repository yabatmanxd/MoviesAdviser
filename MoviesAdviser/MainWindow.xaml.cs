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

namespace MoviesAdviser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            for (int i = DateTime.Now.Year; i >= 1900; i--)
            {
                cb_year.Items.Add(i);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
