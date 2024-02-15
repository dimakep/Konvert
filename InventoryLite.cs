using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Data.Sqlite;

namespace Konvert
{
    class InventoryLite
    {
        private static readonly string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB.db");
        public static readonly SqliteConnection sqlConnection = new("Data Source = " + path);
        public static string sqlRequest; /// Строка SqlCommand для операций с БД
        public static int counterDB, btnclick; /// Переменная для Id и Index БД
        public static List<int> idFromDB = new();

        public static void DBOpen() ///Открыть базу данных
        {
            sqlConnection.Open();
        }

        public static void DBClose() ///Закрыть базу данных
        {
            sqlConnection.Close();
        }
        ///
        /// Получение количества записей в БД
        ///
        public static void MaxId()
        {
            SqliteCommand command = new("SELECT COUNT(Id) FROM Recipient", sqlConnection);
            DBOpen();
            counterDB = (int)command.ExecuteScalar();
            DBClose();
        }
        ///
        /// Добавление записи в таблицу БД
        /// 
        public static void AddInTable()
        {
            DBOpen();
            SqliteCommand command = new("INSERT INTO Recipient (Firm, Indexs, Region, Area, City, Street, Home, Frame, Structure, Flat)" +
                    " VALUES (@Firm, @Index, @Region, @Area, @City, @Street, @Home, @Frame, @Structure, @Flat)", sqlConnection);
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
            command.ExecuteNonQuery();

            DBClose();
        }
        ///
        /// Изменение строки в таблице
        ///
        public static void UpdateInTable()
        {
            using SqliteCommand command = new("UPDATE Recipient " +
                " SET [Firm] = @Firm, [Indexs] = @Index, [Region] = @Region, [Area] = @Area, [City] = @City, [Street] = @Street, " +
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
            using SqliteCommand command = new("DELETE FROM Recipient WHERE Firm = N'" + Variables.Firm + "'", sqlConnection);
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
            SqliteCommand command = new(sqlRequest, sqlConnection);
            DBOpen(); // Открываем базу данных
            SqliteDataReader reader = command.ExecuteReader();
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
            ///Если нашел совпадение по Recipient
            ///
            DBOpen();
            SqliteCommand command = sqlConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Recipient WHERE Firm = @Firm";
            command.Parameters.AddWithValue("@Firm", Variables.Firm.Trim());

            SqliteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
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
                    //DBOpen();

                    //using (SqlCommand command = new("SELECT * FROM [Recipient] WHERE Firm = '" + Variables.Firm.Trim() + "'"))
                    {
                        //SqlDataReader reader = command.ExecuteReader();
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
                    //DBClose();
                    btnclick = 1;
                }

            }
            DBClose();

        }
        
        public static void DBAddArray()
        {
            using SqliteCommand command = sqlConnection.CreateCommand();

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

        public static void CreateBD()
        {
           DBOpen();

            SqliteCommand command = new();
            command.Connection = sqlConnection;
            command.CommandText = "CREATE TABLE Recipient(ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Firm TEXT NOT NULL, Indexs INT, Region TEXT, Area TEXT, City TEXT, Street TEXT, Home TEXT, Frame TEXT, Structure TEXT, Flat TEXT)";
            command.ExecuteNonQuery();
            Console.WriteLine("Таблица Users создана");
        }


    }
}
