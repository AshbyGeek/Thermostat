using Thermostat.Models;

namespace Thermostat.HvacAlgorithms
{
    /// <summary>
    /// Defines how an algorithm interacts with system state
    /// </summary>
    public interface IHvacAlgorithm
    {
        HvacSystem GetNewSystemState(HvacSetPoint currentSetPoint, HvacSensors currentSensorValues);

        string Name { get; }
    }
}
