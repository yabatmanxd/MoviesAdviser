using MoviesAdviser.Models;
using MoviesAdviser.Services;
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

namespace MoviesAdviser.Pages
{
    /// <summary>
    /// Interaction logic for favorites.xaml
    /// </summary>
    public partial class favorites : Page
    {
        public favorites()
        {
            InitializeComponent();
            lb_movies.ItemsSource = JSONProcessor.GetFavorites();
        }
        private void Lb_movies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var navService = NavigationService.GetNavigationService(this);
            var filmObj = (Movie)lb_movies.SelectedItem;
            var filmPage = new filmInfo(filmObj, "");
            filmPage.Title = filmObj.Title + " - Информация о фильме";
            navService.Navigate(filmPage);
        }
    }
}
