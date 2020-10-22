using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Thermostat.HvacAlgorithms;
using Thermostat.Models;

namespace Thermostat_Tests.HvacAlgorithms
{
    [TestClass]
    public class CoolOnlyTest
    {
        [DataTestMethod]
        //       max, min, indoor, outdoor, isCooling
        [DataRow( 60,  0,    85,     100,     true  )]
        [DataRow( 70,  0,    85,     100,     true  )]
        [DataRow( 80,  0,    85,     100,     true  )]
        [DataRow( 85,  0,    85,     100,     false )]
        [DataRow( 90,  0,    85,     100,     false )]
        [DataRow(100,  0,    85,     100,     false )]
        [DataRow( 60,  0,    75,     100,     true  )]
        [DataRow( 70,  0,    75,     100,     true  )]
        [DataRow( 80,  0,    75,     100,     false )]
        [DataRow( 85,  0,    75,     100,     false )]
        [DataRow( 90,  0,    75,     100,     false )]
        [DataRow(100,  0,    75,     100,     false )]
        [DataRow( 60,  0,    85,       0,     true  )]
        [DataRow( 70,  0,    85,       0,     true  )]
        [DataRow( 80,  0,    85,       0,     true  )]
        [DataRow( 85,  0,    85,       0,     false )]
        [DataRow( 90,  0,    85,       0,     false )]
        [DataRow(100,  0,    85,       0,     false )]
        [DataRow( 60,  0,    75,       0,     true  )]
        [DataRow( 70,  0,    75,       0,     true  )]
        [DataRow( 80,  0,    75,       0,     false )]
        [DataRow( 85,  0,    75,       0,     false )]
        [DataRow( 90,  0,    75,       0,     false )]
        [DataRow(100,  0,    75,       0,     false )]
        [DataRow( 60,  0,    85,     500,     true  )]
        [DataRow( 70,  0,    85,     500,     true  )]
        [DataRow( 80,  0,    85,     500,     true  )]
        [DataRow( 85,  0,    85,     500,     false )]
        [DataRow( 90,  0,    85,     500,     false )]
        [DataRow(100,  0,    85,     500,     false )]
        [DataRow( 60,  0,    75,     500,     true  )]
        [DataRow( 70,  0,    75,     500,     true  )]
        [DataRow( 80,  0,    75,     500,     false )]
        [DataRow( 85,  0,    75,     500,     false )]
        [DataRow( 90,  0,    75,     500,     false )]
        [DataRow(100,  0,    75,     500,     false )]
        public void GetNewSystemStateTest(double maxTemp, double minTemp, double indoorTemp, double outdoorTemp, bool cooling)
        {
            var alg = new CoolOnly();

            var setPoint = new HvacSetPoint()
            {
                MaxTemp = maxTemp,
                MinTemp = minTemp,
            };

            HvacSensors sensors = new HvacSensors()
            {
                IndoorTemp = indoorTemp,
                OutdoorTemp = outdoorTemp,
            };

            var newSystemState = alg.GetNewSystemState(setPoint, sensors);

            if (cooling)
            {
                Assert.AreEqual(HvacSystem.NormalCooling, newSystemState);
            }
            else
            {
                Assert.AreEqual(HvacSystem.Off, newSystemState);
            }
        }
    }
}
