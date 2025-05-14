using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace уют_
{
    public partial class RegistrationForm : Form
    {
        private bool isPasswordVisible = false;

        public RegistrationForm()
        {
            InitializeComponent();

            // Настройка иконки глаза для пароля
            EyeMini.Image = GetImageFromBytes(Properties.Resources.EyeClose);

            // Настройка поля пароля
            RegP.UseSystemPasswordChar = false;
            RegP.PasswordChar = '\0';

            RegP.Enter += RegP_Enter;
            RegP.Leave += RegP_Leave;
            RegL.Enter += RegL_Enter;
            RegL.Leave += RegL_Leave;
            RegP.KeyPress += RegP_KeyPress;

            // Установка placeholder текста
            RegL.Text = Properties.Resources.LoginPlaceholder;
            RegL.ForeColor = Color.FromArgb(181, 181, 181);
            RegP.Text = Properties.Resources.PasswordPlaceholder;
            RegP.ForeColor = Color.FromArgb(181, 181, 181);
            RegP.PasswordChar = '\0';

            // Настройка кнопки регистрации
            Button_Main_Registration.Enabled = false;
            Button_Main_Registration.Click += Button_Main_Registration_Click;

            // Настройка кнопки закрытия
            Close_Button.MouseEnter += Close_Button_MouseEnter;
            Close_Button.MouseLeave += Close_Button_MouseLeave;

            // Настройка перехода к авторизации
            Label_Autorization.Click += Label_Autorization_Click;

            // Настройка сообщений об ошибках
            ErrorPass.ForeColor = Color.Red;
            ErrorPass.Visible = false;
            ErrorLog.ForeColor = Color.Red;
            ErrorLog.Visible = false;

            // Подписка на события изменения текста
            RegP.TextChanged += TextBox2_TextChanged;
            RegL.TextChanged += TextBox1_TextChanged;
            EyeMini.Click += EyeMini_Click;
        }

        private Image GetImageFromBytes(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        private void EyeMini_Click(object sender, EventArgs e)
        {
            if (RegP.Text == Properties.Resources.PasswordPlaceholder || string.IsNullOrEmpty(RegP.Text))
                return;

            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                RegP.PasswordChar = '\0';
                EyeMini.Image = GetImageFromBytes(Properties.Resources.EyeOpen);
            }
            else
            {
                RegP.PasswordChar = '*';
                EyeMini.Image = GetImageFromBytes(Properties.Resources.EyeClose);
            }

            int selectionStart = RegP.SelectionStart;
            RegP.Focus();
            RegP.SelectionStart = selectionStart;
        }

        private void RegL_Enter(object sender, EventArgs e)
        {
            if (RegL.Text == Properties.Resources.LoginPlaceholder)
            {
                RegL.Text = "";
                RegL.ForeColor = SystemColors.WindowText;
            }
        }

        private void RegL_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegL.Text))
            {
                RegL.Text = Properties.Resources.LoginPlaceholder;
                RegL.ForeColor = Color.FromArgb(181, 181, 181);
                ErrorLog.Visible = false;
            }
        }

        private void RegP_Enter(object sender, EventArgs e)
        {
            if (RegP.Text == Properties.Resources.PasswordPlaceholder)
            {
                RegP.Text = "";
                RegP.ForeColor = SystemColors.WindowText;
                RegP.PasswordChar = '*';
                isPasswordVisible = false;
                EyeMini.Image = GetImageFromBytes(Properties.Resources.EyeClose);
            }
        }

        private void RegP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegP.Text))
            {
                RegP.Text = Properties.Resources.PasswordPlaceholder;
                RegP.ForeColor = Color.FromArgb(181, 181, 181);
                RegP.PasswordChar = '\0';
                isPasswordVisible = true;
                ErrorPass.Visible = false;
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = RegP.SelectionStart;
            int selectionLength = RegP.SelectionLength;

            string password = RegP.Text;
            string login = RegL.Text.Trim();

            if (string.IsNullOrEmpty(password) || password == Properties.Resources.PasswordPlaceholder)
            {
                ErrorPass.Visible = false;
                Button_Main_Registration.Enabled = false;
                RegP.PasswordChar = '\0';
                isPasswordVisible = true;
                return;
            }

            if (password.Contains(" "))
            {
                int cursorPosition = RegP.SelectionStart;
                string newText = password.Replace(" ", "");
                RegP.Text = newText;
                RegP.SelectionStart = cursorPosition - (password.Length - newText.Length);
                return;
            }

            if (!isPasswordVisible)
            {
                RegP.PasswordChar = '*';
            }

            bool hasError = false;

            if (password.Length < 8)
            {
                ShowPasswordError(Properties.Resources.PasswordErrorLengthShort);
                hasError = true;
            }
            else if (password.Length > 32)
            {
                ShowPasswordError(Properties.Resources.PasswordErrorLengthLong);
                hasError = true;
            }
            else if (password == login)
            {
                ShowPasswordError(Properties.Resources.PasswordMatchError);
                hasError = true;
            }
            else
            {
                ErrorPass.Visible = false;
            }

            Button_Main_Registration.Enabled = !hasError &&
                !string.IsNullOrWhiteSpace(RegL.Text) &&
                RegL.Text != Properties.Resources.LoginPlaceholder;

            RegP.BackColor = SystemColors.Window;
            RegP.SelectionStart = selectionStart;
            RegP.SelectionLength = selectionLength;
        }

        private void Button_Main_Registration_Click(object sender, EventArgs e)
        {
            string login = RegL.Text.Trim();
            string password = RegP.Text.Trim();

            if (login == Properties.Resources.LoginPlaceholder || string.IsNullOrWhiteSpace(login))
            {
                ShowError(Properties.Resources.LoginRequiredMessage);
                Button_Main_Registration.Enabled = false;
                return;
            }

            if (password == Properties.Resources.PasswordPlaceholder || string.IsNullOrWhiteSpace(password))
            {
                ShowPasswordError(Properties.Resources.PasswordRequiredMessage);
                Button_Main_Registration.Enabled = false;
                return;
            }

            bool success = AccountManager.AddAccount(login, PasswordHasher.HashPassword(password));
            if (success)
            {
                AppContext.CurrentUser = new Account { Login = login, Password = password };
                MessageBox.Show(Properties.Resources.AccountCreatedSuccess);
                RegL.Text = "";
                RegP.Text = "";

                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string login = RegL.Text.Trim();

            if (login == Properties.Resources.LoginPlaceholder || string.IsNullOrWhiteSpace(login))
            {
                ErrorLog.Visible = false;
                Button_Main_Registration.Enabled = false;
                return;
            }

            bool hasInvalidChars = !System.Text.RegularExpressions.Regex.IsMatch(login, @"^[a-zA-Z0-9_]*$");
            bool hasLengthError = false;

            if (hasInvalidChars)
            {
                ShowError(Properties.Resources.LoginErrorChars);
                Button_Main_Registration.Enabled = false;
                return;
            }
            else if (login.Length < 3)
            {
                ShowError(Properties.Resources.LoginErrorLengthShort);
                hasLengthError = true;
            }
            else if (login.Length > 20)
            {
                ShowError(Properties.Resources.LoginErrorLengthLong);
                hasLengthError = true;
            }
            else
            {
                ErrorLog.Visible = false;
            }

            bool isPasswordValid = !string.IsNullOrWhiteSpace(RegP.Text) &&
                                 RegP.Text != Properties.Resources.PasswordPlaceholder &&
                                 !ErrorPass.Visible;

            Button_Main_Registration.Enabled = !hasLengthError && isPasswordValid;
            RegL.BackColor = SystemColors.Window;
        }

        private void ShowError(string message)
        {
            ErrorLog.Text = message;
            ErrorLog.Visible = true;
        }

        private void ShowPasswordError(string message)
        {
            ErrorPass.Text = message;
            ErrorPass.Visible = true;
        }

        private void Label_Autorization_Click(object sender, EventArgs e)
        {
            AutorizationForm autorizationForm = new AutorizationForm();
            autorizationForm.Show();
            this.Hide();
        }

        private void Close_Button_MouseLeave(object sender, EventArgs e)
        {
            PanelClose.BackColor = Color.FromArgb(39, 168, 224);
            Close_Button.BackColor = Color.FromArgb(39, 168, 224);
        }

        private void Close_Button_MouseEnter(object sender, EventArgs e)
        {
            PanelClose.BackColor = Color.FromArgb(246, 62, 51);
            Close_Button.BackColor = Color.FromArgb(246, 62, 51);
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void Close_Button_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void Label_Autorization_MouseEnter(object sender, EventArgs e)
        {
            Label_Autorization.ForeColor = Color.FromArgb(141, 141, 141);
        }

        private void Label_Autorization_MouseLeave(object sender, EventArgs e)
        {
            Label_Autorization.ForeColor = Color.FromArgb(181, 181, 181);
        }

        private void RegP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void RegL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }
    }
}