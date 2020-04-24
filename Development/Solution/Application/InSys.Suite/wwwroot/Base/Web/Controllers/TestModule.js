angular.module('app')
    .controller('TestModule', ['$scope', '$controller', '$priv', '$state', function ($s, $c, $priv, $st) {
        $c('insysTableController', { $scope: $s });

        $s.MenuPrivileges = $priv.Data;
        $s.RecordID = $st.params.ID == undefined ? '' : $st.params.ID;
        $s.Schema = {};
        $s.TableSchema = [];
        $s.MenuCode = 'TestModule';
      
        
        
        $s.tblOptions = {
            Columns: [
                { Name: 'ID', Label: '#' },
                { Name: 'Name', Label: 'Name' },
            ],
            HasNew: $s.MenuPrivileges.HasNew,
            HasDelete: $s.MenuPrivileges.HasDelete,
            HasEdit: $s.MenuPrivileges.HasEdit,
            Filters: [
                {Name: 'Name', Type: 9, Label:'Name' }
            ]
        };

        
        $s.Init = function () {

            if ($s.RecordID != '') {
                $s.SetSystemStatus('Loading record #' + $s.RecordID, 'loading');

                $s.LoadForm().then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                    } else {
                        console.log('Return',ret);
                        $s.Schema = $s.PlotDefault(ret.Data.Form, ret.Data.Schema, $s.RecordID);
                        $s.TableSchema = ret.Data.Schema;
                        $s.SetSystemStatus('Ready');
                    }
                    $s.$apply();
                });


            } else {
                $s.LoadTable($s.tblOptions, 'LoadList', 'TestModule', {MenuCode: $s.MenuCode}).then(function (ret) {
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
            return $s.Request('LoadForm', { ID: $s.RecordID, MenuCode: $s.MenuCode },'TestModule')
        }

        $s.delete = function () {
            $s.deleteRow('DeleteRecord', 'TestModule', { MenuCode: $s.MenuCode })
        }

        $s.genderLookup = {
            tblOptions: {
                Columns: [
                    { Name: 'ID', Label: '#' },
                    { Name: 'Name', Label: 'Gender' }
                ]
            },
            method: 'LoadLookup',
            controller: 'TestModule',
            parameter: { LookupName: 'gender', MenuCode: $s.MenuCode }
        };

        $s.saveForm = function () {
            if (!$s.IsTabsValid('testmodule', $s.TableSchema, 'general')) return;
            if (!$s.IsTabsValid('testmodule', $s.dependentsOptions.tblOptions.TableSchema, 'dependents')) return;

            if (!$s.tblOptions.HasEdit && $s.RecordID > 0) {
                $s.Prompt('You are not allowed to update this record.');
                return;
            }
            $s.SetSystemStatus('Saving record #' + $s.RecordID, 'loading');
            console.log('depend', $s.dependentsOptions);
            $s.Request('SaveForm', { Data: $s.Schema, MenuCode: $s.MenuCode, Dependents: $s.dependentsOptions.tblOptions.Data.Rows }, 'TestModule').then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message, 'error');
                    $s.$apply();
                } else {
                    $s.Prompt('Save successful.');
                    $st.go($st.current.name, { ID: ret.Data }, { reload: true });
                }
            });
        }
        
        
        $s.dependentsOptions = {
            trigger: function () { },
            tblOptions: {
                Data: { Rows: [], Total: 0 },
                Columns: [
                    { Name: 'ID', Label: '#' },
                    { Name: 'DependentName', Label: 'Dependent', ControlType: 'text' },
                ],
                HasNew: true,
                HasEdit: true,
                HasDelete: true,
                Filters: [
                    { Name: 'DependentName', Type: 9, Label: 'Dependent' },
                ],
                deleteRow: function (tblOptions) {
                    $s.deleteDetailRow('DeleteDetail', 'TestModule', tblOptions, { DetailName: 'dependents', MenuCode: $s.MenuCode });
                },
                isEditable: true
            },
            Method: 'LoadDetail',
            Controller: 'TestModule',
            Parameter: { DetailName: 'dependents', ID_MyModule: $s.RecordID, MenuCode: $s.MenuCode }
        };

        $s.loadDependents = function () {
            if ($s.dependentsOptions.tblOptions.Data != undefined)
                if ($s.dependentsOptions.tblOptions.Data.Rows != undefined)
                    if ($s.dependentsOptions.tblOptions.Data.Rows.length > 0)
                        return;
            $s.Request('LoadSchema', { Table: 'tTestModule_Dependent' }, 'Menu').then(function (ret2) {
                if (ret2.Type == 2) {
                    $s.SetSystemStatus(ret2.Message, 'error');
                } else {
                    $s.dependentsOptions.tblOptions.TableSchema = ret2.Data;
                    $s.dependentsOptions.trigger().then(function (tblOptions) {
                        $s.dependentsOptions.tblOptions = tblOptions;
                        $s.dependentsOptions.tblOptions.newForm = function () {
                            var defaultValue = $s.PlotDefault({}, ret2.Data, 0);
                            defaultValue.ID_TestModule = $s.RecordID;
                            $s.dependentsOptions.tblOptions.Data.Rows.unshift(defaultValue);
                        }
                    });
                }
                $s.$apply();
            });

        }


        $s.Init();
    }]);