using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
/// <summary>
/// класс данных аккаунта
/// </summary>
public static class AccountManager
{
    private static string file = "Ak.xml"; // файл XML
    /// <summary>
    /// загружает список из XML-файла и возвращает их в виде объектов класса Account
    /// </summary>
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
                        TypeAnk = accountElement.Element("TypeAnk")?.Value ?? "2",
                        Rooms = accountElement.Element("Rooms")?.Value ?? "0",
                        RentPriceOt = accountElement.Element("RentPriceOt")?.Value ?? "0",
                        RentPriceDo = accountElement.Element("RentPriceDo")?.Value ?? "1000000000",
                        BuyPriceOt = accountElement.Element("BuyPriceOt")?.Value ?? "0",
                        BuyPriceDo = accountElement.Element("BuyPriceDo")?.Value ?? "1000000000"
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
    /// <summary>
    /// Добавляем аккаунта
    /// </summary>
    /// <returns></returns>
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
                    new XElement("TypeAnk", "2"),
                    new XElement("Rooms", "0"),
                    new XElement("RentPriceOt", "0"),
                    new XElement("RentPriceDo", "1000000000"),
                    new XElement("BuyPriceOt", "0"),
                    new XElement("BuyPriceDo", "1000000000")
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
    /// <summary>
    /// обновляем данные аккаунта
    /// </summary>
    /// <param name="updatedAccount"></param>
    /// <returns></returns>
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
    /// <summary>
    /// поиск аккаунта по логину
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public static Account FindAccount(string login)
    {
        List<Account> accounts = LoadAccounts();
        return accounts.FirstOrDefault(a => a.Login == login);
    }
}