using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Thermostat.Models.Database;
using Thermostat.Views;

namespace Thermostat.ViewModels
{
    /// <summary>
    /// ViewModel for the base window, which manages the different views
    /// </summary>
    public class WindowViewModel: BaseViewModel
    {
        private readonly IServiceProvider _ServiceProvider;
        private readonly MainViewModel MainViewModel; // need to keep track of this guy cause he's important

        private ThermostatContext _Db; //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider">Inversion of Control Container, provides a way to track and retrieve services</param>
        public WindowViewModel(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;

            MainViewModel = new MainViewModel(new DelegateCommand(ShowHistoryView), new DelegateCommand(ShowSettingsView));
            CurrentViewModel = MainViewModel;

            ShowMainViewCommand = new DelegateCommand(ShowMainView);
        }

        /// <summary>
        /// ViewModel of the currently active view.
        /// The view uses this to determine which view to show.
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get => _CurrentViewModel;
            set
            {
                if (_CurrentViewModel != value)
                {
                    _CurrentViewModel = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsNotMainView)); /// <see cref="IsNotMainView"/> derives from this property, so it also changes
                }
            }
        }
        private BaseViewModel _CurrentViewModel;

        public bool IsNotMainView => CurrentViewModel != MainViewModel;

        public ICommand ShowMainViewCommand { get; }

        /// <summary>
        /// Shows the History View.
        /// Backing function for an ICommand.
        /// </summary>
        private void ShowHistoryView()
        {
            _Db = _ServiceProvider.GetService<ThermostatContext>();
            var clock = _ServiceProvider.GetService<Models.ISystemClock>();
            CurrentViewModel = new HistoryViewModel(_Db, clock);

            // Note that ordinarily we would need to dispose of the _Db, but in this case the HistoryView needs him until that view is closed
            // EntityFramework leans towards short lived database contexts, so we should dispose of it as soon as is reasonable
        }

        /// <summary>
        /// Shows the Settings View.
        /// Backing function for an ICommand.
        /// </summary>
        private void ShowSettingsView()
        {
            CurrentViewModel = new SettingsViewModel();
        }

        /// <summary>
        /// Shows the main view.
        /// Backing function for an ICommand.
        /// </summary>
        private void ShowMainView()
        {
            CurrentViewModel = MainViewModel;

            // Dispose of the database, just in case the old view used the database
            _Db?.Dispose();
            _Db = null;
        }
    }
}
