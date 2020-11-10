using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Thermostat.Models
{
    /// <summary>
    /// Represents the current state of all hvac related sensors
    /// </summary>
    [Microsoft.EntityFrameworkCore.Owned] ///This little flag flattens the database by indicating that this class should be folded into the <see cref="Database.State{T}"/> wrapper.
    public class HvacSensors : INotifyPropertyChanged
    {
        public HvacSensors(double indoorTemp, double outdoorTemp)
        {
            IndoorTemp = indoorTemp;
            OutdoorTemp = outdoorTemp;
        }

        /// <summary>
        /// Entity Framework needs a parameterless constructor
        /// </summary>
        public HvacSensors() { }

        /// <summary>
        /// The current indoor temperature as measured by the thermostat, in degrees farenheit.
        /// </summary>
        public double IndoorTemp
        {
            get => _IndoorTemp;
            set
            {
                if (_IndoorTemp != value)
                {
                    _IndoorTemp = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _IndoorTemp;

        /// <summary>
        /// The current outdoor temperature, in degrees farenheit.
        /// </summary>
        public double OutdoorTemp 
        {
            get => _OutdoorTemp;
            set
            {
                if (_OutdoorTemp != value)
                {
                    _OutdoorTemp = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _OutdoorTemp;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
