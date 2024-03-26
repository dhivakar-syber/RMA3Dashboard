(function () {
    $(function () {

        var _$partBucketsTable = $('#PartBucketsTable');
        var _partBucketsService = abp.services.app.partBuckets;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.PartBuckets.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.PartBuckets.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.PartBuckets.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/PartBuckets/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/PartBuckets/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditPartBucketModal'
        });       

		 var _viewPartBucketModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/PartBuckets/ViewpartBucketModal',
            modalClass: 'ViewPartBucketModal'
        });

		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }

        var dataTable = _$partBucketsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _partBucketsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#PartBucketsTableFilter').val(),
					partNumberFilter: $('#PartNumberFilterId').val(),
					bucketsFilter: $('#BucketsFilterId').val(),
					minValueFilter: $('#MinValueFilterId').val(),
					maxValueFilter: $('#MaxValueFilterId').val(),
					buyerFilter: $('#BuyerFilterId').val(),
					supplierFilter: $('#SupplierFilterId').val()
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
                                    _viewPartBucketModal.open({ id: data.record.partBucket.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.partBucket.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deletePartBucket(data.record.partBucket);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "partBucket.partNumber",
						 name: "partNumber"   
					},
					{
						targets: 2,
						 data: "partBucket.buckets",
						 name: "buckets"   
					},
					{
						targets: 3,
						 data: "partBucket.value",
						 name: "value"   
					},
					{
						targets: 4,
						 data: "partBucket.buyer",
						 name: "buyer"   
					},
					{
						targets: 5,
						 data: "partBucket.supplier",
						 name: "supplier"   
					}
            ]
        });

        function getPartBuckets() {
            dataTable.ajax.reload();
        }

        function deletePartBucket(partBucket) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _partBucketsService.delete({
                            id: partBucket.id
                        }).done(function () {
                            getPartBuckets(true);
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

        $('#CreateNewPartBucketButton').click(function () {
            _createOrEditModal.open();
        });        

        $('#ImportPartBucketsFromExcelButton').fileupload({
            url: abp.appPath + 'App/PartBuckets/ImportFromExcel',
            dataType: 'json',
            maxFileSize: 1048576 * 100,
            dropZone: $('#PartBucketsTable'),
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

		$('#ExportToExcelButton').click(function () {
            _partBucketsService
                .getPartBucketsToExcel({
				filter : $('#PartBucketsTableFilter').val(),
					partNumberFilter: $('#PartNumberFilterId').val(),
					bucketsFilter: $('#BucketsFilterId').val(),
					minValueFilter: $('#MinValueFilterId').val(),
					maxValueFilter: $('#MaxValueFilterId').val(),
					buyerFilter: $('#BuyerFilterId').val(),
					supplierFilter: $('#SupplierFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditPartBucketModalSaved', function () {
            getPartBuckets();
        });

		$('#GetPartBucketsButton').click(function (e) {
            e.preventDefault();
            getPartBuckets();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getPartBuckets();
		  }
		});
    });
})();