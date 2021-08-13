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
    public partial class LoginPage : Form
    {
        SqlConnection con = new SqlConnection("Data Source=ALPHA\\SQLEXPRESS; initial catalog=SedaAkvaryum; Integrated Security=TRUE");
        public LoginPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayitlimi = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from Login", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (textBox1.Text == dr["Kullanici_Adi"].ToString() && textBox2.Text == dr["Sifre"].ToString())
                {
                    kayitlimi = true;
                    break;
                }
            }
            con.Close();
            if (kayitlimi == true)
            {
                MainPage main = new MainPage();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Hatalıdır!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PasswordChange main = new PasswordChange();
            main.Show();
            this.Hide();
        }
    }
}
