using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tHourType")]
    public partial class tHourType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsForApproval { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
    }
    [NotMapped]
    public class vHourType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsForApproval { get; set; }
        public bool? IsActive { get; set; }
    }
}
