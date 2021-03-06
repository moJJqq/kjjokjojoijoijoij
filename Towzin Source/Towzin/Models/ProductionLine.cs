//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Towzin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductionLine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductionLine()
        {
            this.ProductionLineTechnicalProblem = new HashSet<ProductionLineTechnicalProblem>();
            this.Order = new HashSet<Order>();
        }
    
        public long ID { get; set; }
        public string ProductionLineName { get; set; }
        public string ProductionLineLatinName { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string Creator { get; set; }
        public System.DateTime AddDate { get; set; }
        public string LastModifier { get; set; }
        public System.DateTime LastModificationDate { get; set; }
        public byte[] Version { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionLineTechnicalProblem> ProductionLineTechnicalProblem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
