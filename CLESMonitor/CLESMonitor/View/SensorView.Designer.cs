namespace CLESMonitor.View
{
    partial class SensorView
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GSRChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.HRChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GSRChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRChart)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.GSRChart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.HRChart, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(593, 310);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // GSRChart
            // 
            this.GSRChart.BorderlineColor = System.Drawing.Color.Gray;
            this.GSRChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.GSRChart.BorderlineWidth = 2;
            chartArea1.AxisX.Title = "Sessietijd";
            chartArea1.Name = "ChartArea1";
            this.GSRChart.ChartAreas.Add(chartArea1);
            this.GSRChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.GSRChart.Legends.Add(legend1);
            this.GSRChart.Location = new System.Drawing.Point(299, 3);
            this.GSRChart.Name = "GSRChart";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Blue;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.GSRChart.Series.Add(series1);
            this.GSRChart.Size = new System.Drawing.Size(291, 304);
            this.GSRChart.TabIndex = 5;
            this.GSRChart.Text = "GSRChart";
            title1.Name = "GSR-waarden";
            title1.Text = "Huidgeleiding";
            this.GSRChart.Titles.Add(title1);
            // 
            // HRChart
            // 
            this.HRChart.BorderlineColor = System.Drawing.Color.Gray;
            this.HRChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.HRChart.BorderlineWidth = 2;
            chartArea2.AxisX.Title = "Sessietijd";
            chartArea2.Name = "ChartArea1";
            this.HRChart.ChartAreas.Add(chartArea2);
            this.HRChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.HRChart.Legends.Add(legend2);
            this.HRChart.Location = new System.Drawing.Point(3, 3);
            this.HRChart.Name = "HRChart";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.OrangeRed;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.HRChart.Series.Add(series2);
            this.HRChart.Size = new System.Drawing.Size(290, 304);
            this.HRChart.TabIndex = 4;
            this.HRChart.Text = "HRChart";
            title2.Name = "HR-values";
            title2.Text = "Hartslag";
            this.HRChart.Titles.Add(title2);
            // 
            // SensorViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 310);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SensorViewForm";
            this.Text = "HR en GSR waarden";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SensorViewForm_FormClosing);
            this.Shown += new System.EventHandler(this.SensorViewForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GSRChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HRChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.DataVisualization.Charting.Chart HRChart;
        public System.Windows.Forms.DataVisualization.Charting.Chart GSRChart;
    }
}