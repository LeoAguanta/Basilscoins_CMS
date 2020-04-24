using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tProductLine")]
    public class tProductLine
    {
        public int Id { get; set; }
        public int ID_Company { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ID_CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int ID_ModifiedBy { get; set; }
    }
}
