using InSys.Context;
using InSys.Helper;
using InSys.ITI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InSys.ITI.LeaveAdjustment
{
    public class LeaveAdjustmentDb: InSysContext
    {
        public LeaveAdjustmentDb(BrowserSession session) : base(session)
        {
            this.SessionContext = session;
        }

        public DbQuery<CountData> countData { get; set; }
        public DbSet<tLeaveAdjustment> tLeaveAdjustment { get; set; }
        public DbQuery<vLeaveAdjusment> vLeaveAdjusment { get; set; }
        public DbQuery<tLookUpData> tLookUpData { get; set; }
        public DbSet<tLeaveAdjustment_Detail> tLeaveAdjustment_Detail { get; set; }
        public DbQuery<vLeaveAdjustment_Detail> vLeaveAdjustment_Detail { get; set; }


    }
}
