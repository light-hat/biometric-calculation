using Calculator.AdditionalModules;
using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class calculateDlg : Form
    {
        #region Конструктор

        public calculateDlg()
        {
            InitializeComponent();
        }

        #endregion

        #region Обработчик клика для кнопки ОК

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.hammingDist = Convert.ToDouble(textBox1.Text);
                this.standDeviation = Convert.ToDouble(textBox2.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            catch (Exception ex) { ErrorHandler.showErrorMessage(ex.Message); }
        }

        #endregion

        #region Поля класса, использующиеся как возвращаемое значение

        public double hammingDist { get; set; }

        public double standDeviation { get; set; }

        #endregion
    }
}
