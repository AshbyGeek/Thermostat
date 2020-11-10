using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Thermostat.Models
{
    /// <summary>
    /// Represents the current state of the system, both input and output.
    /// </summary>
    public interface ISystemIO
    {
        HvacSensors CurrentSensorValues { get; }

        HvacSystem CurrentSystemState { get; }
    }
}
