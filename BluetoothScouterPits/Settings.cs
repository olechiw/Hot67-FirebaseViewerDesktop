using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BluetoothScouterPits
{
    public partial class Settings : Form
    {

        public string Username => SettingsValues.Username;
        public string Password => SettingsValues.Password;

        public string ApiKey => SettingsValues.ApiKey;
        public string EventName => SettingsValues.EventName;
        public string DatabaseUrl => SettingsValues.DatabaseUrl;
        public List<string> AverageHeaders => SettingsValues.AverageHeaders;
        public List<string> SumHeaders => SettingsValues.SumHeaders;

        private struct SettingsValues
        {
            public static string Username;
            public static string Password;
            public static string ApiKey;
            public static string EventName;
            public static string DatabaseUrl;
            public static List<string> AverageHeaders;
            public static List<string> SumHeaders;
        }

        private const string ConfigurationFile = "config.cfg";


        public Settings()
        {
            InitializeComponent();
            LoadConfig();
            BindValues();
        }

        private static void LoadConfig()
        {
            try
            {
                using (var reader = new StreamReader(ConfigurationFile))
                {
                    SettingsValues.Username = reader.ReadLine(); // Firebase Username
                    SettingsValues.Password = reader.ReadLine(); // Firebase Password
                    SettingsValues.ApiKey = reader.ReadLine(); // Firebase api key
                    SettingsValues.EventName = reader.ReadLine(); // Name of firebase tag to fetch matches from
                    SettingsValues.DatabaseUrl = reader.ReadLine(); // Website of firebase connection

                    // Headers to create average column
                    var averages = reader.ReadLine();
                    if (averages != null && averages.Split(',').ToList().Any())
                        SettingsValues.AverageHeaders = averages.Split(',').ToList();

                    // Headers to create sum column
                    var sums = reader.ReadLine();
                    if (sums != null && sums.Split(',').ToList().Any())
                        SettingsValues.SumHeaders = sums.Split(',').ToList();
                }
            }
            catch (IOException)
            {
                // Empty or corrupted, clear out the data
                using (var writer = new StreamWriter(ConfigurationFile))
                {
                    writer.WriteLine(""); // Username
                    writer.WriteLine(""); // Password
                    writer.WriteLine(""); // Api Key
                    writer.WriteLine(""); // Event name
                    writer.WriteLine(""); // Firebase Connection
                    writer.WriteLine(""); // Headers to average (csv)
                    writer.WriteLine(""); // Headers to sum (csv)

                    writer.FlushAsync(); // Save
                }
            }
        }

        private void BindValues()
        {
            SettingsValues nonStaticInstance = new SettingsValues();
            usernameTextBox.DataBindings.Add("Username", nonStaticInstance, "Username");
            passwordTextBox.DataBindings.Add("Password", nonStaticInstance, "Password");
            apiKeyTextBox.DataBindings.Add("ApiKey", nonStaticInstance, "ApiKey");
            eventNameTextBox.DataBindings.Add("EventName", nonStaticInstance, "EventName");
            databaseUrlTextBox.DataBindings.Add("DatabaseUrl", nonStaticInstance, "DatabaseUrl");
        }
    }
}
