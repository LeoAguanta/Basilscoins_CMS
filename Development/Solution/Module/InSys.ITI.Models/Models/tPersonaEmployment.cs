using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPersonaEmployment")]
    public partial class tPersonaEmployment
    {
        public int ID { get; set; }
        public int? ID_Persona { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public decimal Salary { get; set; }
        public string ReasonForLeaving { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DatetimeCreated { get; set; } = DateTime.Now;
    }
}
