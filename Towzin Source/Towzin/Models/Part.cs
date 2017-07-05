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
    
    public partial class Part
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Part()
        {
            this.OrderPart = new HashSet<OrderPart>();
            this.Part1 = new HashSet<Part>();
            this.PartFrame = new HashSet<PartFrame>();
            this.PartGroupMember = new HashSet<PartGroupMember>();
            this.PartPackageDetail = new HashSet<PartPackageDetail>();
            this.PartSpecification = new HashSet<PartSpecification>();
            this.ProductionLineTechnicalProblem = new HashSet<ProductionLineTechnicalProblem>();
            this.ProductiveDetails = new HashSet<ProductiveDetails>();
            this.PartPackage = new HashSet<PartPackage>();
        }
    
        public long ID { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string PartCode { get; set; }
        public Nullable<long> PartWesteID { get; set; }
        public long NatureID { get; set; }
        public Nullable<double> Length { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Weight { get; set; }
        public long MajorUntiID { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string Creator { get; set; }
        public System.DateTime AddDate { get; set; }
        public string LastModifier { get; set; }
        public System.DateTime LastModificationDate { get; set; }
        public byte[] Version { get; set; }
    
        public virtual Nature Nature { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPart> OrderPart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Part> Part1 { get; set; }
        public virtual Part Part2 { get; set; }
        public virtual Unit Unit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartFrame> PartFrame { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartGroupMember> PartGroupMember { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartPackageDetail> PartPackageDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartSpecification> PartSpecification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionLineTechnicalProblem> ProductionLineTechnicalProblem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductiveDetails> ProductiveDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartPackage> PartPackage { get; set; }
    }
}