using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.Sql
{
    internal class GenerlyInterface
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
    }

}
