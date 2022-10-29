using System.Windows.Forms;

namespace Calculator.AdditionalModules
{
    /// <summary>
    /// Обработка ошибок
    /// </summary>
    public static class ErrorHandler
    {
        /// <summary>
        /// Показать сообщение об ошибке
        /// </summary>
        /// <param name="message">Сообщение</param>
        public static void showErrorMessage(string message)
        {
            MessageBox.Show(message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Показать предупреждение
        /// </summary>
        /// <param name="message">Сообщение</param>
        public static void showWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Сообщение с какой-либо информацией
        /// </summary>
        /// <param name="message">Сообщение</param>
        public static void showInfoMessage(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработка диалоговых окон для подтверждения действия
        /// </summary>
        /// <param name="question">Вопрос, задаваемый пользователю программой</param>
        /// <returns>Ответ пользователя в виде логического значения</returns>
        public static bool processingQuestion(string question)
        {
            DialogResult result = MessageBox.Show(question, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    return true;

                case DialogResult.No:
                    return false;

                default:
                    return false;
            }
        }
    }
}
