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

        public static void DBAddArray()
        {
            DBOpen();
            //using SqlCommand command = sqlConnection.CreateCommand();
            string sqlExpression = "SELECT COUNT(*) FROM Recipient";
            SqliteCommand command = new(sqlExpression, sqlConnection);
            object idFromDB = command.ExecuteScalar();
            DBClose();

        }
        public static void CreateBD()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=" + path))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "CREATE TABLE Recipient(ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Firm TEXT NOT NULL, Indexs TEXT, Region TEXT, Area TEXT, City TEXT, Street TEXT, Home TEXT, Frame TEXT, Structure TEXT, Flat TEXT)";

                //command.Connection = connection;
                //command.CommandText = "CREATE TABLE Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, Age INTEGER NOT NULL)";
                //command.ExecuteNonQuery();

                command.ExecuteNonQuery();

                Console.WriteLine("Таблица Users создана");
            }
        }

    }
}
