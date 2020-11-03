using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Thermostat.ViewModels
{
    /// <summary>
    /// View model for <see cref="Views.MainView"/>
    /// </summary>
    public class MainViewModel: BaseViewModel
    {
        public MainViewModel(ICommand openHistory, ICommand openSettings)
        {
            // Dependency injection for these commands, makes this class simpler and more self contained.
            OpenHistoryCommand = openHistory;
            OpenSettingsCommand = openSettings;
        }

        /// <summary>
        /// Opens <see cref="Views.HistoryView"/>
        /// </summary>
        public ICommand OpenHistoryCommand { get; }

        /// <summary>
        /// Opens <see cref="Views.SettingsView"/>
        /// </summary>
        public ICommand OpenSettingsCommand { get; }

        /// <summary>
        /// The min allowable temperature.
        /// Used by autoHeatCool and HeatOnly algorithms
        /// </summary>
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

        /// <summary>
        /// The max allowable temperature.
        /// Used by AutoHeatCool and CoolOnly algorithms
        /// </summary>
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
