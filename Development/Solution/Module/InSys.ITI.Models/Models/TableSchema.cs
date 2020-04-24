using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InSys.ITI.Models.Models
{
    [NotMapped]
    public class TableSchema
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public int Length { get; set; }
        public string DefaultValue { get; set; }
        public bool AllowNull { get; set; }
    }
}
