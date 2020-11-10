using System;
using System.Collections.Generic;
using System.Text;
using Thermostat.Models;

namespace Thermostat.HvacAlgorithms
{
    public class AutoHeatCool : IHvacAlgorithm
    {
        public HvacSystem GetNewSystemState(HvacSetPoint currentSetPoint, HvacSensors currentSensorValues)
        {
            if (currentSensorValues.IndoorTemp > currentSetPoint.MaxTemp)
            { return HvacSystem.NormalCooling; }

            else if (currentSensorValues.IndoorTemp < currentSetPoint.MinTemp)
            { return HvacSystem.NormalHeating; }

            else
                return HvacSystem.Off;
            
        }
    }
} 
