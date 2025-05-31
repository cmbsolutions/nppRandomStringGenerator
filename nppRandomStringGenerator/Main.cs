using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;
using NppPluginNET.Utils;
using nppRandomStringGenerator.Forms;
using nppRandomStringGenerator.Storage;

namespace Kbg.NppPluginNET
{
    class Main
    {
        public static readonly string PluginConfigDirectory = Path.Combine(Npp.notepad.GetConfigDirectory(), PluginName);
        internal const string PluginName = "nppRandomStringGenerator";
        static ConfigAndGenerate ConfigAndGenerate = null;
        static About About = null;
        static Settings MySettings = null;
        static InsertGuid mInsertGuid = null;
        public static bool isShuttingDown = false;


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
            PluginBase.SetCommand(0, "Config && Generate", myDockableDialog);
            PluginBase.SetCommand(1, "Insert GUID at cursor", myDockableGuidDialog);
            PluginBase.SetCommand(2, "&About", AboutnppRandomStringGenerator);
        }

        private static void myDockableGuidDialog()
        {
            MySettings = new Settings();
            MySettings.Load();

            mInsertGuid = new InsertGuid
            {
                settings = MySettings
            };
            mInsertGuid.LoadSettings();
            mInsertGuid.ShowDialog();

            mInsertGuid = null;

        }

        internal static void SetToolBarIcons()
        {

        }

        internal static void PluginCleanUp()
        {

        }


        internal static void myDockableDialog()
        {
            MySettings = new Settings();
            MySettings.Load();

            ConfigAndGenerate = new ConfigAndGenerate();
            ConfigAndGenerate.settings = MySettings;
            ConfigAndGenerate.LoadSettings();
            ConfigAndGenerate.ShowDialog();

            ConfigAndGenerate = null;
        }
        /// <summary>
        /// Shows the "About" dialog window
        /// </summary>
        public static void AboutnppRandomStringGenerator()
        {
            About = new About();
            About.ShowDialog();
            About = null;

        }

    }

}