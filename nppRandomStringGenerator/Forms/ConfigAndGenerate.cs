using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;
using nppRandomStringGenerator.Modules;
using nppRandomStringGenerator.Storage;

namespace Kbg.NppPluginNET
{
    public partial class ConfigAndGenerate : Form
    {
        private string AvailableChars;
        private string StartChars;
        private IScintillaGateway Editor;
        private INotepadPPGateway Notepad;

        private StringGenerator Generator;

        private readonly List<KeyValuePair<string, string>> GuidInfos = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("N", "32 digits:\r\n\r\nExample: 00000000000000000000000000000000"),
            new KeyValuePair<string, string>("D", "32 digits separated by hyphens:\r\n\r\nExample: 00000000-0000-0000-0000-000000000000"),
            new KeyValuePair<string, string>("B", "32 digits separated by hyphens, enclosed in braces:\r\n\r\nExample: {00000000-0000-0000-0000-000000000000}"),
            new KeyValuePair<string, string>("P", "32 digits separated by hyphens, enclosed in parentheses:\r\n\r\nExample: (00000000-0000-0000-0000-000000000000)"),
            new KeyValuePair<string, string>("X", "Four hexadecimal values enclosed in braces, where the fourth value is a subset of eight hexadecimal values that is also enclosed in braces:\r\n\r\nExample: {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}")
        };

        
        
        public Settings settings { get; set; }


        public ConfigAndGenerate()
        {
            InitializeComponent();
            this.Editor = new ScintillaGateway(PluginBase.GetCurrentScintilla());
            this.Notepad = new NotepadPPGateway();
            ButtonCancel.Enabled = false;
            numericUpDownCores.Maximum = Environment.ProcessorCount;
            numericUpDownCores.Value = (int)(Environment.ProcessorCount / 2);
            CheckboxDarkMode.Visible = false;
            TabControl1.TabPages.Remove(tabPageAdvanced);
        }

        public void LoadSettings()
        {
            foreach (nppRandomStringGenerator.Storage.Models.ConfigItem configitem in settings.settings.ConfigItems)
            {
                if (configitem == null || configitem.Name.StartsWith("Quick")) { continue; }   

                Control ctrl = this.Controls.Find(configitem.Name, true).FirstOrDefault();

                if (ctrl != null && ctrl.Name.StartsWith("NumericUpDown"))
                {
                    NumericUpDown nupdown = ctrl as NumericUpDown;
                    nupdown.Value = Math.Min(Convert.ToDecimal(configitem.Value), nupdown.Maximum);
                }
                if (ctrl != null && ctrl.Name.StartsWith("Checkbox"))
                {
                    System.Windows.Forms.CheckBox check = ctrl as System.Windows.Forms.CheckBox;
                    check.Checked = Convert.ToBoolean(configitem.Value);
                    TriggerCheckBoxChangeEvent(check);
                }
                if (ctrl != null && ctrl.Name.StartsWith("Textbox"))
                {
                    TextBox txt = ctrl as TextBox;
                    txt.Text = configitem.Value;
                }
                if (ctrl != null && ctrl.Name.StartsWith("Radio"))
                {
                    System.Windows.Forms.RadioButton radio = ctrl as System.Windows.Forms.RadioButton;
                    radio.Checked = Convert.ToBoolean(configitem.Value);
                }
                if (ctrl != null && ctrl.Name.StartsWith("TabControl"))
                {
                    System.Windows.Forms.TabControl tab = ctrl as System.Windows.Forms.TabControl;
                    tab.SelectedTab = tab.TabPages[Convert.ToInt32(configitem.Value)];
                }
                if (ctrl != null && ctrl.Name.StartsWith("ComboBox"))
                {
                    ComboBox combo = ctrl as ComboBox;
                    combo.Text = configitem.Value;
                }
            }
        }

