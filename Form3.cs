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
    public partial class Form3 : Form
    {

        int UseId;
        DataBase dataBase = new DataBase();

        public Form3(int id)
        {
            this.UseId = id;
            InitializeComponent();
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            dataBase.OpenConnection();
            initComboBox(comboBox1, "Product");
            initComboBox(comboBox2, "Prodavec");
            initComboBox(comboBox3, "Sklad");
            updateTableListView(listView1, "Product", "ProductN", comboBox1);
            updateTableListView(listView2, "Prodavec", "NameProdavec", comboBox2);
            updateTableListView(listView3, "Sklad", "NameSklad", comboBox3);

        }


        private void Lodbutton2_Click(object sender, EventArgs e)
        {
            addTableInfo(textBox1, "Product", "ProductN");
            updateTableListView(listView1, "Product", "ProductN", comboBox1);
        }


        private void Lbutton2_Click(object sender, EventArgs e)
        {
            addTableInfo(textBox2, "Prodavec", "NameProdavec");
            updateTableListView(listView2, "Prodavec", "NameProdavec", comboBox2);
        }


        private void updateTableListView(ListView view, string nameTable, string columns, ComboBox box)
        {
            view.Items.Clear();

            string request = $"select * from {nameTable} where UseId = '{UseId}'";


            SqlDataReader reader = null;

            try
            {
                SqlCommand command = new SqlCommand(request, dataBase.GetConnection());

                reader = command.ExecuteReader();

                ListViewItem item = null;

                while (reader.Read())
                {
                    item = new ListViewItem(new string[] { Convert.ToString(reader["id"]),
                        Convert.ToString(reader["UseId"]),
                        Convert.ToString(reader[$"{columns}"])});


                    view.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
            initComboBox(box, nameTable);
        }

        private void Tbutton4_Click(object sender, EventArgs e)
        {
            listView4.Items.Clear();

            string request = $"select * from Spisok where UseId = '{UseId}'";


            SqlDataReader reader = null;

            try
            {
                SqlCommand command = new SqlCommand(request, dataBase.GetConnection());

                reader = command.ExecuteReader();

                ListViewItem item = null;

                while (reader.Read())
                {
                    item = new ListViewItem(new string[] { 
                        Convert.ToString(reader["Product"]),
                        Convert.ToString(reader["Prodavec"]),
                        Convert.ToString(reader["Sklad"]),
                        Convert.ToString(reader["KolVo"]), 
                        Convert.ToString(reader["id"]),
                        Convert.ToString(reader["UseId"]) });

                    listView4.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
        }

        private void Lbutton3_Click(object sender, EventArgs e)
        {
            addTableInfo(textBox3, "Sklad", "NameSklad");
            updateTableListView(listView3, "Sklad", "NameSklad", comboBox3);
        }

        private void addTableInfo(TextBox box, string nameTable, string column)
        {
            if (box.Text == "" || box.Text.Contains(" "))
            {
                MessageBox.Show("Некорректно введены данные");
                box.Clear();
            }
            else
            {
                string request = $"insert into {nameTable} (UseId, {column}) values ('{UseId}', N'{box.Text}')";

                SqlDataAdapter adapter = new SqlDataAdapter();

                DataTable table = new DataTable();

                SqlCommand command = new SqlCommand(request, dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);
                box.Clear();
            }
        }

        private void initComboBox(ComboBox box, string nameDB)
        {
            box.Items.Clear();

            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable table = new DataTable();

            string request = $"select * from {nameDB} where UseId = '{UseId}'";

            SqlCommand command = new SqlCommand(request, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++) 
               
            {
                if(box.Name == "comboBox1")
                    box.Items.Add(table.Rows[i][2]);
                else
                    box.Items.Add(table.Rows[i][1]);
            }
        }

        private void Tbutton5_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" ||
                textBox4.Text == "" ||
                textBox4.Text.Contains(" "))
            {
                MessageBox.Show("Введены не все данные");
            }
            else
            {
                string request = $"insert into Spisok (UseId, Product, Prodavec, Sklad, KolVo) values ('{UseId}'," +
                    $" N'{comboBox1.SelectedItem}', N'{comboBox2.SelectedItem}', N'{comboBox3.SelectedItem}'," +
                    $" '{textBox4.Text}')";

                SqlDataAdapter adapter = new SqlDataAdapter();

                DataTable table = new DataTable();

                SqlCommand command = new SqlCommand(request, dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);
            }
        }
    }

}
