using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tLeave_Detail")]
    public class tLeave_Detail
    {
        public int Id { get; set; }
        public int ID_Leave { get; set; }
        public int ID_LeaveDayType { get; set; }
        public int ID_CreatedBy { get; set; }
        public int ID_ModifiedBy { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public float Day { get; set; }
        public bool IsPaid { get; set; }
        public string Reasons { get; set; }
    }

}
