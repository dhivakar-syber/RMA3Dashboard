(function () {
    $(function () {

        var _$rawMaterialGradesTable = $('#RawMaterialGradesTable');
        var _rawMaterialGradesService = abp.services.app.rawMaterialGrades;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.RawMaterialGrade';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.RawMaterialGrades.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.RawMaterialGrades.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.RawMaterialGrades.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialGrades/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialGrades/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditRawMaterialGradeModal'
         });       


		        var _entityTypeHistoryModal = app.modals.EntityTypeHistoryModal.create();
		        function entityHistoryIsEnabled() {
            return abp.auth.hasPermission('Pages.Administration.AuditLogs') &&
                abp.custom.EntityHistory &&
                abp.custom.EntityHistory.IsEnabled &&
                _.filter(abp.custom.EntityHistory.EnabledEntities, entityType => entityType === _entityTypeFullName).length === 1;
        }

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }

        var dataTable = _$rawMaterialGradesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _rawMaterialGradesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#RawMaterialGradesTableFilter').val(),
					nameFilter: $('#NameFilterId').val(),
					isGroupFilter: $('#IsGroupFilterId').val(),
					hasMixtureFilter: $('#HasMixtureFilterId').val(),
					rawMaterialGradeNameFilter: $('#RawMaterialGradeNameFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    width: 120,
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.rawMaterialGrade.id });                                
                            }
                        },
                        {
                            text: app.localize('History'),
                            visible: function () {
                                return entityHistoryIsEnabled();
                            },
                            action: function (data) {
                                _entityTypeHistoryModal.open({
                                    entityTypeFullName: _entityTypeFullName,
                                    entityId: data.record.rawMaterialGrade.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteRawMaterialGrade(data.record.rawMaterialGrade);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "rawMaterialGrade.name",
						 name: "name"   
					},
					{
						targets: 2,
						 data: "rawMaterialGrade.isGroup",
						 name: "isGroup"  ,
						render: function (isGroup) {
							if (isGroup) {
								return '<div class="text-center"><i class="fa fa-check kt--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 3,
						 data: "rawMaterialGrade.hasMixture",
						 name: "hasMixture"  ,
						render: function (hasMixture) {
							if (hasMixture) {
								return '<div class="text-center"><i class="fa fa-check kt--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 4,
						 data: "rawMaterialGradeName" ,
						 name: "rawMaterialGradeFk.name" 
					}
            ]
        });

        function getRawMaterialGrades() {
            dataTable.ajax.reload();
        }

        function deleteRawMaterialGrade(rawMaterialGrade) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _rawMaterialGradesService.delete({
                            id: rawMaterialGrade.id
                        }).done(function () {
                            getRawMaterialGrades(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

		$('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

        $('#CreateNewRawMaterialGradeButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _rawMaterialGradesService
                .getRawMaterialGradesToExcel({
				filter : $('#RawMaterialGradesTableFilter').val(),
					nameFilter: $('#NameFilterId').val(),
					isGroupFilter: $('#IsGroupFilterId').val(),
					hasMixtureFilter: $('#HasMixtureFilterId').val(),
					rawMaterialGradeNameFilter: $('#RawMaterialGradeNameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditRawMaterialGradeModalSaved', function () {
            getRawMaterialGrades();
        });

		$('#GetRawMaterialGradesButton').click(function (e) {
            e.preventDefault();
            getRawMaterialGrades();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getRawMaterialGrades();
		  }
            });
    });
})();