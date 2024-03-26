(function () {
    $(function () {
        console.log('BaseRMrate')

        var _$baseRMRatesTable = $('#BaseRMRatesTable');
        var _baseRMRatesService = abp.services.app.baseRMRates;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.BaseRMRate';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.BaseRMRates.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.BaseRMRates.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.BaseRMRates.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/BaseRMRates/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BaseRMRates/_CreateOrEditModal.js',
             modalClass: 'CreateOrEditBaseRMRateModal'
             
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

        var dataTable = _$baseRMRatesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _baseRMRatesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#BaseRMRatesTableFilter').val(),
					rMGroupNameFilter: $('#RMGroupNameFilterId').val(),
					unitOfMeasurementCodeFilter: $('#UnitOfMeasurementCodeFilterId').val(),
					yearNameFilter: $('#YearNameFilterId').val(),
					buyerNameFilter: $('#BuyerNameFilterId').val(),
					supplierNameFilter: $('#SupplierNameFilterId').val()
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
                            _createOrEditModal.open({ id: data.record.baseRMRate.id });                                
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
                                    entityId: data.record.baseRMRate.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteBaseRMRate(data.record.baseRMRate);
                            }
                        }]
                    }
                },
                {
                    targets: 1,
                    data: "rmGroupName",
                    name: "rmGroupFk.name"
                },
                {
                    targets: 2,
                    data: "buyerName",
                    name: "buyerFk.name"
                },
                {
                    targets: 3,
                    data: "supplierName",
                    name: "supplierFk.name"
                },
                {
                    targets: 4,
                    data: "unitOfMeasurementCode",
                    name: "unitOfMeasurementFk.code"
                },
                {
                    targets: 5,
                    data: "baseRMRate.month",
                    name: "month",
                    render: function (month) {
                        return app.localize('Enum_Months_' + month);
                    }
                },
                {
                    targets: 6,
                    data: "yearName",
                    name: "yearFk.name"
                },
                {
                    targets: 7,
                    data: "baseRMRate.unitRate",
                    name: "unitRate"
                },
                {
                    targets: 8,
                    data: "baseRMRate.scrapPercent",
                    name: "scrapPercent"
                },
                {
                    targets: 9,
                    data: "baseRMRate.scrapAmount",
                    name: "scrapAmount"
                },
                {
                    targets: 10,
                    data: "baseRMRate.weightRatio",
                    name: "weightRatio"
                },
                {
                    targets: 11,
                    data: "baseRMRate.lossRatio",
                    name: "lossRatio"
                }
					
            ]
        });

        function getBaseRMRates() {
            dataTable.ajax.reload();
        }

        function deleteBaseRMRate(baseRMRate) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _baseRMRatesService.delete({
                            id: baseRMRate.id
                        }).done(function () {
                            getBaseRMRates(true);
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

        $('#CreateNewBaseRMRateButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _baseRMRatesService
                .getBaseRMRatesToExcel({
				filter : $('#BaseRMRatesTableFilter').val(),
					rmGroupNameFilter: $('#RMGroupNameFilterId').val(),
					unitOfMeasurementCodeFilter: $('#UnitOfMeasurementCodeFilterId').val(),
					yearNameFilter: $('#YearNameFilterId').val(),
					buyerNameFilter: $('#BuyerNameFilterId').val(),
					supplierNameFilter: $('#SupplierNameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditBaseRMRateModalSaved', function () {
            getBaseRMRates();
        });

		$('#GetBaseRMRatesButton').click(function (e) {
            e.preventDefault();
            getBaseRMRates();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getBaseRMRates();
		  }
		});
    });
})();