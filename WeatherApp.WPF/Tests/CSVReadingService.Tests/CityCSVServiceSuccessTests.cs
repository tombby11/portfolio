using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSVReadingService.Tests
{
    /// <summary>
    /// Summary description for CityCSVServiceSuccessTests
    /// </summary>
    [TestClass]
    public class CityCSVServiceSuccessTests
    {
        public CityCSVServiceSuccessTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

   
        [TestMethod]
        public void Constructor_ShouldNotBeNull()
        {
            ICsvService SUT = new CityCsvService();
            Assert.IsNotNull(SUT);
        }

        [TestMethod]
        public void Constructor_ShouldSetCities()
        {
            CityCsvService SUT = new CityCsvService();
            Assert.IsTrue(SUT.Cities.Count>0);
        }

        [TestMethod]
        public void GetCity_ExistingName_ShouldReturnCity()
        {
            CityCsvService SUT = new CityCsvService();
            string key = "Stockholm";

            City found = SUT.GetCity(key);
            Assert.AreEqual(found.Name,key);
        }

        [TestMethod]
        public void GetCity_NotExistingName_ShouldReturnNull()
        {
            CityCsvService SUT = new CityCsvService();
            string key = "Stockholm2";

            City found = SUT.GetCity(key);
            Assert.IsNull(found);
        }
    }
}
