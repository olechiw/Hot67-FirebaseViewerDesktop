using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BluetoothScouterPits
{
    public partial class Settings : Form
    {
        private const string ConfigurationFile = "config.cfg";

        public const string ColumnsColumnName = "Column";
        // Name of datatable column for the calculated columns

        // A private class with public members of the values, to use for databinding
        private readonly SettingsValues settingsValuesInstance = new SettingsValues();


        public Settings()
        {
            InitializeComponent();
            LoadConfig();
            BindValues();
            FetchMatches();
        }

        // Load the settings from the configuration file
        private void LoadConfig()
        {
            if (File.Exists(ConfigurationFile))
            {
                using (var reader = new StreamReader(ConfigurationFile))
                {
                    var line = reader.ReadLine();
                    settingsValuesInstance.Username =
                        line ?? ""; // Firebase Username

                    line = reader.ReadLine();
                    settingsValuesInstance.Password =
                        line ?? ""; // Firebase Password

                    line = reader.ReadLine();
                    settingsValuesInstance.ApiKey =
                        line ?? ""; // Firebase api key

                    line = reader.ReadLine();
                    settingsValuesInstance.EventName =
                        line ?? ""; // Name of firebase tag to fetch matches from

                    line = reader.ReadLine();
                    settingsValuesInstance.DatabaseUrl =
                        line ?? ""; // Website of firebase connection

                    // Headers to create average column
                    var averages = reader.ReadLine();

                    settingsValuesInstance.AverageHeaders = new DataTable();
                    settingsValuesInstance.AverageHeaders.Columns.Add("Column");

                    if (averages != null && averages.Split(',').ToList().Any())
                        foreach (var v in averages.Split(',').ToList())
                            settingsValuesInstance.AverageHeaders.Rows.Add(v);

                    // Headers to create sum column
                    var sums = reader.ReadLine();

                    settingsValuesInstance.SumHeaders = new DataTable();
                    settingsValuesInstance.SumHeaders.Columns.Add(ColumnsColumnName);

                    if (sums != null && sums.Split(',').ToList().Any())
                        foreach (var v in sums.Split(',').ToList())
                            settingsValuesInstance.SumHeaders.Rows.Add(v);

                    // Headers to create minimum column
                    var minimums = reader.ReadLine();

                    settingsValuesInstance.MinimumHeaders = new DataTable();
                    settingsValuesInstance.MinimumHeaders.Columns.Add(ColumnsColumnName);

                    if (minimums != null && minimums.Split(',').ToList().Any())
                        foreach (var v in minimums.Split(',').ToList())
                            settingsValuesInstance.MinimumHeaders.Rows.Add(v);

                    // Headers to create maximum column
                    var maximums = reader.ReadLine();

                    settingsValuesInstance.MaximumHeaders = new DataTable();
                    settingsValuesInstance.MaximumHeaders.Columns.Add(ColumnsColumnName);

                    if (maximums != null && maximums.Split(',').ToList().Any())
                        foreach (var v in maximums.Split(',').ToList())
                            settingsValuesInstance.MaximumHeaders.Rows.Add(v);
                }
            }
            else
            {
                settingsValuesInstance.Username = "";
                settingsValuesInstance.Password = "";
                settingsValuesInstance.ApiKey = "";
                settingsValuesInstance.EventName = "";
                settingsValuesInstance.DatabaseUrl = "";
                settingsValuesInstance.AverageHeaders = new DataTable();
                settingsValuesInstance.AverageHeaders.Columns.Add("Column");
                settingsValuesInstance.SumHeaders = new DataTable();
                settingsValuesInstance.SumHeaders.Columns.Add("Column");
                settingsValuesInstance.MinimumHeaders = new DataTable();
                settingsValuesInstance.MinimumHeaders.Columns.Add("Column");
                settingsValuesInstance.MaximumHeaders = new DataTable();
                settingsValuesInstance.MaximumHeaders.Columns.Add("Column");
            }
        }

        // Bind all of the settings values (besides the headers) to respective fields
        private void BindValues()
        {
            try
            {
                usernameTextBox.DataBindings.Clear();
                usernameTextBox.DataBindings.Add(
                    "Text",
                    settingsValuesInstance,
                    "Username",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);
                passwordTextBox.DataBindings.Add(
                    "Text",
                    settingsValuesInstance,
                    "Password",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);
                apiKeyTextBox.DataBindings.Add(
                    "Text",
                    settingsValuesInstance,
                    "ApiKey",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);
                eventNameTextBox.DataBindings.Add(
                    "Text",
                    settingsValuesInstance,
                    "EventName",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);
                databaseUrlTextBox.DataBindings.Add(
                    "Text",
                    settingsValuesInstance,
                    "DatabaseUrl",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);

                averageDataGridView.DataSource = settingsValuesInstance.AverageHeaders;
                sumDataGridView.DataSource = settingsValuesInstance.SumHeaders;
                minimumDataGridView.DataSource = settingsValuesInstance.MinimumHeaders;
                maximumDataGridView.DataSource = settingsValuesInstance.MaximumHeaders;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // Prevent disposal of object on close, 
        // so that the settings values all persist 
        // and dont have to be reloaded from file each time
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            WriteSettingsToFile();
            e.Cancel = true;
        }

        // Pull down a copy of the database, and display the headers of the first match
        private void OnFetchButtonClick(object sender, EventArgs e)
        {
            FetchMatches();
        }

        private async void FetchMatches()
        {
            retreivedColumnsDataGridView.Rows.Clear();
            var results = await new DataSource(this).Get();

            if (!results.Any()) return;

            var matchObj = results.First();
            foreach (var column in matchObj.GetAllKeys())
                retreivedColumnsDataGridView.Rows.Add(column);
        }

        // Called in dispose method in Settings.Designer.cs, so effectively Dispose()
        private void WriteSettingsToFile()
        {
            using (var writer = new StreamWriter(ConfigurationFile))
            {
                // Write to the file in the order of which they properties occur. 
                // Using reflection for easier expandability, i really don't know why
                #region old
                /*
                foreach (var property in settingsValuesInstance.GetType().GetProperties())
                {
                    // Not a list of headers
                    if (property.PropertyType != typeof(DataTable))
                    {
                        writer.WriteLine(property.GetValue(settingsValuesInstance).ToString());
                    }

                    // List of headers, so turn into csv for loading later on
                    else
                    {
                        var value = "";
                        Console.WriteLine(property.Name);
                        var list = (DataTable) property.GetValue(settingsValuesInstance);

                        for (var i = 0; i < list.Rows.Count; ++i)
                        {
                            if (string.IsNullOrWhiteSpace(value)) continue;

                            value += list.Rows[i][ColumnsColumnName];
                            if (i + 1 < list.Rows.Count)
                                value += ","; // CSV
                        }
                        Console.WriteLine(value);
                        writer.WriteLine(value);
                    }
                }
                */
                #endregion

                writer.WriteLine(settingsValuesInstance.Username);
                writer.WriteLine(settingsValuesInstance.Password);
                writer.WriteLine(settingsValuesInstance.ApiKey);
                writer.WriteLine(settingsValuesInstance.EventName);
                writer.WriteLine(settingsValuesInstance.DatabaseUrl);

                var output = "";
                for (var i = 0; i < AverageHeaders.Rows.Count; ++i)
                {
                    output += AverageHeaders.Rows[i][ColumnsColumnName].ToString();
                    if (i + 1 < AverageHeaders.Rows.Count) output += ",";
                }
                writer.WriteLine(output);

                output = "";
                for (var i = 0; i < SumHeaders.Rows.Count; ++i)
                {
                    output += SumHeaders.Rows[i][ColumnsColumnName].ToString();
                    if (i + 1 < SumHeaders.Rows.Count) output += ",";
                }

                output = "";
                for (var i = 0; i < MinimumHeaders.Rows.Count; ++i)
                {
                    output += MinimumHeaders.Rows[i][ColumnsColumnName].ToString();
                    if (i + 1 < MinimumHeaders.Rows.Count) output += ",";
                }

                output = "";
                for (var i = 0; i < MaximumHeaders.Rows.Count; ++i)
                {
                    output += MaximumHeaders.Rows[i][ColumnsColumnName].ToString();
                    if (i + 1 < MaximumHeaders.Rows.Count) output += ",";
                }

                writer.Flush();
            }
        }

        // The settings model with all of the given values to keep track of
        private class SettingsValues : INotifyPropertyChanged
        {
            private string apiKey;

            private string databaseUrl;
            private string eventName;
            private string password;
            private string username;

            public string Username
            {
                get => username;
                set
                {
                    username = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("Username"));
                }
            }

            public string Password
            {
                get => password;
                set
                {
                    password = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("Password"));
                }
            }

            public string ApiKey
            {
                get => apiKey;
                set
                {
                    apiKey = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("ApiKey"));
                }
            }

            public string EventName
            {
                get => eventName;
                set
                {
                    eventName = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("EventName"));
                }
            }

            public string DatabaseUrl
            {
                get => databaseUrl;
                set
                {
                    databaseUrl = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("DatabaseUrl"));
                }
            }

            public DataTable AverageHeaders { get; set; }
            public DataTable SumHeaders { get; set; }
            public DataTable MinimumHeaders { get; set; }
            public DataTable MaximumHeaders { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            private void InvokePropertyChanged(PropertyChangedEventArgs e)
            {
                PropertyChanged?.Invoke(this, e);
            }
        }

        #region Public Settings Accessors

        public string Username => settingsValuesInstance.Username;
        public string Password => settingsValuesInstance.Password;
        public string ApiKey => settingsValuesInstance.ApiKey;
        public string EventName => settingsValuesInstance.EventName;
        public string DatabaseUrl => settingsValuesInstance.DatabaseUrl;
        public DataTable AverageHeaders => settingsValuesInstance.AverageHeaders;
        public DataTable SumHeaders => settingsValuesInstance.SumHeaders;
        public DataTable MinimumHeaders => settingsValuesInstance.MinimumHeaders;
        public DataTable MaximumHeaders => settingsValuesInstance.MaximumHeaders;

        #endregion
    }
}