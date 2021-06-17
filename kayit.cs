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
    public partial class kayit : Form
    {
        public kayit()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= otogaleri.mdb");
        OleDbCommand komut = new OleDbCommand();
        Form1 giris = new Form1();
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            giris.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into kullanici(kadi,sifre,tel,adres,mail) values('" + kadi.Text+ "','" + sifre.Text.ToString() + "','" +tel.Text+ "','" + adres.Text.ToString() + "','" + mail.Text.ToString() + "')",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Başarıyla Kayıt Oldunuz.");
            this.Hide();
            giris.Show();
            kadi.Text = "";
            sifre.Text = "";
            tel.Text = "";
            mail.Text = "";
            adres.Text = "";
            
        }

        private void tel_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tel.Text, "[^0-9]"))
            {
                MessageBox.Show("Lütfen rakam giriniz.");
                tel.Text = tel.Text.Remove(tel.Text.Length - 1);
            }
        }

        private void kayit_Load(object sender, EventArgs e)
        {

        }
    }
}
