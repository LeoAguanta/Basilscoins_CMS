using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{

    [Table("tOB")]
    public class tOB
    {
        public int Id { get; set; }
        public int ID_Company { get; set; }
        public int ID_Employee { get; set; }
        public int ID_FilingStatus { get; set; }
        public int ApproverLevel { get; set; }
        public int ID_CreatedBy { get; set; }
        public int ID_ModifiedBy { get; set; }
        public DateTime FiledDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsPosted { get; set; }
        public string RefNum { get; set; }
        public string Reasons { get; set; }
    }

    public class vOB
    {
        public int ID { get; set; }
        public string RefNum { get; set; }
        public string EmployeeName { get; set; }
        public string FiledDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reasons { get; set; }
        public int ID_Company { get; set; }
        public string Status { get; set; }
        public string LastModifiedBy { get; set; }
        public string ModifiedAt { get; set; }
    }
}
