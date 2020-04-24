angular.module('app')
    .controller('IONSDashboard', ['$scope', '$controller', function ($s, $c) {
        $c('insysTableController', { $scope: $s });

        $s.myController = 'DashBoard';
        $s.MenuCode = 'IONSDashboard';
        $s.LeaveItem = [];
        $s.Announcements = [];
        $s.UpcomingEvent = [];
        $s.TimeKeepingSummary = [];
        $s.FilingTypes = [];
        $s.IsLimit = true;
       

        $s.ShowToggle = function () {
            $s.IsLimit = !$s.IsLimit;
        }

        $s.GetDescriptiveHourName = function (e) {
            //This function is applicable only for minutes.
            var Minute = e;
            var Hours = Minute >= 60 ? Math.trunc(Minute / 60) : 0;
            var RemainingMinutes = Minute % 60;

            var HourName = Hours > 0 ? Hours.toString() + ' hr(s)' : '';
            var MinuteName = RemainingMinutes > 0 ? RemainingMinutes.toString() + ' min(s)' : '';

            if (HourName != '' && MinuteName != '')
                return HourName + ' and ' + MinuteName;
            else
                return HourName + MinuteName;
        }

        $s.LoadInitialDashBoard = function () {
            return $s.Request('LoadForm', { MenuCode: $s.MenuCode }, $s.myController);
        }

        $s.init = function () {
            $s.LoadInitialDashBoard().then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message);
                } else {
                    $s.Announcements = ret.Data.AnnouncementData;
                    $s.TimeKeepingSummary = ret.Data.TimeSummaryData;
                    $s.LeaveItem = ret.Data.LeaveDetailsData;
                    $s.UpcomingEvent = ret.Data.UpcommingEventData;

                    //format timekeeping summary
                    $s.TimeKeepingSummary[0].Absent = $s.TimeKeepingSummary[0].Absent + ' day(s)'
                    $s.TimeKeepingSummary[0].Tardy = $s.GetDescriptiveHourName($s.TimeKeepingSummary[0].Tardy);
                    $s.TimeKeepingSummary[0].Undertime = $s.GetDescriptiveHourName($s.TimeKeepingSummary[0].Undertime);
                    $s.TimeKeepingSummary[0].Overtime = $s.GetDescriptiveHourName($s.TimeKeepingSummary[0].Overtime);

                    $s.$apply();
                }
            });
            $s.InitializeOptions();
        }

        $s.deleteRow = function (selectedRow) {
            // = data.Data.Rows.filter(x => { return x.IsChecked == true; });
            var Ids = Enumerable.From(selectedRow.Data.Rows).Where(function (x) { return x.IsChecked == true }).Select(function (x) { return (x.ID == undefined ? x.Id : x.ID);  }).ToArray();
            //Ready To Delete
        }

        $s.newFormAction = function (controller, template, param) {
            $s.FileApplication(controller, template, param);
        }

        $s.openFormAction = function (controller, template, row) {// function (controller, template, param) {
            var Id = row.ID == undefined ? row.Id : row.ID;
            $s.FileApplication(controller, template, Id);
        }

        $s.FileApplication = function (controller, template, param) {
            //Just Change template and controller depending on the current filing type the user are working with.
            //Create your own controller per filing type
            //Leave //Overtime //OfficialBusiness //MissedLog //COS
            $s.Dialog({
                template: template,//'COS',
                controller: controller,//'dlgLeave',
                size: 'lg',
                windowClass: 'fileApplication-dlg',
                data: param
            })
        }

        $s.ViewPayslip = function () {
            //Load InSys Report here
        }

        //FILING APPLICATIONS {
        $s.FilingOptions = [];
        $s.InitializeOptions = function () {
            $s.FilingOptions['leave'] = {
                trigger: function () { },
                tblOptions: {
                    Data: { Rows: [] },
                    Columns: [
                        { Name: 'Id', Label: '#' },
                        { Name: 'EmployeeName', Label: 'Employee Name'},
                        { Name: 'FiledDate', Label: 'Date Filed', Format: 'LL' },
                        { Name: 'LeavePayrollItem', Label: 'Leave Item' },
                        { Name: 'StartDate', Label: 'From', Format: 'LL' },
                        { Name: 'EndDate', Label: 'To', Format: 'LL' },
                        { Name: 'IsPosted', Label: 'Posted' },
                        { Name: 'Status', Label: 'Status' }
                        
                    ],
                    Filters: [
                        { Name: 'RefNum', Type: 1, Value: null },
                        { Name: 'StartDate', Type: 1, Value: null },
                        { Name: 'EndDate', Type: 1, Value: null },
                        { Name: 'ID_FilingStatus', Type: 1, Value: null },
                        { Name: 'IsPosted', Type: 1, Value: null }
                    ],
                    deleteRow: function (tblOptions) {
                        $s.deleteDetailRow('DeleteRecord', 'Leave', tblOptions, { MenuCode: 'IONSDashBoardFilingApplications_ApplicationsLeave' });
                    },
                    isEditable: false,
                    hasOpenIcon: true,
                    hasOpenIconClick: function (row) { $s.LoadForm(row.Id, $s.FilingOptions['leave']).then(function (res) { $s.openForm('leave', res.Data); }); },
                    openForm: function (row) { $s.LoadForm(row.Id, $s.FilingOptions['leave']).then(function (res) { $s.openForm('leave', res.Data); });}
                },
                Method: 'LoadList',
                Controller: 'Leave',
                Parameter: { MenuCode: 'IONSDashBoardFilingApplications_ApplicationsLeave' }
            };
            $s.FilingOptions['ot'] = {
                trigger: function () { },
                tblOptions: {
                    Data: { Rows: [] },
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'ReferenceNo', Label: 'Reference No.'},
                        { Name: 'WorkDate', Label: 'Date', Format: 'LL'},
                        { Name: 'StartTime', Label: 'From', Format: 'LT'},
                        { Name: 'EndTime', Label: 'To', Format: 'LT'},
                        { Name: 'ComputedHours', Label: 'Total Hours'},
                        { Name: 'ID_FilingStatus', Label: 'Status'},
                        { Name: 'IsPosted', Label: 'Posted'}
                    ],
                    Filters: [
                        { Name: 'ReferenceNo', Type: 1, Value: null },
                        { Name: 'WorkDate', Type: 1, Value: null },
                        { Name: 'ID_FilingStatus', Type: 1, Value: null },
                        { Name: 'IsPosted', Type: 1, Value: null }
                    ],
                    deleteRow: function (tblOptions) {
                        $s.deleteDetailRow('DeleteRecord', 'Overtime', tblOptions, { MenuCode: 'IONSDashBoardFilingApplications_ApplicationsOvertime' });
                    },
                    isEditable: false,
                    hasOpenIcon: true,
                    hasOpenIconClick: function (row) { $s.LoadForm(row.ID, $s.FilingOptions['ot']).then(function (res) { $s.openForm('ot', res.Data); }); },
                    openForm: function (row) { $s.LoadForm(row.ID, $s.FilingOptions['ot']).then(function (res) { $s.openForm('ot', res.Data); }); }
                },
                Method: 'LoadList',
                Controller: 'Overtime',
                Parameter: { MenuCode: 'IONSDashBoardFilingApplications_ApplicationsOvertime' }
            };
            $s.FilingOptions['ob'] = {
                trigger: function () { },
                tblOptions: {
                    Data: { Rows: [] },
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'ReferenceNo', Label: 'Reference No.', ControlType: 'text' },
                        { Name: 'StartDate', Label: 'From', ControlType: 'text' },
                        { Name: 'EndDate', Label: 'To', ControlType: 'text' },
                        { Name: 'FilingStatus', Label: 'Status', ControlType: 'text' },
                        { Name: 'IsPosted', Label: 'Posted', ControlType: 'checkbox' }
                    ],
                    Filters: [
                        { Name: 'ReferenceNo', Type: 1, Value: null },
                        { Name: 'WorkDate', Type: 1, Value: null },
                        { Name: 'ID_FilingStatus', Type: 1, Value: null },
                        { Name: 'IsPosted', Type: 1, Value: null }
                    ],
                    deleteRow: function (tblOptions) {
                        $s.deleteDetailRow('DeleteRecord', 'OB', tblOptions, { MenuCode: 'IONSDashBoardFilingApplications_ApplicationsOB' });
                    },
                    isEditable: false,
                    hasOpenIcon: true,
                    hasOpenIconClick: function (row) { $s.LoadForm(row.ID, $s.FilingOptions['ob']).then(function (res) { $s.openForm('ob', res.Data); }); },
                    openForm: function (row) { $s.LoadForm(row.ID, $s.FilingOptions['ob']).then(function (res) { $s.openForm('ob', res.Data); }); }
                },
                Method: 'LoadList',
                Controller: 'OB',
                Parameter: { MenuCode: 'IONSDashBoardFilingApplications_ApplicationsOB' }
            };
            $s.FilingOptions['ml'] = {
                trigger: function () { },
                tblOptions: {
                    Data: { Rows: [] },
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'ReferenceNo', Label: 'Reference No.', ControlType: 'text' },
                        { Name: 'WorkDate', Label: 'Date', ControlType: 'text' },
                        { Name: 'FilingStatus', Label: 'Status', ControlType: 'text' },
                        { Name: 'IsPosted', Label: 'Posted', ControlType: 'checkbox' }
                    ],
                    Filters: [
                        { Name: 'ReferenceNo', Type: 1, Value: null },
                        { Name: 'WorkDate', Type: 1, Value: null },
                        { Name: 'ID_FilingStatus', Type: 1, Value: null },
                        { Name: 'IsPosted', Type: 1, Value: null }
                    ],
                    deleteRow: function (tblOptions) {
                        $s.deleteDetailRow('DeleteRecord', 'MissedLog', tblOptions, { MenuCode: 'IONSDashBoardFilingApplications_ApplicationsMissedLog' });
                    },
                    isEditable: false,
                    hasOpenIcon: true,
                    hasOpenIconClick: function (row) { },
                    openForm: function () { }
                },
                Method: 'LoadList',
                Controller: 'MissedLog',
                Parameter: { MenuCode: 'IONSDashBoardFilingApplications_ApplicationsMissedLog' },
                isEditable: false,
                hasOpenIcon: true,
                hasOpenIconClick: function (row) { $s.LoadForm(row.ID, $s.FilingOptions['ml']).then(function (res) { $s.openForm('ml', res.Data); }); },
                openForm: function (row) { $s.LoadForm(row.ID, $s.FilingOptions['ml']).then(function (res) { $s.openForm('ml', res.Data); }); }
            };
            $s.FilingOptions['cos'] = {
                trigger: function () { },
                tblOptions: {
                    Data: { Rows: [] },
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'ReferenceNo', Label: 'Reference No.', ControlType: 'text' },
                        { Name: 'StartDate', Label: 'Date', ControlType: 'text' },
                        { Name: 'EndDate', Label: 'Date', ControlType: 'text' },
                        { Name: 'FilingStatus', Label: 'Status', ControlType: 'text' },
                        { Name: 'IsPosted', Label: 'Posted', ControlType: 'checkbox' }
                    ],
                    Filters: [
                        { Name: 'ReferenceNo', Type: 1, Value: null },
                        { Name: 'StartDate', Type: 1, Value: null },
                        { Name: 'EndDate', Type: 1, Value: null },
                        { Name: 'ID_FilingStatus', Type: 1, Value: null },
                        { Name: 'IsPosted', Type: 1, Value: null }
                    ],
                    deleteRow: function (tblOptions) {
                        $s.deleteDetailRow('DeleteRecord', 'ChangeOfSchedule', tblOptions, { MenuCode: 'IONSDashBoardFilingApplications_ApprovalsChangeOfSchedule' });
                    },
                    isEditable: false,
                    hasOpenIcon: true,
                    hasOpenIconClick: function (row) { },
                    openForm: function () { }
                },
                Method: 'LoadList',
                Controller: 'ChangeOfSchedule',
                Parameter: { MenuCode: 'IONSDashBoardFilingApplications_ApprovalsChangeOfSchedule' }
            };
        }
        $s.loadTabSchema = function (ft, table) {
            if ($s.FilingOptions[ft].tblOptions.Data != undefined)
                if ($s.FilingOptions[ft].tblOptions.Data.Rows != undefined)
                    if ($s.FilingOptions[ft].tblOptions.Data.Rows.length > 0)
                        return;
            $s.Request('LoadSchema', { Table: table }, 'Menu').then(function (ret2) {
                if (ret2.Type == 2) {
                    $s.SetSystemStatus(ret2.Message, 'error');
                    $s.$apply();
                } else {
                    $s.FilingOptions[ft].tblOptions.TableSchema = ret2.Data;
                    $s.FilingOptions[ft].tblOptions.HasNew = true;
                    $s.FilingOptions[ft].tblOptions.HasEdit = true;
                    $s.FilingOptions[ft].tblOptions.HasDelete = true;
                    $s.FilingOptions[ft].trigger().then(function (tblOptions) {
                        $s.FilingOptions[ft].tblOptions = tblOptions;
                        $s.FilingOptions[ft].tblOptions.newForm = function () {
                            var data = $s.PlotDefault({}, ret2.Data, 0);
                            $s.LoadForm(data.ID, $s.FilingOptions[ft]).then(function (res) { $s.openForm(ft, res.Data); });
                        }
                    });
                }
            });
        }
        $s.LoadForm = function (id, option) {
            return $s.Request('LoadForm', { ID: id, MenuCode: option.Parameter.MenuCode }, option.Controller);
        }
        $s.openForm = function (ft,data) {
            var template;
            var controller;
            switch (ft) {
                case 'leave':
                    template = 'IONS_Leave';
                    controller = 'leaveDlgCtrl';
                    break;
                case 'ot':
                    template = 'IONS_Overtime';
                    controller = 'overtimeDlgCtrl';
                    break;
                case 'ob':
                    template = 'IONS_OfficialBusiness';
                    controller = 'officialBusinessDlgCtrl';
                    break;
                case 'ml':
                    template = 'IONS_MissedLog';
                    controller = 'missedLogDlgCtrl';
                    break;
                case 'cos':
                    template = 'IONS_COS';
                    controller = 'changeOfScheduleDlgCtrl';
                    break;
                default:
                    console.log('Invalid filing type.');
            }

            var dd = $s.Dialog({
                template: template,
                controller: controller,
                size: 'lg',
                windowClass: 'fileApplication-dlg',
                data: { data, Controller: controller }
            });
            dd.result.then(function () {
                $s.Request($s.FilingOptions[ft].Method, { MenuCode: $s.FilingOptions[ft].Parameter.MenuCode }, $s.FilingOptions[ft].Controller).then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message);
                    } else {
                        console.log(ret.Data.Rows)
                        $s.FilingOptions[ft].tblOptions.Data.Rows = ret.Data.Rows;
                        $s.FilingOptions[ft].tblOptions.Data.Rows.reverse();
                        $s.SetSystemStatus('Ready');
                    }
                    $s.$apply();
                });
            });
        }
        //} FILING APPLICATIONS
        $s.init();
    }])
    .controller('leaveDlgCtrl', ['$scope', '$controller', '$uibModalInstance', 'dData', '$state', function ($s, $c, $mi, $dlgData, $st ) {
        $c('BaseController', { $scope: $s });
        $s.Data = {};
        $s.Details = [];
        $s.ApprovalHistories = [];
        $s.MenuCode = 'IONSDashBoardFilingApplications_ApplicationsLeave';
        $s.ApiController = 'Leave';
        $s.PayrollItems = [];
        $s.FilingStatus = [];
        $s.TableSchema = [];
        
        $s.Init = function () {
            $s.Data = $dlgData.data.Form;//$dlgData.Options.Data.Rows;
            $s.PayrollItems = $dlgData.data.PayrollItems.Data.Rows;
            $s.FilingStatus = $dlgData.data.FilingStatus;
            $s.TableSchema = $dlgData.data.Schema;
            $s.LoadApprovalHistory();
            console.log(1,$dlgData.data);
        }
        $s.Save = function (post) {
            $s.Data.IsPosted = post ? 1 : 0;
            if ($s.IsTabsValid('form.leaveFrm', $s.TableSchema))
            {
               
                $s.Request('SaveForm', { Data: $s.Data, Post: $s.varPost, MenuCode: $s.MenuCode }, $s.ApiController).then(function (res) {
                    console.log('leave result',res);
                });
                //$s.Request('SaveForm', { Data: $s.Data, Post: post, MenuCode: $s.MenuCode }, $s.ApiController).then(function (res) {
            }
        }
        $s.Close = function () {
            $mi.close();
        }
        $s.LoadApprovalHistory = function () {
            $s.Request('LoadApprovalHistory', { Data: $s.Data, MenuCode: $s.MenuCode }, $s.ApiController).then(function (res) {
                if (res.Type == 2) {
                    $s.SetSystemStatus(res.Message, 'error');
                    $s.Prompt('An error occured. Please contact your administrator', 'Error');
                    $s.$apply();
                } else {
                    $s.ApprovalHistories = res.Data;
                }
            });
        }
    }])
    .controller('overtimeDlgCtrl', ['$scope', '$controller', '$uibModalInstance', 'dData','$state', function ($s, $c, $mi, $dlgData,$st ) {
        $c('BaseController', { $scope: $s });
        $s.MenuCode = 'IONSDashBoardFilingApplications_ApplicationsOvertime';
        $s.ApiController = 'Overtime';
        $s.Data = {};
        $s.TableSchema = [];
        $s.Details = [];
        $s.ApprovalHistories = [];
        $s.WorkCredits = [];
        $s.Init = function () {
            $s.Data = $dlgData.data.Form;
            $s.TableSchema = $dlgData.data.Schema;
            $s.WorkCredits = $dlgData.data.WorkCredits.Data.Rows;
            
            $s.LoadApprovalHistories();
        }
        $s.Save = function (post) {
            if ($s.IsTabsValid('form.overtimeFrm', $s.TableSchema))
            {
                $s.Request('SaveForm', { Data: $s.Data, Post: post, MenuCode: $s.MenuCode }, $s.ApiController).then(function (res) {
                    if (res.Type == 2)
                    {
                        $s.Prompt('An error occured. Please contact your Administrator', 'Error','error');
                        $s.$apply();
                        return;
                    }
                    else
                    {
                        if (post) {
                            $s.Prompt('Save and Post successful!', 'Success');
                        } else
                        {
                            $s.Prompt('Save successful!', 'Success');
                        }
                        $s.Data = res.Data;
                    }
                });
            }
        }
        $s.Cancel = function () {
            
        }
        $s.Unpost = function () {
            $s.Confirm('Do you wish to continue? Once unpost, Filing will return to 1st approver.','Warning','warning').then(function () {
                $s.Request('ExecuteAction', { Action: { Name: 'UnpostOvertime', Parameter: { ID: $s.Data.ID } }, MenuCode: $s.MenuCode }, $s.ApiController).then(function (res) {
                    if (res.Type == 2) {
                        $s.Prompt('An error occured. Please contact your Administrator', 'Error','error');
                        $s.$apply();
                        return;
                    }
                    else {
                        $s.Prompt('Unpost successful!', 'Success');
                        $s.Data = res.Data;
                        $s.$apply();
                    }
                });
            });
        }
        $s.Post = function () {
            $s.Confirm('Do you wish to continue?', 'Confirm').then(function () {
                $s.Request('ExecuteAction', { Action: { Name: 'PostOvertime', Parameter: { ID: $s.Data.ID } }, MenuCode: $s.MenuCode }, $s.ApiController).then(function (res) {
                    if (res.Type == 2) {
                        $s.Prompt('An error occured. Please contact your Administrator', 'Error','error');
                        $s.$apply();
                        return;
                    }
                    else {
                        $s.Data = res.Data;
                        $s.$apply();
                    }
                });
            });
        }
        $s.LoadApprovalHistories = function () {
            $s.Request('LoadApprovalHistory', { Data: $s.Data, MenuCode: $s.MenuCode }, $s.ApiController,false).then(function (res) {
                if (res.Type == 2) {
                    $s.SetSystemStatus(res.Message, 'error');
                    $s.Prompt('An error occured. Please contact your Administrator', 'Error','error');
                    $s.$apply();
                    return;
                } else {
                    $s.ApprovalHistories = res.Data;
                }
            });
        }
        $s.Close = function () {
            $mi.close();
        }
    }])
    .controller('officialBusinessDlgCtrl', ['$scope', '$controller', '$uibModalInstance', 'dData', function ($s, $c, $mi, $dlgData, ) {
        $c('BaseController', { $scope: $s });
        $s.Data = {};
        $s.Details = [];
        $s.ApprovalHistories = [];
        $s.MenuCode = 'IONSDashBoardFilingApplications_ApplicationsOB';
        $s.ApiController = 'OB';

        $s.Init = function () {
            $s.Data = $dlgData.Options.Data.Rows;
            $s.LoadApprovalHistory();
        }
        $s.Save = function (post) {
            
        }
        $s.Close = function () {
            $mi.close();
        }
        $s.LoadApprovalHistory = function () {
            $s.Request('LoadApprovalHistory', { Data: $s.Data, MenuCode: $s.MenuCode }, $s.ApiController).then(function (res) {
                if (res.Type == 2) {
                    $s.SetSystemStatus(res.Message, 'error');
                    $s.Prompt('An error occured. Please contact your administrator', 'Error');
                    $s.$apply();
                } else {
                    $s.ApprovalHistories = res.Data;
                }
            });
        }
    }])
    .controller('missedLogDlgCtrl', ['$scope', '$controller', '$uibModalInstance', 'dData', function ($s, $c, $mi, $dlgData, ) {
        $c('BaseController', { $scope: $s });
        $s.Data = {};
        $s.Details = [];
        $s.ApprovalHistories = [];
        $s.MenuCode = 'IONSDashBoardFilingApplications_ApplicationsMissedLog';
        $s.ApiController = 'MissedLog';

        $s.Init = function () {
            $s.Data = $dlgData.Options.Data.Rows;
            $s.LoadApprovalHistory();
        }
        $s.Save = function (post) {
        }
        $s.Close = function () {
            $mi.close();
        }
        $s.LoadApprovalHistory = function () {
            $s.Request('LoadApprovalHistory', { Data: $s.Data, MenuCode: $s.MenuCode }, $s.ApiController).then(function (res) {
                if (res.Type == 2) {
                    $s.SetSystemStatus(res.Message, 'error');
                    $s.Prompt('An error occured. Please contact your administrator', 'Error');
                    $s.$apply();
                } else {
                    $s.ApprovalHistories = res.Data;
                }
            });
        }
    }])
    .controller('changeOfScheduleDlgCtrl', ['$scope', '$controller', '$uibModalInstance', 'dData', function ($s, $c, $mi, $dlgData, ) {
        $c('BaseController', { $scope: $s });
        $s.Data = {};
        $s.Details = [];
        $s.ApprovalHistories = [];
        $s.MenuCode = 'IONSDashBoardFilingApplications_ApplicationsChangeOfSchedule';
        $s.ApiController = 'ChangeOfSchedule';

        $s.Init = function () {
            $s.Data = $dlgData.Options.Data.Rows;
            $s.LoadApprovalHistory();
        }
        $s.Save = function (post) {

        }
        $s.Close = function () {
            $mi.close();
        }
        $s.LoadApprovalHistory = function () {
            $s.Request('LoadApprovalHistory', { Data: $s.Data, MenuCode: $s.MenuCode }, $s.ApiController).then(function (res) {
                if (res.Type == 2) {
                    $s.SetSystemStatus(res.Message, 'error');
                    $s.Prompt('An error occured. Please contact your administrator', 'Error');
                    $s.$apply();
                } else {
                    $s.ApprovalHistories = res.Data;
                }
            });
        }
    }]);