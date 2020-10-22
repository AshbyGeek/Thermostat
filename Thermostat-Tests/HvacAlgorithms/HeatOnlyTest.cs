using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Thermostat.HvacAlgorithms;
using Thermostat.Models;

namespace Thermostat_Tests.HvacAlgorithms
{
    [TestClass]
    public class HeatOnlyTest
    {
        [DataTestMethod]
        //      indoor, outdoor,  max,  min,   heat
        [DataRow( 85,      99,     50,   45,  false , DisplayName = "Temp 1 - cool set way low")]
        [DataRow( 85,      99,     80,   55,  false , DisplayName = "Temp 1 - cool set a little low")]
        [DataRow( 85,      99,     85,   55,  false , DisplayName = "Temp 1 - cool set on temp")]
        [DataRow( 85,      99,     86,   55,  false , DisplayName = "Temp 1 - cool a little high")]
        [DataRow( 85,      99,     99,   55,  false , DisplayName = "Temp 1 - cool a lot high")]
        [DataRow( 65,      65,     40,   30,  false , DisplayName = "Temp 2 - cool set way low")]
        [DataRow( 65,      65,     64,   55,  false , DisplayName = "Temp 2 - cool set a little low")]
        [DataRow( 65,      65,     65,   55,  false , DisplayName = "Temp 2 - cool set on temp")]
        [DataRow( 65,      65,     67,   55,  false , DisplayName = "Temp 2 - cool a little high")]
        [DataRow( 65,      65,     99,   55,  false , DisplayName = "Temp 2 - cool a lot high")]
        [DataRow( 55,      50,     99,   30,  false , DisplayName = "Temp 3 - heat set way low")]
        [DataRow( 55,      50,     99,   54,  false , DisplayName = "Temp 3 - heat set a little low")]
        [DataRow( 55,      50,     99,   55,  false , DisplayName = "Temp 3 - heat set on temp")]
        [DataRow( 55,      50,     99,   56,  true  , DisplayName = "Temp 3 - heat a little high")]
        [DataRow( 55,      50,     99,   80,  true  , DisplayName = "Temp 3 - heat a lot high")]
        public void GetNewSystemState(double indoorTemp, double outdoorTemp, double maxTemp, double minTemp, bool heat)
        {
            var alg = new HeatOnly();

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

            if (heat)
            {
                Assert.AreEqual(HvacSystem.NormalHeating, newSystemState);
            }
            else
            {
                Assert.AreEqual(HvacSystem.Off, newSystemState);
            }
        }
    }
}
