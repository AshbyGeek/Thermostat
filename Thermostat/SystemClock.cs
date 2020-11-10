using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using WeakEvent;

namespace Thermostat.Models
{
    /// <summary>
    /// Actual implementation of a clock, based on C#'s <see cref="Timer"/> class.
    /// Resolution of 1 second.
    /// </summary>
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

        // WeakEventSource lets us ensure that this class will never be cause of memory leaks.
        // Memory leaks in languages that use garbage collection are almost exclusively due to objects retaining
        // references to objects without the developers noticing. Events are easy to overlook.
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
