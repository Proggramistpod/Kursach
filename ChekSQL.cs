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
        public bool IsConnected { get; private set; }
        public DatabaseConnectionManager()
        {
            IsConnected = false;
        }
        public async Task<bool> ConnectionOpen()
        {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["vet_clinic"].ConnectionString;
                    _mySqlConnection = new MySqlConnection(connectionString);
                    await _mySqlConnection.OpenAsync();
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
        public async Task<bool> ConnectionClose()
        {
            if (_mySqlConnection != null && _mySqlConnection.State == System.Data.ConnectionState.Open)
            {
                await _mySqlConnection.CloseAsync();
                IsConnected = false;
                return true;
            }
            return false;
        }
    }
}
