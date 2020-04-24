using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tLeaveSource")]
    public class tLeaveSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
