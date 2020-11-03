using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Thermostat.Models
{
    [Microsoft.EntityFrameworkCore.Owned]
    public class HvacSystem : IEquatable<HvacSystem>
    {
        /// <summary>
        /// Turns the A/C Compressor on or off via the Y wire.
        /// </summary>
        /// <value>
        ///   <c>true</c> when cooling; otherwise <c>false</c>.
        /// </value>
        public bool IsCooling { get; set; } = false;

        /// <summary>
        /// Controlls the air handler's fan via the G wire.
        /// </summary>
        /// <value>
        ///   <c>true</c> when the fan is running; otherwise <c>false</c>.
        /// </value>
        public bool IsFanRunning { get; set; } = false;

        /// <summary>
        /// Turns the furnace on or off via the W wire.
        /// </summary>
        /// <value>
        ///   <c>true</c> when heating; otherwise <c>false</c>.
        /// </value>
        public bool IsHeating { get; set; } = false;

        /// <summary>
        /// Turns on auxilary heat via the X/Aux wire.
        /// </summary>
        /// <value>
        ///   <c>true</c> auxilary heat is on; otherwise <c>false</c>.
        /// </value>
        public bool IsAuxHeat { get; set; } = false;

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


        public bool Equals([AllowNull] HvacSystem other)
        {
            return !ReferenceEquals(other, null)
                   && IsCooling == other.IsCooling
                   && IsFanRunning == other.IsFanRunning
                   && IsHeating == other.IsHeating
                   && IsAuxHeat == other.IsAuxHeat;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as HvacSystem);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash |=  (IsCooling     ? 1 : 0);
            hash |= ((IsFanRunning  ? 1 : 0) << 1);
            hash |= ((IsHeating     ? 1 : 0) << 2);
            hash |= ((IsAuxHeat     ? 1 : 0) << 3);

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


        public static HvacSystem NormalCooling = new HvacSystem()
        {
            IsCooling = true,
            IsFanRunning = true,
        };

        public static HvacSystem NormalHeating = new HvacSystem()
        {
            IsHeating = true,
            IsFanRunning = true,
        };

        public static HvacSystem Off = new HvacSystem();
    }
}
