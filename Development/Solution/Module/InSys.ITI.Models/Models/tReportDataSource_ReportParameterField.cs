using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tReportDataSource_ReportParameterField")]
    public class tReportDataSource_ReportParameterField
    {
        public int ID { get; set; }
        public int? ID_ReportDataSource { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? ID_ReportDataSource_SubReport { get; set; }
    }
}
