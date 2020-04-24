using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tUsers")]
    public class tUsers
    {
        public int ID { get; set; }
        public string LogInName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int ID_Roles { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_CreatedBy { get; set; }
        public int? ID_ModifiedBy { get; set; }
        public int? ID_Employee { get; set; }
        public bool? IsFirstLog { get; set; } = true;
        public DateTime? LastPasswordChangeDate { get; set; }
        public DateTime? BlockedDate { get; set; }
        public int? ID_SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public string GUID { get; set; }
        public string ImageFile_GUID { get; set; }
        [NotMapped]
        public string SessionID { get; set; }
    }
    [NotMapped]
    public class vUsers {
        public int ID { get; set; }
        public string LogInName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int ID_Roles { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public int? ID_CreatedBy { get; set; }
        public int? ID_ModifiedBy { get; set; }
        public int? ID_Employee { get; set; }
        public bool? IsFirstLog { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public DateTime? BlockedDate { get; set; }
        public int? ID_SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public string GUID { get; set; }
        public string ImageFile_GUID { get; set; }
        public string Roles { get; set; }
        public string Employee { get; set; }
        public string SecretQuestion { get; set; }
    }



}
