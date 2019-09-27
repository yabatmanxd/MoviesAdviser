using System;
using MoviesAdviser.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
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
    /// Логика взаимодействия для waitConnection.xaml
    /// </summary>
    public partial class waitConnection : Page
    {
        Thread WaitConn;
        public waitConnection()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WaitConn = new Thread(new ThreadStart(delegate ()
            {
                while (true)
                {
                    if (ConnectionTester.TestConnection())
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate ()
                        {
                            var a = NavigationService.GetNavigationService(this);
                            a.Navigate((Uri)(new Uri("Pages/main.xaml", UriKind.Relative)));
                        });
                        WaitConn.Abort();
                    }
                    Thread.Sleep(1000);
                }
            }));
            WaitConn.IsBackground = true;
            WaitConn.Name = "Ожидание соединения";
            WaitConn.Start();
        }
    }
}
