using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApprover_Module")]
    public class tApprover_Module
    {
        public int ID { get; set; }
        public int ID_Approver { get; set; }
        public int ID_FilingModules { get; set; }
        public bool? IsActive { get; set; }
    }
    [NotMapped]
    public class vApprover_Module
    {
        public int ID { get; set; }
        public int ID_Approver { get; set; }
        public int ID_FilingModules { get; set; }
        public bool? IsActive { get; set; }
        public string Name { get; set; }
    }
    [NotMapped]
    public class ApproverModuleData
    {
        public int ID { get; set; }
        public int ID_Approver { get; set; }
        public int ID_FilingModules { get; set; }
        public bool? IsActive { get; set; }
        [NotMapped]
        public List<tApprover_Default> Default { get; set; }
        [NotMapped]
        public List<int> DeletedApprovers { get; set; }
    }
}
