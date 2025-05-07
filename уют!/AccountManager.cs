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
                // Если файла нет — создаём новый XML
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
                    accounts.Add(new Account { Login = login, Password = password });
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
                    new XElement("Password", password)
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

    // Новый метод для поиска аккаунта по логину
    public static Account FindAccount(string login)
    {
        List<Account> accounts = LoadAccounts();
        return accounts.FirstOrDefault(a => a.Login == login);
    }
}