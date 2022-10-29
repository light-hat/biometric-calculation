using Calculator.AdditionalModules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace Calculator.Database
{
    /// <summary>
    /// Взаимодействие со sqlite-файлом
    /// </summary>
    public class SQLiteHelper
    {
        #region Конструктор
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="file_path">Путь до обрабатываемого файла</param>
        public SQLiteHelper(string file_path)
        {
            _sqlite_path = file_path;

            sqlCmd = new SQLiteCommand();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Чтение данных из базы
        /// </summary>
        /// <param name="table_name">Имя целевой таблицы в базе</param>
        /// <returns>Структура обрабатываемой таблицы</returns>
        public OpenedTableStruct readDB(string table_name)
        {
            try
            {
                switch (File.Exists(_sqlite_path))
                {
                    case true:
                        {
                            OpenedTableStruct table = new OpenedTableStruct();

                            dbConn = new SQLiteConnection("Data Source=" + _sqlite_path + ";Version=3;");
                            dbConn.Open();
                            sqlCmd.Connection = dbConn;

                            // Получаем количество столбцов и их названия

                            sqlCmd.CommandText = string.Format("pragma table_info({0})", table_name);
                            IDataReader reader = sqlCmd.ExecuteReader();

                            List<string> ret_table_column_names = new List<string>();

                            while (reader.Read())
                            {
                                ret_table_column_names.Add(reader.GetValue(1).ToString());
                            }

                            for (int i = 2; i < ret_table_column_names.Count; i++)
                            {
                                ret_table_column_names[i] = ret_table_column_names[i].Remove(0, 1);
                            }

                            reader.Close();
                            reader.Dispose();

                            // Читаем сами данные

                            List<string> ret_table_data = new List<string>();

                            sqlCmd.CommandText = string.Format("SELECT * FROM {0};", table_name);
                            IDataReader table_data_reader = sqlCmd.ExecuteReader();

                            List<string[]> tmp_table_rows = new List<string[]>();

                            while (table_data_reader.Read())
                            {
                                string[] tmp = new string[ret_table_column_names.Count];

                                for (int i = 0; i < ret_table_column_names.Count; i++)
                                {
                                    tmp[i] = table_data_reader.GetValue(i).ToString();
                                }

                                tmp_table_rows.Add(tmp);
                            }

                            string[,] tmp_ret_table = new string[tmp_table_rows.Count, ret_table_column_names.Count];

                            for (int i = 0; i < tmp_table_rows.Count; i++)
                            {
                                for (int j = 0; j < ret_table_column_names.Count; j++)
                                {
                                    tmp_ret_table[i, j] = tmp_table_rows[i].ToArray()[j];
                                }
                            }

                            table_data_reader.Close();
                            table_data_reader.Dispose();

                            // Выводим ответ

                            table.columns_headers = ret_table_column_names.ToArray();
                            table.table = tmp_ret_table;

                            return table;
                        }

                    case false:
                        {
                            ErrorHandler.showErrorMessage("Что-то пошло не так: этого файла базы данных не существует");
                            return null;
                        }

                    default:
                        return null;
                }
            }

            catch (Exception e)
            {
                ErrorHandler.showErrorMessage(e.Message + '\n' + e.StackTrace);

                return null;
            }
        }

        #endregion

        #region Поля класса

        /// <summary>
        /// Хранит путь до файла базы данных, с которым работаем
        /// </summary>
        private string _sqlite_path;

        /// <summary>
        /// Экземпляр класса подключения к БД
        /// </summary>
        private SQLiteConnection dbConn;

        /// <summary>
        /// Экземпляр класса для исполнения команд, посылаемых базе данных
        /// </summary>
        private SQLiteCommand sqlCmd;

        #endregion
    }
}
