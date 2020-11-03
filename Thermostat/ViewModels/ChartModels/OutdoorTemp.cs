using System;
using System.Collections.Generic;
using System.Linq;
using Thermostat.Models.Database;

namespace Thermostat.ViewModels
{
    /// <summary>
    /// Dataset for outdoor temperature, queries data from the HvacSensorHistory part of the database and converts it to a DateModel.
    /// </summary>
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
