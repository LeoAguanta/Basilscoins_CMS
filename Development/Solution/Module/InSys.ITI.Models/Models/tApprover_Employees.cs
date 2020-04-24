using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tApprover_Employees")]
    public partial class tApprover_Employees
    {
        public int ID { get; set; }
        public int ID_Approver { get; set; }
        public int ID_Employee { get; set; }
    }
    [NotMapped]
    public class vApprover_Employees
    {
        public int ID { get; set; }
        public int ID_Approver { get; set; }
        public string Name { get; set; }
        public int ID_Employee { get; set; }
        public bool IsChecked { get; set; }
    }
    [NotMapped]
    public class vApprover_OrgList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Master { get; set; }
        public int? ID_Parent { get; set; }
        public string Parent { get; set; }
    }
}
