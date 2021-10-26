using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;
using Microsoft;

namespace Kbg.NppPluginNET
{
    public partial class ConfigAndGenerate : Form
    {
        private string AvailableChars;
        private string StartChars;
        private IScintillaGateway Editor;
        private INotepadPPGateway Notepad;

        public ConfigAndGenerate()
        {
            InitializeComponent();
            this.Editor = new ScintillaGateway(PluginBase.GetCurrentScintilla());
            this.Notepad = new NotepadPPGateway();
        }

        private void bGenerate_Click(Object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.AvailableChars = "";
            this.StartChars = "";

            if (chkNumbers.Checked) this.AvailableChars += "0123456789";
            if (chkLowercase.Checked)
            {
                this.AvailableChars += "abcdefghijklmnopqrstuvwxyz";
                if (chkBeginLetter.Checked) this.StartChars += "abcdefghijklmnopqrstuvwxyz";
            }
            if (chkUppercase.Checked)
            {
                this.AvailableChars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                if (chkBeginLetter.Checked) this.StartChars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            if (chkSymbols.Checked && txtSymbols.TextLength > 0) this.AvailableChars += txtSymbols.Text;
            if (chkSimilar.Checked)
            {
                Regex regexObj = new Regex("[iI1lLoO0|!jJ]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                this.AvailableChars = regexObj.Replace(this.AvailableChars, "");
                if (chkBeginLetter.Checked) this.StartChars = regexObj.Replace(this.StartChars, "");
            }

            if (this.AvailableChars.Length > 0)
            {
                if (rbNew.Checked) this.Notepad.FileNew();
                if (rbCurrent.Checked) this.Editor.DocumentEnd();

                int idx = 0;
                int previousChar = 0;
                int currentChar = 0;

                Random rnd = new Random();

                for (int i = 0; i < nudQuantity.Value; i++)
                {
                    string code = "";

                    for (int y = 0; y < nudLength.Value; y++)
                    {
                        if (y == 0 && chkBeginLetter.Checked)
                        {
                            idx = rnd.Next(0, this.StartChars.Length - 1);
                            code += this.StartChars[idx];
                            previousChar = (int)this.StartChars[idx];
                        }
                        else
                        {
                            idx = rnd.Next(0, this.AvailableChars.Length - 1);
                            currentChar = (int)this.AvailableChars[idx];

                            if (chkSequential.Checked)
                            {
                                while (previousChar - 1 == currentChar || previousChar + 1 == currentChar || previousChar == currentChar)
                                {
                                    idx = rnd.Next(0, this.AvailableChars.Length - 1);
                                    currentChar = (int)this.AvailableChars[idx];
                                }
                            }
                            previousChar = (int)this.AvailableChars[idx];
                            code += this.AvailableChars[idx];
                        }
                    }

                    this.Editor.AddText(code.Length, code);
                    this.Editor.NewLine();
                }
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Strings are generated.");
            this.Close();
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

        private void chkLowercase_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkLowercase.Checked && !chkUppercase.Checked) chkBeginLetter.Checked = false;
        }

        private void chkUppercase_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkLowercase.Checked && !chkUppercase.Checked) chkBeginLetter.Checked = false;
        }

    }
}
