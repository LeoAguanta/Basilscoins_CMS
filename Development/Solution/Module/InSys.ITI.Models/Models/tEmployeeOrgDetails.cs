using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tEmployeeOrgDetails")]
    public class tEmployeeOrgDetails
    {
        public int ID_Employee { get; set; }
        public int ID_Org { get; set; }
        public int ID_Branch { get; set; }
        public int ID_Division { get; set; }
        public int ID_Department { get; set; }
        public int ID_Section { get; set; }
        public int ID_JobClassGroup { get; set; }
        public int ID_JobClass { get; set; }
        public int ID_Designation { get; set; }
    }
}
