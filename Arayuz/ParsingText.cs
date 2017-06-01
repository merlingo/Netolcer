using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arayuz
{
    public class ParsingText
    {
         List<ParcalaOge> satirList;
        DataTable dt;
        public DataTable DT
        {
            get
            {
                return dt;
            }
        }
      public  String AllString = "";
      public ParsingText(String fname, string sicaklik1SensorAd = "Sicaklik(1.Sensör)", string sicaklik2Sensor = "Sicaklik(2.Sensör)", string nem1Sensor = "Nem(1.Sensör)", string nem2Sensor = "Nem(2.Sensör)")
        {
            satirList = new List<ParcalaOge>();
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
                            float isi1 = float.Parse(parcalar[1].Split(' ')[0].Replace('.', ','));
                            float isi2 = float.Parse(parcalar[3].Split(' ')[0].Replace('.', ','));
                            float NemSensör1 = float.Parse(parcalar[2].Split(' ')[0].Replace('.', ','));
                            float NemSensör2 = float.Parse(parcalar[4].Split(' ')[0].Replace('.', ','));
                            i++;
                            SatirSayisi = SatirSayisi + line.Count();
                            //HATA: eğer birbirlerinin yerlerine yazılmışlarsa
                            if (isi1 == NemSensör1 | isi1 == NemSensör2 | isi2 == NemSensör2 | isi2 == NemSensör1)
                            {
                                AllString = allText.Remove(SatirSayisi + 1, line.Count());
                                continue;
                            }


                            try
                            {

                                satirList.Add(new ParcalaOge
                                {
                                    isi1 = float.Parse(parcalar[1].Split(' ')[0].Replace('.', ',')),
                                    isi2 = float.Parse(parcalar[3].Split(' ')[0].Replace('.', ',')),
                                    NemSensör1 = float.Parse(parcalar[2].Split(' ')[0].Replace('.', ',')),
                                    NemSensör2 = float.Parse(parcalar[4].Split(' ')[0].Replace('.', ',')),
                                    tarih = new System.DateTime(int.Parse("20" + tarih[2]), int.Parse(tarih[1]), int.Parse(tarih[0]), int.Parse(saat[0]), int.Parse(saat[1]), int.Parse(saat[2]))
                                });
                                parcalar_onceki = parcalar;
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
                //alınan Parcalı ogelerden birleşik bir tablo hazırla
                dt = new DataTable();
                dt.Columns.Add("Tarih", typeof(DateTime));
                dt.Columns.Add(sicaklik1SensorAd, typeof(float));
                dt.Columns.Add(sicaklik2Sensor, typeof(float));
                dt.Columns.Add(nem1Sensor, typeof(float));
                dt.Columns.Add(nem2Sensor, typeof(float));
                foreach (ParcalaOge po in satirList)
                {
                    dt.Rows.Add(po.tarih, po.isi1, po.isi2, po.NemSensör1, po.NemSensör2);
                }
               
            }
        }
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

    }

     class ParcalaOge
    {
        public float isi1;
        public float isi2;
        public float NemSensör1;
        public float NemSensör2;
        public DateTime tarih;
        
    }
   
}
