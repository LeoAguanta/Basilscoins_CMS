using InSys.Helper;
using InSys.ITI.Controller;
using InSys.ITI.Models.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using z.Data;

namespace InSys.ITI.ApproverMatrix
{
    public class ApproverMatrix:BaseModule
    {
        public ApproverMatrix(BrowserSession _Session, Pair _Parameter)
        {
            this.Session = _Session;
            this.Parameter = _Parameter;
        }
        public ApproverMatrix()
        {

        }

        public override BaseModule Initialize(BrowserSession _Session, Pair _Parameter)
        {
            return new ApproverMatrix(_Session,_Parameter);
        }

        public override ReturnSet LoadList()
        {
            ReturnSet res = new ReturnSet();

            TableOptions to = Parameter["Data"].ToObject<TableOptions>();

            try
            {
                using (var db = new ApproverMatrixDB(Session))
                {
                    var data = db.QueryTable<tApprover>("(select * from dbo.tApprover where IsActive = 1)a", to);
                    res = new ReturnSet() { Data = new { Total = data.Count, Rows = data.Data.ToList() }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex,Session);
            }

            return res;
        }
        public override ReturnSet Save()
        {
            try
            {
                using (var db = new ApproverMatrixDB(Session))
                {
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var ta = Parameter["Data"].ToObject<tApprover>();
                            if (db.Any("(SELECT * FROM tApprover WHERE ID = {0})a", ta.ID)) db.Update(ta);
                            else db.Add(ta);
                            db.SaveChanges(true);

                            var obj = Parameter["Data"].ToObject<JObject>();
                            var deletedModules = obj["DeletedModules"] != null ? obj["DeletedModules"].ToObject<List<int>>() : new List<int>();
                            var filingModules = Parameter["FilingModules"].ToObject<List<ApproverModuleData>>();

                            //delete modules
                            foreach (int id in deletedModules)
                            {
                                //check if module has approvers
                                var approvers = db.ExecQuery<tApprover_Default>("select * from dbo.tApprover_Default where ID_Approver_Module = {0}",id).Select(x => x.ID).ToList();
                                foreach (var approverID in approvers)
                                {
                                    var del = db.Single<tApprover_Default>("SELECT * FROM tApprover_Default WHERE ID = {0}",approverID);
                                    if (del != null) db.Remove(del);
                                }
                                if (approvers.Count > 0) db.SaveChanges(true);

                                var del2 = db.Single<tApprover_Module>("SELECT * FROM tApprover_Module WHERE ID = {0}",id);
                                if (del2 != null) db.Remove(del2);
                            }
                            if (deletedModules.Count > 0) db.SaveChanges(true);

                            //remove approvers
                            foreach (var fm in filingModules) {
                                var deletedApprovers = fm.DeletedApprovers;
                                if (deletedApprovers == null) continue;
                                foreach (int id in deletedApprovers)
                                {
                                    var del = db.Single<tApprover_Default>("SELECT * FROM tApprover_Default WHERE ID = {0}",id);
                                    if (del != null) db.Remove(del);
                                }
                                if (deletedApprovers.Count > 0) db.SaveChanges(true);
                            }

                            //save modules
                            foreach(var amd in filingModules)
                            {
                                var tam = new tApprover_Module
                                {
                                    ID = amd.ID,
                                    ID_Approver = ta.ID,
                                    ID_FilingModules = amd.ID_FilingModules,
                                    IsActive = true
                                };
                                if (db.Any("(SELECT * FROM tApprover_Module WHERE ID = {0})a", tam.ID)) db.Update(tam);
                                else db.Add(tam);
                                db.SaveChanges(true);

                                if (amd.Default != null)
                                {
                                    foreach (tApprover_Default td in amd.Default)
                                    {
                                        td.ID_Approver_Module = tam.ID;
                                        if (db.Any("(SELECT * FROM tApprover_Default WHERE ID = {0})a", td.ID)) db.Update(td);
                                        else db.Add(td);
                                    }
                                    if (amd.Default.Count > 0) db.SaveChanges(true);
                                }
                            }
                            
                            var selectedEmployees = Parameter["Employees"].ToObject<List<tApprover_Employees>>();
                            var deletedEmployees = Parameter["DeletedEmployees"].ToObject<List<int>>();

                            //delete Employees
                            foreach (var id in deletedEmployees)
                            {
                                var rec = db.Single<tApprover_Employees>("SELECT * FROM tApprover_Employees WHERE ID = {0}",id);
                                if (rec != null) db.Remove(rec);
                            }
                            if (deletedEmployees.Count > 0) db.SaveChanges(true);
                            //save Employees
                            foreach (var emp in selectedEmployees)
                            {
                                if (emp.ID == 0)
                                {
                                    db.Add(emp);
                                }
                                else
                                {
                                    if (db.tApprover_Employee.Where(x => x.ID == emp.ID).Any())
                                        db.Update(emp);
                                    else
                                        db.Add(emp);
                                }
                                
                            }
                            if (selectedEmployees.Count > 0) db.SaveChanges(true);

                            db.SaveChanges(true);
                            tran.Commit();

                            return new ReturnSet { Data = ta.ID, Type = ReturnType.Result };
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            return ExceptionLogger(ex, Session);
                        }
                    }
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
                var ID = Parameter["ID"].IsNull(0).ToInt32();
                using (var db = new ApproverMatrixDB(Session))
                {
                    var record = db.Single<vApprover>(@"SELECT a.ID,a.Name,a.ID_Company,c.Name Company,a.IsActive FROM dbo.tApprover a
                                                        LEFT JOIN dbo.tCompany c ON c.ID = a.ID_Company WHERE a.ID = {0} AND a.IsActive = 1",ID);
                    List<vApprover_Module> filingModules = new List<vApprover_Module>();
                    var employees = new List<vApprover_Employees>();
                    if (record != null) {
                        filingModules = db.ExecQuery<vApprover_Module>(Helpers.GetSqlQuery("vApprover_Module").BuildParameter(null, record.ID)).ToList();
                    }
                    var approverEmployees = db.ExecQuery<vApprover_Employees>(@"SELECT 
                                                                                ae.ID,
                                                                                ae.ID_Approver,
                                                                                p.Name,
                                                                                cast(1 as bit) IsChecked,
                                                                                ae.ID_Employee
                                                                                FROM dbo.tApprover_Employees ae
                                                                                INNER JOIN dbo.tEmployee e ON e.ID = ae.ID_Employee
                                                                                LEFT JOIN dbo.tPersona p ON p.ID = e.ID_Persona
                                                                                WHERE ae.ID_Approver = {0}",ID).ToList();

                    return new ReturnSet {
                        Data = new 
                        { 
                            Form = record.IsNull(new vApprover()),
                            Schema = Helpers.GetSchema("tApprover"),
                            FilingModules = filingModules,
                            AllEmployees = approverEmployees
                        },
                        Type = ReturnType.Result
                    };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex,Session);
            }
        }
        public override ReturnSet DeleteRecord()
        {
            return base.DeleteRecord();
        }
        public virtual ReturnSet LoadApproverByModule()
        {
            var ret = new ReturnSet();
            try
            {
                using (var db = new ApproverMatrixDB(Session))
                {
                    var data = db.ExecQuery<vApprover_Default>("select tad.*, tp.Name Employee, tp2.Name Employee2, tp3.Name Employee3, tp.ImageFile Image, tp2.ImageFile Image2, tp3.ImageFile Image3 from dbo.tApprover_Default tad" +
                        " inner join dbo.tEmployee te on tad.ID_Employee = te.ID" +
                        " left join dbo.tEmployee te2 on tad.ID_Employee2 = te2.ID" +
                        " left join dbo.tEmployee te3 on tad.ID_Employee3 = te3.ID" +
                        " left join dbo.tPersona tp on te.ID_Persona = tp.ID" +
                        " left join dbo.tPersona tp2 on te2.ID_Persona = tp2.ID" +
                        " left join dbo.tPersona tp3 on te3.ID_Persona = tp3.ID" +
                        " where tad.ID_Approver_Module = {0}", Parameter["ID"].ToInt32());
                    return new ReturnSet() { Data = data.ToList(), Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex,Session);
            }
        }
        public virtual ReturnSet LoadEmployeeList()
        {
            var ret = new ReturnSet();
            try
            {
                TableOptions to = Parameter["data"].ToObject<TableOptions>();
                var ID_Company = Parameter["ID_Company"].IsNull(0).ToInt32();
                using (var db = new ApproverMatrixDB(Session))
                {
                    List<EmployeeList> data = new List<EmployeeList>();


                    var employees = db.QueryTable<EmployeeList>(@"(SELECT 
                                                                   e.ID,p.Name,e.ID_Org ID_Designation,oml.Name Designation,e.ID_Company
                                                                   FROM dbo.tJobClassApproverCandidates jcac
                                                                   INNER JOIN dbo.tOrg o ON o.ID = jcac.ID_Org
                                                                   INNER JOIN dbo.tOrg o2  ON o2.ID_Parent = o.ID
                                                                   LEFT JOIN dbo.tEmployee e ON e.ID_Org = o2.ID
                                                                   LEFT JOIN dbo.tPersona p ON p.ID = e.ID_Persona
                                                                   LEFT JOIN dbo.tOrgMasterList oml ON oml.ID = o2.ID_Master
                                                                   WHERE e.IsActive = 1 AND e.ID_Company = {0})a", to,ID_Company);
                    ret = new ReturnSet() 
                    { 
                        Data = new {
                            Total = employees.Count,
                            Rows = employees.Data.ToList()
                        }, 
                        Type = ReturnType.Result 
                    };
                    return ret;
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadApproverSchema()
        {
            var ret = new ReturnSet();
            try
            {
                using (var db = new ApproverMatrixDB(Session))
                {
                    return new ReturnSet()
                    {
                        Data = new
                        {
                            Form = new vApprover_Default(),
                            Schema = Helpers.GetSchema("tApprover_Default")
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
        public virtual ReturnSet LoadFilingModules()
        {
            try
            {
                using (var db = new ApproverMatrixDB(Session))
                {
                    var data = db.ExecQuery<vFilingModules>("select tfm.*, tm.Name from dbo.tFilingModules tfm" +
                        " inner join dbo.tMenus tm on tfm.ID_Menus = tm.Id");
                    return new ReturnSet() { Data = data.ToList(), Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadEmployees() {
            try
            {
                var Ids = Parameter["Data"].ToObject<List<int>>();
                var obj = Parameter["Filters"].ToObject<JObject>();

                var masterId = obj["Master"].IsNull(0).ToInt32();
                var recordID = obj["ID"].IsNull(0).ToInt32();
                var companyId = obj["ID_Company"].IsNull(0).ToInt32();
                using (var db = new ApproverMatrixDB(Session)) 
                {
                    List<vApprover_Employees> employees = new List<vApprover_Employees>();
                    var companyIds = Helpers.CompanyRights(Session.ID_Roles).Split(",");
                    var IDsFilter = "";
                    if (Ids.Count > 0)
                    {
                        IDsFilter = $"AND e.ID NOT IN({String.Join(",", Ids)})";
                    }
                    employees = GetEmployeeByOrg(masterId, IDsFilter,recordID,companyId);
                    return new ReturnSet
                    {
                        Data = employees,
                        Type = ReturnType.Result
                    };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex,Session);
            }
        }
        List<vApprover_Employees> employees = new List<vApprover_Employees>();
        private List<vApprover_Employees> GetEmployeeByOrg(int OrgId,params object[] param)
        {
            try
            {
                using (var db = new ApproverMatrixDB(Session))
                {
                    var orgs = db.tOrg.Where(x => x.ID_Parent == OrgId).ToList();
                    if (orgs.Count > 0)
                    {
                        foreach (var org in orgs)
                        {
                            GetEmployeeByOrg(org.ID,param);
                        }
                    }
                    else {
                        List<vApprover_Employees> employee = new List<vApprover_Employees>();
                        if (OrgId == 0)
                        {
                            employee = db.ExecQuery<vApprover_Employees>(@"SELECT "
                                                                        + " 0 ID, "
                                                                        + " {0} ID_Approver, "
                                                                        + " p.Name, "
                                                                        + " CAST(0 AS BIT) IsChecked, "
                                                                        + " e.ID ID_Employee "
                                                                        + " FROM dbo.tEmployee E "
                                                                        + " INNER JOIN dbo.tPersona p ON p.ID = e.ID_Persona "
                                                                        + $" WHERE e.IsActive = 1 AND e.ID_Company = {param[2]} "
                                                                        + $"{param[0]}", param[1]).ToList();
                        }
                        else {
                            employee = db.ExecQuery<vApprover_Employees>(@"SELECT "
                                                                            + " 0 ID, "
                                                                            + " {0} ID_Approver, "
                                                                            + " p.Name, "
                                                                            + " CAST(0 AS BIT) IsChecked, "
                                                                            + " e.ID ID_Employee "
                                                                            + " FROM dbo.tEmployee E "
                                                                            + " INNER JOIN dbo.tPersona p ON p.ID = e.ID_Persona "
                                                                            + $" WHERE e.IsActive = 1 AND ID_Org = {OrgId} "
                                                                            + $"{param[0]}", param[1]).ToList();
                        }
                         
                        if (employee != null) employees.AddRange(employee);
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return employees;
        }
        public virtual ReturnSet PostApproverMatrixTemplate() 
        {
            try
            {
                using (var db = new ApproverMatrixDB(Session))
                {
                    using (var tran = db.Database.BeginTransaction()) 
                    {
                        try
                        {
                            var ID = Parameter["ID"].IsNull(0).ToInt32();

                            var ta = db.tApprovers.Where(x => x.ID == ID).SingleOrDefault();

                            if (ta != null)
                            {
                                var filingModules = db.tApprover_Module.Where(x => x.ID_Approver == ta.ID).ToList();
                                var taEmployees = db.tApprover_Employee.Where(x => x.ID_Approver == ta.ID).ToList();

                                if (filingModules.Count == 0)
                                    throw new Exception("Please setup filing modules.");
                                if (taEmployees.Count == 0)
                                    throw new Exception("Please select employees to apply the template.");
                                foreach (var emp in taEmployees)
                                {
                                    foreach (var mod in filingModules)
                                    {
                                        var tAppEmpMod = new tApprover_Employee_Module()
                                        {
                                            ID = 0,
                                            ID_Employee = emp.ID_Employee,
                                            ID_FilingModules = mod.ID_FilingModules,
                                            IsActive = true
                                        };
                                        var existingApproverEmpMod = db.tApprover_Employee_Module.Where(x => x.ID_Employee == emp.ID_Employee && x.ID_FilingModules == mod.ID_FilingModules).SingleOrDefault();
                                        if (existingApproverEmpMod != null)
                                        {
                                            var existingApproverEmpModApp = db.tApprover_Employee_Module_Approvers.Where(x => x.ID_Approver_Employee_Module == existingApproverEmpMod.ID).ToList();
                                            if (existingApproverEmpModApp.Count > 0)
                                            {
                                                db.RemoveRange(existingApproverEmpModApp);
                                                db.SaveChanges();
                                            }
                                            db.Remove(existingApproverEmpMod);
                                        };

                                        db.Add(tAppEmpMod);
                                        db.SaveChanges(true);

                                        var approverDefaults = db.tApprover_Default.Where(x => x.ID_Approver_Module == mod.ID).ToList();
                                        foreach (var approverDefault in approverDefaults)
                                        {
                                            var newApproverEmployeeModuleApprovers = new tApprover_Employee_Module_Approvers()
                                            {
                                                ID = 0,
                                                ID_Approver_Employee_Module = tAppEmpMod.ID,
                                                ID_Employee = approverDefault.ID_Employee,
                                                ID_Employee2 = approverDefault.ID_Employee2,
                                                ID_Employee3 = approverDefault.ID_Employee2,
                                                ID_Level = approverDefault.ID_Level,
                                                IsPowerApprover = approverDefault.IsPowerApprover,
                                                IsActive = approverDefault.IsActive
                                            };
                                            db.Add(newApproverEmployeeModuleApprovers);
                                        }
                                        db.SaveChanges(true);
                                    }
                                }
                            }

                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            return ExceptionLogger(ex, Session);
                        }
                    }
                }
                    return new ReturnSet { };
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex,Session);
            }
        }
        public virtual ReturnSet LoadOrgTypes() {
            try
            {
                using (var db = new ApproverMatrixDB(Session))
                {
                    var companyId = Parameter["ID_Company"].IsNull(0).ToInt32();
                    var orgtypes = db.ExecQuery<vCompanyOrgType>(@"SELECT
                                                                    cot.ID,
                                                                    cot.ID_OrgType,
                                                                    ot.Name OrgType,
                                                                    cot.ID_Company,
                                                                    c.Name Company,
                                                                    cot.SeqNo
                                                                    FROM dbo.tCompanyOrgType cot
                                                                    LEFT JOIN dbo.tOrgType ot ON ot.ID = cot.ID_OrgType
                                                                    LEFT JOIN dbo.tCompany c ON c.ID = cot.ID_Company
                                                                    WHERE cot.ID_Company = {0}",companyId).ToList();
                    if (orgtypes.Count == 0)
                        throw new Exception("No Company Organization Types found, Please setup Organizational Type on Company module.");
                    return new ReturnSet
                    {
                        Data = orgtypes,
                        Type = ReturnType.Result
                    };
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
                using (var db = new ApproverMatrixDB(Session))
                {
                    var orgtypeId = Parameter["ID"].IsNull(0).ToInt32();
                    var companyId = Parameter["ID_Company"].IsNull(0).ToInt32();

                    return base.LoadLookup<vApprover_OrgList>(db,@"SELECT
                                                                 o.ID,
                                                                 oml.Name,
                                                                 o.ID_Master,
                                                                 o.ID_Parent,
                                                                 oml2.Name Parent
                                                                 FROM dbo.tOrg o
                                                                 INNER JOIN dbo.tCompanyOrgType cot ON cot.ID = o.ID_CompanyOrgType
                                                                 LEFT JOIN dbo.tOrgMasterList oml ON oml.ID = o.ID_Master
                                                                 LEFT JOIN dbo.tOrg o2 ON o2.ID = o.ID_Parent
                                                                 LEFT JOIN dbo.tOrgMasterList oml2 ON oml2.ID = o2.ID"
                                                                +$" WHERE cot.ID = {orgtypeId} AND cot.ID_Company = {companyId}");
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex,Session);
            }
        }
    }
}
