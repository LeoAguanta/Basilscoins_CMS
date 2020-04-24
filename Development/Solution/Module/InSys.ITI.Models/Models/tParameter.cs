using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tParameter")]
    public partial class tParameter
    {
        public int ID { get; set; }
        public int? ID_TaxComputation { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal DaysPerYear { get; set; }
        public decimal HoursPerDay { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal FirstHalfMonthlyRate { get; set; }
        public decimal SecondHalfMonthlyRate { get; set; }
        public decimal MonthsPerYear { get; set; }
        public bool CompressedWorkWeek { get; set; }
        public decimal MinTakeHomePayPerc { get; set; }
        public decimal MinTakeHomePayAmt { get; set; }
        public int? ID_Company { get; set; }
    }
    
    public class vParameter
    {
        public int ID { get; set; }
        public int? ID_TaxComputation { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal DaysPerYear { get; set; }
        public decimal HoursPerDay { get; set; }
        public bool? IsActive { get; set; }
        public string Comment { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public decimal FirstHalfMonthlyRate { get; set; }
        public decimal SecondHalfMonthlyRate { get; set; }
        public decimal MonthsPerYear { get; set; }
        public bool CompressedWorkWeek { get; set; }
        public decimal MinTakeHomePayPerc { get; set; }
        public decimal MinTakeHomePayAmt { get; set; }
        public int? ID_Company { get; set; }
        //view
        public string Company { get; set; }
        public string TaxComputation { get; set; }
    }
}
