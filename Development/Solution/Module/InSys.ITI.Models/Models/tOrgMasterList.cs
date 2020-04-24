using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tOrgMasterList")]
    public partial class tOrgMasterList
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ID_OrgType { get; set; }
        public bool? IsActive { get; set; }
    }
    [NotMapped]
    public class vOrgMasterList
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string OrgType { get; set; }
        public int ID_OrgType { get; set; }
        public bool? IsActive { get; set; }
    }
}
