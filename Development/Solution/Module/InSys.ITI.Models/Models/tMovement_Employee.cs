using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tMovement_Employee")]
    public class tMovement_Employee
    {
        public int ID { get; set; }
        public int ID_Movement { get; set; }
        public int ID_Employee { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_UserModifiedBy { get; set; }
    }
    public class vMovement_Employee
    {
        public int ID { get; set; }
        public int ID_Movement { get; set; }
        public int ID_Employee { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_UserModifiedBy { get; set; }
        //view
        public string UserCreatedBy { get; set; }
        public string UserModifiedBy { get; set; }
        public string Employee { get; set; }
    }
    public class Movement_Employee_Data
    {
        public int ID { get; set; }
        public int ID_Movement { get; set; }
        public int ID_Employee { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_UserModifiedBy { get; set; }
        public List<tMovement_Employee_Detail> MovementType { get; set; } = new List<tMovement_Employee_Detail>();
    }
}
