using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace уют_
{
    public partial class Anketa: Form
    {
        public Anketa()
        {
            InitializeComponent();
            ClousBox.Click += ClousBox_Click;

            //СomboBox_vopros1
            comboBox_vopros1.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ручного ввода
            comboBox_vopros1.Items.AddRange(new object[] { "покупка", "аренда", "все" });//варианты ответов
            //СomboBox_vopros1
            comboBox_vopros2.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ручного ввода
            comboBox_vopros2.Items.AddRange(new object[] { "покупка", "аренда", "все" });//варианты ответов

        }

        private void ClousBox_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
