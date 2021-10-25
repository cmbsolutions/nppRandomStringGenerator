using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;
using nppRandomStringGenerator;

namespace Kbg.NppPluginNET
{
    public partial class ConfigAndGenerate : Form
    {
        private string AvailableChars;
        private string StartChars;
        private IScintillaGateway Editor;
        private INotepadPPGateway Notepad;

        private generator Gen;

        public ConfigAndGenerate()
        {
            InitializeComponent();
            this.Editor = new ScintillaGateway(PluginBase.GetCurrentScintilla());
            this.Notepad = new NotepadPPGateway();
        }

        private void bGenerate_Click(object sender, EventArgs e)
        {
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
                if (rbNew.Checked)
                {
                    this.Notepad.FileNew();
                }

                bGenerate.Enabled = false;
                toolStripSplitButton1.Enabled = true;
                this.Gen = new generator();
                this.Gen.AvailableChars = this.AvailableChars;
                this.Gen.StartChars = this.StartChars;
                this.Gen.quantity = (int)nudQuantity.Value;
                this.Gen.stringLength = (int)nudLength.Value;

                this.Gen.Generate(ref this.Editor, ref this.Notepad);
                //backgroundWorker1.RunWorkerAsync();
            }
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

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > toolStripProgressBar1.Maximum) toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
            else toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ( e.Cancelled)
            {
                MessageBox.Show("Generation was cancelled by user");
                this.Close();
            } else
            {
                toolStripSplitButton1.Enabled = false;
                MessageBox.Show("Random strings are generated");
                this.Close();
            }
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            if (this.Gen != null) this.Gen.CancelGenerator();        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Decimal perc = 0;
                int old_perc = 0;
                int idx = 0;
                Random rnd = new Random();

                for (int i = 0; i < nudQuantity.Value; i++)
                {
                    string code = "";
                    perc += nudQuantity.Value / 100;
                    if ( (int)perc != old_perc)
                    {
                        backgroundWorker1.ReportProgress((int)perc);
                        old_perc = (int)perc;
                    }                    

                    if ( backgroundWorker1.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    for (int y = 0; y < nudLength.Value; y++)
                    {
                        if ( y == 0 && chkBeginLetter.Checked)
                        {
                            idx = rnd.Next(0, this.StartChars.Length - 1);
                            code += this.StartChars.Substring(idx, 1);
                        }
                        else
                        {
                            idx = rnd.Next(0, this.AvailableChars.Length - 1);
                            code += this.AvailableChars.Substring(idx, 1);
                        }
                    }

                    this.Editor.AddText(code.Length, code);
                    this.Editor.NewLine();
                }
                e.Result = "Completed";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
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
