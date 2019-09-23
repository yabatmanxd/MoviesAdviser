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
using System.Windows.Shapes;

namespace MoviesAdviser
{
    /// <summary>
    /// Interaction logic for NoConnectionWindow.xaml
    /// </summary>
    public partial class NoConnectionWindow : Window
    {
        Thread WaitConn;
        public NoConnectionWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WaitConn = new Thread(new ThreadStart(delegate ()
            {
                while(true)
                {
                    if(ConnectionTester.TestConnection())
                    {
                        Application.Current.Dispatcher.Invoke((Action)delegate ()
                        {
                            new MainWindow().Show();
                            Close();
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
