using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tUpcommingEvents")]
    public class tUpcommingEvents
    {
        public int ID { get; set; }
        public int ID_Company { get; set; }
        public int ID_CreatedBy { get; set; }
        public int ID_ModifiedBy { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ShowOn { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DateTime? ShowUntil { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
    }

    public class vUpcommingEvents
    { 
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Attachment { get; set; }
    
    }
}
