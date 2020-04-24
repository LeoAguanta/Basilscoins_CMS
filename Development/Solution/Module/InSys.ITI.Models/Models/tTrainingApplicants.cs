using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tTrainingApplicants")]
    public class tTrainingApplicants
    {
        public int ID { get; set; }
        public int ID_Training { get; set; }
        public int ID_Employee { get; set; }
        public int ID_Status { get; set; }
        public string ReasonForNotAttending { get; set; }
        public int ID_EmployeeFilingStatus { get; set; } = 1;
        public int ApproverLevel { get; set; } = 1;
        public string Attachments { get; set; }
        public int? ID_PostStatus { get; set; }
    }

    [NotMapped]
    public class vTrainingApplicants
    {
        public int ID { get; set; }
        public int ID_Training { get; set; }
        public int ID_Employee { get; set; }
        public string EmployeeName { get; set; }
        public int ID_Status { get; set; }
        public string ReasonForNotAttending { get; set; }
        public int ID_EmployeeFilingStatus { get; set; } = 1;
        public int ApproverLevel { get; set; } = 1;
        public string Status { get; set; }
        public string Attachments { get; set; }
        public int? ID_PostStatus { get; set; }
    }


}
