using System;
using System.Collections.Generic;
using System.Text;
using Thermostat.Models;

namespace Thermostat.HvacAlgorithms
{
    public class NoHeatCool : IHvacAlgorithm
    {
        public string Name => "Off";

        public HvacSystem GetNewSystemState(HvacSetPoint currentSetPoint, HvacSensors currentSensorValues)
        {
            return HvacSystem.Off;
        }
    }
}
