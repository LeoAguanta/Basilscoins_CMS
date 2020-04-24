using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class tApproverMatrixPerLevel
    {
        public int ID_Menu { get; set; }
        public int ID_FilingModule { get; set; }
        public int ID_Employee { get; set; }
        public string Employee { get; set; }
        public int ID_Level { get; set; }
        public string Approver1 { get; set; }
        public string Approver2 { get; set; }
        public string Approver3 { get; set; }
        public int ID_Approver1 { get; set; }
        public int ID_Approver2 { get; set; }
        public int ID_Approver3 { get; set; }
        public bool IsPowerApprover { get; set; }
    }
}
