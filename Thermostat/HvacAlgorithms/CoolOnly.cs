using System;
using System.Collections.Generic;
using System.Text;
using Thermostat.Models;

namespace Thermostat.HvacAlgorithms
{
    public class CoolOnly : IHvacAlgorithm
    {
        public HvacSystem GetNewSystemState(HvacSetPoint currentSetPoint, HvacSensors currentSensorValues)
        {
            if (currentSensorValues.IndoorTemp > currentSetPoint.MaxTemp)
            {
                return HvacSystem.NormalCooling;
            }
            else
            {
                return HvacSystem.Off;
            }
        }
    }
}
