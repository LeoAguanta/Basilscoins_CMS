using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicationForm_CharacterReference")]
    public class tApplicationForm_CharacterReference
    {
        public int ID { get; set; }
        public int ID_ApplicationForm { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public string ContactNo { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DatetimeCreated { get; set; }
        public int? ID_UserCreatedBy { get; set; }
    }
    [NotMapped]
    public class vApplicationForm_CharacterReference
    {
        public int ID { get; set; }
        public int ID_ApplicationForm { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public string ContactNo { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DatetimeCreated { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public bool IsEmploymentReference { get; set; }
    }
}
