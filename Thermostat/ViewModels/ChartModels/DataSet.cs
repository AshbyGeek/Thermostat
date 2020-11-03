using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using Thermostat.Models.Database;

namespace Thermostat.ViewModels
{
    /// <summary>
    /// Base class for any time series data, provides a <see cref="LineSeries"/> for benefit of the chart view
    /// </summary>
    public abstract class DataSet : BaseViewModel
    {
        public DataSet(string name, bool enabled = false)
        {
            Name = name;
            Enabled = enabled;
        }

        /// <summary>
        /// Name of this dataset
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Used to populate this data set with values from the database.
        /// Implement in subclasses.
        /// </summary>
        /// <param name="db">database to pull values from</param>
        /// <param name="minDateTime">earliest date/time to include</param>
        /// <param name="maxDateTime">most recent date/time to include</param>
        /// <returns>list of values</returns>
        public abstract IEnumerable<DateModel> QueryData(ThermostatContext db, DateTime minDateTime, DateTime maxDateTime);

        /// <summary>
        /// True if this data set should be displayed
        /// </summary>
        public bool Enabled
        {
            get => _Enabled;
            set
            {
                if (_Enabled != value)
                {
                    _Enabled = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _Enabled;

        public LineSeries Series
        {
            get => _Series;
            set
            {
                if (_Series != value)
                {
                    _Series = value;
                    OnPropertyChanged();
                }
            }
        }
        private LineSeries _Series;
    }
}
