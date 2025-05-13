
using System;
using System.Collections.Generic;
using System.Xml.Linq;

public class Account
{
    public string Login { get; set; }
    public string Password { get; set; }
    //далее идут свойства для анкетирования
    public string TransactionTypeAnk { get; set; } = "2";//аренда или покупка (по умолчанию 2)
    public string TypeAnk { get; set; } = "Все";//дом или квартира (по умолчанию все)
    public string Rooms { get; set; } = "все";// количество комнат (по умолчанию все)
    public string RentPriceOt { get; set; } = "все";//минимальная цена аренды
    public string RentPriceDo { get; set; } = "все";//максимальная цена аренды
    public string BuyPriceOt { get; set; } = "все";//минимальная цена покупки
    public string BuyPriceDo { get; set; } = "все";//максимальная цена покупки
}