using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tFacultyLoadParameter")]
    public class tFacultyLoadParameter
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public decimal? MinimumLoad { get; set; }
        public decimal? MaximumLoad { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}
