using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class UserAccountModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public int ID_SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public int ID_User { get; set; }
    }
}
