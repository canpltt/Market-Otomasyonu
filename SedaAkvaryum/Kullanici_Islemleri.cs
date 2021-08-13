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
    public partial class Kullanici_Islemleri : Form
    {
        public Kullanici_Islemleri()
        {
            InitializeComponent();
        }
        public static string conString = "Data Source=ALPHA\\SQLEXPRESS; initial catalog=SedaAkvaryum; Integrated Security=TRUE";
        SqlConnection baglanti = new SqlConnection(conString);

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Kullanici_Islemleri_Load(object sender, EventArgs e)
        {
            kayitGetir();
            label4.Text = DateTime.Now.ToLongDateString();
        }
        public void kayitGetir()
        {
            baglanti.Open();
            string kayit = "SELECT Kullanici_Adi AS 'Kullanıcı Adı', Sifre AS 'Şifre', Telefon_No AS 'Telefon Numarası'  from Login";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null || textBox2.Text == null || textBox3.Text == null) MessageBox.Show("Kullanıcı bilgilerini tam giriniz.");
            else
            {
                baglanti.Open();
                SqlCommand com = new SqlCommand("insert into Login(Kullanici_Adi, Sifre, Telefon_No) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "')", baglanti);
                com.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı Başarıyla Eklendi!");
                baglanti.Close();
                kayitGetir();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null) MessageBox.Show("Kullanıcı adı bilgisi hatalı veya eksik.");
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Login where Kullanici_Adi='" + textBox1.Text.ToString() + "'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarıyla Silindi!");
                dataGridView1.ClearSelection();
                kayitGetir();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * From Login where Kullanici_Adi='" + textBox1.Text.ToString() + "'", baglanti);
            baglanti.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Dispose();
            if (reader.Read())
            {
                cmd = new SqlCommand("Update Login set Telefon_No='" + textBox3.Text.ToString() + "', Sifre='" + textBox2.Text.ToString() + "' where Kullanici_Adi='" + textBox1.Text.ToString() + "'", baglanti);
                reader.Close();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarıyla Güncellendi!");
                cmd.Dispose();

            }
            baglanti.Close();
            kayitGetir();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                button1.Visible = false;
                button4.Visible = false;
                button2.Visible = true;
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
            }
            else
            {
                button1.Visible = true;
                button4.Visible = true;
                button2.Visible = false;
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
            this.Hide();
        }
    }
}
