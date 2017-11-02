using System;
using System.Collections.Generic;
using System.Text;

namespace EndecoDemo.Models.ViewModels.Stock
{
    public class StockChartViewModel
    {
        public IEnumerable<StockDetailViewModel> Stock { set; get; }
        public StockDetailViewModel MinPrice { get; set; }
        public StockDetailViewModel MaxPrice { get; set; }
        public string MostCostlyHour { get; set; }
    }
}
