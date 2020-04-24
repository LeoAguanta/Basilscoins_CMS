using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tReportDataSource_SubReport")]
    public class tReportDataSource_SubReport
    {
        public int ID { get; set; }
        public int ID_ReportDataSource { get; set; }
        public string Name { get; set; }
        public string DataSource { get; set; }
        public int DbConnection { get; set; }
    }
}
