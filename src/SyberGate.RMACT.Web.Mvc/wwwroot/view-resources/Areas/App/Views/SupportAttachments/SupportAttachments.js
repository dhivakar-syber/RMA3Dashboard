(function ($) {
    app.modals.SupportAttachmentsViewModel = function () {

        var _tenantDashboardService = abp.services.app.tenantDashboard;
        var _$supportAttachmentsTable = $('#SupportAttachments');
        var _a3DocumentsService = abp.services.app.a3Documents;
        var _modalManager;
        var mail = "sgt101055@gmail.com"
        this.init = function (modalManager) {
            _modalManager = modalManager;
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
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _a3DocumentsService.getAllSupportAttachments,
                inputFilter: function () {
                    return {
                        a3IdFilter:56
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
                                text: app.localize('Export RM Sheet'),
                                visible: function () {
                                    return true;
                                },
                                action: function (data) {
                                    exportA3Document(data.record.a3Document);
                                }
                            },
                            {
                                text: app.localize('Load RM Sheet'),
                                visible: function () {
                                    return true;
                                },
                                action: function (data) {
                                    loadA3Document(data.record.a3Document);
                                }
                            },
                            {
                                text: app.localize('Send Mail For Approval'),
                                visible: function (data) {
                                    console.log('email')
                                    return !data.record.a3Document.isApproved && _permissions.confirm && (data.record.a3Document.l4Status == null);
                                },

                                action: function (data) {
                                    abp.notify.info(app.localize('Email Sending Process Started'));
                                    sendrmsheetconfirmationmail(data.record.a3Document)

                                }
                            },
                            {
                                text: app.localize('Support Attachments'),
                                visible: function (data) {
                                    console.log('email')
                                  
                                    return true;
                                },

                               
                            },
                            
                            {
                                text: app.localize('Delete'),
                                visible: function (data) {
                                    return !data.record.a3Document.isApproved && _permissions.delete;
                                },
                                action: function (data) {
                                    deleteA3Document(data.record.a3Document);
                                }
                            }]
                    }
                },
                
                {
                    targets: 1,
                    data: "Supportattachments.a3Id",
                    name: "buyer"
                },
                {
                    targets: 2,
                    data: "Supportattachments.buyer",
                    name: "supplier"
                }
                
                
                
                

            ]
        });

        function loadA3Document(docId) {
            console.log('load Rm Sheet');
            abp.event.trigger('app.dashboardA3Document.LoadA3Document', docId);
            _modalManager.close();
        }

        function sendrmsheetconfirmationmail(doc) {
            console.log(doc);
            _tenantDashboardService
                .sendRMsheetConfirmationEmail({
                    emailAddress: mail,
                    a3Id: doc.id,
                    buyer: doc.buyer,
                    buyerName: doc.buyer,
                    supplier: doc.supplier,
                    supplierName: doc.supplier && doc.supplier.split(' - ').length > 1 ? doc.supplier.split(' - ')[0] : null,
                    templatePath: abp.appPath + 'assets/SampleFiles/A3sheet7New.xlsx',
                    appPath: abp.appPath
                })
                .done(function () {
                    abp.notify.info(app.localize('All Rmsheet Confirmation Email Sent Successfully'));
                    _modalManager.close();
                });
        }

        function exportA3Document(doc) {
            console.log(doc);
            _tenantDashboardService
                .getA3ToExcelFromDoc({
                    a3Id: doc.id,
                    buyer: doc.buyer,
                    buyerName: doc.buyer,
                    supplier: doc.supplier,
                    supplierName: doc.supplier && doc.supplier.split(' - ').length > 1 ? doc.supplier.split(' - ')[0] : null,
                    templatePath: abp.appPath + 'assets/SampleFiles/A3sheet7New.xlsx'
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                    _modalManager.close();
                });
        }

        function approveA3Doc(doc) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _a3DocumentsService.approve({
                            id: doc.id
                        }).done(function () {
                            getA3Documents(true);
                            abp.notify.success(app.localize('SuccessfullyApproved'));
                            abp.event.trigger('app.createOrEditBaseRMRateModalSaved');
                            //abp.event.trigger('app.dashboardFilters.DateRangePicker.OnBuyerChange', $('#BuyerFilterId').val());
                            _modalManager.close();
                        });
                    }
                }
            );
        }

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

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getA3Documents();
            }
        });
    };
})(jQuery);