using InSys.Helper;
using InSys.ITI.Controller;
using InSys.ITI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using z.Data;

namespace InSys.ITI.LeaveParameter
{
    public class LeaveParameter : BaseModule
    {
        public override BaseModule Initialize(BrowserSession _Session, Pair _Parameter)
        {
            return new LeaveParameter(_Session, _Parameter);
        }
        public LeaveParameter(BrowserSession _Session, Pair _Parameter)
        {
            this.Session = _Session;
            this.Parameter = _Parameter;
        }
        public LeaveParameter() { }

        public override ReturnSet LoadList()
        {
            try
            {
                TableOptions to = Parameter["data"].ToObject<TableOptions>();

                using (var db = new LeaveParameterDb(Session))
                {
                    var LeaveParameterData = db.QueryTable<tLeaveParameter>(
                            "(SELECT * FROM dbo.tLeaveParameter)a", to);
                    return new ReturnSet() { Data = new { Total = LeaveParameterData.Count, Rows = LeaveParameterData.Data.ToList() }, Type = ReturnType.Result };
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
                using (var db = new LeaveParameterDb(Session))
                {
                    var data = db.Single<tLeaveParameter>("SELECT * FROM dbo.tLeaveParameter WHERE ID = {0}", Parameter["ID"].IsNull(0).ToInt32());
                    return new ReturnSet()
                    {
                        Data = new
                        {
                            Form = data.IsNull(new tLeaveParameter()
                            {
                                DateTimeCreated = DateTime.Now
                            }),
                            Schema = Helpers.GetSchema("tLeaveParameter")
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
            var Record = Parameter["Data"].ToObject<tLeaveParameter>();
            var LeaveParameterItemToDelete = Parameter["DetailsToDelete"].ToObject<List<int>>();
            var LeaveParameterItem = Parameter["Details"].ToObject<List<tLeaveParameterItem>>();
            int RecordId = Record.ID;
            try
            {
                using (var db = new LeaveParameterDb(Session))
                {

                    if (RecordId == 0) db.Add(Record); //New
                    else if (RecordId > 0) //Edit
                    {
                        var RecordToUpdate = db.Single<tLeaveParameter>("SELECT * FROM dbo.tLeaveParameter WHERE ID = {0}", RecordId);
                        if (RecordToUpdate != null)
                            db.Update(Record);
                        else throw new Exception("Data does not exists");
                    }
                    db.SaveChanges(true);
                    RecordId = Record.ID;
                    //Post Leave Parameter Items
                    PostLeaveParameterItem(LeaveParameterItem, LeaveParameterItemToDelete, RecordId, db);

                }
                return new ReturnSet() { Data = RecordId, Type = ReturnType.Result };
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }

        public override ReturnSet DeleteRecord()
        {
            try
            {
                using (var db = new LeaveParameterDb(Session))
                {
                    List<int> ids = Parameter["Data"].ToObject<List<int>>();
                    foreach (int id in ids)
                    {
                        var RecordToDelete = db.Single<tLeaveParameter>("SELECT * FROM dbo.tLeaveParameter WHERE ID = {0}", id);

                        //Do not allow system generated value
                        db.Remove(RecordToDelete);
                    }
                    db.SaveChanges();
                }

                return new ReturnSet() { Message = "Record deleted.", Type = ReturnType.Result };
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }

        public virtual string PostLeaveParameterItem(List<tLeaveParameterItem> LeaveParameterItems, List<int> DeleteLeaveParameterItem, int ParentId, LeaveParameterDb db)
        {
            string message = "";
            try
            {
                //Delete Schedule Details
                foreach (var sd in DeleteLeaveParameterItem)
                {
                    var recordToDelete = db.Single<tLeaveParameterItem>("SELECT * FROM tLeaveParameterItem WHERE ID = {0}", sd);
                    if (recordToDelete != null) { db.Remove(recordToDelete); }
                }
                db.SaveChanges();

                foreach (var detail in LeaveParameterItems)
                {
                    var recordToUpdate = db.Single<tLeaveParameterItem>("SELECT * FROM tLeaveParameterItem WHERE ID = {0}", detail.ID);
                    detail.ID_LeaveParameter = ParentId;
                    if (recordToUpdate == null)
                        db.Add(detail);
                    else db.Update(detail);
                }
                db.SaveChanges();
                return message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public override ReturnSet LoadLookup()
        {
            try {
                var paramName = Parameter["Name"].ToString().ToLower();
                ReturnSet ResultData = new ReturnSet();
                using ( var db = new LeaveParameterDb(Session)) {
                    switch (paramName) {
                        case ("leaveaccrualtype"):
                            ResultData = base.LoadLookup<tLookUpData>(db, "SELECT Id, Name, IsActive FROM tLeaveAccrualType");
                            break;
                        case ("leaveparameteritemreferencedate"):
                            ResultData = base.LoadLookup<tLookUpData>(db, "SELECT ID, Name, IsActive FROM tLeaveParameterItemReferenceDate");
                            break;
                        case ("accrualoption"):
                            ResultData = base.LoadLookup<tLookUpData>(db, "SELECT ID, Name, IsActive FROM tAccrualOption");
                            break;
                        case ("leavepayrollitem"):
                            ResultData = base.LoadLookup<tLookUpData>(db, "SELECT ID, Name, IsActive FROM tPayrollItem WHERE IsForLeave = 1");
                            break;
                    }

                    return new ReturnSet() { Data = ResultData.Data, Type = ReturnType.Result };
                    

                }
            
            } catch (Exception ex) {
                return ExceptionLogger(ex, Session);
            }
        }

    }
}
