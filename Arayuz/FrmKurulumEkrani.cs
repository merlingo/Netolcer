using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Arayuz
{
    public partial class FrmKurulumEkrani : Form
    {
        List<DataLogger> DataLoggerlar = new List<DataLogger>();
        string path = Application.StartupPath + "\\DataLoggerListesi.xml";
        public DataLoggerListesi dll;
        public FrmKurulumEkrani()
        {
            Icon icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\unnamed.ico");
            this.Icon = icon;
            InitializeComponent();
            initGrid();
            this.gvDataLoggerListesi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gvDataLoggerListesi.CellClick +=gvDataLoggerListesi_CellContentClick;
            DataLoggerListesi dll = new DataLoggerListesi();
            GuncelleBtn.Hide();
        }
        public FrmKurulumEkrani(DataLoggerListesi dataLoggerlist)
        {
            Icon icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\unnamed.ico");
            this.Icon = icon;
            dll = dataLoggerlist;
            InitializeComponent();
            txtAdress.Text= dll.Adres;
             txtKurumAdi.Text=dll.KurumAdi;
            txtMail.Text= dll.Mail;
            txtSehir.Text=dll.Sehir;
            button1.Hide();
            KurulumTamamlaBtn.Hide();
            groupBox1.Hide();
            initGrid();
            this.gvDataLoggerListesi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gvDataLoggerListesi.CellClick += gvDataLoggerListesi_CellContentClick;
        }
        private void initGrid()
        {
            gvDataLoggerListesi.Columns.Add("TelNo", "Tel. No");
            gvDataLoggerListesi.Columns.Add("DataLoggerAdi", "Data Logger Adı");
            gvDataLoggerListesi.Columns.Add("SensorSayisi", "Sensör Sayısı");
            gvDataLoggerListesi.Columns.Add("TxtFileName", "Txt Dosyası Yolu");
            gvDataLoggerListesi.Columns.Add("Degistir", "Degistir");
            gvDataLoggerListesi.Columns.Add("Sil", "Sil");

        }
        private void gvDataLoggerListesi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex==4 && e.RowIndex >= 0)
            {
                int i = gvDataLoggerListesi.Rows[e.RowIndex].Index;
                DataLogger dl = (DataLogger)gvDataLoggerListesi.Rows[e.RowIndex].Cells[0].Tag;
                if (dl != null)
                {
                    FrmDataLoggerEkle fdle = new FrmDataLoggerEkle(dl);
                    if (fdle.ShowDialog() == DialogResult.OK)
                        DataLoggerDegistir(fdle.dl, dl.Isim, i);
                }
            }
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                if (MessageBox.Show("Silmek istediğinize Emin misiniz?","Data Logger Silme Onayı",MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    int i = gvDataLoggerListesi.Rows[e.RowIndex].Index;
                    DataLogger dl = (DataLogger)gvDataLoggerListesi.Rows[e.RowIndex].Tag;
                    gvDataLoggerListesi.Rows.RemoveAt(e.RowIndex);
                    DataLoggerlar.Remove(dl);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FrmDataLoggerEkle fdle = new FrmDataLoggerEkle();
            if (DialogResult.OK == fdle.ShowDialog())
            {
                DataLoggerKaydet(fdle.dl);
            }
           
        }
        private void XmleYaz()
        {
           

            XmlSerializer serializer = new XmlSerializer(typeof(DataLoggerListesi));
            FileStream fs = new FileStream(path, FileMode.Create);
            serializer.Serialize(fs, dll);
            fs.Close(); 
        }
        void dllOlustur()
        {
            dll.DataLoggerlar = DataLoggerlar;
            dll.Adres = txtAdress.Text;
            dll.KurumAdi = txtKurumAdi.Text;
            dll.Mail = txtMail.Text;
            dll.Sehir = txtSehir.Text;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dllOlustur();
            XmleYaz();
            FrmAnaEkran anaEkran = new FrmAnaEkran();
            anaEkran.Show();
            Hide();
        }





        private void DataLoggerKaydet(DataLogger dl)
        {
            DataLoggerlar.Add(dl);
            int r = gvDataLoggerListesi.Rows.Add();
            gvDataLoggerListesi.Rows[r].Cells[0].Tag = dl;
            gvDataLoggerListesi.Rows[r].Cells["TelNo"].Value = dl.TelefonNumarasi;
            gvDataLoggerListesi.Rows[r].Cells["DataLoggerAdi"].Value = dl.Isim;
            gvDataLoggerListesi.Rows[r].Cells["SensorSayisi"].Value = dl.SensorSayisi;
            gvDataLoggerListesi.Rows[r].Cells["TxtFileName"].Value = dl.TxtPath;
            DataGridViewButtonCell buttonCellDegistir = new DataGridViewButtonCell();
            buttonCellDegistir.Value = "Değiştir";
            gvDataLoggerListesi.Rows[r].Cells["Degistir"]=buttonCellDegistir;
            DataGridViewButtonCell buttonCellSil = new DataGridViewButtonCell();
            
            buttonCellSil.Value ="Sil";
            gvDataLoggerListesi.Rows[r].Cells["Sil"] = buttonCellSil;


        }
        private void DataLoggerDegistir(DataLogger dl,string Isim, int r)
        {
            DataLoggerlar.Remove(DataLoggerlar.Find(x=>x.Isim==Isim));
            DataLoggerKaydet(dl);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //seçilen kroki datalogger krokipath'e eklenecek.
            FileStream fs = new FileStream(path, FileMode.Append);

        }

        private void GuncelleBtn_Click(object sender, EventArgs e)
        {
            dll.Adres = txtAdress.Text;
            dll.KurumAdi = txtKurumAdi.Text;
            dll.Mail = txtMail.Text;
            dll.Sehir = txtSehir.Text;
            XmleYaz();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
      
    }
}
