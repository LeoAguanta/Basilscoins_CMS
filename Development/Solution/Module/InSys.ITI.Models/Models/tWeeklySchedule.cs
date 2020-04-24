using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tWeeklySchedule")]
    public class tWeeklySchedule
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ID_DailyScheduleMon { get; set; }
        public int ID_DailyScheduleTue { get; set; }
        public int ID_DailyScheduleWed { get; set; }
        public int ID_DailyScheduleThu { get; set; }
        public int ID_DailyScheduleFri { get; set; }
        public int ID_DailyScheduleSat { get; set; }
        public int ID_DailyScheduleSun { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
        public int? ID_Company { get; set; }
    }

    [NotMapped]
    public class vWeeklySchedule
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ID_DailyScheduleMon { get; set; }
        public int ID_DailyScheduleTue { get; set; }
        public int ID_DailyScheduleWed { get; set; }
        public int ID_DailyScheduleThu { get; set; }
        public int ID_DailyScheduleFri { get; set; }
        public int ID_DailyScheduleSat { get; set; }
        public int ID_DailyScheduleSun { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
    }
}
