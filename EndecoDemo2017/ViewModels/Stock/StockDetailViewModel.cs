using System;

namespace EndecoDemo2017.ViewModels.Stock
{
    public class StockDetailViewModel
    {
        private int _memberId { set; get; }
        private DateTime _date { get; set; }
        private decimal _marketPrice { get; set; }
        private string _stockType { get; set; }
    }
}
