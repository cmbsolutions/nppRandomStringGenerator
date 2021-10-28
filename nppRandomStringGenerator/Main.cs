using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Kbg.NppPluginNET
{
    class Main
    {
        internal const string PluginName = "nppRandomStringGenerator";
        static ConfigAndGenerate ConfigAndGenerate = null;

        public static void OnNotification(ScNotification notification)
        {  
            // This method is invoked whenever something is happening in notepad++
            // use eg. as
            // if (notification.Header.Code == (uint)NppMsg.NPPN_xxx)
            // { ... }
            // or
            //
            // if (notification.Header.Code == (uint)SciMsg.SCNxxx)
            // { ... }

        }

        internal static void CommandMenuInit()
        {
            PluginBase.SetCommand(0, "Config && Generate", myDockableDialog );
            PluginBase.SetCommand(1, "&About", AboutnppRandomStringGenerator);
        }

        internal static void SetToolBarIcon()
        {

        }

        internal static void PluginCleanUp()
        {

        }


        internal static void myDockableDialog()
        {
            ConfigAndGenerate = new ConfigAndGenerate();

            ConfigAndGenerate.ShowDialog();

            ConfigAndGenerate = null;
        }
        /// <summary>
        /// Shows the "About" dialog window
        /// </summary>
        public static void AboutnppRandomStringGenerator()
        {
            const int xsize = 300, ysize = 180;

            var gitVersionInformationType = Assembly.GetExecutingAssembly().GetType("nppRandomStringGenerator.GitVersionInformation");
            var semVer = (string)gitVersionInformationType.GetField("SemVer").GetValue(null);

            var dialog = new Form
            {
                Text = "About nppRandomStringGenerator",
                ClientSize = new Size(xsize, ysize),
                SizeGripStyle = SizeGripStyle.Hide,
                ShowIcon = false,
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                ShowInTaskbar = false,
                Controls =
                {
                    new Button
                    {
                        Name = "Ok",
                        Text = "&Ok",
                        Size = new Size(75, 23),
                        Location = new Point(xsize - 75 - 13, ysize - 23 - 13),
                        UseVisualStyleBackColor = true
                    },
                    new Label
                    {
                        Location = new Point(13,13),
                        Size = new Size(xsize-13-13,ysize-13-13-23-6),
                        Text = $"CSV Query v{semVer}\r\n\r\nAllows SQL queries against CSV files.\r\n\r\nThe SQL syntax is the same as SQLite.\r\nThe table \"THIS\" represents the current file.\r\n\r\nBy jokedst@gmail.com\r\nLicense: GPL v3",
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Consolas", 8.25F)
                    }
                }
            };
            dialog.Controls["Ok"].Click += (a, b) => dialog.Close();

            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.ShowDialog();
        }

    }
}