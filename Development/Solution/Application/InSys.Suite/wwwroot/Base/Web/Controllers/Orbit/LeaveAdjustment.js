angular.module('app')
    .controller('OrbitLeaveAdjustment', ['$scope', '$controller', '$priv', '$state', function ($s, $c, $priv, $st) {
        $c('insysTableController', { $scope: $s });
        $s.MenuPrivileges = $priv.Data;
        $s.RecordID = $st.params.ID == undefined ? '' : $st.params.ID;
        $s.Schema = {};
        $s.tableSchema = [];
        $s.IsDetailsTabClicked = 0;
        $s.LeaveAdjustmentDetailRecord = [];
        $s.LeaveAdjustmentDetailSchema = [];

        $s.myController = 'LeaveAdjustment';
        $s.MenuCode = 'OrbitLeaveAdjustment';

        $s.tblOptions = {
            Columns: [
                { Name: 'ID', Label: '#' },
                { Name: 'RefNum', Label: 'Ref#' },
                { Name: 'Name', Label: 'Name' },
                { Name: 'ReferenceDate', Label: 'Reference Date' },
                { Name: 'Description', Label: 'Description' },
                { Name: 'CreatedBy', Label: 'Created By' },
                { Name: 'IsLocked', Label: 'Is Locked' },
                { Name: 'IsPosted', Label: 'Is Posted' }
            ],
            HasNew: $s.MenuPrivileges.HasNew,
            HasDelete: $s.MenuPrivileges.HasDelete,
            HasEdit: $s.MenuPrivileges.HasEdit,
            Filters: [

                { Name: 'RefNum', Type: 9, Label: 'Ref#' },
                { Name: 'Name', Type: 9, Label: 'Name' },
                { Name: 'ReferenceDate', Type: 9, Label: 'Reference Date' },
                { Name: 'Description', Type: 9, Label: 'Description' },
                { Name: 'CreatedBy', Type: 9, Label: 'Created By' },
                { Name: 'IsLocked', Type: 1, ControlType: 'radio', Label: 'Is Locked' },
                { Name: 'IsPosted', Type: 1, ControlType: 'radio', Label: 'Is Posted' }
            ]
        };

        //// BEGIN --- Load record
        $s.Init = function () {
            if ($s.RecordID != '') {
                $s.SetSystemStatus('Loading record #' + $s.RecordID, 'loading');
                $s.LoadForm().then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                    } else {
                        $s.Schema = $s.PlotDefault(ret.Data.Form, ret.Data.Schema, $s.RecordID);
                        $s.TableSchema = ret.Data.Schema;
                        $s.LeaveAdjustmentDetailSchema = ret.Data.DetailSchema;
                        $s.SetSystemStatus('Ready');
                    }
                    $s.$apply();
                })
            } else {
                $s.LoadTable($s.tblOptions, 'LoadList', $s.myController, { MenuCode: $s.MenuCode }).then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message);
                    } else {
                        $s.SetSystemStatus('Ready');
                    }
                    $s.$apply();
                });
            }
        }

        $s.LoadForm = function () {
            return $s.Request('LoadForm', { ID: $s.RecordID, MenuCode: $s.MenuCode }, $s.myController);
        }
        $s.SetSystemStatus('Loading table', 'loading');
        $s.Init();
        //// END ----- Load record

        ////Save New - Edit - Delete
        $s.saveForm = function () {
            if ($s.IsTabsValid('form.leaveadjustmentdetail', $s.LeaveAdjustmentDetailSchema)) {
                if (!$s.tblOptions.HasEdit && $s.RecordID > 0) {
                    $s.Prompt('You are not allowed to update this record.');
                    return;
                }
                $s.SetSystemStatus('Saving record #' + $s.RecordID, 'loading');
                $s.Request('SaveForm', { Data: $s.Schema, Details: $s.LeaveAdjustmentDetailRecord, DetailsToDelete: $s.DeletedLeaveAdjustmentDetails, MenuCode: $s.MenuCode }, $s.myController).then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                        $s.$apply();
                    } else {
                        $s.Prompt('Save successful.');
                        $s.SetDirtyFormToFalse($s.form);
$st.go($st.current.name, { ID: ret.Data }, { reload: true });
                    }
                });
            }

        }

        $s.delete = function () {
            $s.deleteRow('DeleteRecord', $s.myController, { MenuCode: $s.MenuCode });
        }

        /////////////DAILY SCHEDULE DETAILS
        $s.DeletedLeaveAdjustmentDetails = [];
        $s.HourTypes = [];

        $s.SelectAllLeaveAdjustmentDetailRows = false;
        $s.CheckAllLeaveAdjustmentDetailRecords = function () {
            $s.SelectAllLeaveAdjustmentDetailRows = !$s.SelectAllLeaveAdjustmentDetailRows;
            Enumerable.From($s.LeaveAdjustmentDetailRecord).ForEach(function (rowDetails) {
                rowDetails.IsChecked = $s.SelectAllLeaveAdjustmentDetailRows;
            });
        }

        $s.ImportDetails = function () { }
        $s.ExportDetails = function () { }
        $s.ExportDetails = function () { }
        $s.AddEmployee = function () { }

        $s.delLeaveAdjustmentDetails = function () {
            var rows = Enumerable
                .From($s.LeaveAdjustmentDetailRecord)
                .Where(function (x) { return x.IsChecked == true })
                .Select(function (x) { return x; }).ToArray();

            rowLength = rows.length;

            if (rowLength > 0) {
                $s.Confirm('Are you sure you want to delete (' + rowLength + (rowLength > 1 ? ') records?' : ') record?'), 'Delete Record').then(function (r) {
                    for (var y = 0; y < rowLength; y++) {
                        var Index = $s.LeaveAdjustmentDetailRecord.indexOf(rows[y]);
                        if ($s.LeaveAdjustmentDetailRecord[Index].Id > 0) {
                            $s.DeletedLeaveAdjustmentDetails.push($s.LeaveAdjustmentDetailRecord[Index].Id);
                        }
                        $s.LeaveAdjustmentDetailRecord.splice(Index, 1);
                    }
                });
            }
        }

        $s.newLeaveAdjustmentDetails = function () {
            var newRecord = {
                ID: 0,
                ID_LeaveAdjustment: $s.Schema.ID,
                ID_LeaveSource: 1,
                ID_Employee: null,
                ID_LeaveType: null,
                EffectiveDate: new Date(),
                Value: 0.00,
                Remarks: '',
                CreatedAt: new Date(),
                ID_CreatedBy: 0,
                ID_ModifiedBy: 0,
                ModifiedAt: new Date()
            }
            $s.LeaveAdjustmentDetailRecord.push(newRecord);
        }


        $s.employeeLookUp = [];
        $s.leaveTypeLookUp = [];

        $s.employeeLookUp = {
            tblOptions: {
                Columns: [
                    { Name: 'ID', Label: '#' },
                    { Name: 'Name', Label: 'Employee' }
                ]
            },
            Filters: [
                { Name: 'Name', Type: 9, Label: 'Employee' },
                //{ Name: 'ID_AssignedEmployee', Type: 1, ControlType: 'text', Label: 'Employee', Options: { controller: 'GetLookUp', method: 'LoadLookUp', parameter: { Name: 'Employee' } } }
            ],
            method: 'LoadLookUp',
            controller: $s.myController,
            parameter: { Name: 'employee', MenuCode: $s.MenuCode }
        };

        $s.leaveTypeLookUp  = {
            tblOptions: {
                Columns: [
                    { Name: 'ID', Label: '#' },
                    { Name: 'Name', Label: 'Leave Type' }
                ]
            },
            Filters: [
                { Name: 'Name', Type: 9, Label: 'Leave Type' },
                //{ Name: 'ID_AssignedEmployee', Type: 1, ControlType: 'text', Label: 'Employee', Options: { controller: 'GetLookUp', method: 'LoadLookUp', parameter: { Name: 'Employee' } } }
            ],
            method: 'LoadLookUp',
            controller: $s.myController,
            parameter: { Name: 'leavetype', MenuCode: $s.MenuCode }
        };

        $s.GetLeaveAdjustmentDetails = function () {
            $s.Request('GetLeaveAdjustmentDetails', { ID_LeaveAdjustment: $s.Schema.ID, MenuCode: $s.MenuCode }, $s.myController).then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message, 'error');
                    $s.$apply();
                } else {
                    $s.LeaveAdjustmentDetailRecord = ret.Data.Data;
                    $s.SetSystemStatus('Ready');
                }
            });
        }
        

    }]);


//Validate Duplicate Duplicate LeaveType and EmployeeId