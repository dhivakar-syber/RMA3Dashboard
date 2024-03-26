(function () {
    $(function () {

        var _$a3DocumentsTable = $('#A3DocumentsTable');
        var _a3DocumentsService = abp.services.app.a3Documents;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.A3Documents.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.A3Documents.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.A3Documents.Delete')
        };

               


		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }

        var dataTable = _$a3DocumentsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _a3DocumentsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#A3DocumentsTableFilter').val(),
					buyerFilter: $('#BuyerFilterId').val(),
					supplierFilter: $('#SupplierFilterId').val(),
					monthFilter: $('#MonthFilterId').val(),
					yearFilter: $('#YearFilterId').val()
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
                            window.location="/App/A3Documents/CreateOrEdit/" + data.record.a3Document.id;                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteA3Document(data.record.a3Document);
                            }
                        }]
                    }
                },
                {
                    targets: 1,
                    data: "a3Document.version",
                    name: "version"
                },
					{
						targets: 2,
						 data: "a3Document.buyer",
						 name: "buyer"   
					},
					{
						targets: 3,
						 data: "a3Document.supplier",
						 name: "supplier"   
					},
					{
						targets: 4,
						 data: "a3Document.month",
						 name: "month"   
					},
					{
						targets: 5,
						 data: "a3Document.year",
						 name: "year"   
					}
            ]
        });

        function getA3Documents() {
            dataTable.ajax.reload();
        }

        function deleteA3Document(a3Document) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _a3DocumentsService.delete({
                            id: a3Document.id
                        }).done(function () {
                            getA3Documents(true);
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

                

		

        abp.event.on('app.createOrEditA3DocumentModalSaved', function () {
            getA3Documents();
        });

		$('#GetA3DocumentsButton').click(function (e) {
            e.preventDefault();
            getA3Documents();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getA3Documents();
		  }
		});
    });
})();