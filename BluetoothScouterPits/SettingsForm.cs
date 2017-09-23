using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace BluetoothScouterPits
{
    public partial class SettingsForm : Form, IFirebaseSettingsObject
    {
        public const string ColumnsColumnName = "Column";

        private readonly string configurationFile;
        // Name of datatable column for the calculated columns

        // A private class with public members of the values, to use for databinding
        protected readonly SettingsValues SettingsValuesInstance = new SettingsValues();


        public SettingsForm(string configFile = "config.cfg", bool testing = false)
        {
            configurationFile = configFile;

            InitializeComponent();
            if (testing) return;

            SetSettingsProperties(File.Exists(configurationFile)
                ? ReadSettings(new StreamReader(configurationFile))
                : null);

            BindValues();
            FetchMatches();
        }

        // Load the settings from the configuration file
        public List<string> ReadSettings(TextReader reader)
        {
            var config = new List<string>();
            var line = reader.ReadLine();
            while (line != null)
            {
                config.Add(line);
                line = reader.ReadLine();
            }

            reader.Close();

            return config;
        }

        public static bool IsString(PropertyInfo p)
        {
            return p.PropertyType.Name == "String";
        }

        public static bool IsDataTable(PropertyInfo p)
        {
            return p.PropertyType.Name == "DataTable";
        }

        public void SetSettingsProperties(IReadOnlyList<string> values)
        {
            if (values != null)
            {
                var settingsProperties = SettingsValuesInstance.GetType().GetProperties();

                var i = 0;
                for (; i < values.Count; ++i)
                    if (IsDataTable(settingsProperties[i]))
                    {
                        var table = new DataTable();
                        table.Columns.Add(ColumnsColumnName);
                        foreach (var v in values[i].Split(','))
                            table.Rows.Add(v);
                        settingsProperties[i].SetValue(SettingsValuesInstance, table);
                    }
                    else if (IsString(settingsProperties[i]))
                    {
                        settingsProperties[i].SetValue(SettingsValuesInstance, values[i]);
                    }
                // Account for extra values not stored, only continue if some leftover
                if (i + 1 >= settingsProperties.Length) return;

                for (; i < settingsProperties.Length; ++i)
                {
                    var property = settingsProperties[i];
                    if (IsString(property))
                    {
                        property.SetValue(SettingsValuesInstance, "");
                    }
                    else if (IsDataTable(property))
                    {
                        var table = new DataTable();
                        table.Columns.Add(ColumnsColumnName);
                        property.SetValue(SettingsValuesInstance, table);
                    }
                }
            }
            else
            {
                foreach (var property in SettingsValuesInstance.GetType().GetProperties())
                    if (IsString(property))
                    {
                        property.SetValue(SettingsValuesInstance, "");
                    }
                    else if (IsDataTable(property))
                    {
                        var table = new DataTable();
                        table.Columns.Add(ColumnsColumnName);
                        property.SetValue(SettingsValuesInstance, table);
                    }
            }
        }


        // Bind all of the settings values (besides the headers) to respective fields
        public void BindValues()
        {
            try
            {
                usernameTextBox.DataBindings.Clear();
                usernameTextBox.DataBindings.Add(
                    "Text",
                    SettingsValuesInstance,
                    "Username",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);
                passwordTextBox.DataBindings.Add(
                    "Text",
                    SettingsValuesInstance,
                    "Password",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);
                apiKeyTextBox.DataBindings.Add(
                    "Text",
                    SettingsValuesInstance,
                    "ApiKey",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);
                eventNameTextBox.DataBindings.Add(
                    "Text",
                    SettingsValuesInstance,
                    "EventName",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);
                databaseUrlTextBox.DataBindings.Add(
                    "Text",
                    SettingsValuesInstance,
                    "DatabaseUrl",
                    false,
                    DataSourceUpdateMode.OnPropertyChanged);
                averageDataGridView.DataSource = SettingsValuesInstance.AverageHeaders;
                sumDataGridView.DataSource = SettingsValuesInstance.SumHeaders;
                minimumDataGridView.DataSource = SettingsValuesInstance.MinimumHeaders;
                maximumDataGridView.DataSource = SettingsValuesInstance.MaximumHeaders;
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
            WriteSettings(new StreamWriter(configurationFile));
            e.Cancel = true;
        }

        // Pull down a copy of the database, and display the headers of the first match
        private void OnFetchButtonClick(object sender, EventArgs e)
        {
            FetchMatches();
        }

        public async void FetchMatches()
        {
            retreivedColumnsDataGridView.Rows.Clear();

            try
            {
                var results = await new DataSource(this).Get();

                if (!results.Any()) return;
                var matchObj = results.First();
                foreach (var column in matchObj.GetAllKeys())
                    retreivedColumnsDataGridView.Rows.Add(column);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // Called in dispose method in SettingsForm.Designer.cs, so effectively Dispose()
        public void WriteSettings(TextWriter outputStream)
        {
            foreach (var property in SettingsValuesInstance.GetType().GetProperties())
                if (IsString(property))
                {
                    outputStream.WriteLine(property.GetValue(SettingsValuesInstance));
                }
                else if (IsDataTable(property))
                {
                    var table = (DataTable) property.GetValue(SettingsValuesInstance);
                    var s = "";
                    for (var i = 0; i < table.Rows.Count; ++i)
                    {
                        var row = table.Rows[i];
                        s += row[ColumnsColumnName];
                        if (i + 1 < table.Rows.Count)
                            s += ",";
                    }
                    outputStream.WriteLine(s);
                }
            outputStream.Flush();
            outputStream.Close();
        }

        // The settings model with all of the given values to keep track of
        public class SettingsValues : INotifyPropertyChanged
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

        #region Public SettingsForm Accessors

        public string Username => SettingsValuesInstance.Username;
        public string Password => SettingsValuesInstance.Password;
        public string ApiKey => SettingsValuesInstance.ApiKey;
        public string EventName => SettingsValuesInstance.EventName;
        public string DatabaseUrl => SettingsValuesInstance.DatabaseUrl;
        public DataTable AverageHeaders => SettingsValuesInstance.AverageHeaders;
        public DataTable SumHeaders => SettingsValuesInstance.SumHeaders;
        public DataTable MinimumHeaders => SettingsValuesInstance.MinimumHeaders;
        public DataTable MaximumHeaders => SettingsValuesInstance.MaximumHeaders;

        #endregion
    }
}