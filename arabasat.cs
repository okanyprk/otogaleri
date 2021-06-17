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
    public partial class arabasat : Form
    {
        public arabasat()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= otogaleri.mdb");
        DataTable dt = new DataTable();

        void uygunaracdoldur()
        {
            comboBox2.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand komut = new OleDbCommand("select plaka from arac where kiradurumu='" + "kiralanmamis" + "' AND  satisdurumu='" + "satilmamis" + "'", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku["plaka"].ToString());
            }
            baglanti.Close();
        }
        void musteridoldur()
        {
            comboBox1.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand komut = new OleDbCommand("select tckimlik from musteri", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["tckimlik"].ToString());
            }
            baglanti.Close();
        }
        void doldur()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                dt.Clear();
                OleDbDataAdapter listele = new OleDbDataAdapter("select * from satis", baglanti);
                listele.Fill(dt);
                dataGridView1.DataSource = dt;
                listele.Dispose();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.RowHeadersVisible = false;

                baglanti.Close();

                comboBox2.Text = "";
                textBox2.Text = "";
                comboBox1.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        void ucretbul()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                OleDbCommand komut = new OleDbCommand("select fiyat from arac where plaka='" + comboBox2.Text + "'", baglanti);
                OleDbDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    textBox1.Text=(oku["fiyat"].ToString());
                }
                baglanti.Close();
            }
            catch (Exception)
            {

                ;
            }
        }

        private void arabasat_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            musteridoldur();
            uygunaracdoldur();
            doldur();

        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            anamenu menu = new anamenu();
            menu.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label8.Text= DateTime.Now.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand komut = new OleDbCommand("insert into satis(tcno,plaka,satistarihi,ucret) values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "','" + textBox1.Text+ "')", baglanti);
                //
                // 
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                komut.ExecuteNonQuery();

                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }


                    OleDbCommand sil = new OleDbCommand("delete from arac where plaka='" + comboBox2.Text + "'", baglanti);
                    sil.ExecuteNonQuery();
                    uygunaracdoldur();
                    doldur();
                }
                catch
                {

                    ;
                }
                MessageBox.Show("Satış İşlemi İşleminiz Başarılı");
                baglanti.Close();

                doldur();
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            ucretbul();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes && dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim() != "")
            {
                try
                {

                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    OleDbCommand sil = new OleDbCommand("delete from satis where id=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "", baglanti);
                    sil.ExecuteNonQuery();
                    uygunaracdoldur();
                    musteridoldur();
                    MessageBox.Show("Silme İşleminiz Başarılı");
                    doldur();
                    baglanti.Close();

                }
                catch
                {

                    ;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("update satis set tcno='" + comboBox1.Text + "',plaka='" + comboBox2.Text + "',satistarihi='" + textBox2.Text + "',ucret='" + textBox1.Text + "' where id=" + dataGridView1.CurrentRow.Cells[0].Value.ToString(), baglanti);
            // 
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi Başarılı");
            doldur();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = textBox2.Text;
            textBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            dt.Clear();

            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From arac_satis where tcno like'%" + textBox4.Text + "%'or plaka like '%" + textBox4.Text + "%'or satistarihi like '%" + textBox4.Text +  "%'or ucret like '%" + textBox4.Text + "%'", baglanti);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr.Dispose();
            baglanti.Close();
        }
    }
}
