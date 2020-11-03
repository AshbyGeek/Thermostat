using System;
using System.Diagnostics.CodeAnalysis;

namespace Thermostat.ViewModels
{
    /// <summary>
    /// A reading (unit agnostic, so long as it fits in a double) that is associated with a date.
    /// Immutable.
    /// </summary>
    public struct DateModel : IEquatable<DateModel>
    {
        public DateModel(DateTime dateTime, double value)
        {
            DateTime = dateTime;
            Value = value;
        }

        public DateTime DateTime { get; }

        public double Value { get; }

        public bool Equals([AllowNull] DateModel other)
        {
            return DateTime == other.DateTime;
        }
    }
}
