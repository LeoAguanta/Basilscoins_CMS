using InSys.Context;
using InSys.Helper;
using InSys.HRMS.Models;
using InSys.ITI.Controller;
using InSys.ITI.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using z.Data;
using System.Text.RegularExpressions;

namespace InSys.ITI.EmployeeRecord
{
    public class EmployeeRecord : BaseModule
    {
        public override BaseModule Initialize(BrowserSession _Session, Pair _Parameter)
        {
            return new EmployeeRecord(_Session, _Parameter);
        }
        public EmployeeRecord() { }
        public EmployeeRecord(BrowserSession _Session, Pair _Parameter)
        {
            this.Session = _Session;
            this.Parameter = _Parameter;
        }

        public override ReturnSet LoadList()
        {
            string message = "";
            var r = new ReturnSet();
            try
            {
                TableOptions to = Parameter["data"].ToObject<TableOptions>();
                using (var db = new EmployeeRecordDb(Session))
                {
                    var fs = to.Filters.Where(x => x.Name == "ID_Company").FirstOrDefault();
                    if (fs == null)
                        to.Filters.Add(new FilterSchema()
                        {
                            Name = "ID_Company",
                            Type = FilterType.Equals,
                            Value = Helpers.IIf(Session.ID_Company == 0, Helpers.CompanyRights(Session.ID_Roles), Session.ID_Company)
                        });

                    var data = db.QueryTable<vEmployeeRecordList>("(SELECT * FROM dbo.vEmployeeRecordList)a", to);
                    r = new ReturnSet() { Data = new { Total = data.Count, Rows = data.Data.ToList() }, Type = ReturnType.Result };
                    return r;
                }
            }
            catch (Exception ex)
            {
                message = (ex.InnerException ?? ex).Message;
                Logger.LogError(ref message, "LoadList", Helpers.CurrentUser(Session), "InSys.ITI.People");
                r = new ReturnSet() { Message = message, Type = ReturnType.Error };
                return r;
            }
        }
        public override ReturnSet LoadForm()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    List<vPersonaAddress> personaAddress = new List<vPersonaAddress>();
                    var employee = db.Single<vEmployee>($"{Helpers.GetSqlQuery("vEmployee")} where e.ID = {{0}}", Parameter["ID"].ToInt32());
                    var data = new vPersona();
                    if (employee != null)
                    {
                        data = db.Single<vPersona>($"{Helpers.GetSqlQuery("vPersona")} where tp.ID = {{0}} AND tp.ID_Company IN({Helpers.IIf(Session.ID_Company == 0, Helpers.CompanyRights(Session.ID_Roles), Session.ID_Company)})", employee.ID_Persona);
                    }
                    
                    
                    if (data != null)
                    {
                        personaAddress = db.ExecQuery<vPersonaAddress>($"{Helpers.GetSqlQuery("vPersonaAddress")} WHERE tpa.ID_Persona = {{0}}", data.ID).ToList();
                    }

