using InSys.Helper;
using InSys.ITI.Controller;
using InSys.ITI.Models.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using z.Data;

namespace InSys.ITI.LeaveAdjustment
{
    public class LeaveAdjustmentController: BaseController
    {
        public LeaveAdjustmentController(IHostingEnvironment hostingEnvironment, IAntiforgery _antiForgery) : base(hostingEnvironment, _antiForgery)
        {
        }

        public virtual async Task<Result> GetLeaveAdjustmentDetails() => await TaskResult(r => {
            try {
                var ID_LeaveAdjustment = Parameter["ID_LeaveAdjustment"].ToInt32();
                using (var db = new LeaveAdjustmentDb(Session)){
                    var record = db.ExecQuery<vLeaveAdjustment_Detail>(@"SELECT ld.Id, ld.ID_LeaveAdjustment, ld.ID_Employee, ld.ID_LeaveType, ld.EffectiveDate, ld.Value,
	                                                                    ld.Remarks, ld.CreatedAt, ld.ID_CreatedBy, ld.ModifiedAt, ld.ID_ModifiedBy,
	                                                                    e.EmployeeName, lt.Name AS LeaveType
                                                                    FROM tLeaveAdjustment_Detail ld
	                                                                    INNER JOIN vEmployees e ON e.ID_Employee = ld.ID_Employee
	                                                                    INNER JOIN tLeaveType lt ON lt.ID = ld.ID_LeaveType
                                                                    WHERE ld.ID_LeaveAdjustment = {0}", ID_LeaveAdjustment).ToList();
                    //var LeaveAdjustmentSchema = Helpers.GetSchema("tLeaveAdjustment_Detail");

                    r.ResultSet = new ReturnSet() { Data = new { Data = record }, Type = ReturnType.Result };
                }
            }
            catch (Exception ex) {
                string msg = (ex.InnerException ?? ex).Message;
                Logger.LogError(ref msg, "GetLeaveAdjustmentDetails", Helpers.CurrentUser(Session), "InSys.ITI.LeaveAdjustment");
                r.ResultSet = new ReturnSet() { Message = msg, Type = ReturnType.Error };
            }

            return r;
        
        });
    }
}
