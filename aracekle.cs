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
    public partial class aracekle : Form
    {
        public aracekle()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= otogaleri.mdb");
        DataTable dt = new DataTable();
        string yeniyol = "";
        void doldur()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                dt.Clear();
                OleDbDataAdapter listele = new OleDbDataAdapter("select * from arac", baglanti);
                listele.Fill(dt);
                dataGridView1.DataSource = dt;
                listele.Dispose();
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[3].Width = 77;
                dataGridView1.Columns[4].Width = 76;
                dataGridView1.Columns[5].Width = 76;
                dataGridView1.Columns[6].Width = 80;
                dataGridView1.Columns[7].Width = 80;
                baglanti.Close();
                marka.Text = "";
                model.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                comboBox1.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }
        private void aracekle_Load(object sender, EventArgs e)
        {
            doldur();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand komut = new OleDbCommand ("insert into arac(marka,model,yil,renk,kilometre,resim,plaka,fiyat,durum) values('" + marka.Text + "','" + model.Text + "','" + textBox3.Text + "','"+ textBox4.Text + "','" + textBox5.Text + "','" + Path.GetFileName(yeniyol) + "','" + textBox6.Text + "','" + textBox7.Text + "','" +comboBox1.Text+"')", baglanti);

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                komut.ExecuteNonQuery();
                MessageBox.Show("Ekleme İşleminiz Başarılı");
                pictureBox1.ImageLocation = "";
                baglanti.Close();
                doldur();
            }
            catch (Exception hata)
            {
                
                MessageBox.Show(hata.Message);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        { 
            DosyaAc.Filter = "Resim Dosyası |*.jpg;*.nef;*.png | Tüm Dosyalar |*.*";
            DosyaAc.ShowDialog();
            string dosyayolu = DosyaAc.FileName;
        yeniyol = Application.StartupPath + "\\images\\" + Guid.NewGuid().ToString() + ".jpg";
            File.Copy(dosyayolu, yeniyol);
            pictureBox1.ImageLocation = dosyayolu;
        

        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            marka.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            model.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            pictureBox1.ImageLocation = Application.StartupPath + "\\images\\" + dataGridView1.CurrentRow.Cells[6].Value.ToString();
            
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
                    OleDbCommand sil = new OleDbCommand("delete from arac where aracid=" + textBox1.Text+ "", baglanti);
                    sil.ExecuteNonQuery();
                    MessageBox.Show("Silme İşleminiz Başarılı");
                    doldur();
                    pictureBox1.Image = null;
                    baglanti.Close();

                }
                catch
                {

                    ;
                }
            }
        }

        private void guncelle_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("update arac set marka='" + marka.Text + "',model='" + model.Text + "',yil='" + textBox3.Text + "',kilometre='" + textBox5.Text + "',renk='" + textBox4.Text + "',plaka='" + textBox6.Text + "',resim='" + Path.GetFileName(yeniyol) +"',durum='"+comboBox1.Text + "' where aracid=" +textBox1.Text+ "", baglanti);
            //
            // 
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme İşlemi Başarılı");
            doldur();
            pictureBox1.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            anamenu anaamenu = new anamenu();
            anaamenu.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dt.Clear();

            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From arac where model like'%" + textBox2.Text +"%'", baglanti);
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;
            adtr.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            marka.Text = "";
            model.Text = "";
            comboBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            pictureBox1.Image = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
