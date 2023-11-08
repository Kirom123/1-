using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _1с
{
    public partial class Form1 : Form
    {

        DataBase dataBase = new DataBase();

        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Vxod_Click(object sender, EventArgs e)
        {

            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable table = new DataTable();

            var nameUser = log.Text;
            var passwordUser = pass.Text;


            string request = $"select id, Login, Password from Users where Login = '{nameUser}' and Password = '{passwordUser}'";

            SqlCommand command = new SqlCommand(request, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);



            if (table.Rows.Count == 1)
            {
                int id = Convert.ToInt32(table.Rows[0][0]);
                Form3 form = new Form3(id);
                form.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Не верно введен логин или пароль.");
            dataBase.CloseConnection();

        }

        private void Reg_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
            this.Hide();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataBase.OpenConnection();
        }
    }
}
