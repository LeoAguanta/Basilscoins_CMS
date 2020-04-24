using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tLeaveAdjustment")]
    public class tLeaveAdjustment
    {
        public int ID { get; set; }
        public string RefNum { get; set; }
        public DateTime ReferenceDate { get; set; }
        public string Name { get; set; }
        public int ID_Company { get; set; }
        public int ID_LeaveSource { get; set; }
        public int ID_CreatedBy { get; set; }
        public int ID_ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsPosted { get; set; }
        public bool IsLocked { get; set; }
        public string Description { get; set; }
    }

    public class vLeaveAdjusment{
        public int ID { get; set; }
        public string RefNum { get; set; }
        public string Name { get; set; }
        //public string LeaveSource { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Description { get; set; }
        public int ID_CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsPosted { get; set; }
        public bool IsLocked { get; set; }
        public int ID_Company { get; set; }

    }
}
