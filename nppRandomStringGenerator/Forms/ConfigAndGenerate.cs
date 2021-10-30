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

        private void ButtonGenerate_Click(Object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.AvailableChars = "";
            this.StartChars = "";

            if (CheckboxNumbers.Checked) this.AvailableChars += "0123456789";
            if (CheckboxLowercase.Checked)
            {
                this.AvailableChars += "abcdefghijklmnopqrstuvwxyz";
                if (CheckboxBeginLetter.Checked) this.StartChars += "abcdefghijklmnopqrstuvwxyz";
            }
            if (CheckboxUppercase.Checked)
            {
                this.AvailableChars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                if (CheckboxBeginLetter.Checked) this.StartChars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            if (CheckboxSymbols.Checked && TextboxSymbols.TextLength > 0) this.AvailableChars += TextboxSymbols.Text;
            if (CheckboxSimilar.Checked)
            {
                Regex regexObj = new Regex("[iI1lLoO0|!jJ]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                this.AvailableChars = regexObj.Replace(this.AvailableChars, "");
                if (CheckboxBeginLetter.Checked) this.StartChars = regexObj.Replace(this.StartChars, "");
            }

            if (RadioButtonInline.Checked && TextboxSeperator.TextLength > 0)
            {
                this.AvailableChars = this.AvailableChars.Replace(TextboxSeperator.Text, "");
                if (CheckboxBeginLetter.Checked) this.StartChars = this.StartChars.Replace(TextboxSeperator.Text, "");
            }

            if (this.AvailableChars.Length > 0)
            {
                if (RadioButtonNew.Checked) this.Notepad.FileNew();
                if (RadioButtonCurrent.Checked) this.Editor.DocumentEnd();
                if (RadioButtonInline.Checked) this.Editor.DocumentStart();

                int idx = 0;
                int previousChar = 0;
                int currentChar = 0;

                Random rnd = new Random();

                for (int i = 0; i < NumericUpDownQuantity.Value; i++)
                {
                    string code = "";

                    for (int y = 0; y < NumericUpDownLength.Value; y++)
                    {
                        if (y == 0 && CheckboxBeginLetter.Checked)
                        {
                            idx = rnd.Next(0, this.StartChars.Length - 1);
                            code += this.StartChars[idx];
                            previousChar = (int)this.StartChars[idx];
                        }
                        else
                        {
                            idx = rnd.Next(0, this.AvailableChars.Length - 1);
                            currentChar = (int)this.AvailableChars[idx];

                            if (CheckboxSequential.Checked)
                            {
                                while (previousChar - 1 == currentChar || previousChar + 1 == currentChar)
                                {
                                    idx = rnd.Next(0, this.AvailableChars.Length - 1);
                                    currentChar = (int)this.AvailableChars[idx];
                                }
                            }
                            if (CheckboxDuplicate.Checked)
                            {
                                while (previousChar == currentChar)
                                {
                                    idx = rnd.Next(0, this.AvailableChars.Length - 1);
                                    currentChar = (int)this.AvailableChars[idx];
                                }
                            }
                            previousChar = (int)this.AvailableChars[idx];
                            code += this.AvailableChars[idx];
                        }
                    }

                    if (RadioButtonInline.Checked)
                    {
                        code = TextboxSeperator.Text + code;
                        this.Editor.LineEnd();
                        this.Editor.AddText(code.Length, code);
                        this.Editor.LineDown();
                    } else
                    {
                        this.Editor.AddText(code.Length, code);
                        this.Editor.NewLine();
                    }
                }
                MessageBox.Show("Strings are generated.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Nothing to generate.");
            }

            this.Cursor = Cursors.Default;
        }

        private void NumericUpDownQuantity_ValueChanged(object sender, EventArgs e)
        {
            if ( NumericUpDownQuantity.Value > 4096)
            {
                toolTip1.Active = true;
                toolTip1.SetToolTip(NumericUpDownQuantity, "This could take a while depending on your hardware.");
                NumericUpDownQuantity.ForeColor = Color.Red;
            } else
            {
                toolTip1.Active = false;
                NumericUpDownQuantity.ForeColor = Color.Black;
            }
        }

        private void CheckboxLowercase_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckboxLowercase.Checked && !CheckboxUppercase.Checked) CheckboxBeginLetter.Checked = false;
        }

        private void CheckboxUppercase_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckboxLowercase.Checked && !CheckboxUppercase.Checked) CheckboxBeginLetter.Checked = false;
        }

        private void RadioButtonInline_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonInline.Checked)
            {
                NumericUpDownQuantity.Enabled = false;
                NumericUpDownQuantity.Maximum = this.Editor.GetLineCount();
                NumericUpDownQuantity.Value = this.Editor.GetLineCount();
                TextboxSeperator.Enabled = true;
            }
            else
            {
                NumericUpDownQuantity.Maximum = 10240;
                NumericUpDownQuantity.Value = 8;
                NumericUpDownQuantity.Enabled = true;
                TextboxSeperator.Enabled = false;
            }

        }

        private void CheckboxBeginLetter_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckboxLowercase.Checked && !CheckboxUppercase.Checked) CheckboxBeginLetter.Checked = false;
        }
    }
}
