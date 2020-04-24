using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tEtfMigration")]
    public class tEtfMigration
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
        public DateTime? ProcessFinish { get; set; }
    }
    public class vEtfMigration
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public int? ID_UserCreatedBy { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
        public DateTime? ProcessFinish { get; set; }
        //view
        public string UserCreatedBy { get; set; }
    }
}
