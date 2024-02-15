using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace Konvert
{
    public static class Inventory
    {
        static readonly string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB.mdf");
        public static readonly string connectionString = $"Data Source = (LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=" + path + "; Integrated Security=True;Connect Timeout=30";
        public static readonly SqlConnection sqlConnection = new("Data Source = (LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=" + path + "; Integrated Security=True;Connect Timeout=30");/// Connection String
        public static string sqlRequest; /// Строка SqlCommand для операций с БД
        public static int counterDB, btnclick; /// Переменная для Id и Index БД
        public static List<int> idFromDB = new();

        public static void DBOpen()
        {
            sqlConnection.Open();
        }

        public static void DBClose()
        {
            sqlConnection.Close();
        }
        ///
        /// Получение количества записей в БД
        ///
        public static void MaxId()
        {
            SqlCommand command = new("SELECT COUNT(Id) FROM Recipient", sqlConnection);
            DBOpen();
            counterDB = (int)command.ExecuteScalar();
            DBClose();
        }
        ///
        /// Добавление записи в таблицу БД
        /// 
        public static void AddInTable()
        {
            using SqlCommand command = new("INSERT INTO Recipient " +
                " VALUES (@Firm, @Index, @Region, @Area, @City, @Street, @Home, @Frame, @Structure, @Flat)", sqlConnection);
            DBOpen(); /// Открываем базу данных для добавление данных
                      ///
                      /// Привязка данных к колонкам таблицы
                      /// 
            command.Parameters.Add(new SqlParameter("@Firm", Variables.Firm));
            command.Parameters.Add(new SqlParameter("@Index", Variables.Index));
            command.Parameters.Add(new SqlParameter("@Region", Variables.Region));
            command.Parameters.Add(new SqlParameter("@Area", Variables.Area));
            command.Parameters.Add(new SqlParameter("@City", Variables.City));
            command.Parameters.Add(new SqlParameter("@Street", Variables.Street));
            command.Parameters.Add(new SqlParameter("@Home", Variables.Home));
            command.Parameters.Add(new SqlParameter("@Frame", Variables.Frame));
            command.Parameters.Add(new SqlParameter("@Structure", Variables.Structure));
            command.Parameters.Add(new SqlParameter("@Flat", Variables.Flat));
            command.ExecuteNonQuery();/// Внесение данных в таблицу
            DBClose();
        }
        ///
        /// Изменение строки в таблице
        ///
        public static void UpdateInTable()
        {
            using SqlCommand command = new("UPDATE Recipient " +
                " SET [Firm] = @Firm, [Index] = @Index, [Region] = @Region, [Area] = @Area, [City] = @City, [Street] = @Street, " +
                "[Home] = @Home, [Frame] = @Frame, [Structure] = @Structure, [Flat] = @Flat " +
                "WHERE Id = '" + Variables.ID + "'", sqlConnection);
            DBOpen(); // Открываем базу данных для добавление данных
            /// Привязка данных к колонкам таблицы

            command.Parameters.AddWithValue("@Firm", Variables.Firm);
            command.Parameters.AddWithValue("@Index", Variables.Index);
            command.Parameters.AddWithValue("@Region", Variables.Region);
            command.Parameters.AddWithValue("@Area", Variables.Area);
            command.Parameters.AddWithValue("@City", Variables.City);
            command.Parameters.AddWithValue("@Street", Variables.Street);
            command.Parameters.AddWithValue("@Home", Variables.Home);
            command.Parameters.AddWithValue("@Frame", Variables.Frame);
            command.Parameters.AddWithValue("@Structure", Variables.Structure);
            command.Parameters.AddWithValue("@Flat", Variables.Flat);
            command.ExecuteNonQuery();// Внесение данных в таблицу
            DBClose(); // закрываем базу данных
        }
        ///
        /// Удаление заданной строки из БД по имени отправителя
        /// 
        public static void DelInTable()
        {
            using SqlCommand command = new("DELETE FROM Recipient WHERE Firm = '" + Variables.Firm + "'", sqlConnection);
            try
            {
                DBOpen();
                _ = command.ExecuteNonQuery();
                DBClose();
            }
            catch
            {
                MessageBox2 messageBox2 = new("Ошибка", "К сожалению, это сейчас не возможно!\nПопрубуйте позже");
                _ = messageBox2.ShowDialog();

            }

        }
        /// 
        /// Поиск в базе данных
        /// 
        public static void FindInTable()
        {
            Console.WriteLine(Variables.Firm);
            SqlCommand command = new(sqlRequest, sqlConnection);
            DBOpen(); // Открываем базу данных
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                reader.Read();
                /// Привязка данных к колонкам таблицы
                /// 
                Variables.ID = Convert.ToInt32(reader[0], CultureInfo.CurrentCulture);
                Variables.Firm = reader[1].ToString();
                Variables.Index = Convert.ToInt32(reader[2], CultureInfo.CurrentCulture);
                Variables.Region = reader[3].ToString();
                Variables.Area = reader[4].ToString();
                Variables.City = reader[5].ToString();
                Variables.Street = reader[6].ToString();
                Variables.Home = reader[7].ToString();
                Variables.Frame = reader[8].ToString();
                Variables.Structure = reader[9].ToString();
                Variables.Flat = reader[10].ToString();
            }
            catch
            {
                Variables.ID = 0;
            }
            reader.Close();
            DBClose(); // закрываем базу данных
        }
        ///
        /// Поиск совпадений по названию организации
        /// 
        public static void CoincidenceFind()
        {
            ///
            /// Убираем пробелы в начале и конце RecipientTextBox
            /// 
            using SqlDataAdapter dataAdapter = new("SELECT Firm FROM [Recipient] WHERE Firm = N'" + Variables.Firm.Trim() + "'", sqlConnection);
            DataTable table = new();
            dataAdapter.Fill(table);
            ///
            ///Если нашел совпадение по Recipient
            ///
            if (table.Rows.Count > 0)
            {
                const string message =
                        "Такой получатель уже существует\nЗаменить данные о получателе?";
                const string caption = "Внимание!!!";
                MessageBox1 messageBox1 = new(caption, message);
                messageBox1.ShowDialog();

                if (messageBox1.DialogResult == true)
                {
                    ///
                    /// Нажата Ok
                    /// 
                    DBOpen();

                    using (SqlCommand command = new("SELECT * FROM [Recipient] WHERE Firm = '" + Variables.Firm.Trim() + "'", sqlConnection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        ///
                        ///При нахождении совпадений Получателя выводит данные в переменные
                        ///
                        Variables.ID = Convert.ToInt32(reader[0], CultureInfo.CurrentCulture);
                        Variables.Firm = reader[1].ToString();
                        Variables.Index = Convert.ToInt32(reader[2], CultureInfo.CurrentCulture);
                        Variables.Region = reader[3].ToString();
                        Variables.Area = reader[4].ToString();
                        Variables.City = reader[5].ToString();
                        Variables.Street = reader[6].ToString();
                        Variables.Home = reader[7].ToString();
                        Variables.Frame = reader[8].ToString();
                        Variables.Structure = reader[9].ToString();
                        Variables.Flat = reader[10].ToString();
                        reader.Close();
                    }
                    DBClose();
                    btnclick = 1;
                }

            }

        }
        ///
        /// Загрузка Id в массив
        /// 
        public static void DBAddArray()
        {
            using SqlCommand command = sqlConnection.CreateCommand();

            idFromDB.Clear();
            command.CommandText = "SELECT Id FROM Recipient";
            DBOpen();
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    idFromDB.Add(reader.GetInt32(0));
                }
            }
            DBClose();
        }

    }
}

