using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BluetoothScouterPits
{
    public partial class MainForm : Form
    {
        private string username;
        private string password;
        private List<string> averageHeaders;
        private List<string> sumHeaders;

        public MainForm()
        {
            InitializeComponent();

            SetWatermark(searchBox, "Search By Team");
            // TestLoad();

            LoadConfig();
        }

        private const string ConfigurationFile = "config.cfg";
        private void LoadConfig()
        {
            try
            {
                using (var reader = new StreamReader(ConfigurationFile))
                {
                    username = reader.ReadLine(); // Firebase username
                    password = reader.ReadLine(); // Firebase password

                    // Headers to create average column
                    var averages = reader.ReadLine();
                    if (averages != null && averages.Split(',').ToList().Any())
                        averageHeaders = averages.Split(',').ToList();

                    // Headers to create sum column
                    var sums = reader.ReadLine();
                    if (sums != null && sums.Split(',').ToList().Any())
                        sumHeaders = sums.Split(',').ToList();
                }
            }
            catch (IOException e)
            {
                // Empty or corrupted, clear out the data
                using (var writer = new StreamWriter(ConfigurationFile))
                {
                    writer.WriteLine(""); // Username
                    writer.WriteLine(""); // Password
                    writer.WriteLine(""); // Headers to average (csv)
                    writer.WriteLine(""); // Headers to sum (csv)
                    writer.FlushAsync();
                }
            }
        }

        /*
        public async void TestLoad()
        {
            var values = await new DataSource("jakob.olechiw@gmail.com", "firebasetest").Get();
            String s = "";
            foreach (var v in values)
                s += v;
            searchBox.Text = s;
        }
        */

        private void OnSyncMenu(object sender, EventArgs e)
        {

        }

        private void OnSettingsMenu(object sender, EventArgs e)
        {

        }

        private void OnExitMenu(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnHelpMenu(object sender, EventArgs e)
        {

        }

        
        // Within your class or scoped in a more appropriate location:
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public static void SetWatermark(TextBox box, string message)
        {
            SendMessage(box.Handle, 0x1501, 1, message);
        }
    }
}
