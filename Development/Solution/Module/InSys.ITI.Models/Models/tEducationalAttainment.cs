using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{

    [Table("tEducationalAttainment")]
    public class tEducationalAttainment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int ID_CreatedBy { get; set; }
        public int? ID_ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
    [NotMapped]
    public class vEducationalAttainment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int ID_CreatedBy { get; set; }
        public int? ID_ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }

}
