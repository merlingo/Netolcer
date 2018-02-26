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
        DataLogger dataLogger;
        public FrmStatistikler(DataLogger dl)
        {
            this.dataLogger = dl;
            InitializeComponent();
            header.Text = dl.Isim + " - ISTATISTIKLER";
            genelOrtalamalariHesapla();

        }
        private void genelOrtalamalariHesapla()
        {
            NetOlcerBirimi enYuksekSicaklik = dataLogger.EnYuksekSicaklik();
            lblEnyuksekSicaklikDeger.Text = enYuksekSicaklik.Deger.ToString();
            lblEnyuksekSicaklikZaman.Text = enYuksekSicaklik.Zaman.ToString("dd MMMM yyyy HH mm");
            lblEnyuksekSicaklikSensor.Text = enYuksekSicaklik.Sensor;

            NetOlcerBirimi enDusukSicaklik = dataLogger.EnDusukSicaklik();
            lblEnDusukSicaklikDeger.Text = enDusukSicaklik.Deger.ToString();
            lblEnDusukSicaklikZaman.Text = enDusukSicaklik.Zaman.ToString("dd MMMM yyyy HH mm");
            lblEndusukSicaklikSensor.Text = enDusukSicaklik.Sensor;

            NetOlcerBirimi enYuksekNem = dataLogger.EnYuksekNem();
            lblEnyuksekNemDeger.Text = enYuksekNem.Deger.ToString();
            lblEnyuksekNemZaman.Text = enYuksekNem.Zaman.ToString("dd MMMM yyyy HH mm");
            lblEnyuksekNemSensor.Text = enYuksekNem.Sensor;

            NetOlcerBirimi enDusukNem = dataLogger.EnDusukNem();
            lblEndusukNemDeger.Text = enDusukNem.Deger.ToString();
            lblEndusukNemZaman.Text = enDusukNem.Zaman.ToString("dd MMMM yyyy HH mm");
            lblEndusukNemsensor.Text = enDusukNem.Sensor;

            lblOrtalamaSicaklik.Text = dataLogger.OrtalamaSicaklik().ToString();
            lblOrtalamaNem.Text = dataLogger.OrtalamaNem().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
