using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace FlipEBookCreatorApplication
{
    public partial class CategoryPannel : Form
    {
        public CategoryPannel()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text;
            string description = textBox2.Text;
            
            if (title.Equals("") || description.Equals("") )
            {

                label6.Text = "Fields should not empty";


            }
            else
            {
               
                    string query = "insert into category(category,description) values('" + title + "','" + description+  "')";
                    SqlConnection con = new SqlConnection(MyDatabase.connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    label6.Text = "Data is saved successfully !";
                    MessageBox.Show("Data is saved successfully");

                

            }
            show();

        }

        private void CategoryPannel_Load(object sender, EventArgs e)
        {
            show();
        }
        private void show()
        {


            string query = "select * from category";
            SqlConnection con = new SqlConnection(MyDatabase.connectionString);
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                
            }
        }
    }
}
