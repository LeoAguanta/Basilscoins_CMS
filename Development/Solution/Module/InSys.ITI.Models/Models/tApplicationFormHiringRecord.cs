using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicationFormHiringRecord")]
    public class tApplicationFormHiringRecord
    {
        public int ID { get; set; }
        public int? ID_ApplicationForm { get; set; }
        public int? ID_ForHiringType { get; set; }
        public string Remarks { get; set; }
        public DateTime? DatePrepared { get; set; }
        public DateTime? DateSigned { get; set; }
        public string Attachment { get; set; }
    }
}
