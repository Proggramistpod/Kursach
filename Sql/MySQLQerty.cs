using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp4.Sql;
using static WindowsFormsApp4.IDataSave;

namespace WindowsFormsApp4
{
    internal class MySQLQerty
    {
        ForInterfaceSQL InterfaceSQL = new ForInterfaceSQL();
        public async Task GetListName(MySqlConnection mySqlConnection)
        {
            string qwerty = "SELECT NameDiagnosis FROM Diagnosis";
            using (MySqlCommand mySqlCommand = new MySqlCommand(qwerty, mySqlConnection))
            {
                string sqlQueryDiagnosis = "SELECT NameDiagnosis FROM Diagnosis";
                string sqlQeryCity = "SELECT NameCity from city;";
                string sqlQerySpecies = "SELECT NameSpecies from species;";
                string sqlQeuryJobTitle = "SELECT JobTitle from jobtitle";
                string sqlQeuryService = "SELECT Service from service";
                IDataSave.NameDiagnosis = await InterfaceSQL.ArrayFromDB(sqlQueryDiagnosis, "NameDiagnosis", mySqlConnection);
                IDataSave.NameSpecies = await InterfaceSQL.ArrayFromDB(sqlQerySpecies, "NameSpecies", mySqlConnection);
                IDataSave.NameСity = await InterfaceSQL.ArrayFromDB(sqlQeryCity, "NameCity", mySqlConnection);
                IDataSave.NameJobTitle = await InterfaceSQL.ArrayFromDB(sqlQeuryJobTitle, "JobTitle", mySqlConnection);
                IDataSave.NameService = await InterfaceSQL.ArrayFromDB(sqlQeuryService, "Service", mySqlConnection);
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

            string qwerty = @"SELECT jt.LvLaccess
                            FROM employees AS e
                            JOIN jobtitle AS jt ON e.IdJobTitle = jt.IdJobTitle
                            WHERE e.Id_Employees = @EmployeeId;";
            using (MySqlCommand mysqlc = new MySqlCommand(qwerty, mySqlConnection))
            {
                try
                {
                    mysqlc.Parameters.AddWithValue("@EmployeeId", IDataSave.idEmployees);
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

        public async Task ShowDataGrisView_TypeService(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = "SELECT Id_Service, Service AS 'Услуга', Price AS 'Цена', Description AS 'Описание' FROM service";
            await InterfaceSQL.ShowDataGridView(mySqlConnection, dataGridView, query, "Id_Service");

        }
        public async Task ShowDataGrisView_AllVisits(MySqlConnection mySqlConnection, DataGridView dataGridView) //Показывает ВСЕ ВИЗИТЫ
        {
            string query = @"
                        SELECT
                            v.Id_Visits AS 'Id_Visits', 
                            p.Name_Pets AS 'Имя животного',
                            p.Breed AS 'Порода животного',
                            s.NameSpecies AS 'Вид животного',
                            v.Data_Visits AS 'Дата визита',
                            ser.Service AS 'Тип услуги',
                            v.Serviced AS 'Оказано'
                        FROM visits AS v
                        JOIN pets AS p ON v.Id_Pet = p.Id_pets
                        JOIN species AS s ON p.Species_Id = s.IdAnimal
                        JOIN service AS ser ON v.Id_Service = ser.Id_Service;";

            using (MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection))
            {
                try
                {
                    using (MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(mySqlCommand))
                    {
                        myDataAdapter.SelectCommand = mySqlCommand;
                        myDataAdapter.SelectCommand.Connection = mySqlConnection;
                        DataTable DT = new DataTable();
                        await myDataAdapter.FillAsync(DT);
                        dataGridView.DataSource = DT;

                        // Сохраняем ID в Tag строки
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            object idValue = DT.Rows[i]["Id_Visits"];

                            if (idValue != DBNull.Value)
                            {
                                dataGridView.Rows[i].Tag = Convert.ToInt32(idValue);
                            }
                            else
                            {
                                dataGridView.Rows[i].Tag = null;
                            }
                        }
                        if (dataGridView.Columns.Contains("Id_Visits"))
                        {
                            dataGridView.Columns["Id_Visits"].Visible = false;
                        }


                        foreach (DataGridViewColumn column in dataGridView.Columns)
                        {
                            column.DefaultCellStyle.NullValue = "-";
                        }
                    }
                }
                catch (MySqlException e)
                {
                    MessageBox.Show($"Непредвиденная ошибка. {e}");
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


        public async Task AddDate_OwnerPets(MySqlConnection mySqlConnection, IDataSave.Pets pets, IDataSave.OwnerPets OwnerPets) //ПЕРЕПИШИ
        {
            MySqlTransaction mst = null;
            object Petid;
            string queryOwner = @"
            INSERT INTO owner_pets (First_Name, Second_Name, Middle_Name, Number_Phone, Email, idCity, Street, id_pets)
            VALUES (@FirstName, @SecondName, @MiddleName, @NumberPhone, @Email, (SELECT idCity FROM city WHERE NameCity = @City), @Address, @petID_Owner);";

            string queryPet = @"
            INSERT INTO pets (Name_Pets, Species_Id, Breed, Color, Sex, Mark_Animal)
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
            catch (MySqlException ex)
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
            string qwerty = @"SELECT owner_pets.First_Name, owner_pets.Second_Name, owner_pets.Middle_Name, owner_pets.Number_Phone, owner_pets.Email, city.NameCity, owner_pets.Street 
                FROM owner_pets JOIN city ON city.idCity = owner_pets.IdCity WHERE owner_pets.id_Owner = @idOwner";
            Nullable<OwnerPets> ownerPets = null;
            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(qwerty, mySqlConnection))
                {
                    if (!string.IsNullOrEmpty(IDataSave.idStr.ToString()) && int.TryParse(IDataSave.idStr.ToString(), out int idOwnerValue))
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
                                tempOwnerPets.city = mySqlDataReader["NameCity"] != DBNull.Value ? mySqlDataReader["NameCity"].ToString() : null;
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
            string query = @"SELECT pets.Name_Pets, species.NameSpecies, pets.Breed, pets.Sex, pets.Color, pets.Mark_Animal 
                FROM pets JOIN owner_pets ON pets.Id_Pets = owner_pets.id_Pets JOIN species ON species.IdAnimal = pets.Species_Id  WHERE owner_pets.id_Pets = @idOwner";
            try
            {
                using (MySqlCommand mySQLCommand = new MySqlCommand(query, mySqlConnection))
                {
                    if (!string.IsNullOrEmpty(IDataSave.idStr.ToString()) && int.TryParse(IDataSave.idStr.ToString(), out int idOwnerPets))
                    {
                        mySQLCommand.Parameters.Add("@idOwner", MySqlDbType.Int32).Value = idOwnerPets;
                        using (var mySQLDataReader = await mySQLCommand.ExecuteReaderAsync())
                        {
                            if (await mySQLDataReader.ReadAsync())
                            {
                                IDataSave.Pets tempPets = new Pets();
                                tempPets.petName = mySQLDataReader["Name_Pets"] != DBNull.Value ? mySQLDataReader["Name_Pets"].ToString() : null;
                                tempPets.species = mySQLDataReader["NameSpecies"] != DBNull.Value ? mySQLDataReader["NameSpecies"].ToString() : null;
                                tempPets.breed = mySQLDataReader["Breed"] != DBNull.Value ? mySQLDataReader["Breed"].ToString() : null;
                                tempPets.color = mySQLDataReader["Color"] != DBNull.Value ? mySQLDataReader["Color"].ToString() : null;
                                tempPets.sex = Convert.ToInt32(mySQLDataReader["Sex"] != DBNull.Value ? mySQLDataReader["Sex"] : null);
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
        public async Task AddDiagnosisPet(MySqlConnection mySqlConnection, string diagnosisName, int IdVisits, DateTime dateDiagnosis)
        {
            try
            {
                string query = @"INSERT INTO diagnosispets (IdPets, IdDiagnosis, DateGetDiagnosis)
                                VALUE (
                                    (SELECT v.Id_Pet FROM visits v WHERE v.Id_Visits = @IdVisits),
                                    (SELECT idDiagnosis FROM diagnosis WHERE NameDiagnosis = @diagnosisName),
                                    @DateDiag
                                );";
                using (MySqlCommand mysqlcommand = new MySqlCommand(query, mySqlConnection))
                {
                    mysqlcommand.Parameters.Add("@IdVisits", MySqlDbType.Int32).Value = IdVisits;
                    mysqlcommand.Parameters.Add("@diagnosisName", MySqlDbType.String).Value = diagnosisName;
                    mysqlcommand.Parameters.Add("@DateDiag", MySqlDbType.Date).Value = dateDiagnosis.Date;
                    await mysqlcommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не предвиденная ошибка. {ex}");
            }
        }
        public async Task AddEmployes(IDataSave.Employees employees, MySqlConnection conn)
        {
            string query = @"
                INSERT INTO Employees (
                    First_Name, Second_Name, Middle_Name, IdJobTitle,
                    Number_Phone, Email, Serias_Passport, Number_Passport,
                    Passport_Issued, Date_Pasport_Issued, Unit_Code_Passport,
                    Login, Password, Word_Access, IdCity, Address
                )
                VALUES (
                    @FirstName, @SecondName, @MiddleName,
                    (SELECT IdJobTitle FROM jobtitle WHERE JobTitle = @JobTitle),
                    @NumberPhone, @Email, @SeriesPassport, @NumberPassport,
                    @PassportIssued, @DataPassportIssued, @UnicodePassport,
                    @Login, @Password, @WordAccess,
                    (SELECT idCity FROM city WHERE NameCity = @City), @Address
                );
";

            var parameters = new Dictionary<string, object>
    {
        { "@FirstName", employees.FirstName },
        { "@SecondName", employees.SecondName },
        { "@MiddleName", employees.MiddleName },
        { "@JobTitle", employees.JobTitle },
        { "@NumberPhone", employees.NumberPhone },
        { "@Email", employees.Email },
        { "@SeriesPassport", employees.SeriesPassport },
        { "@NumberPassport", employees.NumberPassport },
        { "@PassportIssued", employees.PassportIssued },
        { "@DataPassportIssued", employees.DatePassportIssued },  // Убрали .Date
        { "@UnicodePassport", employees.UnitCodePassport },
        { "@Login", employees.Login },
        { "@Password", employees.Password },
        { "@WordAccess", employees.WordAccess },
        { "@City", employees.City },
        { "@Address", employees.Address }
    };
           await ForInterfaceSQL.ExecuteNonQueryAsync(query, conn, parameters);
        }
        public async Task ShowDataGridView_Employees(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = @"SELECT
                                e.Id_Employees,
                                CONCAT_WS(' ', e.First_Name, e.Second_Name, e.Middle_Name) AS 'ФИО',
                                j.JobTitle AS 'Должность',
                                e.Number_Phone AS 'Номер телефона',
                                e.Email AS 'Электронная почта'
                            FROM
                                employees e
                            JOIN
                                jobtitle j ON j.IdJobTitle = e.IdJobTitle";
            await InterfaceSQL.ShowDataGridView(mySqlConnection, dataGridView, query, "Id_Employees");
        }
        public async Task ShowDataGridView_AllDiagnosis(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = @"SELECT
                                IdDiagnosis,
                                NameDiagnosis AS 'Название диагноза',
                                Description AS 'Описание'
                            FROM
                                diagnosis;";
            await InterfaceSQL.ShowDataGridView(mySqlConnection, dataGridView, query, "IdDiagnosis");
        }
        public async Task ShowDataGridView_City(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = @"SELECT
                                idCity,
                                NameCity AS 'Название города'
                            FROM
                                city;";
            await InterfaceSQL.ShowDataGridView(mySqlConnection, dataGridView, query, "idCity");
        }
        public async Task ShowDataGridView_Species(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = @"SELECT
                                IdAnimal,
                                NameSpecies AS 'Название вида'
                            FROM
                                species;";
            await InterfaceSQL.ShowDataGridView(mySqlConnection, dataGridView, query, "IdAnimal");
        }
        public async Task ShowDataGrisView_OwnerPets(MySqlConnection mySqlConnection, DataGridView dataGridView)
        {
            string query = @"
                   SELECT
                       owner_pets.Id_Owner,
                       CONCAT_WS(' ', owner_pets.First_Name, owner_pets.Second_Name, owner_pets.Middle_Name) AS 'Полное имя',
                       CONCAT_WS(' ', (SELECT NameCity FROM city where owner_pets.IdCity = city.idCity), owner_pets.Street) AS 'Адрес',
                       owner_pets.Number_Phone AS 'Номер телефона',
                       pets.Name_Pets AS 'Питомца',
                       (select NameSpecies from species where pets.Species_Id = species.IdAnimal) AS 'Вид',
                       pets.Breed AS 'Порода',
                       pets.Color AS 'Цвет'
                     FROM
                         owner_pets 
                     JOIN
                         pets  ON owner_pets.Id_pets = pets.Id_Pets
                     JOIN
                         species  ON pets.Species_Id = species.IdAnimal;";
            await InterfaceSQL.ShowDataGridView(mySqlConnection, dataGridView, query, "Id_Owner");
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
            await InterfaceSQL.ShowDataGridView_WHEN(mySqlConnection, dataGridView, query, "Идентификатор визита", "@employeeId", Convert.ToInt32(IDataSave.idEmployees));
        }
        public async void ShowDataGrid_DiagnosisPet(MySqlConnection mySqlConnection, object Id, DataGridView dataGridView)
        {
            try
            {

                string query = @"
                    SELECT
                        dp.IdDiagnosis,
                        d.NameDiagnosis AS 'диагноз',
                        d.Description AS 'Описание',
                        dp.DateGetDiagnosis AS 'Дата выдачи'
                    FROM
                        DiagnosisPets dp
                    JOIN
                        Diagnosis d ON dp.IdDiagnosis = d.IdDiagnosis
                    JOIN
                        visits v ON dp.IdPets = v.Id_Pet 
                    WHERE
                        v.Id_Visits = @VisitID;";
                await InterfaceSQL.ShowDataGridView_WHEN(mySqlConnection, dataGridView, query, "IdDiagnosis", "@VisitID", Convert.ToInt32(Id));
            }
            catch
            {

            }
        }
        public async Task ChangeData_OwnerPets(MySqlConnection conn, IDataSave.OwnerPets owner, IDataSave.Pets pet)
        {
            using (MySqlTransaction transaction = await conn.BeginTransactionAsync())
            {
                try
                {
                    // 1. Обновляем владельца (owner_pets)
                    string updateOwnerQuery = @"
                    UPDATE owner_pets 
                    SET 
                        First_Name = @firstName,
                        Second_Name = @secondName,
                        Middle_Name = @middleName,
                        Number_Phone = @phone,
                        Email = @email,
                        IdCity = (SELECT IdCity FROM city WHERE NameCity = @city),
                        Street = @address
                    WHERE id_Owner = @ownerId";

                    using (MySqlCommand cmdOwner = new MySqlCommand(updateOwnerQuery, conn, transaction))
                    {
                        cmdOwner.Parameters.AddWithValue("@ownerId", idStr);
                        cmdOwner.Parameters.AddWithValue("@firstName", owner.firstName);
                        cmdOwner.Parameters.AddWithValue("@secondName", owner.secondName);
                        cmdOwner.Parameters.AddWithValue("@middleName", owner.middleName);
                        cmdOwner.Parameters.AddWithValue("@phone", owner.numberPhone);
                        cmdOwner.Parameters.AddWithValue("@email", owner.email);
                        cmdOwner.Parameters.AddWithValue("@city", owner.city);
                        cmdOwner.Parameters.AddWithValue("@address", owner.address);

                        int ownerRows = await cmdOwner.ExecuteNonQueryAsync();
                        if (ownerRows == 0) throw new Exception("Владелец не найден");
                    }

                    // 2. Обновляем животное (pets)
                    string updatePetQuery = @"
                    UPDATE pets 
                    SET 
                        Name_Pets = @petName,
                        Species_id = (SELECT IdAnimal FROM species WHERE NameSpecies = @species),
                        Breed = @breed,
                        Color = @color,
                        Sex = @sex,
                        Mark_Animal = @mark
                    WHERE Id_pets = (select id_pets from owner_pets where id_Owner = @petId)";

                    using (MySqlCommand cmdPet = new MySqlCommand(updatePetQuery, conn, transaction))
                    {
                        cmdPet.Parameters.AddWithValue("@petId", idStr);
                        cmdPet.Parameters.AddWithValue("@petName", pet.petName);
                        cmdPet.Parameters.AddWithValue("@species", pet.species);
                        cmdPet.Parameters.AddWithValue("@breed", pet.breed);
                        cmdPet.Parameters.AddWithValue("@color", pet.color);
                        cmdPet.Parameters.AddWithValue("@sex", pet.sex);
                        cmdPet.Parameters.AddWithValue("@mark", pet.mark);

                        int petRows = await cmdPet.ExecuteNonQueryAsync();
                        if (petRows == 0) throw new Exception("Животное не найдено");
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }
        public async Task ChangeData_Employees(MySqlConnection mysqlconn, IDataSave.Employees employees)
        {

            string query = @"UPDATE 
                                employees
                            SET
                                First_Name = @FirstName,
                                Second_Name = @SecondName,
                                Middle_Name = @MiddleName,
                                Number_Phone = @NumberPhone,
                                IdJobTitle =  (SELECT IdJobTitle FROM jobtitle WHERE JobTitle = @JobTitle),
                                Email = @Email,
                                Serias_Passport = @SeriesPassport,
                                Number_Passport = @NumberPassport,
                                Passport_Issued = @PassportIssued,
                                Date_Pasport_Issued = @DataPassportIssued,
                                Unit_Code_Passport = @UnicodePassport,
                                Login = @Login,
                                Password = @Password,
                                Word_Access = @WordAccess,
                                IdCity = (SELECT idCity FROM city WHERE NameCity = @City),
                                Address = @Address
                            WHERE
                                Id_Employees = @Id_Employees;"
            ;
                var parameters = new Dictionary<string, object>
                {
                    { "@FirstName", employees.FirstName },
                    { "@SecondName", employees.SecondName },
                    { "@MiddleName", employees.MiddleName },
                    { "@NumberPhone", employees.NumberPhone },
                    { "@JobTitle", employees.JobTitle },
                    { "@Email", employees.Email },
                    { "@SeriesPassport", employees.SeriesPassport },
                    { "@NumberPassport", employees.NumberPassport },
                    { "@PassportIssued", employees.PassportIssued },
                    { "@DataPassportIssued", employees.DatePassportIssued.Date }, 
                    { "@UnicodePassport", employees.UnitCodePassport },
                    { "@Login", employees.Login },
                    { "@Password", employees.Password },
                    { "@WordAccess", employees.WordAccess },
                    { "@City", employees.City },
                    { "@Address", employees.Address },
                    { "@Id_Employees", IDataSave.idStr } // Не забываем Id_Employees
                };
                await ForInterfaceSQL.ExecuteNonQueryAsync(query, mysqlconn, parameters);
        }
        public async Task<IDataSave.Employees> EmployeesSelect(MySqlConnection mysqlconn)
        {
            IDataSave.Employees employees = new IDataSave.Employees();
            string query = @"SELECT
                        e.First_Name,
                        e.Second_Name,
                        e.Middle_Name,
                        j.JobTitle,
                        e.Number_Phone,
                        e.Email,
                        e.Serias_Passport,
                        e.Number_Passport,
                        e.Passport_Issued,
                        e.Date_Pasport_Issued,
                        e.Unit_Code_Passport,
                        e.Login,
                        e.Password,
                        e.Word_Access,
                        c.NameCity,
                        e.Address
                    FROM
                        employees e
                    LEFT JOIN
                        jobtitle j ON e.IdJobTitle = j.IdJobTitle
                    LEFT JOIN
                        city c ON e.IdCity = c.IdCity
                    WHERE
                        e.Id_Employees = @Id_Employees";

            using (MySqlCommand command = new MySqlCommand(query, mysqlconn))
            {
                command.Parameters.AddWithValue("@Id_Employees", IDataSave.idStr);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        employees.FirstName = reader["First_Name"] != DBNull.Value ? reader["First_Name"].ToString() : null;
                        employees.SecondName = reader["Second_Name"] != DBNull.Value ? reader["Second_Name"].ToString() : null;
                        employees.MiddleName = reader["Middle_Name"] != DBNull.Value ? reader["Middle_Name"].ToString() : null;
                        employees.JobTitle = reader["JobTitle"] != DBNull.Value ? reader["JobTitle"].ToString() : null;
                        employees.NumberPhone = reader["Number_Phone"] != DBNull.Value ? reader["Number_Phone"].ToString() : null;
                        employees.Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null;
                        employees.SeriesPassport = reader["Serias_Passport"] != DBNull.Value ? reader["Serias_Passport"].ToString() : null;
                        employees.NumberPassport = reader["Number_Passport"] != DBNull.Value ? reader["Number_Passport"].ToString() : null;
                        employees.PassportIssued = reader["Passport_Issued"] != DBNull.Value ? reader["Passport_Issued"].ToString() : null;
                        employees.DatePassportIssued = reader["Date_Pasport_Issued"] != DBNull.Value ? Convert.ToDateTime(reader["Date_Pasport_Issued"]) : DateTime.Now;
                        employees.UnitCodePassport = reader["Unit_Code_Passport"] != DBNull.Value ? reader["Unit_Code_Passport"].ToString() : null;
                        employees.Login = reader["Login"] != DBNull.Value ? reader["Login"].ToString() : null;
                        employees.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : null;
                        employees.WordAccess = reader["Word_Access"] != DBNull.Value ? reader["Word_Access"].ToString() : null;
                        employees.City = reader["NameCity"] != DBNull.Value ? reader["NameCity"].ToString() : null;
                        employees.Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null;
                    }
                    else
                    {
                        MessageBox.Show("Сотрудник не найден");
                        IDataSave.Employees employeesTeps = new IDataSave.Employees();
                        return employeesTeps;
                    }
                }
            }
            return employees;
        }
        public async Task Add_Visits(IDataSave.Visits v, MySqlConnection connection)
        {
            try
            {
                string query = @"
                INSERT INTO visits (Id_Pet, Id_Doctor, Data_Visits, Id_Service)
                VALUES (@Pet, @Doctor, @Data_Visits, (SELECT Id_Service from service where Service = @service));";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@Pet", v.Pets },
                        { "@Doctor", v.Employees },
                        { "@Data_Visits", v.Date },
                        { "@service", v.Serviced }
                    };
                await ForInterfaceSQL.ExecuteNonQueryAsync(query, connection, parameters);
            }
            catch (MySqlException ex)
            {
                // Log the error, re-throw, or handle as appropriate for your application.
                Console.WriteLine($"Database error: {ex.Message}");
                throw; // Re-throw to propagate the exception up the call stack.
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., null reference, type conversion).
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task Update_Visits(MySqlConnection connection, IDataSave.Visits v)
        {
            string query = @"UPDATE 
                                visits
                            SET
                                Id_Pet = @Pet,
                                Id_Doctor = @Doctor,
                                Data_Visits = @Data_Visits,
                                Id_Service = (SELECT Id_Service FROM service WHERE Service = @service)
                            WHERE Id_Visits = @IdVisits;";
            var parameters = new Dictionary<string, object>
            {
                { "@Pet", v.Pets },
                { "@Doctor", v.Employees },
                { "@Data_Visits", v.Date }, // Убрали .Date, чтобы сохранить и время
                { "@service", v.Serviced },
                { "@IdVisits", IDataSave.idStr }
            };
            await ForInterfaceSQL.ExecuteNonQueryAsync(query, connection, parameters);

        }
        public async Task<IDataSave.Visits> Select_Visits(MySqlConnection connection)
        {
            IDataSave.Visits v;
            string query = @"SELECT
                                visits.Id_Pet,
                                visits.Id_Doctor,
                                visits.Data_Visits,
                                service.Service
                            FROM visits
                            INNER JOIN service
                            ON visits.Id_Service = service.Id_Service
							WHERE visits.Id_Service = @visits;
            ";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@visits", idStr);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        v.Pets = Convert.ToInt32(reader["Id_Pet"] != DBNull.Value ? reader["Id_Pet"] : null);
                        v.Employees = Convert.ToInt32(reader["Id_Doctor"] != DBNull.Value ? reader["Id_Doctor"] : null);
                        v.Date = Convert.ToDateTime(reader["Data_Visits"] != DBNull.Value ? reader["Data_Visits"] : DateTime.Now);
                        v.Serviced = reader["Service"] != DBNull.Value ? reader["Service"].ToString() : null;
                        return v;
                    }
                }
            }
            IDataSave.Visits a = new IDataSave.Visits
            {

            };
            return a;
        }
        public async Task<IDataSave.Service> Select_Service(MySqlConnection connection)
        {
            IDataSave.Service S = new IDataSave.Service { };
            string query = @"SELECT
	                            Service,
	                            Price,
	                            Description
                            FROM
	                            service
                            WHERE
	                            Id_Service = @ServiceId;"
            ;
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ServiceId", idStr);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            S.Name = reader["Service"].ToString();
                            S.Description = reader["Description"].ToString();
                            S.Price = Convert.ToInt32(reader["Price"], CultureInfo.InvariantCulture);
                            return S;
                        }
                        else
                        {
                            return S;
                        }
                    }
                }
            }
            catch (MySqlException ex) 
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                IDataSave.Service a = new IDataSave.Service
                {

                };
                return a;
            } 
        }
        public async Task Update_Service(MySqlConnection connection, IDataSave.Service Service)
        {
            string query = @"UPDATE service
                            SET
                                Service = @Service,
                                Price = @Price,
                                Description = @Description
                            WHERE
                                Id_Service = @ServiceId;";
            var parameters = new Dictionary<string, object>
            {
                { "@ServiceId", IDataSave.idStr },
                { "@Service", Service.Name },
                { "@Price", Service.Price },
                { "@Description", Service.Description }
            };

            try
            {
                await ForInterfaceSQL.ExecuteNonQueryAsync(query, connection, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }
        public async Task Add_Service(MySqlConnection connection, IDataSave.Service Service)
        {
            string query = @"INSERT INTO service (Service, Price, Description)
                            VALUES (@service, @price, @description);";
            var parameters = new Dictionary<string, object>
            {
                { "@service", Service.Name },
                { "@price", Service.Price },
                { "@description", Service.Description }
            };

            try
            {
                await ForInterfaceSQL.ExecuteNonQueryAsync(query, connection, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        public async Task<String> Select_City(MySqlConnection connection)
        {
            string query = @"SELECT
	                            NameCity
                            FROM
	                            city
                            WHERE
	                            idCity = @Id;"
           ;
            return await InterfaceSQL.ListTable(query, connection, "NameCity");
        }
        public async Task Update_City(MySqlConnection connection, string City)
        {
            string query = @"UPDATE city
                             SET
	                            NameCity = @Name
                            WHERE
	                            idCity = @Id;"
           ;
            try
            {

                var parameters = new Dictionary<string, object>
                {
                    { "@Id", IDataSave.idStr },
                    { "@Name", City }
                };
                await ForInterfaceSQL.ExecuteNonQueryAsync(query, connection, parameters);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        public async Task Add_City(MySqlConnection connection, string City)
        {
            string query = @"INSERT INTO city (NameCity) VALUES (@Name)"
           ;
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", City);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        public async Task<string> Select_Species(MySqlConnection connection)
        {
            string query = @"SELECT NameSpecies FROM species WHERE IdAnimal = @Id;";
            return await InterfaceSQL.ListTable(query, connection, "NameSpecies");
        }
        public async Task Update_Species(MySqlConnection connection, string Species)
        {
            string query = @"UPDATE species
                             SET
	                            NameSpecies = @Name
                            WHERE
	                            IdAnimal = @Id;"
           ;
            try
            {
                    var parameters = new Dictionary<string, object>
                {
                    { "@Id", IDataSave.idStr },
                    { "@Name", Species }
                };
                    await ForInterfaceSQL.ExecuteNonQueryAsync(query, connection, parameters);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        public async Task Add_Species(MySqlConnection connection, string Species)
        {
            string query = @"INSERT INTO species (NameSpecies) VALUES (@Species)";
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Species", Species);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        public async Task ShowDataGrid_JobTitle(MySqlConnection connection, DataGridView dataGridView)
        {
            string query = @"SELECT 
                                IdJobTitle,
                                JobTitle AS 'Должность'
                            FROM
                                jobtitle";
            await InterfaceSQL.ShowDataGridView(connection, dataGridView, query, "IdJobTitle");
        }
        public async Task<string> Select_JobTitle(MySqlConnection connection)
        {
            string query = @"SELECT 
                                JobTitle
                            FROM
                                jobtitle";
            return await InterfaceSQL.ListTable(query, connection, "JobTitle");
        }
        public async Task Update_JobTitle(MySqlConnection connection, string job) 
        {
            string query = @"UPDATE jobtitle
                             SET
	                            JobTitle = @Name
                            WHERE
	                            IdJobTitle = @Id;"
           ;
            try
            {
                    var parameters = new Dictionary<string, object>
                {
                    { "@Id", IDataSave.idStr },
                    { "@Name", job }
                };
                    await ForInterfaceSQL.ExecuteNonQueryAsync(query, connection, parameters);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        public async Task Add_JobTitle(MySqlConnection connection, string job) 
        {
            string query = @"INSERT INTO jobtitle (JobTitle) VALUES (@Job)";
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Job", job);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        public async Task<IDataSave.Descrip> Select_Diagnosis(MySqlConnection connection)
        {
            IDataSave.Descrip descrip = new IDataSave.Descrip();
            string query = @"SELECT NameDiagnosis, Description FROM diagnosis WHERE idDiagnosis = @Id;";
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", IDataSave.idStr);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            descrip.Name = reader["NameDiagnosis"].ToString();
                            descrip.Description = reader["Description"].ToString();
                            return descrip;
                        }
                        return descrip;
                    }
                }
            }
            catch (MySqlException ex)
            {
                IDataSave.Descrip descript = new IDataSave.Descrip();
                MessageBox.Show($"Ошибка: {ex.Message}");
                return descript;
            }
        }
        public async Task Add_Diagnosis(MySqlConnection connection, IDataSave.Descrip descrip)
        {
            string query = @"INSERT INTO diagnosis (NameDiagnosis, Description) VALUE (@Name, @Description);";
            var parameters = new Dictionary<string, object>
            {
                { "@Name", descrip.Name },
                { "@Description", descrip.Description }
            };

            try
            {
                await ForInterfaceSQL.ExecuteNonQueryAsync(query, connection, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        public async Task Update_Diagnosis(MySqlConnection connection, IDataSave.Descrip descrip)
        {
            string query = @"UPDETE diagnosis SET NameDiagnosis = @Name, Description = @Description) WHERE idDiagnosis = @Id;";
            var parameters = new Dictionary<string, object>
            {
                { "@Name", descrip.Name },
                { "@Description", descrip.Description },
                { "@Id", IDataSave.idStr }
            };

            try
            {
                await ForInterfaceSQL.ExecuteNonQueryAsync(query, connection, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        public async Task DeletTable(string NameTable, MySqlConnection mySqlConnection, string TableId)
        {
            await ForInterfaceSQL.DeleteRecordAsync(mySqlConnection, NameTable, TableId, IDataSave.idStr.ToString());
        }
        public async Task<bool> DeleteOwnerAndPetsAsync(MySqlConnection connection)
        {
            string ownerId = idStr.ToString();
            // Проверяем входные данные
            if (string.IsNullOrEmpty(ownerId))
            {
                MessageBox.Show("ID владельца не может быть пустым.");
                return false;
            }

            try
            {
                // Начинаем транзакцию
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Удаляем животных, принадлежащих владельцу
                        await DeletePetsByOwnerAsync(connection, transaction, ownerId);

                        // 2. Удаляем владельца
                        await ForInterfaceSQL.DeleteRecordAsync(connection, "owner_pets", "id_Owner", ownerId); // Используем уже существующую функцию


                        // Если все прошло успешно, подтверждаем транзакцию
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Если произошла ошибка, откатываем транзакцию
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при удалении владельца и питомцев: {ex.Message}");
                        return false;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
                return false;
            }
        }


        private static async Task DeletePetsByOwnerAsync(MySqlConnection connection, MySqlTransaction transaction, string ownerId)
        {
            string query = "DELETE FROM pets WHERE Id_pets IN (SELECT id_pets FROM owner_pets WHERE id_Owner = @ownerId);";
            using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@ownerId", ownerId);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
