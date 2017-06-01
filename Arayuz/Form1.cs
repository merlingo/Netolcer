using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baglanti;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

namespace Arayuz
{
    public partial class Form1 : Form
    {
        ParsingText pt;
        DateTime minDate;
        DateTime maxDate;
        public Form1()
        {
            //get text file
            dosyaIste();
            InitializeComponent();
            string appPath = Application.StartupPath;
            Icon icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\unnamed.ico");

            this.Icon = icon;
            // Define the border style of the form to a dialog box.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = true;

            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = true;

            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;

            //bind comboboxVeriAraligi to dictionary veriaralıgı
            Dictionary<double, string> VeriAraliklari = new Dictionary<double, string>();
            VeriAraliklari.Add(0, "Anonim");
            //VeriAraliklari.Add(0.5, "30 sn"); //tasarım dk ustune yapıldı
            VeriAraliklari.Add(5, "5 dk");
            VeriAraliklari.Add(10, "10 dk");
            VeriAraliklari.Add(20, "20 dk");
            VeriAraliklari.Add(30, "30 dk");

            comboBoxVeriAraligi.DataSource = new BindingSource(VeriAraliklari, null);
            comboBoxVeriAraligi.DisplayMember = "Value";
            comboBoxVeriAraligi.ValueMember = "Key";


            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMMM yyyy HH mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd MMMM yyyy HH mm";
            //limitle
            DataTable dt = pt.DT;
            dateTimePicker1.MinDate = dt.Rows[0].Field<DateTime>("Tarih");
            minDate = dt.Rows[0].Field<DateTime>("Tarih");
            maxDate = dt.Rows[dt.Rows.Count - 1].Field<DateTime>("Tarih");

            dateTimePicker1.Value = minDate;
            dateTimePicker1.MaxDate = maxDate;
            dateTimePicker2.MinDate = minDate;
            dateTimePicker2.MaxDate = maxDate;
            dateTimePicker2.Value = maxDate;


            listView1.View = View.Details;
            //columnları ayarla
            listView1.Columns.Add("Tarih", 55);
            listView1.Columns.Add("Saat", 55);
            listView1.Columns.Add("Nem", 55);
            listView1.Columns.Add("Sensör", 55);
            listView2.View = View.Details;
            listView2.Columns.Add("Tarih", 55);
            listView2.Columns.Add("Saat", 55);
            listView2.Columns.Add("Sıcaklık", 55);
            listView2.Columns.Add("Sensör", 55);

            listView1.Resize += listView_Resize;
            foreach (ColumnHeader c in listView1.Columns)
                c.Width = listView1.Width / 4;
            listView2.Resize += listView_Resize;
            foreach (ColumnHeader c in listView2.Columns)
                c.Width = listView1.Width / 4;
            //this.Sensör2Tablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.DataSource = pt.DT;
            list1ViewNemDoldur(tehlikeNemLink.Text);
            list2ViewSicaklikDoldur(tehlikeliSicLink.Text);
            //ortalamaNem.Text = ArayuzDoldurma.ortalamaNem(pt.DT, minDate, maxDate).ToString();
            //ortalamaSicaklik.Text = ArayuzDoldurma.ortalamaSicaklik(pt.DT, minDate, maxDate).ToString();

        }

        private void dosyaIste()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = "C:\\Users\\yiğido\\Desktop\\2014-2015\\JERA ELECTRONICS";
            dialog.Title = "Select a text file";
            string allText = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fname = dialog.FileName;
                allText = System.IO.File.ReadAllText(fname);
                pt = new ParsingText(allText);

