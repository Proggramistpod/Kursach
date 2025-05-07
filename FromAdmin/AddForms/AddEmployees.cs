using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class AddEmployees : Form
    {
        private DatabaseConnectionManager _connectionManager = new DatabaseConnectionManager();
        private MySQLQerty mySQLQerty = new MySQLQerty();
        bool Add;
        public AddEmployees(bool isAdd)
        {
            Add = isAdd;
            InitializeComponent();
        }

        private async void Employees_Load(object sender, EventArgs e)
        {
            await _connectionManager.ConnectionOpen();
            await mySQLQerty.GetListName(_connectionManager.GetConnection());
            cmbBoxFactCity.Items.AddRange(IDataSave.NameСity);
            cmbBoxTitle.Items.AddRange(IDataSave.NameJobTitle);
            if (!Add)
            {
                IDataSave.Employees empl = await mySQLQerty.EmployeesSelect(_connectionManager.GetConnection());
                txtFirstName.Text = empl.FirstName;
                txtSecondName.Text = empl.SecondName;
                txtMiddleNAme.Text = empl.MiddleName;
                cmbBoxTitle.Text = empl.JobTitle;
                txtNumberTel.Text = empl.NumberPhone;
                txtEmail.Text = empl.Email;
                cmbBoxFactCity.Text = empl.City;
                txtFactSteet.Text = empl.Address;
                txtSeralPasport.Text = empl.SeriesPassport;
                txtNumberPasport.Text = empl.NumberPassport; 
                txtWhenPasport.Text = empl.PassportIssued;
                datePasport.Value = empl.DatePassportIssued != DateTime.MinValue ? empl.DatePassportIssued : DateTime.Today;
                txtBoxUnitCode.Text = empl.UnitCodePassport;
                txtLogin.Text = empl.Login;
                txtPassword.Text = empl.Password;
            }
            await _connectionManager.ConnectionClose();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (datePasport.Value < DateTime.Today.AddYears(-70))
            {
                MessageBox.Show("Дата паспорта не может быть старше 70 лет");
                return;
            }
            ValidateEmailTextBox(txtEmail);
            DialogResult = MessageBox.Show("Проверка", "Уверены?", MessageBoxButtons.YesNo);
            if (DialogResult == DialogResult.Yes)
            {
                await _connectionManager.ConnectionOpen();
                IDataSave.Employees empl = new IDataSave.Employees();
                empl.FirstName = txtFirstName.Text;
                empl.SecondName = txtSecondName.Text;
                empl.MiddleName = txtMiddleNAme.Text;
                empl.JobTitle = cmbBoxTitle.Text;
                empl.NumberPhone = txtNumberTel.Text;
                empl.Email = txtEmail.Text;
                empl.City = cmbBoxFactCity.Text;
                empl.Address = txtFactSteet.Text;
                empl.SeriesPassport = txtSeralPasport.Text;
                empl.NumberPassport = txtNumberPasport.Text;
                empl.PassportIssued = txtWhenPasport.Text;
                empl.DatePassportIssued = datePasport.Value;
                empl.UnitCodePassport = txtBoxUnitCode.Text;
                empl.Login = txtLogin.Text;
                empl.Password = txtPassword.Text;

                if (Add)
                    await mySQLQerty.AddEmployes(empl, _connectionManager.GetConnection());
                else
                    await mySQLQerty.ChangeData_Employees(_connectionManager.GetConnection(), empl);
                await _connectionManager.ConnectionClose();
            }
        }
       private void PressKeyOnlyNumeric(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
        private bool ValidateEmailTextBox(TextBox textBox)
        {
            string email = textBox.Text;

            try
            {
                MailAddress address = new MailAddress(email); 
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат адреса электронной почты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Focus();
                textBox.SelectAll();
                return false;
            }
        }
    }
}

