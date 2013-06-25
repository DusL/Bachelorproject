namespace CLESMonitor.View
{
    partial class CTLModelUtilityView
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
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Actieve Taken", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Geschiedenis", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Een taak die afgelopen is",
            "0:01",
            "9:59"}, -1);
            this.openScenarioFileButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.activeListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // openScenarioFileButton
            // 
            this.openScenarioFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openScenarioFileButton.Location = new System.Drawing.Point(0, 350);
            this.openScenarioFileButton.Name = "openScenarioFileButton";
            this.openScenarioFileButton.Size = new System.Drawing.Size(120, 40);
            this.openScenarioFileButton.TabIndex = 22;
            this.openScenarioFileButton.Text = "Open Scenario";
            this.openScenarioFileButton.UseVisualStyleBackColor = true;
            this.openScenarioFileButton.Click += new System.EventHandler(this.openScenarioFileButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "XML files (*.xml)|*.xml";
            // 
            // activeListView
            // 
            this.activeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.activeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            listViewGroup5.Header = "Actieve Taken";
            listViewGroup5.Name = "listViewGroup1";
            listViewGroup5.Tag = "";
            listViewGroup6.Header = "Geschiedenis";
            listViewGroup6.Name = "listViewGroup2";
            listViewGroup6.Tag = "";
            this.activeListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup5,
            listViewGroup6});
            listViewItem3.Group = listViewGroup6;
            this.activeListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3});
            this.activeListView.Location = new System.Drawing.Point(0, 0);
            this.activeListView.Name = "activeListView";
            this.activeListView.Size = new System.Drawing.Size(442, 344);
            this.activeListView.TabIndex = 23;
            this.activeListView.UseCompatibleStateImageBehavior = false;
            this.activeListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Naam";
            this.columnHeader1.Width = 180;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Starttijd";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Eindtijd";
            this.columnHeader3.Width = 80;
            // 
            // CTLModelUtilityView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 402);
            this.Controls.Add(this.activeListView);
            this.Controls.Add(this.openScenarioFileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CTLModelUtilityView";
            this.Text = "CTLModelUtilityView";
            this.Shown += new System.EventHandler(this.CTLModelUtilityView_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.OpenFileDialog openFileDialog;
        public System.Windows.Forms.ListView activeListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        public System.Windows.Forms.Button openScenarioFileButton;


    }
}