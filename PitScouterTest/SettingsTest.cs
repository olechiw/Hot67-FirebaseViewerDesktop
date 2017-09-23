using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using BluetoothScouterPits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PitScouterTest
{
    [TestClass]
    public class SettingsTest
    {
        // Test configuration file for an actual database to pull expected data from
        private const string TestConfigurationFile = "UnitTestConfig.cfg";

        private const string IntPropertyName = "intProperty";
        private const string StringPropertyName = "stringProperty";
        private const string DataTablePropertyName = "dataTableProperty";

        // Test config values for assertion that reading/writing to config file works
        private readonly List<string> testConfigValues = new List<string>
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

        // Properties to test reflection methods
        public int intProperty => 1;

        public string stringProperty => "string";
        public DataTable dataTableProperty => new DataTable();

        [TestMethod]
        public void TestIsString()
        {
            var pInt = GetType().GetProperty(IntPropertyName);
            var pString = GetType().GetProperty(StringPropertyName);
            var pTable = GetType().GetProperty(DataTablePropertyName);


            Assert.IsFalse(SettingsForm.IsString(pInt));
            Assert.IsTrue(SettingsForm.IsString(pString));
            Assert.IsFalse(SettingsForm.IsString(pTable));
        }

        [TestMethod]
        public void TestIsDataTable()
        {
            var pInt = GetType().GetProperty(IntPropertyName);
            var pString = GetType().GetProperty(StringPropertyName);
            var pTable = GetType().GetProperty(DataTablePropertyName);

            Assert.IsFalse(SettingsForm.IsDataTable(pInt));
            Assert.IsFalse(SettingsForm.IsDataTable(pString));
            Assert.IsTrue(SettingsForm.IsDataTable(pTable));
        }

        [TestMethod]
        public void TestReadSettings()
        {
            var testConfig = testConfigValues.Aggregate((current, next) => current + next);
            var reader = new StringReader(testConfig);

            var result = new SettingsForm(testing: true).ReadSettings(reader);

            var resultConfig = result.Aggregate((s, s1) => s + s1);
            // Assure were read properly
            Assert.AreEqual(testConfig, resultConfig);
        }

        [TestMethod]
        public void TestWriteSettings()
        {
            var settings = new SettingsForm(testing: true);

            // Load testing configuration
            settings.SetSettingsProperties(testConfigValues);

            var writer = new StringWriter();
            settings.WriteSettings(writer);

            var resultConfig = new List<string>();
            var reader = new StringReader(writer.ToString());
            var line = reader.ReadLine();
            while (line != null)
            {
                resultConfig.Add(line);
                line = reader.ReadLine();
            }

            // Assure were written properly
            Assert.AreEqual(testConfigValues.Aggregate((s, s1) => s + s1),
                resultConfig.Aggregate((s, s1) => s + s1));
        }
    }
}