namespace OpenCloseApp
{
    partial class Form1
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
            this.buttonPublishOpenClose = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.checkBoxOpen = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPublishSetWatts = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownWatts = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWatts)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPublishOpenClose
            // 
            this.buttonPublishOpenClose.Location = new System.Drawing.Point(12, 63);
            this.buttonPublishOpenClose.Name = "buttonPublishOpenClose";
            this.buttonPublishOpenClose.Size = new System.Drawing.Size(75, 23);
            this.buttonPublishOpenClose.TabIndex = 0;
            this.buttonPublishOpenClose.Text = "Publish";
            this.buttonPublishOpenClose.UseVisualStyleBackColor = true;
            this.buttonPublishOpenClose.Click += new System.EventHandler(this.buttonPublishOpenClose_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(59, 21);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(191, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.Text = "battery1";
            // 
            // checkBoxOpen
            // 
            this.checkBoxOpen.AutoSize = true;
            this.checkBoxOpen.Location = new System.Drawing.Point(12, 29);
            this.checkBoxOpen.Name = "checkBoxOpen";
            this.checkBoxOpen.Size = new System.Drawing.Size(52, 17);
            this.checkBoxOpen.TabIndex = 2;
            this.checkBoxOpen.Text = "Open";
            this.checkBoxOpen.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxOpen);
            this.groupBox1.Controls.Add(this.buttonPublishOpenClose);
            this.groupBox1.Location = new System.Drawing.Point(18, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(98, 97);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OpenClose";
            // 
            // buttonPublishSetWatts
            // 
            this.buttonPublishSetWatts.Location = new System.Drawing.Point(9, 63);
            this.buttonPublishSetWatts.Name = "buttonPublishSetWatts";
            this.buttonPublishSetWatts.Size = new System.Drawing.Size(78, 22);
            this.buttonPublishSetWatts.TabIndex = 5;
            this.buttonPublishSetWatts.Text = "Publish";
            this.buttonPublishSetWatts.UseVisualStyleBackColor = true;
            this.buttonPublishSetWatts.Click += new System.EventHandler(this.buttonPublishSetWatts_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownWatts);
            this.groupBox2.Controls.Add(this.buttonPublishSetWatts);
            this.groupBox2.Location = new System.Drawing.Point(149, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(101, 96);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SetWatts";
            // 
            // numericUpDownWatts
            // 
            this.numericUpDownWatts.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownWatts.Location = new System.Drawing.Point(9, 29);
            this.numericUpDownWatts.Maximum = new decimal(new int[] {
            250000,
            0,
            0,
            0});
            this.numericUpDownWatts.Minimum = new decimal(new int[] {
            250000,
            0,
            0,
            -2147483648});
            this.numericUpDownWatts.Name = "numericUpDownWatts";
            this.numericUpDownWatts.Size = new System.Drawing.Size(78, 20);
            this.numericUpDownWatts.TabIndex = 6;
            this.numericUpDownWatts.Value = new decimal(new int[] {
            250000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 169);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxName);
            this.Name = "Form1";
            this.Text = "OpenCloseForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWatts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPublishOpenClose;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.CheckBox checkBoxOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPublishSetWatts;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownWatts;
        private System.Windows.Forms.Label label1;
    }
}

