(function ($) {
    app.modals.SupportAttachmentsViewModel = function () {
        console.log('SupportAttachments')
        var _tenantDashboardService = abp.services.app.tenantDashboard;
        var _$supportAttachmentsTable = $('#SupportAttachments');
        var _a3DocumentsService = abp.services.app.a3Documents;
        var _modalManager;
        var mail = "sgt101055@gmail.com"
        var A3id = 0;
        this.init = function (modalManager) {
            _modalManager = modalManager;
           // getA3Documents();
            dataTable.ajax.reload();
            //dataTable.ajax.processing(false);
            abp.ui.clearBusy($("body"));
        };

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.A3Documents.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.A3Documents.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.A3Documents.Delete'),
            confirm: abp.auth.hasPermission('Pages.Administration.A3Documents.Confirm')
        };







        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z");
        }

        var dataTable = _$supportAttachmentsTable.DataTable({
            paging: true,
            pageLength: 10,
            serverSide: true,
            processing: true,
            hasContent: true,
            listAction: {
                ajaxFunction: _a3DocumentsService.getAllSupportAttachments,
                inputFilter: function () {
                    return {
                       
                        a3IdFilter: $('#a3FilterId').val(),
                        buyerFilter: $('#BuyerFilterId').val(),
                        supplierFilter: $('#supplierFilterId').val(),
                        versionFilter: $('#versionFilterId').val()
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
                    render: function (data, type, row) {
                        // Add toggle button HTML here
                        return '<button class="toggle-button" style="background-color: #71180C; color: white;" data-id="' + row.supportAttachment.id + '" data-buyer="' + row.supportAttachment.buyer + '" data-supplier="' + row.supportAttachment.supplier + '">Delete</button>';
                    }
                },
                
                {
                    targets: 1,
                    data: "supportAttachment.buyer",
                    name: "A3Id"
                },
                {
                    targets: 2,
                    data: "supportAttachment.supplier",
                    name: "SupplierAttachment"
                },
                {
                    targets: 3,
                    data: "supportAttachment.version",
                    name: "version"
                },
                {
                    targets: 4,
                    data: "supportAttachment.fileName",
                    name: "FileName"
                }
                
                
                
                

            ]
        });

        $(document).on('click', '.toggle-button', function () {

            

            
            var id = $(this).data('id');
            var buyer = $(this).data('buyer');
            var supplier = $(this).data('supplier');
            
            _a3DocumentsService.deleteSupportAttachments(id).done(function () {
                abp.notify.info(app.localize('selected support Attachment deleted Sent Successfully'));
                _modalManager.close();
            });;
        });

        

       

        $('#fileupload').fileupload({
            url: abp.appPath + 'App/A3Documents/UploadExcel',
            dataType: 'json',
            maxFileSize: 1048576 * 100,
            formData: {
                a3id: $('#a3FilterId').val(),
                buyer: $('#BuyerFilterId').val(),
                supplier: $('#supplierFilterId').val(),
                version: $('#versionFilterId').val()
                        },
            dropZone: $("body"),
            done: function (e, response) {
                var jsonResult = response.result;
                if (jsonResult.success) {
                    abp.notify.info(app.localize('Support Attachment Uploaded Successfully'));
                } else {
                    abp.notify.warn(app.localize('upload Failed'));
                }
            }
        }).prop('disabled', !$.support.fileInput)
            .parent().addClass($.support.fileInput ? undefined : 'disabled');

        




       

        //$(document).keypress(function (e) {
        //    if (e.which === 13) {
        //        getA3Documents();
        //    }
        //});

       // getA3Documents();
    };
})(jQuery);