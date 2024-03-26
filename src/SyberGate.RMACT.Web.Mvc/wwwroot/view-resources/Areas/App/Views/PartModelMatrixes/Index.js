(function () {
    $(function () {

        var _$partModelMatrixesTable = $('#PartModelMatrixesTable');
        var _partModelMatrixesService = abp.services.app.partModelMatrixes;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.PartModelMatrix';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.PartModelMatrixes.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.PartModelMatrixes.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.PartModelMatrixes.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/PartModelMatrixes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/PartModelMatrixes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditPartModelMatrixModal'
        });       

		 var _viewPartModelMatrixModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/PartModelMatrixes/ViewpartModelMatrixModal',
            modalClass: 'ViewPartModelMatrixModal'
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

        var dataTable = _$partModelMatrixesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _partModelMatrixesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#PartModelMatrixesTableFilter').val(),
					partNumberFilter: $('#PartNumberFilterId').val(),
					leadModelNameFilter: $('#LeadModelNameFilterId').val()
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
                                    _viewPartModelMatrixModal.open({ id: data.record.partModelMatrix.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.partModelMatrix.id });                                
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
                                    entityId: data.record.partModelMatrix.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deletePartModelMatrix(data.record.partModelMatrix);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "partModelMatrix.partNumber",
						 name: "partNumber"   
					},
					{
						targets: 2,
						 data: "partModelMatrix.quantity",
						 name: "quantity"   
					},
					{
						targets: 3,
						 data: "leadModelName" ,
						 name: "leadModelFk.name" 
					}
            ]
        });

        function getPartModelMatrixes() {
            dataTable.ajax.reload();
        }

        function deletePartModelMatrix(partModelMatrix) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _partModelMatrixesService.delete({
                            id: partModelMatrix.id
                        }).done(function () {
                            getPartModelMatrixes(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }
        $('#ImportPartModelMatrixesFromExcelButton').fileupload({
            url: abp.appPath + 'App/PartModelMatrixes/ImportFromExcel',
            dataType: 'json',
            maxFileSize: 1048576 * 100,
            dropZone: $('#PartModelMatrixesTable'),
            done: function (e, response) {
                var jsonResult = response.result;
                if (jsonResult.success) {
                    abp.notify.info(app.localize('UpdatePartModelMatrixesMasterProcessStarted'));
                } else {
                    abp.notify.warn(app.localize('UpdatePartModelMatrixesMasterProcessFailed'));
                }
            }
        }).prop('disabled', !$.support.fileInput)
            .parent().addClass($.support.fileInput ? undefined : 'disabled');

        


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

        $('#CreateNewPartModelMatrixButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditPartModelMatrixModalSaved', function () {
            getPartModelMatrixes();
        });

		$('#GetPartModelMatrixesButton').click(function (e) {
            e.preventDefault();
            getPartModelMatrixes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getPartModelMatrixes();
		  }
		});
    });
})();