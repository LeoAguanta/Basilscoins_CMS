using System;
using System.Collections.Generic;
using System.Text;

namespace InSys.Helper
{
    public class TableOptions : ICloneable
    {
        public string SortName { get; set; }
        public string SortDirection { get; set; }
        public int? Page { get; set; }
        public int? Count { get; set; }
        public List<FilterSchema> Filters { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
