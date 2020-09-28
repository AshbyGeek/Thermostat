using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using WeakEvent;

namespace Thermostat.Models
{
    public interface ISystemClock
    {
        int ResolutionMilliseconds { get; }

        event EventHandler<EventArgs> TimeTick;

        public DateTime Now { get; }
    }

    public class SystemClock : ISystemClock, IDisposable
    {
        
        public SystemClock()
        {
            _Timer = new Timer(ResolutionMilliseconds)
            {
                AutoReset = true
            };
            _Timer.Elapsed += (s, e) => OnTimeTick();
            _Timer.Start();
        }

        public int ResolutionMilliseconds { get; } = 1000;

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
