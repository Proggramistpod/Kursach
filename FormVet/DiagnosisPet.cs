using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class DiagnosisPet : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        string NameDiagnosis;
        int str; //номер записи
        public DiagnosisPet(object tag)
        {
            InitializeComponent();
            str = Convert.ToInt32(tag);
            cmbBox_Diagnosis.Items.AddRange(IDataSave.NameDiagnosis);
        }

        private async void DiagnosisPet_Load(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            mySQLQerty.ShowDataGrid_DiagnosisPet(_connectionManager.GetConnection(), str, dataGridView1);
            await _connectionManager.ConnectionClose();
        }

        private async void AddDiagnosis_Click(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            await mySQLQerty.AddDiagnosisPet(_connectionManager.GetConnection(), NameDiagnosis, str, dateTimePicker1.Value);
            mySQLQerty.ShowDataGrid_DiagnosisPet(_connectionManager.GetConnection(), str, dataGridView1);
            await _connectionManager.ConnectionClose();
        }

        private void cmbBox_SelectedIndexChangedDiagnosis(object sender, EventArgs e)
        {
            NameDiagnosis = cmbBox_Diagnosis.SelectedItem.ToString();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
