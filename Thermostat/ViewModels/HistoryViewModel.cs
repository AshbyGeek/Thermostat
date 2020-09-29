using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Thermostat.Models;
using Thermostat.Models.Database;

namespace Thermostat.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private ThermostatContext _Db;

        public HistoryViewModel(ThermostatContext db, ISystemClock systemClock)
        {
            _Db = db;

            var dayConfig = LiveCharts.Configurations.Mappers.Xy<DateModel>()
                                .X(dateModel => (double)dateModel.DateTime.Ticks)
                                .Y(dateModel => dateModel.Value);

            DateFormatter = value => new DateTime((long)value).ToString("g");

            ChartSeries = new SeriesCollection(dayConfig);

            CurrentUsageHistorySettings.PropertyChanged += (s, e) => UpdateAllCharts();


            foreach (var dataSet in CurrentUsageHistorySettings.DataSets)
            {
                dataSet.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(DataSet.Enabled) || e.PropertyName == nameof(DataSet.Name))
                    {
                        UpdateChart((DataSet)sender);
                    }
                };
            }

            UpdateAllCharts();
        }

        private void UpdateAllCharts()
        {
            foreach (var dataSet in CurrentUsageHistorySettings.DataSets)
            {
                UpdateChart(dataSet);
            }
        }

        public UsageHistorySettings CurrentUsageHistorySettings { get; } = new UsageHistorySettings();

        public SeriesCollection ChartSeries { get; }

        public Func<double, string> DateFormatter { get; }

        private void UpdateChart(DataSet dataSet)
        {
            if (dataSet.Enabled)
            {
                var data = dataSet.QueryData(_Db, CurrentUsageHistorySettings.MinDate, CurrentUsageHistorySettings.MaxDate);
                if (data.Count() > 0)
                {
                    if (dataSet.Series == null)
                    {
                        var series = new LineSeries()
                        {
                            Title = dataSet.Name,
                            Values = new ChartValues<DateModel>(data),
                        };
                        ChartSeries.Add(series);
                        dataSet.Series = series;
                    }
                    else
                    {
                        var currentValues = dataSet.Series.Values.Cast<DateModel>().ToList();

                        var toAddFront = data.TakeWhile(x => x.DateTime < currentValues.First().DateTime);
                        var toRemoveFront = currentValues.TakeWhile(x => x.DateTime < data.First().DateTime);

                        var toAddBack = data.Reverse().TakeWhile(x => x.DateTime > currentValues.Last().DateTime).Reverse();
                        var toRemoveBack = currentValues.Reverse<DateModel>().TakeWhile(x => x.DateTime > data.Last().DateTime).Reverse();

                        for (int i = 0; i < toRemoveFront.Count(); i++)
                        {
                            dataSet.Series.Values.RemoveAt(0);
                        }
                        for (int i = 0; i < toRemoveBack.Count(); i++)
                        {
                            dataSet.Series.Values.RemoveAt(dataSet.Series.Values.Count - 1);
                        }

                        foreach (var value in toAddFront.Reverse())
                        {
                            dataSet.Series.Values.Insert(0, value);
                        }
                        foreach (var value in toAddBack)
                        {
                            dataSet.Series.Values.Add(value);
                        }
                    }
                }
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
    }

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

        public IReadOnlyCollection<DataSet> DataSets { get; } = new Collection<DataSet>()
        {
            new IndoorTemp(true),
            new OutdoorTemp(false),
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

        public virtual IEnumerable<DateModel> QueryData(ThermostatContext db, DateTime minDateTime, DateTime maxDateTime) => null;


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

    public class IndoorTemp : DataSet
    {
        public IndoorTemp(bool enabled = false) : base("Inside Temperature", enabled)
        {
        }

        public override IEnumerable<DateModel> QueryData(ThermostatContext db, DateTime minDateTime, DateTime maxDateTime)
        {
            return db.HvacSensorHistory.Where(x => x.DateTime >= minDateTime && x.DateTime < maxDateTime)
                                       .OrderBy(x => x.DateTime)
                                       .AsEnumerable()
                                       .Select(x => new DateModel(x.DateTime, x.Data.IndoorTemp));
        }
    }


    public class OutdoorTemp : DataSet
    {
        public OutdoorTemp(bool enabled = false) : base("Outside Temperature", enabled)
        {
        }

        public override IEnumerable<DateModel> QueryData(ThermostatContext db, DateTime minDateTime, DateTime maxDateTime)
        {
            return db.HvacSensorHistory.Where(x => x.DateTime >= minDateTime && x.DateTime < maxDateTime)
                                       .OrderBy(x => x.DateTime)
                                       .AsEnumerable()
                                       .Select(x => new DateModel(x.DateTime, x.Data.OutdoorTemp));
        }
    }
}
