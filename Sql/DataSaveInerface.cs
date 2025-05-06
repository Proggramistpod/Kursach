using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    internal class IDataSave
    {
        public static int idPets, IdOwner;
        public static object idEmployees { get; set; }
        public static int LevelAccess { get; set; }
        public static int idStr { get; set; }
        public static bool isAdd { get; set; }
        public static string[] NameDiagnosis { get; set; }
        public static string[] NameСity { get; set; }
        public static string[] NameSpecies { get; set; }
        public static string[] NameJobTitle { get; set; }
        public static string[] NameService { get; set; }
        public struct OwnerPets
        {
            public string firstName;
            public string secondName;
            public string middleName;
            public string numberPhone;
            public string email;
            public string city;
            public string address;
        }
        public struct Pets
        {
            public string petName;
            public string species;
            public string breed;
            public string color;
            public int sex;
            public string mark;
        }
        public struct Employees
        {
            public string FirstName;
            public string SecondName;
            public string MiddleName;
            public string JobTitle;
            public string NumberPhone;
            public string Email;
            public string City;
            public string Address;
            public string SeriesPassport;
            public string NumberPassport;
            public string PassportIssued;
            public DateTime DatePassportIssued;
            public string UnitCodePassport;
            public string Login;
            public string Password;
            public string WordAccess; 
        }
        public struct Visits
        {
            public int Pets;
            public int Employees;
            public DateTime Date;
            public string Serviced;
        }
        public struct Service
        {
            public string Name;
            public string Description;
            public int Price;
        }
    }
}
