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
    /// <summary>
    /// Starting point of the whole application.
    /// Sets up services like databasing (including migrations), the system clock
    /// and then starts and shows the window
    /// </summary>
    class EntryPoint
    {
        [STAThread]
        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ThermostatContext>(options => options.UseSqlite(Settings.Default.DefaultConnection), ServiceLifetime.Transient);
            services.AddSingleton<ISystemClock>(new SystemClock());

            var serviceProvider = services.BuildServiceProvider();

            // Set up DB migrations after running the build ServiceProvider, because we need the DB to be able to migrate.
            using (var db = serviceProvider.GetService<ThermostatContext>())
            {
                db.Database.Migrate();

                //TODO: remove these bogus DEVELOPMENT values
                // For temporary development purposes, add some values to the database that can 
                // be viewed in history. Since we don't have any data to collect yet, make something up.
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

            // Actually start the application
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