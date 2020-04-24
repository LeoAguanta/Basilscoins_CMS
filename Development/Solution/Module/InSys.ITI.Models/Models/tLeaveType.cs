using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tLeaveType")]
    public class tLeaveType
    {
        public int ID { get; set; }
        public int ID_Company { get; set; }
        public int ID_LeaveApplicableGender { get; set; }
        public int ID_CreatedBy { get; set; }
        public int? ID_ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
        public bool ShowInIONS { get; set; }
        public bool IsSystem { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
    }

    public class vLeaveType
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool ShowInIONS { get; set; }
        public string Remarks { get; set; }
        public int ID_Company { get; set; }
        public string ApplicableGender { get; set; }
    }
}
