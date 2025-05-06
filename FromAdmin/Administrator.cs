using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4.FromAdmin.AddForms;

namespace WindowsFormsApp4
{
    //Визиты 0
    //Сервис 1
    //Владельцы животных и Животные 2
    //Диагнозы 3
    //Сотрудники 4
    //Города 5
    //Виды животных 6
    public partial class Administrator : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        int Index; // Номер таблицы
        public Administrator()
        {
            InitializeComponent();
        }

        private async void cmbBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            Index = cmbBoxTable.SelectedIndex;
            await _connectionManager.ConnectionOpen();
            switch (Index)
            {
                case 0:
                    await mySQLQerty.ShowDataGrisView_AllVisits(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 1:
                    await mySQLQerty.ShowDataGrisView_TypeService(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 2:
                    await mySQLQerty.ShowDataGrisView_OwnerPets(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 3:
                    await mySQLQerty.ShowDataGridView_AllDiagnosis(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 4:
                    await mySQLQerty.ShowDataGridView_Employees(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 5:
                    await mySQLQerty.ShowDataGridView_City(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 6:
                    await mySQLQerty.ShowDataGridView_Species(_connectionManager.GetConnection(), dataGridView1);
                    break;
                default:
                    return;
            }
            await _connectionManager.ConnectionClose();
        }

        private void btnAddRecordDB_Click(object sender, EventArgs e)
        {
            IDataSave.idStr = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag);
            switch (Index)
            {
                case 0:
                    AddVisits v = new AddVisits(true);
                    v.Show();
                    break;
                case 1:
                    AddService s = new AddService(true);
                    s.Show();
                    break;
                case 2:
                    AddSQLOwnerAndPets a = new AddSQLOwnerAndPets(true);
                    a.Show();                                      
                    break;
                case 4:
                    AddEmployees f = new AddEmployees(true);
                    f.Show();
                    break;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IDataSave.idStr = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            IDataSave.idStr = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag);
            switch (Index)
            {
                case 0:
                    AddVisits v = new AddVisits(false);
                    v.Show();
                    break;
                case 1:
                    AddService s = new AddService(false);
                    s.Show();
                    break;
                case 2:
                    AddSQLOwnerAndPets a = new AddSQLOwnerAndPets(false);
                    a.Show();
                    break;
                case 4:
                    AddEmployees f = new AddEmployees(false);
                    f.Show();
                    break;
            }
        }

        private void Administrator_Load(object sender, EventArgs e)
        {
            IDataSave.idStr = -1;
        }
    }
}
