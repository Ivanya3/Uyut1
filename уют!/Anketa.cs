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
            comboBox_vopros1.SelectedIndexChanged += ComboBox_vopros1_SelectedIndexChanged;
            panel_pok.Visible = false;// по умолчанию делаем panel_pok и panel_are невидемыми
            panel_are.Visible = false;

            //СomboBox_vopros1
            comboBox_vopros1.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ручного ввода
            comboBox_vopros1.Items.AddRange(new object[] { "покупка", "аренда", "все" });//варианты ответов
            //СomboBox_vopros2
            comboBox_vopros2.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ручного ввода
            comboBox_vopros2.Items.AddRange(new object[] { "квартира", "дом", "все" });//варианты ответов
            //СomboBox_vopros3
            comboBox_vopros3.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ручного ввода
            comboBox_vopros3.Items.AddRange(new object[] { "1", "2", "3", "4", "5+", "все" });//варианты ответов
            //comboBox_vopros4_ot   
            comboBox_vopros4_ot.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ручного ввода
            comboBox_vopros4_ot.Items.AddRange(new object[] { "2 000", "10 000", "15 000", "25 000", "30 000", "50 000", "75 000", "все" });//варианты ответов
            //comboBox_vopros4_do
            comboBox_vopros4_do.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ручного ввода
            comboBox_vopros4_do.Items.AddRange(new object[] { "2 000", "10 000", "15 000", "25 000", "30 000", "50 000", "75 000", "все" });//варианты ответов
            //comboBox_vopros5_ot
            comboBox_vopros5_ot.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ручного ввода
            comboBox_vopros5_ot.Items.AddRange(new object[] { "500 000", "1 000 000", "2 500 000", "4 000 000", "7 000 000", "10 000 000", "20 000 000", "50 000 000", "все" });//варианты ответов
            //comboBox_vopros5_do
            comboBox_vopros5_do.DropDownStyle = ComboBoxStyle.DropDownList; // Запрет ручного ввода
            comboBox_vopros5_do.Items.AddRange(new object[] { "500 000", "1 000 000", "2 500 000", "4 000 000", "7 000 000", "10 000 000", "20 000 000", "50 000 000", "все" });//варианты ответов
        }

        private void ClousBox_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ComboBox_vopros1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_vopros1.SelectedItem?.ToString())
            {
                case "покупка":
                    panel_pok.Visible=true;
                    panel_are.Visible = false;
                    break;

                case "аренда":
                    panel_are.Visible = true;
                    panel_pok.Visible = false;
                    break;

                case "все":
                    panel_pok.Visible = true;
                    panel_are.Visible = true;
                    break;
            }
        }
        //currentUser.TransactionTypeAnk = comboBox_vopros1.SelectedItem.ToString();
        //bool success = AccountManager.UpdateAccount(currentUser);

        private void button1_Click(object sender, EventArgs e)// устанавливаем настройки и анкеты
        {
            Account currentUser = AppContext.CurrentUser;
            if (comboBox_vopros1.SelectedItem.ToString() == "покупка")
            {
                currentUser.TransactionTypeAnk = "1";
                bool success = AccountManager.UpdateAccount(currentUser);
            }
            if (comboBox_vopros1.SelectedItem.ToString() == "аренда")
            {
                currentUser.TransactionTypeAnk = "0";
                bool success = AccountManager.UpdateAccount(currentUser);
            }
            if (comboBox_vopros1.SelectedItem.ToString() == "все")
            {
                currentUser.TransactionTypeAnk = "2";
                bool success = AccountManager.UpdateAccount(currentUser);
            }
            if (comboBox_vopros2.Text != "")
            {
                currentUser.TypeAnk = comboBox_vopros2.SelectedItem.ToString();
                bool success = AccountManager.UpdateAccount(currentUser);
            }
            if (comboBox_vopros3.Text != "")
            {
                currentUser.Rooms = comboBox_vopros3.SelectedItem.ToString();
                bool success = AccountManager.UpdateAccount(currentUser);
            }
            if (comboBox_vopros4_ot.Text != "")
            {
                currentUser.RentPriceOt = comboBox_vopros4_ot.SelectedItem.ToString();
                bool success = AccountManager.UpdateAccount(currentUser);
            }
            if (comboBox_vopros4_do.Text != "")
            {
                currentUser.RentPriceDo = comboBox_vopros4_do.SelectedItem.ToString();
                bool success = AccountManager.UpdateAccount(currentUser);
            }
            if (comboBox_vopros5_ot.Text != "")
            {
                currentUser.BuyPriceOt = comboBox_vopros5_ot.SelectedItem.ToString();
                bool success = AccountManager.UpdateAccount(currentUser);
            }
            if (comboBox_vopros5_do.Text != "")
            {
                currentUser.BuyPriceDo = comboBox_vopros5_do.SelectedItem.ToString();
                bool success = AccountManager.UpdateAccount(currentUser);
            }
            MainForm mainForm = new MainForm();//открываем основную форму
            mainForm.Show();
            this.Hide();// выходем из этой формы
        }
    }
}
