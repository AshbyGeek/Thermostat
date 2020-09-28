using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Thermostat.Models.Database;
using Thermostat.Views;

namespace Thermostat.ViewModels
{
    public class WindowViewModel: BaseViewModel
    {
        private readonly IServiceProvider _ServiceProvider;
        private ThermostatContext _Db;

        public WindowViewModel(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;

            MainViewModel = new MainViewModel(new DelegateCommand(ShowHistoryView), new DelegateCommand(ShowSettingsView));
            CurrentViewModel = MainViewModel;

            ShowMainViewCommand = new DelegateCommand(ShowMainView);
        }

        public BaseViewModel CurrentViewModel
        {
            get => _CurrentViewModel;
            set
            {
                if (_CurrentViewModel != value)
                {
                    _CurrentViewModel = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsNotMainView));
                }
            }
        }
        private BaseViewModel _CurrentViewModel;
        MainViewModel MainViewModel;


        public bool IsNotMainView => CurrentViewModel != MainViewModel;

        private void ShowHistoryView()
        {
            _Db = _ServiceProvider.GetService<ThermostatContext>();
            CurrentViewModel = new HistoryViewModel(_Db);
        }

        private void ShowSettingsView()
        {
            CurrentViewModel = new SettingsViewModel();
        }

        private void ShowMainView()
        {
            CurrentViewModel = MainViewModel;

            _Db?.Dispose();
            _Db = null;
        }
        public ICommand ShowMainViewCommand { get; }
    }
}
