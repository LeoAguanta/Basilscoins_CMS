using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{ 
    [Table("tOvertime")]
    public partial class tOvertime
    {
        public int ID { get; set; }
        public string ReferenceNo { get; set; }
        public int ID_Employee { get; set; }
        public int ID_FilingStatus { get; set; }
        public int ID_WorkCredit { get; set; }
        public DateTime FilingDate { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime StartTime { get; set; }
        public int? StartMinute { get; set; }
        public DateTime EndTime { get; set; }
        public int? EndMinute { get; set; }
        public decimal ComputedHours { get; set; }
        public decimal ConsideredHours { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Reason { get; set; }
        public int CurrentApproverLevel { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApproverComment { get; set; }
        public bool IsPosted { get; set; }
    }
    [NotMapped]
    public class vOvertime
    {
        public int ID { get; set; }
        public string ReferenceNo { get; set; }
        public int ID_Employee { get; set; }
        public int ID_FilingStatus { get; set; }
        public int ID_WorkCredit { get; set; }
        public DateTime FilingDate { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime StartTime { get; set; }
        public int? StartMinute { get; set; }
        public DateTime EndTime { get; set; }
        public int? EndMinute { get; set; }
        public decimal ComputedHours { get; set; }
        public decimal ConsideredHours { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Reason { get; set; }
        public int CurrentApproverLevel { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApproverComment { get; set; }
        public bool IsPosted { get; set; }
        public string FilingStatus { get; set; }
    }
}
