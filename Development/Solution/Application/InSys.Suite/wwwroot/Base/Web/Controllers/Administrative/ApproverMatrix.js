
angular.module('app')
    .controller('AdministrativeApproverMatrix', ['$scope', '$controller', '$priv', '$state', function ($s, $c, $priv, $st) {
        $c('insysTableController', { $scope: $s });
        $s.MenuPrivileges = $priv.Data;
        $s.RecordID = $st.params.ID == undefined ? '' : $st.params.ID;
        $s.Schema = {};
        $s.TableSchema = [];
        $s.Employees = [];
        $s.EmployeeTmpList = [];
        $s.tblOptions = {
            Columns: [
                { Name: 'ID', Label: '#' },
                { Name: 'Name' },
                { Name: 'IsActive', Label: 'Active' },
            ],
            HasNew: $s.MenuPrivileges.HasNew,
            HasDelete: $s.MenuPrivileges.HasDelete,
            HasEdit: $s.MenuPrivileges.HasEdit,
            Filters: [
                { Name: 'Name', Type: 9, Label: 'Name' },
                { Name: 'IsActive', Type: 1, ControlType: 'radio', Label: 'Active' },
            ]
        };

        $s.MCode = 'AdministrativeApproverMatrix';
        $s.FilingModulesSelected = [];
        $s.CurrentFilingModule = null;

        //lookupconfig
        $s.companyLookup = {
            tblOptions: {
                Columns: [
                    { Name: 'ID', Label: '#' },
                    { Name: 'Name', Label: 'Company' }
                ]
            },
            method: 'LoadCompanyList',
            controller: 'ApproverMatrix'
        };
        $s.updateCompanyLookup = function (data) {
            $s.Schema.ID_Company = data.ID;
            $s.Schema.Company = data.Name;
        }
        //

        $s.Init = function () {
            if ($s.RecordID != '') {
                $s.SetSystemStatus('Loading record #' + $s.RecordID, 'loading');
                $s.LoadForm().then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                    } else {
                        $s.Schema = $s.PlotDefault(ret.Data.Form, ret.Data.Schema, $s.RecordID);
                        $s.TableSchema = ret.Data.Schema;
                        $s.FilingModulesSelected = ret.Data.FilingModules;
                        $s.Employees = ret.Data.AllEmployees;
                        $s.EmployeeTmpList = angular.copy($s.Employees);
                        $s.SetSystemStatus('Ready');
                    }
                    $s.$apply();
                })
            } else {
                $s.LoadTable($s.tblOptions, 'LoadList', 'ApproverMatrix', { MenuCode: $s.MCode }).then(function (ret) {
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
            return $s.Request('LoadForm', { ID: $s.RecordID, MenuCode: $s.MCode }, 'ApproverMatrix');
        }

        $s.saveForm = function () {
            if ($s.IsTabsValid('form.approvermatrix')) {
                if (!$s.tblOptions.HasEdit && $s.RecordID > 0) {
                    $s.Prompt('You are not allowed to update this record.');
                    return;
                }
                $s.SetSystemStatus('Saving record #' + $s.RecordID, 'loading');
                var countError = 0;
                var errorMsg = "";
                Enumerable.From($s.FilingModulesSelected).ForEach(function (fm) {
                    if (fm.Default != undefined) {
                        if (fm.Default.length == 0) {
                            countError += 1;
                            errorMsg = 'Atleast 1 approver is required for module ' + fm.Name + '.';
                            return;
                        } else {
                            Enumerable.From(fm.Default).ForEach(function (approvers, idx) {
                                approvers.ID_Level = idx + 1;
                                if (approvers.ID_Employee == null || approvers.ID_Employee == undefined) {
                                    countError += 1;
                                    errorMsg = 'Approver 1 is required.';
                                    return;
                                }
                            })
                        }
                    }
                });
                if (countError > 0) {
                    $s.Prompt(errorMsg);
                    $s.SetSystemStatus('Ready');
                    return;
                } else {
                    var selectedEmployees = Enumerable.From($s.Employees).Where(function (x) { return x.IsChecked === true }).ToArray();
                    var deletedEmployees = Enumerable.From($s.Employees).Where(function (x) { return x.ID > 0 && x.IsChecked === false }).Select(function (y) { return y.ID }).ToArray();

                    $s.Request('SaveForm', { Data: $s.Schema, FilingModules: $s.FilingModulesSelected, Employees: selectedEmployees, DeletedEmployees: deletedEmployees, MenuCode: $s.MCode }, 'ApproverMatrix').then(function (ret) {
                        if (ret.Type == 2) {
                            $s.SetSystemStatus(ret.Message, 'error');
                            $s.$apply();
                        } else {
                            $s.Prompt('Changes had been successfully saved.');
                            $s.SetDirtyFormToFalse($s.form);
$st.go($st.current.name, { ID: ret.Data }, { reload: true });
                        }
                    });
                }
            }
        }

        $s.delete = function () {
            $s.deleteRow('DeleteRecord', 'ApproverMatrix', { MenuCode: $s.MCode })
        }

        $s.openFilingModules = function () {
            $s.Dialog({
                template: 'FilingModules',
                controller: 'dlgFilingModules',
                size: 'md',
                windowClass: 'filingModules-dlg',
                data: { ID: $s.Schema.ID, Data: $s.FilingModulesSelected }
            }).result.then(function (ret) {
                if (ret != undefined) {
                    $s.FilingModulesSelected = ret;
                    $s.CurrentFilingModule = null;
                    $s.ApproverSelected = [];
                }
            });
        }

        $s.removeModule = function (mod) {
            if ($s.CurrentFilingModule == mod.ID_FilingModules) $s.CurrentFilingModule = null; $s.ApproverSelected = [];
            var idx = Enumerable.From($s.FilingModulesSelected).Select(x => x.ID_FilingModules).IndexOf(mod.ID_FilingModules);
            if ($s.FilingModulesSelected[idx].ID_FilingModules == $s.CurrentFilingModule.ID_FilingModules) {
                $s.CurrentFilingModule = null;
            }
            if ($s.Schema.DeletedModules == undefined) $s.Schema.DeletedModules = [];
            if (mod.ID > 0) $s.Schema.DeletedModules.push(mod.ID);
            $s.FilingModulesSelected.splice(idx, 1);
        }

        $s.LoadApproverSchema = function () {
            if ($s.CurrentFilingModule == null) {
                $s.SetSystemStatus('Please select a Filing Type.', 'warning');
                return;
            }
            if ($s.CurrentFilingModule.Default == undefined) $s.CurrentFilingModule.Default = [];
            if ($s.CurrentFilingModule.Default.length < 5) {
                $s.Request('LoadApproverSchema', { MenuCode: $s.MCode }, 'ApproverMatrix').then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                        $s.$apply();
                    } else {
                        var sc = $s.PlotDefault(ret.Data.Form, ret.Data.Schema, 0);
                        sc.ID_Level = $s.CurrentFilingModule.Default.length + 1;
                        sc.ID_Approver_Module = $s.CurrentFilingModule.ID;
                        $s.CurrentFilingModule.Default.push(sc);
                    }
                });
            } else {
                $s.SetSystemStatus('Max approver level reached.', 'warning');
            }
        }

        $s.loadApprovers = function (mod, idx) {
            $s.CurrentFilingModule = mod;
            $('.am-filing-item').removeClass('selected');
            $('#afi-' + idx).addClass('selected');
            $('#afim-' + idx).addClass('selected');

            if (mod.ID == 0) return;
            if (mod.Default != undefined && mod.Default.length > 0) return;
            $s.SetSystemStatus('Loading approvers for ' + mod.Name, 'loading')
            $s.Request('LoadApproverByModule', { ID: mod.ID, MenuCode: $s.MCode }, 'ApproverMatrix').then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message, 'error');
                    $s.$apply();
                } else {
                    $s.CurrentFilingModule.Default = ret.Data;
                    $s.SetSystemStatus('Ready');
                }
            })
        }

        $s.LoadEmployees = function (id) {
            var selectedEmployees = Enumerable.From($s.Employees).Where(function (x) { return x.IsChecked == true; }).Select(function (x) { return x.ID_Employee; }).ToArray();
            $s.Dialog({
                template: 'ApproverMatrixLoadEmployess',
                controller: 'dlgApproverMatrixLoadEmployess',
                size: 'md',
                windowClass: 'ApproverMatrixLoadEmployess-dlg',
                data: { ID: id, Data: selectedEmployees, ID_Company: $s.Schema.ID_Company || $s.Session('ID_Company') }
            }).result.then(function (ret) {
                if (ret != undefined) {
                    var data = ret;
                    if (data.length > 0) {
                        console.log(data)
                        $s.Employees = [...data,...$s.Employees.filter(x => x.IsChecked == true) ];
                    $s.EmployeeTmpList = angular.copy($s.Employees);
                }
                }
            });
        }
        $s.checkAll = false;
        $s.checkAllEmp = function () {
            $s.checkAll = !$s.checkAll;
            Enumerable.From($s.EmployeeTmpList).ForEach(function (x) {
                x.IsChecked = $s.checkAll;
            });
            Enumerable.From($s.Employees).ForEach(function (x) {
                x.IsChecked = $s.checkAll;
            });
            $s.selectedCount = Enumerable.From($s.Employees).Count(x => x.IsChecked);
        }
        $s.checkSource = function (data) {
            var source = $s.Employees.find(x => { return x.ID_Employee == data.ID_Employee });
            source.IsChecked = data.IsChecked;
            $s.selectedCount = Enumerable.From($s.Employees).Count(x => x.IsChecked);
        }
        $s.AddApprover = function (targetColumn, model) {
            $s.Dialog({
                template: 'EmployeeList',
                controller: 'dlgApproverEmployeeList',
                size: 'md',
                windowClass: 'select-approver-dlg',
                data: { Data: $s.Schema.ID_Company || $s.Session('ID_Company') }
            }).result.then(function (ret) {
                if (ret != undefined) {
                    model[targetColumn] = ret.ID;
                    model[targetColumn.substr(3)] = ret.Name;
                }
            });
        }

        $s.ApplyTemplate = function (id) {
            $s.Confirm("Are you sure you want to apply this template?", "Apply").then(function () {
                $s.Request('PostApproverMatrixTemplate', { ID: id, MenuCode: $s.MCode }, 'ApproverMatrix').then(function (ret) {
                    if (ret.Type === 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                        $s.$apply();
                    } else {
                        $s.Prompt("Template has been applied successfully!", "Success");
                        $s.SetSystemStatus('Ready');
                    }
                });
            });
        }

        $s.RemoveApprover = function (targetColumn, model) {
            model[targetColumn] = null;
            model[targetColumn.substr(3)] = null;
        }

        $s.RemoveDefaultApprover = function (idx) {
            var deleted = $s.CurrentFilingModule.Default[idx];
            if ($s.CurrentFilingModule.DeletedApprovers == undefined) $s.CurrentFilingModule.DeletedApprovers = [];
            if (deleted.ID > 0) $s.CurrentFilingModule.DeletedApprovers.push(deleted.ID);
            $s.CurrentFilingModule.Default.splice(idx, 1);
        }

        $s.SetSystemStatus('Loading table', 'loading');
        $s.Init();
    }])
    .controller('dlgFilingModules', ['$scope', '$uibModalInstance', '$controller', 'dData', function ($s, $mi, $c, $dlgData) {
        $c('BaseController', { $scope: $s });
        $s.FilingModules = [];
        $s.Request('LoadFilingModules', { MenuCode: 'AdministrativeApproverMatrix' }, 'ApproverMatrix').then(function (ret) {
            if (ret.Type == 2) {
                $s.SetSystemStatus(ret.Message, 'error');
                $s.$apply();
            } else {
                $s.FilingModules = Enumerable.From(ret.Data).Select(x => {
                    return $s.IsExist(x)
                }).ToArray();
            }
        });

        $s.countSelected = function () {
            var a = Enumerable.From($s.FilingModules).Count(x => x.IsChecked);
            return a;
        }

        $s.IsExist = function (mod) {
            var existingData = Enumerable.From($dlgData.Data).Where(x => x.ID_FilingModules == mod.ID).FirstOrDefault();
            var nData = {};
            if (existingData == undefined) {
                nData = { IsChecked: false, ID: 0, ID_FilingModules: mod.ID, ID_Approver: $dlgData.ID, IsActive: true, Name: mod.Name, Default: [] };
            } else {
                nData = { IsChecked: true, ID: existingData.ID || 0, ID_FilingModules: existingData.ID_FilingModules || mod.ID, ID_Approver: existingData.ID_Approver || $dlgData.ID, IsActive: true, Name: mod.Name, Default: existingData.Default || [] };
            }
            return nData;
        }

        $s.load = function () {
            var selected = Enumerable.From($s.FilingModules).Where(x => x.IsChecked == true).ToArray();
            $mi.close(selected);
        }

        $s.close = function () {
            $mi.close();
        }
    }])
    .controller('dlgApproverEmployeeList', ['$scope', '$uibModalInstance', '$controller', 'dData', function ($s, $mi, $c, $dlgData) {
        $c('insysTableController', { $scope: $s });
        $s.tblOptions = {
            Columns: [
                { Name: 'Name' },
                { Name: 'Designation', Label: 'Position' }
            ],
            Filters: [
                { Name: 'Name', Type: 9, Label: 'Name' },
                { Name: 'Designation', Type: 9, Label: 'Position' }
            ]
        };
        $s.LoadTable($s.tblOptions, 'LoadEmployeeList', 'ApproverMatrix', { ID_Company: $dlgData.Data || $s.Session('ID_Company'), MenuCode: 'AdministrativeApproverMatrix' }).then(function (ret) {
            if (ret.Type == 2) {
                $s.SetSystemStatus(ret.Message,'error');
            } else {
                $s.SetSystemStatus('Ready');
            }
            $s.$apply();
        });

        $s.openForm = function (emp) {
            $mi.close(emp)
        }

        $s.close = function () {
            $mi.close();
        }
    }])
    .controller('dlgApproverMatrixLoadEmployess', ['$scope', '$controller', '$uibModalInstance', 'dData', function ($s, $c, $mi, $dlgData, ) {
        $c('BaseController', { $scope: $s });
        $s.MCode = 'AdministrativeApproverMatrix';
        $s.orgTypeMasterList = [];
        $s.orgTypes = [];
        $s.dlgData = $dlgData; 
        $s.Init = function () {
            $s.Request('LoadOrgTypes', { ID_Company: $s.dlgData.ID_Company, MenuCode: $s.MCode }, 'ApproverMatrix').then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message,'error');
                    $s.$apply();
                } else {
                    $s.orgTypes = ret.Data;
                }
            });
        }
        $s.Org = {};
        $s.LoadMasterOption = function () {
            $s.MasterLookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Name' }
                    ]
                },
                method: 'LoadLookUp',
                controller: 'ApproverMatrix',
                parameter: { ID: $s.selectedOrgType, ID_Company: $s.dlgData.ID_Company || $s.Session('ID_Company'), Name: "Master", MenuCode: $s.MCode }
            }
        }
        $s.loadMasterlist = function (id)
        {
            $s.Request('LoadMasterList', { ID: id, MenuCode: $s.MCode },'ApproverMatrix').then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message);
                    $s.$apply();
                } else {
                    $s.orgTypeMasterList = ret.Data;
                }
            });
        }
        $s.LoadEmployees = function () {
            var MasterID = $s.selectedOrgType == undefined ? undefined : $s.Org.ID_Master;
            $s.Request('LoadEmployees', { Filters: { ID: $s.dlgData.ID, ID_Company: $s.dlgData.ID_Company || $s.Session('ID_Company'), Master: MasterID, }, Data: $s.dlgData.Data, MenuCode: $s.MCode }, 'ApproverMatrix').then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message);
                    $s.$apply();
                } else {
                    var data = [];
                    data = ret.Data;
                    $mi.close(data)
                }
            });
        }
        $s.Close = function () {
            $mi.close();
        }
        $s.Init();
    }]);