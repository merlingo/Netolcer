using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Arayuz
{
    public class XmlHelper
    {
        public static void xmleDataLoggerEkle(DataLoggerListesi dataloggerlar, DataLogger dl)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(ConstantValues.path);
            StringWriter sww = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(dl.GetType());
                x.Serialize(writer, dl);
                XmlDocumentFragment fragment = xml.CreateDocumentFragment();
                string str = sww.ToString();
                fragment.InnerXml = str.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "").Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                xml.DocumentElement.FirstChild.AppendChild(fragment);
                xml.Save(ConstantValues.path);
                dataloggerlar.DataLoggerlar.Add(dl);

            }
        }

        public static void xmlDataLoggerGuncelle(DataLogger dl, string ad)
        {

            XmlDocument xml = new XmlDocument();
            xml.Load(ConstantValues.path);
            XmlNodeList elements = xml.SelectNodes("//DataLoggerlar//DataLoggerlar");
            foreach (XmlNode element in elements.Item(0))
            {
                if (element.InnerXml.Contains(ad))
                {
                    XmlElement xlDlIsim = xml.CreateElement("DLIsim");
                    xlDlIsim.InnerText = dl.Isim;
                    element.ReplaceChild(xlDlIsim, element.ChildNodes.Item(0));

                    XmlElement xlKonum = xml.CreateElement("Konum");
                    xlKonum.InnerText = dl.Konum.ToString();
                    element.ReplaceChild(xlKonum, element.ChildNodes.Item(1));

                    XmlElement xTxtPath = xml.CreateElement("TxtPath");
                    xTxtPath.InnerText = dl.TxtPath;
                    element.ReplaceChild(xTxtPath, element.ChildNodes.Item(2));

                    XmlElement xlId = xml.CreateElement("Id");
                    xlId.InnerText = dl.Id;
                    element.ReplaceChild(xlId, element.ChildNodes.Item(3));

                    XmlElement xlDLSensorSayisi = xml.CreateElement("DLSensorSayisi");
                    xlDLSensorSayisi.InnerText = dl.SensorSayisi.ToString();
                    element.ReplaceChild(xlDLSensorSayisi, element.ChildNodes.Item(4));

                    XmlElement xlTelefonNumarasi = xml.CreateElement("TelefonNumarasi");
                    xlTelefonNumarasi.InnerText = dl.TelefonNumarasi;
                    element.ReplaceChild(xlTelefonNumarasi, element.ChildNodes.Item(5));


                    XmlElement xlSonKalibrasyonTarihi = xml.CreateElement("SonKalibrasyonTarihi");
                    xlSonKalibrasyonTarihi.InnerText = dl.SonKalibrasyonTarihi.ToString("o");
                    element.ReplaceChild(xlSonKalibrasyonTarihi, element.ChildNodes.Item(6));

                    XmlElement xlTehlikeliUstSicaklik = xml.CreateElement("TehlikeliUstSicaklik");
                    xlTehlikeliUstSicaklik.InnerText = dl.TehlikeliUstSicaklik.ToString();

                    XmlElement xlTehlikeliAltSicaklik = xml.CreateElement("TehlikeliAltSicaklik");
                    xlTehlikeliAltSicaklik.InnerText = dl.TehlikeliAltSicaklik.ToString();

                    XmlElement xlTehlikeliUstNem = xml.CreateElement("TehlikeliUstNem");
                    xlTehlikeliUstNem.InnerText = dl.TehlikeliUstNem.ToString();

                    XmlElement xlTehlikeliAltNem = xml.CreateElement("TehlikeliAltNem");
                    xlTehlikeliAltNem.InnerText = dl.TehlikeliAltNem.ToString();

                    XmlElement xlTehlikeliUstSicaklik2 = xml.CreateElement("TehlikeliUstSicaklik2");
                    xlTehlikeliUstSicaklik2.InnerText = dl.TehlikeliUstSicaklik2.ToString();

                    XmlElement xlTehlikeliAltSicaklik2 = xml.CreateElement("TehlikeliAltSicaklik2");
                    xlTehlikeliAltSicaklik2.InnerText = dl.TehlikeliAltSicaklik2.ToString();

                    XmlElement xlTehlikeliUstNem2 = xml.CreateElement("TehlikeliUstNem2");
                    xlTehlikeliUstNem2.InnerText = dl.TehlikeliUstNem2.ToString();

                    XmlElement xlTehlikeliAltNem2 = xml.CreateElement("TehlikeliAltNem2");
                    xlTehlikeliAltNem2.InnerText = dl.TehlikeliAltNem2.ToString();
                    if (element.ChildNodes.Count <= 12)
                    {
                        element.AppendChild(xlTehlikeliUstSicaklik);
                        element.AppendChild(xlTehlikeliAltSicaklik);
                        element.AppendChild(xlTehlikeliUstNem);
                        element.AppendChild(xlTehlikeliAltNem);
                        element.AppendChild(xlTehlikeliUstSicaklik2);
                        element.AppendChild(xlTehlikeliAltSicaklik2);
                        element.AppendChild(xlTehlikeliUstNem2);
                        element.AppendChild(xlTehlikeliAltNem2);
                    }
                    else
                    {
                        element.ReplaceChild(xlTehlikeliUstSicaklik, element.ChildNodes.Item(8));
                        element.ReplaceChild(xlTehlikeliAltSicaklik, element.ChildNodes.Item(9));
                        element.ReplaceChild(xlTehlikeliUstNem, element.ChildNodes.Item(10));
                        element.ReplaceChild(xlTehlikeliAltNem, element.ChildNodes.Item(11));
                        element.ReplaceChild(xlTehlikeliUstSicaklik2, element.ChildNodes.Item(12));
                        element.ReplaceChild(xlTehlikeliAltSicaklik2, element.ChildNodes.Item(13));
                        element.ReplaceChild(xlTehlikeliUstNem2, element.ChildNodes.Item(14));
                        element.ReplaceChild(xlTehlikeliAltNem2, element.ChildNodes.Item(15));
                    }



                    XmlElement xlSensorListesi = xml.CreateElement("SensorListesi");
                    XmlElement xlSensor1 = xml.CreateElement("Sensor");
                    XmlElement xlSensorIndex = xml.CreateElement("SensorIndex");
                    xlSensorIndex.InnerText = dl.SensorListesi[0].Index.ToString();
                    xlSensor1.AppendChild(xlSensorIndex);
                    XmlElement xlSensorIsim = xml.CreateElement("SensorIsim");
                    xlSensorIsim.InnerText = dl.SensorListesi[0].Isim;
                    xlSensor1.AppendChild(xlSensorIsim);
                    xlSensor1.AppendChild(xml.CreateElement("_degerler"));
                    xlSensor1.AppendChild(xml.CreateElement("Konum"));
                    xlSensorListesi.AppendChild(xlSensor1);

                    XmlElement xlSensor2 = xml.CreateElement("Sensor");
                    XmlElement xlSensorIndex2 = xml.CreateElement("SensorIndex");
                    xlSensorIndex2.InnerText = dl.SensorListesi[1].Index.ToString();
                    xlSensor2.AppendChild(xlSensorIndex2);
                    XmlElement xlSensorIsim2 = xml.CreateElement("SensorIsim");
                    xlSensorIsim2.InnerText = dl.SensorListesi[1].Isim;
                    xlSensor2.AppendChild(xlSensorIsim2);
                    xlSensor2.AppendChild(xml.CreateElement("_degerler"));
                    xlSensor2.AppendChild(xml.CreateElement("Konum"));
                    xlSensorListesi.AppendChild(xlSensor2);

                    element.ReplaceChild(xlSensorListesi, element.ChildNodes.Item(7));
                    xml.Save(ConstantValues.path);
                    break;

                }
            }


        }
    }
}
