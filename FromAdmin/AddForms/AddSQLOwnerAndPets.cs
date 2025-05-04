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
        int Sex;
        public AddSQLOwnerAndPets(bool add)
        {
            InitializeComponent();
            isAdd = add;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (rdBtnSexM.Checked == true)
            {
                Sex = 1;
            }
            else if (rdBtnSexF.Checked == true)
            {
                Sex = 0;
            }
            else 
            {
                MessageBox.Show("Ошибка", "Ошибка. Выберите пол", MessageBoxButtons.OK);
                return;
            }
            IDataSave.OwnerPets OP = new IDataSave.OwnerPets()
            {
                firstName = txtFirstName.Text,
                secondName = txtSecondName.Text,
                middleName = txtMiddleNAme.Text,
                numberPhone = txtNumberTel.Text,
                email = txtEmail.Text,
                city = cmbBox_City.Text,
                address = txtAddres.Text,
            };
            IDataSave.Pets p = new IDataSave.Pets
            {
                petName = txtPetName.Text,
                species = cmbBox_Species.Text,
                breed = txtBreed.Text,
                color = txtColar.Text,
                sex = Sex,
                mark = txtMark.Text
            };
            await _connectionManager.ConnectionOpen();
            if(isAdd)
            {
                if(txtFirstName.Text != "" || txtSecondName.Text != "" || txtAddres.Text != "" || txtPetName.Text != "" || txtPetName.Text != "" || cmbBox_Species.Text != "" || txtBreed.Text != "")
                {
                    await mySQLQerty.AddDate_OwnerPets(_connectionManager.GetConnection(), p, OP);
                }
                else
                {
                    MessageBox.Show("Ошибка", "Ошибка. Ввеидте везде значение", MessageBoxButtons.OK);
                    return;
                }
            }
            else if (!isAdd)
            {
                await mySQLQerty.ChangeData_OwnerPets(_connectionManager.GetConnection(), OP, p);
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
                cmbBox_City.Text = OP.city;
                txtAddres.Text = OP.address;
                IDataSave.Pets P = await mySQLQerty.GetStructureTablePets(_connectionManager.GetConnection());
                txtPetName.Text = P.petName;
                cmbBox_Species.Text = P.species;
                txtBreed.Text = P.breed;
                txtColar.Text = P.color;
                P.sex = Sex;
                txtMark.Text = P.mark;
                await _connectionManager.ConnectionClose();
                if (Sex == 0)
                {
                    rdBtnSexF.Checked = true;
                    rdBtnSexM.Checked = false;
                }
                else if (Sex == 1)
                {
                    rdBtnSexM.Checked = true;
                    rdBtnSexF.Checked = false;
                }
                btnAdd.Text = "Изменить";
            }
            else
            {
                btnAdd.Text = "Добавить";
            }
                cmbBox_City.Items.AddRange(IDataSave.NameСity);
            cmbBox_Species.Items.AddRange(IDataSave.NameSpecies);
        }
    }
}
