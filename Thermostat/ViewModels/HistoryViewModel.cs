using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Thermostat.Models.Database;

namespace Thermostat.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private ThermostatContext _Db;

        public HistoryViewModel(ThermostatContext db)
        {
            _Db = db;
        }

        public UsageHistorySettings CurrentUsageHistorySettings { get; } = new UsageHistorySettings();
    }

    public class DateModel
    {
        public DateTime DateTime { get; set; }

        public double Value { get; set; }
    }



    public class UsageHistorySettings : BaseViewModel
    {
        public UsageHistorySettings()
        {
            var dayConfig = LiveCharts.Configurations.Mappers.Xy<DateModel>()
                                .X(dateModel => (double)dateModel.DateTime.Ticks / TimeSpan.FromSeconds(1).Ticks)
                                .Y(dateModel => dateModel.Value);
            ChartSeries = new SeriesCollection(dayConfig);


            foreach (var dataset in DataSets)
            {
                dataset.PropertyChanged += Dataset_PropertyChanged;
                UpdateChart(dataset);
            }
        }

        private void Dataset_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DataSet.Name) || e.PropertyName == nameof(DataSet.Enabled))
            {
                var dataSet = (DataSet)sender;
                UpdateChart(dataSet);
            }
        }

        private void UpdateChart(DataSet dataSet)
        {
            if (dataSet.Enabled)
            {
                var series = new LineSeries()
                {
                    Title = dataSet.Name,
                    Values = new ChartValues<DateModel>
                    {
                        new DateModel
                        {
                            DateTime = DateTime.Now,
                            Value = 72,
                        },
                        new DateModel
                        {
                            DateTime = DateTime.Now.AddSeconds(1),
                            Value = 72,
                        },
                        new DateModel
                        {
                            DateTime = DateTime.Now.AddSeconds(2),
                            Value = 72,
                        },
                        new DateModel
                        {
                            DateTime = DateTime.Now.AddSeconds(3),
                            Value = 73,
                        },
                        new DateModel
                        {
                            DateTime = DateTime.Now.AddSeconds(4),
                            Value = 73,
                        }
                    }
                    //TODO: get real values
                };
                dataSet.Series = series;
                ChartSeries.Add(series);
            }
            else
            {
                var series = dataSet.Series;
                if (series != null)
                {
                    ChartSeries.Remove(series);
                }
                dataSet.Series = null;
            }
        }

        public SeriesCollection ChartSeries { get; }

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
        private DateTime _MinDate;

        public DateTime MaxDate
        {
            get => _MaxDate;
            set
            {
                if (_MaxDate != value)
                {
                    _MinDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _MaxDate;

        public IReadOnlyCollection<DataSet> DataSets { get; } = new Collection<DataSet>()
        {
            new DataSet("Inside Temperature", true),
            new DataSet("Outside Temperature", false),
        };
    }

    public class DataSet : BaseViewModel
    {
        public DataSet(string name, bool enabled = false)
        {
            Name = name;
            Enabled = enabled;
        }

        public string Name { get; }

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
