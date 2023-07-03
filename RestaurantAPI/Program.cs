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
         * Jest tutaj jedna metoda, tworzenie WebHosta, która tworzy i konfiguruje Hosta 
         * WebHost to komponent, który zarz¹dza cyklem ¿ycia aplikacji webowej, obs³uguje ¿¹dania HTTP i zarz¹dza konfiguracj¹ i uruchamianiem serwera.
         * na podstawie klasy Startup.
         * W tym przypadku metoda UseStartup ustawia klasê Startup jako punkt wejœcia dla aplikacji.
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
