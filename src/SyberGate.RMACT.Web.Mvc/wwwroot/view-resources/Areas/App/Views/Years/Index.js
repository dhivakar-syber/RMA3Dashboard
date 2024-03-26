(function () {
    $(function () {

        var _$yearsTable = $('#YearsTable');
        var _yearsService = abp.services.app.years;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.Years.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.Years.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.Years.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Years/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Years/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditYearModal'
        });       


		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }

        var dataTable = _$yearsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _yearsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#YearsTableFilter').val()
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
                            _createOrEditModal.open({ id: data.record.year.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteYear(data.record.year);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "year.name",
						 name: "name"   
                },
                {
                    targets: 2,
                    data: "year.seqNo",
                    name: "seqno"
                }
            ]
        });

        function getYears() {
            dataTable.ajax.reload();
        }

        function deleteYear(year) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _yearsService.delete({
                            id: year.id
                        }).done(function () {
                            getYears(true);
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

        $('#CreateNewYearButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditYearModalSaved', function () {
            getYears();
        });

		$('#GetYearsButton').click(function (e) {
            e.preventDefault();
            getYears();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getYears();
		  }
		});
    });
})();