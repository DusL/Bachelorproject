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
            this.HRSensorComboBox = new System.Windows.Forms.ComboBox();
            this.hrSensorTypeRadioButton1 = new System.Windows.Forms.RadioButton();
            this.hrSensorTypeRadioButton2 = new System.Windows.Forms.RadioButton();
            this.hrValueLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hrTrackBar = new System.Windows.Forms.TrackBar();
            this.hrMinusButton = new System.Windows.Forms.Button();
            this.hrPlusButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gsrValueLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gsrTrackBar = new System.Windows.Forms.TrackBar();
            this.gsrMinusButton = new System.Windows.Forms.Button();
            this.gsrPlusButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sdLabel = new System.Windows.Forms.Label();
            this.meanLabel = new System.Windows.Forms.Label();
            this.maxLabel = new System.Windows.Forms.Label();
            this.minLabel = new System.Windows.Forms.Label();
            this.gsrSDLabel = new System.Windows.Forms.Label();
            this.hrSDLabel = new System.Windows.Forms.Label();
            this.gsrMeanLabel = new System.Windows.Forms.Label();
            this.hrMeanLabel = new System.Windows.Forms.Label();
            this.hrMaxLabel = new System.Windows.Forms.Label();
            this.gsrMaxLabel = new System.Windows.Forms.Label();
            this.gsrMinLabel = new System.Windows.Forms.Label();
            this.hrMinLabel = new System.Windows.Forms.Label();
            this.hrCalLabel = new System.Windows.Forms.Label();
            this.gsrCalLabel = new System.Windows.Forms.Label();
            this.gsrLevelLabel = new System.Windows.Forms.Label();
            this.hrLevelLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HRLabel = new System.Windows.Forms.Label();
            this.sensorButton = new System.Windows.Forms.Button();
            this.calibrateButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hrTrackBar)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gsrTrackBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.HRSensorComboBox);
            this.groupBox1.Controls.Add(this.hrSensorTypeRadioButton1);
            this.groupBox1.Controls.Add(this.hrSensorTypeRadioButton2);
            this.groupBox1.Controls.Add(this.hrValueLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.hrTrackBar);
            this.groupBox1.Controls.Add(this.hrMinusButton);
            this.groupBox1.Controls.Add(this.hrPlusButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 114);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hartslagmeter";
            // 
            // HRSensorComboBox
            // 
            this.HRSensorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HRSensorComboBox.Enabled = false;
            this.HRSensorComboBox.FormattingEnabled = true;
            this.HRSensorComboBox.Items.AddRange(new object[] {
            "Kies een COM poort.."});
            this.HRSensorComboBox.Location = new System.Drawing.Point(204, 20);
            this.HRSensorComboBox.Name = "HRSensorComboBox";
            this.HRSensorComboBox.Size = new System.Drawing.Size(140, 21);
            this.HRSensorComboBox.TabIndex = 23;
            this.HRSensorComboBox.SelectedIndexChanged += new System.EventHandler(this.HRSensorComboBox_SelectedIndexChanged);
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
            this.hrValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hrValueLabel.Location = new System.Drawing.Point(178, 89);
            this.hrValueLabel.Name = "hrValueLabel";
            this.hrValueLabel.Size = new System.Drawing.Size(15, 15);
            this.hrValueLabel.TabIndex = 20;
            this.hrValueLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Huidige hartslag (slagen/minuut)\r\n";
            // 
            // hrTrackBar
            // 
            this.hrTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hrTrackBar.Location = new System.Drawing.Point(9, 47);
            this.hrTrackBar.Maximum = 150;
            this.hrTrackBar.Minimum = 30;
            this.hrTrackBar.Name = "hrTrackBar";
            this.hrTrackBar.Size = new System.Drawing.Size(288, 45);
            this.hrTrackBar.TabIndex = 16;
            this.hrTrackBar.TickFrequency = 5;
            this.hrTrackBar.Value = 70;
            this.hrTrackBar.Scroll += new System.EventHandler(this.hrTrackbar_Scroll);
            // 
            // hrMinusButton
            // 
            this.hrMinusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hrMinusButton.Location = new System.Drawing.Point(303, 47);
            this.hrMinusButton.Name = "hrMinusButton";
            this.hrMinusButton.Size = new System.Drawing.Size(30, 30);
            this.hrMinusButton.TabIndex = 18;
            this.hrMinusButton.Text = "-";
            this.hrMinusButton.UseVisualStyleBackColor = true;
            this.hrMinusButton.Click += new System.EventHandler(this.hrMinusButton_Click);
            // 
            // hrPlusButton
            // 
            this.hrPlusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hrPlusButton.Location = new System.Drawing.Point(339, 47);
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
            this.groupBox3.Location = new System.Drawing.Point(6, 210);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(375, 78);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zweetmeter";
            // 
            // gsrValueLabel
            // 
            this.gsrValueLabel.AutoSize = true;
            this.gsrValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gsrValueLabel.Location = new System.Drawing.Point(178, 49);
            this.gsrValueLabel.Name = "gsrValueLabel";
            this.gsrValueLabel.Size = new System.Drawing.Size(15, 15);
            this.gsrValueLabel.TabIndex = 20;
            this.gsrValueLabel.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Huidige huidgeleiding (siemens)";
            // 
            // gsrTrackBar
            // 
            this.gsrTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gsrTrackBar.Location = new System.Drawing.Point(9, 19);
            this.gsrTrackBar.Maximum = 100;
            this.gsrTrackBar.Name = "gsrTrackBar";
            this.gsrTrackBar.Size = new System.Drawing.Size(288, 45);
            this.gsrTrackBar.TabIndex = 16;
            this.gsrTrackBar.TickFrequency = 5;
            this.gsrTrackBar.Value = 50;
            this.gsrTrackBar.Scroll += new System.EventHandler(this.gsrTrackBar_Scroll);
            // 
            // gsrMinusButton
            // 
            this.gsrMinusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gsrMinusButton.Location = new System.Drawing.Point(303, 19);
            this.gsrMinusButton.Name = "gsrMinusButton";
            this.gsrMinusButton.Size = new System.Drawing.Size(30, 30);
            this.gsrMinusButton.TabIndex = 18;
            this.gsrMinusButton.Text = "-";
            this.gsrMinusButton.UseVisualStyleBackColor = true;
            this.gsrMinusButton.Click += new System.EventHandler(this.gsrMinusButton_Click);
            // 
            // gsrPlusButton
            // 
            this.gsrPlusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gsrPlusButton.Location = new System.Drawing.Point(339, 19);
            this.gsrPlusButton.Name = "gsrPlusButton";
            this.gsrPlusButton.Size = new System.Drawing.Size(30, 30);
            this.gsrPlusButton.TabIndex = 17;
            this.gsrPlusButton.Text = "+";
            this.gsrPlusButton.UseVisualStyleBackColor = true;
            this.gsrPlusButton.Click += new System.EventHandler(this.gsrPlusButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sdLabel);
            this.groupBox2.Controls.Add(this.meanLabel);
            this.groupBox2.Controls.Add(this.maxLabel);
            this.groupBox2.Controls.Add(this.minLabel);
            this.groupBox2.Controls.Add(this.gsrSDLabel);
            this.groupBox2.Controls.Add(this.hrSDLabel);
            this.groupBox2.Controls.Add(this.gsrMeanLabel);
            this.groupBox2.Controls.Add(this.hrMeanLabel);
            this.groupBox2.Controls.Add(this.hrMaxLabel);
            this.groupBox2.Controls.Add(this.gsrMaxLabel);
            this.groupBox2.Controls.Add(this.gsrMinLabel);
            this.groupBox2.Controls.Add(this.hrMinLabel);
            this.groupBox2.Controls.Add(this.hrCalLabel);
            this.groupBox2.Controls.Add(this.gsrCalLabel);
            this.groupBox2.Controls.Add(this.gsrLevelLabel);
            this.groupBox2.Controls.Add(this.hrLevelLabel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.HRLabel);
            this.groupBox2.Controls.Add(this.sensorButton);
            this.groupBox2.Controls.Add(this.calibrateButton);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 345);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Emotionele staat";
            // 
            // sdLabel
            // 
            this.sdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sdLabel.AutoSize = true;
            this.sdLabel.Location = new System.Drawing.Point(198, 33);
            this.sdLabel.Name = "sdLabel";
            this.sdLabel.Size = new System.Drawing.Size(22, 13);
            this.sdLabel.TabIndex = 51;
            this.sdLabel.Text = "SD";
            // 
            // meanLabel
            // 
            this.meanLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.meanLabel.AutoSize = true;
            this.meanLabel.Location = new System.Drawing.Point(158, 33);
            this.meanLabel.Name = "meanLabel";
            this.meanLabel.Size = new System.Drawing.Size(34, 13);
            this.meanLabel.TabIndex = 50;
            this.meanLabel.Text = "Mean";
            // 
            // maxLabel
            // 
            this.maxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maxLabel.AutoSize = true;
            this.maxLabel.Location = new System.Drawing.Point(121, 33);
            this.maxLabel.Name = "maxLabel";
            this.maxLabel.Size = new System.Drawing.Size(27, 13);
            this.maxLabel.TabIndex = 49;
            this.maxLabel.Text = "Max";
            // 
            // minLabel
            // 
            this.minLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.minLabel.AutoSize = true;
            this.minLabel.Location = new System.Drawing.Point(91, 33);
            this.minLabel.Name = "minLabel";
            this.minLabel.Size = new System.Drawing.Size(24, 13);
            this.minLabel.TabIndex = 48;
            this.minLabel.Text = "Min";
            // 
            // gsrSDLabel
            // 
            this.gsrSDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gsrSDLabel.AutoSize = true;
            this.gsrSDLabel.Location = new System.Drawing.Point(198, 74);
            this.gsrSDLabel.Name = "gsrSDLabel";
            this.gsrSDLabel.Size = new System.Drawing.Size(22, 13);
            this.gsrSDLabel.TabIndex = 47;
            this.gsrSDLabel.Text = "SD";
            // 
            // hrSDLabel
            // 
            this.hrSDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hrSDLabel.AutoSize = true;
            this.hrSDLabel.Location = new System.Drawing.Point(198, 52);
            this.hrSDLabel.Name = "hrSDLabel";
            this.hrSDLabel.Size = new System.Drawing.Size(22, 13);
            this.hrSDLabel.TabIndex = 46;
            this.hrSDLabel.Text = "SD";
            // 
            // gsrMeanLabel
            // 
            this.gsrMeanLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gsrMeanLabel.AutoSize = true;
            this.gsrMeanLabel.Location = new System.Drawing.Point(158, 74);
            this.gsrMeanLabel.Name = "gsrMeanLabel";
            this.gsrMeanLabel.Size = new System.Drawing.Size(34, 13);
            this.gsrMeanLabel.TabIndex = 45;
            this.gsrMeanLabel.Text = "Mean";
            // 
            // hrMeanLabel
            // 
            this.hrMeanLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hrMeanLabel.AutoSize = true;
            this.hrMeanLabel.Location = new System.Drawing.Point(158, 52);
            this.hrMeanLabel.Name = "hrMeanLabel";
            this.hrMeanLabel.Size = new System.Drawing.Size(34, 13);
            this.hrMeanLabel.TabIndex = 44;
            this.hrMeanLabel.Text = "Mean";
            // 
            // hrMaxLabel
            // 
            this.hrMaxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hrMaxLabel.AutoSize = true;
            this.hrMaxLabel.Location = new System.Drawing.Point(121, 52);
            this.hrMaxLabel.Name = "hrMaxLabel";
            this.hrMaxLabel.Size = new System.Drawing.Size(27, 13);
            this.hrMaxLabel.TabIndex = 43;
            this.hrMaxLabel.Text = "Max";
            // 
            // gsrMaxLabel
            // 
            this.gsrMaxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gsrMaxLabel.AutoSize = true;
            this.gsrMaxLabel.Location = new System.Drawing.Point(121, 74);
            this.gsrMaxLabel.Name = "gsrMaxLabel";
            this.gsrMaxLabel.Size = new System.Drawing.Size(27, 13);
            this.gsrMaxLabel.TabIndex = 42;
            this.gsrMaxLabel.Text = "Max";
            // 
            // gsrMinLabel
            // 
            this.gsrMinLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gsrMinLabel.AutoSize = true;
            this.gsrMinLabel.Location = new System.Drawing.Point(91, 74);
            this.gsrMinLabel.Name = "gsrMinLabel";
            this.gsrMinLabel.Size = new System.Drawing.Size(24, 13);
            this.gsrMinLabel.TabIndex = 41;
            this.gsrMinLabel.Text = "Min";
            // 
            // hrMinLabel
            // 
            this.hrMinLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hrMinLabel.AutoSize = true;
            this.hrMinLabel.Location = new System.Drawing.Point(91, 52);
            this.hrMinLabel.Name = "hrMinLabel";
            this.hrMinLabel.Size = new System.Drawing.Size(24, 13);
            this.hrMinLabel.TabIndex = 40;
            this.hrMinLabel.Text = "Min";
            // 
            // hrCalLabel
            // 
            this.hrCalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hrCalLabel.AutoSize = true;
            this.hrCalLabel.Location = new System.Drawing.Point(14, 52);
            this.hrCalLabel.Name = "hrCalLabel";
            this.hrCalLabel.Size = new System.Drawing.Size(46, 13);
            this.hrCalLabel.TabIndex = 35;
            this.hrCalLabel.Text = "Hartslag";
            // 
            // gsrCalLabel
            // 
            this.gsrCalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gsrCalLabel.AutoSize = true;
            this.gsrCalLabel.Location = new System.Drawing.Point(14, 74);
            this.gsrCalLabel.Name = "gsrCalLabel";
            this.gsrCalLabel.Size = new System.Drawing.Size(74, 13);
            this.gsrCalLabel.TabIndex = 34;
            this.gsrCalLabel.Text = "Huidgeleiding:";
            // 
            // gsrLevelLabel
            // 
            this.gsrLevelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gsrLevelLabel.AutoSize = true;
            this.gsrLevelLabel.Location = new System.Drawing.Point(317, 74);
            this.gsrLevelLabel.Name = "gsrLevelLabel";
            this.gsrLevelLabel.Size = new System.Drawing.Size(53, 13);
            this.gsrLevelLabel.TabIndex = 31;
            this.gsrLevelLabel.Text = "Unknown";
            // 
            // hrLevelLabel
            // 
            this.hrLevelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hrLevelLabel.AutoSize = true;
            this.hrLevelLabel.Location = new System.Drawing.Point(317, 52);
            this.hrLevelLabel.Name = "hrLevelLabel";
            this.hrLevelLabel.Size = new System.Drawing.Size(53, 13);
            this.hrLevelLabel.TabIndex = 30;
            this.hrLevelLabel.Text = "Unknown";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Huidgeleiding";
            // 
            // HRLabel
            // 
            this.HRLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HRLabel.AutoSize = true;
            this.HRLabel.Location = new System.Drawing.Point(263, 52);
            this.HRLabel.Name = "HRLabel";
            this.HRLabel.Size = new System.Drawing.Size(46, 13);
            this.HRLabel.TabIndex = 28;
            this.HRLabel.Text = "Hartslag";
            // 
            // sensorButton
            // 
            this.sensorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sensorButton.Location = new System.Drawing.Point(261, 305);
            this.sensorButton.Name = "sensorButton";
            this.sensorButton.Size = new System.Drawing.Size(120, 40);
            this.sensorButton.TabIndex = 25;
            this.sensorButton.Text = "Bekijk sensor input";
            this.sensorButton.UseVisualStyleBackColor = true;
            this.sensorButton.Click += new System.EventHandler(this.sensorButton_Click);
            // 
            // calibrateButton
            // 
            this.calibrateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.calibrateButton.Location = new System.Drawing.Point(6, 305);
            this.calibrateButton.Name = "calibrateButton";
            this.calibrateButton.Size = new System.Drawing.Size(120, 40);
            this.calibrateButton.TabIndex = 24;
            this.calibrateButton.Text = "Kalibreren";
            this.calibrateButton.UseVisualStyleBackColor = true;
            this.calibrateButton.Click += new System.EventHandler(this.calibrateButton_Click);
            // 
            // FuzzyModelUtilityView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(387, 345);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FuzzyModelUtilityView";
            this.Text = "FuzzyModelUtilityView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FuzzyModelUtilityView_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hrTrackBar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gsrTrackBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label hrValueLabel;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TrackBar hrTrackBar;
        public System.Windows.Forms.Button hrMinusButton;
        public System.Windows.Forms.Button hrPlusButton;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Label gsrValueLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button sensorButton;
        public System.Windows.Forms.Button calibrateButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label HRLabel;
        public System.Windows.Forms.Label gsrLevelLabel;
        public System.Windows.Forms.Label hrLevelLabel;
        private System.Windows.Forms.Label gsrCalLabel;
        private System.Windows.Forms.Label hrCalLabel;
        public System.Windows.Forms.Label hrMinLabel;
        public System.Windows.Forms.Label gsrSDLabel;
        public System.Windows.Forms.Label hrSDLabel;
        public System.Windows.Forms.Label gsrMeanLabel;
        public System.Windows.Forms.Label hrMeanLabel;
        public System.Windows.Forms.Label hrMaxLabel;
        public System.Windows.Forms.Label gsrMaxLabel;
        public System.Windows.Forms.Label gsrMinLabel;
        public System.Windows.Forms.Label sdLabel;
        public System.Windows.Forms.Label meanLabel;
        public System.Windows.Forms.Label maxLabel;
        public System.Windows.Forms.Label minLabel;
        public System.Windows.Forms.RadioButton hrSensorTypeRadioButton1;
        public System.Windows.Forms.RadioButton hrSensorTypeRadioButton2;
        public System.Windows.Forms.Button gsrMinusButton;
        public System.Windows.Forms.Button gsrPlusButton;
        public System.Windows.Forms.TrackBar gsrTrackBar;
        public System.Windows.Forms.ComboBox HRSensorComboBox;

    }
}