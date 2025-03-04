using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Input;
using MySqlX.XDevAPI.Common;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApp4
{
    internal class MySQLQerty
    {
        public bool AccessLoginAndPassword(string login, string password, MySqlConnection mySqlConnection)
        {
            string qwerty = "SELECT pswrd FROM access WHERE Login = @login";
            using (MySqlCommand mySqlCommand = new MySqlCommand(qwerty, mySqlConnection))
            {
                mySqlCommand.Parameters.AddWithValue("@login", login);
                object passwordObject;
                try
                {
                    passwordObject = mySqlCommand.ExecuteScalar();
                }
                catch (Exception innerEx)
                {
                    MessageBox.Show($"Внутренняя ошибка вывода. Проблема в {innerEx}", "Внутренняя ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (passwordObject != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int get_EmployyesAccess(string login, MySqlConnection mySqlConnection)
        {
            string qwerty = "select Id_Employees from employees inner join access on id_empolyees = Id_Employees where Login = @login";
            using (MySqlCommand mysqlc = new MySqlCommand(qwerty, mySqlConnection))
            {
                try
                {
                    mysqlc.Parameters.AddWithValue("@login", login);
                    object result = mysqlc.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;
                }
            }
        }
    }
}
