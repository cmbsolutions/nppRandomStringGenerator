using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Kbg.NppPluginNET
{
    public partial class ConfigAndGenerate : Form
    {
        private string AvailableChars;
        private IScintillaGateway Editor;
        private INotepadPPGateway Notepad;

        public ConfigAndGenerate()
        {
            InitializeComponent();
            this.Editor = new ScintillaGateway(PluginBase.GetCurrentScintilla());
            this.Notepad = new NotepadPPGateway();
        }

        private void bGenerate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.AvailableChars = "";

            if (chkNumbers.Checked) this.AvailableChars += "0123456789";
            if (chkLowercase.Checked) this.AvailableChars += "abcdefghijklmnopqrstuvwxyz";
            if (chkUppercase.Checked) this.AvailableChars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (chkSymbols.Checked && txtSymbols.TextLength > 0) this.AvailableChars += txtSymbols.Text;
            if (chkSimilar.Checked)
            {
                Regex regexObj = new Regex("[iI1lLoO0|!jJ]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                this.AvailableChars = regexObj.Replace(this.AvailableChars, "");
            }

            if (rbNew.Checked)
            {
                this.Notepad.FileNew();
            }

            // TODO: add as background task and manage cancelation
            Random rnd = new Random();

            for (int i = 0; i<nudQuantity.Value; i++)
            {
                string code = "";

                for (int y = 0;y<nudLength.Value; y++)
                {
                    int idx = rnd.Next(0, this.AvailableChars.Length - 1);
                    while (y==0 && chkBeginLetter.Checked && !Regex.IsMatch(this.AvailableChars.Substring(idx, 1), @"\A[a-z]\z", RegexOptions.IgnoreCase | RegexOptions.Multiline))
                    {
                        idx = rnd.Next(0, this.AvailableChars.Length);
                    }

                    code += this.AvailableChars.Substring(idx,1);
                }

                this.Editor.AddText(code.Length, code);
                this.Editor.NewLine();
            }

            this.Cursor = Cursors.Default;
        }

        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            if ( nudQuantity.Value > 4096)
            {
                toolTip1.Active = true;
                toolTip1.SetToolTip(nudQuantity, "This could take a while depending on the hardware!");
                nudQuantity.ForeColor = Color.Red;
            } else
            {
                toolTip1.Active = false;
                nudQuantity.ForeColor = Color.Black;
            }
        }
    }
}
