using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace уют_
{
    public partial class AutorizationForm : Form
    {
        private bool isPasswordVisible = false;
        public AutorizationForm()
        {
            InitializeComponent();

            //начальная настройка элементов управления
            Button_Main_Autorization.Enabled = false;
            EyeMini.Image = GetImageFromBytes(Properties.Resources.EyeClose);

            //подписка на события элементов управления
            Label_Register.MouseEnter += Label_Register_MouseEnter;
            PanelClose.MouseLeave += PanelClose_MouseLeave;
            PanelClose.MouseEnter += PanelClose_MouseEnter;
            Close_Button.MouseLeave += Close_Button_MouseLeave;
            Close_Button.MouseEnter += Close_Button_MouseEnter;
            Label_Register.MouseLeave += Label_Register_MouseLeave;
            Label_Register.Click += Label_Register_Click;
            Button_Main_Autorization.Click += Button_Main_Autorization_Click;
            Button_Main_Autorization.MouseEnter += Button_Main_Autorization_MouseEnter;
            Button_Main_Autorization.MouseLeave += Button_Main_Autorization_MouseLeave;


            PanelClose.BackColor = Color.FromArgb(39, 168, 224);

            //текста-подсказок в полях ввода
            InitializePlaceholders();

            //подписка на события текстовых полей
            AutL.TextChanged += TextBox1_TextChanged;
            AutL.Enter += AutL_Enter;
            AutL.Leave += AutL_Leave;
            AutP.Enter += AutP_Enter;
            AutP.Leave += AutP_Leave;
            AutP.TextChanged += AutP_TextChanged;
            EyeMini.Click += EyeMini_Click;
        }

        //инициализации текстов-подсказок в полях ввода
        private void InitializePlaceholders()
        {
            AutL.Text = Properties.Resources.LoginPlaceholder;
            AutL.ForeColor = Color.FromArgb(181, 181, 181);

            AutP.Text = Properties.Resources.PasswordPlaceholder;
            AutP.ForeColor = Color.FromArgb(181, 181, 181);
            AutP.PasswordChar = '\0';
        }

        private void AutL_Enter(object sender, EventArgs e)
        {
            if (AutL.Text == Properties.Resources.LoginPlaceholder)
            {
                AutL.Text = "";
                AutL.ForeColor = SystemColors.WindowText;
            }
        }

        private void AutL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AutL.Text))
            {
                AutL.Text = Properties.Resources.LoginPlaceholder;
                AutL.ForeColor = Color.FromArgb(181, 181, 181);
            }
        }


        private void Label_Register_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Show();
            this.Hide();
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button_Main_Autorization_MouseEnter(object sender, EventArgs e)
        {
            Button_Main_Autorization.BackColor = Color.FromArgb(0, 122, 193);
        }

        private void Button_Main_Autorization_MouseLeave(object sender, EventArgs e)
        {
            Button_Main_Autorization.BackColor = Color.FromArgb(39, 168, 224);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateLoginButtonState();
        }

        private void AutP_TextChanged(object sender, EventArgs e)
        {
            UpdateLoginButtonState();
        }

        private void UpdateLoginButtonState()
        {
            string login = AutL.Text.Trim();
            string password = AutP.Text;

            bool isLoginValid = !string.IsNullOrEmpty(login) && login != Properties.Resources.LoginPlaceholder;
            bool isPasswordValid = !string.IsNullOrEmpty(password) && password != Properties.Resources.PasswordPlaceholder;

            Button_Main_Autorization.Enabled = isLoginValid && isPasswordValid;
        }

        private void Button_Main_Autorization_Click(object sender, EventArgs e)
        {
            string login = AutL.Text.Trim();
            string password = AutP.Text;

            if (login == Properties.Resources.LoginPlaceholder || password == Properties.Resources.PasswordPlaceholder)
            {
                MessageBox.Show(Properties.Resources.LoginRequiredMessage);
                return;
            }

            Account userAccount = AccountManager.FindAccount(login);

            if (userAccount == null)
            {
                MessageBox.Show(Properties.Resources.AccountNotFound);
                return;
            }

            bool isPasswordCorrect = PasswordHasher.VerifyPassword(password, userAccount.Password);

            if (!isPasswordCorrect)
            {
                MessageBox.Show(Properties.Resources.WrongPassword);
                return;
            }

            AppContext.CurrentUser = userAccount;
            ProfileForm profileForm = new ProfileForm();
            profileForm.Show();
            this.Hide();
        }

        Point LastPoint;

        private void PanelBlue_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }


        private void PanelBlue_MouseDown(object sender, MouseEventArgs e)
        {
            LastPoint = new Point(e.X, e.Y);
        }

        private void PanelClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Image GetImageFromBytes(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        private void Close_Button_MouseEnter(object sender, EventArgs e)
        {
            PanelClose.BackColor = Color.FromArgb(246, 62, 51);
            Close_Button.BackColor = Color.FromArgb(246, 62, 51);
        }

        private void Close_Button_MouseLeave(object sender, EventArgs e)
        {
            PanelClose.BackColor = Color.FromArgb(39, 168, 224);
            Close_Button.BackColor = Color.FromArgb(39, 168, 224);
        }

        private void PanelClose_MouseEnter(object sender, EventArgs e)
        {
            PanelClose.BackColor = Color.FromArgb(246, 62, 51);
            Close_Button.BackColor = Color.FromArgb(246, 62, 51);
        }

        private void PanelClose_MouseLeave(object sender, EventArgs e)
        {
            PanelClose.BackColor = Color.FromArgb(39, 168, 224);
            Close_Button.BackColor = Color.FromArgb(39, 168, 224);
        }

        private void Label_Register_MouseEnter(object sender, EventArgs e)
        {
            Label_Register.ForeColor = Color.FromArgb(141, 141, 141);
        }

        private void Label_Register_MouseLeave(object sender, EventArgs e)
        {
            Label_Register.ForeColor = Color.FromArgb(181, 181, 181);
        }

        private void Close_Button_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EyeMini_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"Клик! Видимость: {isPasswordVisible}");
            Console.WriteLine($"Текст: {AutP.Text}, Длина: {AutP.Text.Length}");

            // Игнорируем переключение, если поле содержит placeholder или пустое
            if (AutP.Text == Properties.Resources.PasswordPlaceholder || string.IsNullOrEmpty(AutP.Text))
                return;

            // Переключаем состояние видимости
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                // Показываем пароль
                AutP.PasswordChar = '\0';  // Отключаем символы-заполнители
                EyeMini.Image = GetImageFromBytes(Properties.Resources.EyeOpen);
            }
            else
            {
                // Скрываем пароль
                AutP.PasswordChar = '*';  // Включаем символы-заполнители
                EyeMini.Image = GetImageFromBytes(Properties.Resources.EyeClose);
            }

            // Сохраняем позицию курсора
            int selectionStart = AutP.SelectionStart;
            AutP.Focus();
            AutP.SelectionStart = selectionStart;
        }

        private void AutP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AutP.Text))
            {
                AutP.Text = Properties.Resources.PasswordPlaceholder;
                AutP.ForeColor = Color.FromArgb(181, 181, 181);
                AutP.PasswordChar = '\0';
                isPasswordVisible = true;
                EyeMini.Image = GetImageFromBytes(Properties.Resources.EyeClose);
            }
        }

        private void AutP_Enter(object sender, EventArgs e)
        {
            if (AutP.Text == Properties.Resources.PasswordPlaceholder)
            {
                AutP.Text = "";
                AutP.ForeColor = SystemColors.WindowText;
                AutP.PasswordChar = '*';
                isPasswordVisible = false;
                EyeMini.Image = GetImageFromBytes(Properties.Resources.EyeClose);
            }
        }
    }
}