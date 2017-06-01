using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arayuz
{
    public partial class FrmCiktiTarihDegistir : Form
    {
        public DateTime baslangic;
        public DateTime bitiş;
        public FrmCiktiTarihDegistir(DateTime baslangic, DateTime bitiş)
        {
            InitializeComponent();
              dateTimePicker1.Value=baslangic;
             dateTimePicker2.Value=bitiş ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baslangic = dateTimePicker1.Value;
            bitiş = dateTimePicker2.Value;

        }
    }
}
