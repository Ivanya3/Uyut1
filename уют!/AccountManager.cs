using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;

public static class AccountManager
{
    private static string file = "Ak.xml"; // файл XML

    public static List<Account> LoadAccounts()
    {
        List<Account> accounts = new List<Account>();

        try
        {
            if (!File.Exists(file))
            {
                XDocument newDoc = new XDocument(
                    new XElement("Accounts")
                );
                newDoc.Save(file);
                return accounts;
            }

            XDocument doc = XDocument.Load(file);

            foreach (XElement accountElement in doc.Root.Elements("Account"))
            {
                string login = accountElement.Element("Login")?.Value;
                string password = accountElement.Element("Password")?.Value;

                if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
                {
                    accounts.Add(new Account { 
                        Login = login,
                        Password = password,
                        TransactionTypeAnk = accountElement.Element("TransactionTypeAnk")?.Value ?? "2",
                        TypeAnk = accountElement.Element("TypeAnk")?.Value ?? "Все",
                        Rooms = accountElement.Element("Rooms")?.Value ?? "все",
                        RentPriceOt = accountElement.Element("RentPriceOt")?.Value ?? "все",
                        RentPriceDo = accountElement.Element("RentPriceDo")?.Value ?? "все",
                        BuyPriceOt = accountElement.Element("BuyPriceOt")?.Value ?? "все",
                        BuyPriceDo = accountElement.Element("BuyPriceDo")?.Value ?? "все"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки аккаунтов: " + ex.Message);
        }

        return accounts;
    }

    public static bool AddAccount(string login, string password)
    {
        try
        {
            List<Account> accounts = LoadAccounts();

            // Проверяем, есть ли уже такой логин
            if (accounts.Any(a => a.Login == login))
            {
                MessageBox.Show("Этот логин уже занят!");
                return false;
            }

            XDocument doc = XDocument.Load(file);
            doc.Root.Add(
                new XElement("Account",
                    new XElement("Login", login),
                    new XElement("Password", password),
                    new XElement("TransactionTypeAnk", "2"),
                    new XElement("TypeAnk", "Все"),
                    new XElement("Rooms", "все"),
                    new XElement("RentPriceOt", "все"),
                    new XElement("RentPriceDo", "все"),
                    new XElement("BuyPriceOt", "все"),
                    new XElement("BuyPriceDo", "все")
                ));
            doc.Save(file);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка сохранения: " + ex.Message);
            return false;
        }
    }
    public static bool UpdateAccount(Account updatedAccount)
    {
        try
        {
            XDocument doc = XDocument.Load(file);

            // Находим аккаунт для обновления
            XElement accountElement = doc.Root.Elements("Account")
                .FirstOrDefault(a => a.Element("Login")?.Value == updatedAccount.Login);

            if (accountElement != null)
            {
                // Обновляем все поля
                accountElement.Element("Password").Value = updatedAccount.Password;
                accountElement.Element("TransactionTypeAnk").Value = updatedAccount.TransactionTypeAnk;
                accountElement.Element("TypeAnk").Value = updatedAccount.TypeAnk;
                accountElement.Element("Rooms").Value = updatedAccount.Rooms;
                accountElement.Element("RentPriceOt").Value = updatedAccount.RentPriceOt;
                accountElement.Element("RentPriceDo").Value = updatedAccount.RentPriceDo;
                accountElement.Element("BuyPriceOt").Value = updatedAccount.BuyPriceOt;
                accountElement.Element("BuyPriceDo").Value = updatedAccount.BuyPriceDo;

                doc.Save(file);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка обновления: " + ex.Message);
            return false;
        }
    }

    public static Account FindAccount(string login)
    {
        List<Account> accounts = LoadAccounts();
        return accounts.FirstOrDefault(a => a.Login == login);
    }
}