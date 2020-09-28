using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Thermostat.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public MainViewModel(ICommand openHistory, ICommand openSettings)
        {
            OpenHistoryCommand = openHistory;
            OpenSettingsCommand = openSettings;
        }

        public ICommand OpenHistoryCommand { get; }

        public ICommand OpenSettingsCommand { get; }


        public int LowSetPoint 
        {
            get => _LowSetPoint;
            set
            {
                if (_LowSetPoint != value)
                {
                    _LowSetPoint = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _LowSetPoint = 70;

        public int HighSetPoint
        {
            get => _HighSetPoint;
            set
            {
                if (_HighSetPoint != value)
                {
                    _HighSetPoint = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _HighSetPoint = 70;
    }
}
