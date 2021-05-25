using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FlipEBookCreatorApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //go to registeration Page

            RegisterationForm page = new RegisterationForm();
            page.ShowDialog();
            //ConcatinatePDF p = new ConcatinatePDF();
            //p.Show();

            //  MergeFilesTest m = new MergeFilesTest();
            //  m.Show();

            //WriteBookPannel b = new WriteBookPannel();
            //b.Show();

          //  videoTest v = new videoTest();
         //   v.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //login button

            //MergeFilesTest m = new MergeFilesTest();
            //m.Show();


            //WriteBookPannel t = new WriteBookPannel();
            //  t.Show();


            string userid = textBox1.Text;
            //  User_Pannel user = new User_Pannel(userid);
            // user.Show();

            string query = "select * from users where userid='" + textBox1.Text + "' and password='" + textBox2.Text + "' ";
            SqlConnection con = new SqlConnection(MyDatabase.connectionString);
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {

                this.Hide();
                string userid1 = dt.Rows[0]["userid"].ToString();
                User_Pannel user1 = new User_Pannel(userid1);
                user1.Show();
            }


            else
            {

                MessageBox.Show("No data is found");

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
