(function () {
    $(function () {

        var _$partsTable = $('#PartsTable');
        var _tenantDashboardService = abp.services.app.tenantDashboard;
        var _partsService = abp.services.app.parts;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.Part';
        var _$Buyer;
        var _$Supplier;
        var _$BuyerName;
        var _$SupplierName;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.Parts.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.Parts.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.Parts.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Parts/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Parts/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditPartModal'
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

        var dataTable = _$partsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _partsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#PartsTableFilter').val(),
					partNoFilter: $('#PartNoFilterId').val(),
					supplierNameFilter: $('#SupplierNameFilterId').val(),
					buyerNameFilter: $('#BuyerNameFilterId').val(),
					rmGroupNameFilter: $('#RMGroupNameFilterId').val()
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
                            _createOrEditModal.open({ id: data.record.part.id });                                
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
                                    entityId: data.record.part.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deletePart(data.record.part);
                            }
                        }]
                    }
                },
                {
                    targets: 1,
                    data: "part.partNo",
                    name: "partNo"
                },
                {
                    targets: 2,
                    data: "part.description",
                    name: "description"
                },
                {
                    targets: 3,
                    data: "supplierName",
                    name: "supplierFk.name"
                },
                {
                    targets: 4,
                    data: "buyerName",
                    name: "buyerFk.name"
                },
                {
                    targets: 5,
                    data: "rmGroupName",
                    name: "rmGroupFk.name"
                },
                {
                    targets: 6,
                    data: "part.grossInputWeight",
                    name: "grossInputWeight"
                },
                {
                    targets: 7,
                    data: "part.castingForgingWeight",
                    name: "castingForgingWeight"
                },
                {
                    targets: 8,
                    data: "part.finishedWeight",
                    name: "finishedWeight"
                },
                {
                    targets: 9,
                    data: "part.scrapRecoveryPercent",
                    name: "scrapRecoveryPercent"
                }	
            ]
        });

        function getParts() {
            dataTable.ajax.reload();
        }

        function deletePart(part) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _partsService.delete({
                            id: part.id
                        }).done(function () {
                            getParts(true);
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

        $('#CreateNewPartButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ImportPartsToExcelTemplateButton').click(function () {
            abp.ui.setBusy(_$partsTable);
            _partsService.getPartsTemplate({
                supplierId: _$Supplier,
                buyerId: _$Buyer,
                templatePath: abp.appPath + 'assets/SampleFiles/PartsUploadTemplate.xlsx'
            })
            .done(function (result) {
                app.downloadTempFile(result);
            })
            .always(function () {
                abp.ui.clearBusy(_$partsTable);
            });
        });

        $('#ImportPartsFromExcelButton').fileupload({
            url: abp.appPath + 'App/Parts/ImportPartMasterFromExcel',
            dataType: 'json',
            maxFileSize: 1048576 * 100,
            dropZone: $('#PartsTable'),
            done: function (e, response) {
                var jsonResult = response.result;
                if (jsonResult.success) {
                    abp.notify.info(app.localize('UpdatePartsMasterProcessStarted'));
                } else {
                    abp.notify.warn(app.localize('UpdatePartsMasterProcessFailed'));
                }
            }
        }).prop('disabled', !$.support.fileInput)
            .parent().addClass($.support.fileInput ? undefined : 'disabled');

        $('#ImportGlobusDataFromExcelButton').fileupload({
            url: abp.appPath + 'App/Parts/ImportGlobusDataFromExcel',
            dataType: 'json',
            maxFileSize: 1048576 * 100,
            dropZone: $('#PartsTable'),
            done: function (e, response) {
                var jsonResult = response.result;
                if (jsonResult.success) {
                    abp.notify.info(app.localize('UpdateGlobusDatasMasterProcessStarted'));
                } else {
                    abp.notify.warn(app.localize('UpdateGlobusDatasMasterProcessFailed'));
                }
            }
        }).prop('disabled', !$.support.fileInput)
            .parent().addClass($.support.fileInput ? undefined : 'disabled');

		$('#ExportToExcelButton').click(function () {
            _partsService
                .getPartsToExcel({
				filter : $('#PartsTableFilter').val(),
					partNoFilter: $('#PartNoFilterId').val(),
					supplierNameFilter: $('#SupplierNameFilterId').val(),
					buyerNameFilter: $('#BuyerNameFilterId').val(),
					rmGroupNameFilter: $('#RMGroupNameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditPartModalSaved', function () {
            getParts();
        });

		$('#GetPartsButton').click(function (e) {
            e.preventDefault();
            getParts();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getParts();
		  }
        });

        _tenantDashboardService
            .getBuyerSettings()
            .done(function (result) {
                _$Buyer =result;
            });

        _tenantDashboardService
            .getSupplierSettings()
            .done(function (result) {
                _$Supplier = result;
            });

    });
})();