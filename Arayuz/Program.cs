using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arayuz
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                string path = Application.StartupPath + "\\DataLoggerListesi.xml";
                if (File.Exists(path))//eğer xml dosyası varsa anaekran acılır yoksa kurulum ekranı açılır
                    Application.Run(new FrmAnaEkran());
                else
                    Application.Run(new FrmKurulumEkrani());
            }
            else
            {
                string dlName = args[0];
                MessageBox.Show(dlName);

                string path = Application.StartupPath + "\\DataLoggerListesi.xml";
                if (!File.Exists(path))//eğer xml dosyası varsa anaekran acılır yoksa kurulum ekranı açılır
                    MessageBox.Show("Lütfen uygulama kurulumunu yapınız.");
                else
                    Application.Run(new FrmAnaEkran(dlName));
            }
        }
     
    }
}
