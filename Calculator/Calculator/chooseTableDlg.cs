using System;
using System.Windows.Forms;

namespace Calculator
{
    /// <summary>
    /// Диалоговая форма для выбора таблицы
    /// </summary>
    public partial class chooseTableDlg : Form
    {
        /// <summary>
        /// Конструктор класса формы
        /// </summary>
        public chooseTableDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик клика для кнопки "Выбрать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            switch (radioButton1.Checked)
            {
                case true:
                    CurrentTableId = 0;
                    DialogResult = DialogResult.OK;
                    Close();

                    break;

                case false:
                    CurrentTableId = 1;
                    DialogResult = DialogResult.OK;
                    Close();

                    break;
            }
        }

        /// <summary>
        /// Возвращаемый номер таблицы
        /// </summary>
        public int CurrentTableId { get; set; }
    }
}
