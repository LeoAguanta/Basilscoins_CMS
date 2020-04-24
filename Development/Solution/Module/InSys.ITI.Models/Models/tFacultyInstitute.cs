using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tFacultyInstitute")]
    public class tFacultyInstitute
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int ID_Company { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}
