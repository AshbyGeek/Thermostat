﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thermostat.Models.Database;

namespace Thermostat.Migrations
{
    [DbContext(typeof(ThermostatContext))]
    [Migration("20200928021249_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("Thermostat.Models.Database.State<Thermostat.Models.HvacSensors>", b =>
                {
                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("DateTime");

                    b.ToTable("HvacSensorHistory");
                });

            modelBuilder.Entity("Thermostat.Models.Database.State<Thermostat.Models.HvacSetPoint>", b =>
                {
                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("DateTime");

                    b.ToTable("HvacSetPointHistory");
                });

            modelBuilder.Entity("Thermostat.Models.Database.State<Thermostat.Models.HvacSystem>", b =>
                {
                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("DateTime");

                    b.ToTable("HvacSystemStateHistory");
                });

            modelBuilder.Entity("Thermostat.Models.Database.State<Thermostat.Models.HvacSensors>", b =>
                {
                    b.OwnsOne("Thermostat.Models.HvacSensors", "Data", b1 =>
                        {
                            b1.Property<DateTime>("State<HvacSensors>DateTime")
                                .HasColumnType("TEXT");

                            b1.Property<double>("IndoorTemp")
                                .HasColumnType("REAL");

                            b1.Property<double>("OutdoorTemp")
                                .HasColumnType("REAL");

                            b1.HasKey("State<HvacSensors>DateTime");

                            b1.ToTable("HvacSensorHistory");

                            b1.WithOwner()
                                .HasForeignKey("State<HvacSensors>DateTime");
                        });
                });

            modelBuilder.Entity("Thermostat.Models.Database.State<Thermostat.Models.HvacSetPoint>", b =>
                {
                    b.OwnsOne("Thermostat.Models.HvacSetPoint", "Data", b1 =>
                        {
                            b1.Property<DateTime>("State<HvacSetPoint>DateTime")
                                .HasColumnType("TEXT");

                            b1.Property<double>("MaxTemp")
                                .HasColumnType("REAL");

                            b1.Property<double>("MinTemp")
                                .HasColumnType("REAL");

                            b1.HasKey("State<HvacSetPoint>DateTime");

                            b1.ToTable("HvacSetPointHistory");

                            b1.WithOwner()
                                .HasForeignKey("State<HvacSetPoint>DateTime");
                        });
                });

            modelBuilder.Entity("Thermostat.Models.Database.State<Thermostat.Models.HvacSystem>", b =>
                {
                    b.OwnsOne("Thermostat.Models.HvacSystem", "Data", b1 =>
                        {
                            b1.Property<DateTime>("State<HvacSystem>DateTime")
                                .HasColumnType("TEXT");

                            b1.Property<bool>("IsAuxHeat")
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("IsCooling")
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("IsFanRunning")
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("IsHeating")
                                .HasColumnType("INTEGER");

                            b1.HasKey("State<HvacSystem>DateTime");

                            b1.ToTable("HvacSystemStateHistory");

                            b1.WithOwner()
                                .HasForeignKey("State<HvacSystem>DateTime");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
