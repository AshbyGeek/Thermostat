using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using WeakEvent;

namespace Thermostat.Models
{
    /// <summary>
    /// Interface representing a system clock.
    /// </summary>
    /// <remarks>
    /// Wrapping this very simple concept inside our own structure allows us to easily 
    /// make a fake clock that isn't actually time based (will make unit testing much nicer).
    /// </remarks>
    public interface ISystemClock
    {
        /// <summary>
        /// The best possible resolution of this clock, in milliseconds.
        /// </summary>
        /// <remarks>
        /// If your clock can resolve times smaller than a millisecond, then you
        /// might want to make your own ISystemClock instead of using this one...
        /// </remarks>
        int ResolutionMilliseconds { get; }

        /// <summary>
        /// Event triggered on a regularly recuring interval.
        /// Time between events is <see cref="ResolutionMilliseconds"/>.
        /// </summary>
        event EventHandler<EventArgs> TimeTick;

        /// <summary>
        /// The current time and date
        /// </summary>
        public DateTime Now { get; }
    }

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
