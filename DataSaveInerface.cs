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
        public static object idEmployees { get; set; }
        public static string idStr { get; set; }
        public static bool isAdd { get; set; }
        public static object idPet { get; set; }
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
            public string sex;
            public string mark;
        }
    }
}
