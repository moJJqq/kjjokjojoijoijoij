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
    
    public partial class VW_ProductionLineTechnicalProblem_Index
    {
        public string ProductionLineName { get; set; }
        public string TechnicalProblemsName { get; set; }
        public string PartCode { get; set; }
        public string Name { get; set; }
        public string OperatorName { get; set; }
        public string DescriptionTechnicalProblem { get; set; }
        public string SolutionTechnicalProblem { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string Creator { get; set; }
        public System.DateTime AddDate { get; set; }
        public string LastModifier { get; set; }
        public System.DateTime LastModificationDate { get; set; }
        public long ID { get; set; }
        public long ProductionLineID { get; set; }
        public long TechnicalProblemID { get; set; }
        public long PartID { get; set; }
        public long OperatorID { get; set; }
    }
}
