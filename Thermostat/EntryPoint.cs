using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Thermostat.Models;
using Thermostat.Models.Database;
using Thermostat.Properties;
using Thermostat.ViewModels;
using Thermostat.Views;

namespace Thermostat
{

    class EntryPoint
    {
        [STAThread]
        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ThermostatContext>(options => options.UseSqlite(Settings.Default.DefaultConnection), ServiceLifetime.Transient);
            services.AddSingleton<ISystemClock>(new SystemClock());

            var serviceProvider = services.BuildServiceProvider();

            using (var db = serviceProvider.GetService<ThermostatContext>())
            {
                db.Database.Migrate();

                db.HvacSensorHistory.Add(new State<HvacSensors>()
                {
                    DateTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(20)),
                    Data = new HvacSensors()
                    {
                        IndoorTemp = 72,
                        OutdoorTemp = 90,
                    }
                });

                db.HvacSensorHistory.Add(new State<HvacSensors>()
                {
                    DateTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(15)),
                    Data = new HvacSensors()
                    {
                        IndoorTemp = 75.6,
                        OutdoorTemp = 90.5,
                    }
                });

                db.HvacSensorHistory.Add(new State<HvacSensors>()
                {
                    DateTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)),
                    Data = new HvacSensors()
                    {
                        IndoorTemp = 70,
                        OutdoorTemp = 84,
                    }
                });

                db.SaveChanges();
            }

            App application = new App();
            application.InitializeComponent();
            application.Startup += (s, e) =>
            {
                var window = new Window();
                window.Show();
                window.Activate();
                window.DataContext = new WindowViewModel(serviceProvider);
            };
            application.Run();
        }
    }

}