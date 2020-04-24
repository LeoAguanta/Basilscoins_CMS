using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tMissedLog_Detail")]
    public class tMissedLog_Detail
    {
        public int Id { get; set; }
        public string RefNum { get; set; }
        public int ID_Company { get; set; }
        public int ID_Employee { get; set; }
        public int ID_MissedLog { get; set; }
        public int ID_MissedLogType { get; set; }
        public int ID_CreatedBy { get; set; }
        public int ID_ModifiedBy { get; set; }
        public DateTime LogTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Reasons { get; set; }
    }


    public class vMissedLogDetail
    {
        public int Id { get; set; }
        public string RefNum { get; set; }
        public int ID_Company { get; set; }
        public int ID_Employee { get; set; }
        public string Employee { get; set; }
        public int ID_MissedLog { get; set; }
        public int ID_MissedLogType { get; set; }
        public string MissLogType { get; set; }
        public DateTime LogTime { get; set; }
        public string Remarks { get; set; }
    }
}
