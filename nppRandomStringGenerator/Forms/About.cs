using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Kbg.NppPluginNET
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            label1.Text = $"nppRandomStringGenerator v{GetAssemblyFileVersion()}\r\n\r\nGenerates random strings with configurable output.";
        }

        public static string GetAssemblyFileVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersion.FileVersion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
