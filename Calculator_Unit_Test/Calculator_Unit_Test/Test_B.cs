using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Calculate;

namespace Calculator_Unit_Test
{
    /// <summary>
    /// Данный тестовый класс проверяет корректность усреднения значений энтропии
    /// </summary>
    [TestClass]
    public class Test_B
    {
        /// <summary>
        /// Усредняем целые
        /// </summary>
        [TestMethod]
        public void TestIntAveraging()
        {
            Assert.AreEqual(EntropyCalculator.calculate(new double[,] { { 3, 10 }, { 12, 25 } }), 12.5);
        }

        /// <summary>
        /// Усредняем дробные
        /// </summary>
        [TestMethod]
        public void TestDoubleAveraging()
        {
            Assert.AreEqual(EntropyCalculator.calculate(new double[,] { { 10.2, 26.4 }, { 4.8, 3.14 } }), 11,135);
        }

        /// <summary>
        /// Усредняем и целые, и дробные
        /// </summary>
        [TestMethod]
        public void TestMixAveraging()
        {
            Assert.AreEqual(EntropyCalculator.calculate(new double[,] { { 6.8, 4 }, { 45.9, 24 } }), 20.175);
        }
    }
}
