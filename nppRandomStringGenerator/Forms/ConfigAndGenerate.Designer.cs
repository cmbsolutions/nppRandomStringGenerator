namespace Kbg.NppPluginNET
{
    partial class ConfigAndGenerate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigAndGenerate));
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.chkNumbers = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkLowercase = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkUppercase = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkBeginLetter = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkSimilar = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkDuplicate = new System.Windows.Forms.CheckBox();
            this.txtSymbols = new System.Windows.Forms.TextBox();
            this.chkSymbols = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkSequential = new System.Windows.Forms.CheckBox();
            this.bGenerate = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.rbInline = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudLength
            // 
            this.nudLength.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudLength.Location = new System.Drawing.Point(198, 7);
            this.nudLength.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.nudLength.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(90, 23);
            this.nudLength.TabIndex = 0;
            this.nudLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLength.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "String length:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quantity:";
            // 
            // nudQuantity
            // 
            this.nudQuantity.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudQuantity.Location = new System.Drawing.Point(198, 36);
            this.nudQuantity.Maximum = new decimal(new int[] {
            10240,
            0,
            0,
            0});
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(90, 23);
            this.nudQuantity.TabIndex = 2;
            this.nudQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudQuantity.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudQuantity.ValueChanged += new System.EventHandler(this.nudQuantity_ValueChanged);
            // 
            // chkNumbers
            // 
            this.chkNumbers.AutoSize = true;
            this.chkNumbers.Checked = true;
            this.chkNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNumbers.Location = new System.Drawing.Point(198, 65);
            this.chkNumbers.Name = "chkNumbers";
            this.chkNumbers.Size = new System.Drawing.Size(98, 19);
            this.chkNumbers.TabIndex = 4;
            this.chkNumbers.Text = "( e.g. 123456 )";
            this.chkNumbers.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Include numbers:";
            // 
            // chkLowercase
            // 
            this.chkLowercase.AutoSize = true;
            this.chkLowercase.Checked = true;
            this.chkLowercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLowercase.Location = new System.Drawing.Point(198, 90);
            this.chkLowercase.Name = "chkLowercase";
            this.chkLowercase.Size = new System.Drawing.Size(112, 19);
            this.chkLowercase.TabIndex = 6;
            this.chkLowercase.Text = "( e.g. abcdefgh )";
            this.chkLowercase.UseVisualStyleBackColor = true;
            this.chkLowercase.CheckedChanged += new System.EventHandler(this.chkLowercase_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Include Lowercase Characters:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Include Uppercase Characters:";
            // 
            // chkUppercase
            // 
            this.chkUppercase.AutoSize = true;
            this.chkUppercase.Checked = true;
            this.chkUppercase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUppercase.Location = new System.Drawing.Point(198, 115);
            this.chkUppercase.Name = "chkUppercase";
            this.chkUppercase.Size = new System.Drawing.Size(122, 19);
            this.chkUppercase.TabIndex = 8;
            this.chkUppercase.Text = "( e.g. ABCDEFGH )";
            this.chkUppercase.UseVisualStyleBackColor = true;
            this.chkUppercase.CheckedChanged += new System.EventHandler(this.chkUppercase_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Begin With A Letter:";
            // 
            // chkBeginLetter
            // 
            this.chkBeginLetter.AutoSize = true;
            this.chkBeginLetter.Checked = true;
            this.chkBeginLetter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBeginLetter.Location = new System.Drawing.Point(198, 140);
            this.chkBeginLetter.Name = "chkBeginLetter";
            this.chkBeginLetter.Size = new System.Drawing.Size(237, 19);
            this.chkBeginLetter.TabIndex = 10;
            this.chkBeginLetter.Text = "( don\'t begin with a number or symbol )";
            this.chkBeginLetter.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "No Similar Characters:";
            // 
            // chkSimilar
            // 
            this.chkSimilar.AutoSize = true;
            this.chkSimilar.Checked = true;
            this.chkSimilar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSimilar.Location = new System.Drawing.Point(198, 190);
            this.chkSimilar.Name = "chkSimilar";
            this.chkSimilar.Size = new System.Drawing.Size(271, 19);
            this.chkSimilar.TabIndex = 12;
            this.chkSimilar.Text = "( don\'t use characters like i, l, 1, L, o, 0, O, etc. )";
            this.chkSimilar.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "No Duplicate Characters:";
            // 
            // chkDuplicate
            // 
            this.chkDuplicate.AutoSize = true;
            this.chkDuplicate.Enabled = false;
            this.chkDuplicate.Location = new System.Drawing.Point(198, 215);
            this.chkDuplicate.Name = "chkDuplicate";
            this.chkDuplicate.Size = new System.Drawing.Size(279, 19);
            this.chkDuplicate.TabIndex = 14;
            this.chkDuplicate.TabStop = false;
            this.chkDuplicate.Text = "( don\'t use the same character more than once )";
            this.chkDuplicate.UseVisualStyleBackColor = true;
            // 
            // txtSymbols
            // 
            this.txtSymbols.Location = new System.Drawing.Point(219, 162);
            this.txtSymbols.Name = "txtSymbols";
            this.txtSymbols.Size = new System.Drawing.Size(216, 23);
            this.txtSymbols.TabIndex = 16;
            this.txtSymbols.Text = "!\";#$%&\'()*+,-./:;<=>?@[]^_`{|}~";
            // 
            // chkSymbols
            // 
            this.chkSymbols.AutoSize = true;
            this.chkSymbols.Checked = true;
            this.chkSymbols.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSymbols.Location = new System.Drawing.Point(198, 165);
            this.chkSymbols.Name = "chkSymbols";
            this.chkSymbols.Size = new System.Drawing.Size(15, 14);
            this.chkSymbols.TabIndex = 17;
            this.chkSymbols.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 15);
            this.label9.TabIndex = 18;
            this.label9.Text = "Include Symbols:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 241);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 15);
            this.label10.TabIndex = 20;
            this.label10.Text = "No Sequential Characters:";
            // 
            // chkSequential
            // 
            this.chkSequential.AutoSize = true;
            this.chkSequential.Enabled = false;
            this.chkSequential.Location = new System.Drawing.Point(198, 240);
            this.chkSequential.Name = "chkSequential";
            this.chkSequential.Size = new System.Drawing.Size(274, 19);
            this.chkSequential.TabIndex = 19;
            this.chkSequential.TabStop = false;
            this.chkSequential.Text = "( don\'t use sequential characters, e.g. abc, 789 )";
            this.chkSequential.UseVisualStyleBackColor = true;
            // 
            // bGenerate
            // 
            this.bGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bGenerate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bGenerate.Location = new System.Drawing.Point(172, 293);
            this.bGenerate.Name = "bGenerate";
            this.bGenerate.Size = new System.Drawing.Size(163, 38);
            this.bGenerate.TabIndex = 23;
            this.bGenerate.Text = "Generate";
            this.bGenerate.UseVisualStyleBackColor = true;
            this.bGenerate.Click += new System.EventHandler(this.bGenerate_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 267);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 15);
            this.label12.TabIndex = 24;
            this.label12.Text = "Generate:";
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.Checked = true;
            this.rbNew.Location = new System.Drawing.Point(198, 264);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(69, 19);
            this.rbNew.TabIndex = 25;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "New tab";
            this.rbNew.UseVisualStyleBackColor = true;
            // 
            // rbInline
            // 
            this.rbInline.AutoSize = true;
            this.rbInline.Enabled = false;
            this.rbInline.Location = new System.Drawing.Point(281, 264);
            this.rbInline.Name = "rbInline";
            this.rbInline.Size = new System.Drawing.Size(54, 19);
            this.rbInline.TabIndex = 26;
            this.rbInline.Text = "Inline";
            this.rbInline.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.Orange;
            this.toolTip1.ForeColor = System.Drawing.Color.Black;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(295, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 15);
            this.label11.TabIndex = 27;
            this.label11.Text = "(min:8, max:128)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(295, 38);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 15);
            this.label13.TabIndex = 28;
            this.label13.Text = "(min:1, max:10240)";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 334);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(516, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 29;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel1.Text = "Progress:";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.Enabled = false;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(59, 20);
            this.toolStripSplitButton1.Text = "Cancel";
            this.toolStripSplitButton1.ToolTipText = "Cancel";
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // ConfigAndGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 356);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.rbNew);
            this.Controls.Add(this.rbInline);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.bGenerate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chkSequential);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkSymbols);
            this.Controls.Add(this.txtSymbols);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chkDuplicate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkSimilar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkBeginLetter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkUppercase);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkLowercase);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkNumbers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudQuantity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudLength);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigAndGenerate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate Passwords";
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.CheckBox chkNumbers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkLowercase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkUppercase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkBeginLetter;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkSimilar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkDuplicate;
        private System.Windows.Forms.TextBox txtSymbols;
        private System.Windows.Forms.CheckBox chkSymbols;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkSequential;
        private System.Windows.Forms.Button bGenerate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.RadioButton rbInline;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
    }
}