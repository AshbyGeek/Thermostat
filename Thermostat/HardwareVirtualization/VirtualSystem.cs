using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Thermostat.Models;

namespace Thermostat.HardwareVirtualization
{
    /// <summary>
    /// A simple simulation of HVAC hardware.
    /// Moves temperature towards outside temp at a rate relative to the difference between the inside and outside temperature.
    /// Heat/AuxHeat/Cool moves temperature a fixed amount up or down.
    /// </summary>
    public class VirtualSystem : ISystemIO, INotifyPropertyChanged
    {
        public VirtualSystem(ISystemClock clock)
        {
            Clock = clock;
            Clock.TimeTick += Clock_TimeTick;
        }

        public ISystemClock Clock { get; }

        public HvacSensors CurrentSensorValues { get; } = new HvacSensors(70, 80);

        public HvacSystem CurrentSystemState { get; } = new HvacSystem();


        public double HeatTransferRate
        {
            get => _HeatTransferRate;
            set
            {
                if (_HeatTransferRate != value)
                {
                    _HeatTransferRate = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _HeatTransferRate = 2;



        private void Clock_TimeTick(object? sender, EventArgs e)
        {
            var newTemp = CurrentSensorValues.IndoorTemp;

            if (CurrentSystemState.IsHeating)
            {
                newTemp += HeatTransferRate;
            }

            if (CurrentSystemState.IsAuxHeat)
            {
                newTemp += HeatTransferRate;
            }

            if (CurrentSystemState.IsCooling)
            {
                newTemp -= HeatTransferRate;
            }

            // Heat Transfer to/from the outside
            var tempDir = (CurrentSensorValues.OutdoorTemp - CurrentSensorValues.IndoorTemp) / Math.Abs(CurrentSensorValues.OutdoorTemp - CurrentSensorValues.IndoorTemp);
            newTemp += HeatTransferRate * tempDir / 60;

            CurrentSensorValues.IndoorTemp = newTemp;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
