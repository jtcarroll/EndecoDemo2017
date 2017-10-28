using EndecoDemo.Models.Stock;

namespace EndecoDemo2017.ViewModels.Member
{
    public class MemberViewModel
    {
        public int MemberId { get; set; }
        public string Email { get; set; }
        public StockDetail[] Stock {get;set;}
    }
}
