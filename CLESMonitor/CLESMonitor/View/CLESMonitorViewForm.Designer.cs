namespace CLESMonitor.View

{
    partial class CLESMonitorViewForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.CLChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ESChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.clTextBox = new System.Windows.Forms.TextBox();
            this.esTextBox = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pauseButton = new System.Windows.Forms.Button();
            this.calibrateButton = new System.Windows.Forms.Button();
            this.timeLable = new System.Windows.Forms.Label();
            this.sesionTimeBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hrTrackBar = new System.Windows.Forms.TrackBar();
            this.hrPlusButton = new System.Windows.Forms.Button();
            this.hrMinusButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hrSensorTypeRadioButton1 = new System.Windows.Forms.RadioButton();
            this.hrSensorTypeRadioButton2 = new System.Windows.Forms.RadioButton();
            this.hrValueLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gsrValueLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gsrTrackBar = new System.Windows.Forms.TrackBar();
            this.gsrMinusButton = new System.Windows.Forms.Button();
            this.gsrPlusButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.openScenarioFileButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sensorButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CLChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ESChart)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hrTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gsrTrackBar)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CLChart
            // 
            this.CLChart.BorderlineColor = System.Drawing.Color.Gray;
            this.CLChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.CLChart.BorderlineWidth = 2;
            chartArea1.Name = "ChartArea1";
            this.CLChart.ChartAreas.Add(chartArea1);
            this.CLChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.CLChart.Legends.Add(legend1);
            this.CLChart.Location = new System.Drawing.Point(3, 3);
            this.CLChart.Name = "CLChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.CLChart.Series.Add(series1);
            this.CLChart.Size = new System.Drawing.Size(474, 376);
            this.CLChart.TabIndex = 0;
            this.CLChart.Text = "chart1";
            title1.Name = "CL-Waarden";
            title1.Text = "CL-Waarden";
            this.CLChart.Titles.Add(title1);
            // 
            // ESChart
            // 
            this.ESChart.BorderlineColor = System.Drawing.Color.Gray;
            this.ESChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.ESChart.BorderlineWidth = 2;
            chartArea2.Name = "ChartArea1";
            this.ESChart.ChartAreas.Add(chartArea2);
            this.ESChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.ESChart.Legends.Add(legend2);
            this.ESChart.Location = new System.Drawing.Point(483, 3);
            this.ESChart.Name = "ESChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.ESChart.Series.Add(series2);
            this.ESChart.Size = new System.Drawing.Size(474, 376);
            this.ESChart.TabIndex = 1;
            this.ESChart.Text = "chart2";
            title2.Name = "ES-waarden";
            title2.Text = "ES-Waarden";
            this.ESChart.Titles.Add(title2);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(138, 661);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(120, 40);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(390, 660);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(120, 40);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "CL-waarde";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(857, 403);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ES-waarde";
            // 
            // clTextBox
            // 
            this.clTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clTextBox.Enabled = false;
            this.clTextBox.Location = new System.Drawing.Point(73, 400);
            this.clTextBox.Name = "clTextBox";
            this.clTextBox.Size = new System.Drawing.Size(50, 20);
            this.clTextBox.TabIndex = 7;
            // 
            // esTextBox
            // 
            this.esTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.esTextBox.Enabled = false;
            this.esTextBox.Location = new System.Drawing.Point(922, 400);
            this.esTextBox.Name = "esTextBox";
            this.esTextBox.Size = new System.Drawing.Size(50, 20);
            this.esTextBox.TabIndex = 8;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(472, 222);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "";
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(264, 661);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(120, 40);
            this.pauseButton.TabIndex = 11;
            this.pauseButton.Text = "Pauze";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // calibrateButton
            // 
            this.calibrateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.calibrateButton.Location = new System.Drawing.Point(516, 661);
            this.calibrateButton.Name = "calibrateButton";
            this.calibrateButton.Size = new System.Drawing.Size(120, 40);
            this.calibrateButton.TabIndex = 12;
            this.calibrateButton.Text = "Kalibreren";
            this.calibrateButton.UseVisualStyleBackColor = true;
            this.calibrateButton.Click += new System.EventHandler(this.calibrateButton_Click);
            // 
            // timeLable
            // 
            this.timeLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLable.AutoSize = true;
            this.timeLable.Location = new System.Drawing.Point(826, 675);
            this.timeLable.Name = "timeLable";
            this.timeLable.Size = new System.Drawing.Size(58, 13);
            this.timeLable.TabIndex = 13;
            this.timeLable.Text = "Sessie Tijd";
            // 
            // sesionTimeBox
            // 
            this.sesionTimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sesionTimeBox.Location = new System.Drawing.Point(890, 672);
            this.sesionTimeBox.Name = "sesionTimeBox";
            this.sesionTimeBox.Size = new System.Drawing.Size(82, 20);
            this.sesionTimeBox.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.CLChart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ESChart, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 382);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // hrTrackBar
            // 
            this.hrTrackBar.LargeChange = 10;
            this.hrTrackBar.Location = new System.Drawing.Point(6, 43);
            this.hrTrackBar.Maximum = 150;
            this.hrTrackBar.Minimum = 30;
            this.hrTrackBar.Name = "hrTrackBar";
            this.hrTrackBar.Size = new System.Drawing.Size(266, 45);
            this.hrTrackBar.TabIndex = 16;
            this.hrTrackBar.TickFrequency = 10;
            this.hrTrackBar.Value = 70;
            this.hrTrackBar.Scroll += new System.EventHandler(this.hrTrackBar_Scroll);
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.hrSensorTypeRadioButton1);
            this.groupBox1.Controls.Add(this.hrSensorTypeRadioButton2);
            this.groupBox1.Controls.Add(this.hrValueLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.hrTrackBar);
            this.groupBox1.Controls.Add(this.hrMinusButton);
            this.groupBox1.Controls.Add(this.hrPlusButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 113);
            this.groupBox1.TabIndex = 19;
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
            this.hrValueLabel.Size = new System.Drawing.Size(25, 13);
            this.hrValueLabel.TabIndex = 20;
            this.hrValueLabel.Text = "999";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(481, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(473, 222);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sensoren";
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
            this.groupBox3.Size = new System.Drawing.Size(461, 78);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zweetmeter";
            // 
            // gsrValueLabel
            // 
            this.gsrValueLabel.AutoSize = true;
            this.gsrValueLabel.Location = new System.Drawing.Point(174, 51);
            this.gsrValueLabel.Name = "gsrValueLabel";
            this.gsrValueLabel.Size = new System.Drawing.Size(25, 13);
            this.gsrValueLabel.TabIndex = 20;
            this.gsrValueLabel.Text = "999";
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 426);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(957, 228);
            this.tableLayoutPanel2.TabIndex = 21;
            // 
            // openScenarioFileButton
            // 
            this.openScenarioFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openScenarioFileButton.Location = new System.Drawing.Point(12, 661);
            this.openScenarioFileButton.Name = "openScenarioFileButton";
            this.openScenarioFileButton.Size = new System.Drawing.Size(120, 40);
            this.openScenarioFileButton.TabIndex = 21;
            this.openScenarioFileButton.Text = "Open Scenario File";
            this.openScenarioFileButton.UseVisualStyleBackColor = true;
            this.openScenarioFileButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "XML files (*.xml)|*.xml";
            // 
            // sensorButton
            // 
            this.sensorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sensorButton.Location = new System.Drawing.Point(642, 661);
            this.sensorButton.Name = "sensorButton";
            this.sensorButton.Size = new System.Drawing.Size(120, 40);
            this.sensorButton.TabIndex = 22;
            this.sensorButton.Text = "Bekijk sensor input";
            this.sensorButton.UseVisualStyleBackColor = true;
            this.sensorButton.Click += new System.EventHandler(this.sensorButton_Click);
            // 
            // CLESMonitorViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 712);
            this.Controls.Add(this.sensorButton);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.openScenarioFileButton);
            this.Controls.Add(this.sesionTimeBox);
            this.Controls.Add(this.timeLable);
            this.Controls.Add(this.calibrateButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.esTextBox);
            this.Controls.Add(this.clTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.MinimumSize = new System.Drawing.Size(825, 600);
            this.Name = "CLESMonitorViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLES-Monitor";
            ((System.ComponentModel.ISupportInitialize)(this.CLChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ESChart)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hrTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gsrTrackBar)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.DataVisualization.Charting.Chart CLChart;
        public System.Windows.Forms.DataVisualization.Charting.Chart ESChart;
        public System.Windows.Forms.TextBox clTextBox;
        public System.Windows.Forms.TextBox esTextBox;
        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.Windows.Forms.Button startButton;
        public System.Windows.Forms.Button stopButton;
        public System.Windows.Forms.Button pauseButton;
        public System.Windows.Forms.Button calibrateButton;
        private System.Windows.Forms.Label timeLable;
        public System.Windows.Forms.TextBox sesionTimeBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label hrValueLabel;
        public System.Windows.Forms.TrackBar hrTrackBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Label gsrValueLabel;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TrackBar gsrTrackBar;
        private System.Windows.Forms.Button gsrMinusButton;
        private System.Windows.Forms.Button gsrPlusButton;
        private System.Windows.Forms.RadioButton hrSensorTypeRadioButton1;
        private System.Windows.Forms.RadioButton hrSensorTypeRadioButton2;
        private System.Windows.Forms.Button openScenarioFileButton;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Button hrPlusButton;
        public System.Windows.Forms.Button hrMinusButton;
        public System.Windows.Forms.Button sensorButton;

    }
}

