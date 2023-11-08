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

namespace _1с
{
    public partial class Form2 : Form
    {

        DataBase data = new DataBase();

        public Form2()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var nameUser = Log.Text;
            var passwordUser = Pass.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable table = new DataTable();


            string request = $"select id, Login, Password from Users where Login = '{nameUser}' and Password = '{passwordUser}'";

            SqlCommand command = new SqlCommand(request, data.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Есть такой,  давай другой");
                Log.Clear();
                Pass.Clear();
            }
            else
            {
                request = $"insert into Users(Login,Password) values('{nameUser}', '{passwordUser}')";

                command = new SqlCommand(request, data.GetConnection());

                data.OpenConnection();
                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Вы успешно зарегистрировались!!!");
                        Form1 form = new Form1();
                        form.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось зарегистрироваться");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                data.CloseConnection();
            }
        }
    }
}

