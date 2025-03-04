using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;

namespace WindowsFormsApp4
{
    internal class DatabaseConnectionManager
    {
        private MySqlConnection _mySqlConnection;
        private readonly string _connectionStringName = "vet_clinic";
        public bool IsConnected { get; private set; }
        private int IdEmployees;
        public DatabaseConnectionManager()
        {
            IsConnected = false;
        }
        public bool Connection()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["vet_clinic"].ConnectionString;
                _mySqlConnection = new MySqlConnection(connectionString);
                _mySqlConnection.Open();
                IsConnected = true;
                return true; 
            }
            catch (MySqlException ex)
            {
                IsConnected = false;
                Console.WriteLine($"Ошибка подключения: {ex.Message}"); 
                return false; 
            }
        }
        public MySqlConnection GetConnection()
        {
            return _mySqlConnection;
        }
        public void Open()
        {
            Connection();
        }
        public void Close()
        {
            if (_mySqlConnection != null && _mySqlConnection.State == System.Data.ConnectionState.Open)
            {
                _mySqlConnection.Close();
                IsConnected = false;
            }
        }
    }
}
