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
    public partial class FrmStatistikler : Form
    {
        DataLoggerListesi dataLoggerlar;
        public FrmStatistikler(DataLoggerListesi dll)
        {
            this.dataLoggerlar = dll;
            InitializeComponent();
            foreach (DataLogger dl in dataLoggerlar.DataLoggerlar)
            {
                if (dl.Dt == null)
                    dl.ParsingText();
            }
            genelOrtalamalariHesapla();

        }
        private void genelOrtalamalariHesapla()
        {
            NetOlcerBirimi enYuksekSicaklik = dataLoggerlar.EnYuksekSicaklik();
            lblEnyuksekSicaklikDeger.Text = enYuksekSicaklik.Deger.ToString();
            lblEnyuksekSicaklikZaman.Text = enYuksekSicaklik.Zaman.ToString("dd MMMM yyyy HH mm");
            lblEnyuksekSicaklikSensor.Text = enYuksekSicaklik.Sensor;

            NetOlcerBirimi enDusukSicaklik = dataLoggerlar.EnDusukSicaklik();
            lblEnDusukSicaklikDeger.Text = enDusukSicaklik.Deger.ToString();
            lblEnDusukSicaklikZaman.Text = enDusukSicaklik.Zaman.ToString("dd MMMM yyyy HH mm");
            lblEndusukSicaklikSensor.Text = enDusukSicaklik.Sensor;

            NetOlcerBirimi enYuksekNem = dataLoggerlar.EnYuksekNem();
            lblEnyuksekNemDeger.Text = enYuksekNem.Deger.ToString();
            lblEnyuksekNemZaman.Text = enYuksekNem.Zaman.ToString("dd MMMM yyyy HH mm");
            lblEnyuksekNemSensor.Text = enYuksekNem.Sensor;

            NetOlcerBirimi enDusukNem = dataLoggerlar.EnDusukNem();
            lblEndusukNemDeger.Text = enDusukNem.Deger.ToString();
            lblEndusukNemZaman.Text = enDusukNem.Zaman.ToString("dd MMMM yyyy HH mm");
            lblEndusukNemsensor.Text = enDusukNem.Sensor;

            lblOrtalamaSicaklik.Text = dataLoggerlar.OrtalamaSicaklik().ToString();
            lblOrtalamaNem.Text = dataLoggerlar.OrtalamaNem().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
