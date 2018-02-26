using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Xml.Serialization;

namespace Arayuz
{
    [XmlType("DataLogger")]
    public class DataLogger
    {
       //Her datalogger için gerekli bilgilerin collection edilmesinde kullanılacak objedir. İlk formda listesi olacak ve secildiginde bu objenin icindeki bilgileri göstecek ekrana yönlendirilir.

      
        private string _isim="";
        [XmlElement("DLIsim")]
        public string Isim
        {
            get { return _isim; }
            set { _isim = value; }
        }

        private double _konum=0;//DEĞİŞTİRİLECEK - RESİM ÜSTÜNDEKİ KONUMU SAPTANACAK
        [XmlElement("Konum")]
        public double Konum
        {
            get { return _konum; }
            set { _konum = value; }
        }

        string _txtPath="";
                [XmlElement("TxtPath")]
        public string TxtPath
        {
            get { return _txtPath; }
            set { 
                _txtPath = value;
                //ParsingText();
            }
        }
        string _id = "";
        [XmlElement("Id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        int _sensorSayisi =2;
        [XmlElement("DLSensorSayisi")]
        public int SensorSayisi
        {
            get { return _sensorSayisi; }
            set { _sensorSayisi = value; }
        }
         string _telefonNumarasi = "";
        [XmlElement("TelefonNumarasi")]
        public string TelefonNumarasi
        {
            get { return _telefonNumarasi; }
            set { _telefonNumarasi = value; }
        }
        double _tehlikeliUstSicaklik = 0;
        [XmlElement("TehlikeliUstSicaklik")]
        public double TehlikeliUstSicaklik
        {
            get { return _tehlikeliUstSicaklik; }
            set { _tehlikeliUstSicaklik = value; }
        }
        double _tehlikeliAltSicaklik = 0;
        [XmlElement("TehlikeliAltSicaklik")]
        public double TehlikeliAltSicaklik
        {
            get { return _tehlikeliAltSicaklik; }
            set { _tehlikeliAltSicaklik = value; }
        }
        double _tehlikeliUstNem = 0;
        [XmlElement("TehlikeliUstNem")]
        public double TehlikeliUstNem
        {
            get { return _tehlikeliUstNem; }
            set { _tehlikeliUstNem = value; }
        }
        double _tehlikeliAltNem = 0;
        [XmlElement("TehlikeliAltNem")]
        public double TehlikeliAltNem
        {
            get { return _tehlikeliAltNem; }
            set { _tehlikeliAltNem = value; }
        }
        double _tehlikeliUstSicaklik2 = 0;
        [XmlElement("TehlikeliUstSicaklik2")]
        public double TehlikeliUstSicaklik2
        {
            get { return _tehlikeliUstSicaklik2; }
            set { _tehlikeliUstSicaklik2 = value; }
        }
        double _tehlikeliAltSicaklik2 = 0;
        [XmlElement("TehlikeliAltSicaklik2")]
        public double TehlikeliAltSicaklik2
        {
            get { return _tehlikeliAltSicaklik2; }
            set { _tehlikeliAltSicaklik2 = value; }
        }
        double _tehlikeliUstNem2 = 0;
        [XmlElement("TehlikeliUstNem2")]
        public double TehlikeliUstNem2
        {
            get { return _tehlikeliUstNem2; }
            set { _tehlikeliUstNem2 = value; }
        }
        double _tehlikeliAltNem2 = 0;
        [XmlElement("TehlikeliAltNem2")]
        public double TehlikeliAltNem2
        {
            get { return _tehlikeliAltNem2; }
            set { _tehlikeliAltNem2 = value; }
        }
        DateTime _sonKalibrasyonTarihi = DateTime.Today;
        [XmlElement("SonKalibrasyonTarihi")]
        public DateTime SonKalibrasyonTarihi
        {
            get { return _sonKalibrasyonTarihi; }
            set { _sonKalibrasyonTarihi = value; }
        }
        DataTable dt = null;
        [System.Xml.Serialization.XmlIgnore]
        public DataTable Dt
        {
            get { return dt; }
            set { dt = value; }
           
        }
        [XmlArray("SensorListesi")]
        List<Sensor> _sensorListesi = new List<Sensor>();
        public List<Sensor> SensorListesi
        {
            get { return _sensorListesi; }
            set { _sensorListesi = value; }
        }

        [System.Xml.Serialization.XmlIgnore]
        Chart chartDataLogger = null;

        public void dokumanaYaz(XmlDocument doc)
        {
           XmlNode node= doc.CreateElement("DataLogger");

           XmlNode DLIsim = doc.CreateElement("DLIsim");
           DLIsim.InnerText = Isim;
           node.AppendChild(DLIsim);
           doc.DocumentElement.FirstChild.AppendChild(node);
           

        }
       
        public String toStringJson()
        {
            String json = "";
            json = "{\"ad\":\"" + _isim + "\",\"sensorSayisi\":\"" + SensorSayisi + "\",\"telefonNumarasi\":\"" + TelefonNumarasi + "\",\"sonKalibrasyonTarihi\":\"" + SonKalibrasyonTarihi + "\"}";

            return json;
        }
        public static bool FormatGecerliMi(string deger)
        {
            int c = deger.Count();
            if (deger.StartsWith("0"))
                return double.Parse(deger) < (10 ^ (c - 2));
            return double.Parse(deger) < (10 ^(c-1));
        }

            public void ParsingText(bool verilerYuklensin=true)
        {
            dt = new DataTable();

            if (verilerYuklensin)
                VerileriDosyadanYukle();
            //alınan Parcalı ogelerden birleşik bir tablo hazırla

            dt.Columns.Add("Tarih", typeof(DateTime));
            string sicaklik1SensorAd = SensorListesi[0].Isim + "-Sicaklik";
            string nem1Sensor = SensorListesi[0].Isim + "-Nem";
            string sicaklik2Sensor = SensorListesi[1].Isim + "-Sicaklik";
            string nem2Sensor = SensorListesi[1].Isim + "-Nem";

            dt.Columns.Add(sicaklik1SensorAd, typeof(float));
            dt.Columns.Add(nem1Sensor, typeof(float));
            dt.Columns.Add(sicaklik2Sensor, typeof(float));
            dt.Columns.Add(nem2Sensor, typeof(float));
            int count = SensorListesi[0]._degerler.Count;
            for (int j = 0; j < count; j++)
            {
                if (j == count - 1)
                    continue;
                if (SensorListesi[0]._degerler[j].Zaman == SensorListesi[0]._degerler[j + 1].Zaman)
                    dt.Rows.Add(
                        SensorListesi[0]._degerler[j].Zaman,
                        SensorListesi[0]._degerler[j].Deger,//1. sensor ısı değeri
                        SensorListesi[0]._degerler[j + 1].Deger,//1. sensor nem degeri
                        SensorListesi[1]._degerler[j].Deger,//2. sensor ısı değeri
                        SensorListesi[1]._degerler[j + 1].Deger);//2. sensor nem değeri
            }
        }
            public void VerileriDosyadanYukle()
            {
                string fname = TxtPath;
                List<NetOlcerBirimi> satirList;
                String AllString = "";
                satirList = new List<NetOlcerBirimi>();
                string allText = System.IO.File.ReadAllText(fname);
                using (StringReader reader = new StringReader(allText))
                {
                    AllString = allText;
                    string line;
                    string[] parcalar;
                    string[] p;
                    string[] parcalar_onceki = new string[5];
                    string[] saat;
                    string[] tarih;
                    int SatirSayisi = 0;
                    int i = 0;
                    int sfr = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        try
                        {
                            if (line.Count() <= 1)
                                continue;
                            if (line.Contains("JERA") || line.Contains("SAAT") || line.Contains(")"))
                                continue;
                            //YENI MODUL p0 saati verecek tarih doldururken lazım olacak
                            p = line.Split(new String[] { "     " }, StringSplitOptions.RemoveEmptyEntries);
                            saat = p[0].Split(new String[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                            //SAATin ilk hour kısmında 2 karakter fazla satır sonu karakteri, onları silme
                            saat[0] = saat[0].Remove(0, 1);
                            // Do something with the line
                            parcalar = p[1].Split(new String[] { "   " }, StringSplitOptions.RemoveEmptyEntries);
                            tarih = parcalar[0].Split(new String[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                            parcalar_onceki = parcalar_onceki.Count() == sfr ? parcalar : parcalar_onceki;
                           
                           
                            if (parcalar.Length == 5)
                            {
                                if (parcalar[4].Length <= 1)
                                    continue;
                                double isi1, isi2, NemSensör1, NemSensör2;
                                if (FormatGecerliMi(parcalar[1].Split(' ')[0]))//eğer txtdeki format geçerli değil ise . yerine , konulur
                                {
                                    isi1 = double.Parse(parcalar[1].Split(' ')[0]);
                                    isi2 = double.Parse(parcalar[3].Split(' ')[0]);
                                    NemSensör1 = double.Parse(parcalar[2].Split(' ')[0]);
                                    NemSensör2 = double.Parse(parcalar[4].Split(' ')[0]);
                                }
                                else
                                {
                                    isi1 = double.Parse(parcalar[1].Split(' ')[0].Replace('.', ','));
                                    isi2 = double.Parse(parcalar[3].Split(' ')[0].Replace('.', ','));
                                    NemSensör1 = double.Parse(parcalar[2].Split(' ')[0].Replace('.', ','));
                                    NemSensör2 = double.Parse(parcalar[4].Split(' ')[0].Replace('.', ','));
                                }
                                i++;
                                SatirSayisi = SatirSayisi + line.Count();
                                //HATA: eğer birbirlerinin yerlerine yazılmışlarsa ama 0 değilse
                                if ((isi1 == NemSensör1 && isi1 != 0) | isi1 == NemSensör2 | (isi2 == NemSensör2 && isi2 != 0) | isi2 == NemSensör1)
                                {
                                    AllString = allText.Remove(SatirSayisi + 1, line.Count());
                                    continue;
                                }
                                try
                                {
                                    //sensör listesinde 2 tane sensör vardır her ikisinde de isi ve nem değerleri vardır.
                                    SensorListesi[0].DegerleEkle(new NetOlcerBirimi
                                    {
                                        Tip = "isi",
                                        Birim = "C",
                                        Zaman = new System.DateTime(int.Parse("20" + tarih[2]), int.Parse(tarih[1]), int.Parse(tarih[0]), int.Parse(saat[0]), int.Parse(saat[1]), int.Parse(saat[2])),
                                        Deger = isi1,
                                        SensorIndex = 0,
                                        Sensor = SensorListesi[0].Isim
                                    });
                                    SensorListesi[0].DegerleEkle(new NetOlcerBirimi
                                    {
                                        Tip = "nem",
                                        Birim = "%",
                                        Zaman = new System.DateTime(int.Parse("20" + tarih[2]), int.Parse(tarih[1]), int.Parse(tarih[0]), int.Parse(saat[0]), int.Parse(saat[1]), int.Parse(saat[2])),
                                        Deger = NemSensör1,//float.Parse(parcalar[2].Split(' ')[0].Replace('.', ',')),
                                        SensorIndex = 0,
                                        Sensor = SensorListesi[0].Isim
                                    });
                                    SensorListesi[1].DegerleEkle(new NetOlcerBirimi
                                    {
                                        Tip = "isi",
                                        Birim = "C",
                                        Zaman = new System.DateTime(int.Parse("20" + tarih[2]), int.Parse(tarih[1]), int.Parse(tarih[0]), int.Parse(saat[0]), int.Parse(saat[1]), int.Parse(saat[2])),
                                        Deger = isi2,//float.Parse(parcalar[3].Split(' ')[0].Replace('.', ',')),
                                        SensorIndex = 1,
                                        Sensor = SensorListesi[1].Isim
                                    });
                                    SensorListesi[1].DegerleEkle(new NetOlcerBirimi
                                    {
                                        Tip = "nem",
                                        Birim = "%",
                                        Zaman = new System.DateTime(int.Parse("20" + tarih[2]), int.Parse(tarih[1]), int.Parse(tarih[0]), int.Parse(saat[0]), int.Parse(saat[1]), int.Parse(saat[2])),
                                        Deger = NemSensör2,// float.Parse(parcalar[4].Split(' ')[0].Replace('.', ',')),
                                        SensorIndex = 1,
                                        Sensor = SensorListesi[1].Isim
                                    });

                                }
                                catch
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine(line);
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            //public static async Task VerileriDosyadanYukleAync()
            //{
            //    String result = "", cr;
            //    string line;
            //    string[] parcalar;
            //    string[] p;
            //    string[] parcalar_onceki = new string[5];
            //    string[] saat;
            //    string[] tarih;
            //    int SatirSayisi = 0;
            //    int i = 0;
            //    int sfr = 0;
            //    List<NetOlcerBirimi> SensorListesi = new List<NetOlcerBirimi>();
            //    // FileStream fileStream = new FileStream(@"D:\git\Net olcer\Arayuz\bin\Release\Koridor2.txt", FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
            //    using (StreamReader reader = File.OpenText(@"D:\git\Net olcer\Arayuz\bin\Release\Koridor2.txt"))
            //    {
            //        Console.WriteLine("Opened file.");
            //        while ((cr = await reader.ReadLineAsync()) != null)
            //        {
            //            try
            //            {
            //                if (line.Count() <= 1)
            //                    continue;
            //                if (line.Contains("JERA") || line.Contains("SAAT") || line.Contains(")"))
            //                    continue;
            //                //YENI MODUL p0 saati verecek tarih doldururken lazım olacak
            //                p = line.Split(new String[] { "     " }, StringSplitOptions.RemoveEmptyEntries);
            //                saat = p[0].Split(new String[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            //                //SAATin ilk hour kısmında 2 karakter fazla satır sonu karakteri, onları silme
            //                saat[0] = saat[0].Remove(0, 1);
            //                // Do something with the line
            //                parcalar = p[1].Split(new String[] { "   " }, StringSplitOptions.RemoveEmptyEntries);
            //                tarih = parcalar[0].Split(new String[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            //                parcalar_onceki = parcalar_onceki.Count() == sfr ? parcalar : parcalar_onceki;

            //                if (parcalar.Length == 5)
            //                {
            //                    if (parcalar[4].Length <= 1)
            //                        continue;
            //                    double isi1, isi2, NemSensör1, NemSensör2;
            //                    if (FormatGecerliMi(parcalar[1].Split(' ')[0]))//eğer txtdeki format geçerli değil ise . yerine , konulur
            //                    {
            //                        isi1 = double.Parse(parcalar[1].Split(' ')[0]);
            //                        isi2 = double.Parse(parcalar[3].Split(' ')[0]);
            //                        NemSensör1 = double.Parse(parcalar[2].Split(' ')[0]);
            //                        NemSensör2 = double.Parse(parcalar[4].Split(' ')[0]);
            //                    }
            //                    else
            //                    {
            //                        isi1 = double.Parse(parcalar[1].Split(' ')[0].Replace('.', ','));
            //                        isi2 = double.Parse(parcalar[3].Split(' ')[0].Replace('.', ','));
            //                        NemSensör1 = double.Parse(parcalar[2].Split(' ')[0].Replace('.', ','));
            //                        NemSensör2 = double.Parse(parcalar[4].Split(' ')[0].Replace('.', ','));
            //                    }
            //                    i++;
            //                    SatirSayisi = SatirSayisi + line.Count();
            //                    //HATA: eğer birbirlerinin yerlerine yazılmışlarsa ama 0 değilse
            //                    if ((isi1 == NemSensör1 && isi1 != 0) | isi1 == NemSensör2 | (isi2 == NemSensör2 && isi2 != 0) | isi2 == NemSensör1)
            //                    {
            //                        AllString = allText.Remove(SatirSayisi + 1, line.Count());
            //                        continue;
            //                    }
            //                    try
            //                    {
            //                        //sensör listesinde 2 tane sensör vardır her ikisinde de isi ve nem değerleri vardır.
            //                        SensorListesi[0].DegerleEkle(new NetOlcerBirimi
            //                        {
            //                            Tip = "isi",
            //                            Birim = "C",
            //                            Zaman = new System.DateTime(int.Parse("20" + tarih[2]), int.Parse(tarih[1]), int.Parse(tarih[0]), int.Parse(saat[0]), int.Parse(saat[1]), int.Parse(saat[2])),
            //                            Deger = isi1,
            //                            SensorIndex = 0,
            //                            Sensor = SensorListesi[0].Isim
            //                        });
            //                        SensorListesi[0].DegerleEkle(new NetOlcerBirimi
            //                        {
            //                            Tip = "nem",
            //                            Birim = "%",
            //                            Zaman = new System.DateTime(int.Parse("20" + tarih[2]), int.Parse(tarih[1]), int.Parse(tarih[0]), int.Parse(saat[0]), int.Parse(saat[1]), int.Parse(saat[2])),
            //                            Deger = NemSensör1,//float.Parse(parcalar[2].Split(' ')[0].Replace('.', ',')),
            //                            SensorIndex = 0,
            //                            Sensor = SensorListesi[0].Isim
            //                        });
            //                        SensorListesi[1].DegerleEkle(new NetOlcerBirimi
            //                        {
            //                            Tip = "isi",
            //                            Birim = "C",
            //                            Zaman = new System.DateTime(int.Parse("20" + tarih[2]), int.Parse(tarih[1]), int.Parse(tarih[0]), int.Parse(saat[0]), int.Parse(saat[1]), int.Parse(saat[2])),
            //                            Deger = isi2,//float.Parse(parcalar[3].Split(' ')[0].Replace('.', ',')),
            //                            SensorIndex = 1,
            //                            Sensor = SensorListesi[1].Isim
            //                        });
            //                        SensorListesi[1].DegerleEkle(new NetOlcerBirimi
            //                        {
            //                            Tip = "nem",
            //                            Birim = "%",
            //                            Zaman = new System.DateTime(int.Parse("20" + tarih[2]), int.Parse(tarih[1]), int.Parse(tarih[0]), int.Parse(saat[0]), int.Parse(saat[1]), int.Parse(saat[2])),
            //                            Deger = NemSensör2,// float.Parse(parcalar[4].Split(' ')[0].Replace('.', ',')),
            //                            SensorIndex = 1,
            //                            Sensor = SensorListesi[1].Isim
            //                        });

            //                    }
            //                    catch
            //                    {
            //                        continue;
            //                    }
            //                }
            //                else
            //                {
            //                    Console.WriteLine(line);
            //                }
            //            }
            //            catch
            //            {
            //                continue;
            //            }
            //        }
            //    }
            //}
        public string HataAyikla(string AllString)
    {
        using (StringReader reader = new StringReader(AllString))
        {
            string line;
            string[] parcalar;
            string[] parcalar_onceki = new string[5];
            int sfr = 0;
            int SatirSayisi=0;
            int i=0;
            while ((line = reader.ReadLine()) != null)
            {
                // Do something with the line
                parcalar = line.Split(new String[] { "   " }, StringSplitOptions.RemoveEmptyEntries);
                parcalar_onceki = parcalar_onceki.Count() == sfr ? parcalar : parcalar_onceki;
                i++;
                SatirSayisi=SatirSayisi + line.Count()+1;
                float isi1 = float.Parse(parcalar[1].Split(' ')[0].Replace('.', ','));
                float isi2 = float.Parse(parcalar[3].Split(' ')[0].Replace('.', ','));
                float NemSensör1 = float.Parse(parcalar[2].Split(' ')[0].Replace('.', ','));
                float NemSensör2 = float.Parse(parcalar[4].Split(' ')[0].Replace('.', ','));
                //HATA: eğer birbirlerinin yerlerine yazılmışlarsa
                if (parcalar.Length == 5)
                {
                    if (parcalar[1] == parcalar[2] || parcalar[2] == parcalar[3] || parcalar[3] == parcalar[4]) { AllString = AllString.Remove(SatirSayisi + 1, line.Count()); }

                }
            }
        }
        return AllString;
    }

        public NetOlcerBirimi EnYuksekSicaklik()
        {
            List<NetOlcerBirimi> tumısılar = new List<NetOlcerBirimi>();
            tumısılar.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "isi"));
            tumısılar.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "isi"));
            return DataLogger.EnYuksekDeger(tumısılar);
        }
        public NetOlcerBirimi EnYuksekSicaklik(DateTime altTarih, DateTime ustTarih)
        {
            List<NetOlcerBirimi> tumısılar = new List<NetOlcerBirimi>();
            tumısılar.AddRange(SensorListesi[0]._degerler.Where(x=>x.Tip=="isi" && x.Zaman>=altTarih && x.Zaman<=ustTarih));
            tumısılar.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "isi" && x.Zaman >= altTarih && x.Zaman <= ustTarih));

            return DataLogger.EnYuksekDeger(tumısılar);
        }
        public static NetOlcerBirimi EnYuksekDeger(List<NetOlcerBirimi> veriler)
        {
            if (veriler.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            double max = double.MinValue;
            NetOlcerBirimi enyuksrk = new NetOlcerBirimi();
            foreach (NetOlcerBirimi type in veriler)
            {
                if (type.Deger > max)
                {
                    max = type.Deger;
                    enyuksrk = type;
                }
            }
            return enyuksrk;
        }
        public NetOlcerBirimi EnDusukSicaklik()
        {
            List<NetOlcerBirimi> tumısılar = new List<NetOlcerBirimi>();
            tumısılar.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "isi"));
            tumısılar.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "isi"));
            return DataLogger.EnDusukDeger(tumısılar);
        }
        public NetOlcerBirimi EnDusukSicaklik(DateTime altTarih, DateTime ustTarih)
        {
            List<NetOlcerBirimi> tumısılar = new List<NetOlcerBirimi>();
            tumısılar.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "isi" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
            tumısılar.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "isi" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
            return DataLogger.EnDusukDeger(tumısılar);
        }
        public static NetOlcerBirimi EnDusukDeger(List<NetOlcerBirimi> veriler)
        {
            if (veriler.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            double min = double.MaxValue;
            NetOlcerBirimi endusuk = new NetOlcerBirimi() ;
            foreach (NetOlcerBirimi type in veriler)
            {
                if (type.Deger < min)
                {
                    min = type.Deger;
                    endusuk = type;
                }
            }
            return endusuk;
        }
        public NetOlcerBirimi EnYuksekNem()
        {
            List<NetOlcerBirimi> tumısılar = new List<NetOlcerBirimi>();
            tumısılar.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "nem"));
            tumısılar.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "nem"));

