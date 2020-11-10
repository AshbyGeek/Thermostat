using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Thermostat.HardwareVirtualization;
using Thermostat.HvacAlgorithms;
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
        static readonly VirtualClock Clock = new VirtualClock();
        static readonly VirtualSystem System = new VirtualSystem(Clock);

        [STAThread]
        static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ThermostatContext>(options => options.UseSqlite(Settings.Default.DefaultConnection), ServiceLifetime.Transient);
            services.AddSingleton<Func<ThermostatContext>>(x => x.GetService<ThermostatContext>); // Perhaps too clever, this creates a "service" that is simply a factory method for the database context

            // Add services for hardware
            services.AddSingleton<ISystemClock>(Clock);
            services.AddSingleton<ISystemIO>(System);

            // Add hvac modes
            services.AddSingleton<IHvacAlgorithm, AutoHeatCool>();
            services.AddSingleton<IHvacAlgorithm, NoHeatCool>();
            services.AddSingleton<IHvacAlgorithm, HeatOnly>();
            services.AddSingleton<IHvacAlgorithm, CoolOnly>();

            // Add services for thermostat
            services.AddSingleton<IHvacManager, HvacManager>();
            services.AddSingleton<WindowViewModel>();
            services.AddSingleton<IChangeViewCommandProvider>(x => x.GetService<WindowViewModel>());
            services.AddTransient<MainViewModel>();
            services.AddTransient<ScreensaverViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<HistoryViewModel>();


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
                    Data = new HvacSensors(72, 90),
                });
                db.HvacSensorHistory.Add(new State<HvacSensors>()
                {
                    DateTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(15)),
                    Data = new HvacSensors(75.6, 90.5)
                });
                db.HvacSensorHistory.Add(new State<HvacSensors>()
                {
                    DateTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(5)),
                    Data = new HvacSensors(70, 84)
                });

                db.SaveChanges();
            }


            // Actually start the application
            App application = new App();
            application.InitializeComponent();
            application.Startup += (s, e) =>
            {
                // Start the hardware simulation and its associated window
                var virtualVM = new VirtualHardwareViewModel(System, Clock);
                var virtualWindow = new Window
                {
                    Content = new VirtualHardwareView(),
                    DataContext = virtualVM
                };
                virtualWindow.Show();
                virtualWindow.Activate();

                var window = new Window
                {
                    DataContext = serviceProvider.GetService<WindowViewModel>()
                };
                window.Show();
                window.Activate();
            };
            application.Run();
        }
    }

}