using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.Sql
{
    internal class ForInterfaceSQL
    {
        public async Task<string[]> ArrayFromDB(string sqlQuery, string records, MySqlConnection mySqlConnection)
        {
            List<string> List = new List<string>();
            using (MySqlCommand mysqlCommand = new MySqlCommand(sqlQuery, mySqlConnection))
            {
                using (MySqlDataReader mysqlread = mysqlCommand.ExecuteReader())
                {
                    while (await mysqlread.ReadAsync())
                    {
                        string str = mysqlread.GetString(records);
                        List.Add(str);
                    }
                }
            }
            return List.ToArray();
        }
        public async Task ShowDataGridView(MySqlConnection mySqlConnection, DataGridView dataGridView, string query, string tag)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                try
                {
                    using (MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(mySqlCommand))
                    {
                        myDataAdapter.SelectCommand = mySqlCommand;
                        myDataAdapter.SelectCommand.Connection = mySqlConnection;
                        DataTable DT = new DataTable();
                        await myDataAdapter.FillAsync(DT);
                        dataGridView.DataSource = DT;

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            object idValue = DT.Rows[i][tag];

                            if (idValue != DBNull.Value)
                            {
                                dataGridView.Rows[i].Tag = Convert.ToInt32(idValue);
                            }
                        }
                        if (dataGridView.Columns.Contains(tag))
                        {
                            dataGridView.Columns[tag].Visible = false;
                        }


                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            column.DefaultCellStyle.NullValue = "-";
                        }
                    }
                }
                catch (MySqlException e)
                {
                    MessageBox.Show($"Непредвиденная ошибка. {e}");
                }
            }
        }
        public async Task ShowDataGridView_WHEN(MySqlConnection mySqlConnection, DataGridView dataGridView, string query, string tag, string whenString, int ValueWhen)
        {
            using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                try
                {
                    using (MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(mySqlCommand))
                    {
                        mySqlCommand.Parameters.AddWithValue(whenString, ValueWhen);
                        myDataAdapter.SelectCommand = mySqlCommand;
                        myDataAdapter.SelectCommand.Connection = mySqlConnection;
                        DataTable DT = new DataTable();
                        await myDataAdapter.FillAsync(DT);
                        dataGridView.DataSource = DT;

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            object idValue = DT.Rows[i][tag];

                            if (idValue != DBNull.Value)
                            {
                                dataGridView.Rows[i].Tag = Convert.ToInt32(idValue);
                            }
                        }
                        if (dataGridView.Columns.Contains(tag))
                        {
                            dataGridView.Columns[tag].Visible = false;
                        }


                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            column.DefaultCellStyle.NullValue = "-";
                        }
                    }
                }
                catch (MySqlException e)
                {
                    MessageBox.Show($"Непредвиденная ошибка. {e}");
                }
            }
        }
        public async Task<string> ListTable(string query, MySqlConnection connection, string result)
        {
            string a = "";
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", IDataSave.idStr);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                            return a = Convert.ToString(reader[result], CultureInfo.InvariantCulture);
                        else
                            return null;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return a;
            }
        }
        public static async Task<bool> DeleteRecordAsync(MySqlConnection connection, string tableName, string idColumnName, string idValue)
        {
            string query = $"DELETE FROM `{tableName}` WHERE `{idColumnName}` = @idValue;";

            try
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idValue", idValue);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Запись с {idColumnName} = {idValue} не найдена в таблице {tableName}."); 
                        return false; 
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка при удалении записи из таблицы {tableName}: {ex.Message}");
                return false; 
            }
        }
        public static async Task ExecuteNonQueryAsync(string query, MySqlConnection connection, Dictionary<string, object> parameters = null)
        {
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }
                await command.ExecuteNonQueryAsync();
            }
        }


    }

}
