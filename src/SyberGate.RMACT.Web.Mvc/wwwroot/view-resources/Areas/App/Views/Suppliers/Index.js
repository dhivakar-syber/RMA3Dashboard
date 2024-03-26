(function () {
    $(function () {

        var _$suppliersTable = $('#SuppliersTable');
        var _suppliersService = abp.services.app.suppliers;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.Supplier';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.Suppliers.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.Suppliers.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.Suppliers.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Suppliers/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Suppliers/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditSupplierModal'
        });       

		 var _viewSupplierModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Suppliers/ViewsupplierModal',
            modalClass: 'ViewSupplierModal'
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

        var dataTable = _$suppliersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _suppliersService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#SuppliersTableFilter').val(),
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
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewSupplierModal.open({ id: data.record.supplier.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.supplier.id });                                
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
                                    entityId: data.record.supplier.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteSupplier(data.record.supplier);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "supplier.code",
						 name: "code"   
					},
					{
						targets: 2,
						 data: "supplier.name",
						 name: "name"   
					}
            ]
        });

        function getSuppliers() {
            dataTable.ajax.reload();
        }

        function deleteSupplier(supplier) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _suppliersService.delete({
                            id: supplier.id
                        }).done(function () {
                            getSuppliers(true);
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

        $('#CreateNewSupplierButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _suppliersService
                .getSuppliersToExcel({
				filter : $('#SuppliersTableFilter').val(),
					codeFilter: $('#CodeFilterId').val(),
					nameFilter: $('#NameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditSupplierModalSaved', function () {
            getSuppliers();
        });

		$('#GetSuppliersButton').click(function (e) {
            e.preventDefault();
            getSuppliers();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getSuppliers();
		  }
		});
    });
})();