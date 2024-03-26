(function () {
    $(function () {

        var _$uoMsTable = $('#UOMsTable');
        var _uoMsService = abp.services.app.uoMs;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.UOM';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.UOMs.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.UOMs.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.UOMs.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/UOMs/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/UOMs/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditUOMModal'
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

        var dataTable = _$uoMsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _uoMsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#UOMsTableFilter').val(),
					codeFilter: $('#CodeFilterId').val(),
					descriptionFilter: $('#DescriptionFilterId').val()
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
                            _createOrEditModal.open({ id: data.record.uom.id });                                
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
                                    entityId: data.record.uom.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteUOM(data.record.uom);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "uom.code",
						 name: "code"   
					},
					{
						targets: 2,
						 data: "uom.description",
						 name: "description"   
					}
            ]
        });

        function getUOMs() {
            dataTable.ajax.reload();
        }

        function deleteUOM(uom) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _uoMsService.delete({
                            id: uom.id
                        }).done(function () {
                            getUOMs(true);
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

        $('#CreateNewUOMButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _uoMsService
                .getUOMsToExcel({
				filter : $('#UOMsTableFilter').val(),
					codeFilter: $('#CodeFilterId').val(),
					descriptionFilter: $('#DescriptionFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditUOMModalSaved', function () {
            getUOMs();
        });

		$('#GetUOMsButton').click(function (e) {
            e.preventDefault();
            getUOMs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getUOMs();
		  }
		});
    });
})();