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
    public partial class Muhasebe : Form
    {
        public Muhasebe()
        {
            InitializeComponent();
        }
        public static string conString = "Data Source=ALPHA\\SQLEXPRESS; initial catalog=SedaAkvaryum; Integrated Security=TRUE";
        SqlConnection baglanti = new SqlConnection(conString);

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void kayitGetir()
        {
            baglanti.Open();
            string kayit = "SELECT id, Tutar, Tarih from Muhasebe";
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        DataTable dt;

        private void Muhasebe_Load(object sender, EventArgs e)
        {
            kayitGetir();
            label6.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("Tarih > '{0}' AND Tarih <= '{1}'", dateTimePicker1.Value.ToLongDateString(), dateTimePicker2.Value.ToLongDateString());
            dataGridView1.DataSource = dv;
            //Tarih aralığındaki tutar toplamını yapamadım.
            int toplam = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string donusecek = dataGridView1.Rows[i].Cells[2].Value.ToString();
                toplam =toplam + Convert.ToInt32(donusecek);
            }
            label4.Text = toplam.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kayitGetir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Muhasebe where id='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Başarıyla Silindi!");
            dataGridView1.ClearSelection();
            kayitGetir();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            main.Show();
            this.Hide();
        }
    }
}
