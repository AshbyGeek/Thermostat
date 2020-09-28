using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Thermostat.Models;
using Thermostat.Models.Database;
using Thermostat.Properties;

namespace Thermostat
{

    class EntryPoint
    {
        [STAThread]
        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ThermostatContext>(options => options.UseSqlite(Settings.Default.DefaultConnection));
            services.AddSingleton<ISystemClock>(new SystemClock());

            var serviceProvider = services.BuildServiceProvider();

            using (var db = serviceProvider.GetService<ThermostatContext>())
            {
                db.Database.Migrate();
            }

            App application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }

}