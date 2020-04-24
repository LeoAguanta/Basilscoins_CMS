angular.module('app')
    .controller('PeopleEmployeeMovement', ['$scope', '$controller', '$priv', '$state', function ($s, $c, $priv, $st) {
        $c('insysTableController', { $scope: $s });
        $s.MenuPrivileges = $priv.Data;
        $s.Schema = {};
        $s.RecordID = $st.params.ID == undefined ? '' : $st.params.ID;
        $s.TableSchema = [];
        $s.MenuCode = 'PeopleEmployeeMovement';
        $s.ModuleReady = false;
        //Initialize container for data
        $s.tblOptions = {
            Columns: [
                { Name: 'ID', Label: '#' },
                { Name: 'Code' },
                { Name: 'Description' },
                { Name: 'EffectivityDate', Label: 'Effectivity Date' },
                { Name: 'DateTimeCreated', Label: 'Date/time Created' },
                { Name: 'UserCreatedBy', Label: 'Created By' },
                { Name: 'IsPosted', Label: 'Posted' },
            ],
            HasNew: $s.MenuPrivileges.HasNew,
            HasDelete: $s.MenuPrivileges.HasDelete,
            HasEdit: $s.MenuPrivileges.HasEdit,

            Filters: [
                { Name: 'ID', Type: 1, Label: '#', ControlType: 'int' },
                { Name: 'Code', Type: 9, Label: 'Code' },
                { Name: 'EffectivityDate', Type: 12, Label: 'Effectivity Date', ControlType: 'date', Value: [null, null] },
            ]
        };

        $s.Init = function () {
            if ($s.RecordID != '') {
                $s.SetSystemStatus('Loading record #' + $s.RecordID, 'loading');
                $s.LoadForm().then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                    } else {
                        $s.Schema = $s.PlotDefault(ret.Data.Form, ret.Data.Schema, $s.RecordID);
                        $s.TableSchema = ret.Data.Schema;
                        $s.SetSystemStatus('Ready');
                    }
                    $s.ModuleReady = true;
                    $s.$apply();
                })
            } else {
                $s.LoadTable($s.tblOptions, 'LoadList', 'EmployeeMovement', { MenuCode: $s.MenuCode }).then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                    } else {
                        $s.SetSystemStatus('Ready');
                    }
                    $s.$apply();
                });
            }

        }

        $s.LoadForm = function () {
            return $s.Request('LoadForm', { ID: $s.RecordID, MenuCode: $s.MenuCode }, 'EmployeeMovement');
        }
        $s.saveForm = function () {
            if (!$s.IsTabsValid('form.employeemovement')) return;
            if (!$s.IsTabsValid('form.employeemovement', {}, 'detail')) return;

            if (!$s.tblOptions.HasEdit && $s.RecordID > 0) {
                $s.Prompt('You are not allowed to update this record.');
                return;
            }
            $s.SetSystemStatus('Saving record #' + $s.RecordID, 'loading');
            $s.Request('SaveForm', { Data: $s.Schema, MenuCode: $s.MenuCode, Detail: $s.SelectedEmployee }, 'EmployeeMovement').then(function (ret) {
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
        $s.delete = function () {
            $s.deleteRow('DeleteRecord', 'EmployeeMovement', { MenuCode: $s.MenuCode })
        }

        $s.SetSystemStatus('Loading table', 'loading');
        $s.Init();

        $s.loadDetail = function () {
            if ($s.SelectedEmployee.length == 0)
                $s.Request('LoadDetail', { MenuCode: $s.MenuCode, ID_Movement: $s.RecordID }, 'EmployeeMovement').then(function (ret) {
                    if (ret.Type == 2) $s.SetSystemStatus(ret.Message, 'error');
                    else {
                        $s.SelectedEmployee = ret.Data;
                        if ($s.LeaveParameterLookup.length == 0) {
                            $s.SetSystemStatus('Loading leave parameters', 'loading');
                            $s.LoadLeaveParameter();
                        }
                        if ($s.EmployeeStatusLookup.length == 0) {
                            $s.SetSystemStatus('Loading employee status', 'loading');
                            $s.LoadEmployeeStatus();
                        }
                        if ($s.MovementSource.length == 0) {
                            $s.SetSystemStatus('Loading movement type', 'loading');
                            $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'movementtype' }, 'EmployeeMovement').then(function (ret) {
                                if (ret.Type == 2) {
                                    $s.SetSystemStatus(ret.Message, 'error');
                                } else {
                                    $s.SetSystemStatus('Ready');
                                    $s.MovementSource = ret.Data.Rows;
                                    $s.MovementSource.unshift({ ID: null, Name: '-Select one-' });
                                }
                            });
                        }
                    }
                    $s.$apply();
                });
        }
        $s.timeout = null;
        $s.EmployeeFound = [];
        $s.SelectedEmployee = [];
        $s.EmployeeMovement = [];
        $s.LeaveParameterLookup = [];
        $s.EmployeeStatusLookup = [];
        $s.MovementModel = { SearchKeyword: null };
        $s.CurrentEmployee = null;
        $s.foundIdx = 0;
        $s.SearchEmployee = function (ev) {
            if (ev.keyCode == 13) $s.AddMovementEmployee($s.EmployeeFound[$s.foundIdx]);
            if (ev.keyCode == 38) {
                if ($s.foundIdx > 0) $s.foundIdx -= 1;
                return;
            }
            if (ev.keyCode == 40) {
                if ($s.foundIdx < ($s.EmployeeFound.length - 1)) $s.foundIdx += 1;
                return;
            }
            if (ev.keyCode != 13 && ev.keyCode != 17 && ev.keyCode != 18 && ev.keyCode != 91) {
                if ($s.timeout != null) clearTimeout($s.timeout);
                $s.timeout = setTimeout(function () {
                    if ($s.MovementModel.SearchKeyword == "" || $s.MovementModel.SearchKeyword == null || $s.MovementModel.SearchKeyword.length <= 2) {
                        $s.EmployeeFound = [];
                        $s.foundIdx = 0;
                        $s.$apply();
                        return;
                    }
                    if (($s.MovementModel.SearchKeyword != "" && $s.MovementModel.SearchKeyword != null) && $s.MovementModel.SearchKeyword.length > 2)
                        $s.ShowLoading = true;
                        $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'employeesuggestion', Employee: $s.MovementModel.SearchKeyword }, 'EmployeeMovement', true).then(function (ret) {
                            if (ret.Type == 2) {
                                $s.SetSystemStatus(ret.Message, 'error');
                            } else {
                                Enumerable.From(ret.Data).ForEach(function (emp, idx) {
                                    var startDiv = $('<div />'),
                                        coloredDiv = $('<div />'),
                                        endDiv = $('<div />'),
                                        defaultDiv = $('<div class="res" />'),
                                        startIndex = emp.Name.toLowerCase().indexOf($s.MovementModel.SearchKeyword.toLowerCase()),
                                        endIndex = startIndex + $s.MovementModel.SearchKeyword.length;

                                    coloredDiv.addClass('highlight');

                                    startDiv.append(emp.Name.substr(0, startIndex).replace(new RegExp(' ', 'g'), '&nbsp;'));
                                    coloredDiv.append(emp.Name.substr(startIndex, $s.MovementModel.SearchKeyword.length).replace(new RegExp(' ', 'g'), '&nbsp;'));
                                    endDiv.append(emp.Name.substr(endIndex, emp.Name.length - endIndex).replace(new RegExp(' ', 'g'), '&nbsp;'));
                                    defaultDiv.append((startDiv.html() != '' ? startDiv : ''), coloredDiv, (endDiv.html() != '' ? endDiv : ''));
                                    emp.Template = defaultDiv.html();
                                })
                                $s.EmployeeFound = ret.Data;
                            }
                            if ($s.foundIdx > $s.EmployeeFound.length) $s.foundIdx = 0;
                            $s.ShowLoading = false;
                            $s.$apply();
                        });
                }, 200);
            }
        }
        $s.preventSubmit = function (ev) {
            if (ev.keyCode == 13 || ev.keyCode == 38 || ev.keyCode == 40) ev.preventDefault();
        }
        $s.AddMovementEmployee = function (emp) {
            var empCount = Enumerable.From($s.SelectedEmployee).Where(function (x) { return x.ID_Employee == emp.ID }).ToArray().length;
            var empObj = { ID: 0, ID_Movement: $s.RecordID, ID_Employee: emp.ID, Name: emp.Name, MovementType: [] };
            if (empCount == 0) $s.SelectedEmployee.push(empObj);
        }
        $s.clearFound = function () {
            $s.EmployeeFound = [];
            $s.MovementModel.SearchKeyword = null;
        }
        $s.MovementSource = [];
        $s.loadEmployeeMovement = function (emp) {
            $s.CurrentEmployee = emp;
            $s.LoadLookupOptions();
            if ($s.CurrentEmployee.MovementType != undefined) {
                if ($s.CurrentEmployee.MovementType.length == 0) {
                    $s.SetSystemStatus('Loading employee movement', 'loading');
                    $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'employeemovementdetail', ID: emp.ID }, 'EmployeeMovement').then(function (ret2) {
                        if (ret2.Type == 2) {
                            $s.SetSystemStatus(ret2.Message, 'error');
                        } else {
                            $s.SetSystemStatus('Ready');
                            $s.CurrentEmployee.MovementType = ret2.Data;
                        }
                        $s.$apply();
                    });
                }
            } else {
                $s.SetSystemStatus('Loading employee movement', 'loading');
                $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'employeemovementdetail', ID: emp.ID }, 'EmployeeMovement').then(function (ret2) {
                    if (ret2.Type == 2) {
                        $s.SetSystemStatus(ret2.Message, 'error');
                    } else {
                        $s.SetSystemStatus('Ready');
                        $s.CurrentEmployee.MovementType = ret2.Data;
                    }
                    $s.$apply();
                });
            }
        }
        $s.AddMovementType = function () {
            var empObj = { ID: 0, ID_Movement_Employee: $s.CurrentEmployee.ID, ID_MovementType: null, OldValue: null, NewValue: null };
            if ($s.CurrentEmployee.MovementType == undefined) $s.CurrentEmployee.MovementType = [];
            $s.CurrentEmployee.MovementType.push(empObj);
        }
        $s.RemoveMovement = function (mm) {
            var rowIdx = Enumerable.From($s.CurrentEmployee.MovementType).Select(function (x) { return JSON.stringify(x) }).IndexOf(JSON.stringify(mm));
            if (mm.ID > 0) {
                $s.Confirm('Are you sure you want to delete this record?', 'Delete Record').then(function (r) {
                    $s.Request('DeleteDetail', { MenuCode: $s.MenuCode, DetailName: 'movementtype', ID: mm.ID }, 'EmployeeMovement').then(function (ret) {
                        if (ret.Type == 2) {
                            $s.SetSystemStatus(ret.Message, 'error');
                            $s.$apply();
                        } else {
                            $s.Prompt(ret.Message, 'Delete Record');
                            if (rowIdx > -1) $s.CurrentEmployee.MovementType.splice(rowIdx, 1);
                        }
                    });
                });
            } else $s.CurrentEmployee.MovementType.splice(rowIdx, 1);
        }
        $s.MovementExist = function (obj) {
            return Enumerable.From($s.MovementSource).Where(function (x) {
                return Enumerable.From($s.CurrentEmployee.MovementType).Where(function (y) { return JSON.stringify(y) != JSON.stringify(obj) }).Select(function (y) { return y.ID_MovementType }).IndexOf(x.ID) < 0 || x.ID == null
            }).ToArray();
        }
        $s.getSource = function (obj) {
            if (obj.ID_MovementType == 4) {
                $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'oldbranch', ID_Employee: $s.CurrentEmployee.ID_Employee }, 'EmployeeMovement').then(function (ret) {
                    if (ret.Type == 2) $s.SetSystemStatus(ret.Message, 'error');
                    else {
                        obj.OldValue = ret.Data.ID;
                        obj.OldBranch = ret.Data.Name;
                    }
                    $s.$apply();
                })
            } else if (obj.ID_MovementType == 6) {
                $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'olddepartment', ID_Employee: $s.CurrentEmployee.ID_Employee }, 'EmployeeMovement').then(function (ret) {
                    if (ret.Type == 2) $s.SetSystemStatus(ret.Message, 'error');
                    else {
                        obj.OldValue = ret.Data.ID;
                        obj.OldDepartment = ret.Data.Name;
                    }
                    $s.$apply();
                });
            } else if (obj.ID_MovementType == 7) {
                $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'olddesignation', ID_Employee: $s.CurrentEmployee.ID_Employee }, 'EmployeeMovement').then(function (ret) {
                    if (ret.Type == 2) $s.SetSystemStatus(ret.Message, 'error');
                    else {
                        obj.OldValue = ret.Data.ID;
                        obj.OldDesignation = ret.Data.Name;
                    }
                    $s.$apply();
                });
            } else if (obj.ID_MovementType == 8) {
                $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'oldemployeestatus', ID_Employee: $s.CurrentEmployee.ID_Employee }, 'EmployeeMovement').then(function (ret) {
                    if (ret.Type == 2) $s.SetSystemStatus(ret.Message, 'error');
                    else {
                        obj.OldValue = ret.Data.OldEmployeeStatus == null ? null : ret.Data.OldEmployeeStatus.ID;
                        obj.OldEmployeeStatus = ret.Data.OldEmployeeStatus == null ? null : ret.Data.OldEmployeeStatus.Name;
                    }
                    $s.$apply();
                });
            }
            else if (obj.ID_MovementType == 13) {
                $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'oldleaveparameter', ID_Employee: $s.CurrentEmployee.ID_Employee }, 'EmployeeMovement').then(function (ret) {
                    if (ret.Type == 2) $s.SetSystemStatus(ret.Message, 'error');
                    else {
                        obj.OldValue = ret.Data.OldLeaveParameter == null ? null : ret.Data.OldLeaveParameter.ID;
                        obj.OldLeaveParameter = ret.Data.OldLeaveParameter == null ? null : ret.Data.OldLeaveParameter.Name;
                    }
                    $s.$apply();
                });
            }
        }
        $s.RemoveEmployee = function (mm) {
            var rowIdx = Enumerable.From($s.SelectedEmployee).Select(function (x) { return JSON.stringify(x) }).IndexOf(JSON.stringify(mm));
            if (mm.ID > 0) {
                $s.Confirm('Are you sure you want to delete ' + (mm.Employee || mm.Name) + '\'s record?', 'Delete Record').then(function () {
                    $s.Request('DeleteDetail', { MenuCode: $s.MenuCode, DetailName: 'movementemployee', ID: mm.ID }, 'EmployeeMovement').then(function (ret) {
                        if (ret.Type == 2) {
                            $s.SetSystemStatus(ret.Message, 'error');
                            $s.$apply();
                        } else {
                            $s.Prompt(ret.Message, 'Delete Record');
                            if (rowIdx > -1) {
                                $s.SelectedEmployee.splice(rowIdx, 1);
                                $s.CurrentEmployee = null;
                            }
                        }
                    });
                });
            } else $s.SelectedEmployee.splice(rowIdx, 1);
        }
        $s.LoadEmployeeStatus = function () {
            $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'employeestatus' }, 'EmployeeMovement').then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message, 'error');
                } else {
                    $s.SetSystemStatus('Ready');
                    $s.EmployeeStatusLookup = ret.Data.Rows;
                    $s.EmployeeStatusLookup.unshift({ ID: null, Name: '-Select one-' });
                }
                $s.$apply();
            });
        }
        $s.LoadLeaveParameter = function () {
            $s.Request('LoadLookup', { MenuCode: $s.MenuCode, LookupName: 'leaveparameter' }, 'EmployeeMovement').then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message, 'error');
                } else {
                    $s.SetSystemStatus('Ready');
                    $s.LeaveParameterLookup = ret.Data.Rows;
                    $s.LeaveParameterLookup.unshift({ ID: null, Name: '-Select one-' });
                }
                $s.$apply();
            });
        }
        $s.LoadLookupOptions = function () {
            $s.branchTransferOptions = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Branch' }
                    ]
                },
                method: 'LoadLookup',
                controller: 'EmployeeMovement',
                parameter: { MenuCode: $s.MenuCode, LookupName: 'branch', ID_Employee: $s.CurrentEmployee.ID_Employee }
            }
            $s.departmentTransferOptions = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Department' }
                    ]
                },
                method: 'LoadLookup',
                controller: 'EmployeeMovement',
                parameter: { MenuCode: $s.MenuCode, LookupName: 'department', ID_Employee: $s.CurrentEmployee.ID_Employee }
            }
            $s.positionTransferOptions = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Designation/Position' }
                    ]
                },
                method: 'LoadLookup',
                controller: 'EmployeeMovement',
                parameter: { MenuCode: $s.MenuCode, LookupName: 'designation', ID_Employee: $s.CurrentEmployee.ID_Employee }
            }
        }
        $s.PostMovement = function () {
            $s.Confirm('Once posted application cannot be modified. Are you sure you want to proceed?', 'Post Movement').then(function () {
                $s.Request('Post', { MenuCode: $s.MenuCode, ID: $s.RecordID }, 'EmployeeMovement').then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                    } else {
                        $s.Prompt(ret.Message);
                        $st.reload();
                    }
                    $s.$apply();
                });
            });
        }
    }]);