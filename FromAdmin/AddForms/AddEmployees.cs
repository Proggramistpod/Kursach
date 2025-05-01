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
    public partial class AddEmployees : Form
    {
        bool Add;
        public AddEmployees(bool isAdd)
        {
            Add = isAdd;
            InitializeComponent();
        }

        private void Employees_Load(object sender, EventArgs e)
        {

        }
        //public string FirstName;
        //public string SecondName;
        //public string MiddleName;
        //public string JobTitle;
        //public string NumberPhone;
        //public string Email;
        //public string SeriesPassport;
        //public string NumberPassport;
        //public string PassportIssued;
        //public DateTime DatePassportIssued;
        //public string UnitCodePassport;
        //public string Login;
        //public string Password;
        //public int LevelAccess;
        //public string WordAccess;
        private void btnAdd_Click(object sender, EventArgs e)
        {
           IDataSave.Employees empl = new IDataSave.Employees();
            empl.FirstName = txtFirstName.Text;
            empl.SecondName = txtSecondName.Text;
            empl.MiddleName = txtMiddleNAme.Text;
            empl.JobTitle = cmbTitle.Text;
            empl.NumberPhone = txtNumberTel.Text;
            empl.Email = txtEmail.Text;
            empl.SeriesPassport = txtSeralPasport.Text;
            empl.NumberPassport = txtSeralPasport.Text;
            empl.DatePassportIssued = datePasport.Value;
            empl.UnitCodePassport = txtBoxUnitCode.Text;
            empl.Login = txtLogin.Text;
            empl.Password = txtPassword.Text;
            empl.LevelAccess = Convert.ToInt32(cmbBoxLvlAccess.Text);
             
            
        }
    }
}
