using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class OrgChart
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ID_Parent { get; set; }
        public int Type { get; set; }
        public int? ID_Type { get; set; }
        public string Description { get; set; }
    }
}
