using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arayuz
{
    public partial class TehlikeliDeger : Form
    {
        public double tehlikeliDeger = 0;
        int sensorindex=0;
        Func<NetOlcerBirimi, bool> filterFunc;
        DataLogger dl;
        string tehlikeYazisi;
        public TehlikeliDeger(DataLogger dl, string neTehlikesi)
        {
            this.dl = dl;
            InitializeComponent();
            tehlikeliDegerler=new List<NetOlcerBirimi>();
            if (neTehlikesi == "ys")//Yuksek Sicaklik
            {
                tehlikeYazisi = "Yuksek Sicaklik";
                sensorindex = 0;
                //label ve değer yazılır o deger üstünde olan değerler listelenir.
                labelTehlikeliDeger.Text = "Tehlikeli Yuksek Sicaklik Değer:";
                tehlikeliDeger =dl.TehlikeliUstSicaklik;
                filterFunc = (x => x.Tip == "isi" && x.Deger > tehlikeliDeger && x.SensorIndex==0);

            }
            else if (neTehlikesi == "ds")//Dusuk Sicaklik
            {
                tehlikeYazisi = "Dusuk Sicaklik";
                sensorindex = 0;

                labelTehlikeliDeger.Text = "Tehlikeli Düşük Sıcaklık Değer:";
                tehlikeliDeger = dl.TehlikeliAltSicaklik;
                filterFunc = (x => x.Tip == "isi" && x.Deger < tehlikeliDeger && x.SensorIndex == 0);
            }
            else if (neTehlikesi == "yn")//Yuksek Nem
            {
                tehlikeYazisi = "Yuksek Nem";
                sensorindex = 0;

                labelTehlikeliDeger.Text = "Tehlikeli Yüksek Nem Değer:";
                tehlikeliDeger = dl.TehlikeliUstNem;
                filterFunc = (x => x.Tip == "nem" && x.Deger > tehlikeliDeger && x.SensorIndex == 0);
            }
            else if (neTehlikesi == "dn")//Dusuk Nem
            {
                tehlikeYazisi = "Dusuk Nem";
                sensorindex = 0;

                labelTehlikeliDeger.Text = "Tehlikeli Düşük Nem Değer:";
                tehlikeliDeger = dl.TehlikeliAltNem;
                filterFunc = (x => x.Tip == "nem" && x.Deger < tehlikeliDeger && x.SensorIndex == 0);
            }
            else if (neTehlikesi == "ys2")//Yuksek Sicaklik
            {
                tehlikeYazisi = "Yuksek Sicaklik";
                sensorindex = 1;

                //label ve değer yazılır o deger üstünde olan değerler listelenir.
                labelTehlikeliDeger.Text = "Tehlikeli Yüksek Sıcaklık Değer:";
                tehlikeliDeger = dl.TehlikeliUstSicaklik2;
                filterFunc = (x => x.Tip == "isi" && x.Deger > tehlikeliDeger && x.SensorIndex == 1);

            }
            else if (neTehlikesi == "ds2")//Dusuk Sicaklik
            {
                tehlikeYazisi = "Dusuk Sicaklik";
                sensorindex = 1;

                labelTehlikeliDeger.Text = "Tehlikeli Düşük Sıcaklık Değer:";
                tehlikeliDeger = dl.TehlikeliAltSicaklik2;
                filterFunc = (x => x.Tip == "isi" && x.Deger < tehlikeliDeger && x.SensorIndex == 1);
            }
            else if (neTehlikesi == "yn2")//Yuksek Nem
            {
                tehlikeYazisi = "Yuksek Nem";
                sensorindex = 1;

                labelTehlikeliDeger.Text = "Tehlikeli Yüksek Nem Değer:";
                tehlikeliDeger = dl.TehlikeliUstNem2;
                filterFunc = (x => x.Tip == "nem" && x.Deger > tehlikeliDeger && x.SensorIndex == 1);
            }
            else if (neTehlikesi == "dn2")//Dusuk Nem
            {
                tehlikeYazisi = "Dusuk Nem";
                sensorindex = 1;

                labelTehlikeliDeger.Text = "Tehlikeli Düşük Nem Değer:";
                tehlikeliDeger = dl.TehlikeliAltNem2;
                filterFunc = (x => x.Tip == "nem" && x.Deger < tehlikeliDeger && x.SensorIndex == 1);
            }
            else
            {
               DialogResult dr= MessageBox.Show("Hatalı giriş oldu lütfen tekrar giriniz", "HATA", MessageBoxButtons.OK);
               if (dr == DialogResult.OK)
               {
                   Close();
               }
            }
            tehlikeliDegerler = dl.SensorListesi[0]._degerler.Where(filterFunc).ToList();
            tehlikeliDegerler.AddRange(dl.SensorListesi[1]._degerler.Where(filterFunc).ToList());
            txtTehlikeliDeger.Text = tehlikeliDeger.ToString();
            BindingSource Source = new BindingSource();
            Source.DataSource = tehlikeliDegerler;
            dataGridViewTehlikeliDegerler.DataSource = Source;

            txtTehlikeliDeger.TextChanged += txtTehlikeliDeger_TextChanged;
        }

        List<NetOlcerBirimi> tehlikeliDegerler = new List<NetOlcerBirimi>();
        private void txtTehlikeliDeger_TextChanged(object sender, EventArgs e)
        {
            tehlikeliDegerler = new List<NetOlcerBirimi>();
            //listeyi yeniler ve değeri değiştirir
            if (!Double.TryParse(txtTehlikeliDeger.Text, out tehlikeliDeger))
            {
                txtTehlikeliDeger.BackColor = Color.OrangeRed;
                tehlikeliDeger = -1;
            }
            else
            {
                txtTehlikeliDeger.BackColor = Color.White;

                tehlikeliDegerler = dl.SensorListesi[0]._degerler.Where(filterFunc).ToList();
                tehlikeliDegerler.AddRange(dl.SensorListesi[1]._degerler.Where(filterFunc).ToList());
                BindingSource Source = new BindingSource();
                Source.DataSource = tehlikeliDegerler;
                dataGridViewTehlikeliDegerler.DataSource = Source;
            }
        }

        private void buttonCikti_Click(object sender, EventArgs e)
        {
            //ekrandaki değerlerin çıktısı alınır
            SaveFileDialog comDialog = new SaveFileDialog();
            comDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); ;
            comDialog.Filter = "pdf files (*.pdf)|*.pdf";
            comDialog.FileName = "Netölçer Çıktısı "+tehlikeYazisi+".pdf";
            comDialog.Title = "Nereye Çıkacak?";
            string allText = "";
            //CıktıAl ca = new CıktıAl();

            if (comDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = (FileStream)comDialog.OpenFile();
                //listeden seçili olan dataloggerı datalogger olarak koy
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    string sensorad = dl.SensorListesi[sensorindex].Isim;
                    CıktıAl.tehlikeliDegerCikti(tehlikeliDegerler, dl.Isim, sensorad, fs, tehlikeYazisi);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Rapor alma işlemi tamamlanmıştır");
                    Process.Start(fs.Name);
                    // cikti.createPdf(fs);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pdf oluşturulamadı!!" + ex.Message);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    fs.Close();


                }

            }

        }
    }
}
