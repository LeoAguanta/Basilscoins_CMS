using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tSystemSettings")]
    public class tSystemSettings
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Description { get; set; }
        public int ID_Group { get; set; }
    }
}
