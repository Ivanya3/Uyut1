
using System;
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// класс аккаунт
/// </summary>
public class Account
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string TransactionTypeAnk { get; set; } = "2";
    public string TypeAnk { get; set; } = "Все";
    public string Rooms { get; set; } = "все";
    public string RentPriceOt { get; set; } = "0";
    public string RentPriceDo { get; set; } = "100000000000000000";
    public string BuyPriceOt { get; set; } = "0";
    public string BuyPriceDo { get; set; } = "10000000000000";
}