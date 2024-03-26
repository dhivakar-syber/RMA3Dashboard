(function () {
    $(function () {

        var _$indexNamesTable = $('#IndexNamesTable');
        var _indexNamesService = abp.services.app.indexNames;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.IndexName';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.IndexNames.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.IndexNames.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.IndexNames.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/IndexNames/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/IndexNames/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditIndexNameModal'
        });       

		 var _viewIndexNameModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/IndexNames/ViewindexNameModal',
            modalClass: 'ViewIndexNameModal'
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

        var dataTable = _$indexNamesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _indexNamesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#IndexNamesTableFilter').val(),
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
                                    _viewIndexNameModal.open({ id: data.record.indexName.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.indexName.id });                                
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
                                    entityId: data.record.indexName.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteIndexName(data.record.indexName);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "indexName.name",
						 name: "name"   
					}
            ]
        });

        function getIndexNames() {
            dataTable.ajax.reload();
        }

        function deleteIndexName(indexName) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _indexNamesService.delete({
                            id: indexName.id
                        }).done(function () {
                            getIndexNames(true);
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

        $('#CreateNewIndexNameButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _indexNamesService
                .getIndexNamesToExcel({
				filter : $('#IndexNamesTableFilter').val(),
					nameFilter: $('#NameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditIndexNameModalSaved', function () {
            getIndexNames();
        });

		$('#GetIndexNamesButton').click(function (e) {
            e.preventDefault();
            getIndexNames();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getIndexNames();
		  }
		});
    });
})();