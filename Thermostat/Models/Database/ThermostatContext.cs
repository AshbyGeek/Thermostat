using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Thermostat.Models.Database
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

    /// <summary>
    /// Associates a value with a date and time
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T> where T : notnull
    {
        [Key]
        public DateTime DateTime { get; set; }

        [NotNull]
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

#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
}
