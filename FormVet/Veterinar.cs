using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    public partial class Veterinar : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        public Veterinar()
        {
            InitializeComponent();
        }

        private async void cmbBox1_SelectedIndexChangedTable(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            switch (cmbBox_Table.SelectedIndex)
            {
                case 0:
                    await mySQLQerty.ShowDataGrid_Visists(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 1:
                    await mySQLQerty.ShowDataGrisView_AllVisits(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 2:
                    await mySQLQerty.ShowDataGrisView_TypeService(_connectionManager.GetConnection(), dataGridView1);
                    break;
            }
            await _connectionManager.ConnectionClose();
        }

        private async void Sure_Click(object sender, EventArgs e)
        {
            if (cmbBox_Table.SelectedIndex == 2)
            {
                return;
            }
            await _connectionManager.ConnectionOpen();
            try
            {
                object TagIndex = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag;
                await mySQLQerty.SureDataServised(_connectionManager.GetConnection(), TagIndex);
            }
            catch
            {
                MessageBox.Show("Ошибка. Выберите строку", "Ошибка");
            }
            await _connectionManager.ConnectionClose();
        }

        private async  void Look_Click(object sender, EventArgs e)
        {
            if (cmbBox_Table.SelectedIndex == 2)
            {
                return;
            }
            try
            {
                await _connectionManager.ConnectionOpen();
                object TagIndex = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag;
                DiagnosisPet dg = new DiagnosisPet(TagIndex);
                await _connectionManager.ConnectionClose();
                dg.Show();
            }
            catch
            {
                MessageBox.Show("Ошибка. Выберите строку", "Ошибка");
            }
        }

        private async void Veterinar_Load(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            await mySQLQerty.GetDiagnosisName(_connectionManager.GetConnection());
            await _connectionManager.ConnectionClose();
            cmbBox_Table.SelectedIndex = 1;
        }
    }
}
