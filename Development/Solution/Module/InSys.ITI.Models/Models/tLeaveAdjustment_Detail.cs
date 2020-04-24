using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tLeaveAdjustment_Detail")]
    public class tLeaveAdjustment_Detail
    {
        public int Id { get; set; }
        public int ID_LeaveAdjustment { get; set; }
        public int ID_Employee { get; set; }
        public int ID_LeaveType { get; set; }
        public int ID_CreatedBy { get; set; }
        public int ID_ModifiedBy { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public decimal Value { get; set; }
        public string Remarks { get; set; }
    }

    public class vLeaveAdjustment_Detail
    {
        public int Id { get; set; }
        public int ID_LeaveAdjustment { get; set; }
        public int ID_Employee { get; set; }
        public int ID_LeaveType { get; set; }
        public int ID_CreatedBy { get; set; }
        public int ID_ModifiedBy { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public decimal Value { get; set; }
        public string Remarks { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveType { get; set; }
    }
}
