using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class UserLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool SelectCompany { get; set; }
        public int InvalidCount { get; set; }
    }
}
