using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class RecruitmentStatusCount
    {
        public int Cnt { get; set; }
        public int ID { get; set; }
    }
}
