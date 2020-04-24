using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tWorkCredit")]
    public partial class tWorkCredit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
    [NotMapped]
    public partial class vWorkCredit
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
