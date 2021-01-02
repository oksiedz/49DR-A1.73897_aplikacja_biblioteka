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

namespace AplikacjaBiblioteka
{
    public partial class view_books : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=PLTOKSIEDZKI04\SQLEXPRESS;Initial Catalog=library_management_system;Integrated Security=True");
        public view_books()
        {
            InitializeComponent();
        }

        private void view_books_Load(object sender, EventArgs e)
        {
            disp_books();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string category = "";
            int i = 0;
            switch (comboBox1.Text)
            {
                case "Tytuł":
                    category = "name";
                    break;
                case "Autor":
                    category = "author_name";
                    break;
                case "Wydawnictwo":
                    category = "publication_name";
                    break;
                default:
                    category = "";
                    break;
            }
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if (category == "")
                {
                    cmd.CommandText = "select Id as Identyfikator, name as Tytuł, author_name as Autor, publication_name as Wydawnictwo, purchase_date as Data_zakupu, quantity as Ilość from book_info";
                }
                else
                {
                    cmd.CommandText = "select id as Identyfikator, name as Tytuł, author_name as Autor, publication_name as Wydawnictwo, purchase_date as Data_zakupu, quantity as Ilość from book_info where " + category + " like '%" + textBox2.Text + "%'";
                }
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                i = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;

                con.Close();

                if (i == 0)
                {
                    MessageBox.Show("Nie ma pozycji spełniających kryteria wyszukiwania.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Panel will be visible only if user click on the data grid
            panel2.Visible = true;
            //Assignment of ID of the row
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select id as Identyfikator, name as Tytuł, author_name as Autor, publication_name as Wydawnictwo, purchase_date as Data_zakupu, quantity as Ilość from book_info where id = " + i + "";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                
                //Presentation of row data into boxes
                foreach(DataRow dr in dt.Rows)
                {
                    textBox1.Text = dr["Tytuł"].ToString();
                    textBox3.Text = dr["Autor"].ToString();
                    textBox4.Text = dr["Wydawnictwo"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr["Data_Zakupu"].ToString());
                    textBox6.Text = dr["Ilość"].ToString();
                }    

                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());

            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update book_info set name = '" + textBox1.Text + "', author_name = '" + textBox3.Text + "', publication_name='" + textBox4.Text  + "', purchase_date = '" + DateTime.Parse(dateTimePicker1.Text) + "', quantity ='" + textBox6.Text + "' where id = " + i +"";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_books();
                panel2.Visible = false;
                MessageBox.Show("Pozycja zaktualizowana");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void disp_books()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id as Identyfikator, name as Tytuł, author_name as Autor, publication_name as Wydawnictwo, purchase_date as Data_zakupu, quantity as Ilość from book_info";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;


                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
