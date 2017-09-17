using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BluetoothScouterPits
{
    public partial class MainForm : Form
    {
        private const int teamNumberColumn = 0;
        private const int matchNumberColumn = 1;
        private const string teamNumber = "Team Number";
        private const string matchNumber = "Match Number";
        private const string average = "Average of: ";
        private const string sum = "Sum of: ";
        private const string minimum = "Minimum of: ";
        private const string maximum = "Maximum of: ";


        private readonly DataSource database;
        private readonly Settings settings = new Settings();

        private List<DataSource.MatchObject> matches;

        public MainForm()
        {
            InitializeComponent();

            SetWatermark(searchBox, "Search By Team");

            database = new DataSource(settings);

            Sync();

            // TestLoad();
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

        /*
        public async void TestLoad()
        {
            var values = await new DataSource("jakob.olechiw@gmail.com", "firebasetest").GetValue();
            String s = "";
            foreach (var v in values)
                s += v;
            searchBox.Text = s;
        }
        */

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

        // Within your class or scoped in a more appropriate location:
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam,
            [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public static void SetWatermark(MaskedTextBox box, string message)
        {
            SendMessage(box.Handle, 0x1501, 1, message);
        }

        private bool synchronizedViews = false;
        private void onSearchViewSelected(object sender, EventArgs e)
        {
            if (synchronizedViews) return;
            SynchronizeSelections(searchDataGridView, pinnedDataGridView);
            ShowDetailMatches(searchDataGridView.CurrentRow);
        }
        
        // Changes the selection of one table to another, if possible
        private void SynchronizeSelections(DataGridView masterView, DataGridView childView)
        {
            synchronizedViews = true;
            if (masterView?.CurrentRow == null || childView?.Rows == null)
            {
                synchronizedViews = false;
                return;
            }

            var row = masterView.CurrentRow;

            // If the team is pinned, also highlight pinned team
            var matchingRows = childView.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[teamNumberColumn].Value == row.Cells[teamNumberColumn].Value)
                .ToList();

            if (!matchingRows.Any())
            {
                synchronizedViews = false;
                return;
            }

            childView.ClearSelection();

            matchingRows.First().Selected = true;
            synchronizedViews = false;
        }

        private void onPinnedViewSelected(object sender, EventArgs e)
        {
            if (synchronizedViews) return;
            SynchronizeSelections(pinnedDataGridView, searchDataGridView);
            ShowDetailMatches(pinnedDataGridView.CurrentRow);
        }

        private void ShowDetailMatches(DataGridViewRow row)
        {
            // Null checks
            if (row?.Cells?[teamNumberColumn]?.Value == null) return;

            // Remember the vertical scroll position of the DataGridView
            var saveVScroll = 0;
            if (matchesDataGridView.Rows.Count > 0)
                saveVScroll = matchesDataGridView.FirstDisplayedCell.RowIndex;

            // Remember the horizontal scroll position of the DataGridView
            var saveHScroll = 0;
            if (matchesDataGridView.HorizontalScrollingOffset > 0)
                saveHScroll = matchesDataGridView.HorizontalScrollingOffset;

            // Get the team number of selected row
            var detailTeamNumber = row.Cells[teamNumberColumn].Value.ToString();

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

        private static void PopulateDataGridViewRaw(DataGridView v,
            IReadOnlyCollection<DataSource.MatchObject> matches)
        {
            if (matches == null || !matches.Any()) return;

            var defaultMatch = matches.FirstOrDefault();

            v.Columns.Add(teamNumber, teamNumber);
            v.Columns.Add(matchNumber, matchNumber);

            foreach (var s in defaultMatch.GetAllKeys())
                v.Columns.Add(s, s);

            foreach (var value in matches)
            {
                var values = new List<string>()
                {
                    value.TeamNumber,
                    value.MatchNumber
                };
                values.AddRange(value.GetAllValues());
                v.Rows.Add(values.ToArray<object>());
            }
        }

        private void PopulateDataGridViewCalculated(DataGridView v,
            IReadOnlyCollection<DataSource.MatchObject> matches,
            bool columnsOnly = false)
        {
            if (matches == null || !matches.Any()) return;

            var columns = new List<string>() {teamNumber};
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

            columns.AddRange(result.Select(r => average + r));
            averageColumns.AddRange(result);

            // Sum columns
            result =
            (from DataRow row in settings.SumHeaders.Rows
                select (string) row[Settings.ColumnsColumnName]
                into column
                where rawColumns.Contains(column)
                select column).ToList();

            columns.AddRange(result.Select(r => sum + r));
            sumColumns.AddRange(result);

            // Minimum columns
            result =
            (from DataRow row in settings.MinimumHeaders.Rows
                select (string) row[Settings.ColumnsColumnName]
                into column
                where rawColumns.Contains(column)
                select column).ToList();

            columns.AddRange(result.Select(r => minimum + r));
            minimumColumns.AddRange(result);

            // Maximum columns
            result =
            (from DataRow row in settings.MaximumHeaders.Rows
                select (string) row[Settings.ColumnsColumnName]
                into column
                where rawColumns.Contains(column)
                select column).ToList();

            columns.AddRange(result.Select(r => maximum + r));
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

        private int SafeNumberConversion(string input)
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

        private void onSearchViewDoubleClick(object sender, EventArgs e)
        {
            if (searchDataGridView?.CurrentRow == null) return;

            var row = searchDataGridView.CurrentRow;

            // Check if already pinned
            if (
                pinnedDataGridView.Rows
                    .Cast<DataGridViewRow>()
                    .Any(r => r.Cells[teamNumberColumn].Value == 
                    row.Cells[teamNumberColumn].Value))
                return;

            var finalRow = (DataGridViewRow) searchDataGridView.CurrentRow.Clone();
            for (var i = 0; i < searchDataGridView.CurrentRow.Cells.Count; ++i)
            {
                finalRow.Cells[i].Value = searchDataGridView.CurrentRow.Cells[i].Value;
            }
            pinnedDataGridView.Rows.Add(finalRow);

            SynchronizeSelections(searchDataGridView, pinnedDataGridView);
        }

        private void onPinnedViewDoubleClick(object sender, EventArgs e)
        {
            if (pinnedDataGridView?.CurrentRow == null) return;

            pinnedDataGridView.Rows.Remove(pinnedDataGridView.CurrentRow);

            SynchronizeSelections(pinnedDataGridView, searchDataGridView);
        }
    }
}