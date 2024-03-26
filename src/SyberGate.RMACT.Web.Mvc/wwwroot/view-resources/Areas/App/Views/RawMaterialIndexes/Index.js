(function () {
    $(function () {

        var _$rawMaterialIndexesTable = $('#RawMaterialIndexesTable');
        var _rawMaterialIndexesService = abp.services.app.rawMaterialIndexes;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.RawMaterialIndex';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.RawMaterialIndexes.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.RawMaterialIndexes.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.RawMaterialIndexes.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialIndexes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialIndexes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditRawMaterialIndexModal'
        });       

		 var _viewRawMaterialIndexModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialIndexes/ViewrawMaterialIndexModal',
            modalClass: 'ViewRawMaterialIndexModal'
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

        var dataTable = _$rawMaterialIndexesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _rawMaterialIndexesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#RawMaterialIndexesTableFilter').val(),
					monthFilter: $('#MonthFilterId').val(),
					indexNameNameFilter: $('#IndexNameNameFilterId').val(),
					yearNameFilter: $('#YearNameFilterId').val(),
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
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewRawMaterialIndexModal.open({ id: data.record.rawMaterialIndex.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.rawMaterialIndex.id });                                
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
                                    entityId: data.record.rawMaterialIndex.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteRawMaterialIndex(data.record.rawMaterialIndex);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "rawMaterialIndex.month",
						 name: "month"   ,
						render: function (month) {
							return app.localize('Enum_Months_' + month);
						}
			
					},
					{
						targets: 2,
						 data: "rawMaterialIndex.value",
						 name: "value"   
					},
					{
						targets: 3,
						 data: "indexNameName" ,
						 name: "indexNameFk.name" 
					},
					{
						targets: 4,
						 data: "yearName" ,
						 name: "yearFk.name" 
					},
					{
						targets: 5,
						 data: "rawMaterialGradeName" ,
						 name: "rawMaterialGradeFk.name" 
					}
            ]
        });

        function getRawMaterialIndexes() {
            dataTable.ajax.reload();
        }

        function deleteRawMaterialIndex(rawMaterialIndex) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _rawMaterialIndexesService.delete({
                            id: rawMaterialIndex.id
                        }).done(function () {
                            getRawMaterialIndexes(true);
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

        $('#CreateNewRawMaterialIndexButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _rawMaterialIndexesService
                .getRawMaterialIndexesToExcel({
				filter : $('#RawMaterialIndexesTableFilter').val(),
					monthFilter: $('#MonthFilterId').val(),
					indexNameNameFilter: $('#IndexNameNameFilterId').val(),
					yearNameFilter: $('#YearNameFilterId').val(),
					rawMaterialGradeNameFilter: $('#RawMaterialGradeNameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditRawMaterialIndexModalSaved', function () {
            getRawMaterialIndexes();
        });

		$('#GetRawMaterialIndexesButton').click(function (e) {
            e.preventDefault();
            getRawMaterialIndexes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getRawMaterialIndexes();
		  }
		});
    });
})();