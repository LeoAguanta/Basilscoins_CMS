using InSys.ITI.Controller;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using z.Data;

namespace InSys.ITI.ApproverMatrix
{
    public class ApproverMatrixController : BaseController
    {
        public ApproverMatrixController(IHostingEnvironment hostingEnvironment, IAntiforgery _antiForgery) : base(hostingEnvironment, _antiForgery)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> LoadApproverByModule() => await TaskResult(r => {
            var approverMatrix = new ApproverMatrix(Session,Parameter);
            r.ResultSet = approverMatrix.LoadApproverByModule();
            return r;
        });
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> LoadEmployeeList() => await TaskResult(r => {
            var approverMatrix = new ApproverMatrix(Session, Parameter);
            r.ResultSet = approverMatrix.LoadEmployeeList();
            return r;
        });
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> LoadApproverSchema() => await TaskResult(r => {
            var approverMatrix = new ApproverMatrix(Session, Parameter);
            r.ResultSet = approverMatrix.LoadApproverSchema();
            return r;
        });
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> LoadFilingModules() => await TaskResult(r => {
            var approverMatrix = new ApproverMatrix(Session, Parameter);
            r.ResultSet = approverMatrix.LoadFilingModules();
            return r;
        });
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> LoadEmployees() => await TaskResult(r => {
            var approverMatrix = new ApproverMatrix(Session, Parameter);
            r.ResultSet = approverMatrix.LoadEmployees();
            return r;
        });
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> PostApproverMatrixTemplate() => await TaskResult(r => {
            var approverMatrix = new ApproverMatrix(Session, Parameter);
            r.ResultSet = approverMatrix.PostApproverMatrixTemplate();
            return r;
        });
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> LoadOrgTypes() => await TaskResult(r => {
            var approverMatrix = new ApproverMatrix(Session, Parameter);
            r.ResultSet = approverMatrix.LoadOrgTypes();
            return r;
        });
    }
}
