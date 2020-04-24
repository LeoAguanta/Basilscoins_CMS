using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tOrgType")]
    public partial class tOrgType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int ID_UserCreatedBy { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int ID_UserModifiedBy { get; set; }
    }

    [NotMapped]
    public class vOrgType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int ID_UserCreatedBy { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_UserModifiedBy { get; set; }
    }
}
