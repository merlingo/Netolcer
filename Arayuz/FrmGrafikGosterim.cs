using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Arayuz
{
    public partial class FrmGrafikGosterim : Form
    {
        public List<double> XValues;
        public DataLoggerListesi dataLoggerlar;
        string dataloggerAdi="",datatipi="isi";
        public FrmGrafikGosterim(DataLoggerListesi dls)
        {
            Icon icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\unnamed.ico");
            this.Icon = icon;
            InitializeComponent();
            comboBox1.Items.Add("ISI");
            comboBox1.Items.Add("NEM");
            foreach (DataLogger dl in dls.DataLoggerlar)
            {
                comboBoxDataLogger.Items.Add(dl.Isim);
            }
            XValues = new List<double>();
            dataLoggerlar = dls;
            //ilk tarih ile son tarih arasında kaç gün olduğu bulunur -> gunSayisi, 20 tarihin gözükmesini istiyoruz-> gd=20, gozüken tarihler arasındaki gün sayısı,interval
            //=> gd*interval=gunSayisi
            double interval = intervalHesapla(dataLoggerlar.OlcumBaslangicTarihi(), dataLoggerlar.OlcumBitisTarihi());
            chartDataLogger.ChartAreas[0].AxisX.Interval = interval;
            chartDataLogger.ChartAreas[0].AxisY.Interval = 1.5;
            chartDataLogger.ChartAreas[0].AxisY.Minimum = dataLoggerlar.EnDusukSicaklik().Deger-0.4;
            chartDataLogger.ChartAreas[0].AxisY.Maximum = dataLoggerlar.EnYuksekSicaklik().Deger + 0.4;
            chartDataLogger.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy, hh:mm";
            dataLoggerlariGrafigeYazdir("isi");
            chartDataLogger.MouseClick += chart1_MouseClick;
            chartDataLogger.MouseMove += chart1_MouseMove;
            
        }
        public static double intervalHesapla(DateTime ilkTarih, DateTime sonTarih)
        {
            double interval = 0;
            double gunSayisi = (sonTarih - ilkTarih).TotalDays;
            double gozukenTarihSayisi = 20;
            interval = gunSayisi / gozukenTarihSayisi;
            return interval;
        }
        public void dataLoggerlariGrafigeYazdir(string tip, string datalogger="")
        {
            chartDataLogger.Series.Clear();
            if (datalogger == "")//tüm dataloggerları yazdır
            {
                double interval = intervalHesapla(dataLoggerlar.OlcumBaslangicTarihi(), dataLoggerlar.OlcumBitisTarihi());
                chartDataLogger.ChartAreas[0].AxisX.Interval = interval;
                foreach (DataLogger dl in dataLoggerlar.DataLoggerlar)
                {
                    Series series1 = chartDataLogger.Series.Add(dl.Isim + " - " + dl.SensorListesi[0].Isim);
                    series1.SetDefault(true);
                    series1.Enabled = true;
                    series1.Points.DataBindXY(dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip).Select(x => x.Zaman).ToArray(), dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip).Select(x => x.Deger).ToArray());
                    series1.ChartType = SeriesChartType.Line;

                    Series series2 = chartDataLogger.Series.Add(dl.Isim + " - " + dl.SensorListesi[1].Isim);
                    series2.SetDefault(true);
                    series2.Enabled = true;
                    series2.Points.DataBindXY(dl.SensorListesi[1]._degerler.Where(x => x.Tip == tip).Select(x => x.Zaman).ToArray(), dl.SensorListesi[1]._degerler.Where(x => x.Tip == tip).Select(x => x.Deger).ToArray());

                    series2.ChartType = SeriesChartType.Line;
                }
            }
            else
            {
                foreach (DataLogger dl in dataLoggerlar.DataLoggerlar)
                {
                    if (dl.Isim == datalogger)
                    {
                        double interval = intervalHesapla(dl.IlkTarih(), dl.SonTarih());
                        chartDataLogger.ChartAreas[0].AxisX.Interval = interval;
                        Series series1 = chartDataLogger.Series.Add(dl.Isim + " - " + dl.SensorListesi[0].Isim);
                        series1.Points.DataBindXY(dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip).Select(x => x.Zaman).ToArray(), dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip).Select(x => x.Deger).ToArray());

                        series1.ChartType = SeriesChartType.Line;
                        Series series2 = chartDataLogger.Series.Add(dl.Isim + " - " + dl.SensorListesi[1].Isim);
                        series2.Points.DataBindXY(dl.SensorListesi[1]._degerler.Where(x => x.Tip == tip).Select(x => x.Zaman).ToArray(), dl.SensorListesi[1]._degerler.Where(x => x.Tip == tip).Select(x => x.Deger).ToArray());

                        series2.ChartType = SeriesChartType.Line;
                    }
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            if (selected == "ISI")
            {
                chartDataLogger.ChartAreas[0].AxisY.Minimum = dataLoggerlar.EnDusukSicaklik().Deger - 0.4;
                chartDataLogger.ChartAreas[0].AxisY.Maximum = dataLoggerlar.EnYuksekSicaklik().Deger + 0.4;
                dataLoggerlariGrafigeYazdir("isi", dataloggerAdi);
                datatipi = "isi";
            }
            else if (selected == "NEM")
            {
                chartDataLogger.ChartAreas[0].AxisY.Minimum = dataLoggerlar.EnDusukNem().Deger - 0.4;
                chartDataLogger.ChartAreas[0].AxisY.Maximum = dataLoggerlar.EnYuksekNem().Deger + 0.4;
                dataLoggerlariGrafigeYazdir("nem",dataloggerAdi);
                datatipi = "nem";

            }
        }

        private void comboBoxDataLogger_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seciliDl =(string) comboBoxDataLogger.SelectedItem;
            dataLoggerlariGrafigeYazdir(datatipi,seciliDl);
            dataloggerAdi = seciliDl;

        }

        ToolTip tooltip = new ToolTip();
        Point? clickPosition = null;

        void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            clickPosition = pos;
            var results = chartDataLogger.HitTest(pos.X, pos.Y, false,
                                         ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    var xVal = DateTime.FromOADate(result.ChartArea.AxisX.PixelPositionToValue(pos.X));
                    DataLogger datalg = dataLoggerlar.DataLoggerlar.Find(x => x.Isim == dataloggerAdi);
                    if (datalg == null)
                        return;
                    try
                    {
                        var y1Val = datalg.SensorListesi[0]._degerler.First(x => x.Zaman.Equals(new DateTime(xVal.Year, xVal.Month, xVal.Day, xVal.Hour, xVal.Minute, 0)) && x.Tip == datatipi).Deger;
                        var y2Val = datalg.SensorListesi[1]._degerler.First(x => x.Zaman.Equals(new DateTime(xVal.Year, xVal.Month, xVal.Day, xVal.Hour, xVal.Minute, 0)) && x.Tip == datatipi).Deger;

                        tooltip.Show("Tarih=" + xVal.ToString() + ", sensor1=" + y1Val + ", sensor2=" + y2Val, this.chartDataLogger, e.Location.X, e.Location.Y - 15);
                    }
                    catch
                    {
                       
                            tooltip.RemoveAll();
                            clickPosition = null;
                        
                    }
                }
            }
        }

        void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            tooltip.RemoveAll();
            clickPosition = null;
            var pos = e.Location;
            clickPosition = pos;
            var results = chartDataLogger.HitTest(pos.X, pos.Y, false,ChartElementType.PlottingArea);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.PlottingArea)
                {
                    var xVal = DateTime.FromOADate(result.ChartArea.AxisX.PixelPositionToValue(pos.X));
                    var yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);

                    //xValden alınan zamanı + - girilen tarih aralığı kadar bir zaman aralığı çıkarılır ve tablo o şekilde düzenlenir
                  
                }
            }
        }
    }
}
