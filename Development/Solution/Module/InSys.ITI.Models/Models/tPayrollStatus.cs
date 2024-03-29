﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InSys.ITI.Models.Models
{
    [Table("tPayrollStatus")]
    public class tPayrollStatus
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsProcessPayroll { get; set; }
        public string Comment { get; set; }
        public DateTime DateTImeCreated { get; set; }
    }
}
