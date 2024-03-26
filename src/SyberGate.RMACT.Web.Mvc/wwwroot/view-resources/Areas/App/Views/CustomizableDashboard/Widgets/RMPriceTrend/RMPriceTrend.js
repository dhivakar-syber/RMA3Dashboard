/// <reference path="../../../basermrates/_basermhistorymodal.js" />
$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _a3DocumentsService = abp.services.app.a3Documents;
    var _widgetBase = app.widgetBase.create();
    var _rmPriceTrendTable = $('#RMPriceTrendTable');
    var _userService = abp.services.app.user;

    var _$Container = $('.RMPriceTrendContainer');
    var _buyer = $('#buyer');
    var _supplier = $('#supplier');
    var _dtrange = $('#dtrange');
    var _spec = $('#spec2');
    var _grade = $('#grade2');
    var _plant = $('#plant');

    var _$Buyer;
    var _$Supplier;
    var _$Spec;
    var _$Grade;
    var _$Plant;
    var _$dateRange;
    var _hasMixture = false;
    //var _$remarksModal = $('#remarksModal').modal();
    //_$remarksModal.hide();

    var _$filterModal = $('#filterModal').modal();
    var _createOrEditModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/BaseRMRates/CreateOrEditModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BaseRMRates/_CreateOrEditModal.js',
        modalClass: 'CreateOrEditBaseRMRateModal'
    });

    var _viewHistoryModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/BaseRMRates/BaseRMHistoryModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BaseRMRates/_BaseRMHistoryModal.js',
        modalClass: 'BaseRMHistoryModal',
        modalSize: 'modal-xl'
    });

    var _editRMModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/BaseRMRates/BaseRMEditModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BaseRMRates/_BaseRMEditModal.js',
        modalClass: 'BaseRMEditModal',
        modalSize: 'modal-xl'
    });

    var _listA3Modal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/A3Documents/ListA3Documents',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/A3Documents/_ListA3.js',
        modalClass: 'A3DocumentsViewModel',
        modalSize: 'modal-xl a3listsize'
    });

    var getHasMixture = function () {
        _tenantDashboardService
            .getPartHasMixture({
                buyer: _$Buyer,
                supplier: _$Supplier,
            })
            .done(function (result) {
                _hasMixture = result.partHasMixture;
            });
    }

    var generateA3 = function (remarks) {
        console.log(_$dateRange)
        if (_$Buyer && _$dateRange && _$Supplier) {
            abp.ui.setBusy();
            _a3DocumentsService.createOrEdit({
                buyerId: _$Buyer,
                buyer: _buyer.text(),
                supplierId: _$Supplier,
                supplier: _supplier.text(),
                month: moment(_$dateRange.split('-')[0]).format('MMM'),
                year: moment(_$dateRange.split('-')[0]).format('YYYY'),
                period: _$dateRange,
                groupId: _$Spec,
                //group: _group.text(),
                grade:_$Grade,
                remarks: remarks
            }).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                abp.event.trigger('app.createOrEditA3DocumentModalSaved');
            }).always(function () {
                abp.ui.clearBusy();
            });
        }
    };

    var getA3RMPriceTrend = function (id) {
        if (id) {
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getA3RMPriceTrend({
                    a3Id: id
                })
                .done(function (result) {
                    loadRMPriceTrend(result);
                })
                .always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        }
    };

    var refreshRMPriceTrend = function () {
        console.log(_$dateRange)
        if (_$dateRange && _$Buyer && _$Supplier) {
            getHasMixture();
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .getSupplierRMPriceTrend({
                        buyer: _$Buyer,
                        supplier: _$Supplier,
                        period: _$dateRange,
                        isGenerateA3: false,
                        spec: _$Spec,
                        grade: _$Grade,
                        plant: _$Plant
                    })
                    .done(function (result) {
                        loadRMPriceTrend(result);
                    })
                    .always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
        }
    };

    var loadRMPriceTrend = function (result) {

        console.log('RmpriceTrend');
        for (var i = 0; i < _$Container.length; i++) {
            var container = $(_$Container[i]);
            var $tableBody = container.find('#RMPriceTrend_Content table tbody');
            //var datatable = _rmPriceTrendTable.DataTable({
            //    paging: false,
            //    serverSide: false,
            //    processing: true,
            //    sorting: false,
            //});
            //datatable.draw(false);
            $tableBody.html('');
            for (var rowIndex = 0; rowIndex < result.priceTrends.length; rowIndex++) {
                var price = result.priceTrends[rowIndex];
                var $tr = $('<tr></tr>').append(
                    $('<td><div class=""><button data-rm-groupname="' + price["rmGrade"] + '" data-rm-historyid="' + price["rmGroupId"] + '" class= "btn btn-primary btn-sm btn-historyrm"><i class=" p-0 m-0 fa fa-history"></i></button></div></td> '),
                    $('<td>' + price["parentGrp"] + '</td>'),

                    $('<td>' + price["mixtureGrade"] + '</td>'),
                    $('<td>' + price["rmGrade"] + '</td>'),
                    $('<td>' + price["uom"] + '</td>'),
                    $('<td class= "' + (price["setApproved"] ? 'note' : '') + '">' + price["setteledMY"] + '</td>'),
                    $('<td class="bg-success">' + parseFloat(price["setteledUR"]).toFixed(2) + '</td>'),
                    $('<td>' + price["setteledWRatio"] + '</td>'),
                    $('<td>' + price["setteledLRatio"] + '</td>'),
                    $('<td class= "' + (price["revApproved"] ? 'note' : '') + '">' + price["revisedMY"] + '</td>'),
                    $('<td class="bg-success">' + parseFloat(price["revisedUR"]).toFixed(2) + '</td>'),
                    $('<td>' + price["revisedWRatio"] + '</td>'),
                    $('<td>' + price["revisedLRatio"] + '</td>'),
                    $('<td>' + price["baseRMPOC"] + '</td>'),
                    $('<td class="bg-success">' + parseFloat(price["scrapSetteled"]).toFixed(2) + '</td>'),
                    $('<td class="bg-success">' + parseFloat(price["scrapRevised"]).toFixed(2) + '</td>'),
                    $('<td>' + price["scrapPOC"] + '</td>'),
                    $('<td>' + (price["revIndexName"] != "" ? 'Yes' : 'No') + '</td>'),
                    $('<td>' + price["revIndexName"] + '</td>'),
                    $('<td>' + (price["revIndexValue"] != "" ? price["revIndexValue"] : "") + '</td>'),
                );
                $tableBody.append($tr);
            }
            $(".btn-reviserm").click(function (e) {
                console.log(e);
                var id = $(this).attr('data-rm-trendid');
                _createOrEditModal.open({ id: id, isrevision: true })
            });
            $(".btn-historyrm").click(function (e) {
                console.log(e);
                var id = $(this).attr('data-rm-historyid');
                var rm = $(this).attr('data-rm-groupname');
                _viewHistoryModal.open({ rmid: id, rm: rm, buyerid: _$Buyer, buyer: _buyer.text(), supplierid: _$Supplier, supplier: _supplier.text() })
            });
            

            //datatable.draw(false);
            //if (_hasMixture) {
            //    datatable.column(13).visible(false);
            //    datatable.column(14).visible(false);
            //    datatable.column(15).visible(false);
            //}
        }

        
    };

    $("#btn-generate-a3").click(function () {
        $('#a3-remarks').val('');
        $('#remarksModal').show();
    });
    //$("#btn-config-a3").click(function () {
    //    $('#a3-config').val('');
    //    $('#configModal').show();
    //});

    $("#btn-list-a3").click(function () {
        _listA3Modal.open({ buyer: _buyer.text(),supplier:_supplier.text()});
    });

    $(".refreshRMPriceTrendButton").click(function () {
        refreshRMPriceTrend();
    });

    $(".editRMPriceTrendButton").click(function () {
        _editRMModal.open({ buyerid: _$Buyer, buyer: _buyer.text(), supplierid: _$Supplier, supplier: _supplier.text(), period: _$dateRange, rmgroupid: _$Spec})
    });

    $('#btn-export-a3').click(function () {
        const numbers = [];

        _plant.text().split("|").forEach((element, index) => {
            if (index % 2 !== 0) {
                numbers.push(element);
            }
        });
        const concatenatedString = numbers.join(',');
        abp.ui.setBusy(_$Container);
        _tenantDashboardService
            .getA3ToExcel({
                buyer: _$Buyer,
                supplier: _$Supplier,
                period: _$dateRange,
                buyerName: _buyer.text(),
                supplierName: _supplier.text() && _supplier.text().split(' - ').length > 1 ? _supplier.text().split(' - ')[0] : null,
                isGenerateA3: false,
                templatePath: abp.appPath + 'assets/SampleFiles/A3sheet7New.xlsx',
                group: _$Spec,
                grade: _$Grade,
                groupName: _spec.text(),
                plant: concatenatedString,
                plantName: _plant.text()
            })
            .done(function (result) {
                app.downloadTempFile(result);
            })
            .always(function () {
                abp.ui.clearBusy(_$Container);
            });
    });

    getHasMixture();

    refreshRMPriceTrend();

    //_$filterModal.show();

    //_$remarksModal.hide();

    //_$remarksModal.on('shown.bs.modal', function (event) {
    //    _$remarksModal.hide();
    //    return event.preventDefault() // stops modal from being shown
    //})

    $('#remarksModal .save-button').click(function (e) {
        var remarks = $('#a3-remarks').val();
        generateA3(remarks);
        $('#remarksModal .close-button').click();
        _listA3Modal.open({ buyer: _buyer.text() });
    });

    //$('#configModal .save-button').click(function (e) {
    //    var l4mail = $('#a3-l4-config').val();
    //    var cpmail = $('#a3-cp-config').val();
    //    var finmail = $('#a3-fin-config').val();
    //    _userService.configupdate(
    //        abp.session.userId,
    //        l4mail,
    //        cpmail,
    //         finmail
    //    ).done(function () {
    //        abp.notify.info(app.localize('User Details Saved Successfully'));
    //       // _modalManager.close();
    //        //abp.event.trigger('app.createOrEditUserModalSaved');
    //    })
        
    //});
    
    abp.event.on('app.dashboardFilters.DateRangePicker.OnDateChange', function (_selectedDates) {
        _dtrange.text(_selectedDates.split('-')[0]);
        _$dateRange = _selectedDates;
        _widgetBase.runDelayed(function () { refreshRMPriceTrend() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnSupplierChange', function (_selectedSupplier) {
        _supplier.text(_selectedSupplier.supplierName);
        _$Supplier = _selectedSupplier.supplierId;
        _widgetBase.runDelayed(function () { refreshRMPriceTrend() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnBuyerChange', function (_selectedBuyer) {
        _buyer.text(_selectedBuyer.buyerName);
        _$Buyer = _selectedBuyer.buyerId;
        _widgetBase.runDelayed(function () { refreshRMPriceTrend() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnGradeChange', function (_selectedGrade) {
        _grade.text(_selectedGrade.gradeName);
        _$Grade = _selectedGrade.gradeId;
        _widgetBase.runDelayed(function () { refreshRMPriceTrend() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnGroupChange', function (_selectedGroup) {
        _spec.text(_selectedGroup.groupName);
        _$Spec = _selectedGroup.groupId;
        _widgetBase.runDelayed(function () { refreshRMPriceTrend() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnPlantChange', function (_selectedPlant) {
        _plant.text(_selectedPlant.plantName);
        _$Plant = _selectedPlant.plantId;
        _widgetBase.runDelayed(function () { refreshRMPriceTrend() });
    });

    abp.event.on('app.dashboardA3Document.LoadA3Document', function (doc) {
        var a3Doc = doc;
        _supplier.text(doc.supplier);
        _buyer.text(doc.buyer);
        _widgetBase.runDelayed(function () { getA3RMPriceTrend(a3Doc.id) });
    });

    abp.event.on('app.createOrEditBaseRMRateModalSaved', function () {
        _widgetBase.runDelayed(function () { refreshRMPriceTrend() });
    });

});