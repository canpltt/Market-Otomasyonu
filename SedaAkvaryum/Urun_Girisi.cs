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
    public partial class Urun_Girisi : Form
    {
        public Urun_Girisi()
        {
            InitializeComponent();
        }
        public static string conString = "Data Source=ALPHA\\SQLEXPRESS; initial catalog=SedaAkvaryum; Integrated Security=TRUE";
        SqlConnection baglanti = new SqlConnection(conString);
        private void Urun_Girisi_Load(object sender, EventArgs e)
        {
            label7.Text = DateTime.Now.ToLongDateString();
            button2.Visible = false;
            kayitGetir();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==null) MessageBox.Show("Barkod girişi hatalı.");
            baglanti.Open();
            string kayit = "SELECT * from Urun_Listesi Where Barkod = "+textBox1.Text.ToString()+"";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["Urun_Adi"].ToString();
                textBox3.Text = read["Urun_Fiyati"].ToString();
                textBox4.Text = read["Stok"].ToString();
            }
            baglanti.Close();
            if(textBox2.Text==null) MessageBox.Show("Girilen barkod bulunmamaktadır tekrar deneyiniz.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * From Urun_Listesi where Barkod='" + textBox1.Text.ToString() + "'", baglanti);
            baglanti.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Dispose();
            if (reader.Read())
            {
                cmd = new SqlCommand("Update Urun_Listesi set Stok='" + textBox4.Text.ToString() + "', Urun_Fiyati='"+textBox3.Text.ToString()+"', Urun_Adi='"+textBox2.Text.ToString()+"' where Barkod='" + textBox1.Text.ToString() + "'", baglanti);
                reader.Close();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün Başarıyla Güncellendi!");
                cmd.Dispose();

            }
            baglanti.Close();
            kayitGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox2.Text == null || textBox3.Text == null || textBox4.Text == null)  MessageBox.Show("Ürün bilgilerini tam giriniz.");
            else
            {
                baglanti.Open();
                SqlCommand com = new SqlCommand("insert into Urun_Listesi(Barkod,Urun_Adi,Urun_Fiyati,Stok) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "')", baglanti);
                com.ExecuteNonQuery();
                MessageBox.Show("Ürün Başarıyla Eklendi!");
                baglanti.Close();
                kayitGetir();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                button1.Visible = false;
                button3.Visible = false;
                button2.Visible = true;
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
                textBox4.Text = null;
            }
            else
            {
                button1.Visible = true;
                button3.Visible = true;
                button2.Visible = false;
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
                textBox4.Text = null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null) MessageBox.Show("Barkod bilgisi hatalı veya eksik.");
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Urun_Listesi where Barkod='" + textBox1.Text.ToString() + "'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarıyla Silindi!");
                dataGridView1.ClearSelection();
                kayitGetir();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
            this.Hide();
        }
    }
}
