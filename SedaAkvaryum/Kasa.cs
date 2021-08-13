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
    public partial class Kasa : Form
    {
        public Kasa()
        {
            InitializeComponent();
        }
        public static string conString = "Data Source=ALPHA\\SQLEXPRESS; initial catalog=SedaAkvaryum; Integrated Security=TRUE";
        SqlConnection baglanti = new SqlConnection(conString);
        int sayac = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Kasa_Load(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongDateString();
            label5.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(textBox1.Text=="")
             {
                 baglanti.Open();
                 string kayit = "SELECT * from Urun_Listesi Where Barkod = " + textBox2.Text.ToString() + "";
                 SqlCommand komut = new SqlCommand(kayit, baglanti);
                 SqlDataReader read = komut.ExecuteReader();
                 while (read.Read())
                 {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[sayac].Cells[0].Value = textBox2.Text;
                    dataGridView1.Rows[sayac].Cells[1].Value = read["Urun_Adi"];
                    dataGridView1.Rows[sayac].Cells[2].Value = 1;
                    dataGridView1.Rows[sayac].Cells[3].Value = read["Urun_Fiyati"];
                    sayac++;
                    int toplam = (Convert.ToInt32(label5.Text) + Convert.ToInt32(read["Urun_Fiyati"]));
                    label5.Text = toplam.ToString();
                }
                 baglanti.Close();
             }
             else
             {
                 baglanti.Open();
                 string kayit = "SELECT * from Urun_Listesi Where Barkod = " + textBox2.Text.ToString() + "";
                 SqlCommand komut = new SqlCommand(kayit, baglanti);
                 SqlDataReader read = komut.ExecuteReader();
                 while (read.Read())
                 {  
                    dataGridView1.Rows.Add();
                    int gelen = Convert.ToInt32(read["Urun_Fiyati"]);
                    int adet = Convert.ToInt32(textBox1.Text);
                    int carpim = gelen * adet;
                    dataGridView1.Rows[sayac].Cells[0].Value = textBox2.Text;
                    dataGridView1.Rows[sayac].Cells[1].Value = read["Urun_Adi"];
                    dataGridView1.Rows[sayac].Cells[2].Value = adet;
                    dataGridView1.Rows[sayac].Cells[3].Value = carpim;
                    int toplam = (Convert.ToInt32(label5.Text) + carpim);
                    label5.Text = toplam.ToString();
                    sayac++;
                }
                 baglanti.Close(); 
             }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int secili = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);
                int toplam = Convert.ToInt32(label5.Text) - secili;
                label5.Text = toplam.ToString();
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                sayac--;
            }
            else
            {
                MessageBox.Show("Lüffen silinecek satırı seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label5.Text != "0")
            {
                String tarih = Convert.ToString(DateTime.Now);
                baglanti.Open();
                SqlCommand com = new SqlCommand("insert into Muhasebe(Tutar, Tarih) values('" + label5.Text.ToString() + "','" + tarih.ToString() + "')", baglanti);
                com.ExecuteNonQuery();
                MessageBox.Show("Satış başarıyla gerçekleşti.");
                baglanti.Close();
                //Stok azaltmayı yapamadım.
                Kasa main = new Kasa();
                main.Show();
                this.Hide();
            }
            else MessageBox.Show("Ürün girişi yapılmadan ödeme alınamaz.");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
            this.Hide();
        }
    }
}
