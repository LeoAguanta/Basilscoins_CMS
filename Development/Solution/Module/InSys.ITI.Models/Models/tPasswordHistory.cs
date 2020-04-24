using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPasswordHistory")]
    public class tPasswordHistory
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public int ID_User { get; set; }
    }
}
