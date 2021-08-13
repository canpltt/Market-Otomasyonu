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
    public partial class PasswordChange : Form
    {
        SqlConnection con = new SqlConnection("Data Source=ALPHA\\SQLEXPRESS; initial catalog=SedaAkvaryum; Integrated Security=TRUE");
        public PasswordChange()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Login", con);
            SqlDataReader dr = cmd.ExecuteReader();
            cmd.Dispose();
            while (dr.Read())
            {
                if (textBox1.Text == dr["Telefon_No"].ToString())
                {
                    if (textBox2.Text == textBox3.Text)
                    {
                        dr.Close();
                        cmd = new SqlCommand("Update Login set Sifre='" + textBox2.Text.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        MessageBox.Show("Şifre Başarıyla Güncellendi!");
                        con.Close();
                        break;
                    }
                    else MessageBox.Show("Şifreler Uyuşmuyor!");
                    con.Close();
                    break;
                }
                else MessageBox.Show("Girilen telefon numarasına ait bir kullanıcı yok!");
                con.Close();
                break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginPage main = new LoginPage();
            main.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoginPage main = new LoginPage();
            main.Show();
            this.Hide();
        }

        private void PasswordChange_Load(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToLongDateString();
        }
    }
}
