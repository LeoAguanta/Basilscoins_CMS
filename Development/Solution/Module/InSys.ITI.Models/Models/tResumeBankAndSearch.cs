using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tResumeBankAndSearch")]
    public class tResumeBankAndSearch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PositionApplied { get; set; }
        public int? YearsOfExperience { get; set; }
        public string LastPosition { get; set; }
        public string LastCompany { get; set; }
        public string EducationalAttainment { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
    }
}
