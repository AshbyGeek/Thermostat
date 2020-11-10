using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Thermostat.Models;

namespace Thermostat.HardwareVirtualization
{
    /// <summary>
    /// A fake clock that allows for time to 'speed up' or 'slow down' as needed
    /// Causes the <see cref="TimeTick"/> event to trigger faster or slower while 
    /// reporting the usual interval.
    /// </summary>
    public class VirtualClock : ISystemClock
    {
        private readonly Timer _Timer;

        public VirtualClock()
        {
            _Timer = new Timer();
            _Timer.Elapsed += (s, e) => OnTimeTick();
            Now = DateTime.Now;
            _Timer.Start();
        }

        public double TimeMultiplier
        {
            get => 1.0 / _Timer.Interval;
            set
            {
                _Timer.Interval = 1.0 / value;
            }
        }

        public int ResolutionMilliseconds => 1000;

        public DateTime Now { get; private set; }


        public event EventHandler<EventArgs>? TimeTick;
        protected void OnTimeTick()
        {
            try
            {
                _Timer.Stop();

                Now += TimeSpan.FromMilliseconds(ResolutionMilliseconds);
                TimeTick?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                _Timer.Start();
            }
        }
    }
}
