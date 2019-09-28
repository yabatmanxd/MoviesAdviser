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
            //Создаем объект асинхронной задачи, чтобы выполнять загрузку фильмов в другом потоке
            BackgroundWorker bg = new BackgroundWorker();
            //Так как в асинхронную задачу в качестве параметра можно передать только один объект, то я использую dynamic
            //Можно было бы обойтись без него, но тогда нужен класс Filters с полями Genre, и т.д.
            //В объект dynamic можно в любой момент добавлять любые поля. Их тип тоже будет dynamic, но при обращении к ним можно кастовать их к любому другому типу.
            //Например (int) filters.Year;
            dynamic filters = new ExpandoObject();
            filters.Genre = Genre;
            filters.Country = Country;
            filters.Year = Year;
            filters.SortBy = SortBy;
            filters.Site = Site;
            if (Site.Equals("tvigle.ru"))
            {
                //DoWork содержит в себе делегаты методов, которые будут выполнены, когда запустится асинхронная задача.
                //Делегаты добавляются точно так же, как в обработчике события: через +=
                bg.DoWork += tvigle_work;                
                //tmpList = hdkinoBrowser.GetMoviesList((string)Genre, (int)Year, (string)Country, SortBy);
            }
            else
            {
                bg.DoWork += TMDB_work;
                
               // tmpList = tmdb.GetMoviesList((string)Genre, (int)Year, "", SortBy);
            }
            //Делегаты методов, которые будут выполнен после завершения всех методов DoWork.
            bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
            //Запуск асинхронной задачи. В нее мы передаем динамический объект filters
            bg.RunWorkerAsync(filters);
        }

        

        private void TMDB_work(object sender, DoWorkEventArgs e)
        {
            //В e.Argument лежит объект filters, который передали в RunWorkerAsync
            dynamic filters = e.Argument;
            //Обязательно приводим поля filters к нужному типу, потому что они все dynamic
            List<Movie> movies = tmdb.GetMoviesList((string)filters.Genre, (int)filters.Year, (string)filters.Country, (string)filters.SortBy);
            //Возвращаемое значение асинхронной задачи. Будет использовано в Bg_RunWorkerCompleted
            e.Result = movies;
        }
        private void tvigle_work(object sender, DoWorkEventArgs e)
        {
            //Всё аналогично предыдущему методу
            dynamic filters = e.Argument;
            List<Movie> movies = hdkinoBrowser.GetMoviesList((string)filters.Genre, (int)filters.Year, (string)filters.Country, (string)filters.SortBy);
            e.Result = movies;
        }
        private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Задача выполнена, проверяем результаты, прячем гифку и т.д.
            //В e.Result мы положили список загруженных фильмов, нужно его считать и привести к правильному типу
            List<Movie> movies = (List<Movie>)e.Result;
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
    }
}
