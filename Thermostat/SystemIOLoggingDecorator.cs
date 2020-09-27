using System;
using System.Collections.Generic;
using System.Text;
using Thermostat.Models;

namespace Thermostat
{
    public class SystemIOLoggingDecorator: ISystemIO
    {
        private ISystemIO _BaseIO { get; }

        public HvacSensors CurrentSensorValues => _BaseIO.CurrentSensorValues;

        public HvacSystem CurrentSystemState { set => _BaseIO.CurrentSystemState = value; }

        public SystemIOLoggingDecorator(ISystemIO baseIO)
        {
            //TODO: finish implementing
            _BaseIO = baseIO;
            throw new NotImplementedException();
        }
    }
}