            return DataLogger.EnYuksekDeger(tumısılar);
        }
        public NetOlcerBirimi EnYuksekNem(DateTime altTarih, DateTime ustTarih)
        {
            List<NetOlcerBirimi> tumısılar = new List<NetOlcerBirimi>();
            tumısılar.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "nem" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
            tumısılar.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "nem" && x.Zaman >= altTarih && x.Zaman <= ustTarih));

            return DataLogger.EnYuksekDeger(tumısılar);
        }

        public NetOlcerBirimi EnDusukNem()
        {
            List<NetOlcerBirimi> tumısılar = new List<NetOlcerBirimi>();
            tumısılar.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "nem"));
            tumısılar.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "nem"));
            return DataLogger.EnDusukDeger(tumısılar);
        }
        public NetOlcerBirimi EnDusukNem(DateTime altTarih, DateTime ustTarih)
        {
            List<NetOlcerBirimi> tumısılar = new List<NetOlcerBirimi>();
            tumısılar.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "nem" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
            tumısılar.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "nem" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
            return DataLogger.EnDusukDeger(tumısılar);
        }

        public double OrtalamaNem(int sensorIndex = -1)
        {
            double ortalama = 0;
            if (sensorIndex < 0)
            {
                List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
                tumnemler.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "nem" ));
                tumnemler.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "nem" ));
                ortalama = DataLogger.OrtalamaDeger(tumnemler);
            }
            else
            {
                List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
                tumnemler.AddRange(SensorListesi[sensorIndex]._degerler.Where(x => x.Tip == "nem" ));
                ortalama = DataLogger.OrtalamaDeger(tumnemler);
            }
            return ortalama;
        }
        public double OrtalamaNem(DateTime altTarih, DateTime ustTarih, int sensorIndex=-1 )
        {
            double ortalama = 0;
            if (sensorIndex < 0)
            {
                List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
                tumnemler.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "nem" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
                tumnemler.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "nem" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
                ortalama = DataLogger.OrtalamaDeger(tumnemler);
            }
            else
            {
                List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
                tumnemler.AddRange(SensorListesi[sensorIndex]._degerler.Where(x => x.Tip == "nem" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
                ortalama = DataLogger.OrtalamaDeger(tumnemler);
            }
            return Math.Round(ortalama, 3);
        }
        public static double OrtalamaDeger(List<NetOlcerBirimi> veriler)
        {
            if (veriler.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            double ortalama = 0;
            foreach (NetOlcerBirimi type in veriler)
            {
                    ortalama += type.Deger;
            }
            return Math.Round(ortalama/veriler.Count,3);
        }
        public double OrtalamaSicaklik( int sensorIndex=-1)
        {
            double ortalama = 0;
            if (sensorIndex < 0)
            {
                List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
                tumnemler.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "isi" ));
                tumnemler.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "isi"));
                ortalama = DataLogger.OrtalamaDeger(tumnemler);
            }
            else
            {
                List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
                tumnemler.AddRange(SensorListesi[sensorIndex]._degerler.Where(x => x.Tip == "isi"));
                ortalama = DataLogger.OrtalamaDeger(tumnemler);
            }
            return ortalama;
        }
        public double OrtalamaSicaklik(DateTime altTarih, DateTime ustTarih, int sensorIndex=-1)
        {
            double ortalama = 0;
            if (sensorIndex < 0)
            {
                List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
                tumnemler.AddRange(SensorListesi[0]._degerler.Where(x => x.Tip == "isi" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
                tumnemler.AddRange(SensorListesi[1]._degerler.Where(x => x.Tip == "isi" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
                ortalama = DataLogger.OrtalamaDeger(tumnemler);
            }
            else
            {
                List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
                tumnemler.AddRange(SensorListesi[sensorIndex]._degerler.Where(x => x.Tip == "isi" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
                ortalama = DataLogger.OrtalamaDeger(tumnemler);
            }
            return ortalama;
        }
        public DateTime IlkTarih()
        {
            if (SensorListesi[0]._degerler.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            //iki sensörde aynı tarihte başlar
            return SensorListesi[0]._degerler.First().Zaman;
        }
        public DateTime SonTarih()
        {
            if (SensorListesi[0]._degerler.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            //iki sensörde aynı tarihte başlar
            return SensorListesi[0]._degerler.Last().Zaman;
        }



        public Chart getChart(DateTime ilktrh, DateTime sntrh,string tip="isi")
        {
           
            if (chartDataLogger == null)
            {
                chartDataLogger = new Chart();
                chartDataLogger.Visible = true;
                this.chartDataLogger.Size = new System.Drawing.Size(704, 477);

                var chartArea = new ChartArea();
                chartArea.AxisX.LabelStyle.Format = "dd/MMM\nhh:mm";
                chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
                chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
                chartArea.AxisX.LabelStyle.Font = new Font("Consolas", 8);
                chartArea.AxisY.LabelStyle.Font = new Font("Consolas", 8);

                chartDataLogger.ChartAreas.Add(chartArea);
                double interval = FrmGrafikGosterim.intervalHesapla(ilktrh, sntrh);
                chartDataLogger.ChartAreas[0].AxisX.Interval = interval; 
                chartDataLogger.ChartAreas[0].AxisY.Interval = 1;
                chartDataLogger.ChartAreas[0].AxisY.Minimum = Math.Round(EnDusukSicaklik(ilktrh, sntrh).Deger - 0.4, 3);
                chartDataLogger.ChartAreas[0].AxisY.Maximum = Math.Round(EnYuksekSicaklik(ilktrh, sntrh).Deger + 0.4, 3);
                Series series1 = chartDataLogger.Series.Add(Isim + " - " + SensorListesi[0].Isim);
                series1.Points.DataBindXY(SensorListesi[0]._degerler.Where(x => x.Tip == tip && x.Zaman > ilktrh && x.Zaman < sntrh).Select(x => x.Zaman).ToArray(), SensorListesi[0]._degerler.Where(x => x.Tip == tip && x.Zaman > ilktrh && x.Zaman < sntrh).Select(x => x.Deger).ToArray());
                series1.ChartType = SeriesChartType.Line;
                Series series2 = chartDataLogger.Series.Add(Isim + " - " + SensorListesi[1].Isim);
                series2.Points.DataBindXY(SensorListesi[1]._degerler.Where(x => x.Tip == tip && x.Zaman > ilktrh && x.Zaman < sntrh).Select(x => x.Zaman).ToArray(), SensorListesi[1]._degerler.Where(x => x.Tip == tip && x.Zaman > ilktrh && x.Zaman < sntrh).Select(x => x.Deger).ToArray());
                series2.ChartType = SeriesChartType.Line;
            }
            return chartDataLogger;
        }
    }
    [XmlRoot("DataLoggerlar")]
    [XmlType("DataLoggerListesi")]
    public class DataLoggerListesi  
    {
        [XmlArray("DataLoggerlar")]
        [XmlArrayItem("DataLogger")]
        public List<DataLogger> DataLoggerlar { get; set; }
        [XmlElement("KurumID")]
        public string KurumID { get; set; }
        [XmlElement("KurumAdi")]
        public string KurumAdi { get; set; }
        [XmlElement("Adres")]
        public string Adres { get; set; }
        [XmlElement("Sehir")]
        public string Sehir { get; set; }
        [XmlElement("TelNo")]
        public string TelNo { get; set; }
        [XmlElement("Mail")]
        public string Mail { get; set; }
        [XmlElement("password")]
        public string Password { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        Chart genelChart=null;

        public NetOlcerBirimi EnYuksekSicaklik()
        {
            //data loggerlardaki tümünü tek listede toplayıp içerisinden en yüksek sıcaklığı verme
            List<NetOlcerBirimi> allList = new List<NetOlcerBirimi>();
            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "isi"));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "isi"));
            }
            return DataLogger.EnYuksekDeger(allList);
        }
        public String toStringJson()
        {
            String json = "";
            json = "{\"kurum\":\"" + KurumAdi + "\",\"adres\":\""+Adres+"\",\"sehir\":\""+Sehir+"\",\"telno\":\""+TelNo+"\",\"mail\":\""+Mail+"\",\"password\":\""+Password+"\"}";

            return json;
        }
        public NetOlcerBirimi EnYuksekSicaklik(DateTime altTarih, DateTime ustTarih)
        {
            List<NetOlcerBirimi> allList = new List<NetOlcerBirimi>();
            string dlAdi = "";
            double enYuksekDeger=-1;
            DateTime zaman=new DateTime();
            foreach (DataLogger dl in DataLoggerlar)
            {
                //if (dl.SensorListesi[0]._degerler.Count > 0)
                //    allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "isi" && x.Zaman > altTarih && x.Zaman < ustTarih));
                //if (dl.SensorListesi[1]._degerler.Count > 0)
                //    allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "isi" && x.Zaman > altTarih && x.Zaman < ustTarih));
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "isi" && x.Zaman>altTarih&&x.Zaman<ustTarih));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "isi" && x.Zaman>altTarih&&x.Zaman<ustTarih));
                NetOlcerBirimi nob = DataLogger.EnYuksekDeger(allList);
                if (enYuksekDeger < nob.Deger)
                {
                    enYuksekDeger = nob.Deger;
                    dlAdi = dl.Isim + " - " + nob.Sensor;
                    zaman=nob.Zaman;
                }
            }
            return new NetOlcerBirimi { Birim = "C", Sensor = dlAdi, Deger = enYuksekDeger, SensorIndex = -1, Tip = "isi", Zaman = zaman };
        }
       
        public NetOlcerBirimi EnDusukSicaklik()
        {
            List<NetOlcerBirimi> allList = new List<NetOlcerBirimi>();
            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "isi"));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "isi"));
            }
            return DataLogger.EnDusukDeger(allList);
          
          
        }
        public NetOlcerBirimi EnDusukSicaklik(DateTime altTarih, DateTime ustTarih)
        {
            List<NetOlcerBirimi> allList = new List<NetOlcerBirimi>();
             string dlAdi = "";
            double enYuksekDeger=111;
            DateTime zaman=new DateTime();
            foreach (DataLogger dl in DataLoggerlar)
            {
            //    if (dl.SensorListesi[0]._degerler.Count > 0)
            //        allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "isi" && x.Zaman > altTarih && x.Zaman < ustTarih));
            //    if (dl.SensorListesi[1]._degerler.Count > 0)
            //        allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "isi" && x.Zaman > altTarih && x.Zaman < ustTarih));
            //}

            //return DataLogger.EnDusukDeger(allList);
                if (dl.Dt == null)
                    continue;
                  if (dl.SensorListesi[0]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "isi" && x.Zaman>altTarih&&x.Zaman<ustTarih));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "isi" && x.Zaman>altTarih&&x.Zaman<ustTarih));
                NetOlcerBirimi nob = DataLogger.EnDusukDeger(allList);
                if (enYuksekDeger > nob.Deger)
                {
                    enYuksekDeger = nob.Deger;
                    dlAdi = dl.Isim + " - " + nob.Sensor;
                    zaman=nob.Zaman;
                }
            }
            return new NetOlcerBirimi { Birim = "C", Sensor = dlAdi, Deger = enYuksekDeger, SensorIndex = -1, Tip = "isi", Zaman = zaman };
        }

        public NetOlcerBirimi EnYuksekNem()
        {
            List<NetOlcerBirimi> allList = new List<NetOlcerBirimi>();
            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "nem"));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "nem"));
            }
            return DataLogger.EnYuksekDeger(allList);
        }
        public NetOlcerBirimi EnYuksekNem(DateTime altTarih, DateTime ustTarih)
        {
            List<NetOlcerBirimi> allList = new List<NetOlcerBirimi>();
            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "nem" && x.Zaman > altTarih && x.Zaman < ustTarih));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "nem" && x.Zaman > altTarih && x.Zaman < ustTarih));
            }
            return DataLogger.EnYuksekDeger(allList);
        }

        public NetOlcerBirimi EnDusukNem()
        {
            List<NetOlcerBirimi> allList = new List<NetOlcerBirimi>();
            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "nem"));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "nem"));
            }
            return DataLogger.EnDusukDeger(allList);
        }
        public NetOlcerBirimi EnDusukNem(DateTime altTarih, DateTime ustTarih)
        {
            List<NetOlcerBirimi> allList = new List<NetOlcerBirimi>();
            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "nem" && x.Zaman > altTarih && x.Zaman < ustTarih));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    allList.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "nem" && x.Zaman > altTarih && x.Zaman < ustTarih));
            }
            return DataLogger.EnDusukDeger(allList);
        }
      
        public double OrtalamaNem()
        {
            double ortalama = 0;
            List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();

            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    tumnemler.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "nem"));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    tumnemler.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "nem"));
            }
            ortalama = DataLogger.OrtalamaDeger(tumnemler);

            return ortalama;
        }
        public double OrtalamaNem(DateTime altTarih, DateTime ustTarih)
        {
            double ortalama = 0;
            List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();

            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    tumnemler.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "nem" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    tumnemler.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "nem" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
            }
            ortalama = DataLogger.OrtalamaDeger(tumnemler);

            return ortalama;
        }
    
        public double OrtalamaSicaklik()
        {
            double ortalama = 0;
            List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count > 0)
                     tumnemler.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "isi"));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                    tumnemler.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "isi"));
            }
            ortalama = DataLogger.OrtalamaDeger(tumnemler);
            return ortalama;
        }
        public double OrtalamaSicaklik(DateTime altTarih, DateTime ustTarih)
        {
            double ortalama = 0;
            List<NetOlcerBirimi> tumnemler = new List<NetOlcerBirimi>();
            foreach (DataLogger dl in DataLoggerlar)
            {
                if (dl.Dt == null)
                    continue;
                if (dl.SensorListesi[0]._degerler.Count>0)
                    tumnemler.AddRange(dl.SensorListesi[0]._degerler.Where(x => x.Tip == "isi" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
                if (dl.SensorListesi[1]._degerler.Count > 0)
                        tumnemler.AddRange(dl.SensorListesi[1]._degerler.Where(x => x.Tip == "isi" && x.Zaman >= altTarih && x.Zaman <= ustTarih));
            }
            ortalama = DataLogger.OrtalamaDeger(tumnemler);
            return ortalama;
        }
        public Chart getChart( NetOlcerCiktiOptions nocp,string tip="isi")
        {
           
            if (genelChart == null)
            {
                genelChart = new Chart();
                genelChart.Visible = true;
                this.genelChart.Size = new System.Drawing.Size(704, 477);

                var chartArea = new ChartArea();
                chartArea.AxisX.LabelStyle.Format = "dd/MMM,hh:mm";
                chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
                chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
                chartArea.AxisX.LabelStyle.Font = new Font("Consolas", 8);
                chartArea.AxisY.LabelStyle.Font = new Font("Consolas", 8);
                genelChart.ChartAreas.Add(chartArea);
                DateTime ilktrh =nocp.Tarih(), sontrh=nocp.Tarih("son");
                double interval = FrmGrafikGosterim.intervalHesapla(ilktrh, sontrh);
                genelChart.ChartAreas[0].AxisX.Interval = interval;
                genelChart.ChartAreas[0].AxisY.Interval = 1;
                genelChart.ChartAreas[0].AxisY.Minimum = Math.Round(EnDusukSicaklik(ilktrh, sontrh).Deger - 0.4, 3);
                genelChart.ChartAreas[0].AxisY.Maximum = Math.Round(EnYuksekSicaklik(ilktrh, sontrh).Deger + 0.4, 3);
                foreach (DataLogger dl in DataLoggerlar)
                {
                    if (!nocp.CiktiIcinDataLoggerlar.Exists(x => x.DataLoggerAdi == dl.Isim))
                        continue;
                    Series series1 = genelChart.Series.Add(dl.Isim + " - " + dl.SensorListesi[0].Isim);
                    series1.SetDefault(true);
                    series1.Enabled = true;
                    CiktiDataLoggerItem cdli = nocp.CiktiIcinDataLoggerlar.Find(x=>x.DataLoggerAdi==dl.Isim);
                    if(cdli.BaslangicTarihi != null && cdli.SonlanisTarihi!=null)
                        series1.Points.DataBindXY(dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip && x.Zaman > cdli.BaslangicTarihi && x.Zaman < cdli.SonlanisTarihi).Select(x => x.Zaman).ToArray(), dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip && x.Zaman > cdli.BaslangicTarihi && x.Zaman < cdli.SonlanisTarihi).Select(x => x.Deger).ToArray());
                    else
                        series1.Points.DataBindXY(dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip ).Select(x => x.Zaman).ToArray(), dl.SensorListesi[0]._degerler.Where(x => x.Tip == tip ).Select(x => x.Deger).ToArray());

                    series1.ChartType = SeriesChartType.Line;

                    Series series2 = genelChart.Series.Add(dl.Isim + " - " + dl.SensorListesi[1].Isim);
                    series2.SetDefault(true);
                    series2.Enabled = true;
                    series2.Points.DataBindXY(dl.SensorListesi[1]._degerler.Where(x => x.Tip == tip && x.Zaman > cdli.BaslangicTarihi && x.Zaman < cdli.SonlanisTarihi).Select(x => x.Zaman).ToArray(), dl.SensorListesi[1]._degerler.Where(x => x.Tip == tip && x.Zaman > cdli.BaslangicTarihi && x.Zaman < cdli.SonlanisTarihi).Select(x => x.Deger).ToArray());

                    series2.ChartType = SeriesChartType.Line;
                }

            }
            genelChart.Invalidate();
            return genelChart;
        }

        public DateTime OlcumBaslangicTarihi()
        {
            DateTime ilkTarih = DateTime.MaxValue;
            foreach (var dl in DataLoggerlar)
            {
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    ilkTarih = dl.IlkTarih() < ilkTarih ? dl.IlkTarih() : ilkTarih;
            }
            return ilkTarih;
        }
        public DateTime OlcumBitisTarihi()
        {
            DateTime sonTarih = DateTime.MinValue;
            foreach (var dl in DataLoggerlar)
            {
                if (dl.SensorListesi[0]._degerler.Count > 0)
                    sonTarih = dl.SonTarih() > sonTarih ? dl.SonTarih() : sonTarih;
            }
            return sonTarih;
        }
        public String ToplamOlcumSuresi()
        {
            int günler = (int)((OlcumBitisTarihi() - OlcumBaslangicTarihi()).TotalDays);
            return günler.ToString() + " Gün"; 
        }
       

       
    }
}
