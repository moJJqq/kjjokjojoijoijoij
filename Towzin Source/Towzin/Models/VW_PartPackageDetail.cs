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
    
    public partial class VW_PartPackageDetail
    {
        public long ID { get; set; }
        public string PackageName { get; set; }
        public string PartCode { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string Creator { get; set; }
        public System.DateTime AddDate { get; set; }
        public string LastModifier { get; set; }
        public System.DateTime LastModificationDate { get; set; }
        public long PartID { get; set; }
        public long PackageID { get; set; }
        public long UnitID { get; set; }
    }
}
