namespace Calculator.Database
{
    /// <summary>
    /// Структура, куда записываются данные, собранные из
    /// базы данных для последующего вывода на экран пользователя
    /// </summary>
    public class OpenedTableStruct
    {
        /// <summary>
        /// Табличные данные открываемой таблицы
        /// </summary>
        public string[,] table { get; set; }

        /// <summary>
        /// Заголовки столбцов открываемой таблицы
        /// </summary>
        public string[] columns_headers { get; set; }
    }
}
