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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace уют_
{
    public partial class MainForm : Form
    {

        private Panel templateCard; // Шаблонная карточка
        private int cardCounter = 1; // Счетчик для имен


        public MainForm()
        {
            InitializeComponent();//
            InitializeTemplate();//
            LoadDataFromXml();//создаем список из ДБ
            Close_Button.Click += pictureBox1_Click;//закрытие приложения
            labelProf.Click += labelProf_Click;// открытие профиля
            LoadProperties();//все категории (gj evjkxfyb.)
            labelAnk.Click += labelAnk_Click;//открываем окно анкеты
            panelHeader.Click += panelHeader_Click;//открываем выбор категории:
            panel_choice.Visible = false;// скрываем панель выбор категорий
            panel_tr.Click += panel_tr_Click;//категория аренды
            label_Tr.Click += panel_tr_Click;
            panel_pok.Click += panel_pok_Click;//категория купли-продажи
            label_pok.Click += panel_pok_Click;
            label_All.Click += label_All_Click; // все категории
            panelAll.Click += label_All_Click;
            label_kv.Click += label_kv_Click;//категория квартир
            panelKV.Click += label_kv_Click;
            label_dom.Click += label_dom_Click;//категория дома
            panel_dom.Click += label_dom_Click;
        }

        private void InitializeTemplate()
        {
            templateCard = panelCardTemplate;
            templateCard.Visible = false;
            templateCard.Parent = null; 
        }
        private void CopePanel(int i)
        {
            Panel newCard = new Panel
            {
                Name = $"{cardCounter++}",
                Size = templateCard.Size,
                BackColor = templateCard.BackColor,
                BorderStyle = templateCard.BorderStyle,
                Margin = templateCard.Margin
            };

            // Копируем дочерние элементы
            CloneChildControls(templateCard, newCard, i);

            flowMain.Controls.Add(newCard);
        }
        private void LoadDataFromXml()
    {
            try
            {
                XDocument doc = XDocument.Load("Uyuts.xml");

                List<Property> properties = new List<Property>();

                foreach (XElement propertyElement in doc.Root.Elements("Property"))
                {
                    Property prop = new Property
                    {
                        Id = propertyElement.Element("Id")?.Value,
                        Type = propertyElement.Element("Type")?.Value,
                        City = propertyElement.Element("City")?.Value,
                        TransactionType = int.Parse(propertyElement.Element("TransactionType")?.Value ?? "0"),
                        RentPrice = decimal.Parse(propertyElement.Element("RentPrice")?.Value ?? "0"),
                        BuyPrice = decimal.Parse(propertyElement.Element("BuyPrice")?.Value ?? "0"),
                        Description = propertyElement.Element("Description")?.Value
                    };

                    properties.Add(prop);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

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
                Panel newCard = new Panel
                {
                    Name = $"{cardCounter++}",
                    Size = templateCard.Size,
                    BackColor = templateCard.BackColor,
                    BorderStyle = templateCard.BorderStyle,
                    Margin = templateCard.Margin
                };

                CloneChildControls(templateCard, newCard, i);
                
                flowMain.Controls.Add(newCard);
            }
        }
        private void CloneChildControls(Control source, Control destination, int i)
        {
            foreach (Control original in source.Controls)
            {
                var properties = PropertyManager.LoadProperties();
                Control clone = null;

                if (original is Label lbl)
                {
                    
                    clone = new Label
                    {
                        Name = lbl.Name,
                        Text = properties[i].City,
                        Font = lbl.Font,
                        ForeColor = lbl.ForeColor,
                        Location = lbl.Location,
                        Size = lbl.Size
                    };
                    switch (lbl.Text)
                    {
                        case "Квартира в Москве              ":
                            clone.Text = $"{properties[i].Type} в {properties[i].City}";
                            break;
                        case "50000                          \r\n                    ":
                            if (properties[i].TransactionType == 0) 
                            {
                                clone.Text = $"{properties[i].RentPrice} Pублей/мес";
                            }
                            else if (properties[i].TransactionType == 1) 
                            {
                                clone.Text = $"{properties[i].BuyPrice} Pублей";
                            }
                            else if (properties[i].TransactionType == 2) 
                            {
                                clone.Text = $"{properties[i].BuyPrice} Pублей, {properties[i].RentPrice} Pублей/мес" ;
                            }
                            break;
                        case "3-комн. квартира, 75 кв.м, ремонт      \r\n                                                                  ":
                            clone.Text = properties[i].Description;
                            break;
                        default:
                            clone.Text = lbl.Text;
                            break;
                    }
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

        private void labelProf_Click(object sender, EventArgs e)
        {
            ProfileForm profileForm = new ProfileForm();
            profileForm.Show();
            this.Hide();
        }

        private void labelAnk_Click(object sender, EventArgs e)
        {
            Anketa anketaForm = new Anketa();
            anketaForm.Show();
            this.Hide();
        }

        private void panelHeader_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = true;
        }

        private void panel_tr_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();

            category.Text = label_Tr.Text;

            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i].TransactionType == 0 || properties[i].TransactionType == 2)//находим товар, который удовлетворяет условиям
                {
                    CopePanel(i);
                }
            }
        }

        private void panel_pok_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();

            category.Text = label_pok.Text;

            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i].TransactionType == 1 || properties[i].TransactionType == 2)
                {
                    // Создаем копию шаблона
                    CopePanel(i);
                }
            }
        }

        private void label_All_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();

            category.Text = label_All.Text;

            for (int i = 0; i < properties.Count; i++)
            {
                // Создаем копию шаблона
                CopePanel(i);

            }
        }

        private void label_kv_Click(object sender, EventArgs e)
        { 

            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();

            category.Text = label_kv.Text;

            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i].Type == "Квартира")
                {
                    // Создаем копию шаблона
                    CopePanel(i);
                }
            }
        }
        
        private void label_dom_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();

            category.Text = label_dom.Text;

            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i].Type == "Дом")
                {
                    // Создаем копию шаблона
                    CopePanel(i);
                }
            }

        }

        private void panelAnk_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();


            Account currentUser = AppContext.CurrentUser;

            category.Text = label_Ank.Text;
            int TT = int.Parse(currentUser.TransactionTypeAnk);
            int RPriceOt = int.Parse(currentUser.RentPriceOt);
            int RPriceDo = int.Parse(currentUser.RentPriceDo);
            int BPriceOt = int.Parse(currentUser.BuyPriceOt);
            int BPriceDo = int.Parse(currentUser.BuyPriceDo);
            string TAnk = currentUser.TypeAnk;
            for (int i = 0; i < properties.Count; i++)
            {
                if (((TT == 2 ) || (properties[i].TransactionType == TT || properties[i].TransactionType == 2)) && ((properties[i].Type == TAnk || TAnk == "все")) && (properties[i].RentPrice >= RPriceOt ) && (properties[i].RentPrice <= RPriceDo) && (properties[i].BuyPrice >= BPriceOt) && (properties[i].BuyPrice <= BPriceDo))
                {
                    CopePanel(i);
                }
            }
        }
    }
}
