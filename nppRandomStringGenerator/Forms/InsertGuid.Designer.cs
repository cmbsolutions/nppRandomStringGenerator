namespace nppRandomStringGenerator.Forms
{
    partial class InsertGuid
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioX = new System.Windows.Forms.RadioButton();
            this.radioP = new System.Windows.Forms.RadioButton();
            this.radioB = new System.Windows.Forms.RadioButton();
            this.radioD = new System.Windows.Forms.RadioButton();
            this.radioN = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radioUE = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 173);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioUE);
            this.groupBox1.Controls.Add(this.radioX);
            this.groupBox1.Controls.Add(this.radioP);
            this.groupBox1.Controls.Add(this.radioB);
            this.groupBox1.Controls.Add(this.radioD);
            this.groupBox1.Controls.Add(this.radioN);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 159);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Format";
            // 
            // radioX
            // 
            this.radioX.AutoSize = true;
            this.radioX.Location = new System.Drawing.Point(6, 113);
            this.radioX.Name = "radioX";
            this.radioX.Size = new System.Drawing.Size(392, 17);
            this.radioX.TabIndex = 4;
            this.radioX.TabStop = true;
            this.radioX.Text = "X: {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}";
            this.radioX.UseVisualStyleBackColor = true;
            // 
            // radioP
            // 
            this.radioP.AutoSize = true;
            this.radioP.Location = new System.Drawing.Point(6, 90);
            this.radioP.Name = "radioP";
            this.radioP.Size = new System.Drawing.Size(251, 17);
            this.radioP.TabIndex = 3;
            this.radioP.TabStop = true;
            this.radioP.Text = "P: (00000000-0000-0000-0000-000000000000)";
            this.radioP.UseVisualStyleBackColor = true;
            // 
            // radioB
            // 
            this.radioB.AutoSize = true;
            this.radioB.Location = new System.Drawing.Point(6, 67);
            this.radioB.Name = "radioB";
            this.radioB.Size = new System.Drawing.Size(251, 17);
            this.radioB.TabIndex = 2;
            this.radioB.TabStop = true;
            this.radioB.Text = "B: {00000000-0000-0000-0000-000000000000}";
            this.radioB.UseVisualStyleBackColor = true;
            // 
            // radioD
            // 
            this.radioD.AutoSize = true;
            this.radioD.Location = new System.Drawing.Point(6, 44);
            this.radioD.Name = "radioD";
            this.radioD.Size = new System.Drawing.Size(247, 17);
            this.radioD.TabIndex = 1;
            this.radioD.TabStop = true;
            this.radioD.Text = "D: 00000000-0000-0000-0000-000000000000";
            this.radioD.UseVisualStyleBackColor = true;
            // 
            // radioN
            // 
            this.radioN.AutoSize = true;
            this.radioN.Location = new System.Drawing.Point(6, 21);
            this.radioN.Name = "radioN";
            this.radioN.Size = new System.Drawing.Size(231, 17);
            this.radioN.TabIndex = 0;
            this.radioN.TabStop = true;
            this.radioN.Text = "N: 00000000000000000000000000000000";
            this.radioN.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 173);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 29);
            this.panel2.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(348, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(267, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // radioUE
            // 
            this.radioUE.AutoSize = true;
            this.radioUE.Location = new System.Drawing.Point(6, 136);
            this.radioUE.Name = "radioUE";
            this.radioUE.Size = new System.Drawing.Size(262, 17);
            this.radioUE.TabIndex = 5;
            this.radioUE.TabStop = true;
            this.radioUE.Text = "UE: (ID=00000000000000000000000000000000)";
            this.radioUE.UseVisualStyleBackColor = true;
            // 
            // InsertGuid
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(426, 202);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(442, 241);
            this.MinimumSize = new System.Drawing.Size(442, 241);
            this.Name = "InsertGuid";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Insert Guid";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioX;
        private System.Windows.Forms.RadioButton radioP;
        private System.Windows.Forms.RadioButton radioB;
        private System.Windows.Forms.RadioButton radioD;
        private System.Windows.Forms.RadioButton radioN;
        private System.Windows.Forms.RadioButton radioUE;
    }
}