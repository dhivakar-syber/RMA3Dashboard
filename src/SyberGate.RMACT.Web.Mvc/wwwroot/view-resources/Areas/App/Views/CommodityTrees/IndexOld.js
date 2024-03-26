(function () {
    $(function () {

        var _$commodityTreesTable = $('#CommodityTreesTable');
        var _commodityTreesService = abp.services.app.commodityTrees;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.CommodityTree';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.CommodityTrees.Create'),
            edit: abp.auth.hasPermission('Pages.CommodityTrees.Edit'),
            'delete': abp.auth.hasPermission('Pages.CommodityTrees.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/CommodityTrees/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/CommodityTrees/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditCommodityTreeModal'
        });       

		 var _viewCommodityTreeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/CommodityTrees/ViewcommodityTreeModal',
            modalClass: 'ViewCommodityTreeModal'
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

        var dataTable = _$commodityTreesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _commodityTreesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#CommodityTreesTableFilter').val(),
					dispalayNameFilter: $('#DispalayNameFilterId').val()
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
                                    _viewCommodityTreeModal.open({ id: data.record.commodityTree.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.commodityTree.id });                                
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
                                    entityId: data.record.commodityTree.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteCommodityTree(data.record.commodityTree);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "commodityTree.dispalayName",
						 name: "dispalayName"   
					}
            ]
        });

        function getCommodityTrees() {
            dataTable.ajax.reload();
        }

        function deleteCommodityTree(commodityTree) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _commodityTreesService.delete({
                            id: commodityTree.id
                        }).done(function () {
                            getCommodityTrees(true);
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

        $('#CreateNewCommodityTreeButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditCommodityTreeModalSaved', function () {
            getCommodityTrees();
        });

		$('#GetCommodityTreesButton').click(function (e) {
            e.preventDefault();
            getCommodityTrees();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getCommodityTrees();
		  }
		});
    });
})();