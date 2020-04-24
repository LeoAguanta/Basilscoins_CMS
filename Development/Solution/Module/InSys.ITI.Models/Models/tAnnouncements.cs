using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tAnnouncements")]
    public class tAnnouncements
    {
        public int ID { get; set; }
        public int ID_Company { get; set; }
        public int ID_CreatedBy { get; set; }
        public int? ID_ModifiedBy { get; set; }
        public DateTime ShowOn { get; set; }
        public DateTime? ShowUntil { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
    }

    public class vAnnouncements {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string CreatedBy { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
    }
}
