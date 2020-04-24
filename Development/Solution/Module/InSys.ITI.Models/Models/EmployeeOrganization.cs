using System;
using System.Collections.Generic;
using System.Text;

namespace InSys.ITI.Models.Models
{
    public class EmployeeOrganization
    {
        public int? ID_Designation { get; set; }
        public string Designation { get; set; }
        public int? ID_JobClass { get; set; }
        public string JobClass { get; set; }
        public int? ID_JobClassGroup { get; set; }
        public string JobClassGroup { get; set; }
        public int? ID_Section { get; set; }
        public string Section { get; set; }
        public int? ID_Department { get; set; }
        public string Department { get; set; }
        public int? ID_Division { get; set; }
        public string Division { get; set; }
        public int? ID_Branch { get; set; }
        public string Branch { get; set; }

    }
}
