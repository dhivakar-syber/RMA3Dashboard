(function ($) {
    app.modals.A3DocumentsViewModel = function () {

        var _tenantDashboardService = abp.services.app.tenantDashboard;
        var _$a3DocumentsTable = $('#A3DocumentsTable');
        var _a3DocumentsService = abp.services.app.a3Documents;
        var _modalManager;
        var mail="sgt101055@gmail.com"
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

        var SupportAttachmentModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/A3Documents/ListSupportAttachment',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/A3Documents/_supportAttachments.js',
            modalClass: 'SupportAttachmentsViewModel',
            modalSize: 'modal-xl',
            setBusy: function () { return false; }
        });      


		
		

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
                        yearFilter: $('#YearFilterId').val(),
                        isConfirmedFilter: $("#IsConfirmed").is(':checked')
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
                                    return !data.record.a3Document.isApproved && _permissions.confirm && (data.record.a3Document.l4Status==null) ;
                                },
                                
                                action: function (data) {
                                    abp.notify.info(app.localize('Email Sending Process Started'));
                                    sendrmsheetconfirmationmail(data.record.a3Document)
                                    
                                }
                            },
                            {
                                text: app.localize('Retrigger Mail'),
                                visible: function (data) {
                                    console.log('email')
                                    return !data.record.a3Document.isApproved &&
                                        ((data.record.a3Document.l4Status == 'Awaiting For Approval') ||
                                        (data.record.a3Document.cpStatus == 'Awaiting For Approval') ||
                                        (data.record.a3Document.finStatus == 'Awaiting For Approval') ||
                                        (data.record.a3Document.commadityStatus == 'Awaiting For Approval'));
                                },

                                action: function (data) {
                                    abp.notify.info(app.localize('Email Sending Process Started'));
                                    sendrmsheetRetriggermail(data.record.a3Document)

                                }
                            },
                            {
                                text: app.localize('Resend Email'),
                                visible: function (data) {
                                    console.log('email')
                                    return !data.record.a3Document.isApproved &&
                                        ((data.record.a3Document.l4Status == 'Awaiting For Approval') &&
                                            (data.record.a3Document.cpStatus == 'Awaiting For Approval') &&
                                            (data.record.a3Document.finStatus == 'Awaiting For Approval') &&
                                            (data.record.a3Document.commadityStatus == 'Awaiting For Approval'));
                                },

                                action: function (data) {
                                    abp.notify.info(app.localize('Email Sending Process Started'));
                                    sendrmsheetResendrmail(data.record.a3Document)

                                }
                            },

                            {
                                text: app.localize('Support Attachments'),
                                visible: function (data) {  
                                    console.log('email')
                                    return true;
                                },

                                action: function (data) {


                                    SupportAttachmentModal.open({
                                        a3Id: data.record.a3Document.id,
                                        buyer: data.record.a3Document.buyer,
                                        supplier: data.record.a3Document.supplier,
                                        version: (data.record.a3Document.version).substring(0, (data.record.a3Document.version).lastIndexOf('.'))
                                    });
                                    abp.ui.clearBusy($("body"));

                                }
                            },
                        //{
                        //    text: app.localize('Confirm'),
                        //    visible: function (data) {
                        //        return !data.record.a3Document.isApproved && _permissions.confirm;
                        //    },
                        //    action: function (data) {
                        //        approveA3Doc(data.record.a3Document);
                        //    }
                        //}, 
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
                        data: "a3Document.version",
                        name: "version",
                        render: function (version) {
                            console.log(version);
                            var $span = $("<span/>").addClass("label");
                                $span.addClass("label label-danger-proact label-inline badge badge-light").text(version);
                                return $span[0].outerHTML;

                        },
                        createdCell: function (td, cellData, rowData, row, col) {
                            console.log('createdcell');
                            if (rowData.a3Document.isApproved) {
                                $(td).addClass('note');
                            }
                        }
                    },
					//{
					//	targets: 2,
					//	 data: "a3Document.buyer",
					//	 name: "buyer"   
					//},
					//{
					//	targets: 3,
					//	 data: "a3Document.supplier",
					//	 name: "supplier"   
					//},
					//{
					//	targets: 4,
					//	 data: "a3Document.month",
					//	 name: "month"   
					//},
					{
						targets: 2,
                        data: function(row) {
                            var field1 = (row.a3Document.month);
                            var field2 =(row.a3Document.year);


                            var data = `<span class="field1">${field1}</span><br>`;
                            data += `<span class="field2">${field2}</span><br>`;
                            
                            

                            return data;
                        },
						 name: "monthYear"   
                },
                {
                    targets: 3,
                    data: function (row) {
                        var remarks = row.a3Document.remarks;
                        var words = remarks.split(/\s+/);
                        var wordsPerRow = 3;
                        var rows = [];
                        for (var i = 0; i < words.length; i += wordsPerRow) {
                            rows.push(words.slice(i, i + wordsPerRow).join(" "));
                        }
                        var result = rows.join("<br>");

                        return result;
                    },
                    name: "remarks"
                },
                {
                    targets: 4,
                    data: function (row) {
                        var field1 = (row.a3Document.l4Status ? row.a3Document.l4Status : " ");
                        var field2 = (row.a3Document.cpStatus ? row.a3Document.cpStatus : " ");
                        var field3 = (row.a3Document.finStatus ? row.a3Document.finStatus : " ");
                        var field4 = (row.a3Document.commadityStatus ? row.a3Document.commadityStatus : " ");

                        
                        var data = `Sec Head:<span class="field1">${field1}</span><br>`;
                        data += `CE:<span class="field2">${field2}</span><br>`;
                        data += `F&C:<span class="field3">${field3}</span><br>`;
                        data += `Com Exp:<span class="field4">${field4}</span>`;

                        return data;
                    },
                    name: "ApprovalStatus",
                    createdCell: function (td, cellData, rowData, row, col) {
                        var L4status = rowData.a3Document.l4Status;
                        var cpstatus = rowData.a3Document.cpStatus;
                        var finstatus = rowData.a3Document.finStatus;
                        var commadityExpertstatus = rowData.a3Document.commadityStatus;

                        
                        if (L4status === 'Awaiting For Approval') {
                            $(td).find('.field1').css('color', 'orange');
                        } else if (L4status === 'Approved') {
                            $(td).find('.field1').css('color', 'green');
                        } else if (L4status === 'Rejected') {
                            $(td).find('.field1').css('color', 'red');
                        }

                        if (cpstatus === 'Awaiting For Approval') {
                            $(td).find('.field2').css('color', 'orange');
                        } else if (cpstatus === 'Approved') {
                            $(td).find('.field2').css('color', 'green');
                        } else if (cpstatus === 'Rejected') {
                            $(td).find('.field2').css('color', 'red');
                        }

                        if (finstatus === 'Awaiting For Approval') {
                            $(td).find('.field3').css('color', 'orange');
                        } else if (finstatus === 'Approved') {
                            $(td).find('.field3').css('color', 'green');
                        } else if (finstatus === 'Rejected') {
                            $(td).find('.field3').css('color', 'red');
                        }

                        if (commadityExpertstatus === 'Awaiting For Approval') {
                            $(td).find('.field4').css('color', 'orange');
                        } else if (commadityExpertstatus === 'Approved') {
                            $(td).find('.field4').css('color', 'green');
                        } else if (commadityExpertstatus === 'Rejected') {
                            $(td).find('.field4').css('color', 'red');
                        }
                    },
                    render: function (data) {
                        return data.replace(/<br>/g, "<br/>");
                    }
                }
