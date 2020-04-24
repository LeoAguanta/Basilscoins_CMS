﻿using InSys.Context;
using InSys.Helper;
using InSys.ITI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InSys.ITI.Leave
{
    public class LeaveDb: InSysContext
    {
        public LeaveDb(BrowserSession Session) : base(Session)
        {
            this.SessionContext = Session;
        }
        public DbSet<tLeave> tLeave { get; set; }
        public DbQuery<vLeave> vLeave { get; set; }
        public DbSet<tLeave_Detail> tLeave_Detail { get; set; }
        public DbQuery<CountData> CountData { get; set; }
        public DbSet<tPayrollItem> tPayrollItem { get; set; }
        public DbSet<tFilingStatus> tFilingStatus { get; set; }
    }
}
