using InSys.Helper;
using InSys.ITI.Controller;
using InSys.ITI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using z.Data;

namespace InSys.ITI.LeaveAdjustment
{
    public class LeaveAdjustment: BaseModule
    {
        private string _TableName = "tLeaveAdjustment";
        public override BaseModule Initialize(BrowserSession _Session, Pair _Parameter)
        {
            return new LeaveAdjustment(_Session, _Parameter);
        }
        public LeaveAdjustment(BrowserSession _Session, Pair _Parameter)
        {
            this.Session = _Session;
            this.Parameter = _Parameter;
        }
        public LeaveAdjustment() { }


        public override ReturnSet LoadList()
        {
            try
            {
                var options = Parameter["data"].ToObject<TableOptions>();
                using (var db = new LeaveAdjustmentDb(Session))
                {
                    var data = db.QueryTable<vLeaveAdjusment>(@"(SELECT la.ID, la.RefNum, la.Name, la.[Description], la.ID_CreatedBy, vue.EmployeeName AS CreatedBy, CreatedAt,
	                                                                vue2.EmployeeName AS ModifiedBy, la.ModifiedAt,	la.IsPosted, la.IsLocked, la.ID_Company
                                                                FROM tLeaveAdjustment la
	                                                                LEFT OUTER JOIN vUserEmployee vue ON vue.ID = la.ID_CreatedBy
	                                                                LEFT OUTER JOIN vUserEmployee vue2 ON vue2.ID = la.ID_ModifiedBy
                                                                WHERE la.ID_Company = {0})a", options, Session.ID_Company);
                    return new ReturnSet
                    {
                        Data = new { Total = data.Count, Rows = data.Data.ToList() },
                        Type = ReturnType.Result
                    };
                }

            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }

        public override ReturnSet LoadForm()
        {
            try
            {
                var id = Parameter["ID"].IsNull(0).ToInt32();
                using (var db = new LeaveAdjustmentDb(Session))
                {
                    var record = db.Single<tLeaveAdjustment>(@"SELECT * FROM tLeaveAdjustment WHERE ID = {0}", id);
                    return new ReturnSet
                    {
                        Data = new
                        {
                            Form = record.IsNull(new tLeaveAdjustment()
                            {
                                RefNum = Helpers.getReferenceNumber(_TableName, Session.ID_Company.ToInt32()),
                                ID_Company = Session.ID_Company.ToInt32(),
                                ID_CreatedBy = Session.ID_User,
                                CreatedAt = DateTime.Now,
                                ID_ModifiedBy = Session.ID_User,
                                ReferenceDate = DateTime.Now
                            }),
                            Schema = Helpers.GetSchema(_TableName),
                            DetailSchema = Helpers.GetSchema("tLeaveAdjustment_Detail")
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
            var LeaveAdjustmentData = Parameter["Data"].ToObject<tLeaveAdjustment>();
            var LeaveAdjustmentDetailData = Parameter["Details"].ToObject<List<tLeaveAdjustment_Detail>>();
            var LeaveAdjustmentDeletedData = Parameter["DetailsToDelete"].ToObject<List<int>>();
            int LeaveAdjustmentId = LeaveAdjustmentData.ID;
            int ReturnId = 0;
            try
            {
                using (var db = new LeaveAdjustmentDb(Session))
                {
                    try {
                        db.Database.BeginTransaction();

                        LeaveAdjustmentData.ID_CreatedBy = Session.ID_User;
                        LeaveAdjustmentData.CreatedAt = DateTime.Now;
                        LeaveAdjustmentData.ID_ModifiedBy = Session.ID_User;
                        LeaveAdjustmentData.ModifiedAt = DateTime.Now;
                        LeaveAdjustmentData.ID_Company = Session.ID_Company.ToInt32();

                        LeaveAdjustmentData.RefNum = Helpers.getReferenceNumber(_TableName, Session.ID_Company.ToInt32());

                        if (LeaveAdjustmentId == 0)
                        {
                            db.Add(LeaveAdjustmentData);
                            db.SaveChanges(true);
                            ReturnId = LeaveAdjustmentData.ID;
                            PostLeaveAdjustmentDetails(LeaveAdjustmentDetailData, ReturnId, db);
                        }
                        //Edit
                        else if (LeaveAdjustmentId > 0)
                        {
                            var LeaveAdjustmentToUpdate = db.Single<tLeaveAdjustment>("select * from dbo.tLeaveAdjustment where ID = {0}", LeaveAdjustmentId);

                            if (LeaveAdjustmentToUpdate != null)
                            {
                                db.Update(LeaveAdjustmentData);
                                db.SaveChanges(true);
                                ReturnId = LeaveAdjustmentData.ID;
                                RemoveLeaveAjustmentDetails(LeaveAdjustmentDeletedData, db);
                                PostLeaveAdjustmentDetails(LeaveAdjustmentDetailData, ReturnId, db);
                            }
                            else { throw new Exception("Data does not exists"); }
                        }

                        db.Database.CommitTransaction();
                    }
                    catch (Exception ex) {
                        db.Database.RollbackTransaction();
                        throw new Exception(ex.Message);
                    }
                }

                return new ReturnSet() { Data = ReturnId, Type = ReturnType.Result };
            }
            catch (Exception ex)
            {
                
                return ExceptionLogger(ex, Session);
            }
        }

        public void PostLeaveAdjustmentDetails(List<tLeaveAdjustment_Detail> DetailData, int ID_LeaveAdjustment, LeaveAdjustmentDb db) {
            try {
                foreach (var detail in DetailData) {
                    detail.ID_LeaveAdjustment = ID_LeaveAdjustment;
                    if (db.Any("(SELECT Id FROM tLeaveAdjustment_Detail WHERE ID = {0})a", detail.Id))
                        db.Update(detail);
                    else db.Add(detail);
                }
                db.SaveChanges();
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        
        }

        public void RemoveLeaveAjustmentDetails(List<int> Ids, LeaveAdjustmentDb db) {
            try
            {
                foreach (var id in Ids) {
                    var detailToDelete = db.Single<tLeaveAdjustment_Detail>("SELECT * FROM tLeaveAdjustment_Detail WHERE Id = {0}", id);
                    if (detailToDelete != null){
                        db.Remove(detailToDelete);
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override ReturnSet DeleteRecord()
        {
            try
            {
                var ids = Parameter["data"].ToObject<List<int>>();
                using (var db = new LeaveAdjustmentDb(Session))
                {
                    if (ids.Count > 0)
                    {
                        foreach (var id in ids)
                        {
                            var rec = db.tLeaveAdjustment.SingleOrDefault(x => x.ID == id);
                            if (rec != null) db.Remove(rec);
                        }
                    }
                    db.SaveChanges();
                    return new ReturnSet() { Message = "Successfully Deleted.", Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }


        public override ReturnSet LoadLookup()
        {
            using (var db = new LeaveAdjustmentDb(Session))
            {
                var name = Parameter["Name"].ToString();
                var rawRights = Helpers.CompanyRights(Session.ID_Roles);
                var companyRights = rawRights.Split(",");
                switch (name.Trim().ToLower())
                {
                    case "employee":
                        return base.LoadLookup<tLookUpData>(db, $"SELECT ID_Employee AS ID, EmployeeName AS Name, CAST(1 AS BIT) AS IsActive FROM vEmployees WHERE ID_Company = {Session.ID_Company}");
                    case "leavetype":
                        return base.LoadLookup<tLookUpData>(db, String.Format($"SELECT ID, Name, CAST(1 AS BIT) AS IsActive FROM tLeaveType WHERE ID_Company = {Session.ID_Company}"));
                    default:
                        var msg = $"{name} is not available on your lookup.";
                        Logger.LogError(ref msg, "LoadLookup", Session.Name, "InSys.ITI.LeaveAdjustment");
                        throw new Exception("System Error! Please contact your System Administrator");
                }
            }
        }

    }
}
