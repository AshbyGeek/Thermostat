using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Thermostat.Models.Database
{
    /// <summary>
    /// Associates a value with a date and time
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T>
    {
        [Key]
        public DateTime DateTime { get; set; }

        public T Data { get; set; }
    }

    /// <summary>
    /// EntityFramework Context for the database.
    /// Defines all data tables in the database.
    /// </summary>
    public class ThermostatContext : DbContext
    {
        public ThermostatContext(DbContextOptions options) : base(options)
        { }

        public DbSet<State<HvacSensors>> HvacSensorHistory { get; set; }

        public DbSet<State<HvacSetPoint>> HvacSetPointHistory { get; set; }

        public DbSet<State<HvacSystem>> HvacSystemStateHistory { get; set; }
    }
}
