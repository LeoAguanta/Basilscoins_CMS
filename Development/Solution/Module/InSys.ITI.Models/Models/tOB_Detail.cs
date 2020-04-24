using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tOB_Detail")]
    public class tOB_Detail
    {
        public int Id { get; set; }
        public int ID_OB { get; set; }
        public float StartTimeInMinute { get; set; }
        public float EndTimeInMinute { get; set; }
        public int ID_CreatedBy { get; set; }
        public int ID_ModifiedBy { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Reasons { get; set; }
    }
}
