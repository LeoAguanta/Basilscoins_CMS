using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApprovalHistory")]
    public class tApprovalHistory
    {
        public int ID { get; set; }
        public int ID_Menus { get; set; }
        public int ID_Reference { get; set; }
        public int ID_Employee { get; set; }
        public string ApproverComment { get; set; }
        public string ApprovalAction { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
        public int ApprovalLevel { get; set; } = 1;
        public int ID_TargetEmployee { get; set; }
    }
    [NotMapped]
    public class vApprovalHistory
    {
        public int ID { get; set; }
        public int ID_Menus { get; set; }
        public int ID_Reference { get; set; }
        public int ID_Employee { get; set; }
        public string ApproverComment { get; set; }
        public string ApprovalAction { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public int ApprovalLevel { get; set; } = 1;
        public int ID_TargetEmployee { get; set; }
        public string Approver { get; set; }
        public string Employee { get; set; }
    }
}
