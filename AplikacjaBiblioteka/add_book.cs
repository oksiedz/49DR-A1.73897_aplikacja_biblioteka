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
    public partial class add_book : Form
    {
        //Connection string to the local data base
        SqlConnection con = new SqlConnection(@"Data Source=PLTOKSIEDZKI04\SQLEXPRESS;Initial Catalog=library_management_system;Integrated Security=True");
        public add_book()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Variables used for converstion and data input
                int correctData = 0;
                int quantity = 0;
                //Attempt to convert quantity to int
                bool canConvert = int.TryParse(textBox5.Text, out quantity);

                //Data input validation
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Podaj tytuł książki");
                }
                else if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Podaj autora");
                }
                else if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageBox.Show("Podaj wydawnictwo");
                }
                else if (canConvert == false)
                {
                    MessageBox.Show("Podaj ilość");
                }
                else
                {
                    correctData = 1;
                }

                if (correctData == 1)
                {
                    //Check connection status and set it to open one.
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    //Adding new book to data base
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into book_info(name, author_name, publication_name, purchase_date, quantity, available) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + DateTime.Parse(dateTimePicker1.Text) + "'," + quantity + "," + quantity + ")";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    //Seting default values for all textboxes
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox5.Text = "";

                    //Show info that book was added
                    MessageBox.Show("Dodano książkę");

                    //Closing connection to the base
                    con.Close();
                }
            }    
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
