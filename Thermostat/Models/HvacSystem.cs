using System;
using System.Collections.Generic;
using System.Text;

namespace Thermostat.Models
{
    [Microsoft.EntityFrameworkCore.Owned]
    public class HvacSystem
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
