angular.module('app')
    .controller('PeopleEmployeeRecord', ['$scope', '$controller', '$priv', '$state', function ($s, $c, $priv, $st) {
        $c('insysTableController', { $scope: $s });
        $s.MenuPrivileges = $priv.Data;
        $s.Schema = {};
        $s.Persona = {};
        $s.TableSchema = [];
        $s.PersonaAddress = {};
        $s.PersonaAddressSchema = [];
        $s.RecordID = $st.params.ID == undefined ? '' : $st.params.ID;
        $s.isEditable = false;
        $s.showPersonalInfo = true;
        $s.activeView = 1;

        $s.NationalityList = [];
        $s.CitizenshipList = [];
        $s.CivilStatusList = [];
        $s.GenderList = [];
        $s.ReligionList = [];
        $s.BloodTypeList = [];
        $s.BarangayList = [];
        $s.CityList = [];
        $s.ProvinceList = [];
        $s.EducationalAttainment = [];
        $s.EducationalSchema = [];
        $s.EmploymentSchema = [];
        $s.EmployeeRecord = null;

        $s.MenuCode = 'PeopleEmployeeRecord';
        $s.myController = 'EmployeeRecord';

        //Initialize container for data
        $s.tblOptions = {
            Columns: [
                //{ Name: 'ID', Label: '#' },
                { Name: 'Code', Label: 'Code' },
                { Name: 'LastName', Label: 'Last Name' },
                { Name: 'FirstName', Label: 'First Name' },
                { Name: 'MiddleName', Label: 'Middle Name' },
                { Name: 'EmployeeStatus', Label: 'Employee Status' },
                { Name: 'PayrollScheme', Label: 'Payroll Scheme' },
                { Name: 'PayrollFrequency', Label: 'Payroll Frequency' },
                { Name: 'Parameter', Label: 'Parameter' },
                { Name: 'LeaveParameter', Label: 'Leave Parameter' }
                //{ Name: 'Branch' },
                //{ Name: 'Department' },
                //{ Name: 'Designation' },
            ],
            HasNew: $s.MenuPrivileges.HasNew,
            HasDelete: false,
            HasEdit: $s.MenuPrivileges.HasEdit,
            Filters: [
                { Name: 'Code', Type: 9, Label: 'Code' },
                { Name: 'LastName', Type: 9, Label: 'Last Name' },
                { Name: 'FirstName', Type: 9, Label: 'First Name' },
                { Name: 'MiddleName', Type: 9, Label: 'Middle Name' },
                { Name: 'EmployeeStatus', Type: 9,  Label: 'Employee Status' },
                { Name: 'PayrollScheme', Type: 9,  Label: 'Payroll Scheme' },
                { Name: 'PayrollFrequency', Type: 9,  Label: 'Payroll Frequency' },
                { Name: 'Parameter', Type: 9,  Label: 'Parameter' },
                { Name: 'LeaveParameter', Type: 9,  Label: 'Leave Parameter' }
            ]
        };

        $s.GetAddress = function (IsPresent) {
            return Enumerable.From($s.PersonaAddress).Where(function sa(x) { return x.IsPresentAddress == IsPresent }).FirstOrDefault();
        }

        $s.OpenPersonaWizard = function () {
            $s.Dialog({
                template: 'PersonaSelect',
                controller: 'dlgPersonaSelect',
                size: 'sm',
                windowClass: 'persona-select-dlg', //Please create new windowClass
            }).result.then(function (ret) {

                if (ret != undefined) {
                    if (ret == 0) {
                        $s.Dialog({
                            template: 'PersonaWizard',
                            controller: 'dlgPersonaWizard',
                            size: 'lg',
                            windowClass: 'employee-wizard-dlg er-wizard-dlg',
                            data: {}
                        }).result.then(function (ret) {
                            console.log("return", ret);
                            if (ret != undefined) {
                                $s.Persona = ret.Persona;
                                $s.CharacterReference = ret.CharacterReference;
                                $s.SchemaEducationalBackGround = ret.EducationalBackGround;
                                $s.LicensesAndCertificates = ret.LicensesAndCertificates;
                                $s.PreviousEmployment = ret.PreviousEmployment;
                                $s.Schema.FirstName = $s.Persona.Firstname;
                                $s.Schema.LastName = $s.Persona.Lastname;
                                $st.go($st.current.name + '.Form', { ID: 0 }, { reload: false });
                            } else {
                                $st.go($s.MenuCode, {}, { reload: true });
                            }

                        });
                    } else if (ret == -1) { //Since this is close, then redirect to listing
                        //redirect to list.
                        $st.go($s.MenuCode, {}, { reload: true });
                    } else {
                        $s.Schema.ID_Persona = ret.ID;
                        $s.Schema.FirstName = ret.Firstname;
                        $s.Schema.LastName = ret.Lastname;
                    }
                }
            });
        }

        $s.Init = function () {
            if ($s.RecordID != '') {
                $s.GetLookUpList();
                $s.SetSystemStatus('Loading record #' + $s.RecordID, 'loading');
                $s.LoadForm().then(function (ret) {
                    console.log('return2',ret);
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                    } else {
                        $s.Schema = $s.PlotDefault(ret.Data.Persona, ret.Data.Schema, $s.RecordID);
                        $s.EmployeeRecord = ret.Data.Form;
                        $s.Schema.FirstName = $s.Schema.FirstName == null ? '' : $s.Schema.FirstName;
                        $s.Schema.LastName = $s.Schema.LastName == null ? '' : $s.Schema.LastName;
                        $s.PersonaAddress = ret.Data.PersonaAddress || [];


                        $s.TableSchema = ret.Data.Schema;
                        $s.PersonaAddressSchema = ret.Data.PersonaAddressSchema;
                        $s.EducationalSchema = ret.Data.EducationalSchema;
                        $s.EmploymentSchema = ret.Data.EmploymentSchema;

                        

                        $s.SetSystemStatus('Ready');
                        if ($s.PersonaAddress.length == 0) {
                            $s.PersonaAddress.push($s.PlotDefault({}, $s.PersonaAddressSchema, 0));
                            $s.PersonaAddress[0].IsPresentAddress = true;
                            $s.PersonaAddress.push($s.PlotDefault({}, $s.PersonaAddressSchema, 0));
                        } else if ($s.PersonaAddress.length == 1) {
                            if ($s.PersonaAddress[0].IsPresentAddress == false) {
                                $s.PersonaAddress.push($s.PlotDefault({}, $s.PersonaAddressSchema, 0));
                                $s.PersonaAddress[1].IsPresentAddress = true;
                            } else {
                                $s.PersonaAddress.push($s.PlotDefault({}, $s.PersonaAddressSchema, 0));
                            }
                        }
                        $s.LoadLookuProperties();
                        $s.IsFormReady = true;
                    }
                    $s.$apply();
                })
                //For New Employee, User should Input Personal Details
                //if ($s.RecordID == 0) {
                //    $s.OpenPersonaWizard();   
                //}

            } else {
                $s.LoadTable($s.tblOptions, 'LoadList', $s.myController, { MenuCode: $s.MenuCode }).then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                    } else {
                        $s.SetSystemStatus('Ready');
                    }
                    $s.$apply();
                });
            }

        }
        
        $s.editRecord = function () {
            $s.isEditable = true;
            $s.GetLookUpList();
            console.log($s.TableSchema);
        }

        $s.setActiveView = function (view) {
            $s.showPersonalInfo = false;
            $s.activeView = view;
            switch (view) {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }

        $s.backView = function () {
            $s.showPersonalInfo = true;
        }

        $s.GetLookUpList = function () {
            $s.CityList = { Permanent: [], Present: [] };
            $s.BarangayList = { Permanent: [], Present: [] };

            $s.Nationalitylookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Nationality' }
                    ]
                },
                method: 'LoadLookup',
                controller: $s.myController,
                parameter: { LookupName: 'Nationality', MenuCode: $s.MenuCode }
            }

            $s.CitizenshipLookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Citizenship' }
                    ]
                },
                method: 'LoadLookup',
                controller: $s.myController,
                parameter: { LookupName: 'Citizenship', MenuCode: $s.MenuCode }
            }

            $s.CivilStatusLookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'CivilStatus' }
                    ]
                },
                method: 'LoadLookup',
                controller: $s.myController,
                parameter: { LookupName: 'CivilStatus', MenuCode: $s.MenuCode }
            }

            $s.Request('LoadLookup', { LookupName: 'Gender', MenuCode: 'PeopleEmployeeRecord' }, 'EmployeeRecord').then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message, 'error');
                } else {
                    $s.GenderList = ret.Data.Rows;
                    $s.SetSystemStatus('Ready');
                }
                $s.$apply();
            });

            $s.ReligionLookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Religion' }
                    ]
                },
                method: 'LoadLookup',
                controller: $s.myController,
                parameter: { LookupName: 'Religion', MenuCode: $s.MenuCode }
            }

            $s.BloodTypeLookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'BloodType' }
                    ]
                },
                method: 'LoadLookup',
                controller: $s.myController,
                parameter: { LookupName: 'BloodType', MenuCode: $s.MenuCode }
            }

            $s.EducationalAttainmentLookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'EducationalAttainment' }
                    ]
                },
                method: 'LoadLookup',
                controller: $s.myController,
                parameter: { LookupName: 'EducationalAttainment', MenuCode: $s.MenuCode }
            }

            //$s.Request('LoadLookup', { LookupName: 'Nationality', MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
            //    if (ret.Type == 2) {
            //        $s.SetSystemStatus(ret.Message, 'error');
            //    } else {
            //        $s.NationalityList = ret.Data.Rows;
            //        $s.SetSystemStatus('Ready');
            //    }
            //    $s.$apply();
            //});

            //$s.Request('LoadLookup', { LookupName: 'Citizenship', MenuCode: $s.MenuCode }, $s.myController).then(function (ret) {
            //    if (ret.Type == 2) {
            //        $s.SetSystemStatus(ret.Message, 'error');
            //    } else {
            //        $s.CitizenshipList = ret.Data.Rows;
            //        $s.SetSystemStatus('Ready');
            //    }
            //    $s.$apply();
            //});
            //$s.Request('LoadLookup', { LookupName: 'CivilStatus', MenuCode: 'PeopleEmployeeRecord' }, 'EmployeeRecord').then(function (ret) {
            //    if (ret.Type == 2) {
            //        $s.SetSystemStatus(ret.Message, 'error');
            //    } else {
            //        $s.CivilStatusList = ret.Data.Rows;
            //        $s.SetSystemStatus('Ready');
            //    }
            //    $s.$apply();
            //});
 
            //$s.Request('LoadLookup', { LookupName: 'Religion', MenuCode: 'PeopleEmployeeRecord' }, 'EmployeeRecord').then(function (ret) {
            //    if (ret.Type == 2) {
            //        $s.SetSystemStatus(ret.Message, 'error');
            //    } else {
            //        $s.ReligionList = ret.Data.Rows;
            //        $s.SetSystemStatus('Ready');
            //    }
            //    $s.$apply();
            //});
            //$s.Request('LoadLookup', { LookupName: 'BloodType', MenuCode: 'PeopleEmployeeRecord' }, 'EmployeeRecord').then(function (ret) {
            //    if (ret.Type == 2) {
            //        $s.SetSystemStatus(ret.Message, 'error');
            //    } else {
            //        $s.BloodTypeList = ret.Data.Rows;
            //        $s.SetSystemStatus('Ready');
            //    }
            //    $s.$apply();
            //});
            //$s.Request('LoadLookup', { LookupName: 'EducationalAttainment', MenuCode: 'PeopleEmployeeRecord' }, 'EmployeeRecord').then(function (ret) {
            //    if (ret.Type == 2) {
            //        $s.SetSystemStatus(ret.Message, 'error');
            //    } else {
            //        $s.EducationalAttainment = ret.Data.Rows;
            //        $s.SetSystemStatus('Ready');
            //    }
            //    $s.$apply();
            //});
        }
        $s.UpdateCity = function (data, IsPresent) {
            if (IsPresent) $s.cityLookup2.parameter.ID_Province = data.ID;
            else $s.cityLookup.parameter.ID_Province = data.ID;
            
        }
        $s.UpdateBarangay = function (data, IsPresent) {
            if (IsPresent) $s.barangayLookup2.parameter.ID_City = data.ID;
            else $s.barangayLookup.parameter.ID_City = data.ID;
        }
        $s.LoadForm = function () {
            return $s.Request('LoadForm', { ID: $s.RecordID, MenuCode: 'PeopleEmployeeRecord' }, 'EmployeeRecord');
        }

        $s.saveForm = function () {
            
            $s.SetSystemStatus($s.regex($s.Schema.MiddleName), 'error');
            return;
            if (!$s.IsTabsValid('form.employeerecord', $s.TableSchema, 'personalinfo')) { return; }
            if (!$s.IsTabsValid('form.employeerecord', {}, 'educational')) { return; }
            if (!$s.IsTabsValid('form.employeerecord', {}, 'employment')) { return; }

            if (!$s.tblOptions.HasEdit && $s.RecordID > 0) {
                $s.Prompt('You are not allowed to update this record.');
                return;
            }
            $s.SetSystemStatus('Saving record #' + $s.RecordID, 'loading');
            $s.Request('SaveForm', { ID: $s.RecordID, Data: $s.Schema, PersonaAddress: $s.PersonaAddress, Educational: $s.EducationalRecord, Employment: $s.EmploymentHistory, MenuCode: 'PeopleEmployeeRecord' }, 'EmployeeRecord').then(function (ret) {
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
            //$s.deleteRow('DeleteRecord', 'EmployeeRecord', { MenuCode: 'PeopleEmployeeRecord' });
        }

        $s.SetSystemStatus('Loading table', 'loading');
        $s.Init();

        $s.EducationalRecord = [];
        $s.EmploymentHistory = [];
        $s.loadEducationalRecord = function () {
            if ($s.EducationalRecord.length == 0) {
                $s.SetSystemStatus('Loading educational record.', 'loading');
                $s.Request('LoadLookup', { MenuCode: 'PeopleEmployeeRecord', ID: $s.Schema.ID, LookupName: 'educationalrecord' }, 'EmployeeRecord').then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                        $s.$apply();
                    } else {
                        $s.EducationalRecord = ret.Data;
                        $s.SetSystemStatus('Ready');
                    }
                })
            }
        }
        $s.loadEmploymentHistory = function () {
            if ($s.EmploymentHistory.length == 0) {
                $s.SetSystemStatus('Loading employment history.', 'loading');
                $s.Request('LoadLookup', { MenuCode: 'PeopleEmployeeRecord', ID: $s.Schema.ID, LookupName: 'employmenthistory' }, 'EmployeeRecord').then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                        $s.$apply();
                    } else {
                        $s.EmploymentHistory = ret.Data;
                        $s.SetSystemStatus('Ready');
                    }
                })
            }
        }
        $s.IsCheckAllEducationalRecord = false;
        $s.IsCheckAllEmploymentHistory = false;
        $s.checkall = function (data, checkAllModel) {
            checkAllModel = !checkAllModel;
            Enumerable.From(data).ForEach(function (d) {
                d.IsCheck = checkAllModel;
            });
        }
        $s.Today = new Date();

        $s.addEducational = function () {
            $s.EducationalRecord.push($s.PlotDefault({}, $s.EducationalSchema, 0));
        }
        $s.addEmployment = function () {
            $s.EmploymentHistory.push($s.PlotDefault({}, $s.EmploymentSchema, 0));
        }

        $s.LoadLookuProperties = function () {
            $s.provinceLookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Province' }
                    ]
                },
                method: 'LoadLookup',
                controller: 'EmployeeRecord',
                parameter: { LookupName: 'Province', MenuCode: 'PeopleEmployeeRecord' },
                Filters: [{ Name: 'Province', Type: 9 }]
            };
            $s.cityLookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'City' }
                    ]
                },
                method: 'LoadLookup',
                controller: 'EmployeeRecord',
                parameter: { LookupName: 'City', ID_Province: $s.GetAddress(false).ID_Province, MenuCode: 'PeopleEmployeeRecord' },
                Filters: [{ Name: 'City', Type: 9 }]
            };
            $s.barangayLookup = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Barangay' }
                    ]
                },
                method: 'LoadLookup',
                controller: 'EmployeeRecord',
                parameter: { LookupName: 'Barangay', ID_City: $s.GetAddress(false).ID_City, MenuCode: 'PeopleEmployeeRecord' },
                Filters: [{ Name: 'Barangay', Type: 9 }]
            };

            $s.provinceLookup2 = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Province' }
                    ]
                },
                method: 'LoadLookup',
                controller: 'EmployeeRecord',
                parameter: { LookupName: 'Province', MenuCode: 'PeopleEmployeeRecord' },
                Filters: [{ Name: 'Province', Type: 9 }]
            };
            $s.cityLookup2 = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'City' }
                    ]
                },
                method: 'LoadLookup',
                controller: 'EmployeeRecord',
                parameter: { LookupName: 'City', ID_Province: $s.GetAddress(true).ID_Province, MenuCode: 'PeopleEmployeeRecord' },
                Filters: [{ Name: 'City', Type: 9 }]
            };
            $s.barangayLookup2 = {
                tblOptions: {
                    Columns: [
                        { Name: 'ID', Label: '#' },
                        { Name: 'Name', Label: 'Barangay' }
                    ]
                },
                method: 'LoadLookup',
                controller: 'EmployeeRecord',
                parameter: { LookupName: 'Barangay', ID_City: $s.GetAddress(true).ID_City, MenuCode: 'PeopleEmployeeRecord' },
                Filters: [{ Name: 'Barangay', Type: 9 }]
            };
        }
    }]).controller('dlgPersonaWizard', ['$scope', '$uibModalInstance', '$controller', 'dData', function ($s, $mi, $c, $dlgData) {
        $c('insysTableController', { $scope: $s });

        var PersonaData = {
            Persona: ""
            , EducationalBackGround: ""
            , CharacterReference: ""
            , LicensesAndCertificates: ""
            , PreviousEmployment: ""
        }

        //$s.Schema = $dlgData.Schema;
        $s.currentEmpTab = 0;

        //LookUp
        $s.CivilStatusList = [];
        $s.BloodTypeList = [];
        $s.GenderList = [];
        $s.ReligionList = [];
        $s.CitizenshipList = [];

        //Page 3 LookUp
        $s.EducationalAttaintment = [];

        //Schema
        $s.tPersona = {};
        $s.SchemaEducationalBackGround = [];
        $s.SchemaCharacterReference = [];
        $s.SchemaLicensesCertificates = [];
        $s.SchemaPreviousEmployment = [];

        $s.TableSchemaEducationalBackGround = [];
        $s.TableSchemaCharacterReference = [];
        $s.TableSchemaLicensesCertificates = [];
        $s.TableSchemaPreviousEmployment = [];

        $s.IsPage0Clicked = 0;
        $s.IsPage1Clicked = 0;
        $s.IsPage2Clicked = 0;
        $s.IsPage3Clicked = 0;
        $s.IsPage4Clicked = 0;
        $s.IsPage5Clicked = 0;

        $s.MenuCode = 'PeopleEmployeeRecord';
        $s.myController = 'EmployeeRecord';

        $s.LoadLookUp = function () {

            if ($s.IsPage0Clicked == 0) {

                $s.Nationalitylookup = {
                    tblOptions: {
                        Columns: [
                            { Name: 'ID', Label: '#' },
                            { Name: 'Name', Label: 'Nationality' }
                        ]
                    },
                    method: 'LoadLookup',
                    controller: $s.myController,
                    parameter: { LookupName: 'Nationality', MenuCode: $s.MenuCode }
                }

                $s.CitizenshipLookup = {
                    tblOptions: {
                        Columns: [
                            { Name: 'ID', Label: '#' },
                            { Name: 'Name', Label: 'Citizenship' }
                        ]
                    },
                    method: 'LoadLookup',
                    controller: $s.myController,
                    parameter: { LookupName: 'Citizenship', MenuCode: $s.MenuCode }
                }

                $s.CivilStatusLookup = {
                    tblOptions: {
                        Columns: [
                            { Name: 'ID', Label: '#' },
                            { Name: 'Name', Label: 'CivilStatus' }
                        ]
                    },
                    method: 'LoadLookup',
                    controller: $s.myController,
                    parameter: { LookupName: 'CivilStatus', MenuCode: $s.MenuCode }
                }

                $s.Request('LoadLookup', { LookupName: 'Gender', MenuCode: 'PeopleEmployeeRecord' }, 'EmployeeRecord').then(function (ret) {
                    if (ret.Type == 2) {
                        $s.SetSystemStatus(ret.Message, 'error');
                    } else {
                        $s.GenderList = ret.Data.Rows;
                        $s.SetSystemStatus('Ready');
                    }
                    $s.$apply();
                });

                $s.ReligionLookup = {
                    tblOptions: {
                        Columns: [
                            { Name: 'ID', Label: '#' },
                            { Name: 'Name', Label: 'Religion' }
                        ]
                    },
                    method: 'LoadLookup',
                    controller: $s.myController,
                    parameter: { LookupName: 'Religion', MenuCode: $s.MenuCode }
                }

                $s.BloodTypeLookup = {
                    tblOptions: {
                        Columns: [
                            { Name: 'ID', Label: '#' },
                            { Name: 'Name', Label: 'BloodType' }
                        ]
                    },
                    method: 'LoadLookup',
                    controller: $s.myController,
                    parameter: { LookupName: 'BloodType', MenuCode: $s.MenuCode }
                }

                $s.EducationalAttainmentLookup = {
                    tblOptions: {
                        Columns: [
                            { Name: 'ID', Label: '#' },
                            { Name: 'Name', Label: 'EducationalAttainment' }
                        ]
                    },
                    method: 'LoadLookup',
                    controller: $s.myController,
                    parameter: { LookupName: 'EducationalAttainment', MenuCode: $s.MenuCode }
                }

                //$s.Request('LoadLookUp', { LookupName: 'civilstatus', MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                //    if (ret.Type == 2) {
                //        $s.SetSystemStatus(ret.Message, 'error');
                //    } else {
                //        $s.CivilStatusList = ret.Data.Rows;
                //        $s.SetSystemStatus('Ready');
                //    }
                //});

                //$s.Request('LoadLookUp', { LookupName: 'bloodtype', MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                //    if (ret.Type == 2) {
                //        $s.SetSystemStatus(ret.Message, 'error');
                //    } else {
                //        $s.BloodTypeList = ret.Data.Rows;
                //        $s.SetSystemStatus('Ready');
                //    }
                //});

                //$s.Request('LoadLookUp', { LookupName: 'gender', MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                //    if (ret.Type == 2) {
                //        $s.SetSystemStatus(ret.Message, 'error');
                //    } else {
                //        $s.GenderList = ret.Data.Rows;
                //        $s.SetSystemStatus('Ready');
                //    }
                //});


                //$s.Request('LoadLookUp', { LookupName: 'religion', MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                //    if (ret.Type == 2) {
                //        $s.SetSystemStatus(ret.Message, 'error');
                //    } else {
                //        $s.ReligionList = ret.Data.Rows;
                //        $s.SetSystemStatus('Ready');
                //    }
                //});

                //$s.Request('LoadLookUp', { LookupName: 'citizenship', MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                //    if (ret.Type == 2) {
                //        $s.SetSystemStatus(ret.Message, 'error');
                //    } else {
                //        $s.CitizenshipList = ret.Data.Rows;
                //        $s.SetSystemStatus('Ready');
                //    }
                //});

                $s.IsPage0Clicked = 1;
            }
        }

        $s.Next = function () {
            var TabId = 'ew-tc-' + $s.currentEmpTab;

            //Validate Tab First
            if ($s.currentEmpTab == 2) {
                if ($s.TableSchemaEducationalBackGround.length > 0 && (!$s.IsTabsValid('form.educationalbackground', $s.TableSchemaEducationalBackGround, TabId))) return;
            } else if ($s.currentEmpTab == 3) {
                if ($s.TableSchemaCharacterReference.length > 0 && (!$s.IsTabsValid('form.characterreference', $s.TableSchemaCharacterReference, TabId))) return;
            } else if ($s.currentEmpTab == 4) {
                if ($s.TableSchemaLicensesCertificates.length > 0 && (!$s.IsTabsValid('form.licenseandcertificates', $s.TableSchemaLicensesCertificates, TabId))) return;
            } else if ($s.currentEmpTab == 5) {
                if ($s.TableSchemaPreviousEmployment.length > 0 && (!$s.IsTabsValid('form.previousemployment', $s.TableSchemaPreviousEmployment, TabId))) return;
            }

            if ($s.currentEmpTab < 5) {
                $s.currentEmpTab = $s.currentEmpTab + 1;
                $('.ew-tab-item').removeClass('active');
                $('.ew-tab-content').removeClass('active');
                $('#ew-tc-' + $s.currentEmpTab).addClass('active');
                $('#ew-ti-' + $s.currentEmpTab).addClass('active');

                //Get Details Data.

                if ($s.IsPage2Clicked == 0) {
                    //LookUp
                    $s.Request('LoadLookUp', { LookupName: "educationalattainment", MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                        if (ret.Type == 2) {
                            $s.SetSystemStatus(ret.Message, 'error');
                        } else {
                            $s.EducationalAttaintment = ret.Data.Rows;
                            $s.SetSystemStatus('Ready');
                        }
                    });

                    $s.Request('LoadPersonaSchema', { LookupName: "tPersonaEducationalBackGround", MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                        if (ret.Type == 2) {
                            $s.SetSystemStatus(ret.Message, 'error');
                        } else {
                            $s.TableSchemaEducationalBackGround = ret.Data.Schema;
                            $s.SetSystemStatus('Ready');
                        }
                    });
                    $s.IsPage2Clicked = 1
                } else if ($s.IsPage3Clicked == 0) {

                    $s.Request('LoadPersonaSchema', { LookupName: "tPersonaCharacterReference", MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                        if (ret.Type == 2) {
                            $s.SetSystemStatus(ret.Message, 'error');
                        } else {
                            $s.TableSchemaCharacterReference = ret.Data.Schema;
                            $s.SetSystemStatus('Ready');
                        }
                    });
                    $s.IsPage3Clicked = 1
                } else if ($s.IsPage4Clicked == 0) {

                    $s.Request('LoadPersonaSchema', { LookupName: "tPersonaLicensesAndCertificates", MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                        if (ret.Type == 2) {
                            $s.SetSystemStatus(ret.Message, 'error');
                        } else {
                            $s.TableSchemaLicensesCertificates = ret.Data.Schema;
                            $s.SetSystemStatus('Ready');
                        }
                    });
                    $s.IsPage4Clicked = 1
                } else if ($s.IsPage5Clicked == 0) {

                    $s.Request('LoadPersonaSchema', { LookupName: "tPersonaEmployment", MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                        if (ret.Type == 2) {
                            $s.SetSystemStatus(ret.Message, 'error');
                        } else {
                            $s.TableSchemaPreviousEmployment = ret.Data.Schema;
                            $s.SetSystemStatus('Ready');
                        }
                    });
                    $s.IsPage5Clicked = 1
                    console.log($s.tPersona);
                }
            } else {
                PersonaData.Persona = $s.tPersona;
                PersonaData.EducationalBackGround = $s.SchemaEducationalBackGround;
                PersonaData.CharacterReference = $s.SchemaCharacterReference;
                PersonaData.LicensesAndCertificates = $s.SchemaLicensesCertificates;
                PersonaData.PreviousEmployment = $s.SchemaPreviousEmployment;
                // Final Posting
                console.log(PersonaData);
                $mi.close(PersonaData);
            }
        }

        $s.Prev = function () {
            $('.ew-body').scrollTop(0);
            if ($s.currentEmpTab > 0) {
                $s.currentEmpTab = $s.currentEmpTab - 1;
                $('.ew-tab-item').removeClass('active');
                $('.ew-tab-content').removeClass('active');
                $('#ew-tc-' + $s.currentEmpTab).addClass('active');
                $('#ew-ti-' + $s.currentEmpTab).addClass('active');
            }
        }

        $s.Init = function () {
            $s.SetSystemStatus('Loading record #' + $s.RecordID, 'loading');
            $s.Request('LoadPersonaSchema', { LookupName: "tPersona", MenuCode: $s.MenuCode }, $s.myController ).then(function (ret) {
                if (ret.Type == 2) {
                    $s.SetSystemStatus(ret.Message, 'error');
                } else {
                    $s.Schema = $s.PlotDefault(ret.Data.Form, ret.Data.Schema, $s.RecordID);
                    $s.TableSchema = ret.Data.Schema;
                    $s.SetSystemStatus('Ready');
                }
                $s.$apply();
            })
            $s.LoadLookUp();
        }

        $s.close = function () {
            //Create vlidation that all changes will be cleared.
            $mi.close();
        }



        ///////////EDUCATIONAL BACKGROUD
        $s.SelectAllEducationalBackGround = false;
        $s.CheckAllDetails = function () {
            $s.SelectAllEducationalBackGround = !$s.SelectAllEducationalBackGround;
            Enumerable.From($s.SchemaEducationalBackGround).ForEach(function (rowDetails) {
                rowDetails.IsChecked = $s.SelectAllEducationalBackGround;
            });
        }


        $s.NewEducationBackGround = function () {
            var EducationBackGround = {
                ID: 0
                , ID_Persona: 0
                , ID_EducationAttainmentStatus: null
                , SchoolAttended: null
                , CourseDegree: null
                , IsActive: true
                , DatetimeCreated: new Date()
                , ID_UserCreatedBy: 0
                , From: null
                , To: null
            };

            $s.SchemaEducationalBackGround.push(EducationBackGround);
        }

        $s.DelEducationBackGround = function () {
            var rows = Enumerable
                .From($s.SchemaEducationalBackGround)
                .Where(function (x) { return x.IsChecked == true })
                .Select(function (x) { return x }).ToArray();

            if (rows.length > 0) {
                $s.Confirm('Are you sure you want to delete (' + rows.length + (rows.length > 1 ? ') records?' : ') record?'), 'Delete Record').then(function (r) {
                    for (var y = 0; y < rows.length; y++) {
                        var Index = $s.SchemaEducationalBackGround.indexOf(rows[y]);
                        $s.SchemaEducationalBackGround.splice(Index, 1);
                    }
                });
            }
        }

        ////////////////CHARACTER REFERENCE
        $s.SelectAllCharacterReference = false;
        $s.CheckAllCharReference = function () {
            $s.SelectAllCharacterReference = !$s.SelectAllCharacterReference;
            Enumerable.From($s.SchemaCharacterReference).ForEach(function (rowDetails) {
                rowDetails.IsChecked = $s.SelectAllCharacterReference;
            });
        }

        $s.NewCharReference = function () {
            var CharReference = {
                ID: 0
                , ID_Persona: 0
                , Name: null
                , Position: null
                , Company: null
                , ContactNo: null
                , IsActive: true
                , DatetimeCreated: new Date()
                , ID_UserCreatedBy: 0
            };

            $s.SchemaCharacterReference.push(CharReference);
        }

        $s.DelCharReference = function () {
            var rows = Enumerable
                .From($s.SchemaCharacterReference)
                .Where(function (x) { return x.IsChecked == true })
                .Select(function (x) { return x }).ToArray();

            if (rows.length > 0) {
                $s.Confirm('Are you sure you want to delete (' + rows.length + (rows.length > 1 ? ') records?' : ') record?'), 'Delete Record').then(function (r) {
                    for (var y = 0; y < rows.length; y++) {
                        var Index = $s.SchemaCharacterReference.indexOf(rows[y]);
                        $s.SchemaCharacterReference.splice(Index, 1);
                    }
                });
            }
        }

        ////////////////LICENSES AND CERTIFICATES
        $s.SelectAllLicensesAndCertificates = false;
        $s.CheckAllLicensesAndCertificates = function () {
            $s.SelectAllLicensesAndCertificates = !$s.SelectAllLicensesAndCertificates;
            Enumerable.From($s.SchemaLicensesCertificates).ForEach(function (rowDetails) {
                rowDetails.IsChecked = $s.SelectAllLicensesAndCertificates;
            });
        }

        $s.NewLicensesAndCertificates = function () {
            var LicensesAndCertificates = {
                ID: 0
                , ID_Persona: 0
                , Name: null
                , LicenseNo: null
                , Description: null
                , ValidityDate: null
                , IsActive: true
                , DatetimeCreated: new Date()
                , ID_UserCreatedBy: 0
            };

            $s.SchemaLicensesCertificates.push(LicensesAndCertificates);
        }

        $s.DelLicensesAndCertificates = function () {
            var rows = Enumerable
                .From($s.SchemaLicensesCertificates)
                .Where(function (x) { return x.IsChecked == true })
                .Select(function (x) { return x }).ToArray();

            if (rows.length > 0) {
                $s.Confirm('Are you sure you want to delete (' + rows.length + (rows.length > 1 ? ') records?' : ') record?'), 'Delete Record').then(function (r) {
                    for (var y = 0; y < rows.length; y++) {
                        var Index = $s.SchemaLicensesCertificates.indexOf(rows[y]);
                        $s.SchemaLicensesCertificates.splice(Index, 1);
                    }
                });
            }
        }

        ////////////////PREVIOUS EMPLOYMENT
        $s.SelectAllPreviousEmployment = false;
        $s.CheckAllPreviousEmployment = function () {
            $s.SelectAllPreviousEmployment = !$s.SelectAllPreviousEmployment;
            Enumerable.From($s.SchemaPreviousEmployment).ForEach(function (rowDetails) {
                rowDetails.IsChecked = $s.SelectAllPreviousEmployment;
            });
        }

        $s.NewPreviousEmployment = function () {
            var PreviousEmployment = {
                ID: 0
                , ID_Persona: 0
                , Company: null
                , Position: null
                , Address: null
                , From: null
                , To: null
                , Salary: null
                , ReasonForLeaving: null
                , IsActive: true
                , DatetimeCreated: new Date()
                , ID_UserCreatedBy: 0
            };

            $s.SchemaPreviousEmployment.push(PreviousEmployment);
        }

        $s.DelPreviousEmployment = function () {
            var rows = Enumerable
                .From($s.SchemaPreviousEmployment)
                .Where(function (x) { return x.IsChecked == true })
                .Select(function (x) { return x }).ToArray();

            if (rows.length > 0) {
                $s.Confirm('Are you sure you want to delete (' + rows.length + (rows.length > 1 ? ') records?' : ') record?'), 'Delete Record').then(function (r) {
                    for (var y = 0; y < rows.length; y++) {
                        var Index = $s.SchemaPreviousEmployment.indexOf(rows[y]);
                        $s.SchemaPreviousEmployment.splice(Index, 1);
                    }
                });
            }
        }
        $s.SetSystemStatus('Loading table', 'loading');
        $s.Init();
    }]).controller('dlgPersonaSelect', ['$scope', '$uibModalInstance', '$controller', 'dData', function ($s, $mi, $c, $dlgData) {
        $c('insysTableController', { $scope: $s });

        $s.MenuCode = 'PeopleEmployeeRecord';
        $s.myController = 'EmployeeRecord';


        $s.PersonaWithoutEmploymentList = [];
        $s.Schema = {};
        $s.IsSelectEmployee = false;

        $s.PersonaLookUp = {
            tblOptions: {
                Columns: [
                    { Name: 'ID', Label: '#' }
                    , { Name: 'Name', Label: 'Employee' }
                    , { Name: 'FirstName', Label: 'Firstname' }
                    , { Name: 'LastName', Label: 'Lastname' }
                ]
            },
            method: 'LoadPersonaWithoutEmployment',
            controller: $s.myController,
            parameter: { MenuCode: $s.MenuCode }
        };

        $s.getSchema = function (ret) {
            $s.Schema = ret;
        }

        $s.Select = function () {
            $mi.close($s.Schema);
        }

        $s.CreateNew = function () {
            $mi.close(0);
        }

        $s.close = function () {
            $mi.close(-1);
        }

    }]);