﻿using System;
using System.Collections.Generic;
using System.Text;
using Thermostat.Models;

namespace Thermostat.HvacAlgorithms
{
    public class AutoHeatCool : IHvacAlgorithm
    {
        public string Name => "Auto";

        public HvacSystem GetNewSystemState(HvacSetPoint currentSetPoint, HvacSensors currentSensorValues)
        {
            //TODO: implement
            throw new NotImplementedException();
        }
    }
}
