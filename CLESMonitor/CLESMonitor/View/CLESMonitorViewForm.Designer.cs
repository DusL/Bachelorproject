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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CLChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ESChart)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hrTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CLChart
            // 
            chartArea3.Name = "ChartArea1";
            this.CLChart.ChartAreas.Add(chartArea3);
            this.CLChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.CLChart.Legends.Add(legend3);
            this.CLChart.Location = new System.Drawing.Point(3, 3);
            this.CLChart.Name = "CLChart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.CLChart.Series.Add(series3);
            this.CLChart.Size = new System.Drawing.Size(474, 376);
            this.CLChart.TabIndex = 0;
            this.CLChart.Text = "chart1";
            title3.Name = "CL-Waarden";
            title3.Text = "CL-Waarden";
            this.CLChart.Titles.Add(title3);
            // 
            // ESChart
            // 
            chartArea4.Name = "ChartArea1";
            this.ESChart.ChartAreas.Add(chartArea4);
            this.ESChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.ESChart.Legends.Add(legend4);
            this.ESChart.Location = new System.Drawing.Point(483, 3);
            this.ESChart.Name = "ESChart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.ESChart.Series.Add(series4);
            this.ESChart.Size = new System.Drawing.Size(474, 376);
            this.ESChart.TabIndex = 1;
            this.ESChart.Text = "chart2";
            title4.Name = "ES-waarden";
            title4.Text = "ES-Waarden";
            this.ESChart.Titles.Add(title4);
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startButton.Location = new System.Drawing.Point(12, 660);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(124, 40);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(848, 660);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(124, 40);
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
            this.clTextBox.Location = new System.Drawing.Point(73, 400);
            this.clTextBox.Name = "clTextBox";
            this.clTextBox.Size = new System.Drawing.Size(50, 20);
            this.clTextBox.TabIndex = 7;
            // 
            // esTextBox
            // 
            this.esTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.esTextBox.Location = new System.Drawing.Point(922, 400);
            this.esTextBox.Name = "esTextBox";
            this.esTextBox.Size = new System.Drawing.Size(50, 20);
            this.esTextBox.TabIndex = 8;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 444);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(477, 146);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "";
            // 
            // pauseButton
            // 
            this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(142, 660);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(124, 40);
            this.pauseButton.TabIndex = 11;
            this.pauseButton.Text = "Pauze";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // calibrateButton
            // 
            this.calibrateButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.calibrateButton.Location = new System.Drawing.Point(436, 660);
            this.calibrateButton.Name = "calibrateButton";
            this.calibrateButton.Size = new System.Drawing.Size(124, 40);
            this.calibrateButton.TabIndex = 12;
            this.calibrateButton.Text = "Kalibreren";
            this.calibrateButton.UseVisualStyleBackColor = true;
            // 
            // timeLable
            // 
            this.timeLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timeLable.AutoSize = true;
            this.timeLable.Location = new System.Drawing.Point(9, 627);
            this.timeLable.Name = "timeLable";
            this.timeLable.Size = new System.Drawing.Size(58, 13);
            this.timeLable.TabIndex = 13;
            this.timeLable.Text = "Sessie Tijd";
            // 
            // sesionTimeBox
            // 
            this.sesionTimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sesionTimeBox.Location = new System.Drawing.Point(73, 624);
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
            this.hrTrackBar.Location = new System.Drawing.Point(6, 49);
            this.hrTrackBar.Name = "hrTrackBar";
            this.hrTrackBar.Size = new System.Drawing.Size(266, 45);
            this.hrTrackBar.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 17;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(314, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 18;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.hrTrackBar);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(495, 444);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 100);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hartslagmeter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Gemeten hartslag (slagen/minuut)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "000";
            // 
            // CLESMonitorViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 712);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.sesionTimeBox);
            this.Controls.Add(this.timeLable);
            this.Controls.Add(this.calibrateButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.esTextBox);
            this.Controls.Add(this.clTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "CLESMonitorViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLES-Monitor";
            ((System.ComponentModel.ISupportInitialize)(this.CLChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ESChart)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hrTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TrackBar hrTrackBar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;

    }
}

