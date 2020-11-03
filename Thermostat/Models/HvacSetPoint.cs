using System;
using System.Collections.Generic;
using System.Text;

namespace Thermostat.Models
{
    /// <summary>
    /// Represents the current state of the (user adjusted) temperature set-points.
    /// </summary>
    [Microsoft.EntityFrameworkCore.Owned] ///This little flag flattens the database by indicating that this class should be folded into the <see cref="Database.State{T}"/> wrapper.
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
