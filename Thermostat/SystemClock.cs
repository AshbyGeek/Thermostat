using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using WeakEvent;

namespace Thermostat.Models
{
    public interface ISystemClock
    {
        const int ResolutionMilliseconds = 1000;

        event EventHandler<EventArgs> TimeTick;

        public DateTime Now { get; }
    }

    public class SystemClock : ISystemClock, IDisposable
    {
        public SystemClock()
        {
            _Timer = new Timer(ISystemClock.ResolutionMilliseconds)
            {
                AutoReset = true
            };
            _Timer.Elapsed += (s, e) => OnTimeTick();
            _Timer.Start();
        }

        public DateTime Now => DateTime.Now;

        private readonly Timer _Timer;

        private readonly WeakEventSource<EventArgs> _TimeTickSource = new WeakEventSource<EventArgs>();
        public event EventHandler<EventArgs> TimeTick
        {
            add { _TimeTickSource.Subscribe(value); }
            remove { _TimeTickSource.Unsubscribe(value); }
        }
        protected void OnTimeTick()
        {
            _TimeTickSource.Raise(this, EventArgs.Empty, HandleException);
        }
        private bool HandleException(Exception ex)
        {
            // TODO: logging and more robust handling
            return true;
        }

        public void Dispose()
        {
            _Timer.Dispose();
        }
    }
}
