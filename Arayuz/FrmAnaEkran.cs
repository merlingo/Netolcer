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
using System.Xml;
using System.Xml.Serialization;

namespace Arayuz
{
    public partial class FrmAnaEkran : Form
    {
        DataLogger datalogger;
        DataLoggerListesi dataLoggerlar;
        bool minimizedToTray;
        TarihFiltrelemeSecenegi tfc = TarihFiltrelemeSecenegi.TarihAraligi;
        public FrmAnaEkran(string DlName="")
        {
            if (DlName != "")
            {
                //dlName dataloggerınına sdkarttaki txtfile yuklenir
            }
            //Load += FrmAnaEkran_Load;
            Icon icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\unnamed.ico");
            this.Icon = icon;
            InitializeComponent();
            
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            loadXml();
            //HATA: doldurmadan önce yapılan parsingText fonksiyonu büyük veride hantal çalışıyor. txt dosyasını aylara parçalayıp aylık yüklemeleri yapmalı. Ilk uygulama açıldığında hepsini için parsingText Değil sadece ilki için yapmalı ve secildiğinde eger parsingText yapılmamıssa yapmalı
            DataLoggerListeVieweDoldur();
            gvDataLoggerVerileri.Hide();
            Dictionary<double, string> VeriAraliklari = new Dictionary<double, string>();
            VeriAraliklari.Add(0, "1 dk");
            //VeriAraliklari.Add(0.5, "30 sn"); //tasarım dk ustune yapıldı
            VeriAraliklari.Add(5, "5 dk");
            VeriAraliklari.Add(10, "10 dk");
            VeriAraliklari.Add(20, "20 dk");
            VeriAraliklari.Add(30, "30 dk");
            VeriAraliklari.Add(60, "1 saat");
            VeriAraliklari.Add(60*12, "12 saat");
            VeriAraliklari.Add(60*24, "1 gün");

            comboBoxVeriAraligi.DataSource = new BindingSource(VeriAraliklari, null);
            comboBoxVeriAraligi.DisplayMember = "Value";
            comboBoxVeriAraligi.ValueMember = "Key";

            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMMM yyyy, HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd MMMM yyyy, HH:mm";
            //limitle
            //columnları ayarla

            trreeViewDataLoggerListesi.MouseDoubleClick += lvDataLoggerListesi_MouseClick;
            trreeViewDataLoggerListesi.NodeMouseClick += treeView1_NodeMouseClick;
            header.Text = " NETÖLÇER - " + dataLoggerlar.KurumAdi;
        }
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == SingleInstance.WM_SHOWFIRSTINSTANCE)
            {
                ShowWindow();
            }
            base.WndProc(ref message);
        }
        void MinimizeToTray()
        {
            notifyIcon1 = new NotifyIcon();
            //notifyIcon.Click += new EventHandler(NotifyIconClick);
            notifyIcon1.DoubleClick += new EventHandler(NotifyIconClick);
            notifyIcon1.Text = ProgramInfo.AssemblyTitle;
            notifyIcon1.Visible = true;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            minimizedToTray = true;
        }
        public void ShowWindow()
        {
            if (minimizedToTray)
            {
                notifyIcon1.Visible = false;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                minimizedToTray = false;
            }
            else
            {
                WinApi.ShowToFront(this.Handle);
            }
        }
        void NotifyIconClick(Object sender, System.EventArgs e)
        {
            ShowWindow();
        }
        //private void FrmAnaEkran_Load(object sender, EventArgs e)
        //{
        //    float width_ratio = (Screen.PrimaryScreen.Bounds.Width / 1280);
        //    float heigh_ratio = (Screen.PrimaryScreen.Bounds.Height / 1000f);

        //    SizeF scale = new SizeF(width_ratio, heigh_ratio);

        //    this.Scale(scale);

        //    //And for font size
        //    foreach (Control control in this.Controls)
        //    {
        //        control.Font = new Font("Times New Roman", control.Font.SizeInPoints * heigh_ratio * width_ratio);
        //    }
        //}
        //private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    TreeNode node = trreeViewDataLoggerListesi.SelectedNode;

        //    DataLogger datalg = (DataLogger)node.Tag;
        //    FrmDataLoggerEkle dle = new FrmDataLoggerEkle(datalg);
        //    dle.ShowDialog();
        //    if (dle.DialogResult == DialogResult.OK)
        //    {
        //        dataLoggerlar.DataLoggerlar.Remove(datalg);
        //        dataLoggerlar.DataLoggerlar.Add(dle.dl);

        //    }
        //}

        //Seçilen datalogger ekrana yüklenir 
        void treeView1_NodeMouseClick(Object sender, TreeNodeMouseClickEventArgs e)
        {
            //tek basmada açilir
            TreeNode lvi = e.Node;
            groupBox1.Show();
            DataLogger datalg = (DataLogger)lvi.Tag;
            datalogger = datalg;
            if (! datalg.TxtPath.Equals(""))
            {
                buttonTxtYukle.Show();
                setDataLogger(false);
                 gvDataLoggerVerileri.Show();
            
            }
            else
                gvDataLoggerVerileri.Hide();

        }
        private void lvDataLoggerListesi_MouseClick(object sender, EventArgs e)
        {
            TreeNode lvi = trreeViewDataLoggerListesi.SelectedNode;
            DataLogger datalg = (DataLogger)lvi.Tag;
            dataLoggerBilgileriPenceresiniAcma(datalg);
        }
        void listView_Resize(object sender, EventArgs e)
        {

            foreach (ColumnHeader c in ((ListView)sender).Columns)
                c.Width = ((ListView)sender).Width;
        }
      
        private void setDataLogger(bool verilerYuklensinmi)
        {

            if (datalogger.Dt == null)
            {
                this.Cursor = Cursors.WaitCursor;
                datalogger.ParsingText(verilerYuklensinmi);

            }
            
            DataTable dt = datalogger.Dt;
            if (dt.Rows.Count > 1)
            {
                DateTime minDate;
                DateTime maxDate;
                minDate = dt.Rows[0].Field<DateTime>("Tarih");
                maxDate = dt.Rows[dt.Rows.Count - 1].Field<DateTime>("Tarih");
                //reset mindate maxdate
                dateTimePicker1.MinDate = dateTimePicker1.MaxDate = DateTime.Now;
                dateTimePicker2.MinDate = dateTimePicker2.MaxDate = DateTime.Now;

                dateTimePicker1.MinDate = minDate;
                dateTimePicker1.MaxDate = maxDate;
                dateTimePicker2.MinDate = minDate;
                dateTimePicker2.MaxDate = maxDate;
                dateTimePicker2.Value = maxDate;
                dateTimePicker1.Value = minDate;
                try
                {
                    AraliktaOlanAylariComboboxaDoldur(minDate, maxDate);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Tarih Aralığı hatası: Datalogger usb dosyasında hata var: ilk tarih:" + minDate + " - son tarih:" + maxDate + "\n  " + e.Data + "  " + e.Message);
                }
                gvDataLoggerVerileri.DataSource = datalogger.Dt;
                this.gvDataLoggerVerileri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dlOrtalamaSicaklik.Text = datalogger.OrtalamaSicaklik().ToString();
                dlOrtalamaNem.Text = datalogger.OrtalamaNem().ToString();
                ilkDeger = minDate;
                sonDeger = maxDate;
            }
            groupBox1.Text = datalogger.Isim + " Istatistikleri";
            //lblIcinEnYuksekCicaklik.Text = datalogger.Isim + " İçin En Yüksek Sıcaklık:";
            //lblIcinEnDusukSicaklik.Text = datalogger.Isim + " İçin En Düşük Sıcaklık:";
            //lblIcinEnYuksekNem.Text = datalogger.Isim + " İçin En Yüksek Nem:";
            //lblIcinEnDusukNem.Text = datalogger.Isim + " İçin En Düşük Nem:";
            //lblIcinOrtalamaSicaklik.Text = datalogger.Isim + " İçin Ortalama Sıcaklık:";
            //lblIcinOrtalamaNem.Text = datalogger.Isim + " İçin Ortalama Nem:";

            //sensor adlari yaz
            labelSensor1.Text = datalogger.SensorListesi[0].Isim;
            labelSensor2.Text = datalogger.SensorListesi[1].Isim;
            labelSensorNem1.Text = datalogger.SensorListesi[0].Isim;
            labelSensorNem2.Text = datalogger.SensorListesi[1].Isim;
            dlTehlikeliYuksekSicaklikDeger.Text = datalogger.TehlikeliUstSicaklik.ToString();

            dlTehlikeliDusukSicaklikDeger.Text = datalogger.TehlikeliAltSicaklik.ToString();

            dlTehlikeliYuksekNemDeger.Text = datalogger.TehlikeliUstNem.ToString();

            dlTehlikeliDusukNemDeger.Text = datalogger.TehlikeliAltNem.ToString();

            dlTehlikeliYuksekSicaklikDeger2.Text = datalogger.TehlikeliUstSicaklik2.ToString();

            dlTehlikeliDusukSicaklikDeger2.Text = datalogger.TehlikeliAltSicaklik2.ToString();

            dlTehlikeliYuksekNemDeger2.Text = datalogger.TehlikeliUstNem2.ToString();

            dlTehlikeliDusukNemDeger2.Text = datalogger.TehlikeliAltNem2.ToString();
           
            this.Refresh();
            this.Cursor = Cursors.Default;

        }
        private void button1_Click(object sender, EventArgs e)
        {
       
        }

        public void loadXml()  
        {
            //dataLoggerlar objesi doldurulur

            XmlSerializer serializer = new XmlSerializer(typeof(DataLoggerListesi));
            StreamReader reader = new StreamReader(ConstantValues.path);
            dataLoggerlar = (DataLoggerListesi)serializer.Deserialize(reader);
            List<Task> tasks=new List<Task>();

            foreach (DataLogger dl in dataLoggerlar.DataLoggerlar)
            {
                if(dl.TxtPath.Count()>1)
                    tasks.Add(Task.Factory.StartNew(() => dl.VerileriDosyadanYukle()));
            }
            reader.Close();
            if(tasks.Count>0)
                Task.WaitAll(tasks.ToArray());
        }
        /// <summary>
        /// listView'e datalogger ekleme fonksiyonudur.
        /// Exp: Eğer datalogger'ın sensörListesi boş (null) ise, hata çıkar. dl yapisi bozuk ya da eksik bilgi iceriyorsa hata cikmasa da doğru calismaz
        /// </summary>
        /// <param name="dl">eklenecek datalogger</param>
        private void DataLoggerListeyeEkle(DataLogger dl)
        {
            TreeNode treeNode;
            TreeNode node2 = new TreeNode(dl.SensorListesi[0].Isim);
            TreeNode node3 = new TreeNode(dl.SensorListesi[1].Isim);
            TreeNode[] array = new TreeNode[] { node2, node3 };

            treeNode = new TreeNode(dl.Isim, array);
            treeNode.Tag = dl;
            node2.Tag = dl;
            node3.Tag = dl;
            treeNode.Text = dl.Isim;
            treeNode.Tag = dl;
            trreeViewDataLoggerListesi.Nodes.Add(treeNode);
        }

        public void DataLoggerListeVieweDoldur()
        {
            if (dataLoggerlar == null)
                return;
             foreach (DataLogger dl in dataLoggerlar.DataLoggerlar)
             {
                 DataLoggerListeyeEkle(dl);
                 //if (dl.Dt == null && dl.TxtPath != "")
                 //{
                 //    dl.ParsingText();
                 //}
             }
        }
      
       
       
        DateTime ilkDeger ;
        DateTime sonDeger ;
        private void FiltreButton_Click(object sender, EventArgs e)
        {
            //tarih filtresi
          //  if (pt == null) return;

             ilkDeger = new DateTime();
             sonDeger = new DateTime();
            if (tfc==TarihFiltrelemeSecenegi.TarihAraligi)
            {
                ilkDeger = dateTimePicker1.Value;
                sonDeger = dateTimePicker2.Value;
            }
            else if (tfc == TarihFiltrelemeSecenegi.Aylik)
            {
                ilkDeger = ((KeyValuePair<DateTime, string>)comboBox1.SelectedItem).Key;
                sonDeger = ilkDeger.AddMonths(1);
            }
            // iki deger alınır, ilki oteksiinden büyük degilse hata
            if (ilkDeger > sonDeger)
            {
                MessageBox.Show("Başlangıç zamanı"+ilkDeger.ToShortTimeString()+"- Bitiş zmanı:"+sonDeger.ToShortDateString() +" --  HATA: İLK TARİH DAHA KÜÇÜK OLMALIDIR", "UYARI");
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

            DataRow[] _newDataRown = datalogger.Dt.Select(("Tarih >= '" + ilkDeger.ToString() + "' AND Tarih <= '" + sonDeger.ToString() + "'"));

            List<DataRow> datarows = new List<DataRow>();
            DateTime aralikBasi = ilkDeger;

            foreach (DataRow row in _newDataRown)
            {
                DateTime rowDate = row.Field<DateTime>("Tarih");
                if (aralik == 60)
                {
                    if (rowDate == aralikBasi || (rowDate - aralikBasi).Hours >= 1)
                    {
                        aralikBasi = rowDate;

                        datarows.Add(row);
                    }
                }
                else if (aralik == 60*12)
                {
                    if (rowDate == aralikBasi || (rowDate - aralikBasi).Hours >= 12)
                    {
                        aralikBasi = rowDate;

                        datarows.Add(row);
                    }
                }
                else if (aralik == 60 * 24)
                {
                    if (rowDate == aralikBasi || (rowDate - aralikBasi).Days >= 1)
                    {
                        aralikBasi = rowDate;

                        datarows.Add(row);
                    }
                }
                else
                {
                    if (rowDate == aralikBasi || (rowDate - aralikBasi).Minutes >= aralik)
                    {
                        aralikBasi = rowDate;

                        datarows.Add(row);
                    }
                }
            }
            if (_newDataRown.Count() == 0)
                gvDataLoggerVerileri.DataSource = null;
            else
            gvDataLoggerVerileri.DataSource = datarows.ToArray().CopyToDataTable();
           

        }
        private void button2_Click(object sender, EventArgs e)
        {
            gvDataLoggerVerileri.DataSource = datalogger.Dt;
        }
      
        private void PdfCiktiButton_Click(object sender, EventArgs e)
        {
            //FrmPdfCikti pdfCikti = new FrmPdfCikti(dataLoggerlar);
            //pdfCikti.ShowDialog();

            if (gvDataLoggerVerileri.DataSource == null)
            {
                MessageBox.Show("pdf dosyasına dökülecek veri yok. Lütfen verileri yükleyiniz");
                return;
            }
            // seçili datalogger ve tabloda olan değerler kullanılarak pdf oluşturulur. genel istatistikler olmaz sadece datalogger istatistikleri olur
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
                    NetOlcerCiktiOptions options = new NetOlcerCiktiOptions();
                    options.GenelGrafiklerVarmi = false;
                    options.GenelIstatistiklerVarmi = false;
                    options.DataLoggerGrafiklerVarmi = true;
                    options.DataLoggerIstatistiklerVarmi = true;
                    options.DataLoggerSonKalibrasyonTarihleriVarmi = false;
                    double x = (double)comboBoxVeriAraligi.SelectedValue;
                    options.VeriAraligi = x;
                    options.CiktiIcinDataLoggerlar.Add(new CiktiDataLoggerItem(datalogger));
                    options.HedeflenenOrtamKosullari = "\n" + datalogger.SensorListesi[0].Isim + ": " + dlTehlikeliDusukSicaklikDeger.Text + "(ºC) - " + dlTehlikeliYuksekSicaklikDeger.Text + "(ºC) , " + dlTehlikeliDusukNemDeger.Text + "(%) - " + dlTehlikeliYuksekNemDeger.Text+" (%)"+
                                                       "\n" + datalogger.SensorListesi[1].Isim + ": " + dlTehlikeliDusukSicaklikDeger2.Text + "(ºC) - " + dlTehlikeliYuksekSicaklikDeger2.Text + "(ºC) , " + dlTehlikeliDusukNemDeger2.Text + "(%) - " + dlTehlikeliYuksekNemDeger2.Text + " (%)";//"17(ºC) -23(ºC) , 15(ºC) -25(ºC)" 
                    CıktıAl cikti = new CıktıAl(options, dataLoggerlar);
                    
                    cikti.createPdf(fs,ilkDeger,sonDeger);
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Rapor alma işlemi tamamlanmıştır");
                        Process.Start(fs.Name);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Pdf oluşturulamadı!!" + ex.Message);
                }
                finally
                {
                    fs.Close();

                }
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if(dataLoggerlar.DataLoggerlar.Any(x=>x.Dt!=null)){
            //FrmGrafikGosterim frmGrafik = new FrmGrafikGosterim(dataLoggerlar);
            //frmGrafik.ShowDialog();
            //}
            //else
            //    MessageBox.Show("İstatistikleri görebilmeniz için veri yüklemeniz gerekmektedir.");
            if (gvDataLoggerVerileri.DataSource == null)
            {
                MessageBox.Show("grafiğe dökülecek veri yok. Lütfen verileri yükleyiniz");
                return;
            }
            FrmTekDLGrafik tekDlGrafik = new FrmTekDLGrafik(datalogger, ilkDeger, sonDeger);
            tekDlGrafik.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmDataLoggerEkle dle = new FrmDataLoggerEkle();
            dle.ShowDialog();
            if (dle.DialogResult == DialogResult.OK)
            {
                //dataLoggerlar.DataLoggerlar.Add(dle.dl);
                XmlHelper.xmleDataLoggerEkle(dataLoggerlar, dle.dl);
                DataLoggerListeyeEkle(dle.dl);

            }
        }

       
       
        int indexBul(string name)
        {
            foreach (TreeNode n in trreeViewDataLoggerListesi.Nodes)
            {
                if (n.Text == name)
                    return n.Index;
                
            }
            throw new Exception("girilen dt treeviewde bulunamadı");
        }
        void treeViewGuncelle(DataLogger dl,string ad)
        {
                                //treeview güncelleme
                   // dataLoggerlar = null;
                   // loadXml();

                    int indDl = indexBul(ad);
                    TreeNode treeNode;

                    TreeNode node2 = new TreeNode(dl.SensorListesi[0].Isim);
                    TreeNode node3 = new TreeNode(dl.SensorListesi[1].Isim);
                    TreeNode[] array = new TreeNode[] { node2, node3 };

                    treeNode = new TreeNode(dl.Isim, array);
                    treeNode.Tag = dl;
                    node2.Tag = dl;
                    node3.Tag = dl;
                    treeNode.Text = dl.Isim;
                    treeNode.Tag = dl;
                    trreeViewDataLoggerListesi.Nodes[indDl].Remove();
                    trreeViewDataLoggerListesi.Nodes.Insert(indDl, treeNode);
                    dl.ParsingText(false);
                    datalogger = dl;
                    setDataLogger(false);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            dataLoggerBilgileriPenceresiniAcma(datalogger);
        }
        public void dataLoggerBilgileriPenceresiniAcma(DataLogger dt)
        {
            //seçilen datalogger için frmKurulumEkrani ni açmak gerekir.
            if (dt == null)
            {
                MessageBox.Show("Lütfen değiştirmek istediğiniz dataloggerı soldaki listeden seçiniz");
                return;
            }
            string dlisim = dt.Isim;
            FrmDataLoggerEkle fdle = new FrmDataLoggerEkle(dt);
            DialogResult dr = fdle.ShowDialog();
            if (DialogResult.OK == dr)
            {
                XmlHelper.xmlDataLoggerGuncelle(dt, dlisim);
                treeViewGuncelle(dt, dlisim);
            }

        }

        private void buttonTxtYukle_Click(object sender, EventArgs e)
        {
            //seçilen datalogger için txtdosyasını program dosyasına kopyalar xml dosyasında günceller parsing yapar ve datagridviewe yükler.
            OpenFileDialog fs = new OpenFileDialog();
            string txtpath = "";
            if (datalogger == null)
            {
                MessageBox.Show("Lütfen soldaki listeden verilerini yüklemek istediğiniz dataloggerı seçiniz.");
                return;
            }
            if (fs.ShowDialog() == DialogResult.OK)
            {
                txtpath = Application.StartupPath + "/" + datalogger.Isim + ".txt";
                TxtDosyayiKlasoreKopyalaveXmleEkle(fs.FileName,txtpath);
                setDataLogger(true);
                gvDataLoggerVerileri.Show();

               
              
            }
        }
        public void TxtDosyayiKlasoreKopyalaveXmleEkle(string kopyalananFileName,string txtpath)
        {
            //1-dosyaların durduğu klasöre kopyalanır
            //Eski .txt dosyayı yedekle ve yedekte olanı açmak için arayüz ekle
            if (File.Exists(txtpath))
            {
                //File.Delete(txtpath);
                //varolan dosyanın adını değiştirme
                string yenitxt = txtpath.Split('.')[0] + "_" + DateTime.Now.Ticks + ".txt";
                File.Move(txtpath, yenitxt);
            }
            File.Copy(kopyalananFileName, txtpath);

            datalogger.TxtPath = txtpath;
            foreach (var dl in dataLoggerlar.DataLoggerlar)
            {
                if (dl.Isim == datalogger.Isim)
                {
                    dl.TxtPath = txtpath;
                    break;
                }
            }
            //2- xml verisi olarak eklenir
            XmlDocument xml = new XmlDocument();
            xml.Load(ConstantValues.path);
            XmlNodeList elements = xml.SelectNodes("//DataLoggerlar//DataLoggerlar");
            foreach (XmlNode element in elements.Item(0))
            {
                if (element.InnerXml.Contains(datalogger.Isim))
                {
                    XmlElement xTxtPath = xml.CreateElement("TxtPath");
                    xTxtPath.InnerText = datalogger.TxtPath;
                    element.ReplaceChild(xTxtPath, element.ChildNodes.Item(2));
                    xml.Save(ConstantValues.path);
                    datalogger.Dt = null;
                    datalogger.SensorListesi[0]._degerler = new List<NetOlcerBirimi>();
                    datalogger.SensorListesi[1]._degerler = new List<NetOlcerBirimi>();

                    break;
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (gvDataLoggerVerileri.DataSource == null)
            {
                MessageBox.Show("istatisliğe dökülecek veri yok. Lütfen verileri yükleyiniz");
                return;
            }
            if (datalogger.Dt ==null)
            {
                MessageBox.Show("istatisliğe dökülecek veri yok. Lütfen verileri yükleyiniz");
                return;
            }
            //sadece seçili dl nin istatistiği gösterilecek
            FrmStatistikler frmIstatistik = new FrmStatistikler(datalogger);
            frmIstatistik.ShowDialog();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (datalogger == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz dataloggerı soldaki listeden seçiniz");
                return;
            }

            DialogResult silmeResult = MessageBox.Show(datalogger.Isim + " dataloggerı silmek istediğinize emin misiniz?","Silmek İçin Teyit", MessageBoxButtons.YesNo);
            if(silmeResult == DialogResult.Yes)
            {
                xmlDataLoggerSil(datalogger);
            }
        }

        private void xmlDataLoggerSil(DataLogger datalogger)
        {
             XmlDocument xml = new XmlDocument();
             xml.Load(ConstantValues.path);
                XmlNodeList elements = xml.SelectNodes("//DataLoggerlar//DataLoggerlar");
                foreach (XmlNode element in elements.Item(0))
                {
                    if (element.InnerXml.Contains(datalogger.Isim))
                    {
                        element.ParentNode.RemoveChild(element);
                        int indDl = indexBul(datalogger.Isim);
                        trreeViewDataLoggerListesi.Nodes[indDl].Remove();
                        dataLoggerlar.DataLoggerlar.Remove(datalogger);
                        xml.Save(ConstantValues.path);
                        break;
                    }
                }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            gvDataLoggerVerileri.DataSource = datalogger.Dt;

        }

        private void dlTehlikeliYuksekSicaklikDeger_Click(object sender, EventArgs e)
        {
            //tehlikeli yüksek sıcaklığı değiştirmek için ve o sınırı geçenleri listelemek için 
            TehlikeliDeger td = new TehlikeliDeger(datalogger, "ys");
            DialogResult dr = td.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //xml dosyasını değiştireceğiz
                datalogger.TehlikeliUstSicaklik = td.tehlikeliDeger;
                XmlHelper.xmlDataLoggerGuncelle(datalogger, datalogger.Isim);
                dlTehlikeliYuksekSicaklikDeger.Text = td.tehlikeliDeger.ToString();
                this.Refresh();
            }
        }

        private void dlTehlikeliDusukSicaklikDeger_Click(object sender, EventArgs e)
        {
            TehlikeliDeger td = new TehlikeliDeger(datalogger, "ds");
            DialogResult dr = td.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //xml dosyasını değiştireceğiz
                datalogger.TehlikeliAltSicaklik = td.tehlikeliDeger;
                XmlHelper.xmlDataLoggerGuncelle(datalogger, datalogger.Isim);
                //treeViewGuncelle(datalogger, datalogger.Isim);
                dlTehlikeliDusukSicaklikDeger.Text = td.tehlikeliDeger.ToString();
                this.Refresh();
            }
        }

        private void dlTehlikeliYuksekNemDeger_Click(object sender, EventArgs e)
        {
            TehlikeliDeger td = new TehlikeliDeger(datalogger, "yn");
            DialogResult dr = td.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //xml dosyasını değiştireceğiz
                datalogger.TehlikeliUstNem = td.tehlikeliDeger;
                XmlHelper.xmlDataLoggerGuncelle(datalogger, datalogger.Isim);
               // treeViewGuncelle(datalogger, datalogger.Isim);
                dlTehlikeliYuksekNemDeger.Text = datalogger.TehlikeliUstNem.ToString();
                this.Refresh();
            }
        }
        private void dlTehlikeliYuksekSicaklikDeger2_Click(object sender, EventArgs e)
        {
            //tehlikeli yüksek sıcaklığı değiştirmek için ve o sınırı geçenleri listelemek için 
            TehlikeliDeger td = new TehlikeliDeger(datalogger, "ys2");
            DialogResult dr = td.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //xml dosyasını değiştireceğiz
                datalogger.TehlikeliUstSicaklik2 = td.tehlikeliDeger;
                XmlHelper.xmlDataLoggerGuncelle(datalogger, datalogger.Isim);
                dlTehlikeliYuksekSicaklikDeger2.Text = td.tehlikeliDeger.ToString();
                this.Refresh();
            }
        }

        private void dlTehlikeliDusukSicaklikDeger2_Click(object sender, EventArgs e)
        {
            TehlikeliDeger td = new TehlikeliDeger(datalogger, "ds2");
            DialogResult dr = td.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //xml dosyasını değiştireceğiz
                datalogger.TehlikeliAltSicaklik2 = td.tehlikeliDeger;
                XmlHelper.xmlDataLoggerGuncelle(datalogger, datalogger.Isim);
                dlTehlikeliDusukSicaklikDeger2.Text = td.tehlikeliDeger.ToString();
                this.Refresh();
            }
        }

        private void dlTehlikeliYuksekNemDeger2_Click(object sender, EventArgs e)
        {
            TehlikeliDeger td = new TehlikeliDeger(datalogger, "yn2");
            DialogResult dr = td.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //xml dosyasını değiştireceğiz
                datalogger.TehlikeliUstNem2 = td.tehlikeliDeger;
                XmlHelper.xmlDataLoggerGuncelle(datalogger, datalogger.Isim);
                dlTehlikeliYuksekNemDeger2.Text = td.tehlikeliDeger.ToString();
                this.Refresh();
            }
        }

        private void dlTehlikeliDusukNemDeger2_Click(object sender, EventArgs e)
        {
            TehlikeliDeger td = new TehlikeliDeger(datalogger, "dn2");
            DialogResult dr = td.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //xml dosyasını değiştireceğiz
                datalogger.TehlikeliAltNem2 = td.tehlikeliDeger;
                XmlHelper.xmlDataLoggerGuncelle(datalogger, datalogger.Isim);
                dlTehlikeliDusukNemDeger2.Text = td.tehlikeliDeger.ToString();
                this.Refresh();
            }
        }
        private void dlTehlikeliDusukNemDeger_Click(object sender, EventArgs e)
        {
            TehlikeliDeger td = new TehlikeliDeger(datalogger, "dn");
            DialogResult dr = td.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //xml dosyasını değiştireceğiz
                datalogger.TehlikeliAltNem = td.tehlikeliDeger;
                XmlHelper.xmlDataLoggerGuncelle(datalogger, datalogger.Isim);
                treeViewGuncelle(datalogger, datalogger.Isim);
                dlTehlikeliDusukNemDeger.Text = td.tehlikeliDeger.ToString();
                this.Refresh();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Name=="radioButton1"&&radioButton1.Checked)
            {
                tfc = TarihFiltrelemeSecenegi.TarihAraligi;
                comboBox1.Enabled = false;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                radioButton1.Checked = true;
                radioButton2.Checked = false;

            }
            else if (rb.Name == "radioButton2" && radioButton2.Checked)
            {
                tfc = TarihFiltrelemeSecenegi.Aylik;
                comboBox1.Enabled = true;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                radioButton1.Checked = false;
                radioButton2.Checked = true;

            }
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
           // notifyIcon1.Visible = false;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        void AraliktaOlanAylariComboboxaDoldur(DateTime mindate, DateTime maxdate)
        {
            int month = mindate.Month;
            int year = mindate.Year;
            DateTime aylikDate = new DateTime(mindate.Year, month, 1) ;
            Dictionary<DateTime, string> Aylar = new Dictionary<DateTime, string>();
            if (!(maxdate < DateTime.MaxValue))
                throw new Exception("Geçersiz maximum tarih");
            do
            {
                Aylar.Add(aylikDate, yaziIleAy(month) + ", " + year.ToString());
                if (month == 12)
                {
                    month = 1;
                    year++;
                }
                else
                {
                    month++;
                }
                aylikDate = aylikDate.AddMonths(1);
            } while (!(month >= maxdate.Month && year >= maxdate.Year));
            Aylar.Add(aylikDate, yaziIleAy(month) + ", " + year.ToString());
            comboBox1.DataSource = new BindingSource(Aylar, null);

        }
        string yaziIleAy(int ay)
        {
            if (ay == 1)
                return "Ocak";
            else if (ay == 2)
                return "Şubat";
            else if (ay == 3)
                return "Mart";
            else if (ay == 4)
                return "Nisan";
            else if (ay == 5)
                return "Mayıs";
            else if (ay == 6)
                return "Haziran";
            else if (ay == 7)
                return "Temmuz";
            else if (ay == 8)
                return "Ağustos";
            else if (ay == 9)
                return "Eylül";
            else if (ay == 10)
                return "Ekim";
            else if (ay == 11)
                return "Kasım";
            else if (ay == 12)
                return "Aralık";
            else throw new Exception("Ay 12den büyük olamaz");
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }

        private void çıkışToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.ExitThread();

        }

        private void header_Click(object sender, EventArgs e)
        {
            FrmKurulumEkrani kurulumForm = new FrmKurulumEkrani(dataLoggerlar);
            if (DialogResult.OK == kurulumForm.ShowDialog())
            {
                dataLoggerlar = kurulumForm.dll;
                header.Text = " NETÖLÇER - " + dataLoggerlar.KurumAdi;
            }
        }

       
        
    }
    public enum TarihFiltrelemeSecenegi
    {
        TarihAraligi,
        Aylik
    }
}
