//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EndecoDemo.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class StockDetail
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int StockHeaderId { get; set; }
    
        public virtual StockHeader StockHeader { get; set; }
    }
}
