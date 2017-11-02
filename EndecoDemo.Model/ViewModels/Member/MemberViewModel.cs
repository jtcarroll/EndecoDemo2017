using System.Collections.Generic;

namespace EndecoDemo.Models.ViewModels
{
    public class MemberViewModel
    {
        public int MemberId { get; set; }
        public string Email { get; set; }
        public IEnumerable<StockDetailViewModel> Stock {get;set;}
    }
}
