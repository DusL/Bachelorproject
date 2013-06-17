namespace CLESMonitor.View
{
    partial class FuzzyModelUtilityView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hrSensorTypeRadioButton1 = new System.Windows.Forms.RadioButton();
            this.hrSensorTypeRadioButton2 = new System.Windows.Forms.RadioButton();
            this.hrValueLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hrTrackbar = new System.Windows.Forms.TrackBar();
            this.hrMinusButton = new System.Windows.Forms.Button();
            this.hrPlusButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gsrValueLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gsrTrackBar = new System.Windows.Forms.TrackBar();
            this.gsrMinusButton = new System.Windows.Forms.Button();
            this.gsrPlusButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hrTrackbar)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gsrTrackBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.hrSensorTypeRadioButton1);
            this.groupBox1.Controls.Add(this.hrSensorTypeRadioButton2);
            this.groupBox1.Controls.Add(this.hrValueLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.hrTrackbar);
            this.groupBox1.Controls.Add(this.hrMinusButton);
            this.groupBox1.Controls.Add(this.hrPlusButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 113);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hartslagmeter";
            // 
            // hrSensorTypeRadioButton1
            // 
            this.hrSensorTypeRadioButton1.AutoSize = true;
            this.hrSensorTypeRadioButton1.Checked = true;
            this.hrSensorTypeRadioButton1.Location = new System.Drawing.Point(6, 20);
            this.hrSensorTypeRadioButton1.Name = "hrSensorTypeRadioButton1";
            this.hrSensorTypeRadioButton1.Size = new System.Drawing.Size(76, 17);
            this.hrSensorTypeRadioButton1.TabIndex = 22;
            this.hrSensorTypeRadioButton1.TabStop = true;
            this.hrSensorTypeRadioButton1.Text = "Handmatig";
            this.hrSensorTypeRadioButton1.UseVisualStyleBackColor = true;
            this.hrSensorTypeRadioButton1.CheckedChanged += new System.EventHandler(this.hrSensorTypeRadioButton1_CheckedChanged);
            // 
            // hrSensorTypeRadioButton2
            // 
            this.hrSensorTypeRadioButton2.AutoSize = true;
            this.hrSensorTypeRadioButton2.Location = new System.Drawing.Point(88, 20);
            this.hrSensorTypeRadioButton2.Name = "hrSensorTypeRadioButton2";
            this.hrSensorTypeRadioButton2.Size = new System.Drawing.Size(109, 17);
            this.hrSensorTypeRadioButton2.TabIndex = 21;
            this.hrSensorTypeRadioButton2.Text = "Zephyr BT sensor";
            this.hrSensorTypeRadioButton2.UseVisualStyleBackColor = true;
            this.hrSensorTypeRadioButton2.CheckedChanged += new System.EventHandler(this.hrSensorTypeRadioButton2_CheckedChanged);
            // 
            // hrValueLabel
            // 
            this.hrValueLabel.AutoSize = true;
            this.hrValueLabel.Location = new System.Drawing.Point(178, 91);
            this.hrValueLabel.Name = "hrValueLabel";
            this.hrValueLabel.Size = new System.Drawing.Size(13, 13);
            this.hrValueLabel.TabIndex = 20;
            this.hrValueLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Gemeten hartslag (slagen/minuut)";
            // 
            // hrTrackbar
            // 
            this.hrTrackbar.LargeChange = 10;
            this.hrTrackbar.Location = new System.Drawing.Point(6, 43);
            this.hrTrackbar.Maximum = 150;
            this.hrTrackbar.Minimum = 30;
            this.hrTrackbar.Name = "hrTrackbar";
            this.hrTrackbar.Size = new System.Drawing.Size(266, 45);
            this.hrTrackbar.TabIndex = 16;
            this.hrTrackbar.TickFrequency = 10;
            this.hrTrackbar.Value = 70;
            this.hrTrackbar.Scroll += new System.EventHandler(this.hrTrackbar_Scroll);
            // 
            // hrMinusButton
            // 
            this.hrMinusButton.Location = new System.Drawing.Point(278, 43);
            this.hrMinusButton.Name = "hrMinusButton";
            this.hrMinusButton.Size = new System.Drawing.Size(30, 30);
            this.hrMinusButton.TabIndex = 18;
            this.hrMinusButton.Text = "-";
            this.hrMinusButton.UseVisualStyleBackColor = true;
            this.hrMinusButton.Click += new System.EventHandler(this.hrMinusButton_Click);
            // 
            // hrPlusButton
            // 
            this.hrPlusButton.Location = new System.Drawing.Point(314, 43);
            this.hrPlusButton.Name = "hrPlusButton";
            this.hrPlusButton.Size = new System.Drawing.Size(30, 30);
            this.hrPlusButton.TabIndex = 17;
            this.hrPlusButton.Text = "+";
            this.hrPlusButton.UseVisualStyleBackColor = true;
            this.hrPlusButton.Click += new System.EventHandler(this.hrPlusButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.gsrValueLabel);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.gsrTrackBar);
            this.groupBox3.Controls.Add(this.gsrMinusButton);
            this.groupBox3.Controls.Add(this.gsrPlusButton);
            this.groupBox3.Location = new System.Drawing.Point(6, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(375, 78);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zweetmeter";
            // 
            // gsrValueLabel
            // 
            this.gsrValueLabel.AutoSize = true;
            this.gsrValueLabel.Location = new System.Drawing.Point(178, 51);
            this.gsrValueLabel.Name = "gsrValueLabel";
            this.gsrValueLabel.Size = new System.Drawing.Size(13, 13);
            this.gsrValueLabel.TabIndex = 20;
            this.gsrValueLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Gemeten huidgeleiding (siemens)";
            // 
            // gsrTrackBar
            // 
            this.gsrTrackBar.LargeChange = 10;
            this.gsrTrackBar.Location = new System.Drawing.Point(6, 19);
            this.gsrTrackBar.Maximum = 100;
            this.gsrTrackBar.Name = "gsrTrackBar";
            this.gsrTrackBar.Size = new System.Drawing.Size(266, 45);
            this.gsrTrackBar.TabIndex = 16;
            this.gsrTrackBar.TickFrequency = 10;
            this.gsrTrackBar.Value = 50;
            this.gsrTrackBar.Scroll += new System.EventHandler(this.gsrTrackBar_Scroll);
            // 
            // gsrMinusButton
            // 
            this.gsrMinusButton.Location = new System.Drawing.Point(278, 19);
            this.gsrMinusButton.Name = "gsrMinusButton";
            this.gsrMinusButton.Size = new System.Drawing.Size(30, 30);
            this.gsrMinusButton.TabIndex = 18;
            this.gsrMinusButton.Text = "-";
            this.gsrMinusButton.UseVisualStyleBackColor = true;
            this.gsrMinusButton.Click += new System.EventHandler(this.gsrMinusButton_Click);
            // 
            // gsrPlusButton
            // 
            this.gsrPlusButton.Location = new System.Drawing.Point(314, 19);
            this.gsrPlusButton.Name = "gsrPlusButton";
            this.gsrPlusButton.Size = new System.Drawing.Size(30, 30);
            this.gsrPlusButton.TabIndex = 17;
            this.gsrPlusButton.Text = "+";
            this.gsrPlusButton.UseVisualStyleBackColor = true;
            this.gsrPlusButton.Click += new System.EventHandler(this.gsrPlusButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 294);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sensoren";
            // 
            // FuzzyModelUtilityView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 294);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FuzzyModelUtilityView";
            this.Text = "FuzzyModelUtilityView";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hrTrackbar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gsrTrackBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton hrSensorTypeRadioButton1;
        private System.Windows.Forms.RadioButton hrSensorTypeRadioButton2;
        public System.Windows.Forms.Label hrValueLabel;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TrackBar hrTrackbar;
        public System.Windows.Forms.Button hrMinusButton;
        public System.Windows.Forms.Button hrPlusButton;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Label gsrValueLabel;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TrackBar gsrTrackBar;
        private System.Windows.Forms.Button gsrMinusButton;
        private System.Windows.Forms.Button gsrPlusButton;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}