,

                {
                    targets: 5,
                    data: function (row) {
                        var field1 = (row.a3Document.l4remarks ? row.a3Document.l4remarks : " ");
                        var field2 =(row.a3Document.cpremarks ? row.a3Document.cpremarks : " ");
                        var field3 =(row.a3Document.finremarks ? row.a3Document.finremarks : " ");
                        var field4 =(row.a3Document.commadityExpertremarks ? row.a3Document.commadityExpertremarks : " ");
                        return `Sec Head:${field1}<br>CE:${field2}<br>F&C:${field3}<br>Com Exp:${field4}`;
                    },
                    name: "Remarks",
                    
                    render: function (data) {
                        return data.replace(/<br>/g, "<br/>"); 
                    }
                    
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
        function sendrmsheetRetriggermail(doc) {
            console.log(doc);
            _tenantDashboardService
                .rMsheetRetriggerEmail({
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

        function sendrmsheetResendrmail(doc) {
            console.log(doc);
            _tenantDashboardService
                .rMsheetResendEmail({
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
                    buyerName:doc.buyer,
                    supplier: doc.supplier,
                    supplierName:doc.supplier && doc.supplier.split(' - ').length > 1 ? doc.supplier.split(' - ')[0] : null,
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

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getA3Documents();
		  }
		});
    };
})(jQuery);