using System;
using System.Collections.Generic;
using System.Text;

namespace Thermostat.HardwareVirtualization
{
    public class VirtualHardwareViewModel
    {
        public VirtualSystem System { get; }

        public VirtualClock Clock { get; }

        public VirtualHardwareViewModel(VirtualSystem system, VirtualClock clock)
        {
            System = system;
            Clock = clock;
        }

        public double OutdoorTemp
        {
            get => System.CurrentSensorValues.OutdoorTemp;
            set => System.CurrentSensorValues.OutdoorTemp = value;
        }

        public VirtualHardwareViewModel()
        {
            Clock = new VirtualClock();
            System = new VirtualSystem(Clock);
        }
    }
}
