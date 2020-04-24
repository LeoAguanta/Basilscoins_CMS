using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tReportDataSource")]
    public class tReportDataSource
    {
        public int ID { get; set; }
        public string DataSource { get; set; }
        public string ReportFile { get; set; }
        public string SessionID { get; set; }
        public int DbConnection { get; set; }
        public int ReportType { get; set; }
    }
}
