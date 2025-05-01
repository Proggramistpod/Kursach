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
    }
}
