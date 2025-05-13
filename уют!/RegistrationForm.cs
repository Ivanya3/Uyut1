using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace уют_
{
    public partial class RegistrationForm: Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            button_Reg.Enabled = false;

            button_Reg.Click += button1_Click;
            Close_Button.Click += pictureBox4_Click;//закрытие
            Close_Button.MouseEnter += Close_Button_MouseEnter;
            Close_Button.MouseLeave += Close_Button_MouseLeave;
            

            refund.Click += Label2_Click;//переход

            labelR.ForeColor = Color.Red;
            RegP.TextChanged += TextBox2_TextChanged;


            labelL.ForeColor = Color.Red;
            RegL.TextChanged += TextBox1_TextChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = RegL.Text;
            string password = PasswordHasher.HashPassword(RegP.Text);


            // Пдобавить аккаунт
            bool success = AccountManager.AddAccount(login, password);

            AppContext.CurrentUser = new Account
            {
                Login = RegL.Text,
                Password = RegP.Text
            };
            

            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string login = RegL.Text.Trim(); 

            
            if (string.IsNullOrEmpty(login))
            {
                ShowError("Введите логин");
                button_Reg.Enabled = false;
                return;
            }
            if (login.Length < 3)
            {
                ShowError("Логин должен быть от 3 символов");
                button_Reg.Enabled = false;
                return;
            }

            if (login.Length > 20)
            {
                ShowError("Логин должен быть до 20 символов");
                button_Reg.Enabled = false;
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(login, @"^[a-zA-Z0-9_]+$"))
            {
                ShowError("Только буквы (a-z), цифры (0-9) и _");
                button_Reg.Enabled = false;
                return;
            }

            // Если все проверки пройдены
            labelL.Visible = false;
            button_Reg.Enabled = true;
            RegL.BackColor = SystemColors.Window;
        }

        private void ShowError(string message)
        {
            labelL.Text = message;
            labelL.Visible = true;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            string password = RegP.Text;
            string login = RegL.Text.Trim();

            
            if (string.IsNullOrEmpty(password))
            {
                ShowPasswordError("Введите пароль");
                button_Reg.Enabled = false;
                return;
            }

            if (password.Length < 8)
            {
                ShowPasswordError("Пароль должен быть от 8 символов");
                button_Reg.Enabled = false;
                return;
            }

            if (password.Length > 32)
            {
                ShowPasswordError("Пароль должен быть до 32 символов");
                button_Reg.Enabled = false;
                return;
            }

            if (password == login)
            {
                ShowPasswordError("Пароль не должен совпадать с логином");
                button_Reg.Enabled = false;
                return;
            }

            // Если все проверки пройдены
            labelR.Visible = false;
            button_Reg.Enabled = true;
            RegP.BackColor = SystemColors.Window; // убираем подсветку 
        }

        private void ShowPasswordError(string message)
        {
            labelR.Text = message;
            labelR.Visible = true;
            RegP.BackColor = Color.LightPink; // Подсветка ошибки
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            AutorizationForm autorizationForm = new AutorizationForm();
            autorizationForm.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Close_Button.BackColor = Color.Red;
            Application.Exit();
        }
        private void Close_Button_MouseLeave(object sender, EventArgs e)
        {
            Close_Button.BackColor = Color.Red;
        }
        private void Close_Button_MouseEnter(object sender, EventArgs e)
        {
            Close_Button.BackColor = Color.FromArgb(161, 51, 246);
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {

        }

        Point LastPoint;

        private void RegistrationForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }

        private void RegistrationForm_MouseDown(object sender, MouseEventArgs e)
        {
            LastPoint = new Point(e.X, e.Y);
        }

        private void RegL_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
