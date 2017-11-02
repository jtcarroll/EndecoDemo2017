using System;

namespace EndecoDemo2017.ViewModels.Stock
{
    public class StockDetailViewModel
    {
        public int MemberId { set; get; }
        public DateTime DateUploaded { get; set; }
        public decimal MarketPrice { get; set; }
        public string StockType { get; set; }
        public StockDetailViewModel[] Stock { get; set; }
    }
}
