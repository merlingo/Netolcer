using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Arayuz
{
    public struct NetOlcerBirimi
    {
       public double Deger { get; set; }
       public string Tip { get; set; }
       public string Birim { get; set; }
       public DateTime Zaman { get; set; }
       public string Sensor { get; set; }
       public int SensorIndex { get; set; }

       public override string ToString()
       {
           return "Tip:" + Tip + " Deger" + Deger + " " + Birim + " Zaman:" + Zaman+" Sensor:"+Sensor;
       }    
    }
}
