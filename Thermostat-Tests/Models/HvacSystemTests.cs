using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thermostat.Models;

namespace Thermostat_Tests.Models
{
    [TestClass]
    public class HvacSystemTests
    {
        [DataTestMethod]
        [DataRow(false, false, false, false)]
        [DataRow(false, false, false, true )]
        [DataRow(false, false, true,  false)]
        [DataRow(false, false, true,  true )]
        [DataRow(false, true , false, false)]
        [DataRow(false, true , false, true )]
        [DataRow(false, true , true , false)]
        [DataRow(false, true , true , true )]
        [DataRow(false, false, false, false)]
        [DataRow(true , false, false, true )]
        [DataRow(true , false, true,  false)]
        [DataRow(true , false, true,  true )]
        [DataRow(true , true , false, false)]
        [DataRow(true , true , false, true )]
        [DataRow(true , true , true , false)]
        [DataRow(true , true , true , true )]
        public void Equality_Same(bool isCooling, bool isFanRunning, bool isHeating, bool isAuxHeat)
        {
            var obj1 = new HvacSystem()
            {
                IsCooling = isCooling,
                IsFanRunning = isFanRunning,
                IsHeating = isHeating,
                IsAuxHeat = isAuxHeat,
            };

            var obj2 = new HvacSystem()
            {
                IsCooling = isCooling,
                IsFanRunning = isFanRunning,
                IsHeating = isHeating,
                IsAuxHeat = isAuxHeat,
            };

            Assert.IsTrue(obj1.Equals(obj2));
            Assert.IsTrue(obj1.Equals((object)obj2));
            Assert.IsTrue(obj1 == obj2);
            Assert.IsTrue(obj1 == (object)obj2);
            Assert.IsFalse(obj1 != obj2);
            Assert.IsFalse(obj1 != (object)obj2);
        }

        [TestMethod]
        public void StaticEquality_FirstArgIsNull()
        {
            HvacSystem obj1 = null;
            HvacSystem obj2 = new HvacSystem();

            Assert.IsFalse(obj1 == obj2);
            Assert.IsTrue(obj1 != obj2);
            Assert.IsFalse(obj1 == (object)obj2);
            Assert.IsTrue(obj1 != (object)obj2);
        }

        [TestMethod]
        public void StaticEquality_SecondArgIsNull()
        {
            HvacSystem obj1 = new HvacSystem();
            HvacSystem obj2 = null;

            Assert.IsFalse(obj1 == obj2);
            Assert.IsTrue(obj1 != obj2);
            Assert.IsFalse(obj1 == (object)obj2);
            Assert.IsTrue(obj1 != (object)obj2);
        }


        [TestMethod]
        public void StaticEquality_BothArgsAreNull()
        {
            HvacSystem obj1 = null;
            HvacSystem obj2 = null;

            Assert.IsTrue(obj1 == obj2);
            Assert.IsFalse(obj1 != obj2);
            Assert.IsTrue(obj1 == (object)obj2);
            Assert.IsFalse(obj1 != (object)obj2);
        }



