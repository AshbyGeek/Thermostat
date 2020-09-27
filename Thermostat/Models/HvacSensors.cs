using System;
using System.Collections.Generic;
using System.Text;

namespace Thermostat.Models
{
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
