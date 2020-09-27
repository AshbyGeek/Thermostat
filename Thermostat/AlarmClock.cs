using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Thermostat.Models
{
    /// <summary>
    /// Allows scheduling of alerts at specific times in the future.
    /// Has a resolution of 1 second.
    /// </summary>
    public class AlarmClock
    {
        private readonly Lookup<DateTime, Action> _Alerts = new Lookup<DateTime, Action>();
        private readonly object _AlertsLock = new object();
        private readonly ISystemClock _Clock;

        public AlarmClock(ISystemClock clock)
        {
            _Clock = clock;
            _Clock.TimeTick += Clock_TimeTick;
        }

        private void Clock_TimeTick(object sender, EventArgs e)
        {
            var minTime = _Clock.Now;
            var maxTime = minTime.AddMilliseconds(ISystemClock.ResolutionMilliseconds);

            List<IGrouping<DateTime, Action>> groupList;
            lock(_AlertsLock)
            {
                groupList = _Alerts.ToList();
            }

            foreach(var group in groupList)
            {
                // Since our timer only has a resolution of 1 second, make sure that we catch all such 
                if (group.Key >= minTime && group.Key < maxTime)
                {
                    foreach(var action in group)
                    {
                        try
                        {
                            action.Invoke();
                        }
                        catch
                        {
                            //TODO: error logging
                        }
                    }
                }
            }
        }

        public DateTime Now => DateTime.Now;


        public void AddAlert(DateTime time, Action callback)
        {
            lock (_AlertsLock)
            {
                _Alerts.AddValue(time, callback);
            }
        }

        public void RemoveAlert(DateTime time, Action callback)
        {
            lock (_AlertsLock)
            {
                _Alerts.RemoveValue(time, callback);
            }
        }
    }

    internal class Lookup<Tkey, TValue> : ILookup<Tkey, TValue>
    {
        #region Accessors
        public int Count => _Dict.Sum(x => x.Value.Count);

        public IEnumerable<TValue> this[Tkey key] => _Dict[key];

        public bool Contains(Tkey key) => _Dict.ContainsKey(key);

        public IEnumerator<IGrouping<Tkey, TValue>> GetEnumerator()
        {
            foreach (var key in _Dict.Keys)
            {
                yield return new Grouping(key, _Dict[key]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion



        #region Mutators
        public void AddValue(Tkey key, TValue value)
        {
            if (!_Dict.ContainsKey(key))
            {
                _Dict.Add(key, new List<TValue>());
            }
            _Dict[key].Add(value);
        }

        public void RemoveValue(Tkey key, TValue value)
        {
            _Dict[key].Remove(value);
            if (_Dict[key].Count == 0)
            {
                _Dict.Remove(key);
            }
        }
        #endregion



        #region Internals
        private readonly Dictionary<Tkey, List<TValue>> _Dict = new Dictionary<Tkey, List<TValue>>();

        internal class Grouping : IGrouping<Tkey, TValue>
        {
            public Grouping(Tkey key, IEnumerable<TValue> values)
            {
                Key = key;
                Values = values;
            }

            public Tkey Key { get; }

            public IEnumerable<TValue> Values { get; }

            public IEnumerator<TValue> GetEnumerator() => Values.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => Values.GetEnumerator();
        }
        #endregion
    }
}
