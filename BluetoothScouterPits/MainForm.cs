using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BluetoothScouterPits
{
    public partial class MainForm : Form
    {
        private const int TeamNumberColumn = 0;

        // Have to be the same as in the scouting application!
        private const string TeamNumber = "Team Number";

        private const string MatchNumber = "Match Number";
        private const string Average = "Average of: ";
        private const string Sum = "Sum of: ";
        private const string Minimum = "Minimum of: ";
        private const string Maximum = "Maximum of: ";


        private readonly DataSource database;
        private readonly SettingsForm settingsForm = new SettingsForm();

        private List<DataSource.MatchObject> matches;

        private bool synchronizedViews;

        public MainForm()
        {
            InitializeComponent();

            SetWatermark(searchBox, "Search By Team");

            database = new DataSource(settingsForm);

            Sync();
        }

        private async void Sync()
        {
            database.RefreshCredentials();
            try
            {
                matches = await database.Get();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            RePopulate();
        }

        private void RePopulate()
        {
            // Set columns and rows
            searchDataGridView.DataSource = PopulateDataGridViewCalculated(matches);

            // Set columns but not rows
            pinnedDataGridView.DataSource = PopulateDataGridViewCalculated(matches, true);
        }

        private void OnSyncMenu(object sender, EventArgs e)
        {
            Sync();
        }

        private void OnSettingsMenu(object sender, EventArgs e)
        {
            settingsForm.ShowDialog();
            RePopulate();
        }

        private void OnResetMenu(object sender, EventArgs e)
        {
            RePopulate();
        }

        private void OnExitMenu(object sender, EventArgs e)
        {
            Close();
        }

        private void OnSearchViewSelectionChanged(object sender, EventArgs e)
        {
            if (synchronizedViews) return;
            SynchronizeSelections(searchDataGridView, pinnedDataGridView);
            ShowDetailMatches(searchDataGridView.CurrentRow);
        }

        private void OnPinnedViewSelectionChanged(object sender, EventArgs e)
        {
            if (synchronizedViews) return;
            SynchronizeSelections(pinnedDataGridView, searchDataGridView);
            ShowDetailMatches(pinnedDataGridView.CurrentRow);
        }

        // Changes the selection of one table to another, if possible
        private void SynchronizeSelections(DataGridView masterView, DataGridView childView)
        {
            // Lock the selectionChanged events to prevent infinite triggering
            synchronizedViews = true;

            // Null checks, and release selectionChanged events
            if (masterView?.CurrentRow == null || childView?.Rows == null)
            {
                synchronizedViews = false;
                return;
            }

            var row = masterView.CurrentRow;

            // If the team is pinned, also highlight pinned team
            var matchingRows = childView.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[TeamNumberColumn].Value == row.Cells[TeamNumberColumn].Value)
                .ToList();

            // More checks, and release selectionChanged events
            if (!matchingRows.Any())
            {
                synchronizedViews = false;
                return;
            }

            childView.ClearSelection();

            matchingRows.First().Selected = true;

            // Release selectionChanged events
            synchronizedViews = false;
        }

        // Show all the available inputMatches for a selected team
        private void ShowDetailMatches(DataGridViewRow row)
        {
            // Null checks
            if (row?.Cells[TeamNumberColumn]?.Value == null) return;

            // Remember the vertical scroll position of the DataGridView
            var saveVScroll = 0;
            if (matchesDataGridView.Rows.Count > 0)
                saveVScroll = matchesDataGridView.FirstDisplayedCell.RowIndex;

            // Remember the horizontal scroll position of the DataGridView
            var saveHScroll = 0;
            if (matchesDataGridView.HorizontalScrollingOffset > 0)
                saveHScroll = matchesDataGridView.HorizontalScrollingOffset;

            // Get the team number of selected row
            var detailTeamNumber = row.Cells[TeamNumberColumn].Value.ToString();

            // Clear view and populate with new data
            var teamMatches = matches.Where(m => m.TeamNumber == detailTeamNumber);
            matchesDataGridView.DataSource = PopulateDataGridViewRaw(teamMatches.ToList());

            // Go back to the saved vertical scroll position if available
            if (saveVScroll != 0 && saveVScroll < matchesDataGridView.Rows.Count)
                matchesDataGridView.FirstDisplayedScrollingRowIndex = saveVScroll;

            // Go back to the saved horizontal scroll position if available
            if (saveHScroll != 0)
                matchesDataGridView.HorizontalScrollingOffset = saveHScroll;
        }

        // Fill datagridview with every available json value
        public static DataTable PopulateDataGridViewRaw(
            IReadOnlyCollection<DataSource.MatchObject> matches)
        {
            var table = new DataTable();

            if (matches == null || !matches.Any()) return table;

            var defaultMatch = matches.FirstOrDefault();

            table.Columns.Add(TeamNumber);
            table.Columns.Add(MatchNumber);

            if (defaultMatch != null)
                foreach (var s in defaultMatch.GetAllKeys())
                    table.Columns.Add(s);

            foreach (var value in matches)
            {
                var values = new List<string>
                {
                    value.TeamNumber,
                    value.MatchNumber
                };
                values.AddRange(value.GetAllValues());
                table.Rows.Add(values.ToArray<object>());
            }

            return table;
        }

        // Fill a datagridview with the teamnumber and every calculated column
        public DataTable PopulateDataGridViewCalculated(
            IReadOnlyCollection<DataSource.MatchObject> inputMatches,
            bool columnsOnly = false, SettingsForm inputSettings = null)
        {
            var table = new DataTable();

            if (inputSettings == null)
                inputSettings = settingsForm;

            if (inputMatches == null || !inputMatches.Any()) return table;

            var columns = new List<string> {TeamNumber};
            var firstOrDefault = inputMatches.FirstOrDefault();
            if (firstOrDefault == null) return table;

            var rawColumns = firstOrDefault.GetAllKeys();

            var averageColumns = new List<string>();
            var sumColumns = new List<string>();
            var minimumColumns = new List<string>();
            var maximumColumns = new List<string>();

            #region Enumeration and Validation of Calculated Columns

            List<string> result;
            // Average columns
            try
            {
                result =
                (from DataRow row in inputSettings.AverageHeaders.Rows
                    select (string) row[SettingsForm.ColumnsColumnName]
                    into column
                    where rawColumns.Contains(column)
                    select column).ToList();

                columns.AddRange(result.Select(r => Average + r));
                averageColumns.AddRange(result);
            }
            // Exceptions from user deleting all rows
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            // Sum columns
            try
            {
                result =
                (from DataRow row in inputSettings.SumHeaders.Rows
                    select (string) row[SettingsForm.ColumnsColumnName]
                    into column
                    where rawColumns.Contains(column)
                    select column).ToList();
                columns.AddRange(result.Select(r => Sum + r));
                sumColumns.AddRange(result);
                // Exceptions from user deleting all rows
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            // Minimum columns
            try
            {
                result =
                (from DataRow row in inputSettings.MinimumHeaders.Rows
                    select (string) row[SettingsForm.ColumnsColumnName]
                    into column
                    where rawColumns.Contains(column)
                    select column).ToList();

                columns.AddRange(result.Select(r => Minimum + r));
                minimumColumns.AddRange(result);
            }
            // Exceptions from user deleting all rows
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            // Maximum columns
            try
            {
                result =
                (from DataRow row in inputSettings.MaximumHeaders.Rows
                    select (string) row[SettingsForm.ColumnsColumnName]
                    into column
                    where rawColumns.Contains(column)
                    select column).ToList();

                columns.AddRange(result.Select(r => Maximum + r));
                maximumColumns.AddRange(result);
            }
            // Exceptions from user deleting all rows
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            #endregion

            var teams = inputMatches.Select(m => m.TeamNumber).Distinct().ToList();
            var rows = new List<List<string>>();


            foreach (var t in teams)
            {
                var row = new List<string>
                {
                    t
                };

                row.AddRange(
                    averageColumns.Select(column => inputMatches.Where(m => m.TeamNumber == t)
                            .Select(m => SafeNumberConversion(m.GetValue(column)))
                            .Average()
                            .ToString(CultureInfo.InvariantCulture))
                        .ToList());

                row.AddRange(sumColumns.Select(column => inputMatches.Where(m => m.TeamNumber == t)
                    .Select(m => SafeNumberConversion(m.GetValue(column)))
                    .Sum()
                    .ToString()));

                row.AddRange(minimumColumns.Select(column => inputMatches.Where(m => m.TeamNumber == t)
                    .Select(m => SafeNumberConversion(m.GetValue(column)))
                    .Min()
                    .ToString()));

                row.AddRange(maximumColumns.Select(column => inputMatches.Where(m => m.TeamNumber == t)
                    .Select(m => SafeNumberConversion(m.GetValue(column)))
                    .Max()
                    .ToString()));

                rows.Add(row);
            }

            foreach (var c in columns)
                table.Columns.Add(c);

            if (columnsOnly) return table;

            foreach (var r in rows)
                table.Rows.Add(r.ToArray<object>());

            return table;
        }

        // Pinning and unpinning of rows
        private void OnSearchViewDoubleClick(object sender, EventArgs e)
        {
            if (searchDataGridView?.CurrentRow == null) return;

            var row = searchDataGridView.CurrentRow;

            // Check if already pinned
            if (
                pinnedDataGridView.Rows
                    .Cast<DataGridViewRow>()
                    .Any(r => r.Cells[TeamNumberColumn].Value ==
                              row.Cells[TeamNumberColumn].Value))
                return;

            var values = new List<string>();

            for (var i = 0; i < searchDataGridView.CurrentRow.Cells.Count; ++i)
                values.Add((string) searchDataGridView.CurrentRow.Cells[i].Value);

            // pinnedDataGridView.Rows.Add(finalRow);
            ((DataTable) pinnedDataGridView.DataSource).Rows.Add(values.ToArray());

            SynchronizeSelections(searchDataGridView, pinnedDataGridView);
        }

        private void OnPinnedViewDoubleClick(object sender, EventArgs e)
        {
            if (pinnedDataGridView?.CurrentRow == null) return;

            pinnedDataGridView.Rows.Remove(pinnedDataGridView.CurrentRow);

            SynchronizeSelections(pinnedDataGridView, searchDataGridView);
        }

        // Update the search results
        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            var query = matches.Where(m => m.TeamNumber.Contains(searchBox.Text)).ToList();
            searchDataGridView.DataSource = PopulateDataGridViewCalculated(query);
        }

        // Safely convert a string to an integer, checks int+boolean, defaults to 0
        private static int SafeNumberConversion(string input)
        {
            try
            {
                // Try a simple integer
                return Convert.ToInt32(input);
            }
            // Not an integer value
            catch (FormatException)
            {
                try
                {
                    // Try boolean
                    return Convert.ToInt32(Convert.ToBoolean(input));
                }
                // Not a boolean value
                catch (FormatException)
                {
                    // Just return 0
                    return 0;
                }
            }
        }

        // Used to set watermark of a textbox using native windows function
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam,
            [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public static void SetWatermark(MaskedTextBox box, string message)
        {
            SendMessage(box.Handle, 0x1501, 1, message);
        }

        private void OnMasterViewSelectionChanged(object sender, EventArgs e)
        {
            var row = ((DataGridView) sender).CurrentRow;
            if (row == null) return;
            row.Selected = false;
            row.Selected = true;
        }
    }
}