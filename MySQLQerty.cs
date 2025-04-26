using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp4.IDataSave;

namespace WindowsFormsApp4
{
    internal class MySQLQerty
    {
        public async Task GetDiagnosisName(MySqlConnection mySqlConnection) 
        {
            string qwerty = "SELECT NameDiagnosis FROM Diagnosis";
            using (MySqlCommand mySqlCommand = new MySqlCommand(qwerty, mySqlConnection))
            {
                string sqlQuery = "SELECT NameDiagnosis FROM Diagnosis";

                List<string> diagnosisList = new List<string>(); 

                try
                {
                    using (MySqlCommand mysqlCommand = new MySqlCommand(sqlQuery, mySqlConnection))
                    {
                        using (MySqlDataReader mysqlread = mysqlCommand.ExecuteReader())
                        {
                            while (await mysqlread.ReadAsync())
                            {
                                string diagnosisName = mysqlread.GetString("NameDiagnosis");
                                diagnosisList.Add(diagnosisName);
                            }
                        }
                    }

                    IDataSave.NameDiagnosis = diagnosisList.ToArray(); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при получении названий диагнозов: " + ex.Message);
                    // Обработка ошибки
                }
            }
        }

        public async Task<bool> AccessLoginAndPassword(string login, string password, MySqlConnection mySqlConnection) //Проверка логина и пароля
        {
            string qwerty = "SELECT Password FROM employees WHERE Login = @login";
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
        public async Task<int> get_EmployyesAccess(MySqlConnection mySqlConnection) //Получения уровня досутпа
        {

            string qwerty = "Select Level_Access FROM employees WHERE Id_Employees = @idEmployees";
            using (MySqlCommand mysqlc = new MySqlCommand(qwerty, mySqlConnection))
            {
                try
                {
                    mysqlc.Parameters.AddWithValue("@idEmployees", IDataSave.idEmployees);
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
                catch (MySqlException e)
                {
                    MessageBox.Show($"Не предвиденная ошибка. {e}");
                    return -1;
                }
            }
        }
        public async Task<int> get_IdEmployyes(string login, MySqlConnection mySqlConnection) //Получение id работника
        {
            try
            {
                string qwerty = "SELECT Id_Employees FROM employees WHERE Login = @login";
                using (MySqlCommand mysqlc = new MySqlCommand(qwerty, mySqlConnection))
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
            }
            catch (MySqlException e)
            {
                MessageBox.Show($"Не предвиденная ошибка. {e}");
                return -1;
            }
        }
        public async Task ShowDataGrid_Visists(MySqlConnection mySqlConnection, DataGridView dataGridView) //Показывает визиты у опредленного врача
        {
            string query = @"
                SELECT 
                v.Id_Visits AS 'Идентификатор визита', 
                p.Name_Pets AS 'Имя животного', 
                p.Breed AS 'Порода животного', 
                s.NameSpecies AS 'Вид животного', 
                v.Data_Visits AS 'Дата визита', 
                v.Time_Visits AS 'Время визита', 
                ser.Service AS 'Тип услуги',
                v.Serviced AS 'Оказано'
                FROM visits AS v 
                JOIN pets AS p ON v.Id_Pet = p.Id_pets 
                JOIN species AS s ON p.Species_Id = s.IdAnimal 
                JOIN service AS ser ON v.Id_Service = ser.Id_Service 
                WHERE v.Id_Doctor = @employeeId;";

            using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                try
                {
                    mySqlCommand.Parameters.Add("@employeeId", MySqlDbType.Int32).Value = IDataSave.idEmployees;
                    using (MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(mySqlCommand))
                    {
                        myDataAdapter.SelectCommand = mySqlCommand;
                        myDataAdapter.SelectCommand.Connection = mySqlConnection;
                        DataTable DT = new DataTable();
                        await myDataAdapter.FillAsync(DT);
                        dataGridView.DataSource = DT;
                        for(int i = 0; i < DT.Rows.Count; i++)
                        {
                            dataGridView.Rows[i].Tag = DT.Rows[i]["Идентификатор визита"];
                        }
                        dataGridView.Columns.Remove("Идентификатор визита");
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
        public async Task ShowDataGrisView_TypeService(MySqlConnection mySqlConnection, DataGridView dataGridView)
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
        public async Task ShowDataGrisView_AllVisits(MySqlConnection mySqlConnection, DataGridView dataGridView) //Показывает ВСЕ ВИЗИТЫ
        {
            string query = @"
                SELECT 
                v.Id_Visits AS 'Визит_ID', 
                p.Name_Pets AS 'Имя животного', 
                p.Breed AS 'Порода животного', 
                s.NameSpecies AS 'Вид животного', 
                v.Data_Visits AS 'Дата визита', 
                v.Time_Visits AS 'Время визита', 
                ser.Service AS 'Тип услуги',
                v.Serviced AS 'Оказано'
                FROM visits AS v 
                JOIN pets AS p ON v.Id_Pet = p.Id_pets 
                JOIN species AS s ON p.Species_Id = s.IdAnimal 
                JOIN service AS ser ON v.Id_Service = ser.Id_Service;"
            ;
            using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                try
                {
                    using (MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(mySqlCommand))
                    {
                        DataTable DT = new DataTable();
                        await myDataAdapter.FillAsync(DT);
                        dataGridView.DataSource = DT;
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            dataGridView.Rows[i].Tag = DT.Rows[i]["Визит_ID"];
                        }
                        dataGridView.Columns.Remove("Визит_ID");
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
        public async Task SureDataServised(MySqlConnection mySqlConnection, object Id) //Исправить
        {
            string query = "UPDATE visits SET Serviced = @Serviscd Where Id_Visits = @idVisits;";
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, mySqlConnection))
                {
                    int tiny = 1;
                    cmd.Parameters.Add("@idVisits", MySqlDbType.Int32).Value = Convert.ToInt32(Id);
                    cmd.Parameters.Add("@Serviscd", MySqlDbType.Int32).Value = tiny;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show($"Не предвиденная ошибка. {e}");
            }
        }
        public async void ShowDataGrid_DiagnosisPet(MySqlConnection mySqlConnection, object Id, DataGridView dataGridView)
        {
            try
            {

                string query = @"
                    SELECT
                        d.NameDiagnosis,
                        d.Description,
                        dp.DateGetDiagnosis
                    FROM
                        DiagnosisPets dp
                    JOIN
                        Diagnosis d ON dp.IdDiagnosis = d.IdDiagnosis
                    JOIN
                        Pets p ON dp.IdPets = p.Id_Pets
                    JOIN
                        Visits v ON p.Id_Pets = v.Id_Pet
                    WHERE
                        v.Id_Visits = @VisitID;";

                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@VisitID", Id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dataGridView.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        public async void ShowDataGrisViewOwnerPets(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = @"
                   SELECT
                      op.Id_Owner AS Номер клиента,
                      CONCAT_WS(' ', op.FirstName, op.SecondName, op.MiddleName) AS Полное имя,
                      CONCAT_WS(', ', op.City, op.Street) AS Адрес,
                      op.Number_Phone AS Номер телефона,
                      p.Name_Pets AS Имя питомца,
                      s.NameSpecies AS Вид,
                      p.Breed AS Порода,
                      p.Color AS Цвет
                    FROM
                        owner_pets op
                    JOIN
                        pets p ON op.Id_pets = p.Id_Pets
                    JOIN
                        species s ON p.Species_Id = s.IdAnimal;";
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
            INSERT INTO owner_pets (First_Name, Second_Name, Middle_Name, Number_Phone, Email, idCity, Street, id_pets)
            VALUES (@FirstName, @SecondName, @MiddleName, @Number_Phone, @Email, (SELECT idCity FROM city WHERE NameCity = @City), @Address, @petID_Owner);";

            string queryPet = @"
            INSERT INTO pets (Name_Pets, Species_Id, Breed, Sex, Color, Mark_Animal)
            VALUES (@PetName, (SELECT idAnimal FROM species WHERE NameSpecies = @Species), @Breed, @Color, @Sex, @Mark);
            SELECT LAST_INSERT_ID();";
            try
            {
                mst = await mySqlConnection.BeginTransactionAsync();
                using (MySqlCommand commandPet = new MySqlCommand(queryPet, mySqlConnection))
                {
                    commandPet.Parameters.AddWithValue("@PetName", pets.petName);
                    commandPet.Parameters.AddWithValue("@Species", pets.species);
                    commandPet.Parameters.AddWithValue("@Breed", pets.breed);
                    commandPet.Parameters.AddWithValue("@Color", pets.color);
                    commandPet.Parameters.AddWithValue("@Sex", pets.sex);
                    commandPet.Parameters.AddWithValue("@Mark", pets.mark);

                    Petid = await commandPet.ExecuteScalarAsync();

                }
                using (MySqlCommand command = new MySqlCommand(queryOwner, mySqlConnection))
                {
                    Petid = await command.ExecuteScalarAsync();
                }
                using (MySqlCommand commandOwner = new MySqlCommand(queryOwner, mySqlConnection))
                {
                    commandOwner.Parameters.AddWithValue("@FirstName", OwnerPets.firstName);
                    commandOwner.Parameters.AddWithValue("@SecondName", OwnerPets.secondName);
                    commandOwner.Parameters.AddWithValue("@MiddleName", OwnerPets.middleName);
                    commandOwner.Parameters.AddWithValue("@NumberPhone", OwnerPets.numberPhone);
                    commandOwner.Parameters.AddWithValue("@Email", OwnerPets.email);
                    commandOwner.Parameters.AddWithValue("@City", OwnerPets.city);
                    commandOwner.Parameters.AddWithValue("@Address", OwnerPets.address);
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
            finally
            {
                mst.Dispose();
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
        public async Task AddDiagnosisPet(MySqlConnection mySqlConnection, string diagnosisName, int IdVisits)
        {
            try
            {
                string qwerty = @"INSERT INTO diagnosispets (IdPets, IdDiagnosis, DateGetDiagnosis) 
                            VALUE 
                    ((SELECT pets.Id_pets from pets inner join visits ON visits.Id_Pet = pets.Id_pets where visits.Id_Pet = @IdVisits),     
                    (Select idDiagnosis from diagnosis where NameDiagnosis = @diagnosisName),
                    CURRENT_DATE());";
                using (MySqlCommand mysqlcommand = new MySqlCommand(qwerty, mySqlConnection))
                {
                    mysqlcommand.Parameters.Add("@IdVisits", MySqlDbType.Int32).Value = IdVisits;
                    mysqlcommand.Parameters.Add("@diagnosisName", MySqlDbType.String).Value = diagnosisName;
                    await mysqlcommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не предвиденная ошибка. {ex}");
            }
        }
    }
}
