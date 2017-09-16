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
        private readonly Settings settings = new Settings();

        public MainForm()
        {
            InitializeComponent();

            SetWatermark(searchBox, "Search By Team");
            // TestLoad();
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
            settings.Show();
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
