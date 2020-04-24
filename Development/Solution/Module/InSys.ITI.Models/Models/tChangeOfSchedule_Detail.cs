using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [Table("tChangeOfSchedule_Detail")]
    public class tChangeOfSchedule_Detail
    {
        public int ID { get; set; }
        public int ID_ChangeOfSchedule { get; set; }
        public int ID_ShiftSchedule_Old { get; set; }
        public int ID_ShiftSchedule_New { get; set; }
        public int ID_DayType_Old { get; set; }
        public int ID_DayType_New { get; set; }
        public DateTime WorkDate { get; set; }
        public string Comment { get; set; }
    }
}
