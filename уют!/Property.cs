using System;
using System.Text.RegularExpressions;

/// <summary>
/// Класс для недвижимости
/// </summary>
public class Property
{
    // Приватные поля
    private string _id;
    private string _type;
    private string _city;
    private int _transactionType;
    private decimal? _rentPrice;
    private decimal? _buyPrice;
    private string _description;
    public string Id
    {
        get => _id;
        set
        {
            _id = value;
        }
    }
    public string Type
    {
        get => _type;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Тип недвижимости не может быть пустым.");
            if (value != "Дом" && value != "Квартира")
                throw new ArgumentException("Неверный тип недвижимости. Допустимые значения: 'Дом' или 'Квартира'.");
            _type = value;
        }
    }
    public string City
    {
        get => _city;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Город не может быть пустым.");
            if (!Regex.IsMatch(value, @"^[a-zA-Zа-яА-ЯёЁ\s]+$"))
                throw new ArgumentException("Город может содержать только буквы.");
            _city = value;
        }
    }
    public int TransactionType
    {
        get => _transactionType;
        set
        {
            if (value != 0 && value != 1 && value != 2)
                throw new ArgumentException("Неверный тип сделки. Допустимые значения: 0 или 1.");
            _transactionType = value;
        }
    }
    public decimal? RentPrice
    {
        get => _rentPrice;
        set
        {
            if (value.Value < 0)
                throw new ArgumentException("Цена аренды должна быть положительным числом.");
            _rentPrice = value;
        }
    }

    /// <summary>
    /// Цена покупки
    /// </summary>
    public decimal? BuyPrice
    {
        get => _buyPrice;
        set
        {
            if (value.Value < 0)
                throw new ArgumentException("Цена покупки должна быть положительным числом.");
            _buyPrice = value;
        }
    }
    public string Description
    {
        get => _description;
        set
        {
            if (value != null && value.Length > 1000)
                throw new ArgumentException("Описание не может превышать 1000 символов.");
            _description = value;
        }
    }
}