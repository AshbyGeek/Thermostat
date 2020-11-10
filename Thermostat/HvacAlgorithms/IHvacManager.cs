using System.Collections.Generic;
using System.ComponentModel;
using Thermostat.Models;

namespace Thermostat.HvacAlgorithms
{
    public interface IHvacManager : INotifyPropertyChanged
    {
        IHvacAlgorithm CurrentHvacAlgorithm { get; set; }

        IReadOnlyList<IHvacAlgorithm> ValidModes { get; }

        HvacSetPoint CurrentSetPoint { get; }
    }
}