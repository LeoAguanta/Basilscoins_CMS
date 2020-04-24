using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tShiftSchedule_Detail")]
    public partial class tShiftSchedule_Detail
    {
        public int ID { get; set; }
        public int ID_ShiftSchedule { get; set; }
        public int ID_HourType { get; set; }
        public int Day { get; set; }
        public DateTime StartTime { get; set; }
        public int? StartMinute { get; set; }
        public int? EndMinute { get; set; }
        public decimal Hours { get; set; }
        public DateTime? EndTime { get; set; }
        public int BreakMinutes { get; set; }
        public bool FirstIn { get; set; }
        public bool LastOut { get; set; }
        public int? FlexibleMinutes { get; set; }
        public decimal FlexibleHours { get; set; }
        public bool WithPay { get; set; }
        public bool AutoApprove { get; set; }
        public int? LBoundStartMinute { get; set; }
        public int? UBoundEndMinute { get; set; }
        public int? SeqNo { get; set; }
    }
    [NotMapped]
    public partial class vShiftSchedule_Detail
    {
        public int ID { get; set; }
        public int ID_ShiftSchedule { get; set; }
        public int ID_HourType { get; set; }
        public int Day { get; set; }
        public DateTime StartTime { get; set; }
        public int? StartMinute { get; set; }
        public int? EndMinute { get; set; }
        public decimal Hours { get; set; }
        public DateTime? EndTime { get; set; }
        public int BreakMinutes { get; set; }
        public bool FirstIn { get; set; }
        public bool LastOut { get; set; }
        public int? FlexibleMinutes { get; set; }
        public decimal FlexibleHours { get; set; }
        public bool WithPay { get; set; }
        public bool AutoApprove { get; set; }
        public int? LBoundStartMinute { get; set; }
        public int? UBoundEndMinute { get; set; }
        public int? SeqNo { get; set; }
    }
}
