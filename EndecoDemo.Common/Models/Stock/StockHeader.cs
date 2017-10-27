using System;


namespace EndecoDemo.Models.Stock
{
    public class StockHeader
    {
        public string StockType { get; set; }
        public int MemberId { get; set; }
        public StockDetail[] Stock { set; get; }
        public DateTime DateUpload { get; set; }
    }
}
