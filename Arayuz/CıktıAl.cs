using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace Arayuz
{
    class CıktıAl
    {
        //IHTIYAÇLAR
        //döküman başlığı, şirket adı, çıkarılacak kısımlar->istatistikler-veriler-grafik-seçilidataloggerlar,
        //her datalogger için -> konum-ortalama sıcaklık- en yüksek sıcaklık-endüşük sıcaklık tablosu, ve nem için, ölçüm başlangıç-bitiş tarihleri, toplam ölçüm süresi,hedeflenen ortam koşulu, son kalibrasyon tarihi 
        //hepsinin ortak->veri kayıt sıklığı, kullanılan datalogger sayısı,genel sıcaklık-nem ortalaması, ortalamada en sıcak nokta, ortalamada en soğuk nokta, tüm ölçümlerde oluşan en yüksek anlık sıcaklık
        //tüm ölçümlerde oluşan en düşük anlık sıcaklık, datalogger verileri
        //DÜZEN -> başlık, şirket adı, datalogger sayısı, hepsi için ortak bilgiler, datalogger başlığı altında datalogger için bilgiler ve veriler
       /** Inner class to add a header and a footer. */
    class HeaderFooter : PdfPageEventHelper {
        /** Alternating phrase for the header. */
        Phrase[] header = new Phrase[2];
        /** Current page number (will be reset for every chapter). */
        int pagenumber;
 
        /**
         * Initialize one of the headers.
         * @see com.itextpdf.text.pdf.PdfPageEventHelper#onOpenDocument(
         *      com.itextpdf.text.pdf.PdfWriter, com.itextpdf.text.Document)
         */
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            header[0] = new Phrase("Jera Elektronik");
        }
 
        /**
         * Initialize one of the headers, based on the chapter title;
         * reset the page number.
         * @see com.itextpdf.text.pdf.PdfPageEventHelper#onChapter(
         *      com.itextpdf.text.pdf.PdfWriter, com.itextpdf.text.Document, float,
         *      com.itextpdf.text.Paragraph)
         */
        public override void OnChapter(PdfWriter writer, Document document,
                float paragraphPosition, Paragraph title) {
            header[1] = new Phrase(title.Content);
            pagenumber = 1;
        }
 
        /**
         * Increase the page number.
         * @see com.itextpdf.text.pdf.PdfPageEventHelper#onStartPage(
         *      com.itextpdf.text.pdf.PdfWriter, com.itextpdf.text.Document)
         */
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            pagenumber++;
        }
 
        /**
         * Adds the header and the footer.
         * @see com.itextpdf.text.pdf.PdfPageEventHelper#onEndPage(
         *      com.itextpdf.text.pdf.PdfWriter, com.itextpdf.text.Document)
         */
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            Rectangle rect = writer.GetBoxSize("art");
            int count = writer.PageNumber;
            switch((count % 2)) {
            case 0:
                ColumnText.ShowTextAligned(writer.DirectContent,
                        Element.ALIGN_RIGHT, header[0],
                        rect.Right, rect.Top, 0);
                break;
            case 1:
                ColumnText.ShowTextAligned(writer.DirectContent,
                        Element.ALIGN_LEFT, header[1],
                        rect.Left, rect.Top, 0);
                break;
            }
            ColumnText.ShowTextAligned(writer.DirectContent,
                    Element.ALIGN_CENTER, new Phrase(String.Format("sayfa %d", pagenumber)),
                    (rect.Left + rect.Right) / 2, rect.Bottom - 18, 0);
        }
    }
 
    /** The different epochs. */
    public static  String[] EPOCH =
        { "Forties", "Fifties", "Sixties", "Seventies", "Eighties",
    	  "Nineties", "Twenty-first Century" };
    /** The fonts for the title. */
    public static  Font[] FONT 
    = {
        new Font(FontFactory.GetFont("HELVETICA", 24)),//BAŞLIK için kullanılan font
         new Font(FontFactory.GetFont(".HELVETICA", 18)),
         new Font(FontFactory.GetFont(".HELVETICA", 14)),
         new Font(FontFactory.GetFont(".HELVETICA", 12, Font.BOLD)),
                  new Font(FontFactory.GetFont(".HELVETICA", 12))

    };
    Document document;
    PdfWriter writer;
    NetOlcerCiktiOptions options;
    DataLoggerListesi dataloggerlar;
    public CıktıAl( NetOlcerCiktiOptions options, DataLoggerListesi dls)
    {
        document = new Document(PageSize.A4, 36, 36, 54, 54);
        this.options = options;
        dataloggerlar = dls;
    }
    //public void CreatePdfWithAlteredData(FileStream fs, 
    public void createPdf(FileStream fs)
    {
        //dokuman başlığı
        writer = PdfWriter.GetInstance(document, fs);
        writer.SetBoxSize("art", new Rectangle(36, 54, 559, 788));
        document.Open();
        DateTime genelIlkTarih = options.Tarih();
        DateTime genelSonarih = options.Tarih("son");

        string baslik = "NETÖLÇER - " + dataloggerlar.KurumAdi;
        Paragraph dokumanBasligi = new Paragraph(baslik, FONT[0]);
        Chapter chapterDokumanBaslikveGenelBilgiler = new Chapter(1);
        Section sectionBaslik = chapterDokumanBaslikveGenelBilgiler.AddSection(dokumanBasligi, 0);
        chapterDokumanBaslikveGenelBilgiler.NumberDepth = 0;

        //dokuman için genel bilgiler ve istatistikler
        Section sectionGenelBilgi =chapterDokumanBaslikveGenelBilgiler.AddSection(new Paragraph("Genel Bilgiler",FONT[1]));
        sectionGenelBilgi.Add(new Paragraph());
        sectionGenelBilgi.NumberDepth = 0;

        //kurum adi
        var phraseKurumAdi = new Phrase();
        phraseKurumAdi.Add(new Chunk("KURUM ADI:", FONT[3]));
        phraseKurumAdi.Add(new Chunk(dataloggerlar.KurumAdi, FONT[4]));
        sectionGenelBilgi.Add(phraseKurumAdi);
        sectionGenelBilgi.Add(new Paragraph());

        //Hedeflenen Ortam Koşulları
        var phraseHedeflenenOrtamKosullari = new Phrase();
        phraseHedeflenenOrtamKosullari.Add(new Chunk("Hedeflenen Ortam Kosullari:", FONT[3]));
        phraseHedeflenenOrtamKosullari.Add(new Chunk("17(ºC) -23(ºC) , 15(ºC) -25(ºC)", FONT[4]));
        sectionGenelBilgi.Add(phraseHedeflenenOrtamKosullari);
        sectionGenelBilgi.Add(new Paragraph());

        //Ölçüm başlangıç tarihi ve saati
        var phraseOlcumBaslangicTarih = new Phrase();
        phraseOlcumBaslangicTarih.Add(new Chunk("Olcum başlangic tarihi ve saati:", FONT[3]));
        phraseOlcumBaslangicTarih.Add(new Chunk(genelIlkTarih.ToString("dd/MM/yyyy, hh:mm"), FONT[4]));
        sectionGenelBilgi.Add(phraseOlcumBaslangicTarih);
        sectionGenelBilgi.Add(new Paragraph());

        //ölçüm bitiş tarihi ve saati
        var phraseOlcumBitisTarih = new Phrase();
        phraseOlcumBitisTarih.Add(new Chunk("Olçum Bitis tarihi ve saati:", FONT[3]));
        phraseOlcumBitisTarih.Add(new Chunk(genelSonarih.ToString("dd/MM/yyyy, hh:mm"), FONT[4]));
        sectionGenelBilgi.Add(phraseOlcumBitisTarih);
        sectionGenelBilgi.Add(new Paragraph());

        //toplam ölçüm süresi
        var phraseToplamOlcumSuresi = new Phrase();
        phraseToplamOlcumSuresi.Add(new Chunk("Toplam Olcum Suresi:", FONT[3]));
        int günler = (int)((genelSonarih - genelIlkTarih).TotalDays);
        ;
        phraseToplamOlcumSuresi.Add(new Chunk(günler.ToString() + " Gün", FONT[4]));
        sectionGenelBilgi.Add(phraseToplamOlcumSuresi);
        sectionGenelBilgi.Add(new Paragraph());

        //VeriKayitSikligi
        var phraseVeriKayitSikligi = new Phrase();
        phraseVeriKayitSikligi.Add(new Chunk("Veri Kayit Sikligi:", FONT[3]));
        phraseVeriKayitSikligi.Add(new Chunk(options.VeriAraligi.ToString()+"dk", FONT[4]));
        sectionGenelBilgi.Add(phraseVeriKayitSikligi);
        sectionGenelBilgi.Add(new Paragraph());

        //KullanilanDataloggerSayisi
        var phraseKullanilanDataloggerSayisi = new Phrase();
        phraseKullanilanDataloggerSayisi.Add(new Chunk("Kullanilan Datalogger Sayisi:", FONT[3]));
        phraseKullanilanDataloggerSayisi.Add(new Chunk( options.CiktiIcinDataLoggerlar.Count.ToString(), FONT[4]));
        sectionGenelBilgi.Add(phraseKullanilanDataloggerSayisi);
        sectionGenelBilgi.Add(new Paragraph());
        //        kullanılan sensör sayısı 12
        //alarm sensör sayısı 17
        //toplam sensör sayısı 29
        var phraseKullanilanSensorSayisi = new Phrase();
        phraseKullanilanSensorSayisi.Add(new Chunk("Kullanilan Sensör Sayisi:", FONT[3]));
        phraseKullanilanSensorSayisi.Add(new Chunk("12", FONT[4]));
        sectionGenelBilgi.Add(phraseKullanilanSensorSayisi);
        sectionGenelBilgi.Add(new Paragraph());

        var phraseKullanilanAlarmSensorSayisi = new Phrase();
        phraseKullanilanAlarmSensorSayisi.Add(new Chunk("Alarm Sensör Sayisi:", FONT[3]));
        phraseKullanilanAlarmSensorSayisi.Add(new Chunk("17", FONT[4]));
        sectionGenelBilgi.Add(phraseKullanilanAlarmSensorSayisi);
        sectionGenelBilgi.Add(new Paragraph());

        var phraseKullanilanTopSensorSayisi = new Phrase();
        phraseKullanilanTopSensorSayisi.Add(new Chunk("Toplam Sensör Sayisi:", FONT[3]));
        phraseKullanilanTopSensorSayisi.Add(new Chunk("29", FONT[4]));
        sectionGenelBilgi.Add(phraseKullanilanTopSensorSayisi);
        sectionGenelBilgi.Add(new Paragraph());
        if (options.GenelIstatistiklerVarmi)
        {
            //dokuman için genel bilgiler ve istatistikler
            Section sectionIstatistikler = chapterDokumanBaslikveGenelBilgiler.AddSection(new Paragraph("Istatistikler", FONT[2]));
            sectionGenelBilgi.Add(new Paragraph(" "));
            sectionIstatistikler.NumberDepth = 0;

            sectionIstatistikler.Add(new Paragraph(" "));

            string[,] istatistikArray = new string[5, 2] { 
                                                            { "Genel Sicaklik Ortalamasi", "b" },
                                                            { "Ortalamada En Sicak Nokta:", "b" },
                                                            { "Ortalamada En Soguk Nokta:", "b" },
                                                            { "Tum Olcumlerde Olusan En Yuksek Anlik Sicaklik", "b" },
                                                            {"Tum Olcumlerde Olusan En Dusuk Anlik Sicaklik:","b"}
                                                         };
            PdfPTable IstatistikTablosu = new PdfPTable(2);
            PdfPCell cell, c1;
            istatistikArray[0, 1] = Math.Round(dataloggerlar.OrtalamaSicaklik(genelIlkTarih, genelSonarih),3).ToString();
            istatistikArray[1, 1] = dataloggerlar.EnYuksekSicaklik(genelIlkTarih, genelSonarih).Sensor;
            istatistikArray[2, 1] = dataloggerlar.EnDusukSicaklik(genelIlkTarih, genelSonarih).Sensor;
            istatistikArray[3, 1] = dataloggerlar.EnYuksekSicaklik(genelIlkTarih, genelSonarih).Deger.ToString();
            istatistikArray[4, 1] = dataloggerlar.EnDusukSicaklik(genelIlkTarih, genelSonarih).Deger.ToString();
            for (int i = 0; i < istatistikArray.GetLength(0); i++)
            {
                cell = new PdfPCell(new Phrase(istatistikArray[i, 0]));
                IstatistikTablosu.AddCell(cell);
                c1 = new PdfPCell(new Phrase(istatistikArray[i, 1]));
                IstatistikTablosu.AddCell(c1);
            }

            sectionIstatistikler.Add(IstatistikTablosu);
        }
        
 
        if (options.GenelGrafiklerVarmi)
        {

            Chapter chptrGrafik = new Chapter(2);
            Section sectionGrfk = chptrGrafik.AddSection(new Paragraph("Tum Olçumler Için Sicaklik Grafigi", FONT[2]));
            sectionGrfk.NumberDepth = 0;

           // sectionIstatistikler.Add(new Paragraph());
            using (var chartimage = new MemoryStream())
            {
                dataloggerlar.getChart(options).SaveImage(chartimage, ChartImageFormat.Jpeg);
                System.Drawing.Image img = System.Drawing.Image.FromStream(chartimage);
                img.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                Image image = Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                //image.SetAbsolutePosition(100, 100);
                sectionGrfk.Add(image);
            }
            document.Add(chapterDokumanBaslikveGenelBilgiler);
            document.Add(chptrGrafik);

        }
        else
        document.Add(chapterDokumanBaslikveGenelBilgiler);
        
        int indexDatalogger = 2;
        Chapter chapterDataloggerBilgileri;
        DataLogger dl;
        Section sectionDataloggerBaslik;
        Phrase phraseCihazAdi, phrase1Sensor, phrase2Sensor, phraseKalibrastonTarihi;
        PdfPTable DataloggerIstatistikTablosu, tableDataloggerVeri;
        PdfPCell celldl, c1dl;
        foreach (var dlicincikti in options.CiktiIcinDataLoggerlar)
        {
            DateTime ilkTarih = dlicincikti.BaslangicTarihi;
            DateTime sonTarih = dlicincikti.SonlanisTarihi;
            chapterDataloggerBilgileri = new Chapter(indexDatalogger);
            chapterDataloggerBilgileri.NumberDepth = 0;

            string dlBaslik = (indexDatalogger - 1).ToString() + ". Datalogger - " + dlicincikti.DataLoggerAdi;
            dl = dataloggerlar.DataLoggerlar.Single(x => x.Isim == dlicincikti.DataLoggerAdi);
            sectionDataloggerBaslik = chapterDataloggerBilgileri.AddSection(new Paragraph(dlBaslik,FONT[1]), 0);
            sectionDataloggerBaslik.NumberDepth = 0;


            //dokuman için genel bilgiler ve istatistikler
            sectionDataloggerBaslik.Add(new Paragraph());
            //kurum adi
            phraseCihazAdi = new Phrase();
            phraseCihazAdi.Add(new Chunk("Data Logger:", FONT[3]));
            phraseCihazAdi.Add(new Chunk(dl.Isim, FONT[4]));
            sectionDataloggerBaslik.Add(phraseCihazAdi);
            sectionDataloggerBaslik.Add(new Paragraph());

             phrase1Sensor = new Phrase();
            phrase1Sensor.Add(new Chunk("1. Sensor: ", FONT[3]));
            phrase1Sensor.Add(new Chunk(dl.SensorListesi[0].Isim, FONT[4]));
            sectionDataloggerBaslik.Add(phrase1Sensor);
            sectionDataloggerBaslik.Add(new Paragraph());

             phrase2Sensor = new Phrase();
            phrase2Sensor.Add(new Chunk("2. Sensor: ", FONT[3]));
            phrase2Sensor.Add(new Chunk(dl.SensorListesi[1].Isim, FONT[4]));
            sectionDataloggerBaslik.Add(phrase2Sensor);
            sectionDataloggerBaslik.Add(new Paragraph(" "));

            if (options.DataLoggerSonKalibrasyonTarihleriVarmi)
            {
                sectionDataloggerBaslik.Add(new Paragraph());
                //kurum adi
                phraseKalibrastonTarihi = new Phrase();
                phraseKalibrastonTarihi.Add(new Chunk("Son Kalibrasyon Tarihi:", FONT[3]));
                phraseKalibrastonTarihi.Add(new Chunk(dl.SonKalibrasyonTarihi.ToString(), FONT[4]));
                sectionDataloggerBaslik.Add(phraseKalibrastonTarihi);
                sectionDataloggerBaslik.Add(new Paragraph(" "));

            }
            if (options.DataLoggerIstatistiklerVarmi)
            {
                string[,] istatistikArray = new string[5, 2] { 
                                                            { "Sicaklik Ortalamasi", "b" },
                                                            { "Ortalamada En Sicak Nokta:", "b" },
                                                            { "Ortalamada En Soguk Nokta:", "b" },
                                                            { "Tum Olcumlerde Olusan En Yuksek Anlik Sicaklik", "b" },
                                                            {"Tüm Olcumlerde Olusan En Dusuk Anlik Sicaklik:","b"}
                                                         };
                //istatistik tablosu
                DataloggerIstatistikTablosu = new PdfPTable(2);
                istatistikArray[0, 1] = dl.OrtalamaSicaklik(ilkTarih, sonTarih).ToString();
                istatistikArray[1, 1] = dl.EnYuksekSicaklik(ilkTarih, sonTarih).Sensor;
                istatistikArray[2, 1] = dl.EnDusukSicaklik(ilkTarih, sonTarih).Sensor;
                istatistikArray[3, 1] = dl.EnYuksekSicaklik(ilkTarih, sonTarih).Deger.ToString();
                istatistikArray[4, 1] = dl.EnDusukSicaklik(ilkTarih, sonTarih).Deger.ToString();
                for (int i = 0; i < istatistikArray.GetLength(0); i++)
                {

                    celldl = new PdfPCell(new Phrase(istatistikArray[i, 0]));
                    DataloggerIstatistikTablosu.AddCell(celldl);
                    c1dl = new PdfPCell(new Phrase(istatistikArray[i, 1]));
                    DataloggerIstatistikTablosu.AddCell(c1dl);

                }

                sectionDataloggerBaslik.Add(DataloggerIstatistikTablosu);
            }
            if (options.DataLoggerGrafiklerVarmi)
            {
                Section sectiondlGrafik = chapterDataloggerBilgileri.AddSection(new Paragraph("Sicaklik Grafigi", FONT[2]));
                sectiondlGrafik.NumberDepth = 0;

                sectiondlGrafik.Add(new Paragraph(" "));
                using (var chartimage = new MemoryStream())
                {
                    dl.getChart(ilkTarih,sonTarih).SaveImage(chartimage, ChartImageFormat.Png);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(chartimage);
                    img.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                    Image image = Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                   // image.SetAbsolutePosition(100, 100);
                    sectiondlGrafik.Add(image);
                }
            }
            tableDataloggerVeri = new PdfPTable(dl.Dt.Columns.Count);
            //header of table, first row
            string[] columnNames = dl.Dt.Columns.Cast<DataColumn>()
                                     .Select(x => x.ColumnName)
                                     .ToArray();

            foreach (string column in columnNames)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column));
                tableDataloggerVeri.AddCell(cell);
            }
            DateTime aralikBasi = ilkTarih ;
            if (ilkTarih > sonTarih)
            {
                MessageBox.Show("İLK TARİH DAHA KÜÇÜK OLMALIDIR", "UYARI");
                return;
            }
            dl.Dt = null;
            dl.ParsingText(false);
            DataRow[] _newDataRown = dl.Dt.Select(("Tarih >= '" + ilkTarih.ToString() + "' AND Tarih <= '" + sonTarih.ToString() + "'"));

            foreach (DataRow row in _newDataRown)
            {
                 DateTime rowDate = row.Field<DateTime>("Tarih");
                 if (options.VeriAraligi == 60)
                 {
                     if (rowDate == aralikBasi || (rowDate - aralikBasi).Hours >= 1)
                     {
                         aralikBasi = rowDate;

                         foreach (var item in row.ItemArray)
                         {
                             Console.Write("Item:");
                             Console.WriteLine(item); // Can I add something here to also print the column names?
                             PdfPCell cell = new PdfPCell(new Phrase(item.ToString()));
                             tableDataloggerVeri.AddCell(cell);
                         }
                     }
                 }
                 else
                 {
                     if (rowDate == aralikBasi || (rowDate - aralikBasi).Minutes >= options.VeriAraligi)
                     {
                         aralikBasi = rowDate;

                         foreach (var item in row.ItemArray)
                         {
                             Console.Write("Item:");
                             Console.WriteLine(item); // Can I add something here to also print the column names?
                             PdfPCell cell = new PdfPCell(new Phrase(item.ToString()));
                             tableDataloggerVeri.AddCell(cell);
                         }
                     }
                 }
            }

            sectionDataloggerBaslik.Add(tableDataloggerVeri);
            document.Add(chapterDataloggerBilgileri);
            indexDatalogger++;
        }
        // step 5
        document.Close();
    }

    public void createPdf(FileStream fs,DateTime ilkTarih, DateTime sonTarih)
    {
        //dokuman başlığı
        writer = PdfWriter.GetInstance(document, fs);
        writer.SetBoxSize("art", new Rectangle(36, 54, 559, 788));
        document.Open();

        string baslik = "NETÖLÇER - " + dataloggerlar.KurumAdi;
        Paragraph dokumanBasligi = new Paragraph(baslik, FONT[0]);
        Chapter chapterDokumanBaslikveGenelBilgiler = new Chapter(1);
        Section sectionBaslik = chapterDokumanBaslikveGenelBilgiler.AddSection(dokumanBasligi, 0);
        chapterDokumanBaslikveGenelBilgiler.NumberDepth = 0;

        //dokuman için genel bilgiler ve istatistikler
        Section sectionGenelBilgi = chapterDokumanBaslikveGenelBilgiler.AddSection(new Paragraph("Genel Bilgiler", FONT[1]));
        sectionGenelBilgi.Add(new Paragraph());
        sectionGenelBilgi.NumberDepth = 0;

        //kurum adi
        var phraseKurumAdi = new Phrase();
        phraseKurumAdi.Add(new Chunk("KURUM ADI:", FONT[3]));
        phraseKurumAdi.Add(new Chunk(dataloggerlar.KurumAdi, FONT[4]));
        sectionGenelBilgi.Add(phraseKurumAdi);
        sectionGenelBilgi.Add(new Paragraph());

        //Hedeflenen Ortam Koşulları
        var phraseHedeflenenOrtamKosullari = new Phrase();
        phraseHedeflenenOrtamKosullari.Add(new Chunk("Hedeflenen Ortam Kosullari:", FONT[3]));
        phraseHedeflenenOrtamKosullari.Add(new Chunk("17(ºC) -23(ºC) , 15(ºC) -25(ºC)", FONT[4]));
        sectionGenelBilgi.Add(phraseHedeflenenOrtamKosullari);
        sectionGenelBilgi.Add(new Paragraph());

        //Ölçüm başlangıç tarihi ve saati
        var phraseOlcumBaslangicTarih = new Phrase();
        phraseOlcumBaslangicTarih.Add(new Chunk("Olcum başlangic tarihi ve saati:", FONT[3]));
        phraseOlcumBaslangicTarih.Add(new Chunk(ilkTarih.ToString("dd/MM/yyyy, hh:mm"), FONT[4]));
        sectionGenelBilgi.Add(phraseOlcumBaslangicTarih);
        sectionGenelBilgi.Add(new Paragraph());

        //ölçüm bitiş tarihi ve saati
        var phraseOlcumBitisTarih = new Phrase();
        phraseOlcumBitisTarih.Add(new Chunk("Olçum Bitis tarihi ve saati:", FONT[3]));
        phraseOlcumBitisTarih.Add(new Chunk(sonTarih.ToString("dd/MM/yyyy, hh:mm"), FONT[4]));
        sectionGenelBilgi.Add(phraseOlcumBitisTarih);
        sectionGenelBilgi.Add(new Paragraph());

        //toplam ölçüm süresi
        var phraseToplamOlcumSuresi = new Phrase();
        phraseToplamOlcumSuresi.Add(new Chunk("Toplam Olcum Suresi:", FONT[3]));
        int günler = (int)((sonTarih - ilkTarih).TotalDays);
        ;
        phraseToplamOlcumSuresi.Add(new Chunk(günler.ToString() + " Gün", FONT[4]));
        sectionGenelBilgi.Add(phraseToplamOlcumSuresi);
        sectionGenelBilgi.Add(new Paragraph());

        //VeriKayitSikligi
        var phraseVeriKayitSikligi = new Phrase();
        phraseVeriKayitSikligi.Add(new Chunk("Veri Kayit Sikligi:", FONT[3]));
        phraseVeriKayitSikligi.Add(new Chunk(options.VeriAraligi.ToString() + "dk", FONT[4]));
        sectionGenelBilgi.Add(phraseVeriKayitSikligi);
        sectionGenelBilgi.Add(new Paragraph());

        //KullanilanDataloggerSayisi
        var phraseKullanilanDataloggerSayisi = new Phrase();
        phraseKullanilanDataloggerSayisi.Add(new Chunk("Kullanilan Datalogger Sayisi:", FONT[3]));
        phraseKullanilanDataloggerSayisi.Add(new Chunk(options.CiktiIcinDataLoggerlar.Count.ToString(), FONT[4]));
        sectionGenelBilgi.Add(phraseKullanilanDataloggerSayisi);
        sectionGenelBilgi.Add(new Paragraph());
        //        kullanılan sensör sayısı 12
        //alarm sensör sayısı 17
        //toplam sensör sayısı 29
        var phraseKullanilanSensorSayisi = new Phrase();
        phraseKullanilanSensorSayisi.Add(new Chunk("Kullanilan Sensör Sayisi:", FONT[3]));
        phraseKullanilanSensorSayisi.Add(new Chunk("12", FONT[4]));
        sectionGenelBilgi.Add(phraseKullanilanSensorSayisi);
        sectionGenelBilgi.Add(new Paragraph());

        var phraseKullanilanAlarmSensorSayisi = new Phrase();
        phraseKullanilanAlarmSensorSayisi.Add(new Chunk("Alarm Sensör Sayisi:", FONT[3]));
        phraseKullanilanAlarmSensorSayisi.Add(new Chunk("17", FONT[4]));
        sectionGenelBilgi.Add(phraseKullanilanAlarmSensorSayisi);
        sectionGenelBilgi.Add(new Paragraph());

        var phraseKullanilanTopSensorSayisi = new Phrase();
        phraseKullanilanTopSensorSayisi.Add(new Chunk("Toplam Sensör Sayisi:", FONT[3]));
        phraseKullanilanTopSensorSayisi.Add(new Chunk("29", FONT[4]));
        sectionGenelBilgi.Add(phraseKullanilanTopSensorSayisi);
        sectionGenelBilgi.Add(new Paragraph());
        if (options.GenelIstatistiklerVarmi)
        {
            //dokuman için genel bilgiler ve istatistikler
            Section sectionIstatistikler = chapterDokumanBaslikveGenelBilgiler.AddSection(new Paragraph("Istatistikler", FONT[2]));
            sectionGenelBilgi.Add(new Paragraph(" "));
            sectionIstatistikler.NumberDepth = 0;

            sectionIstatistikler.Add(new Paragraph(" "));

            string[,] istatistikArray = new string[5, 2] { 
                                                            { "Genel Sicaklik Ortalamasi", "b" },
                                                            { "Ortalamada En Sicak Nokta:", "b" },
                                                            { "Ortalamada En Soguk Nokta:", "b" },
                                                            { "Tum Olcumlerde Olusan En Yuksek Anlik Sicaklik", "b" },
                                                            {"Tum Olcumlerde Olusan En Dusuk Anlik Sicaklik:","b"}
                                                         };
            PdfPTable IstatistikTablosu = new PdfPTable(2);
            PdfPCell cell, c1;
            istatistikArray[0, 1] = Math.Round(dataloggerlar.OrtalamaSicaklik(ilkTarih, sonTarih), 3).ToString();
            istatistikArray[1, 1] = dataloggerlar.EnYuksekSicaklik(ilkTarih, sonTarih).Sensor;
            istatistikArray[2, 1] = dataloggerlar.EnDusukSicaklik(ilkTarih, sonTarih).Sensor;
            istatistikArray[3, 1] = dataloggerlar.EnYuksekSicaklik(ilkTarih, sonTarih).Deger.ToString();
            istatistikArray[4, 1] = dataloggerlar.EnDusukSicaklik(ilkTarih, sonTarih).Deger.ToString();
            for (int i = 0; i < istatistikArray.GetLength(0); i++)
            {
                cell = new PdfPCell(new Phrase(istatistikArray[i, 0]));
                IstatistikTablosu.AddCell(cell);
                c1 = new PdfPCell(new Phrase(istatistikArray[i, 1]));
                IstatistikTablosu.AddCell(c1);
            }

            sectionIstatistikler.Add(IstatistikTablosu);
        }


        if (options.GenelGrafiklerVarmi)
        {

            Chapter chptrGrafik = new Chapter(2);
            Section sectionGrfk = chptrGrafik.AddSection(new Paragraph("Tum Olçumler Için Sicaklik Grafigi", FONT[2]));
            sectionGrfk.NumberDepth = 0;

            // sectionIstatistikler.Add(new Paragraph());
            using (var chartimage = new MemoryStream())
            {
                dataloggerlar.getChart(options).SaveImage(chartimage, ChartImageFormat.Jpeg);
                System.Drawing.Image img = System.Drawing.Image.FromStream(chartimage);
                img.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                Image image = Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                //image.SetAbsolutePosition(100, 100);
                sectionGrfk.Add(image);
            }
            document.Add(chapterDokumanBaslikveGenelBilgiler);
            document.Add(chptrGrafik);

        }
        else
            document.Add(chapterDokumanBaslikveGenelBilgiler);

        int indexDatalogger = 2;
        Chapter chapterDataloggerBilgileri;
        DataLogger dl;
        Section sectionDataloggerBaslik;
        Phrase phraseCihazAdi, phrase1Sensor, phrase2Sensor, phraseKalibrastonTarihi;
        PdfPTable DataloggerIstatistikTablosu, tableDataloggerVeri;
        PdfPCell celldl, c1dl;
        foreach (var dlicincikti in options.CiktiIcinDataLoggerlar)
        {
            
            chapterDataloggerBilgileri = new Chapter(indexDatalogger);
            chapterDataloggerBilgileri.NumberDepth = 0;

            string dlBaslik = (indexDatalogger - 1).ToString() + ". Datalogger - " + dlicincikti.DataLoggerAdi;
            dl = dataloggerlar.DataLoggerlar.Single(x => x.Isim == dlicincikti.DataLoggerAdi);
            sectionDataloggerBaslik = chapterDataloggerBilgileri.AddSection(new Paragraph(dlBaslik, FONT[1]), 0);
            sectionDataloggerBaslik.NumberDepth = 0;


            //dokuman için genel bilgiler ve istatistikler
            sectionDataloggerBaslik.Add(new Paragraph());
            //kurum adi
            phraseCihazAdi = new Phrase();
            phraseCihazAdi.Add(new Chunk("Data Logger:", FONT[3]));
            phraseCihazAdi.Add(new Chunk(dl.Isim, FONT[4]));
            sectionDataloggerBaslik.Add(phraseCihazAdi);
            sectionDataloggerBaslik.Add(new Paragraph());

            phrase1Sensor = new Phrase();
            phrase1Sensor.Add(new Chunk("1. Sensor: ", FONT[3]));
            phrase1Sensor.Add(new Chunk(dl.SensorListesi[0].Isim, FONT[4]));
            sectionDataloggerBaslik.Add(phrase1Sensor);
            sectionDataloggerBaslik.Add(new Paragraph());

            phrase2Sensor = new Phrase();
            phrase2Sensor.Add(new Chunk("2. Sensor: ", FONT[3]));
            phrase2Sensor.Add(new Chunk(dl.SensorListesi[1].Isim, FONT[4]));
            sectionDataloggerBaslik.Add(phrase2Sensor);
            sectionDataloggerBaslik.Add(new Paragraph(" "));

            if (options.DataLoggerSonKalibrasyonTarihleriVarmi)
            {
                sectionDataloggerBaslik.Add(new Paragraph());
                //kurum adi
                phraseKalibrastonTarihi = new Phrase();
                phraseKalibrastonTarihi.Add(new Chunk("Son Kalibrasyon Tarihi:", FONT[3]));
                phraseKalibrastonTarihi.Add(new Chunk(dl.SonKalibrasyonTarihi.ToString(), FONT[4]));
                sectionDataloggerBaslik.Add(phraseKalibrastonTarihi);
                sectionDataloggerBaslik.Add(new Paragraph(" "));

            }
            if (options.DataLoggerIstatistiklerVarmi)
            {
                string[,] istatistikArray = new string[5, 2] { 
                                                            { "Sicaklik Ortalamasi", "b" },
                                                            { "Ortalamada En Sicak Nokta:", "b" },
                                                            { "Ortalamada En Soguk Nokta:", "b" },
                                                            { "Tum Olcumlerde Olusan En Yuksek Anlik Sicaklik", "b" },
                                                            {"Tüm Olcumlerde Olusan En Dusuk Anlik Sicaklik:","b"}
                                                         };
                //istatistik tablosu
                DataloggerIstatistikTablosu = new PdfPTable(2);
                istatistikArray[0, 1] = dl.OrtalamaSicaklik(ilkTarih, sonTarih).ToString();
                istatistikArray[1, 1] = dl.EnYuksekSicaklik(ilkTarih, sonTarih).Sensor;
                istatistikArray[2, 1] = dl.EnDusukSicaklik(ilkTarih, sonTarih).Sensor;
                istatistikArray[3, 1] = dl.EnYuksekSicaklik(ilkTarih, sonTarih).Deger.ToString();
                istatistikArray[4, 1] = dl.EnDusukSicaklik(ilkTarih, sonTarih).Deger.ToString();
                for (int i = 0; i < istatistikArray.GetLength(0); i++)
                {

                    celldl = new PdfPCell(new Phrase(istatistikArray[i, 0]));
                    DataloggerIstatistikTablosu.AddCell(celldl);
                    c1dl = new PdfPCell(new Phrase(istatistikArray[i, 1]));
                    DataloggerIstatistikTablosu.AddCell(c1dl);

                }

                sectionDataloggerBaslik.Add(DataloggerIstatistikTablosu);
            }
            if (options.DataLoggerGrafiklerVarmi)
            {
                Section sectiondlGrafik = chapterDataloggerBilgileri.AddSection(new Paragraph("Sicaklik Grafigi", FONT[2]));
                sectiondlGrafik.NumberDepth = 0;

                sectiondlGrafik.Add(new Paragraph(" "));
                using (var chartimage = new MemoryStream())
                {
                    dl.getChart(ilkTarih, sonTarih).SaveImage(chartimage, ChartImageFormat.Png);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(chartimage);
                    img.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone);
                    Image image = Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    // image.SetAbsolutePosition(100, 100);
                    sectiondlGrafik.Add(image);
                }
            }
            tableDataloggerVeri = new PdfPTable(dl.Dt.Columns.Count);
            //header of table, first row
            string[] columnNames = dl.Dt.Columns.Cast<DataColumn>()
                                     .Select(x => x.ColumnName)
                                     .ToArray();

            foreach (string column in columnNames)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column));
                tableDataloggerVeri.AddCell(cell);
            }
            DateTime aralikBasi = ilkTarih;
            if (ilkTarih > sonTarih)
            {
                MessageBox.Show("İLK TARİH DAHA KÜÇÜK OLMALIDIR", "UYARI");
                return;
            }
            dl.Dt = null;
            dl.ParsingText(false);
            DataRow[] _newDataRown = dl.Dt.Select(("Tarih >= '" + ilkTarih.ToString() + "' AND Tarih <= '" + sonTarih.ToString() + "'"));

            foreach (DataRow row in _newDataRown)
            {
                DateTime rowDate = row.Field<DateTime>("Tarih");
                if (options.VeriAraligi == 60)
                {
                    if (rowDate == aralikBasi || (rowDate - aralikBasi).Hours >= 1)
                    {
                        aralikBasi = rowDate;

                        foreach (var item in row.ItemArray)
                        {
                            Console.Write("Item:");
                            Console.WriteLine(item); // Can I add something here to also print the column names?
                            PdfPCell cell = new PdfPCell(new Phrase(item.ToString()));
                            tableDataloggerVeri.AddCell(cell);
                        }
                    }
                }
                else
                {
                    if (rowDate == aralikBasi || (rowDate - aralikBasi).Minutes >= options.VeriAraligi)
                    {
                        aralikBasi = rowDate;

                        foreach (var item in row.ItemArray)
                        {
                            Console.Write("Item:");
                            Console.WriteLine(item); // Can I add something here to also print the column names?
                            PdfPCell cell = new PdfPCell(new Phrase(item.ToString()));
                            tableDataloggerVeri.AddCell(cell);
                        }
                    }
                }
            }

            sectionDataloggerBaslik.Add(tableDataloggerVeri);
            document.Add(chapterDataloggerBilgileri);
            indexDatalogger++;
        }
        // step 5
        document.Close();
    }
      
        public static void createPdf(FileStream fs,string baslik, DateTime[] tarihAraligi, double[] ortalamalar, DataTable sicDegerler)
        {
            Document document = new Document(PageSize.A4, 36, 36, 54, 54);

            PdfWriter writer;
         int a;
        writer = PdfWriter.GetInstance(document, fs);
        Paragraph titleA = null;
        //HeaderFooter eventi = new HeaderFooter();
        writer.SetBoxSize("art", new Rectangle(36, 54, 559, 788));
        document.Open();
        Section section = null;

        int epoch = -1;
        int currentYear = 0;
        Paragraph title = null;
        titleA = new Paragraph(baslik, FONT[0]);
        Chapter chapter1 = new Chapter(1);
        section = chapter1.AddSection(titleA,0);
        chapter1.NumberDepth = 0;
        
        //document.Add(chA);
        //chapter = new Chapter(title, epoch + 1);
        //Chapter chapter = null;
        //iç doldurma
        //2 - tarih araligi
        title = new Paragraph("TARIH ARALIGI ",FONT[1] );
        //chapter = new Chapter(1);
        section = chapter1.AddSection(title,0);
        section.NumberDepth = 0;
        section.Add(new Paragraph( tarihAraligi[0].ToString() + " - "+tarihAraligi[1].ToString() , FONT[2]));
        //document.Add(section);

        //3 - sicaklik chapter
        Chapter chapter2 = new Chapter(2);

         PdfPCell cell;
           title = new Paragraph("NEM VE SICAKLIK" , FONT[1]);
         //chapter = new Chapter(1);
           section = chapter2.AddSection(title, 0);
        section.NumberDepth = 0;
        PdfPTable table = new PdfPTable(sicDegerler.Columns.Count);
           //header of table, first row
        string[] columnNames = sicDegerler.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray(); 
         foreach(string column in columnNames){

          cell = new PdfPCell(new Phrase( column));
           table.AddCell(cell);

        }
         foreach (DataRow row in sicDegerler.Rows)
        {

            
            foreach (var item in row.ItemArray)
            {
                Console.Write("Item:");
                Console.WriteLine(item); // Can I add something here to also print the column names?
                cell = new PdfPCell(new Phrase(item.ToString()));
                 table.AddCell(cell);
            }
             
         }

         section.Add(table);
         document.Add(chapter1);
         document.Add(chapter2);
        // step 5
        document.Close();
       
    }
    }
    public class NetOlcerCiktiOptions 
    {
        public bool GenelIstatistiklerVarmi { get; set; }
        public bool DataLoggerIstatistiklerVarmi { get; set; }
        public bool DataLoggerSonKalibrasyonTarihleriVarmi { get; set; }
        public bool GenelGrafiklerVarmi { get; set; }
        public bool DataLoggerGrafiklerVarmi { get; set; }
        public double VeriAraligi { get; set; }
        public List<CiktiDataLoggerItem> CiktiIcinDataLoggerlar = new List<CiktiDataLoggerItem>();
        public DateTime Tarih(string tip = "ilk")
        {
            DateTime tarih = DateTime.MaxValue;
            if (tip == "ilk")
            {
                foreach (var cdli in CiktiIcinDataLoggerlar)
                {
                    tarih = tarih < cdli.BaslangicTarihi ? tarih : cdli.BaslangicTarihi;
                }
            }
            else
            {
                tarih = DateTime.MinValue;
                foreach (var cdli in CiktiIcinDataLoggerlar)
                {
                    tarih = tarih > cdli.SonlanisTarihi ? tarih : cdli.SonlanisTarihi;
                }
            }
            return tarih;
        }
    }
}
