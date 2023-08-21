using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace Konvert
{
    public class Inventory
    {
        static readonly string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB.mdf");
        public string connectionString = $"Data Source = (LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=" + path + "; Integrated Security=True;Connect Timeout=30";
        public readonly SqlConnection sqlConnection = new("Data Source = (LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=" + path + "; Integrated Security=True;Connect Timeout=30");/// Connection String
        public string sqlRequest; /// Строка SqlCommand для операций с БД
        public int idInv, indexInv, counterDB, btnclick; /// Переменная для Id и Index БД
        public string firmInv, regionInv, areaInv, cityInv, streetInv, homeInv, frameInv, structureInv, flatInv; /// Переменные для занесения данных в БД
        public List<int> idFromDB = new();
        public string defaultPrinterName;

        public void DBOpen()
        {
            sqlConnection.Open();
        }

        public void DBClose()
        {
            sqlConnection.Close();
        }
        ///
        /// Получение количества записей в БД
        ///
        public void MaxId()
        {
            SqlCommand command = new("SELECT COUNT(Id) FROM Recipient", sqlConnection);
            DBOpen();
            counterDB = (int)command.ExecuteScalar();
            DBClose();
        }
        ///
        /// Добавление записи в таблицу БД
        /// 
        public void AddInTable()
        {
            using SqlCommand command = new("INSERT INTO Recipient " +
                " VALUES (@Firm, @Index, @Region, @Area, @City, @Street, @Home, @Frame, @Structure, @Flat)", sqlConnection);
            DBOpen(); /// Открываем базу данных для добавление данных
                      ///
                      /// Привязка данных к колонкам таблицы
                      /// 
            command.Parameters.Add(new SqlParameter("@Firm", firmInv));
            command.Parameters.Add(new SqlParameter("@Index", indexInv));
            command.Parameters.Add(new SqlParameter("@Region", regionInv));
            command.Parameters.Add(new SqlParameter("@Area", areaInv));
            command.Parameters.Add(new SqlParameter("@City", cityInv));
            command.Parameters.Add(new SqlParameter("@Street", streetInv));
            command.Parameters.Add(new SqlParameter("@Home", homeInv));
            command.Parameters.Add(new SqlParameter("@Frame", frameInv));
            command.Parameters.Add(new SqlParameter("@Structure", structureInv));
            command.Parameters.Add(new SqlParameter("@Flat", flatInv));
            command.ExecuteNonQuery();/// Внесение данных в таблицу
            DBClose();
        }
        ///
        /// Изменение строки в таблице
        ///
        public void UpdateInTable()
        {
            using SqlCommand command = new("UPDATE Recipient " +
                " SET [Firm] = @Firm, [Index] = @Index, [Region] = @Region, [Area] = @Area, [City] = @City, [Street] = @Street, " +
                "[Home] = @Home, [Frame] = @Frame, [Structure] = @Structure, [Flat] = @Flat " +
                "WHERE Id = '" + idInv + "'", sqlConnection);
            DBOpen(); // Открываем базу данных для добавление данных
            /// Привязка данных к колонкам таблицы

            command.Parameters.AddWithValue("@Firm", firmInv);
            command.Parameters.AddWithValue("@Index", indexInv);
            command.Parameters.AddWithValue("@Region", regionInv);
            command.Parameters.AddWithValue("@Area", areaInv);
            command.Parameters.AddWithValue("@City", cityInv);
            command.Parameters.AddWithValue("@Street", streetInv);
            command.Parameters.AddWithValue("@Home", homeInv);
            command.Parameters.AddWithValue("@Frame", frameInv);
            command.Parameters.AddWithValue("@Structure", structureInv);
            command.Parameters.AddWithValue("@Flat", frameInv);
            command.ExecuteNonQuery();// Внесение данных в таблицу
            DBClose(); // закрываем базу данных
        }
        ///
        /// Удаление заданной строки из БД по имени отправителя
        /// 
        public void DelInTable()
        {
            using SqlCommand command = new("DELETE FROM Recipient WHERE Firm = N'" + firmInv + "'", sqlConnection);
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
        public void FindInTable()
        {
            SqlCommand command = new(sqlRequest, sqlConnection);
            DBOpen(); // Открываем базу данных
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                reader.Read();
                /// Привязка данных к колонкам таблицы
                idInv = Convert.ToInt32(reader[0]);
                firmInv = reader[1].ToString();
                indexInv = Convert.ToInt32(reader[2]);
                regionInv = reader[3].ToString();
                areaInv = reader[4].ToString();
                cityInv = reader[5].ToString();
                streetInv = reader[6].ToString();
                homeInv = reader[7].ToString();
                frameInv = reader[8].ToString();
                structureInv = reader[9].ToString();
                flatInv = reader[10].ToString();
            }
            catch
            {
                idInv = 0;
            }
            reader.Close();
            DBClose(); // закрываем базу данных
        }
        ///
        /// Поиск совпадений по названию организации
        /// 
        public void CoincidenceFind()
        {
            ///
            /// Убираем пробелы в начале и конце RecipientTextBox
            /// 
            using SqlDataAdapter dataAdapter = new("SELECT Firm FROM [Recipient] WHERE Firm = N'" + firmInv.Trim() + "'", sqlConnection);
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

                    using (SqlCommand command = new("SELECT * FROM [Recipient] WHERE Firm = '" + firmInv.Trim() + "'", sqlConnection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        ///
                        ///При нахождении совпадений Получателя выводит данные в переменные
                        ///
                        idInv = reader.GetInt32(0);
                        firmInv = reader.GetString(1).ToString();
                        indexInv = reader.GetInt32(2);
                        regionInv = reader.GetString(3).ToString();
                        areaInv = reader.GetString(4).ToString();
                        cityInv = reader.GetString(5).ToString();
                        streetInv = reader.GetString(6).ToString();
                        homeInv = reader.GetString(7).ToString();
                        frameInv = reader.GetString(8).ToString();
                        structureInv = reader.GetString(9).ToString();
                        flatInv = reader.GetString(10).ToString();
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
        public void DBAddArray()
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

