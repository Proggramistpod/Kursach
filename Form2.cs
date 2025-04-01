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
    public partial class Form2 : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        public Form2()
        {
            InitializeComponent();
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            if (await _connectionManager.ConnectionOpen() == false)
            {
                MessageBox.Show("Не удалось подключиться к базе данных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            comboBox1.SelectedIndex = 0;
            await _connectionManager.ConnectionClose();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    mySQLQerty.ShowDataGridVisistsService(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 1:
                    mySQLQerty.ShowDataGrisViewAllWisitsService(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 2:
                    mySQLQerty.ShowDataGrisViewTypeService(_connectionManager.GetConnection(), dataGridView1);
                    break;
                case 3:
                    mySQLQerty.ShowDataGrisViewOwnerPets(_connectionManager.GetConnection(), dataGridView1);
                    break;
            }
            await _connectionManager.ConnectionClose();
        }

        private void btn_AddStr_Click(object sender, EventArgs e)
        {
            IDataSave.isAdd = true;
            AddSQLOwnerAndPets OP = new AddSQLOwnerAndPets();
            OP.Show();
        }

        private void btn_ChangeStr_Click(object sender, EventArgs e)
        {
            int selectRow = dataGridView1.SelectedRows[0].Index;
            if(selectRow != null)
            {
                IDataSave.isAdd = false;
                IDataSave.idStr = dataGridView1.Rows[selectRow].Cells["Номер клиента"].Value.ToString();
                AddSQLOwnerAndPets OP = new AddSQLOwnerAndPets();
                OP.Show();
            }
            else
            {
                MessageBox.Show("Выберите строчку", "Выберите строчку", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
