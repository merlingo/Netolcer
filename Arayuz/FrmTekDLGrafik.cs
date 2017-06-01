using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace Arayuz
{
    public partial class FrmTekDLGrafik : Form
    {
        DataLogger datalg;
        string tip = "isi";
        public FrmTekDLGrafik(DataLogger dl,DateTime ilkTarih, DateTime sonTarih)
        {
            datalg = dl;
            string tip = "isi";
            InitializeComponent();
            double interval = intervalHesapla(ilkTarih, sonTarih);
            chartDataLogger.ChartAreas[0].AxisX.Interval = interval;
            chartDataLogger.ChartAreas[0].AxisY.Interval = 1.5;
            chartDataLogger.ChartAreas[0].AxisY.Minimum = dl.EnDusukSicaklik().Deger - 0.4;
            chartDataLogger.ChartAreas[0].AxisY.Maximum = dl.EnYuksekSicaklik().Deger + 0.4;
            chartDataLogger.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy, hh:mm";
            chartDataLogger.ChartAreas[0].AxisX.Interval = interval;
            Series series1 = chartDataLogger.Series.Add(dl.Isim + " - " + dl.SensorListesi[0].Isim);
            series1.Points.DataBindXY(dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip && x.Zaman > ilkTarih && x.Zaman < sonTarih).Select(x => x.Zaman).ToArray(), dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip && x.Zaman > ilkTarih && x.Zaman < sonTarih).Select(x => x.Deger).ToArray());

            series1.ChartType = SeriesChartType.Line;
            Series series2 = chartDataLogger.Series.Add(dl.Isim + " - " + dl.SensorListesi[1].Isim);
            series2.Points.DataBindXY(dl.SensorListesi[1]._degerler.Where(x => x.Tip == tip && x.Zaman > ilkTarih && x.Zaman < sonTarih).Select(x => x.Zaman).ToArray(), dl.SensorListesi[1]._degerler.Where(x => x.Tip == tip && x.Zaman > ilkTarih && x.Zaman < sonTarih).Select(x => x.Deger).ToArray());

            series2.ChartType = SeriesChartType.Line;
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
                    if (datalg == null)
                        return;
                    try
                    {
                        var y1Val = datalg.SensorListesi[0]._degerler.First(x => x.Zaman.Equals(new DateTime(xVal.Year, xVal.Month, xVal.Day, xVal.Hour, xVal.Minute, 0)) && x.Tip == tip).Deger;
                        var y2Val = datalg.SensorListesi[1]._degerler.First(x => x.Zaman.Equals(new DateTime(xVal.Year, xVal.Month, xVal.Day, xVal.Hour, xVal.Minute, 0)) && x.Tip == tip).Deger;

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
            var results = chartDataLogger.HitTest(pos.X, pos.Y, false, ChartElementType.PlottingArea);
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
