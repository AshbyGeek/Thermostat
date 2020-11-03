using System;
using System.Collections.Generic;
using System.Text;

namespace Thermostat.Models
{
    /// <summary>
    /// Represents the current state of all hvac related sensors
    /// </summary>
    [Microsoft.EntityFrameworkCore.Owned] ///This little flag flattens the database by indicating that this class should be folded into the <see cref="Database.State{T}"/> wrapper.
    public class HvacSensors
    {
        /// <summary>
        /// The current indoor temperature as measured by the thermostat, in degrees farenheit.
        /// </summary>
        public double IndoorTemp { get; set; }

        /// <summary>
        /// The current outdoor temperature, in degrees farenheit.
        /// </summary>
        public double OutdoorTemp { get; set; }
    }
}
