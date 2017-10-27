using System;

namespace EndecoDemo.Models.Stock
{
    public class StockDetail
    {
        private int _memberId { set; get; }
        private DateTime _date { get; set; }
        private decimal _marketPrice { get; set; }
        private string _stockType { get; set; }
    }
}
