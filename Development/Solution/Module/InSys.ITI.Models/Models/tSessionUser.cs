using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tSessionUser")]
    public class tSessionUser
    {
        public int ID { get; set; }
        public int ID_User { get; set; }
        public int ID_Company { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string IPAddress { get; set; }
    }
    [NotMapped]
    public class vSessionUser
    {
        public int ID { get; set; }
        public int ID_User { get; set; }
        public int ID_Company { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string IPAddress { get; set; }
        public string User { get; set; }
        public string Company { get; set; }
    }
}
