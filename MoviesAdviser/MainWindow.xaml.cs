using System;
using MoviesAdviser.Services;
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
using MoviesAdviser.Pages;

namespace MoviesAdviser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {                       
            InitializeComponent();
            
            
        }

        private void Favs_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Uri("Pages/favorites.xaml", UriKind.Relative));           
        }
    }
}
