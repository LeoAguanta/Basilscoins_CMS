using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tMovement_Employee_Detail")]
    public class tMovement_Employee_Detail
    {
        public int ID { get; set; }
        public int ID_Movement_Employee { get; set; }
        public int ID_MovementType { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_UserModifiedBy { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? DateTimeApplied { get; set; }
        public bool IsApplied { get; set; }
    }
    public class vMovement_Employee_Detail
    {
        public int ID { get; set; }
        public int ID_Movement_Employee { get; set; }
        public int ID_MovementType { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_UserModifiedBy { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? DateTimeApplied { get; set; }
        public bool IsApplied { get; set; }
        //view
        public string UserCreatedBy { get; set; }
        public string UserModifiedBy { get; set; }
        public string MovementType { get; set; }

        //
        public string NewBranch { get; set; }
        public string NewDepartment { get; set; }
        public string NewDesignation { get; set; }
        public string OldBranch { get; set; }
        public string OldDepartment { get; set; }
        public string OldDesignation { get; set; }
        public string OldEmployeeStatus { get; set; }
        public string OldLeaveParameter { get; set; }

    }
}
