using System;

namespace EndecoDemo.Models.Stock
{
    public class StockDetailModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int StockHeaderId { get; set; }
    }
}
