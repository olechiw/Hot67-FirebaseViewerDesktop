using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BluetoothScouterPits;
using BluetoothScouterPits.Forms;
using BluetoothScouterPits.Interfaces;
using BluetoothScouterPits.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace PitScouterTest
{
    [TestClass]
    public class MainTest
    {
        [TestMethod]
        public async Task TestGetCalculatedDataTable()
        {
            var testSettings = new TestDataSettings();
            var testForm = new MainForm(testSettings, testSettings);
            
            var dataTable = await testForm.GetCalculatedDataTable();

            // 1 team, one row
            Assert.AreEqual(dataTable.Rows.Count, 1, "More/Less Than One Row Found!");

            #region Column Names Testing

            // Not really neccesary atm.

            #endregion

            #region Calculated Columns Testing

            // First test 
            var resultantRow = dataTable.Rows[0];

            Assert.AreEqual( // Average
                resultantRow[MainForm.Average + TestDataSettings.TestValueKey],
                "2", "Average Inaccurate!");

            Assert.AreEqual( // Sum
                resultantRow[MainForm.Sum + TestDataSettings.TestValueKey],
                "6", "Sum Inaccurate!");

            Assert.AreEqual( // Minimum
                resultantRow[MainForm.Minimum + TestDataSettings.TestValueKey],
                "1", "Minimum Inaccurate!");

            Assert.AreEqual( // Maximum
                resultantRow[MainForm.Maximum + TestDataSettings.TestValueKey],
                "3", "Maximum Inaccurate");

            #endregion
        }

        /* This is a test data source, 
         * It contains:
         * 1 team: team "1"
         * 3 Matches, "1", "2", and "3"
         * 1 subvalue, "Value1"
         * The results should be:
         * "Average of Value1" : "2"
         *  "Sum of Value1" : "6"
         *  "Minimum of Value1" : "1"
         *  "Maximum of Value1" : "3"
         *  
         *  
         *  --------
         *  11
         *  {
         *      "Team Number" : "1",
         *      "Match Number" : "1",
         *      "Value1" : "2"
         *  }
         *  21
         *  {
         *      "Team Number" : "1",
         *      "Match Number" : "2",
         *      "Value1" : "1"
         *  }
         *  31
         *  {
         *      "Team Number" : "1",
         *      "Match Number" : "3",
         *      "Value1" : "3"
         *  }
         *  --------
         */

        public class TestDataSettings : SettingsForm, IDataSourceObject
        {
            private const string TestConfigurationFile = "testconfig.cfg";
            public const string TestValueKey = "Value1";

            public TestDataSettings() :
                base(TestConfigurationFile) // Allow it to not be "testing", on purpose, because that way it will setup the columns
            {
                SetSettingsProperties(ReadSettings(new StreamReader(TestConfigurationFile)));

                // Manually set Average Columns. Only thing that will be actually used from above 
                // is the eventname
                AverageColumns.Rows.Clear();
                AverageColumns.Rows.Add(TestValueKey);
                SumColumns.Rows.Clear();
                SumColumns.Rows.Add(TestValueKey);
                MinimumColumns.Rows.Clear();
                MinimumColumns.Rows.Add(TestValueKey);
                MaximumColumns.Rows.Clear();
                MaximumColumns.Rows.Add(TestValueKey);
            }

            // The function that the 
            public async Task<List<MatchObject>> Get()
            {
                return await Get(EventName);
            }

            // Implements interface. Doesn't actually get data, so its not async truly
#pragma warning disable 1998
            public async Task<List<MatchObject>> Get(string key)
#pragma warning restore 1998
            {
                // Three mock matches with the two needed values plus a given TestValueKey for doing calculations on
                var subMatch1 = new JObject
                {
                    {MatchObject.JsonMatchNumberTag, "1"},
                    {MatchObject.JsonTeamNumberTag, "1"},
                    {TestValueKey, "2"}
                };
                var subMatch2 = new JObject
                {
                    {MatchObject.JsonMatchNumberTag, "2"},
                    {MatchObject.JsonTeamNumberTag, "1"},
                    {TestValueKey, "1"}
                };
                var subMatch3 = new JObject
                {
                    {MatchObject.JsonMatchNumberTag, "3"},
                    {MatchObject.JsonTeamNumberTag, "1"},
                    {TestValueKey, "3"}
                };

                return new[]
                {
                    new MatchObject(subMatch1),
                    new MatchObject(subMatch2),
                    new MatchObject(subMatch3)
                }.ToList();
            }

            public async Task<List<MatchObject>> Get(int top)
            {
                return (await Get(EventName)).Take(top).ToList();
            }


            public void SetSettings(IFirebaseSettingsObject firebaseSettings)
            {
                throw new NotImplementedException("Method should never have been called!");
            }

            public void RefreshCredentials()
            {
                // Do nothing, this is a mock datasource with no credentials
            }
        }
    }
}