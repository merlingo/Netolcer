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
    public partial class FrmPdfCikti : Form
    {
        DataLoggerListesi dataloggerlar;
        public FrmPdfCikti(DataLoggerListesi dataloggerlar)
        {
            this.dataloggerlar = dataloggerlar;
            InitializeComponent();
            foreach (DataLogger dl in dataloggerlar.DataLoggerlar)
            {
                
                CiktiDataLoggerItem cbdli = new CiktiDataLoggerItem(dl);
                checkedListBoxDataLogger.Items.Add(cbdli, cbdli.secilebilir);
            }

            checkedListBoxDataLogger.MouseDown += RightClicked;
            checkedListBoxDataLogger.ItemCheck += checkedListBoxDataLogger_ItemCheck;
            string[] optionCheckArray = {"Genel İstatistikler",
                                        "Her Datalogger Icin Istatistikler",
                                        "Datalogger Son Kalibrasyon Tarihleri",
                                        "Genel Grafik",
                                        "Her Datalogger Için Grafik" };
            for (int x = 0; x < optionCheckArray.Length; x++)
            {
                checkedListBox1.Items.Add(optionCheckArray[x], true);
            }
            Dictionary<double, string> VeriAraliklari = new Dictionary<double, string>();
            VeriAraliklari.Add(0, "1 dk");
            //VeriAraliklari.Add(0.5, "30 sn"); //tasarım dk ustune yapıldı
            VeriAraliklari.Add(5, "5 dk");
            VeriAraliklari.Add(10, "10 dk");
            VeriAraliklari.Add(20, "20 dk");
            VeriAraliklari.Add(30, "30 dk");
            VeriAraliklari.Add(60, "1 saat");

            comboBoxVeriAraligi.DataSource = new BindingSource(VeriAraliklari, null);
            comboBoxVeriAraligi.DisplayMember = "Value";
            comboBoxVeriAraligi.ValueMember = "Key";
        }

        void checkedListBoxDataLogger_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CiktiDataLoggerItem item = (CiktiDataLoggerItem)((CheckedListBox)sender).Items[e.Index];
            if(!item.secilebilir)
            {
                MessageBox.Show("UYARI:"+item.DataLoggerAdi +" nin çıktısını almak için veri yüklemelisiniz");
                e.NewValue = CheckState.Unchecked;
            }
        }

        private void RightClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = ((CheckedListBox)sender).IndexFromPoint(e.X, e.Y);
                CiktiDataLoggerItem item = (CiktiDataLoggerItem)checkedListBoxDataLogger.Items[index];
                if (!item.secilebilir)
                {
                    MessageBox.Show("UYARI:" + item.DataLoggerAdi + " nin çıktısını almak için veri yüklemelisiniz");
                    return;
                }
                //tarih girme ekranı - kullanıcı tarih değiştirsin diye.
                FrmCiktiTarihDegistir fctd = new FrmCiktiTarihDegistir(item.BaslangicTarihi, item.SonlanisTarihi);
               
                DialogResult dr = fctd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    item.BaslangicTarihi = fctd.baslangic;
                    item.SonlanisTarihi = fctd.bitiş;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //seçilen ayarlar ile pdf dokumanı oluşturulacak.
            SaveFileDialog comDialog = new SaveFileDialog();
            comDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); ;
            comDialog.Filter = "pdf files (*.pdf)|*.pdf";
            comDialog.FileName = "Netölçer Çıktısı.pdf";
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
                    MessageBox.Show("Rapor alma işlemi fazla veri olmasından dolayı uzun sürmektedir. Lütfen işlem tamamlanmadan kapatmayınız. İşlem tamamlandığında rapor pdf formatında açılacaktır.");
                    NetOlcerCiktiOptions cikiOptions = optionsAl();
                    if (checkBoxVerileriAyarla.Checked)
                        TehlikelileriDegistir(cikiOptions);
                    CıktıAl cikti = new CıktıAl(cikiOptions, dataloggerlar);
                    cikti.createPdf(fs);
                }
                catch
                {
                    MessageBox.Show("Pdf oluşturulamadı!!");
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    fs.Close();
                    MessageBox.Show("Rapor alma işlemi tamamlanmıştır");

                    Process.Start(fs.Name);
                    
                }
            }
        }
        static double fark;

        public void TehlikelileriDegistir(NetOlcerCiktiOptions opt)
        {
            // aralıktaki tehlikeli değer altında kalan en küçük değer bulunur. aradaki fark kadar tüm veriler büyütülür eğer üst sınır aşılmazsa. aşılırsa hata ver
            foreach (var dl in dataloggerlar.DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                CiktiDataLoggerItem cdli = opt.CiktiIcinDataLoggerlar.First(x => x.DataLoggerAdi == dl.Isim);
                for (int i = 0; i < 2; i++)
                {
                    List<NetOlcerBirimi> tehlikeliler = dl.SensorListesi[i]._degerler.Where(x => x.Zaman > cdli.BaslangicTarihi && x.Zaman < cdli.SonlanisTarihi && x.Deger < dl.TehlikeliAltSicaklik&&x.Tip=="isi").ToList();
                    if(tehlikeliler.Count<1)
                        continue;
                    Random randomVal = new Random();
                    fark = (dl.TehlikeliAltSicaklik - tehlikeliler.Min(x => x.Deger)) + randomVal.NextDouble();
                    
                    //List<NetOlcerBirimi> yeniDegerler = dl.SensorListesi[i]._degerler.Where(x => x.Zaman > cdli.BaslangicTarihi && x.Zaman < cdli.SonlanisTarihi && x.Deger < dl.TehlikeliAltSicaklik && x.Tip == "isi")
                    //    .Select(x => new NetOlcerBirimi { Birim = x.Birim, Deger = x.Deger + fark, Zaman = x.Zaman, Sensor = x.Sensor, Tip = x.Tip, SensorIndex = x.SensorIndex }).ToList();
                    //dl.SensorListesi[i]._degerler = yeniDegerler;
                    //foreach (NetOlcerBirimi deger in dl.SensorListesi[i]._degerler.Where(x => x.Zaman > cdli.BaslangicTarihi && x.Zaman < cdli.SonlanisTarihi && x.Deger < dl.TehlikeliAltSicaklik && x.Tip == "isi").ToList())
                    //{
                    //    deger.Deger = fark;
                    //    //deger = new NetOlcerBirimi { Deger = deger.Deger + fark, Birim = deger.Birim, Sensor = deger.Sensor, SensorIndex = deger.SensorIndex, Tip = deger.Tip, Zaman = deger.Zaman };
                    //}
                    //dl.SensorListesi[i]._degerler.Where(x => x.Zaman > cdli.BaslangicTarihi && x.Zaman < cdli.SonlanisTarihi && x.Deger < dl.TehlikeliAltSicaklik && x.Tip == "isi").ToList().ForEach(farkKadarDegistir);
                    for (int j = 0; j < dl.SensorListesi[i]._degerler.Count; j++)
                    {
                        NetOlcerBirimi nob = dl.SensorListesi[i]._degerler[j];
                        if (nob.Tip=="isi")
                        {
                            nob.Deger +=fark;
                            dl.SensorListesi[i]._degerler[j] = nob;
                        }
                    }
                }
            }
        }
        private static void farkKadarDegistir(NetOlcerBirimi nob)
        {
            nob.Deger += fark;
        }
        public NetOlcerCiktiOptions optionsAl()
        {
            //            Genel İstatistikler
            //Her Datalogger İçin İstatistikler
            //Datalogger Son Kalibrasyon Tarihleri
            //Genel Grafik
            //Her Datalogger İçin Grafik
            NetOlcerCiktiOptions cikiOptions = new NetOlcerCiktiOptions();
            foreach (var si in checkedListBox1.CheckedItems)
            {
                if (si.ToString() == "Genel İstatistikler")
                {
                    cikiOptions.GenelIstatistiklerVarmi = true;
                }
                else if (si.ToString() == "Her Datalogger Icin Istatistikler")
                {
                    cikiOptions.DataLoggerIstatistiklerVarmi = true;
                }
                else if (si.ToString() == "Datalogger Son Kalibrasyon Tarihleri")
                {
                    cikiOptions.DataLoggerSonKalibrasyonTarihleriVarmi = true;
                }
                else if (si.ToString() == "Genel Grafik")
                {
                    cikiOptions.GenelGrafiklerVarmi = true;
                }
                else if (si.ToString() == "Her Datalogger Için Grafik")
                {
                    cikiOptions.DataLoggerGrafiklerVarmi = true;
                }
            }
            
            cikiOptions.CiktiIcinDataLoggerlar.AddRange(checkedListBoxDataLogger.CheckedItems.OfType<CiktiDataLoggerItem>().ToList());
            if (checkBox1.Checked)
            {
                foreach (var x in cikiOptions.CiktiIcinDataLoggerlar)
                {
                    x.BaslangicTarihi = dateTimePicker1.Value;
                    x.SonlanisTarihi = dateTimePicker2.Value;
                }
            }
            cikiOptions.VeriAraligi = ((KeyValuePair<double, string>)comboBoxVeriAraligi.SelectedItem).Key;

            return cikiOptions;
        }
        private static void TarihAta(string s)
        {
            Console.WriteLine(s);
        }
    }
    
    public class CiktiDataLoggerItem
    {
        public string DataLoggerAdi { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime SonlanisTarihi { get; set; }
        public bool secilebilir = true;
        public List<Sensor> sensorListesi;
        public CiktiDataLoggerItem(DataLogger dl)
        {
            DataLoggerAdi = dl.Isim;
            try
            {
                BaslangicTarihi = dl.IlkTarih();
                SonlanisTarihi = dl.SonTarih();
                sensorListesi = dl.SensorListesi;
            }
            catch
            {
                secilebilir = false;
                BaslangicTarihi = DateTime.MinValue;
                SonlanisTarihi = DateTime.MaxValue;

            }
        }
       
        public override string ToString()
        {
            return DataLoggerAdi + " ---->    " + BaslangicTarihi.ToShortDateString() + "  -  " + SonlanisTarihi.ToShortDateString();
        }
    }
}
