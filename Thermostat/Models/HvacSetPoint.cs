using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Thermostat.Models
{
    /// <summary>
    /// Represents the current state of the (user adjusted) temperature set-points.
    /// </summary>
    [Microsoft.EntityFrameworkCore.Owned] ///This little flag flattens the database by indicating that this class should be folded into the <see cref="Database.State{T}"/> wrapper.
    public class HvacSetPoint : INotifyPropertyChanged
    {
        public HvacSetPoint(double maxTemp, double minTemp)
        {
            MaxTemp = maxTemp;
            MinTemp = minTemp;
        }

        /// <summary>
        /// Entity Framework needs a parameterless constructor
        /// </summary>
        public HvacSetPoint() { }


        /// <summary>
        /// The maximum temperature allowable before turning on the AC
        /// </summary>
        public double MaxTemp 
        {
            get => _MaxTemp;
            set
            {
                if (_MaxTemp != value)
                {
                    _MaxTemp = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _MaxTemp;

        /// <summary>
        /// The minimum temperature allowable before turning on the heat
        /// </summary>
        public double MinTemp
        {
            get => _MinTemp;
            set
            {
                if (_MinTemp != value)
                {
                    _MinTemp = value;
                    OnPropertyChanged();
                }
            }
        }
        private double _MinTemp;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
