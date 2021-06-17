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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= otogaleri.mdb");
        OleDbCommand komut = new OleDbCommand();
        DataTable musteri = new DataTable();
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
                OleDbDataAdapter listele = new OleDbDataAdapter("select * from musteri ", baglanti);
                listele.Fill(dt);
                dataGridView1.DataSource = dt;
                listele.Dispose();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.RowHeadersVisible = false;
                baglanti.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                comboBox1.Text = "";
                dateTimePicker1.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }


        }
        private void Form2_Load(object sender, EventArgs e)
        {
            doldur();
        }

        private void kaydet_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Length!=11)
            {
                MessageBox.Show("Telefon numarası 11 haneli olmalıdır. Girişiniz Yanlıştır.");

            }
            
            if (textBox1.Text.Length != 11 )
            {
                MessageBox.Show("TC numarası 11 haneli olmalıdır. Girişiniz Yanlıştır.");
            }
            else
            {
                try
                {
                    OleDbCommand komut = new OleDbCommand("insert into musteri(tckimlik,ad,soyad,cinsiyet,dtarih,dyeri,tel,mail,adres) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + comboBox1.Text + "','" + textBox10.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "')", baglanti);
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Ekleme İşleminiz Başarılı");
                    baglanti.Close();

                    doldur();
                }
                catch (Exception hata)
                {

                    MessageBox.Show(hata.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dateTimePicker1.Text= dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void gnclle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand adtr = new OleDbCommand("update musteri set tckimlik='" + textBox1.Text + "',ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',cinsiyet='" + comboBox1.Text +"',dtarih='"+dateTimePicker1.Value + "',dyeri='" + textBox4.Text + "',tel='" + textBox5.Text + "',mail='" + textBox6.Text + "',adres='" + textBox7.Text + "'where id=" + textBox8.Text , baglanti);
            adtr.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi Başarılı");
            doldur();
        }

        private void sil_Click(object sender, EventArgs e)
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
                    OleDbCommand sil = new OleDbCommand("delete from musteri where id=" + textBox8.Text + "", baglanti);
                    sil.ExecuteNonQuery();
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

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            dt.Clear();

            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From musteri where ad like'%" + textBox9.Text + "%'", baglanti);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.ResetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            anamenu menu = new anamenu();
            menu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox10.Text = dateTimePicker1.Value.ToShortDateString();
        }
    }
}

