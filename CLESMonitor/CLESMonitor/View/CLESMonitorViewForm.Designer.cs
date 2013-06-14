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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea13 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend13 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title13 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea14 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend14 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title14 = new System.Windows.Forms.DataVisualization.Charting.Title();
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.openScenarioFileButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sensorButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CLChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ESChart)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // CLChart
            // 
            this.CLChart.BorderlineColor = System.Drawing.Color.Gray;
            this.CLChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.CLChart.BorderlineWidth = 2;
            chartArea13.AxisX.Interval = 2D;
            chartArea13.AxisX.ScaleView.MinSize = 10D;
            chartArea13.AxisX.ScaleView.MinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea13.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Black;
            chartArea13.AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea13.AxisX.Title = "Sessietijd";
            chartArea13.AxisY.Maximum = 2D;
            chartArea13.AxisY.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea13.Name = "ChartArea1";
            this.CLChart.ChartAreas.Add(chartArea13);
            this.CLChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend13.Enabled = false;
            legend13.Name = "Legend1";
            this.CLChart.Legends.Add(legend13);
            this.CLChart.Location = new System.Drawing.Point(3, 3);
            this.CLChart.Name = "CLChart";
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series13.Legend = "Legend1";
            series13.Name = "Series1";
            this.CLChart.Series.Add(series13);
            this.CLChart.Size = new System.Drawing.Size(474, 376);
            this.CLChart.TabIndex = 0;
            this.CLChart.Text = "chart1";
            title13.Name = "CL-Waarden";
            title13.Text = "CL-Waarden";
            this.CLChart.Titles.Add(title13);
            // 
            // ESChart
            // 
            this.ESChart.BorderlineColor = System.Drawing.Color.Gray;
            this.ESChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.ESChart.BorderlineWidth = 2;
            chartArea14.AxisX.Interval = 2D;
            chartArea14.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea14.AxisX.Title = "Sessietijd";
            chartArea14.AxisY.Maximum = 5D;
            chartArea14.Name = "ChartArea1";
            this.ESChart.ChartAreas.Add(chartArea14);
            this.ESChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend14.Enabled = false;
            legend14.Name = "Legend1";
            this.ESChart.Legends.Add(legend14);
            this.ESChart.Location = new System.Drawing.Point(483, 3);
            this.ESChart.Name = "ESChart";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series14.Legend = "Legend1";
            series14.Name = "Series1";
            this.ESChart.Series.Add(series14);
            this.ESChart.Size = new System.Drawing.Size(474, 376);
            this.ESChart.TabIndex = 1;
            this.ESChart.Text = "chart2";
            title14.Name = "ES-waarden";
            title14.Text = "ES-Waarden";
            this.ESChart.Titles.Add(title14);
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
            this.label1.Location = new System.Drawing.Point(9, 378);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "CL-waarde";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(857, 378);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ES-waarde";
            // 
            // clTextBox
            // 
            this.clTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clTextBox.Enabled = false;
            this.clTextBox.Location = new System.Drawing.Point(73, 375);
            this.clTextBox.Name = "clTextBox";
            this.clTextBox.Size = new System.Drawing.Size(50, 20);
            this.clTextBox.TabIndex = 7;
            // 
            // esTextBox
            // 
            this.esTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.esTextBox.Enabled = false;
            this.esTextBox.Location = new System.Drawing.Point(922, 375);
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
            this.richTextBox1.Size = new System.Drawing.Size(472, 247);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 350);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 401);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(957, 253);
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
        public System.Windows.Forms.TextBox sesionTimeBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button openScenarioFileButton;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Button sensorButton;
        public System.Windows.Forms.Label timeLable;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

    }
}