                    if (data == null && Parameter["ID"].IsNull(0).ToInt32() > 0)
                    {
                        return new ReturnSet()
                        {
                            Message = "Page not found.",
                            Type = ReturnType.PageNotFound
                        };
                    }
                    return new ReturnSet()
                    {
                        Data = new
                        {
                            Form = employee.IsNull(new tEmployee()),
                            Persona = data.IsNull(new tPersona()),
                            Schema = Helpers.GetSchema("tPersona"),
                            PersonaAddress = personaAddress,
                            PersonaAddressSchema = Helpers.GetSchema("tPersonaAddress"),
                            EmploymentSchema = Helpers.GetSchema("tPersonaEmployment"),
                            EducationalSchema = Helpers.GetSchema("tPersonaEducationalBackGround")
                        },
                        Type = ReturnType.Result
                    };
                }
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException ?? ex).Message;
                Logger.LogError(ref message, "LoadForm", Helpers.CurrentUser(Session), "InSys.ITI.People");
                r = new ReturnSet() { Message = message, Type = ReturnType.Error };
                return r;
            }
        }
        public override ReturnSet Save()
        {
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    tPersona tfm = Parameter["Data"].ToObject<tPersona>();
                    List<tPersonaAddress> PersonaAddress = new List<tPersonaAddress>();
                    PersonaAddress = Parameter["PersonaAddress"].ToObject<List<tPersonaAddress>>();
                    List<tPersonaEducationalBackGround> Educational = new List<tPersonaEducationalBackGround>();
                    Educational = Parameter["Educational"].ToObject<List<tPersonaEducationalBackGround>>();
                    List<tPersonaEmployment> Employment = new List<tPersonaEmployment>();
                    Employment = Parameter["Employment"].ToObject<List<tPersonaEmployment>>();
                    String email = regexNumOnly(tfm.EmailAddress);
                    using (var tran = db.Database.BeginTransaction())
                    {
                        try
                        {
                            if (db.Any("(select * from dbo.tPersona where ID = {0})a", tfm.ID)) db.Update(tfm);
                            else db.Add(tfm);
                            db.SaveChanges(true);

                            foreach (var pa in PersonaAddress)
                            {
                                pa.ID_Persona = tfm.ID;
                                pa.ID_UserCreatedBy = Session.ID_User;
                                if (db.Any("(select * from dbo.tPersonaAddress where ID = {0})a", pa.ID)) db.Update(pa);
                                else db.Add(pa);
                            }
                            foreach (var ed in Educational)
                            {
                                ed.ID_Persona = tfm.ID;
                                ed.ID_UserCreatedBy = Session.ID_User;
                                if (db.Any("(select * from dbo.tPersonaEducationalBackGround where ID = {0})a", ed.ID)) db.Update(ed);
                                else db.Add(ed);
                            }
                            foreach (var em in Employment)
                            {
                                em.ID_Persona = tfm.ID;
                                em.ID_UserCreatedBy = Session.ID_User;
                                if (db.Any("(select * from dbo.tPersonaEmployment where ID = {0})a", em.ID)) db.Update(em);
                                else db.Add(em);
                            }

                            if (PersonaAddress.Count > 0 || Educational.Count > 0 || Employment.Count > 0) db.SaveChanges(true);

                            tran.Commit();
                        }
                        catch(Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                    return new ReturnSet() { Data = Parameter["ID"].ToInt32(), Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public override ReturnSet LoadLookup()
        {
            string lookupName = Parameter["LookupName"].ToString().ToLower();
            switch (lookupName)
            {
                case "educationalrecord":
                    return LoadEducationalRecord();
                case "employmenthistory":
                    return LoadEmploymentHistory();
                case "nationality":
                    return LoadNationality();
                case "citizenship":
                    return LoadCitizenship();
                case "civilstatus":
                    return LoadCivilStatus();
                case "gender":
                    return LoadGender();
                case "religion":
                    return LoadReligion();
                case "bloodtype":
                    return LoadBloodType();
                case "province":
                    return LoadProvince();
                case "city":
                    return LoadCity();
                case "barangay":
                    return LoadBarangay();
                case "educationalattainment":
                    return LoadEducationalAttainment();
                default:
                    throw new Exception("Method not found.");
            }
        }
        public virtual String regexNumOnly(string test) { //numbers only
            bool wow = Regex.IsMatch(test, @"^[0-9]*$");
            if (wow == true)
            {
                return "matched";
            }
            else { return "mali"; }
        }
        #region lookup

        public virtual ReturnSet LoadEducationalAttainment()
        {
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tEducationalAttainment").ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }

        public virtual ReturnSet LoadBarangay()
        {
            try
            {
                using(var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tBarangay where ID_City = {0}", Parameter["ID_City"].ToInt32()).ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }catch(Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadCity()
        {
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tCity where ID_Province = {0}", Parameter["ID_Province"].ToInt32()).ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadProvince()
        {
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tProvince").ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadBloodType()
        {
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tBloodType").ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadReligion()
        {
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tReligion").ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadGender()
        {
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tGender").ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadCivilStatus()
        {
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tCivilStatus").ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadCitizenship()
        {
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tCitizenship").ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadNationality()
        {
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tLookUpData>("select ID, Name, cast(1 as bit) IsActive from dbo.tNationality").ToList();
                    return new ReturnSet() { Data = new { Rows = data }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex)
            {
                return ExceptionLogger(ex, Session);
            }
        }
        public virtual ReturnSet LoadEducationalRecord()
        {
            string message = "";
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<vPersonaEducationalBackGround>(@"SELECT tpeb.*, tea.Name EducationAttainmentStatus FROM dbo.tPersonaEducationalBackGround tpeb
                                                                                LEFT JOIN dbo.tEducationalAttainment tea ON tpeb.ID_EducationAttainmentStatus = tea.ID where tpeb.ID_Persona = {0}", Parameter["ID"].ToInt32());
                    r = new ReturnSet() { Data = data.ToList(), Type = ReturnType.Result };
                    return r;
                }
            }
            catch (Exception ex)
            {
                message = (ex.InnerException ?? ex).Message;
                Logger.LogError(ref message, "LoadEducationalRecord", Helpers.CurrentUser(Session), "InSys.ITI.EmployeeRecord");
                r = new ReturnSet() { Message = message, Type = ReturnType.Error };
                return r;
            }
        }
        public virtual ReturnSet LoadEmploymentHistory()
        {
            string message = "";
            var r = new ReturnSet();
            try
            {
                using (var db = new EmployeeRecordDb(Session))
                {
                    var data = db.ExecQuery<tPersonaEmployment>("select * from dbo.tPersonaEmployment where ID_Persona = {0}", Parameter["ID"].ToInt32());
                    r = new ReturnSet() { Data = data.ToList(), Type = ReturnType.Result };
                    return r;
                }
            }
            catch (Exception ex)
            {
                message = (ex.InnerException ?? ex).Message;
                Logger.LogError(ref message, "LoadEmploymentHistory", Helpers.CurrentUser(Session), "InSys.ITI.EmployeeRecord");
                r = new ReturnSet() { Message = message, Type = ReturnType.Error };
                return r;
            }
        }
        #endregion
    }

   
}
