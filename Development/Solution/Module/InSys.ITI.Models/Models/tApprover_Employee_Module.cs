using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApprover_Employee_Module")]
    public class tApprover_Employee_Module
    {
        public int ID { get; set; }
        public int ID_Employee { get; set; }
        public int ID_FilingModules { get; set; }
        public bool? IsActive { get; set; }
    }
    [NotMapped]
    public class vApprover_Employee_Module
    {
        public int ID { get; set; }
        public int? ID_Employee { get; set; }
        public int ID_FilingModules { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<vApprover_Employee_Module_Approvers> Default { get; set; }
    }
    [NotMapped]
    public class ApproverEmployeeModuleData
    {
        public int ID { get; set; }
        public int ID_Employee { get; set; }
        public int ID_FilingModules { get; set; }
        public bool? IsActive { get; set; }

        [NotMapped]
        public List<tApprover_Employee_Module_Approvers> Default { get; set; }

        [NotMapped]
        public List<int> DeletedApprovers { get; set; }
    }
}
