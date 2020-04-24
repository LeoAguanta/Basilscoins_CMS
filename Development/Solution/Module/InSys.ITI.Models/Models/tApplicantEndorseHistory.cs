using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicantEndorseHistory")]
    public class tApplicantEndorseHistory
    {
        public int ID { get; set; }
        public int ID_ApplicationForm { get; set; }
        public int ID_MRFFrom { get; set; }
        public int ID_MRFTo { get; set; }
        public int ID_User { get; set; }
        public DateTime DateTimeMoved { get; set; } = DateTime.Now;
    }
    [NotMapped]
    public class vApplicantEndorseHistory
    {
        public int ID { get; set; }
        public int ID_ApplicationForm { get; set; }
        public int ID_MRFFrom { get; set; }
        public int ID_MRFTo { get; set; }
        public int ID_User { get; set; }
        public DateTime DateTimeMoved { get; set; } = DateTime.Now;
        public string User { get; set; }
    }
}
