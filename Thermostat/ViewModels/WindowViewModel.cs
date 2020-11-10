using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Thermostat.HvacAlgorithms;
using Thermostat.Models.Database;
using Thermostat.Views;

namespace Thermostat.ViewModels
{
    /// <summary>
    /// ViewModel for the base window, which manages the different views
    /// </summary>
    public class WindowViewModel: BaseViewModel, IChangeViewCommandProvider
    {
        private MainViewModel MainVM
        {
            get
            {
                if (_MainVM is null)
                {
                    _MainVM = _ServiceProvider.GetService<MainViewModel>();
                }
                return _MainVM;
            }
        }
        private MainViewModel _MainVM;

        private readonly IServiceProvider _ServiceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider">Inversion of Control Container, provides a way to track and retrieve services</param>
        public WindowViewModel(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;

            ShowMainViewCommand = new DelegateCommand(ShowMainView);
            ShowHistoryViewCommand = new DelegateCommand(ShowHistoryView);
            ShowSettingsViewCommand = new DelegateCommand(ShowSettingsView);
            ShowScreenSaverCommand = new DelegateCommand(ShowScreenSaver);
        }

        /// <summary>
        /// ViewModel of the currently active view.
        /// The view uses this to determine which view to show.
        /// </summary>
        public BaseViewModel CurrentViewModel
        {
            get
            {
                if (_CurrentViewModel is null)
                {
                    _CurrentViewModel = MainVM;
                }
                return _CurrentViewModel;
            }
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
        private BaseViewModel? _CurrentViewModel;

        public bool IsNotMainView => CurrentViewModel != MainVM;


        /// <summary>
        /// Shows the History View.
        /// Backing function for an ICommand.
        /// </summary>
        private void ShowHistoryView() => CurrentViewModel = _ServiceProvider.GetService<HistoryViewModel>();
        public ICommand ShowHistoryViewCommand { get; }

        /// <summary>
        /// Shows the Settings View.
        /// Backing function for an ICommand.
        /// </summary>
        private void ShowSettingsView() => CurrentViewModel = _ServiceProvider.GetService<SettingsViewModel>();
        public ICommand ShowSettingsViewCommand { get; }

        /// <summary>
        /// Shows the main view.
        /// Backing function for an ICommand.
        /// </summary>
        private void ShowMainView() => CurrentViewModel = MainVM;
        public ICommand ShowMainViewCommand { get; }

        private void ShowScreenSaver() => CurrentViewModel = _ServiceProvider.GetService<ScreensaverViewModel>();
        public ICommand ShowScreenSaverCommand { get; }
    }
}
