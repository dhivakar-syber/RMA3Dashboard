﻿(function () {
    $(function () {

        var _$unitOfMeasurementsTable = $('#UnitOfMeasurementsTable');
        var _unitOfMeasurementsService = abp.services.app.unitOfMeasurements;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.UnitOfMeasurement';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.UnitOfMeasurements.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.UnitOfMeasurements.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.UnitOfMeasurements.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/UnitOfMeasurements/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/UnitOfMeasurements/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditUnitOfMeasurementModal'
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

        var dataTable = _$unitOfMeasurementsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _unitOfMeasurementsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#UnitOfMeasurementsTableFilter').val(),
					codeFilter: $('#CodeFilterId').val(),
					nameFilter: $('#NameFilterId').val()
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
                            _createOrEditModal.open({ id: data.record.unitOfMeasurement.id });                                
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
                                    entityId: data.record.unitOfMeasurement.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteUnitOfMeasurement(data.record.unitOfMeasurement);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "unitOfMeasurement.code",
						 name: "code"   
					},
					{
						targets: 2,
						 data: "unitOfMeasurement.name",
						 name: "name"   
					}
            ]
        });

        function getUnitOfMeasurements() {
            dataTable.ajax.reload();
        }

        function deleteUnitOfMeasurement(unitOfMeasurement) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _unitOfMeasurementsService.delete({
                            id: unitOfMeasurement.id
                        }).done(function () {
                            getUnitOfMeasurements(true);
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

        $('#CreateNewUnitOfMeasurementButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _unitOfMeasurementsService
                .getUnitOfMeasurementsToExcel({
				filter : $('#UnitOfMeasurementsTableFilter').val(),
					codeFilter: $('#CodeFilterId').val(),
					nameFilter: $('#NameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditUnitOfMeasurementModalSaved', function () {
            getUnitOfMeasurements();
        });

		$('#GetUnitOfMeasurementsButton').click(function (e) {
            e.preventDefault();
            getUnitOfMeasurements();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getUnitOfMeasurements();
		  }
		});
    });
})();