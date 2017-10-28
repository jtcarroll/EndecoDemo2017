using System;

namespace EndecoDemo2017.ViewModels.Stock
{
    public class StockHeaderViewModel
    {
        public string StockType { get; set; }
        public int MemberId { get; set; }
        public StockDetailViewModel[] Stock { set; get; }
        public DateTime DateUpload { get; set; }
    }
}
