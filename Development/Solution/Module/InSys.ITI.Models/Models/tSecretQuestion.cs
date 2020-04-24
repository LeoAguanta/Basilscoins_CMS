using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tSecretQuestion")]
    public class tSecretQuestion
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool? IsActive { get; set; }
    }
}
