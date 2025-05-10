using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace уют_
{
    public partial class MainForm : Form
    {

        private Panel templateCard; // Шаблонная карточка
        private int cardCounter = 1; // Счетчик для имен


        public MainForm()
        {
            InitializeComponent();
            InitializeTemplate();
            LoadDataFromXml();
            Close_Button.Click += pictureBox1_Click;
            label3.Click += label3_Click;


        }

        private void InitializeTemplate()
        {
            templateCard = panelCardTemplate;
            templateCard.Visible = false;
            templateCard.Parent = null; 
        }

        //=========================Создаем список объектов=================================
        private void LoadDataFromXml()
    {
            try
            {
                string filePath = "Uyuts.xml";

                XDocument doc = XDocument.Load(filePath);

                List<Property> properties = new List<Property>();

                int houseCount = 0;
                int flatCount = 0;

                foreach (XElement propertyElement in doc.Root.Elements("Property"))
                {
                    Property prop = new Property
                    {
                        Type = propertyElement.Element("Type")?.Value,
                        City = propertyElement.Element("City")?.Value,
                        TransactionType = int.Parse(propertyElement.Element("TransactionType")?.Value ?? "0"),
                        RentPrice = decimal.Parse(propertyElement.Element("RentPrice")?.Value ?? "0"),
                        BuyPrice = decimal.Parse(propertyElement.Element("BuyPrice")?.Value ?? "0"),
                        Description = propertyElement.Element("Description")?.Value
                    };

                    properties.Add(prop);

                    if (prop.Type == "Дом") houseCount++;
                    else if (prop.Type == "Квартира") flatCount++;
                }

                MessageBox.Show($"Домов: {houseCount}\nКвартир: {flatCount}\nВсего: {properties.Count}");

                // properties — массив с данными
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        //===================копируем=============================================

        private void LoadProperties()
        {
            var properties = PropertyManager.LoadProperties();
            CreateCards(properties.Count); 

        }

        private void CreateCards(int count)
        {
            flowMain.Controls.Clear();

            for (int i = 0; i < count; i++)
            {
                // Создаем копию шаблона
                Panel newCard = new Panel
                {
                    Name = $"panelCardTemplate{cardCounter++}",
                    Size = templateCard.Size,
                    BackColor = templateCard.BackColor,
                    BorderStyle = templateCard.BorderStyle,
                    Margin = templateCard.Margin
                };

                // Копируем дочерние элементы
                CloneChildControls(templateCard, newCard);

                flowMain.Controls.Add(newCard);
            }
        }

        private void CloneChildControls(Control source, Control destination)
        {
            foreach (Control original in source.Controls)
            {
                Control clone = null;

                if (original is Label lbl)
                {
                    clone = new Label
                    {
                        Name = lbl.Name,
                        Text = lbl.Text,
                        Font = lbl.Font,
                        ForeColor = lbl.ForeColor,
                        Location = lbl.Location,
                        Size = lbl.Size
                    };
                }
                else if (original is PictureBox pic)
                {
                    clone = new PictureBox
                    {
                        Name = pic.Name,
                        SizeMode = pic.SizeMode,
                        Image = pic.Image,
                        Size = pic.Size,
                        Location = pic.Location
                    };
                }
                else if (original is Button btn)
                {
                    clone = new Button
                    {
                        Name = btn.Name,
                        Text = btn.Text,
                        Size = btn.Size,
                        Location = btn.Location,
                        FlatStyle = btn.FlatStyle
                    };
                }

                if (clone != null)
                {
                    destination.Controls.Add(clone);
                }
            }
        }


        //=====================================================================






        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close_Button.BackColor = Color.Red;
            Application.Exit();
        }
        
        
        
        Point LastPoint;
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            LastPoint = new Point(e.X, e.Y);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm();
            profileForm.Show();
            this.Hide();
        }

        private void panelCardTemplate_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
