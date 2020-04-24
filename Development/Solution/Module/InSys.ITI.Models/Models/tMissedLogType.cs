using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tMissedLogType")]
    public class tMissedLogType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
