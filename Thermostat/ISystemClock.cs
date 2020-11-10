using System;

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
}
