using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.FromAdmin.AddForms
{
    public partial class AddVisits : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        int IDPets, IdDoctor;
        bool Add;
        public AddVisits(bool isAdd)
        {
            Add = isAdd;
            InitializeComponent();
        }

        private async void AddVisits_Load(object sender, EventArgs e)
        {
            cmbBoxService.Items.AddRange(IDataSave.NameService);
            await _connectionManager.ConnectionOpen();
            await mySQLQerty.ShowDataGrisView_OwnerPets(_connectionManager.GetConnection(), dataGridViewPet);
            await mySQLQerty.ShowDataGridView_Employees(_connectionManager.GetConnection(), dataGridViewEmployees);
            if (!Add)
            {
                IDataSave.Visits visits = await mySQLQerty.Select_Visits(_connectionManager.GetConnection());
                SelectRows(dataGridViewPet, Convert.ToString(visits.Pets));
                SelectRows(dataGridViewEmployees, Convert.ToString(visits.Employees));
                cmbBoxService.Text = visits.Serviced;
                TimePicker.Value = DateTime.Today.Add(visits.Date.TimeOfDay);
                DatePicker1.Value = Convert.ToDateTime(visits.Date.Date);
            }
            await _connectionManager.ConnectionClose();
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            IDPets = Convert.ToInt32(dataGridViewPet.Rows[dataGridViewPet.SelectedRows[0].Index].Tag);
            IdDoctor = Convert.ToInt32(dataGridViewEmployees.Rows[dataGridViewEmployees.SelectedRows[0].Index].Tag);
            IDataSave.Visits visits = new IDataSave.Visits();
            visits.Pets = IDPets;
            visits.Employees = IdDoctor;
            visits.Date = DatePicker1.Value.Date.Add(TimePicker.Value.TimeOfDay);
            visits.Serviced = cmbBoxService.Text;
            await _connectionManager.ConnectionOpen();
            if (Add)
                await mySQLQerty.Add_Visits(visits, _connectionManager.GetConnection());
            else if(!Add)
                await mySQLQerty.Update_Visits(_connectionManager.GetConnection(), visits);
            await _connectionManager.ConnectionClose();
        }
        private void SelectRows(DataGridView dataGridView1, string targetTag)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Tag != null && row.Tag.ToString() == targetTag)
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }
    }
}
