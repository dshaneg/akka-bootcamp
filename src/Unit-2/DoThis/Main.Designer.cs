namespace ChartApp
{
    partial class Main
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
            this.sysChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonCpu = new System.Windows.Forms.Button();
            this.buttonMemory = new System.Windows.Forms.Button();
            this.buttonDisk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sysChart)).BeginInit();
            this.SuspendLayout();
            // 
            // sysChart
            // 
            chartArea1.Name = "ChartArea1";
            this.sysChart.ChartAreas.Add(chartArea1);
            this.sysChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.sysChart.Legends.Add(legend1);
            this.sysChart.Location = new System.Drawing.Point(0, 0);
            this.sysChart.Name = "sysChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.sysChart.Series.Add(series1);
            this.sysChart.Size = new System.Drawing.Size(684, 446);
            this.sysChart.TabIndex = 0;
            this.sysChart.Text = "sysChart";
            // 
            // buttonCpu
            // 
            this.buttonCpu.Location = new System.Drawing.Point(568, 208);
            this.buttonCpu.Name = "buttonCpu";
            this.buttonCpu.Size = new System.Drawing.Size(104, 52);
            this.buttonCpu.TabIndex = 1;
            this.buttonCpu.Text = "CPU (OFF)";
            this.buttonCpu.UseVisualStyleBackColor = true;
            this.buttonCpu.Click += new System.EventHandler(this.buttonCpu_Click);
            // 
            // buttonMemory
            // 
            this.buttonMemory.Location = new System.Drawing.Point(568, 281);
            this.buttonMemory.Name = "buttonMemory";
            this.buttonMemory.Size = new System.Drawing.Size(104, 52);
            this.buttonMemory.TabIndex = 2;
            this.buttonMemory.Text = "MEMORY (OFF)";
            this.buttonMemory.UseVisualStyleBackColor = true;
            this.buttonMemory.Click += new System.EventHandler(this.buttonMemory_Click);
            // 
            // buttonDisk
            // 
            this.buttonDisk.Location = new System.Drawing.Point(568, 352);
            this.buttonDisk.Name = "buttonDisk";
            this.buttonDisk.Size = new System.Drawing.Size(104, 52);
            this.buttonDisk.TabIndex = 3;
            this.buttonDisk.Text = "DISK (OFF)";
            this.buttonDisk.UseVisualStyleBackColor = true;
            this.buttonDisk.Click += new System.EventHandler(this.buttonDisk_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 446);
            this.Controls.Add(this.buttonDisk);
            this.Controls.Add(this.buttonMemory);
            this.Controls.Add(this.buttonCpu);
            this.Controls.Add(this.sysChart);
            this.Name = "Main";
            this.Text = "System Metrics";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sysChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart sysChart;
        private System.Windows.Forms.Button buttonCpu;
        private System.Windows.Forms.Button buttonMemory;
        private System.Windows.Forms.Button buttonDisk;
    }
}

