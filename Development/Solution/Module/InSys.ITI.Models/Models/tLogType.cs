using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tLogType")]
    public class tLogType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
