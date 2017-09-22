using System;
using System.Reflection;
using BluetoothScouterPits;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PitScouterTest
{
    [TestClass]
    public class SettingsTest
    {
        // Test configuration file for an actual database to pull expected data from
        private const string TestConfigurationFile = "UnitTestConfig.cfg";

        // Properties to test reflection methods
        private int intProperty { get; }
        private const string IntPropertyName = "intProperty";
        private string stringProperty { get; }
        private const string StringPropertyName = "stringProperty";
        private DataTable dataTableProperty { get; }
        private const string DataTablePropertyName = "dataTableProperty";

        // Test config values for assertion that reading/writing to config file works
        private readonly List<string> testConfigValues = new List<string>()
        {
            "email",
            "password",
            "apiKey",
            "event",
            "url",
            "1,2,3,4,5",
            "1,2,3,4,5",
            "1,2,3,4,5",
            "1,2,3,4,5"
        };

        [TestMethod]
        public void TestIsString()
        {
            var pInt = GetType().GetProperty(IntPropertyName);
            var pString = GetType().GetProperty(StringPropertyName);
            var pTable = GetType().GetProperty(DataTablePropertyName);


            Assert.IsFalse(Settings.IsString(pInt));
            Assert.IsTrue(Settings.IsString(pString));
            Assert.IsFalse(Settings.IsString(pTable));

        }

        [TestMethod]
        public void TestIsDataTable()
        {
            var pInt = GetType().GetProperty(IntPropertyName);
            var pString = GetType().GetProperty(StringPropertyName);
            var pTable = GetType().GetProperty(DataTablePropertyName);

            Assert.IsFalse(Settings.IsDataTable(pInt));
            Assert.IsFalse(Settings.IsDataTable(pString));
            Assert.IsTrue(Settings.IsDataTable(pTable));
        }

        [TestMethod]
        public void TestReadSettings()
        {
            var testConfig = testConfigValues.Aggregate((current, next) => current + next);
            var reader = new StringReader(testConfig);

            var result = new Settings(true).ReadSettings(reader);

            // Assure were read properly
            Assert.AreEqual(testConfig, result.Aggregate((current, next) => current + next));
        }

        [TestMethod]
        public void TestWriteSettings()
        {
            var settings = new Settings(true);

            // Load testing configuration
            settings.SetSettingsProperties(testConfigValues);

            var writer = new StringWriter();
            settings.WriteSettings(writer);

            // Assure were written properly
            Assert.AreEqual(testConfigValues.Aggregate(((s, s1) => s + s1)), writer.ToString());
        }
    }
}
