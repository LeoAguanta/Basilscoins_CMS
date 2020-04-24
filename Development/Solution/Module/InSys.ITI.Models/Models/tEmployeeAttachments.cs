using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tEmployeeAttachments")]
    public class tEmployeeAttachments
    {
        public int Id { get; set; }
        public int ID_Employee { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public string Attachment { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
