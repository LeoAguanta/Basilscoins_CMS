using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{

    [Table("tRecruitmentStatus")]
    public class tRecruitmentStatus
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool IsSystemGenerated { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class vRecruitmentStatus {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }


}
