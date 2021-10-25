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
    }
}