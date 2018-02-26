using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arayuz
{
   [XmlType("Sensor")]
   public  class Sensor
    {
       [System.Xml.Serialization.XmlIgnore]
       public List<NetOlcerBirimi> _degerler = new List<NetOlcerBirimi>();
        //[XmlArray("Degerler")]
        //public List<NetOlcerBirimi> Degerler
        //{
        //    get { return _degerler; }
        //    set { _degerler = value; }
        //}
        private int _index = 1;
        [XmlElement("SensorIndex")]
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
        private string _isim="";
       [XmlElement("SensorIsim")]
        public string Isim
        {
            get { return _isim; }
            set { _isim = value; }
        }
       [XmlElement("Konum")]
        string _konum="";
        public string Konum
        {
            get { return _konum; }
            set { _konum = value; }
        }
       
        public void DegerleriDoldur(List<NetOlcerBirimi> degerler)
        {
            _degerler = degerler;
        }
        public void DegerleEkle(NetOlcerBirimi deger)
        {
            _degerler.Add(deger);
        }
        public NetOlcerBirimi DegerGoster(int i)
        {
            return _degerler[i];
        }
        public NetOlcerBirimi DegerGoster(DateTime time)
        {
            return _degerler.Find(x => x.Zaman == time) ;
        }
       // DataLogger _bagliOlduguDataLogger=new DataLogger();
       //[System.Xml.Serialization.XmlIgnore]
       // public DataLogger BagliOlduguDataLogger
       // {
       //     get { return _bagliOlduguDataLogger; }
       //     set { _bagliOlduguDataLogger = value; }
       // }
    }
}
