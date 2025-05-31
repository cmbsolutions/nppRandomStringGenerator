using Kbg.NppPluginNET.PluginInfrastructure;
using nppRandomStringGenerator.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nppRandomStringGenerator.Forms
{
    public partial class InsertGuid : Form
    {
        public Settings settings { get; set; }
        private IScintillaGateway Editor;
        private INotepadPPGateway Notepad;

        public InsertGuid()
        {
            InitializeComponent();
            this.Editor = new ScintillaGateway(PluginBase.GetCurrentScintilla());
            this.Notepad = new NotepadPPGateway();
        }

        public void LoadSettings()
        {
            foreach (nppRandomStringGenerator.Storage.Models.ConfigItem configitem in settings.settings.ConfigItems)
            {
                if (configitem == null || !configitem.Name.StartsWith("Quick")) { continue; }

                Control ctrl = this.Controls.Find(configitem.Value, true).FirstOrDefault();

                if (ctrl != null)
                {
                    RadioButton radio = ctrl as RadioButton;
                    radio.Checked = true;
                }
            }
        }

        private void SaveSettings()
        {
            foreach (nppRandomStringGenerator.Storage.Models.ConfigItem configitem in settings.settings.ConfigItems)
            {
                if (configitem == null || !configitem.Name.StartsWith("Quick")) { continue; }

                foreach (Control ctrl in this.groupBox1.Controls)
                {
                    if (ctrl is RadioButton radio && radio.Checked)
                    {
                        configitem.Value = radio.Name;
                        break;
                    }
                }
            }

            settings.Save();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (radioB.Checked) { this.Editor.InsertText(this.Editor.GetCurrentPos(), Guid.NewGuid().ToString("B")); }
            if (radioD.Checked) { this.Editor.InsertText(this.Editor.GetCurrentPos(), Guid.NewGuid().ToString("D")); }
            if (radioN.Checked) { this.Editor.InsertText(this.Editor.GetCurrentPos(), Guid.NewGuid().ToString("N")); }
            if (radioX.Checked) { this.Editor.InsertText(this.Editor.GetCurrentPos(), Guid.NewGuid().ToString("X")); }
            if (radioUE.Checked) { this.Editor.InsertText(this.Editor.GetCurrentPos(), "(ID=" + Guid.NewGuid().ToString("N") + ")"); }

            this.SaveSettings();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
