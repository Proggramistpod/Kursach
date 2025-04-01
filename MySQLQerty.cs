using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp4.IDataSave;

namespace WindowsFormsApp4
{
    internal class MySQLQerty
    {
        public async Task<bool> AccessLoginAndPassword(string login, string password, MySqlConnection mySqlConnection)
        {
            string qwerty = "SELECT pswrd FROM access WHERE Login = @login";
            using (MySqlCommand mySqlCommand = new MySqlCommand(qwerty, mySqlConnection))
            {
                mySqlCommand.Parameters.AddWithValue("@login", login);
                object passwordObject;
                try
                {
                    passwordObject = await mySqlCommand.ExecuteScalarAsync();
                }
                catch (Exception innerEx)
                {
                    MessageBox.Show($"Внутренняя ошибка вывода. Проблема в {innerEx}", "Внутренняя ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (passwordObject != null)
                {
                    string storedPassword = passwordObject.ToString();
                    return string.Equals(password, storedPassword, StringComparison.Ordinal);
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<int> get_EmployyesAccess(string login, MySqlConnection mySqlConnection)
        {

            string qwerty = "select Id_Employees from employees inner join access on id_empolyees = Id_Employees where Login = @login";
            using (MySqlCommand mysqlc = new MySqlCommand(qwerty, mySqlConnection))
            {
                try
                {
                    mysqlc.Parameters.AddWithValue("@login", login);
                    object result = await mysqlc.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;
                }
            }
        }
        public async void ShowDataGridVisistsService(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = @"
                SELECT
                    op.id_Owner AS 'Номер клиента',
                    CONCAT_WS(' ', op.First_Name, op.Second_Name, op.Middle_Name) AS 'ФИО',
                    p.Name_Pets AS 'Кличка',
                    p.Species AS 'Вид',
                    p.Color AS 'Окрас',
                    s.Service 'Услуга',
                    CONCAT_WS(' ', v.Time_Visits, v.Data_Visits) AS 'Время'
                FROM
                    visits v
                    INNER JOIN employees e ON v.Id_Doctor = e.Id_Employees
                    INNER JOIN owner_pets op ON v.Id_Owner = op.id_Owner
                    INNER JOIN pets p ON op.id_pets = p.Id_Pets
                    INNER JOIN visitsService vs ON v.Id_Visits = vs.Id_Visit
                    INNER JOIN service s ON vs.Id_Service = s.Id_Service
                    WHERE
                        e.Id_Employees = @employeeId;"
            ;
            using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                try
                {
                    mySqlCommand.Parameters.Add("@employeeId", MySqlDbType.Int32).Value = IDataSave.idEmployees;
                    using (MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(mySqlCommand))
                    {
                        DataTable DT = new DataTable();
                        await myDataAdapter.FillAsync(DT);
                        dataGridView.DataSource = DT;
                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            column.DefaultCellStyle.NullValue = "-";
                        }
                    }
                }
                catch (MySqlException e)
                {
                    MessageBox.Show($"Не предвиденная ошибка. {e}");
                }

            }

        }
        public async void ShowDataGrisViewTypeService(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = "SELECT Service AS 'Услуга', Price AS 'Цена', Description AS 'Описание' FROM service";
            using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                try
                {
                    mySqlCommand.Parameters.Add("@employeeId", MySqlDbType.Int32).Value = IDataSave.idEmployees;
                    using (MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(mySqlCommand))
                    {
                        DataTable DT = new DataTable();
                        await myDataAdapter.FillAsync(DT);
                        dataGridView.DataSource = DT;
                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            column.DefaultCellStyle.NullValue = "-";
                        }
                    }
                }
                catch (MySqlException e)
                {
                    MessageBox.Show($"Не предвиденная ошибка. {e}");
                }
            }
        }
        public async void ShowDataGrisViewAllWisitsService(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = @"
                SELECT
                    op.id_Owner AS 'Номер клиента',
                    CONCAT_WS(' ', op.First_Name, op.Second_Name, op.Middle_Name) AS 'ФИО',
                    p.Name_Pets AS 'Кличка',
                    p.Species AS 'Вид',
                    p.Color AS 'Окрас',
                    s.Service 'Услуга',
                    CONCAT_WS(' ', v.Time_Visits, v.Data_Visits) AS 'Время'
                FROM
                    visits v
                    INNER JOIN employees e ON v.Id_Doctor = e.Id_Employees
                    INNER JOIN owner_pets op ON v.Id_Owner = op.id_Owner
                    INNER JOIN pets p ON op.id_pets = p.Id_Pets
                    INNER JOIN visitsService vs ON v.Id_Visits = vs.Id_Visit
                    INNER JOIN service s ON vs.Id_Service = s.Id_Service
                    "
            ;
            using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                try
                {
                    mySqlCommand.Parameters.Add("@employeeId", MySqlDbType.Int32).Value = IDataSave.idEmployees;
                    using (MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(mySqlCommand))
                    {
                        DataTable DT = new DataTable();
                        await myDataAdapter.FillAsync(DT);
                        dataGridView.DataSource = DT;
                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            column.DefaultCellStyle.NullValue = "-";
                        }
                    }
                }
                catch (MySqlException e)
                {
                    MessageBox.Show($"Не предвиденная ошибка. {e}");
                }

            }
        }
        public async void ShowDataGrisViewOwnerPets(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = @"
                SELECT  
                    op.id_Owner AS 'Номер клиента',
                    CONCAT_WS(' ', op.First_Name, op.Second_Name, op.Middle_Name) AS FullName,
                    CONCAT_WS(', ', op.City, op.Street) AS Address,
                    op.Number_Phone AS PhoneNumber,
                    p.Name_Pets AS PetName,
                    p.Species AS Species,
                    p.Breed AS Breed,
                    p.Color AS Color
                 FROM
                    owner_pets op
                JOIN
                    pets p ON op.id_pets = p.Id_Pets;";
            try
            {

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, mySqlConnection))
                {
                    DataTable dataTable = new DataTable();
                    await adapter.FillAsync(dataTable);
                    dataGridView.DataSource = dataTable;
                    dataGridView.Columns["FullName"].HeaderText = "ФИО";
                    dataGridView.Columns["Address"].HeaderText = "Адрес проживания";
                    dataGridView.Columns["PhoneNumber"].HeaderText = "Номер телефона";
                    dataGridView.Columns["PetName"].HeaderText = "Кличка питомца";
                    dataGridView.Columns["Species"].HeaderText = "Вид";
                    dataGridView.Columns["Breed"].HeaderText = "Порода";
                    dataGridView.Columns["Color"].HeaderText = "Окрас";
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        column.DefaultCellStyle.NullValue = "-";
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show($"Не предвиденная ошибка. {e}");
            }
        }
        public async Task AddDateOwnerPets(MySqlConnection mySqlConnection, IDataSave.Pets pets, IDataSave.OwnerPets OwnerPets)
        {
            MySqlTransaction mst = null;
            object Petid;
            string queryOwner = @"
            INSERT INTO owner_pets (First_Name, Second_Name, Middle_Name, Number_Phone, Email, City, Street, id_pets)
            VALUES (@firstName, @secondName, @middleName, @numberPhone, @email, @city, @address, @petID_Owner);";

            string queryPet = @"
            INSERT INTO pets (Name_Pets, Species, Breed, Color, Sex, Mark_Animal)
            VALUES (@petName, @species, @breed, @color, @sex, @mark);";
            string MaxPetsID = "SELECT MAX(id_pets) FROM pets ORDER BY id_pets DESC LIMIT 1;";
            try
            {
                mst = await mySqlConnection.BeginTransactionAsync();
                using (MySqlCommand commandPet = new MySqlCommand(queryPet, mySqlConnection))
                {
                    commandPet.Parameters.AddWithValue("@petName", pets.petName);
                    commandPet.Parameters.AddWithValue("@species", pets.species);
                    commandPet.Parameters.AddWithValue("@breed", pets.breed);
                    commandPet.Parameters.AddWithValue("@color", pets.color);
                    commandPet.Parameters.AddWithValue("@sex", pets.sex);
                    commandPet.Parameters.AddWithValue("@mark", pets.mark);

                    await commandPet.ExecuteNonQueryAsync();

                }
                using (MySqlCommand command = new MySqlCommand(MaxPetsID, mySqlConnection))
                {
                    Petid = await command.ExecuteScalarAsync();
                }
                using (MySqlCommand commandOwner = new MySqlCommand(queryOwner, mySqlConnection))
                {
                    commandOwner.Parameters.AddWithValue("@firstName", OwnerPets.firstName);
                    commandOwner.Parameters.AddWithValue("@secondName", OwnerPets.secondName);
                    commandOwner.Parameters.AddWithValue("@middleName", OwnerPets.middleName);
                    commandOwner.Parameters.AddWithValue("@numberPhone", OwnerPets.numberPhone);
                    commandOwner.Parameters.AddWithValue("@email", OwnerPets.email);
                    commandOwner.Parameters.AddWithValue("@city", OwnerPets.city);
                    commandOwner.Parameters.AddWithValue("@address", OwnerPets.address);
                    commandOwner.Parameters.AddWithValue("@petID_Owner", Petid);

                    await commandOwner.ExecuteNonQueryAsync();
                        
                }
                MessageBox.Show("Данные успешно добавлены.");
                await mst.CommitAsync();
                
            }
            catch (Exception ex)
            {
                await mst.RollbackAsync();
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
        }
        public async Task<IDataSave.OwnerPets> GetStructureTableOwnerPets(MySqlConnection mySqlConnection)
        {
            string qwerty = @"SELECT owner_pets.First_Name, owner_pets.Second_Name, owner_pets.Middle_Name, owner_pets.Number_Phone, owner_pets.Email, owner_pets.City, owner_pets.Street 
                FROM owner_pets WHERE owner_pets.id_Owner = @idOwner";
            Nullable<OwnerPets> ownerPets = null;
            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(qwerty, mySqlConnection))
                {
                    if (!string.IsNullOrEmpty(IDataSave.idStr) && int.TryParse(IDataSave.idStr, out int idOwnerValue))
                    {
                        mySqlCommand.Parameters.Add("@idOwner", MySqlDbType.Int32).Value = idOwnerValue;

                        using (var mySqlDataReader = await mySqlCommand.ExecuteReaderAsync())
                        {
                            if (await mySqlDataReader.ReadAsync())
                            {
                                OwnerPets tempOwnerPets = new OwnerPets();

                                tempOwnerPets.firstName = mySqlDataReader["First_Name"] != DBNull.Value ? mySqlDataReader["First_Name"].ToString() : null;
                                tempOwnerPets.secondName = mySqlDataReader["Second_Name"] != DBNull.Value ? mySqlDataReader["Second_Name"].ToString() : null;
                                tempOwnerPets.middleName = mySqlDataReader["Middle_Name"] != DBNull.Value ? mySqlDataReader["Middle_Name"].ToString() : null;
                                tempOwnerPets.numberPhone = mySqlDataReader["Number_Phone"] != DBNull.Value ? mySqlDataReader["Number_Phone"].ToString() : null;
                                tempOwnerPets.email = mySqlDataReader["Email"] != DBNull.Value ? mySqlDataReader["Email"].ToString() : null;
                                tempOwnerPets.city = mySqlDataReader["City"] != DBNull.Value ? mySqlDataReader["City"].ToString() : null;
                                tempOwnerPets.address = mySqlDataReader["Street"] != DBNull.Value ? mySqlDataReader["Street"].ToString() : null;

                                ownerPets = tempOwnerPets;
                            }
                        }
                    }
                    else
                    {
                        // Обработка случая, когда IDataSave.idStr пустое или не является числом
                        MessageBox.Show("Некорректное значение");
                    }
                    return ownerPets.HasValue ? ownerPets.Value : new OwnerPets();

                }



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не предвиденная ошибка. {ex}");
            }
            return ownerPets.HasValue ? ownerPets.Value : new OwnerPets();
        }
        public async Task<IDataSave.Pets> GetStructureTablePets(MySqlConnection mySqlConnection)
        {
            Nullable<Pets> pets = null;
            string qwerty = @"SELECT pets.Name_Pets, pets.Species, pets.Breed, pets.Sex, pets.Color, pets.Mark_Animal 
                FROM pets JOIN owner_pets ON pets.Id_Pets = owner_pets.id_Pets WHERE owner_pets.id_Pets = @idOwner";
            try
            {
                using(MySqlCommand mySQLCommand = new MySqlCommand(qwerty, mySqlConnection))
                {
                    if(!string.IsNullOrEmpty(IDataSave.idStr) && int.TryParse(IDataSave.idStr, out int idOwnerPets))
                    {
                        mySQLCommand.Parameters.Add("@idOwner", MySqlDbType.Int32).Value = idOwnerPets;
                        using(var mySQLDataReader = await mySQLCommand.ExecuteReaderAsync())
                        {
                            if (await mySQLDataReader.ReadAsync())
                            {
                                IDataSave.Pets tempPets = new Pets();
                                tempPets.petName = mySQLDataReader["Name_Pets"] != DBNull.Value ? mySQLDataReader["Name_Pets"].ToString() : null;
                                tempPets.species = mySQLDataReader["Species"] != DBNull.Value ? mySQLDataReader["Species"].ToString() : null;
                                tempPets.breed = mySQLDataReader["Breed"] != DBNull.Value ? mySQLDataReader["Breed"].ToString() : null;
                                tempPets.color = mySQLDataReader["Color"] != DBNull.Value ? mySQLDataReader["Color"].ToString() : null;
                                tempPets.sex = mySQLDataReader["Sex"] != DBNull.Value ? mySQLDataReader["Sex"].ToString() : null;
                                tempPets.mark = mySQLDataReader["Mark_Animal"] != DBNull.Value ? mySQLDataReader["Mark_Animal"].ToString() : null;
                                pets = tempPets;
                            }    
                        }
                    }
                    else
                    {
                        MessageBox.Show("Некорректное значение");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не предвиденная ошибка. {ex}");
            }
            return pets.HasValue ? pets.Value : new Pets();
        }
    }
}