        private void SaveSettings()
        {
            foreach (nppRandomStringGenerator.Storage.Models.ConfigItem configitem in settings.settings.ConfigItems)
            {
                if (configitem.Name.StartsWith("Quick")) { continue; }

                Control ctrl = this.Controls.Find(configitem.Name, true).FirstOrDefault();

                if (ctrl != null && ctrl.Name.StartsWith("NumericUpDown"))
                {
                    NumericUpDown nupdown = ctrl as NumericUpDown;
                    configitem.Value = nupdown.Value.ToString();
                }
                if (ctrl != null && ctrl.Name.StartsWith("Checkbox"))
                {
                    System.Windows.Forms.CheckBox check = ctrl as System.Windows.Forms.CheckBox;
                    configitem.Value = (check.Checked ? "true" : "false");
                }
                if (ctrl != null && ctrl.Name.StartsWith("Textbox"))
                {
                    TextBox txt = ctrl as TextBox;
                    configitem.Value = txt.Text;
                }
                if (ctrl != null && ctrl.Name.StartsWith("Radio"))
                {
                    System.Windows.Forms.RadioButton radio = ctrl as System.Windows.Forms.RadioButton;
                    configitem.Value = (radio.Checked ? "true" : "false");
                }
                if (ctrl != null && ctrl.Name.StartsWith("TabControl"))
                {
                    System.Windows.Forms.TabControl tab = ctrl as System.Windows.Forms.TabControl;
                    configitem.Value = tab.SelectedIndex.ToString();
                }
                if (ctrl != null && ctrl.Name.StartsWith("ComboBox"))
                {
                    ComboBox combo = ctrl as ComboBox;
                    configitem.Value = combo.Text;
                }
            }

            settings.Save();
        }

        private async void ButtonGenerate_Click(Object sender, EventArgs e)
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

            if (this.AvailableChars.Length > 0 || TabControl1.SelectedTab == TabPageGUID)
            {
                if (RadioButtonNew.Checked) this.Notepad.FileNew();
                if (RadioButtonCurrent.Checked) this.Editor.DocumentEnd();
                if (RadioButtonInline.Checked) this.Editor.DocumentStart();

                Generator = new StringGenerator
                {
                    Editor = this.Editor,
                    Notepad = this.Notepad,
                    AvailableCharacters = this.AvailableChars,
                    StartCharacters = this.StartChars,
                    IsInline = RadioButtonInline.Checked,
                    IsDuplicate = CheckboxDuplicate.Checked,
                    IsSequential = CheckboxSequential.Checked,
                    Prefix = TextboxPrefix.Text,
                    Suffix = TextboxSuffix.Text,
                    RandomMinimumLength = (int)NumericUpDownRandomMin.Value,
                    RandomMaximumLength = (int)NumericUpDownRandomMax.Value,
                    DoRandom = CheckboxDoRandom.Checked,
                    StringLength = (int)NumericUpDownLength.Value,
                    TextSeperator = TextboxSeperator.Text,
                    UseStartCharacters = CheckboxBeginLetter.Checked,
                    StringQuantity = (int)NumericUpDownQuantity.Value,
                    DoGuids = TabControl1.SelectedTab == TabPageGUID,
                    GuidFormat = ComboBoxGUIDFormat.Text,
                    GuidQuantity = (int)NumericUpDownGUIDQuantity.Value,
                    Cores = (int)numericUpDownCores.Value
                };

                ButtonGenerate.Enabled = false;
                //if (!RadioButtonInline.Checked)
                ButtonCancel.Enabled = true;

                await Task.Run(() => Generator.GenerateStrings());

                ButtonCancel.Enabled = false;
                ButtonGenerate.Enabled = true;

                if (!this.CheckboxCloseNoMessage.Checked) {
                    if (Generator.IsCancelled)
                    {
                        MessageBox.Show($"The generation was cancelled after {Generator.ProcessTime.TotalSeconds} seconds.");
                    }
                    else
                    {
                        MessageBox.Show($"Strings are generated in {Generator.ProcessTime.TotalSeconds} seconds.");
                    }
                }
                
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
            if ( NumericUpDownQuantity.Value > 1024000)
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

                NumericUpDownGUIDQuantity.Enabled = false;
                NumericUpDownGUIDQuantity.Maximum = this.Editor.GetLineCount();
                NumericUpDownGUIDQuantity.Value = this.Editor.GetLineCount();
            }
            else
            {
                NumericUpDownQuantity.Maximum = 4096000;
                NumericUpDownQuantity.Enabled = true;
                TextboxSeperator.Enabled = false;

                NumericUpDownGUIDQuantity.Maximum = 4096000;
                NumericUpDownGUIDQuantity.Enabled = true;
            }

        }

        private void CheckboxBeginLetter_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckboxLowercase.Checked && !CheckboxUppercase.Checked) CheckboxBeginLetter.Checked = false;
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

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (Generator != null)
            {
                if (!Generator.CancelJob.IsCancellationRequested)
                {
                    Generator.CancelJob.Cancel();
                }
            }
        }

        private void ComboBoxGUIDFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            label20.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label20.Text = GuidInfos.FirstOrDefault(c => c.Key == ComboBoxGUIDFormat.Text).Value;
            label20.Visible = true;
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            TextboxSymbols.Text = "!@#$%^&*()_+-=[]{};':,.<>?";
        }
    }
}
