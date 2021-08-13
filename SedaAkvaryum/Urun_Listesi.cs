using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SedaAkvaryum
{
    public partial class Urun_Listesi : Form
    {
        public Urun_Listesi()
        {
            InitializeComponent();
        }
        public static string conString = "Data Source=ALPHA\\SQLEXPRESS; initial catalog=SedaAkvaryum; Integrated Security=TRUE";
        SqlConnection baglanti = new SqlConnection(conString);

        private void Urun_Listesi_Load(object sender, EventArgs e)
        {
            kayitGetir();
            label5.Text = DateTime.Now.ToLongDateString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string barkod = textBox1.Text;
            kayitGetirBarkod(barkod);
        }

        public void kayitGetirBarkod(string barkod)
        {
            baglanti.Open();
            string kayit = "SELECT Barkod,Urun_Adi,Urun_Fiyati,Stok from Urun_Listesi Where Barkod LIKE '%"+barkod+"%'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        public void kayitGetirAd(string ad)
        {
            baglanti.Open();
            string kayit = "SELECT Barkod,Urun_Adi,Urun_Fiyati,Stok from Urun_Listesi Where Urun_Adi LIKE '%" + ad + "%'";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ad = textBox2.Text;
            kayitGetirAd(ad);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kayitGetir();
            textBox1.Text = null;
            textBox2.Text = null;
        }
        public void kayitGetir()
        {
            baglanti.Open();
            string kayit = "SELECT Barkod,Urun_Adi,Urun_Fiyati,Stok from Urun_Listesi";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
            this.Hide();
        }
    }
}
