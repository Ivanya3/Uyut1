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
    public partial class AutorizationForm: Form
    {
        public AutorizationForm()
        {
            InitializeComponent();

            Close_Button.MouseEnter += Close_Button_MouseEnter;
            Close_Button.MouseLeave += Close_Button_MouseLeave;
            Close_Button.Click += Close_Button_Click;
            Button_Main_Autorization.MouseEnter += Button_Main_Autorization_MouseEnter;
            Button_Main_Autorization.MouseLeave += Button_Main_Autorization_MouseLeave;
            label3.ForeColor = Color.Red;
            label4.ForeColor = Color.Red;
            label4.Visible = false;
            label3.Visible = false;


            AutL.TextChanged += TextBox1_TextChanged;

            //переход
            Label_Register.Click += Label_Register_Click;

            // Установка начального цвета кнопки
            Close_Button.BackColor = Color.Red;

            //вход
            Button_Main_Autorization.Click += Button_Main_Autorization_Click;

        }

        private void Label_Register_Click(object sender, EventArgs e)
        {
            // Создаём окно регистрации
            RegistrationForm registrationForm = new RegistrationForm();

            // Показываем новую форму
            registrationForm.Show();

            // Закрываем текущую форму
            this.Hide();
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            Close_Button.BackColor = Color.Red;
            Application.Exit();

        }

        private void Close_Button_MouseEnter(object sender, EventArgs e)
        {
            Close_Button.BackColor = Color.FromArgb(161,51,246);
        }

        private void Close_Button_MouseLeave(object sender, EventArgs e)
        {
            Close_Button.BackColor = Color.Red;
        }

        private void Button_Main_Autorization_MouseEnter(object sender, EventArgs e)
        {
            Button_Main_Autorization.BackColor = Color.FromArgb(54, 101, 126);
        }

        private void Button_Main_Autorization_MouseLeave(object sender, EventArgs e)
        {
            Button_Main_Autorization.BackColor = Color.FromArgb(39, 168, 224);
        }

        private void Label_Register_Click_1(object sender, EventArgs e)
        {

        }
        //перемещение--------------------------------------------------------------------
        Point LastPoint;

        private void AutorizationForm_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }

        private void AutorizationForm_MouseDown(object sender, MouseEventArgs e)
        {
            LastPoint = new Point(e.X, e.Y);
        }
        //конец кода для перемещения------------------------------------------------------







        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string login = AutL.Text.Trim(); // Убираем пробелы

            // Если поле пустое - скрываем ошибку и выходим
            if (string.IsNullOrEmpty(login))
            {
                ShowError("*Введите логин");
                Button_Main_Autorization.Enabled = false;
                return;
            }

            // Проверяем условия по очереди
            if (login.Length < 3)
            {
                ShowError("*Логин должен быть от 3 символов");
                Button_Main_Autorization.Enabled = false;
                return;
            }

            if (login.Length > 20)
            {
                ShowError("*Логин должен быть до 20 символов");
                Button_Main_Autorization.Enabled = false;
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(login, @"^[a-zA-Z0-9_]+$"))
            {
                ShowError("*Только буквы (a-z), цифры (0-9) и _");
                Button_Main_Autorization.Enabled = false;
                return;
            }

            // Если все проверки пройдены
            label3.Visible = false;
            Button_Main_Autorization.Enabled = true;
            AutL.BackColor = SystemColors.Window;
        }

        private void ShowError(string message)
        {
            label3.Text = message;
            label3.Visible = true;
        }






        private void Button_Main_Autorization_Click(object sender, EventArgs e)
        {
            string login = AutL.Text.Trim();
            string password = AutP.Text;

            

            // Ищем аккаунт по логину
            Account userAccount = AccountManager.FindAccount(login);

            if (userAccount == null)
            {
                MessageBox.Show("Аккаунт не найден!");
                return;
            }

            // Проверяем пароль (сравниваем хеши)
            bool isPasswordCorrect = PasswordHasher.VerifyPassword(password, userAccount.Password);

            if (!isPasswordCorrect)
            {
                MessageBox.Show("Неверный пароль!");
                return;
            }

            // Есохраняем пользователя и открываем главную форму
            AppContext.CurrentUser = userAccount;

            ProfileForm profileForm = new ProfileForm();
            profileForm.Show();
            this.Hide();
        }







    }
}
