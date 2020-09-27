using System;
using System.Collections.Generic;
using System.Text;
using Thermostat.Models;

namespace Thermostat.HvacAlgorithms
{
    public interface IHvacAlgorithm
    {
        HvacSystem GetNewSystemState(HvacSetPoint currentSetPoint, HvacSensors currentSensorValues);
    }

    public abstract class HvacManager
    {
        public HvacManager(ISystemClock clock, ISystemIO systemIO)
        {
            _Clock = clock;
            _IO = systemIO;
            _Clock.TimeTick += Clock_TimeTick;
        }

        private readonly ISystemClock _Clock;
        private readonly ISystemIO _IO;

        public HvacSetPoint CurrentSetPoint { get; set; }

        public IHvacAlgorithm CurrentHvacAlgorithm { get; set; }

        private void Clock_TimeTick(object sender, EventArgs e)
        {
            _IO.CurrentSystemState = CurrentHvacAlgorithm.GetNewSystemState(CurrentSetPoint, _IO.CurrentSensorValues);
        }
    }
}
