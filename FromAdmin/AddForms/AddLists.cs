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

namespace WindowsFormsApp4.FromAdmin.AddForms
{
    public partial class AddLists : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        string tabl;
        bool add;
        public AddLists(string Table, bool isAdd)
        {
            InitializeComponent();
            add = isAdd;
            tabl = Table;
        }

        private async void btnAccess_Click(object sender, EventArgs e)
        {

            string inf = textBox1.Text;
            await _connectionManager.ConnectionOpen();
            DialogResult = MessageBox.Show("Проверка", "Уверены?", MessageBoxButtons.YesNo);
            if (DialogResult == DialogResult.Yes)
                switch (tabl)
                {
                case "city":
                    if (!add)
                        await mySQLQerty.Update_City(_connectionManager.GetConnection(), inf);
                    else if (add)
                        await mySQLQerty.Add_City(_connectionManager.GetConnection(), inf);
                    break;
                case "species":
                    if (!add)
                        await mySQLQerty.Update_Species(_connectionManager.GetConnection(), inf);
                    else if (add)
                        await mySQLQerty.Add_Species(_connectionManager.GetConnection(), inf);
                    break;
                case "job":
                    if (!add)
                        await mySQLQerty.Update_JobTitle(_connectionManager.GetConnection(), inf);
                    else if (add)
                        await mySQLQerty.Add_JobTitle(_connectionManager.GetConnection(), inf);
                    break;
                }
            await _connectionManager.ConnectionClose();
        }

        private async void AddLists_Load(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            switch (tabl)
            {
                case "city":
                    if (!add)
                        textBox1.Text = await mySQLQerty.Select_City(_connectionManager.GetConnection());
                    this.Text = "Города";
                    break;
                case "species":
                    if (!add)
                        textBox1.Text = await mySQLQerty.Select_Species(_connectionManager.GetConnection());
                    this.Text = "Виды";
                    break;
                case "job":
                    if (!add)
                        textBox1.Text = await mySQLQerty.Select_JobTitle(_connectionManager.GetConnection());
                    this.Text = "Работы";
                    break;
            }
            await _connectionManager.ConnectionClose();
        }
    }
}
