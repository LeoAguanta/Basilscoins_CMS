using InSys.Context;
using InSys.Helper;
using InSys.HRMS.Models;
using InSys.ITI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InSys.ITI.EmployeeRecord
{
    public class EmployeeRecordDb : InSysContext
    {
        public EmployeeRecordDb(BrowserSession Session) : base(Session)
        {
            this.SessionContext = Session;
        }

        public DbQuery<CountData> countDatas { get; set; }
        public DbSet<tPersona> tPersonas { get; set; }
        public DbSet<tPersonaAddress> tPersonaAddresses { get; set; }
        public DbQuery<vPersona> vPersonas { get; set; }
        public DbQuery<vPersonaEducationalBackGround> vPersonaEducationalBackGrounds { get; set; }
        public DbSet<tPersonaEmployment> tPersonaEmployments { get; set; }
        public DbQuery<tLookUpData> tLookUpDatas { get; set; }
        public DbSet<tPersonaEducationalBackGround> tPersonaEducationalBackGrounds { get; set; }
        public DbQuery<vPersonaAddress> vPersonaAddresses { get; set; }
        public DbQuery<vEmployee> vEmployees { get; set; }
        public DbQuery<vPersonaLookUp> vPersonaLookUp { get; set; }
        public DbQuery<vEmployeeRecordList> vEmployeeRecordList { get; set; }
    }
}
