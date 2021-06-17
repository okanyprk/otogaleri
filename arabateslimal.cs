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
using System.IO;

namespace otogaleri
{
    public partial class arabateslimal : Form
    {
        public arabateslimal()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= otogaleri.mdb");
        DataTable dt = new DataTable();

        private void arabateslimal_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            uygunaracdoldur();
        }
        void uygunaracdoldur()
        {
            comboBox1.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand komut = new OleDbCommand("select plaka from arac where kiradurumu='" + "Kirada" + "'", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            OleDbCommand guncelle = new OleDbCommand("update arac set kiradurumu ='" + "kiralanmamis" + "'where plaka='" + comboBox1.Text + "' ", baglanti);
            guncelle.ExecuteNonQuery();
            uygunaracdoldur();
            baglanti.Close();
            MessageBox.Show("Araç Kiralamaya Uygun Hale Getirilmiştir.");
            comboBox1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            anamenu menu = new anamenu();
            menu.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text=DateTime.Now.ToString();
        }
    }
}
