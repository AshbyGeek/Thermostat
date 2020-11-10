using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Thermostat.Models;

namespace Thermostat
{
    /// <summary>
    /// SystemIO Decorator that logs all values.
    /// Not actually in use yet, may need to change approaches.
    /// </summary>
    public class SystemIOLoggingDecorator: ISystemIO
    {
        private ISystemIO _BaseIO { get; }

        public HvacSensors CurrentSensorValues => _BaseIO.CurrentSensorValues;

        public HvacSystem CurrentSystemState => _BaseIO.CurrentSystemState;

        public SystemIOLoggingDecorator(ISystemIO baseIO)
        {
            //TODO: finish implementing
            _BaseIO = baseIO;
            throw new NotImplementedException();
        }
    }
}
