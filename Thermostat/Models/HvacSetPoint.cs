using System;
using System.Collections.Generic;
using System.Text;

namespace Thermostat.Models
{
    public class HvacSetPoint
    {
        /// <summary>
        /// The maximum temperature allowable before turning on the AC
        /// </summary>
        public double MaxTemp { get; set; }

        /// <summary>
        /// The minimum temperature allowable before turning on the heat
        /// </summary>
        public double MinTemp { get; set; }
    }
}
