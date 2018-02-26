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
    public partial class FrmDataLoggerEkle : Form
    {
        public DataLogger dl;
        public FrmDataLoggerEkle()
        {
            Icon icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\unnamed.ico");
            this.Icon = icon;
            InitializeComponent();
            dl = new DataLogger();
        }

        public FrmDataLoggerEkle(DataLogger dl1)
        {
            // TODO: Complete member initialization
            Icon icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\unnamed.ico");
            this.Icon = icon;
            InitializeComponent();
            this.dl = dl1;
           txtDLAdi.Text  =  dl.Isim ;
            txtSensor1.Text = dl.SensorListesi[0].Isim;
            txtSensor2.Text = dl.SensorListesi[1].Isim;
            textBoxTelefonNumarasi.Text = dl.TelefonNumarasi;
            textBoxAltSicaklik.Text = dl.TehlikeliAltSicaklik.ToString();
            textBoxUstSicaklik.Text = dl.TehlikeliUstSicaklik.ToString();
            textBoxAltNem.Text = dl.TehlikeliAltNem.ToString();
            textBoxUstNem.Text = dl.TehlikeliUstNem.ToString();

            textBoxAltSicaklik2.Text = dl.TehlikeliAltSicaklik2.ToString();
            textBoxUstSicaklik2.Text = dl.TehlikeliUstSicaklik2.ToString();
            textBoxAltNem2.Text = dl.TehlikeliAltNem2.ToString();
            textBoxUstNem2.Text = dl.TehlikeliUstNem2.ToString();
        }

        private void btnSensorEkle_Click(object sender, EventArgs e)
        {
          

           
           // 05465864441 erdal
        }

         void FrmDataLoggerEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            { // Autosave and clear up ressources
                MessageBox.Show("carpi butonundan kapandi");
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                return;
            }
            // Display a MsgBox asking the user to close the form.
            if (txtDLAdi.Text == "" || /*txtDLId.Text == "" ||*/ txtSensor1.Text == "" || txtSensor2.Text == "" || textBoxTelefonNumarasi.Text == "")
            {
                MessageBox.Show("Boş kalan alanı doldurunuz!!");
                e.Cancel = true;
                return;
            }
            if (txtSensor2.Text == txtSensor1.Text)
            {
                MessageBox.Show("Sensor Adları Aynı Olamaz!!");
                e.Cancel = true;
                return;
            }
                // Cancel the Closing event
        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            
            DialogResult = DialogResult.OK;
            dl.Isim = txtDLAdi.Text;
            //dl.Id = txtDLId.Text;
            //dl.SonKalibrasyonTarihi = dateTimeKalibrasyonTarihi.Value;
            dl.TelefonNumarasi = textBoxTelefonNumarasi.Text;
            double ustsic, altsic, ustnem, altnem, ustsic2, altsic2, ustnem2, altnem2;
            if (double.TryParse(textBoxUstSicaklik.Text, out ustsic))
            {
                dl.TehlikeliUstSicaklik = ustsic;
            }
            if (double.TryParse(textBoxAltSicaklik.Text, out altsic))
            {
                dl.TehlikeliAltSicaklik = altsic;
            }
            if (double.TryParse(textBoxUstNem.Text, out ustnem))
            {
                dl.TehlikeliUstNem = ustnem;
            }
            if (double.TryParse(textBoxAltNem.Text, out altnem))
            {
                dl.TehlikeliAltNem = altnem;
            }
            if (double.TryParse(textBoxUstSicaklik2.Text, out ustsic2))
            {
                dl.TehlikeliUstSicaklik2 = ustsic2;
            }
            if (double.TryParse(textBoxAltSicaklik2.Text, out altsic2))
            {
                dl.TehlikeliAltSicaklik2 = altsic2;
            }
            if (double.TryParse(textBoxUstNem2.Text, out ustnem2))
            {
                dl.TehlikeliUstNem2 = ustnem2;
            }
            if (double.TryParse(textBoxAltNem2.Text, out altnem2))
            {
                dl.TehlikeliAltNem2 = altnem2;
            }
            if (dl.SensorListesi == null || dl.SensorListesi.Count != 2 || txtSensor2.Text != txtSensor1.Text)
            {
                dl.SensorListesi = new List<Sensor>();
                Sensor sensor1 = new Sensor();
                sensor1.Isim = txtSensor1.Text;
                sensor1.Index = 0;
                dl.SensorListesi.Add(sensor1);
                Sensor sensor2 = new Sensor();
                sensor2.Isim = txtSensor2.Text;
                sensor2.Index = 1;
                dl.SensorListesi.Add(sensor2);
            }
      //      Close();
            //dl.TxtPath = txtDosyaSecimi.Text;
        }

        private void FrmDataLoggerEkle_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fs = new OpenFileDialog();
            if(fs.ShowDialog() == DialogResult.OK)
            {
                dl.TxtPath = fs.FileName;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string link = @"http://localhost:3000/musteri/";
          //string c =  HttpHelper.sendGetRequest(link);
          String json = "{\"kurum\":\"netolcer_test\",\"adres\":\"bilinmezlikgtutest23\",\"sehir\":\"darıca\",\"telno\":\"444345423\",\"mail\":\"mnetolcer23@m.com.tr\",\"password\":\"123453\"}";
          string c = HttpHelper.sendPostRequest(link, Encoding.ASCII.GetBytes(json));
        }

       
    }
}
