using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPersonaCharacterReference")]
    public partial class tPersonaCharacterReference
    {
        public int ID { get; set; }
        public int ID_Persona { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public string ContactNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime DatetimeCreated { get; set; }
        public int? ID_UserCreatedBy { get; set; }
    }
}