        [TestMethod]
        [DataRow(false, false, false, false)]
        [DataRow(false, false, false, true )]
        [DataRow(false, false, true,  false)]
        [DataRow(false, false, true,  true )]
        [DataRow(false, true , false, false)]
        [DataRow(false, true , false, true )]
        [DataRow(false, true , true , false)]
        [DataRow(false, true , true , true )]
        [DataRow(false, false, false, false)]
        [DataRow(true , false, false, true )]
        [DataRow(true , false, true,  false)]
        [DataRow(true , false, true,  true )]
        [DataRow(true , true , false, false)]
        [DataRow(true , true , false, true )]
        [DataRow(true , true , true , false)]
        [DataRow(true , true , true , true )]
        public void Equality_Different(bool isCooling,  bool isFanRunning,  bool isHeating,  bool isAuxHeat)
        {
            var obj1 = new HvacSystem()
            {
                IsCooling = isCooling,
                IsFanRunning = isFanRunning,
                IsHeating = isHeating,
                IsAuxHeat = isAuxHeat,
            };

            var obj2 = new HvacSystem()
            {
                IsCooling = !isCooling,
                IsFanRunning = !isFanRunning,
                IsHeating = !isHeating,
                IsAuxHeat = !isAuxHeat,
            };

            AssertNotEqual(obj1, obj2);

            obj2 = new HvacSystem()
            {
                IsCooling = !isCooling,
                IsFanRunning = isFanRunning,
                IsHeating = isHeating,
                IsAuxHeat = isAuxHeat,
            };
            AssertNotEqual(obj1, obj2);

            obj2 = new HvacSystem()
            {
                IsCooling = isCooling,
                IsFanRunning = !isFanRunning,
                IsHeating = isHeating,
                IsAuxHeat = isAuxHeat,
            };
            AssertNotEqual(obj1, obj2);

            obj2 = new HvacSystem()
            {
                IsCooling = isCooling,
                IsFanRunning = isFanRunning,
                IsHeating = !isHeating,
                IsAuxHeat = isAuxHeat,
            };
            AssertNotEqual(obj1, obj2);

            obj2 = new HvacSystem()
            {
                IsCooling = isCooling,
                IsFanRunning = isFanRunning,
                IsHeating = isHeating,
                IsAuxHeat = !isAuxHeat,
            };
            AssertNotEqual(obj1, obj2);
        }

        private void AssertNotEqual(HvacSystem obj1, HvacSystem obj2)
        {
            Assert.IsFalse(obj1.Equals(obj2));
            Assert.IsFalse(obj1.Equals((object)obj2));
            Assert.IsFalse(obj1 == obj2);
            Assert.IsFalse(obj1 == (object)obj2);
            Assert.IsTrue(obj1 != obj2);
            Assert.IsTrue(obj1 != (object)obj2);
        }

        [TestMethod]
        public void GetHashCode_isUnique()
        {
            List<HvacSystem> systemItems = new List<HvacSystem>();
            for (int i = 0; i < 0b1111; i++)
            {
                systemItems.Add(new HvacSystem()
                {
                    IsCooling = (i & 0b0001) > 0,
                    IsFanRunning = (i & 0b0010) > 0,
                    IsHeating = (i & 0b0100) > 0,
                    IsAuxHeat = (i & 0b1000) > 0,
                });
            }

            var uniqueHashCodes = systemItems.Select(x => x.GetHashCode()).Distinct();

            Assert.AreEqual(systemItems.Count, uniqueHashCodes.Count());
        }


        [DataTestMethod]
        [DataRow(false, false, false, false)]
        [DataRow(false, false, false, true)]
        [DataRow(false, false, true, false)]
        [DataRow(false, false, true, true)]
        [DataRow(false, true, false, false)]
        [DataRow(false, true, false, true)]
        [DataRow(false, true, true, false)]
        [DataRow(false, true, true, true)]
        [DataRow(false, false, false, false)]
        [DataRow(true, false, false, true)]
        [DataRow(true, false, true, false)]
        [DataRow(true, false, true, true)]
        [DataRow(true, true, false, false)]
        [DataRow(true, true, false, true)]
        [DataRow(true, true, true, false)]
        [DataRow(true, true, true, true)]
        public void GetHashCodes_SameForIdentical(bool isCooling, bool isFanRunning, bool isHeating, bool isAuxHeat)
        {
            var obj1 = new HvacSystem()
            {
                IsCooling = isCooling,
                IsFanRunning = isFanRunning,
                IsHeating = isHeating,
                IsAuxHeat = isAuxHeat,
            };

            var obj2 = new HvacSystem()
            {
                IsCooling = isCooling,
                IsFanRunning = isFanRunning,
                IsHeating = isHeating,
                IsAuxHeat = isAuxHeat,
            };

            Assert.AreEqual(obj1.GetHashCode(), obj2.GetHashCode());
        }
    }
}
