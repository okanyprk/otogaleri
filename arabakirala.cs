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
    public partial class arabakirala : Form
    {
        public arabakirala()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= otogaleri.mdb");
        DataTable dt = new DataTable();

        void doldur()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                dt.Clear();
                OleDbDataAdapter listele = new OleDbDataAdapter("select * from kira", baglanti);
                listele.Fill(dt);
                dataGridView1.DataSource = dt;
                listele.Dispose();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.Columns[0].Visible = false;


                baglanti.Close();

                comboBox2.Text = "";
                dateTimePicker1.Text = "";
                dateTimePicker2.Text = "";
                textBox1.Text = "";

                comboBox1.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
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
        void uygunaracdoldur()
        {
            comboBox2.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            OleDbCommand komut = new OleDbCommand("select plaka from arac where durum='" + "İkinci El"+"' AND  kiradurumu='"+"kiralanmamis"+"'", baglanti);
            OleDbDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand komut = new OleDbCommand("insert into kira(musteritc,aracplaka,alistarihi,veristarihi,ucret) values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox1.Text + "')", baglanti);
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
                    OleDbCommand guncelle = new OleDbCommand("update arac set kiradurumu ='" + "Kirada" + "'where plaka='" + comboBox2.Text + "' ", baglanti);
                    guncelle.ExecuteNonQuery();
                    uygunaracdoldur();
                }
                catch (Exception)
                {

                    throw;
                }
                MessageBox.Show("Ekleme İşleminiz Başarılı");
                baglanti.Close();

                doldur();
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }
        }

        private void arabakirala_Load(object sender, EventArgs e)
        {
            musteridoldur();
            uygunaracdoldur();
            doldur();
            timer1.Enabled = true;
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
                    OleDbCommand sil = new OleDbCommand("delete from kira where id=" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "", baglanti);
                    sil.ExecuteNonQuery();
                    uygunaracdoldur();
                    musteridoldur();
                    MessageBox.Show("Silme İşleminiz Başarılı");
                    doldur();
                    baglanti.Close();
                    try
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }
                        OleDbCommand guncelle = new OleDbCommand("update arac set kiradurumu ='" + "kiralanmamis" + "'where plaka='" + comboBox2.Text + "' ", baglanti);
                        guncelle.ExecuteNonQuery();
                        uygunaracdoldur();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
                catch
                {

                    ;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = dateTimePicker2.Value.ToShortDateString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("update kira set musteritc='" + comboBox1.Text + "',aracplaka='" + comboBox2.Text + "',alistarihi='" + dateTimePicker1.Value + "',veristarihi='" + dateTimePicker2.Value + "',ucret='" + textBox1.Text + "' where id=" + dataGridView1.CurrentRow.Cells[0].Value.ToString(), baglanti);
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

        private void button1_Click(object sender, EventArgs e)
        {
            anamenu menu = new anamenu();
            menu.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Text= DateTime.Now.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            dt.Clear();

            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From kira where musteritc like'%" + textBox4.Text + "%'or aracplaka like '%" + textBox4.Text + "%'or alistarihi like '%" + textBox4.Text + "%'or veristarihi like '%" + textBox4.Text + "%'or ucret like '%" + textBox4.Text + "%'", baglanti);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr.Dispose();
        }
    }
    }
