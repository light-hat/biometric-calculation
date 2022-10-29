using Calculator.AdditionalModules;
using Calculator.Calculate;
using Calculator.Database;
using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Calculator
{
    public partial class Calculator : Form
    {
        #region Конструктор

        public Calculator()
        {
            InitializeComponent();

            dataGridView1.MultiSelect = false;
        }

        #endregion

        #region Обработчики кликов

        /// <summary>
        /// Вызываем и обрабатываем диалог выбора таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void базаДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            using (chooseTableDlg ch_dlg = new chooseTableDlg())
            {
                var result = ch_dlg.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    switch (ch_dlg.CurrentTableId)
                    {
                        case 0:
                            callTableReader("entropy_hamming_deviation");

                            break;

                        case 1:
                            callTableReader("ownalien");

                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Непосредственно вычисление значения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вычислениеЗначенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var calcDlg = new calculateDlg()) // Создаём экземпляр диалоговой формы
                {
                    var result = calcDlg.ShowDialog(); // Вызываем диалоговую форму, запрашиваем у пользователя входные данные

                    if (result == DialogResult.OK) // Постараюсь как можно внятнее растолковать, что тут происходит...
                    {
                        double[] full_table_head_rows = getDoubleValuesFromHeadersOfRows(); // Заголовки строк для области данных во всей таблице
                        double[] full_table_head_cols = getDoubleValuesFromHeadersOfColumns(); // Заголовки для столбцов из области данных во всей таблице
                        double[,] full_table_data = getDoubleValuesFromData(); // Непосредственно табличные значения из области данных, тоже вся таблица

                        int[] iter_matrix_rows_indexes = InterpolSearch.Execute(full_table_head_rows, calcDlg.hammingDist); // Индексы заголовков строк искомых элементов в массиве, созданном выше. Важный момент, этот индекс не соответствует индексу из таблицы, что у пользователя на экране.
                        int[] iter_matrix_cols_indexes = InterpolSearch.Execute(full_table_head_cols, calcDlg.standDeviation); // Индексы заголовков столбцов для искомых в таблице данных

                        double[] iter_matrix_row_heads = new double[2]; // Непосредственно заголовки строк искомых элементов
                        double[] iter_matrix_col_heads = new double[2]; // Заголовки столбцов искомых элементов

                        for (int i = 0; i < 2; i++) // Присваиваем значения двум последним массивам
                        {
                            iter_matrix_col_heads[i] = full_table_head_cols[iter_matrix_cols_indexes[i]];
                            iter_matrix_row_heads[i] = full_table_head_rows[iter_matrix_rows_indexes[i]];
                        }

                        double[,] iter_matrix = new double[2, 2]; // Здесь будут храниться табличные данные, над которыми будут производиться вычисления

                        // Запись преобразуемых табличных данных в двумерный массив, который будет обрабатываться дальше
                        for (int i = 0; i < 2; i++)
                            for (int j = 0; j < 2; j++)
                                iter_matrix[i, j] = full_table_data[iter_matrix_rows_indexes[i], iter_matrix_cols_indexes[j]];

                        string full_answer_str = ""; // Эта строка содержит полный ответ, который затем будет выведен в файл
                        string tmp_ans_str; // Строка, куда будут записываться промежуточные результаты

                        tmp_ans_str = string.Format("Усреднение найденных значений: {0} бит", EntropyCalculator.calculate(iter_matrix));
                        MessageBox.Show(tmp_ans_str); // Выводим среднее значение
                        full_answer_str += tmp_ans_str + '\n'; // Записываем

                        double iter_entropy = 0; // Значение энтропии, вычисленное на каждой итерации

                        IterTableStruct iter_table = new IterTableStruct(); // Структура таблицы
                        iter_table.matrix = iter_matrix; // Записываем начальные табличные значения
                        iter_table.row_headers = iter_matrix_row_heads; // Записывам заголовки строк
                        iter_table.column_headers = iter_matrix_col_heads; // Записываем заголовки столбцов

                        bool continue_iter = true; // Логическая переменная, отвечающая за то, продолжать ли вычисления
                        uint counter = 0; // Счётчик итераций

                        while (continue_iter) // Пока пользователь говорит, что хочет продолжить
                        {
                            TableExtender iterTableExter = new TableExtender(iter_table); // Создаём экземпляр класса для расширения таблицы на текущей итерации
                            iterTableExter.extend(); // Расширяем таблицу

                            iter_table = iterTableExter.getNewMatrix(calcDlg.hammingDist, calcDlg.standDeviation); // Получаем новую таблицу

                            iter_entropy = EntropyCalculator.calculate(iter_table.matrix); // Вычисляем среднее значение энтропии на данной итерации

                            tmp_ans_str = string.Format("Результат по {0}-й итерации: {1} бит", Convert.ToString(counter + 1), Convert.ToString(iter_entropy)); // Записываем промежуточный результат
                            continue_iter = ErrorHandler.processingQuestion(tmp_ans_str + ". Продолжить вычисления?"); // Выводим промежуточный результат
                            full_answer_str += tmp_ans_str + '\n'; // И записываем его в ответ

                            if (continue_iter)
                                counter++; // Увеличиваем счётчик
                        }

                        string file_name = "ans.txt"; // Имя файла для записи

                        if (File.Exists(file_name)) // Если он существует - удаляем
                            File.Delete(file_name);

                        using (FileStream file = File.OpenWrite(file_name)) // Создаём файловый поток для создания файла и работы с ним
                        using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8)) // Создаём поток для записи в файл
                            writer.Write(full_answer_str); // Записываем данные в файл

                        Process.Start(file_name); // Запускаем файл
                    }
                }
            }

            catch (Exception ex)
            {
                ErrorHandler.showErrorMessage(ex.Message + '\n' + ex.StackTrace);
            }
        }

        /// <summary>
        /// Обработчик для пункта "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var about = new AboutProgram())
                {
                    about.ShowDialog(this);
                }
            }

            catch (Exception ex)
            {
                ErrorHandler.showErrorMessage(ex.Message + '\n' + ex.StackTrace);
            }
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Преобразует заголовки строк таблицы в массив дробных чисел
        /// </summary>
        /// <returns>Математическое ожидание Хэмминга</returns>
        private double[] getDoubleValuesFromHeadersOfRows()
        {
            double[] result = new double[dataGridView1.Rows.Count - 1];

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                result[i] = Convert.ToDouble(dataGridView1[1, i].Value);
            }

            return result;
        }

        /// <summary>
        /// Преобразует заголовки столбцов таблицы в массив дробных чисел
        /// </summary>
        /// <returns>Стандартное отклонение расстояний Хэмминга</returns>
        private double[] getDoubleValuesFromHeadersOfColumns()
        {
            double[] result = new double[dataGridView1.Columns.Count - 2];

            for (int i = 0; i < dataGridView1.Columns.Count - 2; i++)
                result[i] = Convert.ToDouble(dataGridView1.Columns[i + 2].HeaderText);

            return result;
        }

        /// <summary>
        /// Преобразует данные в таблице в дробные числа. Имеется в виду чисто область данных.
        /// </summary>
        /// <returns>Табличные данные</returns>
        private double[,] getDoubleValuesFromData()
        {
            double[,] result = new double[dataGridView1.Rows.Count - 1, dataGridView1.Columns.Count - 2];

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                for (int j = 0; j < dataGridView1.Columns.Count - 2; j++)
                    result[i, j] = Convert.ToDouble(dataGridView1[j + 2, i].Value);

            return result;
        }

        /// <summary>
        /// Возвращает заголовки столбцов таблицы, отвечающих за значения cтандартного отклонения расстояний Хэмминга
        /// </summary>
        /// <returns>Массив строк, соответствующих стандартному отклонению расстояний Хэмминга</returns>
        private string[] getStringValuesFromHeadersOfColumns()
        {
            string[] result = new string[dataGridView1.Columns.Count - 2];

            for (int i = 0; i < dataGridView1.Columns.Count - 2; i++)
                result[i] = dataGridView1.Columns[i + 2].HeaderText;

            return result;
        }

        /// <summary>
        /// Вызывает метод чтения базы данных и выводит результат на экран
        /// </summary>
        /// <param name="table_name">Имя таблицы в базе данных</param>
        private void callTableReader(string table_name)
        {
            string file_name = "database.sqlite3";

            _path_to_opened_db = file_name;

            SQLiteHelper sqHelper = new SQLiteHelper(_path_to_opened_db);

            OpenedTableStruct ret_table = new OpenedTableStruct();
            ret_table = sqHelper.readDB(table_name);

            dataGridView1.Columns.Add("number", "#");
            dataGridView1.Columns.Add("hammdists", "E(h)");

            for (int i = 2; i < ret_table.columns_headers.Length; i++)
            {
                dataGridView1.Columns.Add(string.Format("stdev{0}", i - 2), ret_table.columns_headers[i]);
            }

            for (int i = 0; i < ret_table.table.GetLength(0); i++)
            {
                dataGridView1.Rows.Add();

                for (int j = 0; j < ret_table.table.GetLength(1); j++)
                {
                    dataGridView1[j, i].Value = ret_table.table[i, j];
                }
            }

            dataGridView1.Rows.Add();
        }

        #endregion

        #region Поля класса

        /// <summary>
        /// Поле класса для хранения пути до обрабатываемой базы данных. Если null - значит база данных не открыта
        /// </summary>
        private string _path_to_opened_db;

        #endregion
    }
}
