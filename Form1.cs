using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        public Form1()
        {
            InitializeComponent();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            if (_connectionManager.IsConnected)
            {
                var username = textBoxLogin.Text;
                var password = textBoxPassword.Text;
                bool returned = Convert.ToBoolean(await mySQLQerty.AccessLoginAndPassword(username, password, _connectionManager.GetConnection()));
                if (returned)
                {
                    IDataSave.idEmployees = await mySQLQerty.get_EmployyesAccess(username, _connectionManager.GetConnection());
                    Form2 f = new Form2();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Нет подключения к базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            await _connectionManager.ConnectionClose();
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            if (await _connectionManager.ConnectionOpen() == false)
            {
                MessageBox.Show("Не удалось подключиться к базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            await _connectionManager.ConnectionClose();
        }
    }
}
