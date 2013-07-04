namespace CLESMonitor.View

{
    partial class MainView
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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.timeLable = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.clesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.clesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bestandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionTimeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clesChart)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeLable
            // 
            this.timeLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLable.AutoSize = true;
            this.timeLable.Location = new System.Drawing.Point(858, 9);
            this.timeLable.Name = "timeLable";
            this.timeLable.Size = new System.Drawing.Size(59, 13);
            this.timeLable.TabIndex = 13;
            this.timeLable.Text = "Sessieduur";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.4375F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.5625F));
            this.tableLayoutPanel1.Controls.Add(this.clesChart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.clesRichTextBox, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(960, 329);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // clesChart
            // 
            this.clesChart.BorderlineColor = System.Drawing.Color.Gray;
            this.clesChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.clesChart.BorderlineWidth = 2;
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.LabelStyle.IsEndLabelVisible = false;
            chartArea1.AxisX.ScaleView.Position = 0D;
            chartArea1.AxisX.ScaleView.Size = 15D;
            chartArea1.AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea1.AxisX.ScrollBar.IsPositionedInside = false;
            chartArea1.AxisY.Maximum = 5D;
            chartArea1.Name = "clesChartArea";
            this.clesChart.ChartAreas.Add(chartArea1);
            this.clesChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.DockedToChartArea = "clesChartArea";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Legend1";
            this.clesChart.Legends.Add(legend1);
            this.clesChart.Location = new System.Drawing.Point(3, 3);
            this.clesChart.Name = "clesChart";
            series1.BorderWidth = 2;
            series1.ChartArea = "clesChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Blue;
            series1.Legend = "Legend1";
            series1.LegendText = "Emotionele Toestand";
            series1.MarkerBorderColor = System.Drawing.Color.Blue;
            series1.MarkerColor = System.Drawing.Color.Magenta;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series1.Name = "esSeries";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.BorderWidth = 2;
            series2.ChartArea = "clesChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.OrangeRed;
            series2.Legend = "Legend1";
            series2.LegendText = "Cognitieve Belasting";
            series2.MarkerBorderColor = System.Drawing.Color.DarkRed;
            series2.MarkerColor = System.Drawing.Color.OrangeRed;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Triangle;
            series2.Name = "clSeries";
            this.clesChart.Series.Add(series1);
            this.clesChart.Series.Add(series2);
            this.clesChart.Size = new System.Drawing.Size(651, 323);
            this.clesChart.TabIndex = 1;
            this.clesChart.Text = "chart2";
            title1.Name = "clesValues";
            title1.Visible = false;
            this.clesChart.Titles.Add(title1);
            // 
            // clesRichTextBox
            // 
            this.clesRichTextBox.Location = new System.Drawing.Point(660, 3);
            this.clesRichTextBox.Name = "clesRichTextBox";
            this.clesRichTextBox.ReadOnly = true;
            this.clesRichTextBox.Size = new System.Drawing.Size(297, 323);
            this.clesRichTextBox.TabIndex = 2;
            this.clesRichTextBox.Text = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoScroll = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 362);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(960, 338);
            this.tableLayoutPanel2.TabIndex = 21;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bestandToolStripMenuItem,
            this.sessionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(9, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(119, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bestandToolStripMenuItem
            // 
            this.bestandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.bestandToolStripMenuItem.Name = "bestandToolStripMenuItem";
            this.bestandToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.bestandToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.bestandToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.bestandToolStripMenuItem.Text = "Bestand";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.quitToolStripMenuItem.Text = "Afsluiten";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // sessionToolStripMenuItem
            // 
            this.sessionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripSeparator1,
            this.stopToolStripMenuItem});
            this.sessionToolStripMenuItem.Name = "sessionToolStripMenuItem";
            this.sessionToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.sessionToolStripMenuItem.Text = "Sessie";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.startToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(136, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(136, 6);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // sessionTimeLabel
            // 
            this.sessionTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionTimeLabel.AutoSize = true;
            this.sessionTimeLabel.Location = new System.Drawing.Point(922, 9);
            this.sessionTimeLabel.Name = "sessionTimeLabel";
            this.sessionTimeLabel.Size = new System.Drawing.Size(43, 13);
            this.sessionTimeLabel.TabIndex = 23;
            this.sessionTimeLabel.Text = "0.00.00";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 712);
            this.Controls.Add(this.sessionTimeLabel);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.timeLable);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(825, 600);
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor voor Cognitieve Belasting en Emotionele Toestand";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CLESMonitorViewForm_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clesChart)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label timeLable;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bestandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        public System.Windows.Forms.Label sessionTimeLabel;
        public System.Windows.Forms.DataVisualization.Charting.Chart clesChart;
        public System.Windows.Forms.RichTextBox clesRichTextBox;

    }
}

