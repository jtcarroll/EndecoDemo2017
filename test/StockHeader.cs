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
    
    public partial class StockHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StockHeader()
        {
            this.StockDetails = new HashSet<StockDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> DateUploaded { get; set; }
        public string StockType { get; set; }
        public int MemberId { get; set; }
        public string FileNameUploaded { get; set; }
    
        public virtual Member Member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockDetail> StockDetails { get; set; }
    }
}
