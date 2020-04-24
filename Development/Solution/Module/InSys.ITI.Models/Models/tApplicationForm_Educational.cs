using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tApplicationForm_Educational")]
    public class tApplicationForm_Educational
    {
        public int ID { get; set; }
        public int ID_ApplicationForm { get; set; }
        public int? ID_EducationAttainmentStatus { get; set; }
        public string SchoolAttended { get; set; }
        public string CourseDegree { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DatetimeCreated { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
    }
    [NotMapped]
    public class vApplicationForm_Educational
    {
        public int ID { get; set; }
        public int ID_ApplicationForm { get; set; }
        public int? ID_EducationAttainmentStatus { get; set; }
        public string SchoolAttended { get; set; }
        public string CourseDegree { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DatetimeCreated { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }
    }
}
