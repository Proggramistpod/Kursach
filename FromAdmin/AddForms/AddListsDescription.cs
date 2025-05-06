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
    public partial class AddListsDescription : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        bool Add;
        public AddListsDescription(bool add )
        {
            InitializeComponent();
            Add = add;
        }

        private async void btnAccess_Click(object sender, EventArgs e)
        {
            IDataSave.Descrip descrip = new IDataSave.Descrip();
            descrip.Name = txtBoxName.Text;
            descrip.Description = txtBoxDescript.Text;
            await _connectionManager.ConnectionOpen();
            if (!Add)
                await mySQLQerty.Update_Diagnosis(_connectionManager.GetConnection(), descrip);
            else if (Add)
                await mySQLQerty.Add_Diagnosis(_connectionManager.GetConnection(), descrip);
            await _connectionManager.ConnectionClose();
        }

        private async void AddListsDescription_Load(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            if (!Add)
            {
                IDataSave.Descrip descrip = await mySQLQerty.Select_Diagnosis(_connectionManager.GetConnection());
                txtBoxName.Text = descrip.Name;
                txtBoxDescript.Text = descrip.Description;
            }
            await _connectionManager.ConnectionClose();
        }
    }
}
