using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI
{
    public class Program
    {
        /*
         * Tu jest punkt startowy aplikacji.
         * Jest tutaj jedna metoda, tworzenie WebHosta, kt�ra tworzy i konfiguruje Hosta 
         * WebHost to komponent, kt�ry zarz�dza cyklem �ycia aplikacji webowej, obs�uguje ��dania HTTP i zarz�dza konfiguracj� i uruchamianiem serwera.
         * na podstawie klasy Startup.
         * W tym przypadku metoda UseStartup ustawia klas� Startup jako punkt wej�cia dla aplikacji.
         */
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
