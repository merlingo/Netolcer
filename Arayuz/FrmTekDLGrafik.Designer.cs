namespace Arayuz
{
    partial class FrmTekDLGrafik
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
            this.chartDataLogger = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartDataLogger)).BeginInit();
            this.SuspendLayout();
            // 
            // chartDataLogger
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDataLogger.ChartAreas.Add(chartArea1);
            this.chartDataLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartDataLogger.Legends.Add(legend1);
            this.chartDataLogger.Location = new System.Drawing.Point(0, 0);
            this.chartDataLogger.Name = "chartDataLogger";
            this.chartDataLogger.Size = new System.Drawing.Size(1501, 830);
            this.chartDataLogger.TabIndex = 0;
            this.chartDataLogger.Text = "chart1";
            // 
            // FrmTekDLGrafik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1501, 830);
            this.Controls.Add(this.chartDataLogger);
            this.Name = "FrmTekDLGrafik";
            this.Text = "FrmTekDLGrafik";
            ((System.ComponentModel.ISupportInitialize)(this.chartDataLogger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartDataLogger;
    }
}