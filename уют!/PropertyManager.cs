using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

public static class PropertyManager// настройки класса недвижимости
{

    public static List<Property> LoadProperties()
    {
        List<Property> properties = new List<Property>(); //создаем переменую по классу Property

        try
        {
            if (!File.Exists("Uyuts.xml"))// проверяем, есть ли файл
                return properties;

            XDocument doc = XDocument.Load("Uyuts.xml"); //скачиваем файл в doc


            foreach (XElement propElement in doc.Root.Elements("Property"))
            {
                decimal rentPrice;//decimal используется, так как rentPrice и buyPrice могут не принимать значение 
                decimal buyPrice;

                bool hasRentPrice = decimal.TryParse(
                    propElement.Element("RentPrice")?.Value,
                    out rentPrice
                );

                bool hasBuyPrice = decimal.TryParse(
                    propElement.Element("BuyPrice")?.Value,
                    out buyPrice
                );

                properties.Add(new Property
                {

                    Type = propElement.Element("Type")?.Value,
                    City = propElement.Element("City")?.Value,
                    TransactionType = int.Parse(propElement.Element("TransactionType")?.Value ?? "0"),
                    RentPrice = hasRentPrice ? rentPrice : (decimal?)null, 
                    BuyPrice = hasBuyPrice ? buyPrice : (decimal?)null,
                    Description = propElement.Element("Description")?.Value
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки: {ex.Message}");
        }

        return properties;
    }
}