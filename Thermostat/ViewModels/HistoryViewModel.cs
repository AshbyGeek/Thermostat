using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using Thermostat.Models;
using Thermostat.Models.Database;

namespace Thermostat.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private Func<ThermostatContext> _DbContextFactoryMethod;

        public HistoryViewModel(Func<ThermostatContext> dbContextFactoryMethod)
        {
            _DbContextFactoryMethod = dbContextFactoryMethod;

            using (var _Db = _DbContextFactoryMethod.Invoke())
            {

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
                using (var _Db = _DbContextFactoryMethod.Invoke())
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
}
