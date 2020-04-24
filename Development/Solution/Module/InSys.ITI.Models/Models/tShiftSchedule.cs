using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tShiftSchedule")]
    public partial class tShiftSchedule
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal WorkingHours { get; set; }
        public int WorkingMinutes { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int StartMinute { get; set; }
        public int EndMinute { get; set; }
        public DateTime? FirstHalfTimeOut { get; set; }
        public DateTime? SecondHalfTimeIn { get; set; }
        public int? FirstHalfMinuteOut { get; set; }
        public int? SecondHalfMinuteIn { get; set; }
        public decimal? FirstHalfWorkingHours { get; set; }
        public int? FirstHalfWorkingMinutes { get; set; }
        public decimal? SecondHalfWorkingHours { get; set; }
        public int? SecondHalfWorkingMinutes { get; set; }
        public bool Flexible { get; set; }
        public bool FirstInLastOut { get; set; }
        //public int NDAMStartMinute { get; set; }
        //public int NDAMEndMinute { get; set; }
        //public int NDPMStartMinute { get; set; }
        //public int NDPMEndMinute { get; set; }
        public int ID_Company { get; set; }
        public bool IsActive { get; set; }
    }
    [NotMapped]
    public class vShiftSchedule
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal WorkingHours { get; set; }
        public int WorkingMinutes { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int StartMinute { get; set; }
        public int EndMinute { get; set; }
        public DateTime? FirstHalfTimeOut { get; set; }
        public DateTime? SecondHalfTimeIn { get; set; }
        public int? FirstHalfMinuteOut { get; set; }
        public int? SecondHalfMinuteIn { get; set; }
        public decimal? FirstHalfWorkingHours { get; set; }
        public int? FirstHalfWorkingMinutes { get; set; }
        public decimal? SecondHalfWorkingHours { get; set; }
        public int? SecondHalfWorkingMinutes { get; set; }
        public bool Flexible { get; set; }
        public bool FirstInLastOut { get; set; }
        //public int NDAMStartMinute { get; set; }
        //public int NDAMEndMinute { get; set; }
        //public int NDPMStartMinute { get; set; }
        //public int NDPMEndMinute { get; set; }
        public int ID_Company { get; set; }
        public bool IsActive { get; set; }
        public string Company { get; set; }
    }
}
