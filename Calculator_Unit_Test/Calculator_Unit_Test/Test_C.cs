using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Calculate;

namespace Calculator_Unit_Test
{
    /// <summary>
    /// Данный тестовый класс реализует проверку корректности работы модуля расширителя таблицы
    /// </summary>
    [TestClass]
    public class Test_C
    {
        /// <summary>
        /// Данный тестовый метод класса проверяет корректность работы алгоритма расширения таблицы и формирования новой
        /// </summary>
        [TestMethod]
        public void TestTableExtend()
        {
            IterTableStruct input_table = new IterTableStruct();
            input_table.row_headers = new double[2] { 145, 150 };
            input_table.column_headers = new double[2] { 20, 22 };
            input_table.matrix = new double[2, 2] { { 41.6, 35 }, { 44.3, 37.2 } };

            IterTableStruct expect_table = new IterTableStruct();
            expect_table.row_headers = new double[2] { 145, 147.5};
            expect_table.column_headers = new double[2] { 20, 21 };
            expect_table.matrix = new double[2, 2] { { 41.6, 38.3 }, { 42.95, 39.525 } };

            TableExtender test_extender = new TableExtender(input_table);
            test_extender.extend();
            IterTableStruct real_result = test_extender.getNewMatrix(146, 20.3);

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    try
                    {
                        Assert.AreEqual(real_result.matrix[i, j], expect_table.matrix[i, j]);
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Matrix test id failed. This is real meaning: " + real_result.matrix[i, j] + "; and expected: " + expect_table.matrix[i, j]);
                        throw e;
                    }
                }

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Assert.AreEqual(real_result.column_headers[i], expect_table.column_headers[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Column headers test id failed. This is real meaning: " + real_result.column_headers[i] + "; and expected: " + expect_table.column_headers[i]);
                    throw e;
                }

                try
                {
                    Assert.AreEqual(real_result.row_headers[i], expect_table.row_headers[i]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Row headers test id failed. This is real meaning: " + real_result.row_headers[i] + "; and expected: " + expect_table.row_headers[i]);
                    throw e;
                }
            }
        }
    }
}
