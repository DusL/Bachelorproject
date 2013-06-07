namespace CLESMonitor.View
{
    partial class SensorViewForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
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
            chartArea5.Name = "ChartArea1";
            this.GSRChart.ChartAreas.Add(chartArea5);
            this.GSRChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend5.Enabled = false;
            legend5.Name = "Legend1";
            this.GSRChart.Legends.Add(legend5);
            this.GSRChart.Location = new System.Drawing.Point(299, 3);
            this.GSRChart.Name = "GSRChart";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.GSRChart.Series.Add(series5);
            this.GSRChart.Size = new System.Drawing.Size(291, 304);
            this.GSRChart.TabIndex = 5;
            this.GSRChart.Text = "GSRChart";
            title5.Name = "ES-waarden";
            title5.Text = "ES-Waarden";
            this.GSRChart.Titles.Add(title5);
            // 
            // HRChart
            // 
            this.HRChart.BorderlineColor = System.Drawing.Color.Gray;
            this.HRChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.HRChart.BorderlineWidth = 2;
            chartArea6.Name = "ChartArea1";
            this.HRChart.ChartAreas.Add(chartArea6);
            this.HRChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend6.Enabled = false;
            legend6.Name = "Legend1";
            this.HRChart.Legends.Add(legend6);
            this.HRChart.Location = new System.Drawing.Point(3, 3);
            this.HRChart.Name = "HRChart";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.HRChart.Series.Add(series6);
            this.HRChart.Size = new System.Drawing.Size(290, 304);
            this.HRChart.TabIndex = 4;
            this.HRChart.Text = "HRChart";
            title6.Name = "ES-waarden";
            title6.Text = "ES-Waarden";
            this.HRChart.Titles.Add(title6);
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