using EndecoDemo.Models.Stock;
using System;
using System.Collections.Generic;

namespace EndecoDemo.Models.ViewModels
{
    public class StockHeaderViewModel
    {
        public int Id { get; set; }
        public string StockType { get; set; }
        public int MemberId { get; set; }
        public string FileNameUploaded { set; get; }
        public IEnumerable<StockDetailModel> StockDetails { set; get; }
        public DateTime DateUploaded { get; set; }
    }
}
