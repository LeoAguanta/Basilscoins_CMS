using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tCompanyOrgType")]
    public partial class tCompanyOrgType
    {
        public int ID { get; set; }
        public int ID_OrgType { get; set; }
        public int? ID_Company { get; set; }
        public int SeqNo { get; set; }
    }
    [NotMapped]
    public class vCompanyOrgType
    {
        public int ID { get; set; }
        public int ID_OrgType { get; set; }
        public string OrgType { get; set; }
        public int ID_Company { get; set; }
        public string Company { get; set; }
        public int SeqNo { get; set; }

    }
}
