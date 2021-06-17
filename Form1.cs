using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace otogaleri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= otogaleri.mdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataReader oku;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void giris_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "SELECT * FROM kullanici WHERE kadi='" +kadi.Text+ "'AND sifre='" +sifre.Text+"'";
            oku = komut.ExecuteReader();
            if(oku.Read())
            {
                anamenu menu = new anamenu();
                menu.Show();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı");
                kadi.Text = "";
                sifre.Text = "";
                
            }
            baglanti.Close();
        }

        private void cikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kayit_Click(object sender, EventArgs e)
        {
            kayit kayitol = new kayit();
            kayitol.Show();
            this.Hide();
        }
    }
}
