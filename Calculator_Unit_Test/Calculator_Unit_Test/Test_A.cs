using System;
using Calculator.Calculate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator_Unit_Test
{
    /// <summary>
    /// Данный класс отвечает за тестирование модуля, отвечающего за интерполяционный поиск
    /// </summary>
    [TestClass]
    public class Test_A
    {
        #region Тестовые методы класса

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между первым и вторым элементом массива
        /// </summary>
        [TestMethod]
        public void TestFirstBorderOfAllColumns()
        {
            int[] expected_result = new int[2] { 0, 1 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.Execute(AllColumnsOfTable, 21);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 1 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    throw e;
                }
            }
        }

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между предпоследним и последним элементом массива
        /// </summary>
        [TestMethod]
        public void TestSecondBorderOfAllColumns()
        {
            int[] expected_result = new int[2] { AllColumnsOfTable.Length - 2, AllColumnsOfTable.Length - 1 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.Execute(AllColumnsOfTable, 71);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 2 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    throw e;
                }
            }
        }

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между первым и вторым элементом массива
        /// </summary>
        [TestMethod]
        public void TestFirstBorderOfAllRows()
        {
            int[] expected_result = new int[2] { 0, 1 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.Execute(AllRowsOfTable, 32);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 3 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    throw e;
                }
            }
        }

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между предпоследним и последним элементом массива
        /// </summary>
        [TestMethod]
        public void TestSecondBorderOfAllRows()
        {
            int[] expected_result = new int[2] { AllRowsOfTable.Length - 2, AllRowsOfTable.Length - 1 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.Execute(AllRowsOfTable, 147);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 4 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    throw e;
                }
            }
        }

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между 0-м и 1-м элементами
        /// </summary>
        [TestMethod]
        public void TestFirstBorderOfIntThreeItemsArray()
        {
            int[] expected_result = new int[2] { 0, 1 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.SeacrhInTheThreeItemsArray(IntThreeItemsArray, 70.5);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 5 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    throw e;
                }
            }
        }

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между 1-м и 2-м элементами
        /// </summary>
        [TestMethod]
        public void TestSecondBorderOfIntThreeItemsArray()
        {
            int[] expected_result = new int[2] { 1, 2 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.SeacrhInTheThreeItemsArray(IntThreeItemsArray, 71.8);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 6 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    throw e;
                }
            }
        }

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между 0-м и 1-м элементами
        /// </summary>
        [TestMethod]
        public void TestFirstBorderOfMixedThreeItemsArray()
        {
            int[] expected_result = new int[2] { 0, 1 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.SeacrhInTheThreeItemsArray(MixThreeItemsArray, 31);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 7 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    throw e;
                }
            }
        }

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между 1-м и 2-м элементами
        /// </summary>
        [TestMethod]
        public void TestSecondBorderOfMixedThreeItemsArray()
        {
            int[] expected_result = new int[2] { 1, 2 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.SeacrhInTheThreeItemsArray(MixThreeItemsArray, 34);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 8 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    throw e;
                }
            }
        }

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между 0-м и 1-м элементами
        /// </summary>
        [TestMethod]
        public void TestFirstBorderOfFractThreeItemsArray()
        {
            int[] expected_result = new int[2] { 0, 1 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.SeacrhInTheThreeItemsArray(FractThreeItemsArray, 5.85);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 9 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    Console.WriteLine("Real array: {" + real_result[0] + ", " + real_result[1] + "}");

                    throw e;
                }
            }
        }

        /// <summary>
        /// Данный тестовый метод осуществляет проверку корректности поиска значения между 1-м и 2-м элементами
        /// </summary>
        [TestMethod]
        public void TestSecondBorderOfFractThreeItemsArray()
        {
            int[] expected_result = new int[2] { 1, 2 };
            int[] real_result = new int[2];

            real_result = InterpolSearch.SeacrhInTheThreeItemsArray(FractThreeItemsArray, 5.75);

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(expected_result[i], real_result[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Test 10 failed. Expected value: " + expected_result[i] + "; real value: " + real_result[i]);
                    Console.WriteLine("What's happened: " + e.Message);

                    Console.WriteLine("Real array: {" + real_result[0] + ", " + real_result[1] + "}");

                    throw e;
                }
            }
        }

        #endregion

        #region Тестовые наборы данных

        /// <summary>
        /// Тестовый набор данных, содержащий все значения заголовков столбцов таблицы
        /// </summary>
        private double[] AllColumnsOfTable = new double[27] { 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70, 72 };

        /// <summary>
        /// Тестовый набор данных, содержащий все значения заголовков строк таблицы
        /// </summary>
        private double[] AllRowsOfTable = new double[25] { 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110, 115, 120, 125, 130, 135, 140, 145, 150 };

        /// <summary>
        /// Тестовый набор данных, содержащий три целых элемента
        /// </summary>
        private double[] IntThreeItemsArray = new double[3] { 70, 71, 72 };

        /// <summary>
        /// Тестовый набор данных, содержащий три элемента, в состав входят целые и дробные числа
        /// </summary>
        private double[] MixThreeItemsArray = new double[3] { 30, 32.5, 35 };

        /// <summary>
        /// Тестовый набор данных, содержащий три дробных элемента
        /// </summary>
        private double[] FractThreeItemsArray = new double[3] { 5.9, 5.8, 5.7 };

        #endregion
    }
}
