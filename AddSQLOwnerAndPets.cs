using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;

namespace WindowsFormsApp4
{
    public partial class AddSQLOwnerAndPets: Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        private bool isAdd;
        public AddSQLOwnerAndPets()
        {
            InitializeComponent();
            isAdd = IDataSave.isAdd;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            if(isAdd)
            {
                if(txtFirstName.Text != "" || txtSecondName.Text != "" || txtCity.Text != "" || txtAddres.Text != "" || txtPetName.Text != "" || txtPetName.Text != "" || txtSpecial.Text != "" || txtBreed.Text != "")
                {
                    IDataSave.OwnerPets OP = new IDataSave.OwnerPets()
                    {
                        firstName = txtFirstName.Text,
                        secondName = txtSecondName.Text,
                        middleName = txtMiddleNAme.Text,
                        numberPhone = txtNumberTel.Text,
                        email = txtEmail.Text,
                        city = txtCity.Text,
                        address = txtAddres.Text,
                    };
                    IDataSave.Pets p = new IDataSave.Pets
                    {
                        petName = txtPetName.Text,
                        species = txtSpecial.Text,
                        breed = txtBreed.Text,
                        color = txtColar.Text,
                        sex = txtSex.Text,
                        mark = txtMark.Text
                    };
                    await mySQLQerty.AddDateOwnerPets(_connectionManager.GetConnection(), p, OP);
                }
                else
                {
                    MessageBox.Show("Ошибка", "Ошибка. Ввеидте везде значение", MessageBoxButtons.OK);
                }
            }
            else if (!isAdd)
            {
               
            }
            await _connectionManager.ConnectionClose();
        }

        private async void AddSQLOwnerAndPets_Load(object sender, EventArgs e)
        {
            if (!isAdd)
            {
                await _connectionManager.ConnectionOpen();
                IDataSave.OwnerPets OP = await mySQLQerty.GetStructureTableOwnerPets(_connectionManager.GetConnection());
                txtFirstName.Text = OP.firstName;
                txtSecondName.Text = OP.secondName;
                txtMiddleNAme.Text = OP.middleName;
                txtNumberTel.Text = OP.numberPhone;
                txtEmail.Text = OP.email;
                txtCity.Text = OP.city;
                txtAddres.Text = OP.address;
                IDataSave.Pets P = await mySQLQerty.GetStructureTablePets(_connectionManager.GetConnection());
                txtPetName.Text = P.petName;
                txtSpecial.Text = P.species;
                txtBreed.Text = P.breed;
                txtColar.Text = P.color;
                txtSex.Text = P.sex;
                txtMark.Text = P.mark;
                await _connectionManager.ConnectionClose();
            }
        }
    }
}
