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
    public partial class view_student_info : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=PLTOKSIEDZKI04\SQLEXPRESS;Initial Catalog=library_management_system;Integrated Security=True");
        public view_student_info()
        {
            InitializeComponent();
        }

        private void view_student_info_Load(object sender, EventArgs e)
        {
            try
            {
                int j = 0;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id as Identyfikator, name as Imie_Nazwisko, index_no as Nr_Indeksu, department as Wydział, phone as Telefon, email as EMail, image as Zdjęcie from student_info";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                Bitmap img;
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                imageCol.Width = 500;
                imageCol.HeaderText = "Podgląd zdjęcia";
                imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageCol.Width = 100;
                dataGridView1.Columns.Add(imageCol);

                foreach (DataRow dr in dt.Rows)
                {
                    img = new Bitmap(@"..\..\" + dr["Zdjęcie"].ToString());
                    dataGridView1.Rows[j].Cells[7].Value = img;
                    dataGridView1.Rows[j].Height = 100;
                    j = j + 1;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int j = 0;
                dataGridView1.Columns.Clear();
                dataGridView1.Refresh();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id as Identyfikator, name as Imie_Nazwisko, index_no as Nr_Indeksu, department as Wydział, phone as Telefon, email as EMail, image as Zdjęcie from student_info where name like '%" + textBox1.Text + "%' or index_no like '%" + textBox1.Text + "%' or department like '%" + textBox1.Text + "%' or phone like '%" + textBox1.Text + "%' or email like '%" + textBox1.Text + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int rowCount = Convert.ToInt32(dt.Rows.Count.ToString());
                
                dataGridView1.DataSource = dt;

                Bitmap img;
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
                imageCol.Width = 500;
                imageCol.HeaderText = "Podgląd zdjęcia";
                imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imageCol.Width = 100;
                dataGridView1.Columns.Add(imageCol);

                foreach (DataRow dr in dt.Rows)
                {
                    img = new Bitmap(@"..\..\" + dr["Zdjęcie"].ToString());
                    dataGridView1.Rows[j].Cells[7].Value = img;
                    dataGridView1.Rows[j].Height = 100;
                    j = j + 1;
                }
                if (rowCount == 0)
                {
                    MessageBox.Show("Nie ma pozycji spełniających kryteria wyszukiwania.");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
