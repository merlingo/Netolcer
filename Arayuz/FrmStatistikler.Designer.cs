namespace Arayuz
{
    partial class FrmStatistikler
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.header = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblEnyuksekSicaklikZaman = new System.Windows.Forms.Label();
            this.lblEnyuksekSicaklikSensor = new System.Windows.Forms.Label();
            this.lblEnyuksekSicaklikDeger = new System.Windows.Forms.Label();
            this.lblEnDusukSicaklikZaman = new System.Windows.Forms.Label();
            this.lblEnDusukSicaklikDeger = new System.Windows.Forms.Label();
            this.lblEndusukSicaklikSensor = new System.Windows.Forms.Label();
            this.lblEnyuksekNemZaman = new System.Windows.Forms.Label();
            this.lblEnyuksekNemDeger = new System.Windows.Forms.Label();
            this.lblEnyuksekNemSensor = new System.Windows.Forms.Label();
            this.lblEndusukNemZaman = new System.Windows.Forms.Label();
            this.lblEndusukNemsensor = new System.Windows.Forms.Label();
            this.lblEndusukNemDeger = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblOrtalamaSicaklik = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblOrtalamaNem = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(676, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "TAMAM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 286);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1581, 126);
            this.panel1.TabIndex = 3;
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.Firebrick;
            this.header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.header.ForeColor = System.Drawing.Color.GhostWhite;
            this.header.Location = new System.Drawing.Point(3, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(1581, 62);
            this.header.TabIndex = 1;
            this.header.Text = "NET ÖLÇER - DATALOGGER UYGULAMASI GENEL İSTATİSTİKLER";
            this.header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetPartial;
            this.tableLayoutPanel3.ColumnCount = 10;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.Controls.Add(this.lblOrtalamaNem, 9, 1);
            this.tableLayoutPanel3.Controls.Add(this.label10, 9, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblOrtalamaSicaklik, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblEndusukNemDeger, 6, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblEndusukNemsensor, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblEndusukNemZaman, 8, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblEnyuksekNemSensor, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblEnyuksekNemDeger, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblEnyuksekNemZaman, 8, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblEndusukSicaklikSensor, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblEnDusukSicaklikDeger, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblEnDusukSicaklikZaman, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblEnyuksekSicaklikDeger, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblEnyuksekSicaklikSensor, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblEnyuksekSicaklikZaman, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label7, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.label5, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 65);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1581, 215);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 103);
            this.label1.TabIndex = 7;
            this.label1.Text = "Genel En Yüksek Sıcaklık:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(6, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 103);
            this.label4.TabIndex = 8;
            this.label4.Text = "Genel En Düşük Sıcaklık:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(791, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 103);
            this.label5.TabIndex = 9;
            this.label5.Text = "Genel En Yüksek Nem:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(791, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 103);
            this.label7.TabIndex = 10;
            this.label7.Text = "Genel En Düşük Nem:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnyuksekSicaklikZaman
            // 
            this.lblEnyuksekSicaklikZaman.AutoSize = true;
            this.lblEnyuksekSicaklikZaman.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnyuksekSicaklikZaman.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEnyuksekSicaklikZaman.ForeColor = System.Drawing.Color.DarkRed;
            this.lblEnyuksekSicaklikZaman.Location = new System.Drawing.Point(477, 3);
            this.lblEnyuksekSicaklikZaman.Name = "lblEnyuksekSicaklikZaman";
            this.lblEnyuksekSicaklikZaman.Size = new System.Drawing.Size(148, 103);
            this.lblEnyuksekSicaklikZaman.TabIndex = 11;
            this.lblEnyuksekSicaklikZaman.Text = "zaman";
            this.lblEnyuksekSicaklikZaman.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnyuksekSicaklikSensor
            // 
            this.lblEnyuksekSicaklikSensor.AutoSize = true;
            this.lblEnyuksekSicaklikSensor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnyuksekSicaklikSensor.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEnyuksekSicaklikSensor.ForeColor = System.Drawing.Color.DarkRed;
            this.lblEnyuksekSicaklikSensor.Location = new System.Drawing.Point(320, 3);
            this.lblEnyuksekSicaklikSensor.Name = "lblEnyuksekSicaklikSensor";
            this.lblEnyuksekSicaklikSensor.Size = new System.Drawing.Size(148, 103);
            this.lblEnyuksekSicaklikSensor.TabIndex = 12;
            this.lblEnyuksekSicaklikSensor.Text = "sensör";
            this.lblEnyuksekSicaklikSensor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnyuksekSicaklikDeger
            // 
            this.lblEnyuksekSicaklikDeger.AutoSize = true;
            this.lblEnyuksekSicaklikDeger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnyuksekSicaklikDeger.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEnyuksekSicaklikDeger.ForeColor = System.Drawing.Color.DarkRed;
            this.lblEnyuksekSicaklikDeger.Location = new System.Drawing.Point(163, 3);
            this.lblEnyuksekSicaklikDeger.Name = "lblEnyuksekSicaklikDeger";
            this.lblEnyuksekSicaklikDeger.Size = new System.Drawing.Size(148, 103);
            this.lblEnyuksekSicaklikDeger.TabIndex = 13;
            this.lblEnyuksekSicaklikDeger.Text = "değer";
            this.lblEnyuksekSicaklikDeger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnDusukSicaklikZaman
            // 
            this.lblEnDusukSicaklikZaman.AutoSize = true;
            this.lblEnDusukSicaklikZaman.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnDusukSicaklikZaman.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEnDusukSicaklikZaman.ForeColor = System.Drawing.Color.Teal;
            this.lblEnDusukSicaklikZaman.Location = new System.Drawing.Point(477, 109);
            this.lblEnDusukSicaklikZaman.Name = "lblEnDusukSicaklikZaman";
            this.lblEnDusukSicaklikZaman.Size = new System.Drawing.Size(148, 103);
            this.lblEnDusukSicaklikZaman.TabIndex = 14;
            this.lblEnDusukSicaklikZaman.Text = "sensör";
            this.lblEnDusukSicaklikZaman.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnDusukSicaklikDeger
            // 
            this.lblEnDusukSicaklikDeger.AutoSize = true;
            this.lblEnDusukSicaklikDeger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnDusukSicaklikDeger.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEnDusukSicaklikDeger.ForeColor = System.Drawing.Color.Teal;
            this.lblEnDusukSicaklikDeger.Location = new System.Drawing.Point(163, 109);
            this.lblEnDusukSicaklikDeger.Name = "lblEnDusukSicaklikDeger";
            this.lblEnDusukSicaklikDeger.Size = new System.Drawing.Size(148, 103);
            this.lblEnDusukSicaklikDeger.TabIndex = 15;
            this.lblEnDusukSicaklikDeger.Text = "değer";
            this.lblEnDusukSicaklikDeger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEndusukSicaklikSensor
            // 
            this.lblEndusukSicaklikSensor.AutoSize = true;
            this.lblEndusukSicaklikSensor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEndusukSicaklikSensor.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEndusukSicaklikSensor.ForeColor = System.Drawing.Color.Teal;
            this.lblEndusukSicaklikSensor.Location = new System.Drawing.Point(320, 109);
            this.lblEndusukSicaklikSensor.Name = "lblEndusukSicaklikSensor";
            this.lblEndusukSicaklikSensor.Size = new System.Drawing.Size(148, 103);
            this.lblEndusukSicaklikSensor.TabIndex = 16;
            this.lblEndusukSicaklikSensor.Text = "zaman";
            this.lblEndusukSicaklikSensor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnyuksekNemZaman
            // 
            this.lblEnyuksekNemZaman.AutoSize = true;
            this.lblEnyuksekNemZaman.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnyuksekNemZaman.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEnyuksekNemZaman.ForeColor = System.Drawing.Color.DarkRed;
            this.lblEnyuksekNemZaman.Location = new System.Drawing.Point(1262, 3);
            this.lblEnyuksekNemZaman.Name = "lblEnyuksekNemZaman";
            this.lblEnyuksekNemZaman.Size = new System.Drawing.Size(148, 103);
            this.lblEnyuksekNemZaman.TabIndex = 17;
            this.lblEnyuksekNemZaman.Text = "zaman";
            this.lblEnyuksekNemZaman.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnyuksekNemDeger
            // 
            this.lblEnyuksekNemDeger.AutoSize = true;
            this.lblEnyuksekNemDeger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnyuksekNemDeger.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEnyuksekNemDeger.ForeColor = System.Drawing.Color.DarkRed;
            this.lblEnyuksekNemDeger.Location = new System.Drawing.Point(948, 3);
            this.lblEnyuksekNemDeger.Name = "lblEnyuksekNemDeger";
            this.lblEnyuksekNemDeger.Size = new System.Drawing.Size(148, 103);
            this.lblEnyuksekNemDeger.TabIndex = 18;
            this.lblEnyuksekNemDeger.Text = "değer";
            this.lblEnyuksekNemDeger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnyuksekNemSensor
            // 
            this.lblEnyuksekNemSensor.AutoSize = true;
            this.lblEnyuksekNemSensor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnyuksekNemSensor.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEnyuksekNemSensor.ForeColor = System.Drawing.Color.DarkRed;
            this.lblEnyuksekNemSensor.Location = new System.Drawing.Point(1105, 3);
            this.lblEnyuksekNemSensor.Name = "lblEnyuksekNemSensor";
            this.lblEnyuksekNemSensor.Size = new System.Drawing.Size(148, 103);
            this.lblEnyuksekNemSensor.TabIndex = 19;
            this.lblEnyuksekNemSensor.Text = "sensör";
            this.lblEnyuksekNemSensor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEndusukNemZaman
            // 
            this.lblEndusukNemZaman.AutoSize = true;
            this.lblEndusukNemZaman.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEndusukNemZaman.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEndusukNemZaman.ForeColor = System.Drawing.Color.Teal;
            this.lblEndusukNemZaman.Location = new System.Drawing.Point(1262, 109);
            this.lblEndusukNemZaman.Name = "lblEndusukNemZaman";
            this.lblEndusukNemZaman.Size = new System.Drawing.Size(148, 103);
            this.lblEndusukNemZaman.TabIndex = 20;
            this.lblEndusukNemZaman.Text = "zaman";
            this.lblEndusukNemZaman.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEndusukNemsensor
            // 
            this.lblEndusukNemsensor.AutoSize = true;
            this.lblEndusukNemsensor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEndusukNemsensor.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEndusukNemsensor.ForeColor = System.Drawing.Color.Teal;
            this.lblEndusukNemsensor.Location = new System.Drawing.Point(1105, 109);
            this.lblEndusukNemsensor.Name = "lblEndusukNemsensor";
            this.lblEndusukNemsensor.Size = new System.Drawing.Size(148, 103);
            this.lblEndusukNemsensor.TabIndex = 21;
            this.lblEndusukNemsensor.Text = "sensor";
            this.lblEndusukNemsensor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEndusukNemDeger
            // 
            this.lblEndusukNemDeger.AutoSize = true;
            this.lblEndusukNemDeger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEndusukNemDeger.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblEndusukNemDeger.ForeColor = System.Drawing.Color.Teal;
            this.lblEndusukNemDeger.Location = new System.Drawing.Point(948, 109);
            this.lblEndusukNemDeger.Name = "lblEndusukNemDeger";
            this.lblEndusukNemDeger.Size = new System.Drawing.Size(148, 103);
            this.lblEndusukNemDeger.TabIndex = 22;
            this.lblEndusukNemDeger.Text = "değer";
            this.lblEndusukNemDeger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(634, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 103);
            this.label8.TabIndex = 23;
            this.label8.Text = "Genel Ortalama Sıcaklık:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrtalamaSicaklik
            // 
            this.lblOrtalamaSicaklik.AutoSize = true;
            this.lblOrtalamaSicaklik.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOrtalamaSicaklik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrtalamaSicaklik.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOrtalamaSicaklik.ForeColor = System.Drawing.Color.DarkRed;
            this.lblOrtalamaSicaklik.Location = new System.Drawing.Point(634, 109);
            this.lblOrtalamaSicaklik.Name = "lblOrtalamaSicaklik";
            this.lblOrtalamaSicaklik.Size = new System.Drawing.Size(148, 103);
            this.lblOrtalamaSicaklik.TabIndex = 24;
            this.lblOrtalamaSicaklik.Text = "zaman";
            this.lblOrtalamaSicaklik.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(1419, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(156, 103);
            this.label10.TabIndex = 25;
            this.label10.Text = "Genel Ortalama Nem";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOrtalamaNem
            // 
            this.lblOrtalamaNem.AutoSize = true;
            this.lblOrtalamaNem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOrtalamaNem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrtalamaNem.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblOrtalamaNem.ForeColor = System.Drawing.Color.Teal;
            this.lblOrtalamaNem.Location = new System.Drawing.Point(1419, 109);
            this.lblOrtalamaNem.Name = "lblOrtalamaNem";
            this.lblOrtalamaNem.Size = new System.Drawing.Size(156, 103);
            this.lblOrtalamaNem.TabIndex = 26;
            this.lblOrtalamaNem.Text = "sensör";
            this.lblOrtalamaNem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.header, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.49398F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.56627F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1587, 415);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // FrmStatistikler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1587, 415);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmStatistikler";
            this.Text = "FrmStatistikler";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblOrtalamaNem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblOrtalamaSicaklik;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblEndusukNemDeger;
        private System.Windows.Forms.Label lblEndusukNemsensor;
        private System.Windows.Forms.Label lblEndusukNemZaman;
        private System.Windows.Forms.Label lblEnyuksekNemSensor;
        private System.Windows.Forms.Label lblEnyuksekNemDeger;
        private System.Windows.Forms.Label lblEnyuksekNemZaman;
        private System.Windows.Forms.Label lblEndusukSicaklikSensor;
        private System.Windows.Forms.Label lblEnDusukSicaklikDeger;
        private System.Windows.Forms.Label lblEnDusukSicaklikZaman;
        private System.Windows.Forms.Label lblEnyuksekSicaklikDeger;
        private System.Windows.Forms.Label lblEnyuksekSicaklikSensor;
        private System.Windows.Forms.Label lblEnyuksekSicaklikZaman;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;


    }
}