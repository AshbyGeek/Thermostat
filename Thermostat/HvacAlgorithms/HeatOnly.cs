using System;
using System.Collections.Generic;
using System.Text;
using Thermostat.Models;

namespace Thermostat.HvacAlgorithms
{
    public class HeatOnly : IHvacAlgorithm
    {
        public HvacSystem GetNewSystemState(HvacSetPoint currentSetPoint, HvacSensors currentSensorValues)
        {
            if (currentSensorValues.IndoorTemp < currentSetPoint.MaxTemp)
            {
                return HvacSystem.NormalHeating;
            }
            else
            {
                return HvacSystem.Off;
            }
                

        }
    }
}
