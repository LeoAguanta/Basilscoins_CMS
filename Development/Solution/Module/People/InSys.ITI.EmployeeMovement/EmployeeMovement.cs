using InSys.Context;
using InSys.Helper;
using InSys.ITI.Controller;
using InSys.ITI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using z.Data;

namespace InSys.ITI.EmployeeMovement
{
    public class EmployeeMovement : BaseModule
    {
        public override BaseModule Initialize(BrowserSession _Session, Pair _Parameter)
        {
            return new EmployeeMovement(_Session, _Parameter);
        }
        public EmployeeMovement(BrowserSession _Session, Pair _Parameter)
        {
            this.Session = _Session;
            this.Parameter = _Parameter;
        }
        public EmployeeMovement() { }

        #region CRUD
        public override ReturnSet LoadList()
        {
            var r = new ReturnSet();
            try
            {
                TableOptions to = Parameter["data"].ToObject<TableOptions>();
                using (var db = new EmployeeMovementDb(Session))
                {
                    var data = db.QueryTable<vMovement>($"({Helpers.GetSqlQuery("vMovement")})a", to);
                    r = new ReturnSet() { Data = new { Total = data.Count, Rows = data.Data.ToList() }, Type = ReturnType.Result };
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
                using (var db = new EmployeeMovementDb(Session))
                {
                    var data = db.Single<vMovement>($"{Helpers.GetSqlQuery("vMovement")} where tm.ID = {{0}}", Parameter["ID"].IsNull(0).ToInt32());
                    if (data == null && Parameter["ID"].IsNull(0).ToInt32() != 0)
                        return new ReturnSet() { Message = "Page not found.", Type = ReturnType.PageNotFound };
                    return new ReturnSet()
                    {
                        Data = new
                        {
                            Form = data.IsNull(new tMovement() { EffectivityDate = DateTime.Now, DateTimeCreated = DateTime.Now, DateTimeModified = DateTime.Now }),
                            Schema = Helpers.GetSchema("tMovement")
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
                using (var db = new EmployeeMovementDb(Session))
                {
                    tMovement tm = Parameter["Data"].ToObject<tMovement>();
                    List<Movement_Employee_Data> detail = Parameter["Detail"].ToObject<List<Movement_Employee_Data>>();
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            if (db.tMovements.AsNoTracking().Any(x => x.ID == tm.ID))
                            {
                                tm.ID_UserModifiedBy = Session.ID_User;
                                tm.DateTimeModified = DateTime.Now;
                                db.Update(tm);
                            }
                            else
                            {
                                tm.ID_UserCreatedBy = Session.ID_User;
                                tm.ID_UserModifiedBy = Session.ID_User;
                                tm.DateTimeCreated = DateTime.Now;
                                tm.DateTimeModified = DateTime.Now;
                                tm.Code = Helpers.getReferenceNumber("tMovement", Session.ID_Company.ToInt32());
                                tm.ID_Company = Session.ID_Company;
                                db.Add(tm);
                            }
                            db.SaveChanges(true);
                            Helpers.updateReferenceNum("tMovement", Session.ID_Company.ToInt32(), Session);

                            foreach (Movement_Employee_Data tme in detail)
                            {
                                tMovement_Employee tMovement_Employee = new tMovement_Employee();
                                tme.ID_Movement = tm.ID;
                                if (db.tMovement_Employees.AsNoTracking().Any(x => x.ID == tme.ID))
                                {
                                    tme.ID_UserModifiedBy = Session.ID_User;
                                    tme.DateTimeModified = DateTime.Now;
                                    tMovement_Employee = tme.ToJson().ToObject<tMovement_Employee>();
                                    db.Update(tMovement_Employee);
                                }
                                else
                                {
                                    tme.ID_UserCreatedBy = Session.ID_User;
                                    tme.ID_UserModifiedBy = Session.ID_User;
                                    tme.DateTimeCreated = DateTime.Now;
                                    tme.DateTimeModified = DateTime.Now;
                                    tMovement_Employee = tme.ToJson().ToObject<tMovement_Employee>();
                                    db.Add(tMovement_Employee);
                                }
                                if (tme.MovementType.Count > 0)
                                {
                                    db.SaveChanges(true);
                                    foreach (tMovement_Employee_Detail tmed in tme.MovementType)
                                    {
                                        tmed.ID_Movement_Employee = tMovement_Employee.ID;
                                        if (db.tMovement_Employee_Details.AsNoTracking().Any(x => x.ID == tmed.ID))
                                        {
                                            tmed.ID_UserModifiedBy = Session.ID_User;
                                            tmed.DateTimeModified = DateTime.Now;
                                            db.Update(tmed);
                                        }
                                        else
                                        {
                                            tmed.ID_UserCreatedBy = Session.ID_User;
                                            tmed.ID_UserModifiedBy = Session.ID_User;
                                            tmed.DateTimeCreated = DateTime.Now;
                                            tmed.DateTimeModified = DateTime.Now;
                                            db.Add(tmed);
                                        }
                                    }
                                }
                            }
                            if (detail.Count > 0) db.SaveChanges();

                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                    return new ReturnSet() { Data = tm.ID, Type = ReturnType.Result };
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
                using (var db = new EmployeeMovementDb(Session))
                {
                    List<int> ids = Parameter["Data"].ToObject<List<int>>();
                    foreach (int id in ids)
                    {
                        var record = db.tMovements.AsNoTracking().Single(x => x.ID == id);
                        if (record != null) db.Remove(record);
                    }
                    db.SaveChanges();
                    return new ReturnSet() { Message = "Record deleted.", Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public override ReturnSet LoadDetail()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var data = db.ExecQuery<vMovement_Employee>($"{Helpers.GetSqlQuery("vMovement_Employee")} where tme.ID_Movement = {{0}}", Parameter["ID_Movement"].ToInt32());
                    r = new ReturnSet() { Data = data.ToList(), Type = ReturnType.Result };
                    return r;
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
                    case "employee":
                        return LoadEmployee();
                    case "employeesuggestion":
                        return LoadEmployeeSuggestion();
                    case "movementtype":
                        return LoadMovementType();
                    case "branch":
                        return LoadMovementBranch();
                    case "oldbranch":
                        return LoadOldBranch();
                    case "department":
                        return LoadMovementDepartment();
                    case "olddepartment":
                        return LoadOldDepartment();
                    case "designation":
                        return LoadMovementDesignation();
                    case "olddesignation":
                        return LoadOldDesignation();
                    case "oldemployeestatus":
                        return LoadOldEmployeeStatus();
                    case "oldleaveparameter":
                        return LoadOldLeaveParameter();
                    case "employeemovementdetail":
                        return LoadEmployeeMovementDetail();
                    case "employeestatus":
                        return LoadEmployeeStatus();
                    case "leaveparameter":
                        return LoadLeaveParameter();
                    default:
                        throw new Exception("Method not found.");
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public override ReturnSet DeleteDetail()
        {
            try
            {
                string detailname = Parameter["DetailName"].ToString().ToLower();
                switch (detailname)
                {
                    case "movementemployee":
                        return DeleteMovementEmployee();
                    case "movementtype":
                        return DeleteMovementEmployeeDetail();
                    default:
                        throw new Exception("Method not found.");
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet DeleteMovementEmployee()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            int? ID = Parameter["ID"].IsNull(0).ToInt32();
                            var record = db.tMovement_Employees.Where(x => x.ID == ID).FirstOrDefault();
                            if (record != null)
                            {
                                foreach (var detail in db.tMovement_Employee_Details.Where(x => x.ID_Movement_Employee == record.ID).ToList())
                                {
                                    db.Remove(detail);
                                }
                                db.Remove(record);
                                db.SaveChanges();
                                tran.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }

                    return new ReturnSet() { Message = "Record deleted.", Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet DeleteMovementEmployeeDetail()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    int? ID = Parameter["ID"].IsNull(0).ToInt32();
                    var record = db.tMovement_Employee_Details.Where(x => x.ID == ID).FirstOrDefault();
                    if (record != null)
                    {
                        db.Remove(record);
                        db.SaveChanges();
                    }
                    return new ReturnSet() { Message = "Record deleted.", Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Lookup
        public virtual ReturnSet LoadEmployee()
        {
            var r = new ReturnSet();
            try
            {
                TableOptions to = Parameter["Data"].ToObject<TableOptions>();
                using (var db = new EmployeeMovementDb(Session))
                {
                    var data = db.QueryTable<tLookUpData>("(SELECT e.ID, p.Name, e.IsActive FROM tEmployee e" +
                                " INNER JOIN tPersona p ON p.ID = e.ID_Persona and e.ID_Company = {0})a", to, Session.ID_Company);
                    return new ReturnSet()
                    {
                        Data = new { Total = data.Count, Rows = data.Data.ToList() },
                        Type = ReturnType.Result
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadMovementType()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("SELECT ID, Name, cast(1 as bit) IsActive FROM dbo.tMovementType where ID not IN(2,3,5,9,10,11,12)");
                    return new ReturnSet()
                    {
                        Data = new { Rows = data.ToList() },
                        Type = ReturnType.Result
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadMovementBranch()
        {
            var r = new ReturnSet();
            try
            {
                TableOptions to = Parameter["data"].ToObject<TableOptions>();
                using (var db = new EmployeeMovementDb(Session))
                {
                    var id_org = db.ExecScalarInt("Select ID_Org as Value from dbo.tEmployee where ID = {0} and ID_Company = {1}", Parameter["ID_Employee"].ToInt32(), Session.ID_Company);
                    var emp_branch = db.ExecScalarInt("Select ID_Branch as Value from dbo.fGetTableOrganization({0})", id_org.IsNull(0));
                    var data = db.QueryTable<tLookUpData>("(SELECT ID, Name, cast(1 as bit) IsActive FROM dbo.fGetMovementOrganization({0}, {1}) where ID <> {2})a", to, Session.ID_Company, "branch", emp_branch.IsNull(0));
                    return new ReturnSet() { Data = new { Total = data.Count, Rows = data.Data.ToList() }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadEmployeeSuggestion()
        {
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("SELECT e.ID, p.Name3 Name, e.IsActive FROM tEmployee e" +
                                " INNER JOIN tPersona p ON p.ID = e.ID_Persona where e.ID_EmployeeStatus NOT IN (5,6) and p.Name like '%' + {0} + '%' and e.ID_Company = {1}", Parameter["Employee"], Session.ID_Company);
                    return new ReturnSet()
                    {
                        Data = data.ToList(),
                        Type = ReturnType.Result
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadOldBranch()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var id_org = db.ExecScalarInt("Select ID_Org as Value from dbo.tEmployee where ID = {0} and ID_Company = {1}", Parameter["ID_Employee"].ToInt32(), Session.ID_Company);
                    var emp = db.ExecQuery<tLookUpData>("Select ID_Branch as ID, Branch as Name, cast(1 as bit) IsActive from dbo.fGetTableOrganization({0})", id_org.IsNull(0));
                    return new ReturnSet() { Data = emp.FirstOrDefault(), Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadMovementDepartment()
        {
            var r = new ReturnSet();
            try
            {
                TableOptions to = Parameter["data"].ToObject<TableOptions>();
                using (var db = new EmployeeMovementDb(Session))
                {
                    var id_org = db.ExecScalarInt("Select ID_Org as Value from dbo.tEmployee where ID = {0} and ID_Company = {1}", Parameter["ID_Employee"].ToInt32(), Session.ID_Company);
                    var emp = db.ExecScalarInt("Select ID_Department as Value from dbo.fGetTableOrganization({0})", id_org.IsNull(0));
                    var data = db.QueryTable<tLookUpData>("(SELECT ID, Name, cast(1 as bit) IsActive FROM dbo.fGetMovementOrganization({0}, {1}) where ID <> {2})a", to, Session.ID_Company, "department", emp.IsNull(0));
                    return new ReturnSet() { Data = new { Total = data.Count, Rows = data.Data.ToList() }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadOldDepartment()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var id_org = db.ExecScalarInt("Select ID_Org as Value from dbo.tEmployee where ID = {0} and ID_Company = {1}", Parameter["ID_Employee"].ToInt32(), Session.ID_Company);
                    var emp = db.ExecQuery<tLookUpData>("Select ID_Department as ID, Department as Name, cast(1 as bit) IsActive from dbo.fGetTableOrganization({0})", id_org.IsNull(0));
                    return new ReturnSet() { Data = emp.FirstOrDefault(), Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadMovementDesignation()
        {
            var r = new ReturnSet();
            try
            {
                TableOptions to = Parameter["data"].ToObject<TableOptions>();
                using (var db = new EmployeeMovementDb(Session))
                {
                    var id_org = db.ExecScalarInt("Select ID_Org as Value from dbo.tEmployee where ID = {0} and ID_Company = {1}", Parameter["ID_Employee"].ToInt32(), Session.ID_Company);
                    var emp = db.ExecScalarInt("Select ID_Designation as Value from dbo.fGetTableOrganization({0})", id_org.IsNull(0));
                    var data = db.QueryTable<tLookUpData>("(SELECT ID, Name, cast(1 as bit) IsActive FROM dbo.fGetMovementOrganization({0}, {1}) where ID <> {2})a", to, Session.ID_Company, "designation", emp.IsNull(0));
                    return new ReturnSet() { Data = new { Total = data.Count, Rows = data.Data.ToList() }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadOldDesignation()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var id_org = db.ExecScalarInt("Select ID_Org as Value from dbo.tEmployee where ID = {0} and ID_Company = {1}", Parameter["ID_Employee"].ToInt32(), Session.ID_Company);
                    var emp = db.ExecQuery<tLookUpData>("Select ID_Designation as ID, Designation as Name, cast(1 as bit) IsActive from dbo.fGetTableOrganization({0})", id_org.IsNull(0));
                    return new ReturnSet() { Data = emp.FirstOrDefault(), Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadOldEmployeeStatus()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var ID_EmployeeStatus = db.ExecScalarInt("Select ID_EmployeeStatus as Value from dbo.tEmployee where ID = {0} and ID_Company = {1}", Parameter["ID_Employee"].ToInt32(), Session.ID_Company);
                    var old = db.Single<tLookUpData>("Select ID, Name, cast(1 as bit) IsActive from dbo.tEmployeeStatus where ID = {0}", ID_EmployeeStatus.IsNull(0));
                    return new ReturnSet() { Data = new { OldEmployeeStatus = old }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadOldLeaveParameter()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var ID_LeaveParameter = db.ExecScalarInt("Select ID_LeaveParameter as Value from dbo.tEmployee where ID = {0} and ID_Company = {1}", Parameter["ID_Employee"].ToInt32(), Session.ID_Company);
                    var old = db.Single<tLookUpData>("Select ID, Name, cast(1 as bit) IsActive from dbo.tLeaveParameter where ID = {0}", ID_LeaveParameter.IsNull(0));
                    return new ReturnSet() { Data = new { OldLeaveParameter = old }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadEmployeeMovementDetail()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var data = db.ExecQuery<vMovement_Employee_Detail>($"{Helpers.GetSqlQuery("vMovement_Employee_Detail")} where tmed.ID_Movement_Employee = {{0}}", Parameter["ID"].ToInt32());
                    return new ReturnSet() { Data = data.ToList(), Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadEmployeeStatus()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("SELECT ID, Name, cast(1 as bit) IsActive FROM dbo.tEmployeeStatus");
                    return new ReturnSet() { Data = new { Rows = data.ToList() }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual ReturnSet LoadLeaveParameter()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("SELECT ID, Name, cast(1 as bit) IsActive FROM dbo.tLeaveParameter");
                    return new ReturnSet() { Data = new { Rows = data.ToList() }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Actions
        public override ReturnSet ExecuteAction()
        {
            try
            {
                string method = Parameter["MethodName"].ToString().ToLower();
                switch (method)
                {
                    case "applymovement":
                        return ApplyMovement();
                    default:
                        throw new Exception("Method not found.");
                }
            }
            catch(Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet ApplyMovement()
        {
            try
            {
                using (var db = new EmployeeMovementDb(Session))
                {
                    var eDate = db.ExecScalarDateTime("select EffectivityDate as Value from dbo.tMovement where ID = {0}", Parameter["ID"].ToInt32());
                    var cDate = db.ExecScalarDateTime("select GETDATE() as Value");

                    if (eDate.ToString("MM/dd/yyyy") == cDate.ToString("MM/dd/yyyy"))
                    {
                        var employee = db.ExecQuery<vMovement_Employee>($"{Helpers.GetSqlQuery("vMovement_Employee")} where tme.ID_Movement = {{0}}", Parameter["ID"].ToInt32()).ToList();
                        using (var tran = db.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var emp in employee)
                                {
                                    var movementPerEmp = db.ExecQuery<tMovement_Employee_Detail>("select * from dbo.tMovement_Employee_Detail where ID_Movement_Employee = {0}", emp.ID).ToList();
                                    foreach (var movement in movementPerEmp)
                                    {
                                        switch (movement.ID_MovementType)
                                        {
                                            case 1:
                                                ApplySalaryMovement(db, movement, emp);
                                                break;
                                            case 4:
                                                ApplyBranchMovement(db, movement, emp);
                                                break;
                                            case 6:
                                                ApplyDepartmentMovement(db, movement, emp);
                                                break;
                                            case 7:
                                                ApplyDesignationMovement(db, movement, emp);
                                                break;
                                            case 8:
                                                ApplyEmployeeStatusMovement(db, movement, emp);
                                                break;
                                            case 13:
                                                ApplyLeaveParameterMovement(db, movement, emp);
                                                break;
                                            default:
                                                throw new Exception("Movement type not found.");
                                        }
                                    }
                                }
                                db.SaveChanges();
                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                throw ex;
                            }
                        }
                    }
                    else
                    {
                        return new ReturnSet() { Message = "Effectivity date does not match current date.", Type = ReturnType.Error };
                    }
                    return new ReturnSet() { Message = "Movement applied.", Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual void ApplySalaryMovement(EmployeeMovementDb db, tMovement_Employee_Detail tmed, vMovement_Employee tme) {
            try
            {
                int ID_Parameter = db.ExecScalarInt("select ID_Parameter as Value from dbo.tEmployee where ID = {0}", tme.ID_Employee);
                var employeeParameter = db.Single<tParameter>("select * from dbo.tParameter where ID = {0}", ID_Parameter);

                if (employeeParameter == null) throw new Exception($"{tme.Employee} parameter not found.");
                var emp = db.Single<tEmployee>("select * from dbo.tEmployee where ID = {0}", tme.ID_Employee);
                if (emp == null) throw new Exception($"{tme.Employee} record not found.");

                emp.MonthlyRate = emp.MonthlyRate + Convert.ToDecimal(tmed.NewValue);
                emp.DailyRate = (emp.MonthlyRate * Convert.ToDecimal(12)) / employeeParameter.DaysPerYear;
                emp.HourlyRate = emp.DailyRate / employeeParameter.HoursPerDay;

                if (emp.MWE.IsNull(false).ToBool())
                {
                    //pending minimum wage earner
                }

                db.Update(emp);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public virtual void ApplyBranchMovement(EmployeeMovementDb db, tMovement_Employee_Detail tmed, vMovement_Employee tme) { }
        public virtual void ApplyDepartmentMovement(EmployeeMovementDb db, tMovement_Employee_Detail tmed, vMovement_Employee tme) { }
        public virtual void ApplyDesignationMovement(EmployeeMovementDb db, tMovement_Employee_Detail tmed, vMovement_Employee tme) { }
        public virtual void ApplyEmployeeStatusMovement(EmployeeMovementDb db, tMovement_Employee_Detail tmed, vMovement_Employee tme) { }
        public virtual void ApplyLeaveParameterMovement(EmployeeMovementDb db, tMovement_Employee_Detail tmed, vMovement_Employee tme) { }
        public override ReturnSet Post()
        {
            try
            {
                using(var db = new EmployeeMovementDb(Session))
                {
                    var data = db.Single<tMovement>("select * from dbo.tMovement where ID = {0}", Parameter["ID"].ToInt32());
                    if (data == null)
                        return new ReturnSet() { Message = "Record not found.", Type = ReturnType.PageNotFound };
                    if(data.IsPosted)
                        return new ReturnSet() { Message = "Record already posted.", Type = ReturnType.Error };
                    data.IsPosted = true;
                    db.Update(data);
                    db.SaveChanges();
                    return new ReturnSet() { Message = "Record posted.", Type = ReturnType.Result };
                }
            }catch(Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        #endregion

    }
    public class EmployeeMovementDb : InSysContext
    {
        public EmployeeMovementDb(BrowserSession Session) : base(Session)
        {
            this.SessionContext = Session;
        }

        public DbSet<tMovement> tMovements { get; set; }
        public DbSet<tMovement_Employee> tMovement_Employees { get; set; }
        public DbSet<tMovement_Employee_Detail> tMovement_Employee_Details { get; set; }
        public DbQuery<vMovement> vMovements { get; set; }
        public DbQuery<vMovement_Employee> vMovement_Employees { get; set; }
        public DbQuery<vMovement_Employee_Detail> vMovement_Employee_Details { get; set; }

        public DbQuery<CountData> countDatas { get; set; }
        public DbSet<tEmployee> tEmployees { get; set; }
        public DbQuery<tLookUpData> tLookUpDatas { get; set; }
        public DbQuery<DecimalReturn> decimalReturns { get; set; }
        public DbQuery<IntReturn> intReturns { get; set; }
        public DbSet<tParameter> tParameters { get; set; }
        public DbQuery<DateTimeReturn> dateTimeReturns { get; set; }
    }

}
