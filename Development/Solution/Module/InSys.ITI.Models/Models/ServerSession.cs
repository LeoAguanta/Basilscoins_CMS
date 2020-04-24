using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class ServerSession
    {
        public int ID_Users { get; set; }
        public int? ID_Employee { get; set; }
        public int ID_Roles { get; set; }
        public int? ID_Company { get; set; }
    }
}
