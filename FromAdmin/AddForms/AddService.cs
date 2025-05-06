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
    public partial class AddService : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        private bool Add;
        public AddService(bool isAdd)
        {
            InitializeComponent();
            Add = isAdd;
        }

        private async void AddService_Load(object sender, EventArgs e)
        {
            IDataSave.Service serv;
            if (!Add)
            {
                await _connectionManager.ConnectionOpen();
                serv = await mySQLQerty.Select_Service(_connectionManager.GetConnection());
                txtBoxName.Text = serv.Name;
                txtBoxPrice.Text = serv.Price.ToString();
                txtBoxDescription.Text = serv.Description;
                await _connectionManager.ConnectionClose();
            }
        }

        private void btnCansel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                IDataSave.Service serv;
                serv.Name = txtBoxName.Text;
                serv.Price = int.Parse(txtBoxPrice.Text);
                serv.Description = txtBoxDescription.Text;
                await _connectionManager.ConnectionOpen();
                if (!Add)
                    await mySQLQerty.Update_Service(_connectionManager.GetConnection(), serv);
                else if (Add)
                    await mySQLQerty.Add_Service(_connectionManager.GetConnection(), serv);
                await _connectionManager.ConnectionClose();
            }
            catch 
            {
                MessageBox.Show("Ошибка, заполните данные правильно", "Ошибка", MessageBoxButtons.OK);
            }
            
        }
    }
}
