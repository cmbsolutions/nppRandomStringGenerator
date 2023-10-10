using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;
using nppRandomStringGenerator.Storage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Kbg.NppPluginNET
{
    public partial class ConfigAndGenerate : Form
    {
        private string AvailableChars;
        private string StartChars;
        private IScintillaGateway Editor;
        private INotepadPPGateway Notepad;

        public Settings settings { get; set; }



        public ConfigAndGenerate()
        {
            InitializeComponent();
            this.Editor = new ScintillaGateway(PluginBase.GetCurrentScintilla());
            this.Notepad = new NotepadPPGateway();
        }

        public void LoadSettings()
        {
            foreach (nppRandomStringGenerator.Storage.Models.ConfigItem configitem in settings.settings.ConfigItems)
            {
                if (configitem == null) { continue; }   

                Control ctrl = this.Controls.Find(configitem.Name, true).FirstOrDefault();

                if (ctrl.Name.StartsWith("NumericUpDown"))
                {
                    NumericUpDown nupdown = ctrl as NumericUpDown;
                    nupdown.Value = Math.Min(Convert.ToDecimal(configitem.Value), nupdown.Maximum);
                }
                if (ctrl.Name.StartsWith("Checkbox"))
                {
                    System.Windows.Forms.CheckBox check = ctrl as System.Windows.Forms.CheckBox;
                    check.Checked = Convert.ToBoolean(configitem.Value);
                    TriggerCheckBoxChangeEvent(check);
                }
                if (ctrl.Name.StartsWith("Textbox"))
                {
                    TextBox txt = ctrl as TextBox;
                    txt.Text = configitem.Value;
                }
                if (ctrl.Name.StartsWith("Radio"))
                {
                    System.Windows.Forms.RadioButton radio = ctrl as System.Windows.Forms.RadioButton;
                    radio.Checked = Convert.ToBoolean(configitem.Value);
                }
            }
        }

        private void SaveSettings()
        {
            foreach (nppRandomStringGenerator.Storage.Models.ConfigItem configitem in settings.settings.ConfigItems)
            {
                Control ctrl = this.Controls.Find(configitem.Name, true).FirstOrDefault();

                if (ctrl.Name.StartsWith("NumericUpDown"))
                {
                    NumericUpDown nupdown = ctrl as NumericUpDown;
                    configitem.Value = nupdown.Value.ToString();
                }
                if (ctrl.Name.StartsWith("Checkbox"))
                {
                    System.Windows.Forms.CheckBox check = ctrl as System.Windows.Forms.CheckBox;
                    configitem.Value = (check.Checked ? "true" : "false");
                }
                if (ctrl.Name.StartsWith("Textbox"))
                {
                    TextBox txt = ctrl as TextBox;
                    configitem.Value = txt.Text;
                }
                if (ctrl.Name.StartsWith("Radio"))
                {
                    System.Windows.Forms.RadioButton radio = ctrl as System.Windows.Forms.RadioButton;
                    configitem.Value = (radio.Checked ? "true" : "false");
                }
            }

            settings.Save();
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

                //for (int i = 0; i < NumericUpDownQuantity.Value; i++)
                var result = Parallel.For(0, (int)NumericUpDownQuantity.Value, (i, state) =>
                {
                    string code = "";

                    int length = (int)NumericUpDownLength.Value;

                    if (CheckboxDoRandom.Checked)
                    {
                        length = rnd.Next((int)NumericUpDownRandomMin.Value, (int)NumericUpDownRandomMax.Value);
                    }

                    for (int y = 0; y < length; y++)
                    {
                        if (y == 0 && CheckboxBeginLetter.Checked)
                        {
                            idx = rnd.Next(0, this.StartChars.Length);
                            code += this.StartChars[idx];
                            previousChar = (int)this.StartChars[idx];
                        }
                        else
                        {
                            idx = rnd.Next(0, this.AvailableChars.Length);
                            currentChar = (int)this.AvailableChars[idx];

                            if (CheckboxSequential.Checked)
                            {
                                while (previousChar - 1 == currentChar || previousChar + 1 == currentChar)
                                {
                                    idx = rnd.Next(0, this.AvailableChars.Length);
                                    currentChar = (int)this.AvailableChars[idx];
                                }
                            }
                            if (CheckboxDuplicate.Checked)
                            {
                                while (previousChar == currentChar)
                                {
                                    idx = rnd.Next(0, this.AvailableChars.Length);
                                    currentChar = (int)this.AvailableChars[idx];
                                }
                            }
                            previousChar = (int)this.AvailableChars[idx];
                            code += this.AvailableChars[idx];
                        }
                    }

                    if (TextboxPrefix.TextLength > 0)
                    {
                        code = TextboxPrefix.Text + code;
                    }

                    if (RadioButtonInline.Checked)
                    {
                        code = TextboxSeperator.Text + code;
                        this.Editor.LineEnd();
                        this.Editor.AddText(code.Length, code);
                        this.Editor.LineDown();
                    }
                    else
                    {
                        if (this.Editor.GetLineCount() > 0)
                        {
                            this.Editor.NewLine();
                            this.Editor.DelLineLeft();
                        }
                        this.Editor.AddText(code.Length, code);
                    }
                });

                if (!this.CheckboxCloseNoMessage.Checked) { MessageBox.Show("Strings are generated."); }
                
                if (this.CheckboxSaveOnClose.Checked) { SaveSettings(); }
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

        private void bLimitRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Removing the maximum limit could hang your system. Are you sure?", "Above and beyond", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                NumericUpDownQuantity.Maximum = Int32.MaxValue;
                label13.Text = "(min:1, max:--)";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            settings.Load(true);
            LoadSettings();
        }

        private void doRandom_CheckedChanged(object sender, EventArgs e)
        {
            NumericUpDownRandomMin.Enabled = CheckboxDoRandom.Checked;
            NumericUpDownRandomMax.Enabled = CheckboxDoRandom.Checked;
            NumericUpDownLength.Enabled = !CheckboxDoRandom.Checked; 
        }

        private void nudRandomMin_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NumericUpDownRandomMin.Value >= NumericUpDownRandomMax.Value)
            {
                toolTip2.Active = true;
                toolTip2.SetToolTip(NumericUpDownRandomMin, "Minimum length must be smaller than maximum length.");
                NumericUpDownRandomMin.ForeColor = Color.Red;
                e.Cancel = true;
            }
            else
            {
                toolTip1.Active = false;
                NumericUpDownRandomMin.ForeColor = Color.Black;
            }
        }

        private void nudRandomMax_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NumericUpDownRandomMax.Value <= NumericUpDownRandomMin.Value)
            {
                toolTip2.Active = true;
                toolTip2.SetToolTip(NumericUpDownRandomMax, "Maximum length must be bigger than minimum length.");
                NumericUpDownRandomMax.ForeColor = Color.Red;
                e.Cancel = true;
            }
            else
            {
                toolTip1.Active = false;
                NumericUpDownRandomMax.ForeColor = Color.Black;
            }
        }

        private void TriggerCheckBoxChangeEvent(System.Windows.Forms.CheckBox check)
        {
            // Find the event handler delegate for CheckedChanged event using reflection
            EventInfo checkedChangedEvent = check.GetType().GetEvent("CheckedChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            if (checkedChangedEvent != null)
            {
                Delegate eventHandler = (Delegate)check.GetType().GetField("EventClick", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(check);

                // Invoke the event handler delegate
                if (eventHandler != null)
                {
                    foreach (Delegate handler in eventHandler.GetInvocationList())
                    {
                        handler.Method.Invoke(handler.Target, new object[] { check, EventArgs.Empty });
                    }
                }
            }
        }

    }
}
