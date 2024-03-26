(function () {
    $(function () {

        var _$plantsTable = $('#PlantsTable');
        var _plantsService = abp.services.app.plants;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.Plants.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.Plants.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.Plants.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Plants/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Plants/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditPlantModal'
        });       

		 var _viewPlantModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Plants/ViewplantModal',
            modalClass: 'ViewPlantModal'
        });

		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }

        var dataTable = _$plantsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _plantsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#PlantsTableFilter').val(),
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
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewPlantModal.open({ id: data.record.plant.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.plant.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deletePlant(data.record.plant);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "plant.code",
						 name: "code"   
					},
					{
						targets: 2,
						 data: "plant.description",
						 name: "description"   
					}
            ]
        });

        function getPlants() {
            dataTable.ajax.reload();
        }

        function deletePlant(plant) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _plantsService.delete({
                            id: plant.id
                        }).done(function () {
                            getPlants(true);
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

        $('#CreateNewPlantButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditPlantModalSaved', function () {
            getPlants();
        });

		$('#GetPlantsButton').click(function (e) {
            e.preventDefault();
            getPlants();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getPlants();
		  }
		});
    });
})();