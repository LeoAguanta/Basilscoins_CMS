using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tRoleAccessRights")]
    public class tRoleAccessRights
    {
        public int ID { get; set; }
        public int? ID_Menus { get; set; }
        public int? ID_Roles { get; set; }
        public string AccessRights { get; set; }
    }
    [NotMapped]
    public class vRoleAccessRights
    {
        public int ID { get; set; }
        public int? ID_Menus { get; set; }
        public int? ID_Roles { get; set; }
        public string EncodedRights { get; set; }
        [NotMapped]
        public AccessRights AccessRights { get; set; }
        [NotMapped]
        public List<vRoleAccessRights> Children { get; set; }

        public string Menus { get; set; }
        public string Code { get; set; }
        public string Roles { get; set; }
        public int? ID_Menus_Parent { get; set; }
        public bool IsChecked { get; set; }
        public int ID_MenusType { get; set; } = 1;
    }
    [NotMapped]
    public class AccessRights
    {
        public bool HasView { get; set; } = false;
        public bool HasNew { get; set; } = false;
        public bool HasEdit { get; set; } = false;
        public bool HasDelete { get; set; } = false;
        public bool PostJob { get; set; } = false;
    }
}
