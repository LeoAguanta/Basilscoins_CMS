using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tScheduleFile")]
    public partial class tScheduleFile
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ID_Company { get; set; }
        public DateTime StartDate { get; set; }
        public bool? IsDefault { get; set; }
        public int? ID_FilingStatus { get; set; }
        public int? ApproverStatus { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApproverComment { get; set; }
        public string ApprovalHistory { get; set; }
        public bool? IsPosted { get; set; }
        public bool IsApplied { get; set; }
        public DateTime? DateApplied { get; set; }
        public DateTime DateCreated { get; set; }
        public string GUID { get; set; }
    }

    [NotMapped]
    public partial class vScheduleFile {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ID_Company { get; set; }
        public DateTime? StartDate { get; set; }
        public int ID_FilingStatus { get; set; }
        public bool IsDefault { get; set; }
        public bool IsPosted { get; set; }
        public bool IsApplied { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? DateApplied { get; set; }
    }
}
