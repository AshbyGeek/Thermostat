using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Thermostat.Views;

namespace Thermostat.ViewModels
{
    public class WindowViewModel: BaseViewModel
    {
        public WindowViewModel()
        {
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
            CurrentViewModel = new HistoryViewModel();
        }

        private void ShowSettingsView()
        {
            CurrentViewModel = new SettingsViewModel();
        }

        private void ShowMainView()
        {
            CurrentViewModel = MainViewModel;
        }
        public ICommand ShowMainViewCommand { get; }
    }
}
