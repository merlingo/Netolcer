using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arayuz
{
    public partial class TehlikeliDeger : Form
    {
        public double tehlikeliDeger = 0;
        Func<NetOlcerBirimi, bool> filterFunc;
        DataLogger dl;
        public TehlikeliDeger(DataLogger dl, string neTehlikesi)
        {
            this.dl = dl;
            InitializeComponent();
            List<NetOlcerBirimi> tehlikeliDegerler=new List<NetOlcerBirimi>();
            if (neTehlikesi == "ys")//Yuksek Sicaklik
            {
                //label ve değer yazılır o deger üstünde olan değerler listelenir.
                labelTehlikeliDeger.Text = "Tehlikeli Yüksek Sıcaklık Değer:";
                tehlikeliDeger =dl.TehlikeliUstSicaklik;
                filterFunc = (x => x.Tip == "isi" && x.Deger > tehlikeliDeger && x.SensorIndex==0);

            }
            else if (neTehlikesi == "ds")//Dusuk Sicaklik
            {
                labelTehlikeliDeger.Text = "Tehlikeli Düşük Sıcaklık Değer:";
                tehlikeliDeger = dl.TehlikeliAltSicaklik;
                filterFunc = (x => x.Tip == "isi" && x.Deger < tehlikeliDeger && x.SensorIndex == 0);
            }
            else if (neTehlikesi == "yn")//Yuksek Nem
            {
                labelTehlikeliDeger.Text = "Tehlikeli Yüksek Nem Değer:";
                tehlikeliDeger = dl.TehlikeliUstNem;
                filterFunc = (x => x.Tip == "nem" && x.Deger > tehlikeliDeger && x.SensorIndex == 0);
            }
            else if (neTehlikesi == "dn")//Dusuk Nem
            {
                labelTehlikeliDeger.Text = "Tehlikeli Düşük Nem Değer:";
                tehlikeliDeger = dl.TehlikeliAltNem;
                filterFunc = (x => x.Tip == "nem" && x.Deger < tehlikeliDeger && x.SensorIndex == 0);
            }
            else if (neTehlikesi == "ys2")//Yuksek Sicaklik
            {
                //label ve değer yazılır o deger üstünde olan değerler listelenir.
                labelTehlikeliDeger.Text = "Tehlikeli Yüksek Sıcaklık Değer:";
                tehlikeliDeger = dl.TehlikeliUstSicaklik2;
                filterFunc = (x => x.Tip == "isi" && x.Deger > tehlikeliDeger && x.SensorIndex == 1);

            }
            else if (neTehlikesi == "ds2")//Dusuk Sicaklik
            {
                labelTehlikeliDeger.Text = "Tehlikeli Düşük Sıcaklık Değer:";
                tehlikeliDeger = dl.TehlikeliAltSicaklik2;
                filterFunc = (x => x.Tip == "isi" && x.Deger < tehlikeliDeger && x.SensorIndex == 1);
            }
            else if (neTehlikesi == "yn2")//Yuksek Nem
            {
                labelTehlikeliDeger.Text = "Tehlikeli Yüksek Nem Değer:";
                tehlikeliDeger = dl.TehlikeliUstNem2;
                filterFunc = (x => x.Tip == "nem" && x.Deger > tehlikeliDeger && x.SensorIndex == 1);
            }
            else if (neTehlikesi == "dn2")//Dusuk Nem
            {
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

        private void txtTehlikeliDeger_TextChanged(object sender, EventArgs e)
        {
            List<NetOlcerBirimi> tehlikeliDegerler = new List<NetOlcerBirimi>();
            //listeyi yeniler ve değeri değiştirir
            if (!Double.TryParse(txtTehlikeliDeger.Text, out tehlikeliDeger))
            {
                txtTehlikeliDeger.BackColor = Color.OrangeRed;
                
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

       
    }
}
