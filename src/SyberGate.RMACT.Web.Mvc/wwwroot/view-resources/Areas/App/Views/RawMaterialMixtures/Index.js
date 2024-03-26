(function () {
    $(function () {

        var _$rawMaterialMixturesTable = $('#RawMaterialMixturesTable');
        var _rawMaterialMixturesService = abp.services.app.rawMaterialMixtures;
		var _entityTypeFullName = 'SyberGate.RMACT.Masters.RawMaterialMixture';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.RawMaterialMixtures.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.RawMaterialMixtures.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.RawMaterialMixtures.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialMixtures/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialMixtures/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditRawMaterialMixtureModal'
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

        var dataTable = _$rawMaterialMixturesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _rawMaterialMixturesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#RawMaterialMixturesTableFilter').val(),
					rmGroupNameFilter: $('#RMGroupNameFilterId').val(),
					rawMaterialGradeNameFilter: $('#RawMaterialGradeNameFilterId').val(),
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
                            _createOrEditModal.open({ id: data.record.rawMaterialMixture.id });                                
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
                                    entityId: data.record.rawMaterialMixture.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteRawMaterialMixture(data.record.rawMaterialMixture);
                            }
                        }]
                    }
                },

                {
                    targets: 1,
                    data: "buyerName",
                    name: "buyerFk.name"
                },
                {
                    targets: 2,
                    data: "supplierName",
                    name: "supplierFk.name"
                },
                {
						targets: 3,
						 data: "rmGroupName" ,
						 name: "rmGroupFk.name" 
					},
					{
						targets: 4,
						 data: "rawMaterialGradeName" ,
						 name: "rawMaterialGradeFk.name" 
					},
					
					
                {
                    targets: 5,
                    data: "rawMaterialMixture.weightRatio",
                    name: "weightRatio"
                },
                {
                    targets: 6,
                    data: "rawMaterialMixture.lossRatio",
                    name: "lossRatio"
                }

            ]
        });

        function getRawMaterialMixtures() {
            dataTable.ajax.reload();
        }

        function deleteRawMaterialMixture(rawMaterialMixture) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _rawMaterialMixturesService.delete({
                            id: rawMaterialMixture.id
                        }).done(function () {
                            getRawMaterialMixtures(true);
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

        $('#CreateNewRawMaterialMixtureButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditRawMaterialMixtureModalSaved', function () {
            getRawMaterialMixtures();
        });

		$('#GetRawMaterialMixturesButton').click(function (e) {
            e.preventDefault();
            getRawMaterialMixtures();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getRawMaterialMixtures();
		  }
		});
    });
})();