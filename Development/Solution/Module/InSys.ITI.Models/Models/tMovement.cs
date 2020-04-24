using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tMovement")]
    public class tMovement
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime EffectivityDate { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_UserModifiedBy { get; set; }
        public bool IsPosted { get; set; }
        public int? ID_Company { get; set; }
    }
    public class vMovement
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime EffectivityDate { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_UserModifiedBy { get; set; }
        public bool IsPosted { get; set; }
        public int ID_Company { get; set; }
        //view
        public string UserCreatedBy { get; set; }
        public string UserModifiedBy { get; set; }
        public string Company { get; set; }
    }
}
