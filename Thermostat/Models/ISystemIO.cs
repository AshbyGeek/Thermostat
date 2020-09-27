using System;
using System.Collections.Generic;
using System.Text;

namespace Thermostat.Models
{
    public interface ISystemIO
    {
        HvacSensors CurrentSensorValues { get; }

        HvacSystem CurrentSystemState { set; }
    }
}
