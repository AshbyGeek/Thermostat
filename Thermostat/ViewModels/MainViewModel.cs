using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Thermostat.HvacAlgorithms;
using Thermostat.Models;

namespace Thermostat.ViewModels
{
    /// <summary>
    /// View model for <see cref="Views.MainView"/>
    /// </summary>
    public class MainViewModel: BaseViewModel
    {
        public MainViewModel(IChangeViewCommandProvider changeViewCommandProvider, IHvacManager hvacManager, ISystemIO systemIO)
        {
            // Dependency injection for these commands, makes this class simpler and more self contained.
            OpenHistoryCommand = changeViewCommandProvider.ShowHistoryViewCommand;
            OpenSettingsCommand = changeViewCommandProvider.ShowSettingsViewCommand;

            _SystemIO = systemIO;
            _SystemIO.CurrentSensorValues.PropertyChanged += CurrentSensorValues_PropertyChanged;

            _HvacManager = hvacManager;
            _HvacManager.PropertyChanged += HvacManager_PropertyChanged;
            _HvacManager.CurrentSetPoint.PropertyChanged += CurrentSetPoint_PropertyChanged;
        }

        private IHvacManager _HvacManager { get; }

        private ISystemIO _SystemIO { get; }


        /// <summary>
        /// Opens <see cref="Views.HistoryView"/>
        /// </summary>
        public ICommand OpenHistoryCommand { get; }

        /// <summary>
        /// Opens <see cref="Views.SettingsView"/>
        /// </summary>
        public ICommand OpenSettingsCommand { get; }

        public IReadOnlyList<IHvacAlgorithm> HvacModes => _HvacManager.ValidModes;

        public IHvacAlgorithm CurrentMode
        {
            get => _HvacManager.CurrentHvacAlgorithm;
            set => _HvacManager.CurrentHvacAlgorithm = value;
        }

        /// <summary>
        /// The min allowable temperature.
        /// Used by autoHeatCool and HeatOnly algorithms
        /// </summary>
        public double LowSetPoint
        {
            get => _HvacManager.CurrentSetPoint.MinTemp;
            set => _HvacManager.CurrentSetPoint.MinTemp = value;
        }

        /// <summary>
        /// The max allowable temperature.
        /// Used by AutoHeatCool and CoolOnly algorithms
        /// </summary>
        public double HighSetPoint
        {
            get => _HvacManager.CurrentSetPoint.MaxTemp;
            set => _HvacManager.CurrentSetPoint.MaxTemp = value;
        }

        public double CurrentTemperature => _SystemIO.CurrentSensorValues.IndoorTemp;




        private void CurrentSensorValues_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_SystemIO.CurrentSensorValues.IndoorTemp))
            {
                OnPropertyChanged(nameof(CurrentTemperature));
            }
        }
        private void HvacManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_HvacManager.CurrentHvacAlgorithm))
            {
                OnPropertyChanged(nameof(CurrentMode));
            }
        }

        private void CurrentSetPoint_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_HvacManager.CurrentSetPoint.MinTemp))
            {
                OnPropertyChanged(nameof(LowSetPoint));
            }
            if (e.PropertyName == nameof(_HvacManager.CurrentSetPoint.MaxTemp))
            {
                OnPropertyChanged(nameof(HighSetPoint));
            }
        }
    }
}
