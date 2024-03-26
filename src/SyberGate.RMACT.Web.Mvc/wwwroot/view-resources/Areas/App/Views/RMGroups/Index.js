(function () {
    $(function () {

        var _$rmGroupsTable = $('#RMGroupsTable');
        var _rmGroupsService = abp.services.app.rMGroups;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.RMGroup';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.RMGroups.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.RMGroups.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.RMGroups.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RMGroups/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RMGroups/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditRMGroupModal'
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

        var dataTable = _$rmGroupsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _rmGroupsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#RMGroupsTableFilter').val(),
					hasMixtureFilter: $('#HasMixtureFilterId').val()
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
                            _createOrEditModal.open({ id: data.record.rmGroup.id });                                
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
                                    entityId: data.record.rmGroup.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteRMGroup(data.record.rmGroup);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "rmGroup.name",
						 name: "name"   
					},
					{
						targets: 2,
						 data: "rmGroup.hasMixture",
						 name: "hasMixture"  ,
						render: function (hasMixture) {
							if (hasMixture) {
								return '<div class="text-center"><i class="fa fa-check kt--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					}
            ]
        });

        function getRMGroups() {
            dataTable.ajax.reload();
        }

        function deleteRMGroup(rmGroup) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _rmGroupsService.delete({
                            id: rmGroup.id
                        }).done(function () {
                            getRMGroups(true);
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

        $('#CreateNewRMGroupButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditRMGroupModalSaved', function () {
            getRMGroups();
        });

		$('#GetRMGroupsButton').click(function (e) {
            e.preventDefault();
            getRMGroups();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getRMGroups();
		  }
		});
    });
})();