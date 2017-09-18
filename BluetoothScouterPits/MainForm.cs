using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BluetoothScouterPits
{
    public partial class MainForm : Form
    {
        private const int TeamNumberColumn = 0;
        private const int MatchNumberColumn = 1;
        // Have to be the same as in the scouting application!
        private const string TeamNumber = "Team Number";
        private const string MatchNumber = "Match Number";
        private const string Average = "Average of: ";
        private const string Sum = "Sum of: ";
        private const string Minimum = "Minimum of: ";
        private const string Maximum = "Maximum of: ";


        private readonly DataSource database;
        private readonly Settings settings = new Settings();

        private List<DataSource.MatchObject> matches;

        private bool synchronizedViews;

        public MainForm()
        {
            InitializeComponent();

            SetWatermark(searchBox, "Search By Team");

            database = new DataSource(settings);

            Sync();
        }

        private async void Sync()
        {
            database.RefreshCredentials();
            matches = await database.Get();

            RePopulate();
        }

        private void RePopulate()
        {
            // Empty all views
            searchDataGridView.Rows.Clear();
            searchDataGridView.Columns.Clear();
            pinnedDataGridView.Rows.Clear();
            pinnedDataGridView.Columns.Clear();
            matchesDataGridView.Rows.Clear();
            matchesDataGridView.Columns.Clear();

            PopulateDataGridViewCalculated(searchDataGridView, matches);

            // Set columns but not rows
            PopulateDataGridViewCalculated(pinnedDataGridView, matches, true);
        }

        private void OnSyncMenu(object sender, EventArgs e)
        {
            Sync();
        }

        private void OnSettingsMenu(object sender, EventArgs e)
        {
            settings.ShowDialog();
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

        // Show all the available matches for a selected team
        private void ShowDetailMatches(DataGridViewRow row)
        {
            // Null checks
            if (row?.Cells?[TeamNumberColumn]?.Value == null) return;

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
            matchesDataGridView.Rows.Clear();
            matchesDataGridView.Columns.Clear();
            var teamMatches = matches.Where(m => m.TeamNumber == detailTeamNumber);
            PopulateDataGridViewRaw(matchesDataGridView, teamMatches.ToList());

            // Go back to the saved vertical scroll position if available
            if (saveVScroll != 0 && saveVScroll < matchesDataGridView.Rows.Count)
                matchesDataGridView.FirstDisplayedScrollingRowIndex = saveVScroll;

            // Go back to the saved horizontal scroll position if available
            if (saveHScroll != 0)
                matchesDataGridView.HorizontalScrollingOffset = saveHScroll;
        }

        // Fill datagridview with every available json value
        private static void PopulateDataGridViewRaw(DataGridView v,
            IReadOnlyCollection<DataSource.MatchObject> matches)
        {
            if (matches == null || !matches.Any()) return;

            var defaultMatch = matches.FirstOrDefault();

            v.Columns.Add(TeamNumber, TeamNumber);
            v.Columns.Add(MatchNumber, MatchNumber);

            foreach (var s in defaultMatch.GetAllKeys())
                v.Columns.Add(s, s);

            foreach (var value in matches)
            {
                var values = new List<string>
                {
                    value.TeamNumber,
                    value.MatchNumber
                };
                values.AddRange(value.GetAllValues());
                v.Rows.Add(values.ToArray<object>());
            }
        }

        // Fill a datagridview with the teamnumber and every calculated column
        private void PopulateDataGridViewCalculated(DataGridView v,
            IReadOnlyCollection<DataSource.MatchObject> matches,
            bool columnsOnly = false)
        {
            if (matches == null || !matches.Any()) return;

            var columns = new List<string> {TeamNumber};
            var rawColumns = matches.FirstOrDefault().GetAllKeys();

            var averageColumns = new List<string>();
            var sumColumns = new List<string>();
            var minimumColumns = new List<string>();
            var maximumColumns = new List<string>();

            #region Enumeration and Validation of Calculated Columns

            // Average columns
            var result =
            (from DataRow row in settings.AverageHeaders.Rows
                select (string) row[Settings.ColumnsColumnName]
                into column
                where rawColumns.Contains(column)
                select column).ToList();

            columns.AddRange(result.Select(r => Average + r));
            averageColumns.AddRange(result);

            // Sum columns
            result =
            (from DataRow row in settings.SumHeaders.Rows
                select (string) row[Settings.ColumnsColumnName]
                into column
                where rawColumns.Contains(column)
                select column).ToList();

            columns.AddRange(result.Select(r => Sum + r));
            sumColumns.AddRange(result);

            // Minimum columns
            result =
            (from DataRow row in settings.MinimumHeaders.Rows
                select (string) row[Settings.ColumnsColumnName]
                into column
                where rawColumns.Contains(column)
                select column).ToList();

            columns.AddRange(result.Select(r => Minimum + r));
            minimumColumns.AddRange(result);

            // Maximum columns
            result =
            (from DataRow row in settings.MaximumHeaders.Rows
                select (string) row[Settings.ColumnsColumnName]
                into column
                where rawColumns.Contains(column)
                select column).ToList();

            columns.AddRange(result.Select(r => Maximum + r));
            maximumColumns.AddRange(result);

            #endregion

            var teams = matches.Select(m => m.TeamNumber).Distinct().ToList();
            var rows = new List<List<string>>();


            foreach (var t in teams)
            {
                var row = new List<string>
                {
                    t
                };

                row.AddRange(
                    averageColumns.Select(column => matches.Where(m => m.TeamNumber == t)
                            .Select(m => SafeNumberConversion(m.GetValue(column)))
                            .Average()
                            .ToString())
                        .ToList());

                row.AddRange(sumColumns.Select(column => matches.Where(m => m.TeamNumber == t)
                    .Select(m => SafeNumberConversion(m.GetValue(column)))
                    .Sum()
                    .ToString()));

                row.AddRange(minimumColumns.Select(column => matches.Where(m => m.TeamNumber == t)
                    .Select(m => SafeNumberConversion(m.GetValue(column)))
                    .Min()
                    .ToString()));

                row.AddRange(maximumColumns.Select(column => matches.Where(m => m.TeamNumber == t)
                    .Select(m => SafeNumberConversion(m.GetValue(column)))
                    .Max()
                    .ToString()));

                rows.Add(row);
            }

            foreach (var c in columns)
                v.Columns.Add(c, c);

            if (columnsOnly) return;

            foreach (var r in rows)
                v.Rows.Add(r.ToArray<object>());
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

            var finalRow = (DataGridViewRow) searchDataGridView.CurrentRow.Clone();
            for (var i = 0; i < searchDataGridView.CurrentRow.Cells.Count; ++i)
                finalRow.Cells[i].Value = searchDataGridView.CurrentRow.Cells[i].Value;
            pinnedDataGridView.Rows.Add(finalRow);

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
            searchDataGridView.Rows.Clear();
            searchDataGridView.Columns.Clear();
            PopulateDataGridViewCalculated(searchDataGridView, query);
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

        private void onMasterViewSelect(object sender, EventArgs e)
        {
            var row = ((DataGridView) sender).CurrentRow;
            row.Selected = false;
            row.Selected = true;
        }
    }
}