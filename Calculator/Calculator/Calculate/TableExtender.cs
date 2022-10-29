namespace Calculator.Calculate
{
    /// <summary>
    /// Класс, реализующий расширение таблицы в 1,5 раза
    /// </summary>
    public class TableExtender
    {
        #region Конструктор

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="iterTable">Структура, содержащая таблицу, а также заголовки её столбцов и строк</param>
        public TableExtender(IterTableStruct iterTable)
        {
            double[,] inp_matrix = iterTable.matrix;
            double[] inp_rows = iterTable.row_headers;
            double[] inp_columns = iterTable.column_headers;

            // Непосредственно табличные значения
            _a[0, 0] = inp_matrix[0, 0];
            _a[0, 2] = inp_matrix[0, 1];
            _a[2, 0] = inp_matrix[1, 0];
            _a[2, 2] = inp_matrix[1, 1];

            // Заголовки для строк
            _table_rows[0] = inp_rows[0];
            _table_rows[2] = inp_rows[1];

            // Заголовки для столбцов
            _table_cols[0] = inp_columns[0];
            _table_cols[2] = inp_columns[1];
        }

        #endregion

        #region Методы

        /// <summary>
        /// Непосредственно расширение
        /// </summary>
        public void extend()
        {
            // Значения энтропии
            _a[0, 1] = (_a[0, 0] + _a[0, 2]) / 2;
            _a[2, 1] = (_a[2, 0] + _a[2, 2]) / 2;

            _a[1, 0] = (_a[0, 0] + _a[2, 0]) / 2;
            _a[1, 2] = (_a[0, 2] + _a[2, 2]) / 2;

            _a[1, 1] = (_a[0, 1] + _a[2, 1] + _a[1, 0] + _a[1, 2]) / 4;

            // Столбцы
            _table_cols[1] = (_table_cols[0] + _table_cols[2]) / 2;

            // Строки
            _table_rows[1] = (_table_rows[0] + _table_rows[2]) / 2;
        }

        /// <summary>
        /// Получает новую таблицу из расширенной
        /// </summary>
        /// <returns>Финальная таблица для данной итерации табличных преобразований</returns>
        public IterTableStruct getNewMatrix(double real_math_exp, double real_stand_deviation)
        {
            // Получение индексов искомых элементов
            int[] indexes_rows = InterpolSearch.SeacrhInTheThreeItemsArray(_table_rows, real_math_exp);
            int[] indexes_cols = InterpolSearch.SeacrhInTheThreeItemsArray(_table_cols, real_stand_deviation);

            // Формируем таблицу

            double[,] final_matrix = new double[2, 2];

            final_matrix[0, 0] = _a[indexes_rows[0], indexes_cols[0]];
            final_matrix[0, 1] = _a[indexes_rows[0], indexes_cols[1]];
            final_matrix[1, 0] = _a[indexes_rows[1], indexes_cols[0]];
            final_matrix[1, 1] = _a[indexes_rows[1], indexes_cols[1]];

            // Получаем заголовки строк получившейся таблицы

            double[] final_rows = new double[2];

            for (int i = 0; i < 2; i++)
                final_rows[i] = _table_rows[indexes_rows[i]];

            // Получаем заголовки столбцов таблицы

            double[] final_cols = new double[2];

            for (int i = 0; i < 2; i++)
                final_cols[i] = _table_cols[indexes_cols[i]];

            // Формируем ответ

            IterTableStruct result = new IterTableStruct();
            result.matrix = final_matrix;
            result.column_headers = final_cols;
            result.row_headers = final_rows;

            return result;
        }

        #endregion

        #region Поля

        /// <summary>
        /// Расширенная таблица на 9 элементов, где производится следующая итерация поиска
        /// </summary>
        private double[,] _a = new double[3, 3];

        /// <summary>
        /// Строки расширенной таблицы
        /// </summary>
        private double[] _table_rows = new double[3];

        /// <summary>
        /// Столбцы расширенной таблицы
        /// </summary>
        private double[] _table_cols = new double[3];

        #endregion
    }
}
