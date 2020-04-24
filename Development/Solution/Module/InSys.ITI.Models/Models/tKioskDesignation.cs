using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    //[Table("tDesignation")]
    [Table("tKioskDesignation")]
    public class tKioskDesignation
    {
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int ID_Company { get; set; }
        public int? ID_EducationalAttainment { get; set; }
        public string JobSummary { get; set; }
        public string Requirements { get; set; }
        public string WorkExperience { get; set; }
        public string RequiredLicenses { get; set; }
        public string Qualifications { get; set; }

    }
}
