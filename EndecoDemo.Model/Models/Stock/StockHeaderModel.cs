using System;
using System.Collections.Generic;

namespace EndecoDemo.Models.Stock
{
    public class StockHeaderModel
    {
        public int Id { get; set; }
        public string StockType { get; set; }
        public int MemberId { get; set; }
        public string FileNameUploaded { set; get; }
        public IEnumerable<StockDetailModel> StockDetails { set; get; }
        public DateTime DateUploaded { get; set; }
    }
}
