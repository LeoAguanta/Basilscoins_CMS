using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tCompanyAccessRights")]
    public class tCompanyAccessRights
    {
        public int ID { get; set; }
        public int ID_Company { get; set; }
        public int ID_Roles { get; set; }
    }
    [NotMapped]
    public class vCompanyAccessRights
    {
        public int ID { get; set; }
        public int ID_Company { get; set; }
        public int ID_Roles { get; set; }
        public string Company { get; set; }
        public string Roles { get; set; }
        [NotMapped]
        public List<vCompanyAccessRights> Children { get; set; }
        public bool IsChecked { get; set; }
    }
}
