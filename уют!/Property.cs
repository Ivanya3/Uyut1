using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Property//создаем класс для недвижимости
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string City { get; set; }
    public int TransactionType { get; set; }
    public decimal? RentPrice { get; set; } 
    public decimal? BuyPrice { get; set; }  
    public string Description { get; set; }
}