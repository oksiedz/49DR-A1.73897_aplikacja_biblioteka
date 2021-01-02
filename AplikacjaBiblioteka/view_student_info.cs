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
            int j = 0;

            try
            {
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

    }
}
