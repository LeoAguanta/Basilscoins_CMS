using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tOrg")]
    public partial class tOrg
    {
        public int ID { get; set; }
        public int ID_Master { get; set; }
        public int? ID_Parent { get; set; }
        public int ID_Company { get; set; }
        public int ID_CompanyOrgType { get; set; }
    }

    [NotMapped]
    public class vOrg 
    {
        public int ID { get; set; }
        public int ID_Master { get; set; }
        public int? ID_Parent { get; set; }
        public string Parent { get; set; }
        public int ID_CompanyOrgType { get; set; }
        public int ID_Company { get; set; }
        public string Company { get; set; }
        public int ID_OrgType { get; set; }
        public string Name { get; set; }
        public string OrgType { get; set; }
        public List<vOrg> Children { get; set; } = new List<vOrg>();
        public bool IsOpen { get; set; } = false;
    }
    [Table("tJobClassApproverCandidates")]
    public partial class tJobClassApproverCandidates
    {
        public int ID { get; set; }
        public int ID_Org { get; set; }
    }
    [NotMapped]
    public class vJobClassApproverCandidates
    {
        public int ID { get; set; }
        public int ID_Org { get; set; }
        public string Name { get; set; }
    }
    [NotMapped]
    public class fOrg {
        public int ID { get; set; }
        public string Name { get; set; }
        public string OrgType { get; set; }
        public int ID_Master { get; set; }
        public int? ID_Parent { get; set; }
        public int ID_CompanyOrgType { get; set; }
        public int SeqNo { get; set; }
        public int ID_Org { get; set; } 
    }
}
