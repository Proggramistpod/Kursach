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
    public partial class Atorisation : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty _mySQLQerty = new MySQLQerty();
        public Atorisation()
        {
            InitializeComponent();
        }
        private async void button1_Click(object sender, EventArgs e) //Кнопка входа
        {
            var username = textBoxLogin.Text;
            var password = textBoxPassword.Text;
            await _connectionManager.ConnectionOpen();
            try
            {
                await _mySQLQerty.AccessLoginAndPassword(username, password, _connectionManager.GetConnection());
                IDataSave.idEmployees = await _mySQLQerty.get_IdEmployyes(username, _connectionManager.GetConnection());
                IDataSave.LevelAccess = await _mySQLQerty.get_EmployyesAccess(_connectionManager.GetConnection());
                await _connectionManager.ConnectionClose();
                switch (IDataSave.LevelAccess)
                {
                    case 1:
                        Administrator a = new Administrator();
                        a.Show(); this.Hide();
                        break;
                    case 2:
                        Veterinar b = new Veterinar();
                        b.Show(); this.Hide();
                        break;
                    default:
                        MessageBox.Show("Это приложение не доступно вам", "Отказ в доступе", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
            }
            catch 
            {
                MessageBox.Show("Проверте правильность логина и пароля или соединение с сетью.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void Form1_Load(object sender, EventArgs e) //Проверка соединения при загрузки
        {
            await _connectionManager.ConnectionOpen();
            if (await _connectionManager.ConnectionOpen() == false)
            {
                MessageBox.Show("Не удалось подключиться к базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            await _mySQLQerty.GetListName(_connectionManager.GetConnection());
            await _connectionManager.ConnectionClose();
        }

        private void label3_Click(object sender, EventArgs e)
        {
             
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
