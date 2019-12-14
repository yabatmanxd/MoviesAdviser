using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MoviesAdviser.Services
{
    //Проверяет наличие интернет соединения и доступа к конкретному сайту
    class ConnectionTester
    {        
        public static bool TestConnection()
        {
            //Пока жив гугл, жив и интернет!
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/"))
                    return true;
            }
            catch
            {
                return false;             
            }
        }
             
    }
}
