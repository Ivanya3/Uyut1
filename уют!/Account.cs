using System;
using System.Text.RegularExpressions;

public class Account
{
    // Поля для хранения данных
    private string _login;
    private string _password;
    private string _transactionTypeAnk = "2"; // по умолчанию "аренда"
    private string _typeAnk = "Все"; // по умолчанию "дом или квартира"
    private string _rooms = "все"; // количество комнат
    private string _rentPriceOt = "все"; // минимальная цена аренды
    private string _rentPriceDo = "все"; // максимальная цена аренды
    private string _buyPriceOt = "все"; // минимальная цена покупки
    private string _buyPriceDo = "все"; // максимальная цена покупки

    // Свойство для логина
    public string Login
    {
        get => _login;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Логин не может быть пустым.");
            if (value.Length < 3 || value.Length > 20)
                throw new ArgumentException("Длина логина должна быть от 3 до 20 символов.");
            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("Логин может содержать только буквы (a-z), цифры (0-9) и символ '_'.");
            _login = value;
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Пароль не может быть пустым.");
            if (value.Length < 8 || value.Length > 32)
                throw new ArgumentException("Длина пароля должна быть от 8 до 32 символов.");
            if (value == _login)
                throw new ArgumentException("Пароль не должен совпадать с логином.");
            _password = value;
        }
    }

    public string TransactionTypeAnk
    {
        get => _transactionTypeAnk;
        set
        {
            if (value != "1" && value != "2")
                throw new ArgumentException("Неверное значение типа сделки. Допустимые значения: '1' (покупка) или '2' (аренда).");
            _transactionTypeAnk = value;
        }
    }

    public string TypeAnk
    {
        get => _typeAnk;
        set
        {
            if (value != "дом" && value != "квартира" && value != "все")
                throw new ArgumentException("Неверное значение типа недвижимости. Допустимые значения: 'Дом', 'Квартира' или 'Все'.");
            _typeAnk = value;
        }
    }

    // Количество комнат
    public string Rooms
    {
        get => _rooms;
        set
        {
            if (!Regex.IsMatch(value, @"^(все|[1-9]\d*)$"))
                throw new ArgumentException("Неверное значение количества комнат. Допустимые значения: 'все' или целое число больше 0.");
            _rooms = value;
        }
    }

    // Минимальная цена аренды
    public string RentPriceOt
    {
        get => _rentPriceOt;
        set
        {
            if (!Regex.IsMatch(value, @"^(все|[1-9]\d*)$"))
                throw new ArgumentException("Неверное значение минимальной цены аренды. Допустимые значения: 'все' или целое число больше 0.");
            _rentPriceOt = value;
        }
    }

    // Максимальная цена аренды
    public string RentPriceDo
    {
        get => _rentPriceDo;
        set
        {
            if (!Regex.IsMatch(value, @"^(все|[1-9]\d*)$"))
                throw new ArgumentException("Неверное значение максимальной цены аренды. Допустимые значения: 'все' или целое число больше 0.");
            _rentPriceDo = value;
        }
    }

    // Минимальная цена покупки
    public string BuyPriceOt
    {
        get => _buyPriceOt;
        set
        {
            if (!Regex.IsMatch(value, @"^(все|[1-9]\d*)$"))
                throw new ArgumentException("Неверное значение минимальной цены покупки. Допустимые значения: 'все' или целое число больше 0.");
            _buyPriceOt = value;
        }
    }

    // Максимальная цена покупки
    public string BuyPriceDo
    {
        get => _buyPriceDo;
        set
        {
            if (!Regex.IsMatch(value, @"^(все|[1-9]\d*)$"))
                throw new ArgumentException("Неверное значение максимальной цены покупки. Допустимые значения: 'все' или целое число больше 0.");
            _buyPriceDo = value;
        }
    }
}