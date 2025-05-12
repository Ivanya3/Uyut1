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
            templateCard = panelCardTemplate;//создаем копию
            templateCard.Visible = false;//скрываем панель-образец
            templateCard.Parent = null; //убираем её из католога
        }

        //=========================Создаем список объектов=================================
        private void LoadDataFromXml()
    {
            try//использую чтоб не вызвать ошбку
            {
                XDocument doc = XDocument.Load("Uyuts.xml");//копируем файл в переменную

                List<Property> properties = new List<Property>();//создаем список класса Property


                foreach (XElement propertyElement in doc.Root.Elements("Property"))//идем по всем элементам doc, то есть Property
                {
                    Property prop = new Property//создаем переменную класса Property
                    {
                        Type = propertyElement.Element("Type")?.Value,//присваиваем Type соответствующий элемент Property
                        City = propertyElement.Element("City")?.Value,//присваиваем City соответствующий элемент Property
                        TransactionType = int.Parse(propertyElement.Element("TransactionType")?.Value ?? "0"),//присваиваем TransactionType соответствующий элемент Property
                        RentPrice = decimal.Parse(propertyElement.Element("RentPrice")?.Value ?? "0"),//присваиваем RentPrice соответствующий элемент Property(если есть)
                        BuyPrice = decimal.Parse(propertyElement.Element("BuyPrice")?.Value ?? "0"),//присваиваем BuyPrice соответствующий элемент Property(если есть)
                        Description = propertyElement.Element("Description")?.Value//присваиваем Description соответствующий элемент Property
                    };

                    properties.Add(prop);//добавляем prop в properties

                }

                // properties — массив с данными из файла
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");//сообщаем об ошибке 
            }
        }

        private void LoadProperties()// создаем католог по умолчанию
        {
            var properties = PropertyManager.LoadProperties();// берем переменную класса properties PropertyManager
            CreateCards(properties.Count); // создаем католог из всех элементов по умолчанию

        }

        private void CreateCards(int count)//для создания панели по образцу
        {
            flowMain.Controls.Clear();// очищаем flowMain

            for (int i = 0; i < count; i++)//
            {
                // Создаем копию шаблона
                Panel newCard = new Panel// создаем панель по образцу
                {
                    Name = $"panelCardTemplate{cardCounter++}",
                    Size = templateCard.Size,
                    BackColor = templateCard.BackColor,
                    BorderStyle = templateCard.BorderStyle,
                    Margin = templateCard.Margin
                };

                // Копируем дочерние элементы
                CloneChildControls(templateCard, newCard, i);
                
                flowMain.Controls.Add(newCard);// добавляем созданную панел товара в католог
            }
        }//=====================================================

        private void CloneChildControls(Control source, Control destination, int i)// для копирования дочерних элементов
        {
            foreach (Control original in source.Controls)//идем по всем элементам
            {
                var properties = PropertyManager.LoadProperties();//берем переменную класса properties PropertyManager
                Control clone = null;//

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


        //=================================================================

        private void pictureBox1_Click(object sender, EventArgs e)// для выхода при нажатии на кнопку закрытия
        {
            Close_Button.BackColor = Color.Red;//для красоты
            Application.Exit();//выходим
        }
        
        
        
        Point LastPoint;// создаем переменную для запоминания расположения
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            LastPoint = new Point(e.X, e.Y);//запоминаем расположение при нажатии ЛКМ
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//измением расположение когда двигаем мышь с зажатым ЛКМ
            {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }

        private void labelProf_Click(object sender, EventArgs e)//открытие профиля
        {
            ProfileForm profileForm = new ProfileForm();//создаем новое окно профиля
            profileForm.Show();//открываем его
            this.Hide();//закрываем существующие (MainForm)
        }


        private void labelAnk_Click(object sender, EventArgs e)//открытие анкеты
        {
            Anketa anketaForm = new Anketa();//создаем новое окно анкеты
            anketaForm.Show();//открываем его
            this.Hide();//закрываем существующие (MainForm)
        }


        private void panelHeader_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = true;//делаем видимым выбор катологов
        }

        private void panel_tr_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = false;// закрываем выбор
            flowMain.Controls.Clear();// очищаем существующий котолог

            var properties = PropertyManager.LoadProperties();//берем список с данными

            category.Text = label_Tr.Text;//меняем название католога

            for (int i = 0; i < properties.Count; i++)//идем по всему списку
            {
                if (properties[i].TransactionType == 0 || properties[i].TransactionType == 2)//находим товар, который удовлетворяет условиям
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
                    CloneChildControls(templateCard, newCard, i);

                    flowMain.Controls.Add(newCard);
                }
            }
        }

        private void panel_pok_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();

            int count = 0;

            category.Text = label_pok.Text;

            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i].TransactionType == 1 || properties[i].TransactionType == 2)
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
                    CloneChildControls(templateCard, newCard, i);

                    flowMain.Controls.Add(newCard);
                }
            }
        }

        private void label_All_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();

            int count = 0;

            category.Text = label_All.Text;

            for (int i = 0; i < properties.Count; i++)
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
                CloneChildControls(templateCard, newCard, i);
                flowMain.Controls.Add(newCard);
                
            }
        }

        private void label_kv_Click(object sender, EventArgs e)
        { 

            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();

            int count = 0;

            category.Text = label_kv.Text;

            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i].Type == "Квартира")
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
                    CloneChildControls(templateCard, newCard, i);

                    flowMain.Controls.Add(newCard);
                }
            }
        }
        
        private void label_dom_Click(object sender, EventArgs e)
        {
            panel_choice.Visible = false;
            flowMain.Controls.Clear();

            var properties = PropertyManager.LoadProperties();

            int count = 0;

            category.Text = label_dom.Text;

            for (int i = 0; i < properties.Count; i++)
            {
                if (properties[i].Type == "Дом")
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
                    CloneChildControls(templateCard, newCard, i);

                    flowMain.Controls.Add(newCard);
                }
            }

        }
    }
}
