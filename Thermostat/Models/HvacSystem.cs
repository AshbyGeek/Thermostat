using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace Thermostat.Models
{
    /// <summary>
    /// Represents the current state of the Hvac Equipment in the system.
    /// Uses value based comparisons.
    /// </summary>
    [Microsoft.EntityFrameworkCore.Owned] ///This little flag flattens the database by indicating that this class should be folded into the <see cref="Database.State{T}"/> wrapper.
    public class HvacSystem : IEquatable<HvacSystem>, INotifyPropertyChanged
    {
        public HvacSystem(bool isCooling = false, bool isFanRunning = false, bool isHeating = false, bool isAuxHeating = false)
        {
            IsCooling = isCooling;
            IsFanRunning = isFanRunning;
            IsHeating = isHeating;
            IsAuxHeat = isAuxHeating;
        }

        public HvacSystem() { }

        public void UpdateFrom(HvacSystem other)
        {
            IsCooling = other.IsCooling;
            IsHeating = other.IsHeating;
            IsAuxHeat = other.IsAuxHeat;
            IsFanRunning = other.IsFanRunning;
        }

        /// <summary>
        /// Turns the A/C Compressor on or off via the Y wire.
        /// </summary>
        /// <value>
        ///   <c>true</c> when cooling; otherwise <c>false</c>.
        /// </value>
        public bool IsCooling
        {
            get => _IsCooling;
            set
            {
                if (_IsCooling != value)
                {
                    _IsCooling = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _IsCooling = false;

        /// <summary>
        /// Controlls the air handler's fan via the G wire.
        /// </summary>
        /// <value>
        ///   <c>true</c> when the fan is running; otherwise <c>false</c>.
        /// </value>
        public bool IsFanRunning
        {
            get => _IsFanRunning;
            set
            {
                if (_IsFanRunning != value)
                {
                    _IsFanRunning = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _IsFanRunning = false;

        /// <summary>
        /// Turns the furnace on or off via the W wire.
        /// </summary>
        /// <value>
        ///   <c>true</c> when heating; otherwise <c>false</c>.
        /// </value>
        public bool IsHeating
        {
            get => _IsHeating;
            set
            {
                if (_IsHeating != value)
                {
                    _IsHeating = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _IsHeating = false;

        /// <summary>
        /// Turns on auxilary heat via the X/Aux wire.
        /// </summary>
        /// <value>
        ///   <c>true</c> auxilary heat is on; otherwise <c>false</c>.
        /// </value>
        public bool IsAuxHeat
        {
            get => _IsAuxHeat;
            set
            {
                if (_IsAuxHeat != value)
                {
                    _IsAuxHeat = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _IsAuxHeat = false;

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            string str = "";
            if (IsCooling)
            {
                str += "Cooling";
            }
            else if (IsHeating)
            {
                str += "Heating";
                if (IsAuxHeat)
                {
                    str += " Lots";
                }
            }

            if (IsFanRunning)
            {
                str += " And Blowing";
            }

            return str;
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Value Based Comparison Implementation

        public bool Equals([AllowNull] HvacSystem other)
        {
            return !ReferenceEquals(other, null)
                   && IsCooling == other.IsCooling
                   && IsFanRunning == other.IsFanRunning
                   && IsHeating == other.IsHeating
                   && IsAuxHeat == other.IsAuxHeat;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as HvacSystem);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash |= (IsCooling ? 1 : 0);
            hash |= ((IsFanRunning ? 1 : 0) << 1);
            hash |= ((IsHeating ? 1 : 0) << 2);
            hash |= ((IsAuxHeat ? 1 : 0) << 3);

            return hash;
        }

        public static bool operator ==(HvacSystem obj1, object obj2)
        {

            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }
            else if (!ReferenceEquals(obj1, null)) // using reference equals null prevents accidental infinite recursion
            {
                return obj1.Equals(obj2);
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(HvacSystem obj1, object obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return false;
            }
            else if (!ReferenceEquals(obj1, null)) // using reference equals null prevents accidental infinite recursion
            {
                return !obj1.Equals(obj2);
            }
            else
            {
                return true;
            }
        }

        public static bool operator ==(HvacSystem obj1, HvacSystem obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }
            else if (!ReferenceEquals(obj1, null)) // using reference equals null prevents accidental infinite recursion
            {
                return obj1.Equals(obj2);
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(HvacSystem obj1, HvacSystem obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return false;
            }
            else if (!ReferenceEquals(obj1, null)) // using reference equals null prevents accidental infinite recursion
            {
                return !obj1.Equals(obj2);
            }
            else
            {
                return true;
            }
        }

        #endregion


        #region Static Convenience Objects

        public static HvacSystem NormalCooling = new HvacSystem(isCooling: true, isFanRunning: true);

        public static HvacSystem NormalHeating = new HvacSystem(isHeating: true, isFanRunning: true);

        public static HvacSystem Off = new HvacSystem();

        #endregion
    }
}
