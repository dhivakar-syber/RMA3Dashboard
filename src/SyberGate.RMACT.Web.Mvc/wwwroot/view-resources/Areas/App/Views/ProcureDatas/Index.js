(function () {
    $(function () {

        var _$procureDatasTable = $('#ProcureDatasTable');
        var _procureDatasService = abp.services.app.procureDatas;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.ProcureDatas.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.ProcureDatas.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.ProcureDatas.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/ProcureDatas/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ProcureDatas/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditProcureDataModal'
        });       

		 var _viewProcureDataModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/ProcureDatas/ViewprocureDataModal',
            modalClass: 'ViewProcureDataModal'
        });

		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }

        var dataTable = _$procureDatasTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _procureDatasService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ProcureDatasTableFilter').val(),
					partNoFilter: $('#PartNoFilterId').val(),
					descriptionFilter: $('#DescriptionFilterId').val(),
					supplierCodeFilter: $('#SupplierCodeFilterId').val(),
					suppliserNameFilter: $('#SuppliserNameFilterId').val(),
					minCurrentExwPriceFilter: $('#MinCurrentExwPriceFilterId').val(),
					maxCurrentExwPriceFilter: $('#MaxCurrentExwPriceFilterId').val(),
					priceCurrencyFilter: $('#PriceCurrencyFilterId').val(),
					uomFilter: $('#UomFilterId').val(),
					buyerFilter: $('#BuyerFilterId').val(),
					minFromDateFilter:  getDateFilter($('#MinFromDateFilterId')),
					maxFromDateFilter:  getDateFilter($('#MaxFromDateFilterId')),
					minToDateFilter:  getDateFilter($('#MinToDateFilterId')),
					maxToDateFilter:  getDateFilter($('#MaxToDateFilterId')),
					minPackagingCostFilter: $('#MinPackagingCostFilterId').val(),
					maxPackagingCostFilter: $('#MaxPackagingCostFilterId').val(),
					minLogisticsCostFilter: $('#MinLogisticsCostFilterId').val(),
					maxLogisticsCostFilter: $('#MaxLogisticsCostFilterId').val(),
					plantCodeFilter: $('#PlantCodeFilterId').val(),
					plantDescriptionFilter: $('#PlantDescriptionFilterId').val(),
					contractNoFilter: $('#ContractNoFilterId').val(),
					minSOBFilter: $('#MinSOBFilterId').val(),
					maxSOBFilter: $('#MaxSOBFilterId').val(),
					minEPUFilter: $('#MinEPUFilterId').val(),
					maxEPUFilter: $('#MaxEPUFilterId').val(),
					statusFilter: $('#StatusFilterId').val(),
					minCreationTimeFilter:  getDateFilter($('#MinCreationTimeFilterId')),
					maxCreationTimeFilter:  getDateFilter($('#MaxCreationTimeFilterId')),
					minDeletionTimeFilter:  getDateFilter($('#MinDeletionTimeFilterId')),
					maxDeletionTimeFilter:  getDateFilter($('#MaxDeletionTimeFilterId')),
					isDeletedFilter: $('#IsDeletedFilterId').val(),
					minLastModificationTimeFilter:  getDateFilter($('#MinLastModificationTimeFilterId')),
					maxLastModificationTimeFilter:  getDateFilter($('#MaxLastModificationTimeFilterId'))
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
                                    _viewProcureDataModal.open({ id: data.record.procureData.id });
                                }
                        },
						{
                            text: '',
                        }]
                    }
                },
					{
						targets: 1,
						 data: "procureData.partNo",
						 name: "partNo"   
					},
					{
						targets: 2,
						 data: "procureData.description",
						 name: "description"   
					},
					{
						targets: 3,
						 data: "procureData.supplierCode",
						 name: "supplierCode"   
					},
					{
						targets: 4,
						 data: "procureData.suppliserName",
						 name: "suppliserName"   
					},
					{
						targets: 5,
						 data: "procureData.currentExwPrice",
						 name: "currentExwPrice"   
					},
					{
						targets: 6,
						 data: "procureData.priceCurrency",
						 name: "priceCurrency"   
					},
					{
						targets: 7,
						 data: "procureData.uom",
						 name: "uom"   
					},
					{
						targets: 8,
						 data: "procureData.buyer",
						 name: "buyer"   
					},
					{
						targets: 9,
						 data: "procureData.fromDate",
						 name: "fromDate" ,
					render: function (fromDate) {
						if (fromDate) {
							return moment(fromDate).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 10,
						 data: "procureData.toDate",
						 name: "toDate" ,
					render: function (toDate) {
						if (toDate) {
							return moment(toDate).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 11,
						 data: "procureData.packagingCost",
						 name: "packagingCost"   
					},
					{
						targets: 12,
						 data: "procureData.logisticsCost",
						 name: "logisticsCost"   
					},
					{
						targets: 13,
						 data: "procureData.plantCode",
						 name: "plantCode"   
					},
					{
						targets: 14,
						 data: "procureData.plantDescription",
						 name: "plantDescription"   
					},
					{
						targets: 15,
						 data: "procureData.contractNo",
						 name: "contractNo"   
					},
					{
						targets: 16,
						 data: "procureData.sob",
						 name: "sob"   
					},
					{
						targets: 17,
						 data: "procureData.epu",
						 name: "epu"   
					},
					{
						targets: 18,
						 data: "procureData.status",
						 name: "status"   
					},
					{
						targets: 19,
						 data: "procureData.creationTime",
						 name: "creationTime" ,
					render: function (creationTime) {
						if (creationTime) {
							return moment(creationTime).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 20,
						 data: "procureData.deletionTime",
						 name: "deletionTime" ,
					render: function (deletionTime) {
						if (deletionTime) {
							return moment(deletionTime).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 21,
						 data: "procureData.isDeleted",
						 name: "isDeleted"  ,
						render: function (isDeleted) {
							if (isDeleted) {
								return '<div class="text-center"><i class="fa fa-check kt--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 22,
						 data: "procureData.lastModificationTime",
						 name: "lastModificationTime" ,
					render: function (lastModificationTime) {
						if (lastModificationTime) {
							return moment(lastModificationTime).format('L');
						}
						return "";
					}
			  
					}
            ]
        });

        function getProcureDatas() {
            dataTable.ajax.reload();
        }

        function deleteProcureData(procureData) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _procureDatasService.delete({
                            id: procureData.id
                        }).done(function () {
                            getProcureDatas(true);
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

        $('#CreateNewProcureDataButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditProcureDataModalSaved', function () {
            getProcureDatas();
        });

		$('#GetProcureDatasButton').click(function (e) {
            e.preventDefault();
            getProcureDatas();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getProcureDatas();
		  }
		});
    });
})();