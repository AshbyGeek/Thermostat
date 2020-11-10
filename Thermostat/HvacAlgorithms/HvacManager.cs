using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Thermostat.Models;

namespace Thermostat.HvacAlgorithms
{

    public class HvacManager : INotifyPropertyChanged, IHvacManager
    {
        public HvacManager(ISystemClock clock, ISystemIO systemIO, IEnumerable<IHvacAlgorithm> validModes)
        {
            _Clock = clock;
            _IO = systemIO;

            ValidModes = validModes.ToList();
            _CurrentHvacAlgorithm = ValidModes.First(x => x.GetType() == typeof(NoHeatCool));

            _Clock.TimeTick += Clock_TimeTick;
        }

        private readonly ISystemClock _Clock;
        private readonly ISystemIO _IO;

        public HvacSetPoint CurrentSetPoint { get; } = new HvacSetPoint(200, -200);

        public IHvacAlgorithm CurrentHvacAlgorithm
        {
            get => _CurrentHvacAlgorithm;
            set
            {
                if (_CurrentHvacAlgorithm != value)
                {
                    _CurrentHvacAlgorithm = value;
                    OnPropertyChanged();
                }
            }
        }
        private IHvacAlgorithm _CurrentHvacAlgorithm;

        public IReadOnlyList<IHvacAlgorithm> ValidModes { get; }

        private void Clock_TimeTick(object? sender, EventArgs e)
        {
            _IO.CurrentSystemState.UpdateFrom(CurrentHvacAlgorithm.GetNewSystemState(CurrentSetPoint, _IO.CurrentSensorValues));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
