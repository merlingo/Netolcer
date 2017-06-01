using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Baglanti;
namespace BaglantiTest
{
    [TestClass]
    public class ArayuzDoldurmaTests
    {
        static string txt = "JERA MUHENDISLIK - DATALOGGER UYGULAMASI\r\n\r\n   SAAT         GUN      ISI1     NemSensor1     ISI2     NemSensor2\r\n 01:55:05 PM  22/10/14   24.7 C   51.1   24.3 C   42.8\r\n \r\n 01:55:10 PM  22/10/14   21.7 C   11.1   14.3 C   22.7\r\n\r\n 02:55:10 PM  22/10/14   21.7 C   11.1   14.3 C   22.7 \r\n\r\n 01:55:10 PM  23/10/14   21.7 C   11.1   14.3 C   22.7\r\n \r\n 01:55:10 PM  22/11/14   21.7 C   11.1   14.3 C   22.7\r\n \r\n 01:55:10 PM  22/10/15   21.7 C   11.1   14.3 C   22.7\r\n \r\n 01:55:10 PM  22/10/15   21.7 C   11.1   14.3 C   22.7\r\n \r\n 01:55:10 PM  22/10/15   21.7 C   11.1   14.3 C   22.7\r\n \r\n 01:55:10 PM  22/10/15   21.7 C   11.1   14.3 C   22.7\r\n ";
        [TestMethod]
        public void ortalamaNem_Test()
        {
            double expected = (51.1 + 11.1 + 11.1 + 11.1 + 11.1 + 11.1 + 11.1 + 11.1 + 11.1) / 9; 
            ArayuzDoldurma ad = new ArayuzDoldurma(new ParsingText(txt));
            Assert.AreEqual(expected,ad.ortalamaNem(1),0.1);
        }
         [TestMethod]
         public void ortalamaSicaklik_Test()
        {
            double expected = (24.7 + 21.7 + 21.7 + 21.7 + 21.7 + 21.7 + 21.7 + 21.7 + 21.7) / 9; 
            ArayuzDoldurma ad = new ArayuzDoldurma(new ParsingText(txt));
            Assert.AreEqual(expected,ad.ortalamaSicaklik(1), 0.1);
        }
         //[TestMethod]
         //public void ortalamaSicaklikDateTime_Test()
         //{
         //    double expected = (24.7 + 21.7+21.7+21.7) / 4;
         //    ArayuzDoldurma ad = new ArayuzDoldurma(new ParsingText(txt));
         //    Assert.AreEqual(expected, ad.ortalamaSicaklik(1, new DateTime(2014, 10, 22, 01, 54, 10), new DateTime(2014, 10, 23, 02, 56, 10)), 0.2);
         //}
         [TestMethod]
         public void ortalamaNemDateTime_Test()
         {
             //double expected = (51.1 + 11.1 + 11.1 ) / 3;
             //ArayuzDoldurma ad = new ArayuzDoldurma(new ParsingText(txt));
             //Assert.AreEqual(expected, ArayuzDoldurma.ortalamaNem(1, new DateTime(2014, 10, 22, 01, 54, 10), new DateTime(2014, 10, 23, 02, 56, 10)), 0.1);
         }
    }
}
