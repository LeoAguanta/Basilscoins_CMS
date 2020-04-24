using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tExaminations")]
    public class tExaminations
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public int PassingPoints { get; set; }
        public int TotalPoints { get; set; }
        public bool IsActive { get; set; }
        public string Attachment { get; set; }
        public string AttachmentGUID { get; set; }
    }
}