                //System.IO.File.WriteAllText( fname, pt.AllString);
            }
            else
            {
                Close();
            }
        }

        void listView_Resize(object sender, EventArgs e)
        {

            foreach (ColumnHeader c in ((ListView)sender).Columns)
                c.Width = ((ListView)sender).Width / 4;
        }

        private void list1ViewNemDoldur(String filter)
        {
            //gridviewden ve comboboxtan al göster
            listView1.Items.Clear();
            DataTable dt = (DataTable)dataGridView2.DataSource;
            int value = 0
                ;
            //((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            if (value == 0)
            {
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Verilen aralıkta nem verisi bulunamamıştır!", "UYARI",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataRow[] drs = dt.AsEnumerable().Where(row => row.Field<float>("Nem(1.Sensör)") > float.Parse(filter) || row.Field<float>("Nem(2.Sensör)") > float.Parse(filter)).ToArray();
                //Select(("Nem(1.Sensör) > '" + filter + "'" + "OR" + " Nem(2.Sensör) > '" + filter + "'"));

                foreach (DataRow dr in drs)
                {

                    if (dr.Field<float>("Nem(1.Sensör)") > dr.Field<float>("Nem(2.Sensör)"))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = dr.Field<DateTime>(0).ToShortDateString();
                        lvi.SubItems.Add(dr.Field<DateTime>(0).ToShortTimeString());
                        lvi.SubItems.Add(dr.Field<float>("Nem(1.Sensör)").ToString());
                        lvi.SubItems.Add("1. Sensör");
                        listView1.Items.Add(lvi);

                    }
                    else if (dr.Field<float>("Nem(1.Sensör)") < dr.Field<float>("Nem(2.Sensör)"))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = dr.Field<DateTime>(0).ToShortDateString();
                        lvi.SubItems.Add(dr.Field<DateTime>(0).ToShortTimeString());
                        lvi.SubItems.Add(dr.Field<float>("Nem(2.Sensör)").ToString());
                        lvi.SubItems.Add("2. Sensör");
                        listView1.Items.Add(lvi);


                    }
                    else
                    {
                        ListViewItem lvi1 = new ListViewItem();
                        lvi1.Text = dr.Field<DateTime>(0).ToShortDateString();
                        lvi1.SubItems.Add(dr.Field<DateTime>(0).ToShortTimeString());
                        lvi1.SubItems.Add(dr.Field<float>("Nem(1.Sensör)").ToString());
                        lvi1.SubItems.Add("İkisi de");
                        listView1.Items.Add(lvi1);
                    }

                }
            }


        }



        private void list2ViewSicaklikDoldur(String filter)
        {
            //gridviewden ve comboboxtan al göster
            listView2.Items.Clear();
            DataTable dt = (DataTable)dataGridView2.DataSource;
            int value = 0
                ;
            //((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            if (value == 0)
            {
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Verilen aralıkta nem verisi bulunamamıştır!", "UYARI",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataRow[] drs = dt.AsEnumerable().Where(row => row.Field<float>("Sicaklik(1.Sensör)") > float.Parse(filter) || row.Field<float>("Sicaklik(2.Sensör)") > float.Parse(filter)).ToArray();

                //Select(("Sicaklik(1.Sensör) > '" + filter + "'" + "OR" + " Sicaklik(2.Sensör) > '" + filter + "'"));

                foreach (DataRow dr in drs)
                {

                    if (dr.Field<float>("Sicaklik(1.Sensör)") > dr.Field<float>("Sicaklik(2.Sensör)"))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = dr.Field<DateTime>(0).ToShortDateString();
                        lvi.SubItems.Add(dr.Field<DateTime>(0).ToShortTimeString());
                        lvi.SubItems.Add(dr.Field<float>("Sicaklik(1.Sensör)").ToString());
                        lvi.SubItems.Add("1. Sensör");
                        listView2.Items.Add(lvi);

                    }
                    else if (dr.Field<float>("Sicaklik(1.Sensör)") < dr.Field<float>("Sicaklik(2.Sensör)"))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = dr.Field<DateTime>(0).ToShortDateString();
                        lvi.SubItems.Add(dr.Field<DateTime>(0).ToShortTimeString());
                        lvi.SubItems.Add(dr.Field<float>("Sicaklik(2.Sensör)").ToString());
                        lvi.SubItems.Add("2. Sensör");
                        listView2.Items.Add(lvi);


                    }
                    else
                    {
                        ListViewItem lvi1 = new ListViewItem();
                        lvi1.Text = dr.Field<DateTime>(0).ToShortDateString();
                        lvi1.SubItems.Add(dr.Field<DateTime>(0).ToShortTimeString());
                        lvi1.SubItems.Add(dr.Field<float>("Sicaklik(1.Sensör)").ToString());
                        lvi1.SubItems.Add("İkisi de");
                        listView2.Items.Add(lvi1);
                    }

                }
            }


        }
        private void tehlikeNemLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string value = "";
            if (InputBox.Show("Nem Değeri Girişi",
     "&Lütfen tehlikeli nem sınır değerini giriniz", ref value) == DialogResult.OK)
            {
                tehlikeNemLink.Text = value;
            }
            ////iki değer ile tehlikeli listesi güncellenir
            list1ViewNemDoldur(tehlikeNemLink.Text);

        }

        private void tehlikeliSicLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            string value = "";
            if (InputBox.Show("Sıcaklık Değeri Girişi",
     "&Lütfen tehlikeli sıcaklık sınır değerini giriniz", ref value) == DialogResult.OK)
            {
                tehlikeliSicLink.Text = value;
            }
            ////iki değer ile tehlikeli listesi güncellenir
            list2ViewSicaklikDoldur(tehlikeliSicLink.Text);
        }

        private void çıkartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Document doc = new Document();
            //PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Test.pdf", FileMode.Create));
            SaveFileDialog comDialog = new SaveFileDialog();
            comDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); ;
            comDialog.Filter = "pdf files (*.pdf)|*.pdf";
            comDialog.FileName = "ÇIKTI.pdf";
            comDialog.Title = "Nereye Çıkacak?";
            string allText = "";
            //CıktıAl ca = new CıktıAl();

            if (comDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = (FileStream)comDialog.OpenFile();
                CıktıAl.createPdf(fs, "NET ÖLÇER", new DateTime[] { dateTimePicker1.Value, dateTimePicker2.Value },
                    new Double[] { Double.Parse(ortalamaNem.Text), Double.Parse(ortalamaSicaklik.Text) },
                    ((DataTable)dataGridView2.DataSource));

            }

        }



        private void FiltreButton_Click(object sender, EventArgs e)
        {
            //tarih filtresi
            if (pt == null) return;

            DateTime ilkDeger = new DateTime();
            DateTime sonDeger = new DateTime();
            ilkDeger = dateTimePicker1.Value;
            sonDeger = dateTimePicker2.Value;

            // iki deger alınır, ilki oteksiinden büyük degilse hata
            if (ilkDeger > sonDeger)
            {
                MessageBox.Show("İLK TARİH DAHA KÜÇÜK OLMALIDIR", "UYARI");
                return;
            }
            //kullanıcı kayıtlı olan son ve ilk tarihten ötesini seçememeli

            //iki değer ile ortalamalar değiştirilir
            int value = 0;
            //((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;
            //ortalamaNem.Text = ArayuzDoldurma.ortalamaNem(pt.DT, ilkDeger, sonDeger).ToString();
            //ortalamaSicaklik.Text = ArayuzDoldurma.ortalamaSicaklik(pt.DT, ilkDeger, sonDeger).ToString();

            //iki değer ile tablolar güncellenir -- HATA : ad den al ki combobox kontrollünü gözden kaçırma
            double aralik = ((KeyValuePair<double, string>)comboBoxVeriAraligi.SelectedItem).Key;

            DataRow[] _newDataRown = pt.DT.Select(("Tarih >= '" + ilkDeger.ToString() + "' AND Tarih <= '" + sonDeger.ToString() + "'"));

            List<DataRow> datarows = new List<DataRow>();
            DateTime aralikBasi = ilkDeger;

            foreach (DataRow row in _newDataRown)
            {
                DateTime rowDate = row.Field<DateTime>("Tarih");
                if (rowDate == aralikBasi || (rowDate - aralikBasi).Minutes >= aralik)
                {
                    aralikBasi = rowDate;

                    datarows.Add(row);
                }
            }
            dataGridView2.DataSource = datarows.ToArray().CopyToDataTable();
            list1ViewNemDoldur(tehlikeNemLink.Text);
            list2ViewSicaklikDoldur(tehlikeliSicLink.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = pt.DT;
        }

        private void xlCiktiButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "excell files (*.xls)|*.xls|All files (*.xlsx*)|*.xlsx*";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Title = "Lütfen kayıtları aktaracağınız xl dosyasını seçiniz";
            string allText = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string xlpath = dialog.FileName;
                DataTable filtrelenmisTable = (DataTable)(dataGridView2.DataSource);
                
                    InputBox.ExportToExcel(filtrelenmisTable, xlpath);
                
                dialog.OpenFile();
            }
        }












    }
    static class InputBox
    {
        /// <summary>
        /// Displays a dialog with a prompt and textbox where the user can enter information
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="promptText">Dialog prompt</param>
        /// <param name="value">Sets the initial value and returns the result</param>
        /// <returns>Dialog result</returns>
        /// 
        public static DialogResult Show(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public static void ExportToExcel(this DataTable Tbl, string ExcelFilePath = null)
        {
            try
            {

                if (Tbl == null || Tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb= excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // column headings
                for (int i = 0; i < Tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, (i + 1)] = Tbl.Columns[i].ColumnName;
                }

                // rows
                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {
                        if (j == 0)
                        {
                            workSheet.Cells[(i + 2), (j + 1)] = ((DateTime)Tbl.Rows[i][j]).ToOADate();
                        }
                        else
                            workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                    }
                }
                Microsoft.Office.Interop.Excel.Range firstColumn = workSheet.UsedRange.Columns[1];
             //   firstColumn.NumberFormat = "dd/MM/yyyy hh:mm";

                //grafik yazılımları ----------------------------------------------
              //  Microsoft.Office.Interop.Excel._Worksheet workSheetGrafik = wb.Worksheets.Add();
              //  Microsoft.Office.Interop.Excel.ChartObjects xlCharts = (Microsoft.Office.Interop.Excel.ChartObjects)workSheetGrafik.ChartObjects(Type.Missing);
              //  Microsoft.Office.Interop.Excel.ChartObject myChart = (Microsoft.Office.Interop.Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
              //  Microsoft.Office.Interop.Excel.Chart chartPage = myChart.Chart;
              //  myChart.Select();
              //  chartPage.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlXYScatterLines;
              ////  chartPage.SetSourceData(workSheet.Range["A2:B25"]);
              //  Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection = chartPage.SeriesCollection();

              //  Microsoft.Office.Interop.Excel.Series series1 = seriesCollection.NewSeries();
              //  //series1.Values = "='Sayfa1'!R" + 2 + "C" + 2 + ":R" + 22 + "C" + 2 + ""; // Reference the Y Axis data
              //  //series1.XValues = "='Sayfa1'!R" + 2 + "C" + 1 + ":R" + 22 + "C" + 1 + ""; // Reference the X Axis data

              //  series1.XValues = workSheet.UsedRange.Columns[1];
              //  series1.Values = workSheet.UsedRange.Columns[2];
                //-------------------------------------------------------------------

// As you three series, you will have to replicate the above coding for two more lines. Consider above series referred for OUR,
// introduce two more series here for warranty, sales

                //series1.XValues = timeSeriesRange; 
                
                //series1.Values = workSheet.get_Range("B2", "B15");          // check fielpath
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                    workSheet.SaveAs(ExcelFilePath);
                    excelApp.Quit();
                    //releaseObject(workSheetGrafik);
                    //releaseObject(workSheet);

                    //releaseObject(xlWorkBook);
                    //releaseObject(excelApp);
                    MessageBox.Show("Excel file saved!");


                    Process.Start(ExcelFilePath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    excelApp.Visible = true;
                }
                }
                catch (Exception ex)
                {
                    
                     throw new Exception("ExportToExcel: \n" + ex.Message);
                }
           
            }

        private static string GetMacro()
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("Sub FormatSheet()" + "\n");
            sb.Append("  Range(\"A6:B13\").Select " + "\n");
            sb.Append("  Selection.Font.ColorIndex = 3" + "\n");
            sb.Append("End Sub");

            return sb.ToString();
        }
    
    
    }

     
    }

