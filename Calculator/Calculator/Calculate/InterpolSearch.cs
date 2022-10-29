namespace Calculator.Calculate
{
    /// <summary>
    /// Немного модифицированный интерполяционный поиск.
    /// Возвращает не конкретный элемент массива, а два соседних элемента,
    /// между которыми находится значение.
    /// </summary>
    public static class InterpolSearch
    {
        /// <summary>
        /// Метод класса, непосредственно выполняющий поиск
        /// </summary>
        /// <param name="a">Входной массив, в котором осуществляется поиск</param>
        /// <param name="key">Искомое значение, ключ</param>
        /// <returns>Массив из 2-х элементов. Содержит индексы элементов, между которыми находится искомое значение.</returns>
        public static int[] Execute(double[] a, double key)
        {
            System.Array.Sort(a);

            int[] ans = new int[2];

            int mid = 0, left = 0, right = a.Length - 1;

            while (a[left] <= key && a[right] >= key)
            {
                mid = (int)(left + ((key - a[left]) * (right - left)) / (a[right] - a[left]));

                if (a[mid] < key) left = mid + 1;

                else if (a[mid] > key) right = mid - 1;

                else
                {
                    ans[0] = mid;
                    ans[1] = mid;

                    return ans;
                }
            }

            if (a[left] == key)
            {
                ans[0] = left;
                ans[1] = left;

                return ans;
            }

            else
            {
                ans[0] = mid;
                ans[1] = left;

                return ans;
            }
        }

        /// <summary>
        /// Поиск элемента в расширенной таблице. Так же, как и в InterpolSearch - поиск и самих элементов,
        /// и 2-х соседних, между которыми находится искомое значение.
        /// Входной массив содержит 3 элемента, причём они не всегда упорядочены по возрастанию.
        /// Так что здесь проще обработать условной конструкцией.
        /// </summary>
        /// <param name="a">Входной массив на 3 элемента</param>
        /// <param name="key">Искомое значение</param>
        /// <returns>Массив с индексами найдённых элементов</returns>
        public static int[] SeacrhInTheThreeItemsArray(double[] a, double key)
        {
            int[] ans = new int[2];

            if (IsAllArrayEntriesEqual(a)) // Все элементы равны между собой
            {
                if (a[0] == key)
                {
                    ans[0] = 0;

                    return ans;
                }

                else throw new System.Exception("Индекс элемента не найден. Проверьте входные значения.");
            }

            else if (a[0] == key) // Первый полностью совпал
            {
                ans[0] = 0;
                ans[1] = 0;

                return ans;
            }

            else if (a[1] == key) // Второй полностью совпал
            {
                ans[0] = 1;
                ans[1] = 1;

                return ans;
            }

            else if (a[2] == key) // Третий полностью совпал
            {
                ans[0] = 2;
                ans[1] = 2;

                return ans;
            }

            else if (IsArrayNormalSorted(a)) // Между элементами, при этом у массива прямой порядок сортировки
            {
                if (a[0] < key && key < a[1])
                {
                    ans[0] = 0;
                    ans[1] = 1;

                    return ans;
                }

                else if (a[1] < key && key < a[2])
                {
                    ans[0] = 1;
                    ans[1] = 2;

                    return ans;
                }

                else throw new System.Exception("Индекс элемента не найден. Проверьте входные значения.");
            }

            else if (IsArrayReverseSorted(a)) // Между элементами, но при обратном порядке сортировки массива
            {
                if (a[0] > key && key > a[1])
                {
                    ans[0] = 0;
                    ans[1] = 1;

                    return ans;
                }

                else if (a[1] > key && key > a[2])
                {
                    ans[0] = 1;
                    ans[1] = 2;

                    return ans;
                }

                else throw new System.Exception("Индекс элемента не найден. Проверьте входные значения.");
            }

            else throw new System.Exception("На вход поступил некорректный массив.");
        }

        #region Определение порядка сортировки массива

        /// <summary>
        /// Определяем, прямой ли порядок сортировки у массива
        /// </summary>
        /// <param name="input">Входной массив на три элемента</param>
        /// <returns>Логическое значение, да или нет</returns>
        private static bool IsArrayNormalSorted(double[] input)
        {
            if (input[0] < input[1] && input[1] < input[2])
                return true;

            return false;
        }

        /// <summary>
        /// Определяем, обратный ли порядок сортировки у массива
        /// </summary>
        /// <param name="input">Входной массив на три элемента</param>
        /// <returns>Логическое значение, да или нет</returns>
        private static bool IsArrayReverseSorted(double[] input)
        {
            if (input[0] > input[1] && input[1] > input[2])
                return true;

            return false;
        }

        /// <summary>
        /// Определяет, все ли элементы массива равны
        /// </summary>
        /// <param name="input">Входной массив на три элемента</param>
        /// <returns>Логическое значение, да или нет</returns>
        private static bool IsAllArrayEntriesEqual(double[] input)
        {
            if (input[0] == input[1] && input[1] == input[2])
                return true;

            return false;
        }

        #endregion
    }
}
