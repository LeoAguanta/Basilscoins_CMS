using InSys.Context;
using InSys.Helper;
using InSys.ITI.Controller;
using InSys.ITI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using z.Data;

namespace InSys.ITI.TestModule
{
    public class TestModule : BaseModule
    {
        public override BaseModule Initialize(BrowserSession _Session, Pair _Parameter)
        {
            return new TestModule(_Session, _Parameter);
        }

        public TestModule()
        {

        }


        public TestModule(BrowserSession _Session, Pair _Parameter)
        {
            this.Session = _Session;
            this.Parameter = _Parameter;
        }

        public override ReturnSet LoadList()
        {
            var r = new ReturnSet();
            try
            {
                TableOptions to = Parameter["data"].ToObject<TableOptions>();
                using (var db = new TestModuleDb(Session))
                {
                    var data = db.QueryTable<tTestModule>("(select * from dbo.tTestModule)a", to);
                    r = new ReturnSet()
                    {
                        Data = new { Total = data.Count, Rows = data.Data.ToList() },
                        Type = ReturnType.Result
                    };

                    return r;
                }



            }
            catch (Exception ex)
            {

                return ExceptionLogger(ex, Session);
            }

        }
        public override ReturnSet LoadForm()
        {

            var r = new ReturnSet();
            try
            {
                using (var db = new TestModuleDb(Session))
                {
                    var data = db.Single<vTestModule>("select * from dbo.vTestModule where ID = {0}", Parameter["ID"].IsNull(0).ToInt32());

                    

                    return new ReturnSet() {

                        Data = new
                        {
                            Form = data.IsNull(new tTestModule()),
                            Schema = Helpers.GetSchema("tTestModule")
                        },
                        Type = ReturnType.Result
                        
                    };
                }



            }
            catch (Exception ex)
            {

                return ExceptionLogger(ex, Session);
            }




        }
        public override ReturnSet Save()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new TestModuleDb(Session))
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            tTestModule tmm = Parameter["Data"].ToObject<tTestModule>();
                            if (db.Any("(select * from dbo.tTestModule where ID = {0})a", tmm.ID)) db.Update(tmm);
                            else db.Add(tmm);
                            db.SaveChanges(true);

                            var dependents = Parameter["Dependents"].ToObject<List<tTestModule_Dependent>>();
                            foreach (var detail in dependents)
                            {
                                if (db.Any("(select * from dbo.tTestModule_Dependent where ID = {0})a", detail.ID)) db.Update(detail);
                                else db.Add(detail);
                            }
                            db.SaveChanges(true);
                            tran.Commit();

                            return new ReturnSet() { Data = tmm.ID, Type = ReturnType.Result };
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public override ReturnSet DeleteRecord()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new TestModuleDb(Session))
                {

                    List<int> ids = Parameter["Data"].ToObject<List<int>>();

                    foreach(int id in ids)
                    {
                        var record = db.Single<tTestModule>("select * from dbo.tTestModule where ID = {0}", id);
                        if (record != null) db.Remove(record);
                    }
                    db.SaveChanges();

                    return new ReturnSet() { Message = "Record deleted.", Type = ReturnType.Result};
                }



            }
            catch (Exception ex)
            {

                return ExceptionLogger(ex, Session);
            }
        }
        public override ReturnSet LoadLookup()
        {
            try
            {
                string lookupName = Parameter["LookupName"].ToString().ToLower();
                switch (lookupName)
                {
                    case "gender":
                        return LoadGender();
                    default:
                        throw new Exception("Lookup not found.");
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }




        public virtual ReturnSet LoadGender()
        {
            var to = Parameter["data"].ToObject<TableOptions>();
            using (var db = new TestModuleDb(Session))
            {
                var data = db.QueryTable<tLookUpData>("(select ID, Name, IsActive from dbo.tGender)a", to);
                return new ReturnSet() { Data = new { Total = data.Count, Rows = data.Data.ToList() }, Type = ReturnType.Result };
            }
        }


        public override ReturnSet LoadDetail()
        {
            string DetailName = Parameter["DetailName"].ToString().ToLower();
            try
            {
                switch (DetailName)
                {
                    case "dependents":
                        return LoadDependents();
                    default:
                        throw new Exception("Detail not found.");
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }

        public virtual ReturnSet LoadDependents()
        {
            TableOptions to = Parameter["data"].ToObject<TableOptions>();
            using (var db = new TestModuleDb(Session))
            {
                var data = db.QueryTable<tTestModule_Dependent>("(select * from dbo.tTestModule_Dependent where ID_TestModule = {0})a", to, Parameter["ID_MyModule"].ToInt32());
                return new ReturnSet() { Data = new { Total = data.Count, Rows = data.Data.ToList() }, Type = ReturnType.Result };
            }
        }


    }

    public class TestModuleDb : InSysContext
    {
        public TestModuleDb(BrowserSession Session) : base(Session)
        {
            this.SessionContext = Session;
        }

        public DbSet<tTestModule> tTestModule { get; set; }
        public DbSet<vTestModule> vTestModule { get; set; }
        public DbQuery<CountData> CountData { get; set; }
        public DbQuery<tLookUpData> tLookUpData { get; set; }

        public DbSet<tTestModule_Dependent> tTestModule_Dependent { get; set; }
    }
}