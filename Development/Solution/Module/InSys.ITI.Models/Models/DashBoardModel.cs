using System;
using System.Collections.Generic;
using System.Text;

namespace InSys.ITI.Models.Models
{
    public class DashBoardModel
    {
    }
    public class vEmployeeLeaveDetails { 
        public int ID_Employee { get; set; }
        public int ID_LeavePayrollItem { get; set; }
        public string Code { get; set; }
        public double Alloted { get; set; }
        public double Used { get; set; }
        public double EndBalance { get; set; }
    }

    public class vEmployeeTimeKeepingSummary { 
        public int ID_Employee { get; set; }
        public decimal Leave { get; set; }
        public decimal Absent { get; set; }
        public decimal Tardy { get; set; }
        public decimal Undertime { get; set; }
        public decimal Overtime { get; set; }
    
    }
}
