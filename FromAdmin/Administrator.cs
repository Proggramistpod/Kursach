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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    //Визиты 0
    //Сервис 1
    //Владельцы животных и Животные 2
    //Диагнозы 3
    //Сотрудники 4
    //Города 5
    //Виды животных 6
    //Должность 7
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
                case 7:
                    await mySQLQerty.ShowDataGrid_JobTitle(_connectionManager.GetConnection(), dataGridView1);
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
                case 3:
                    AddListsDescription ald = new AddListsDescription(true);
                    ald.Show();
                    break;
                case 2:
                    AddSQLOwnerAndPets a = new AddSQLOwnerAndPets(true);
                    a.Show();
                    break;
                case 4:
                    AddEmployees f = new AddEmployees(true);
                    f.Show();
                    break;
                case 5:
                    AddLists l = new AddLists("city", true);
                    l.Show();
                    break;
                case 6:
                    AddLists sp = new AddLists("species", true);
                    sp.Show();
                    break;
                case 7:
                    AddLists sps = new AddLists("species", true);
                    sps.Show();
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
                case 3:
                    AddListsDescription ald = new AddListsDescription(false);
                    ald.Show();
                    break;
                case 4:
                    AddEmployees f = new AddEmployees(false);
                    f.Show();
                    break;
                case 5:
                    AddLists l = new AddLists("city", false);
                    l.Show();
                    break;
                case 6:
                    AddLists sp = new AddLists("species", false);
                    sp.Show();
                    break;
                case 7:
                    AddLists sps = new AddLists("job", false);
                    sps.Show();
                    break;
            }
        }

        private void Administrator_Load(object sender, EventArgs e)
        {
            IDataSave.idStr = -1;
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            DialogResult = MessageBox.Show("Проверка", "Уверены что хотите удалить эту запись?", MessageBoxButtons.YesNo);
            if (DialogResult == DialogResult.Yes)
                switch (Index)
                {
                    case 0:
                        await mySQLQerty.DeletTable("visits", _connectionManager.GetConnection(), "Id_Visits");
                        break;
                    case 1:
                        await mySQLQerty.DeletTable("service", _connectionManager.GetConnection(), "Id_service");
                        break;
                    case 2:
                        await mySQLQerty.DeleteOwnerAndPetsAsync(_connectionManager.GetConnection());
                        break;
                    case 3:
                        await mySQLQerty.DeletTable("diagnosis", _connectionManager.GetConnection(), "IdDiagnosis");
                        break;
                    case 4:
                        await mySQLQerty.DeletTable("employees", _connectionManager.GetConnection(), "Id_Employees");
                        break;
                    case 5:
                        await mySQLQerty.DeletTable("city", _connectionManager.GetConnection(), "idCity");
                        break;
                    case 6:
                        await mySQLQerty.DeletTable("species", _connectionManager.GetConnection(), "IdAnimal");
                        break;
                    case 7:
                        await mySQLQerty.DeletTable("jobtitle", _connectionManager.GetConnection(), "IdJobTitle");
                        break;
                }
            await _connectionManager.ConnectionClose();
        }

        private void textBoxSerch_TextChanged(object sender, EventArgs e)
        {
            var value = textBoxSerch.Text.Trim();
            int cnt = 0, numRow = 0;
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                var row = dataGridView1.Rows[i];
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (row.Cells[j].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                        cnt++; numRow = i;
                        break;

                    }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Уверены", "Уверены что хотите выйти из аккаунта?", MessageBoxButtons.YesNoCancel);
            if (d == DialogResult.Yes)
            {            
            Atorisation a = new Atorisation();
            a.Show();
            this.Hide();
            }
        }
    }
}