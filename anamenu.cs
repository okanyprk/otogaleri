using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otogaleri
{
    public partial class anamenu : Form
    {
        public anamenu()
        {
            InitializeComponent();
        }

        private void cikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void anamenu_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Now.ToString();
        }

        private void ilanekl_Click(object sender, EventArgs e)
        {
            Form2 müsteri = new Form2();
            müsteri.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            aracekle aracekle = new aracekle();
            aracekle.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            arabakirala kirala = new arabakirala();
            kirala.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            arabasat satis = new arabasat();
            satis.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            arabateslimal teslimal = new arabateslimal();
            teslimal.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
