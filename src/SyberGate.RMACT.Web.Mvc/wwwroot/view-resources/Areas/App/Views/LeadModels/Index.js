(function () {
    $(function () {

        var _$leadModelsTable = $('#LeadModelsTable');
        var _leadModelsService = abp.services.app.leadModels;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.LeadModel';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.LeadModels.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.LeadModels.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.LeadModels.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/LeadModels/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/LeadModels/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditLeadModelModal'
        });       

		 var _viewLeadModelModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/LeadModels/ViewleadModelModal',
            modalClass: 'ViewLeadModelModal'
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

        var dataTable = _$leadModelsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _leadModelsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#LeadModelsTableFilter').val(),
					nameFilter: $('#NameFilterId').val(),
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
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewLeadModelModal.open({ id: data.record.leadModel.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.leadModel.id });                                
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
                                    entityId: data.record.leadModel.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteLeadModel(data.record.leadModel);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "leadModel.name",
						 name: "name"   
					},
					{
						targets: 2,
						 data: "leadModel.description",
						 name: "description"   
					}
            ]
        });

        function getLeadModels() {
            dataTable.ajax.reload();
        }

        function deleteLeadModel(leadModel) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _leadModelsService.delete({
                            id: leadModel.id
                        }).done(function () {
                            getLeadModels(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        $('#ImportLeadModelsFromExcelButton').fileupload({
            url: abp.appPath + 'App/LeadModels/ImportFromExcel',
            dataType: 'json',
            maxFileSize: 1048576 * 100,
            dropZone: $('#LeadModelsTable'),
            done: function (e, response) {
                var jsonResult = response.result;
                if (jsonResult.success) {
                    abp.notify.info(app.localize('UpdateLeadModelsMasterProcessStarted'));
                } else {
                    abp.notify.warn(app.localize('UpdateLeadModelsMasterProcessFailed'));
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

        $('#CreateNewLeadModelButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditLeadModelModalSaved', function () {
            getLeadModels();
        });

		$('#GetLeadModelsButton').click(function (e) {
            e.preventDefault();
            getLeadModels();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getLeadModels();
		  }
		});
    });
})();