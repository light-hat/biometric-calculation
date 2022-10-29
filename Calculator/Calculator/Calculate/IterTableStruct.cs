namespace Calculator.Calculate
{
    /// <summary>
    /// Это структура для таблицы, которая получается в каждой итерации преобразований.
    /// Дело в том, что мы модифицируем не только сами табличные данные,
    /// но и заголовки столбцов и строк. Поэтому была создана эта структура,
    /// чтобы хранить всё это дело в одном месте.
    /// </summary>
    public class IterTableStruct
    {
        /// <summary>
        /// Непосредственно таблица
        /// </summary>
        public double[,] matrix { get; set; }

        /// <summary>
        /// Заголовки столбцов
        /// </summary>
        public double[] column_headers { get; set; }

        /// <summary>
        /// Заголовки строк
        /// </summary>
        public double[] row_headers { get; set; }
    }
}
