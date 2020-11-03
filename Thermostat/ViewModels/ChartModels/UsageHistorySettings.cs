using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Thermostat.ViewModels
{
    /// <summary>
    /// Describes the filter settings (min and max date) currently in use by the HistoryViewModel
    /// </summary>
    public class UsageHistorySettings : BaseViewModel
    {
        public DateTime MinDate
        {
            get => _MinDate;
            set
            {
                if (_MinDate != value)
                {
                    _MinDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _MinDate = DateTime.Today;

        public DateTime MaxDate
        {
            get => _MaxDate;
            set
            {
                if (_MaxDate != value)
                {
                    _MaxDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _MaxDate = DateTime.Now;

        /// <summary>
        /// Lists all available data sets. Currently Indoor temperatures and outdoor temperatures, perhaps eventually also humidity.
        /// </summary>
        public IReadOnlyCollection<DataSet> DataSets { get; } = new Collection<DataSet>()
        {
            new IndoorTemp(true),
            new OutdoorTemp(false),
        };
    }
}
