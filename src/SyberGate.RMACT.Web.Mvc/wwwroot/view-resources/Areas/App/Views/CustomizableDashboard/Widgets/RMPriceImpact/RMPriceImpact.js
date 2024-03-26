$(function () {
    console.log('test');
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();

    var _$Container = $('.RMPriceImpactContainer');
    var _$refreshRMPriceImpactButton = _$Container.find('.refreshRMPriceImpactButton');
    var _$updateRMCostConversionButton = _$Container.find('#updateRMCostConversionButton');
    var _buyer = $('#buyer1');
    var _supplier = $('#supplier1');
    var _dtrange = $('#dtrange1');
    
    var _grade = $('#grade1');
    var _spec = $('#spec1');
    var _plant = $('#plant1');
    var _hasMixture = false;

    var _$Buyer;
    var _$Supplier;
    var _$dateRange;
    var _$Grade;
    var _$Group;
    var _$docId;
    var _$Plant;

    var _partBucketViewModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/PartBuckets/PartBucketViewModalData',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/PartBuckets/_PartBucketViewModal.js',
        modalClass: 'PartBucketViewModal',
        modalSize: 'modal-xl'
    });

    var _priceImpactTale = null;

    var getHasMixture = function () {
        _tenantDashboardService
            .getPartHasMixture({
            })
            .done(function (result) {
                _hasMixture = result.partHasMixture;
            });
    }

    var getA3RMPriceImpact = function (id) {
        if (id) {
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getA3RMPriceImpact({
                    a3Id: id
                })
                .done(function (result) {
                    loadPriceImpact(result);
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        }
    }

    var updateRMCostConversion = function () {

        const numbers = [];

        _plant.text().split("|").forEach((element, index) => {
            if (index % 2 !== 0) {
                numbers.push(element);
            }
        });
        const concatenatedString = numbers.join(',');

        if (_$dateRange && _$Buyer && _$Supplier) {
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .updateRMCostConversion({
                    buyer: _$Buyer,
                    supplier: _$Supplier,
                    period: _$dateRange,
                    isGenerateA3: false,
                    group: _$Group,
                //    ,
                //    plant: concatenatedString
                })
                .done(function (result) {
                    refreshRMPriceImpact();
                    $('#EditableCheckboxRmbreakup').prop("checked", false);
                    
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        }
    }
    

    var refreshRMPriceImpact = function () {

        const numbers = [];

        _plant.text().split("|").forEach((element, index) => {
            if (index % 2 !== 0) {
                numbers.push (element);
            }
        });
        const concatenatedString = numbers.join(',');

        if (_$dateRange && _$Buyer) {
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getSupplierRMPriceImpact({
                    buyer: _$Buyer,
                    buyerName: _buyer.text(),
                    supplier: _$Supplier,
                    supplierName: _supplier.text() && _supplier.text().split(' - ').length > 1 ? _supplier.text().split(' - ')[0] : null,
                    period: _$dateRange,
                    isGenerateA3: false,
                    spec: _$Group,
                    grade:_$Grade,
                    /*plant: _plant.text() && _plant.text().split(' - ').length > 1 ? _plant.text().split(' - ')[0] : null*/
                    plant: concatenatedString

                })
                .done(function (result) {
                    $('#EditableCheckboxRmbreakup').prop("checked", false);
                    loadPriceImpact(result);
                    _$docId = 0;
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        }
    };

    var loadPriceImpact = function (result) {
        console.log('test');
        for (var i = 0; i < _$Container.length; i++) {
            var container = $(_$Container[i]);
            var $tableBody = container.find('#RMPriceImpact_Content table tbody');
            var currentAVOB = 0;
            var revisedAVOB = 0;
            var priceImpact = 0;
            var processImpact = 0;
            var RmImpactt = 0;

            $tableBody.html('');
            for (var rowIndex = 0; rowIndex < result.priceImpacts.length; rowIndex++) {
                var price = result.priceImpacts[rowIndex];


                if (price["subPart"] == 1) {
                    price["otherCost"] = "";

                }

                else if (price["isParentSubMixture"] == 1) {
                        price["otherCost"] = "";
                        price["partNo"] = "";
                        price["description"] = "";
                }

                else if (price["subMixture"] == 1) {
                    price["otherCost"] = "";
                    price["partNo"] = "";
                    price["description"] = "";
                }

                else {
                    price["otherCost"] = parseFloat(price["otherCost"]).toFixed(2);
                    currentAVOB += parseFloat(price["currentAVOB"]);
                    revisedAVOB += parseFloat(price["revisedAVOB"]);
                    priceImpact += parseFloat(price["rmImpact"]);
                    processImpact += parseFloat(price["processImpact"]);
                    RmImpactt += parseFloat(price["rmImpactt"]);


                }


                var cls = (price["isParent"]) ? "bg-success" : "";
                var but = (price["isParent"]) ? '<button data-rm-pplant="' + price["plantCode"] + '"data-rm-ppart="' + price["partNo"] + '" class= "btn btn-primary btn-sm btn-expand"><i class=" p-0 m-0 fa fa-chevron-circle-down"></i>' + price["slNo"] + '</button>' : price["slNo"];

                //var butcostmodel = '<button data-rm-ppart="' + price["partNo"] + '"data-rm-prm="' + price["rawMaterialGrade"] + '"data-rm-pbaseprice="' + (price["currentCostPer"]).toFixed(2) + '"data-rm-ptype="' + true + '"  class= "btn btn-primary btn-sm btn-expandcost"><i class=" p-0 m-0 fa fa-external-link-square-alt"></i></button>';
                var butcostmodel = '<span data-rm-ppart="' + price["partNo"] + '"data-rm-prm="' + price["rawMaterialGrade"] + '"data-rm-pbaseprice="' + (price["currentCostPer"]).toFixed(2) + '"data-rm-ptype="' + true + '"  class= "btn btn-expandcost" style="width: max-content; display: inline-block">';
                var butcostmodel1 = '<i class=" p-1 m-0 fa fa-bolt"></i></span >';
                var butrevisemodel = '<span data-rm-ppart="' + price["partNo"] + '"data-rm-prm="' + price["rawMaterialGrade"] + '"data-rm-pbaseprice="' + (price["revisedCostPer"]).toFixed(2) + '"data-rm-ptype="' + false + '"  class= "btn btn-expandcost" style="width: max-content; display: inline-block">';
                var butrevisemodel1 = '<i class=" p-1 m-0 fa fa-bolt"></i></span >';
                var $tr = $('<tr class="cls' + price["partNo"] + '-' + price["plantCode"] + '"></tr>').append(
                    /*$('<td>' + but + '</td>'),*/
                    $('<td>' + price["slNo"] +'</td>'),
                    $('<td class="dtfc-fixed-left bg-success" style="left: 0px; position: sticky;background-color: skyblue; " >' + price["partNo"] + '</td>'),
                    /*$('<td style="background-color: skyblue;">' + price["description"] + '</td>'),*/
                    $('<td >' + price["description"] + '</td>'),
                    $('<td>' + price["plantCode"] + '</td>'),
                    $('<td>' + price["eS1"] + '</td>'),
                    $('<td>' + price["eS2"] + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : price["rawMaterialGroup"]) +'</td>'),
                    $('<td> ' + (price.isParentSubMixture ? " " : price["rawMaterialGrade"]) + '</td>'),

                    $('<td>'  + (price.isParentSubMixture ? " " : (price["currentCostPer"]).toFixed(2)) +'</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["baseRMRate"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["rmSurchargeGradeDiff"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["secondaryProcessing"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["surfaceProtection"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["thickness"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["cuttingCost"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["moqVolume"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["transport"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["others"]).toFixed(2) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : (price["revisedCostPer"]).toFixed(2)) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["revBaseRMRate"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["revRMSurchargeGradeDiff"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["revSecondaryProcessing"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["revSurfaceProtection"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["revThickness"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["revCuttingCost"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["revMOQVolume"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["revTransport"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["revOthers"]).toFixed(2) + '</td>'),

                    //$('<td>' + price["conversionCost"] + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["grossInputWeight"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["castingForgingWeight"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["finishedWeight"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : price["scrapRecovery"]) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["scrapRecoveryPercent"] * 100).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["scrapWeight"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["currentRMCost"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["revisedRMCost"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : price["otherCost"])+ '</td>'),
                    $('<td class="danger1-rmact">' + (price.isParentSubMixture ? " " : parseFloat(price["currentExwPrice"]).toFixed(2)) + '</td>'),
                    $('<td class="danger2-rmact">' + (price.isParentSubMixture ? " " : parseFloat(price["revisedExwPrice"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["exwPriceChangeInCost"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["exwPriceChangeInPer"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["packagingCost"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["logisticsCost"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["currentFCAPrice"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["revisedFCAPrice"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["currentAVOB"]).toFixed(2)) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["revisedAVOB"]).toFixed(2)) + '</td>'),

                    $('<td>' + (price.isParentSubMixture ? " " : price["sob"]) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : price["globusEPU"]) + '</td>'),
                    $('<td class="danger1-rmact">' + (price.isParentSubMixture ? " " : price["rmImpact"]) + '</td>'),
                    $('<td>' + (price.isParentSubMixture ? " " : price["rmReference"]) + '</td>'),
                    $('<td class="bucket-color">' + (price.isParentSubMixture ? " " : price["rmImpactt"])+'</td>'),
                    $('<td class="bucket-color">' + (price.isParentSubMixture ? " " : price["processImpact"]) +'</td>')

                );
                $tableBody.append($tr);
                $(".bucket-color").hide();
                $(".p-3-4").hide();
            }


            var $tr = $('<tr></tr>').append(
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td class="bucket-color2"></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td></td>'),
                $('<td> ' + parseFloat(currentAVOB).toFixed(2) + '</td>'),
                $('<td> ' + parseFloat(revisedAVOB).toFixed(2) + '</td>'),
                $('<td></td>'),
                
                $('<td></td>'),
                $('<td class="danger1-rmact"> ' + parseFloat(priceImpact).toFixed(6) + ' </td>'),
                $('<td></td>'),
                $('<td class="bucket-color2">' + parseFloat(RmImpactt).toFixed(6) + '</td>'),
                $('<td class="bucket-color2">' + parseFloat(processImpact).toFixed(6) + '</td>'),
              
            );

            

            $tableBody.append($tr);
            $(".bucket-color2").hide();
            $(".btn-expand").click(function (e) {
                console.log(e);
                $(this).removeClass('btn-expand');
                $(this).addClass('btn-collapse');
                var id = $(this).attr('data-rm-ppart');
                var plant = $(this).attr('data-rm-pplant');
                if (_$docId && _$docId > 0)
                    getSubPartImpact(id, plant)
                else
                    refreshSubPartImpact(id, plant);

            });


            

            $('#EditableCheckboxRmbreakup').change(function () {
                if ($(this).is(":checked")) {
                    $(".bucket-color").fadeIn(500);
                    $(".p-3-4").show();
                    $(".bucket-color2").show();
                }
                else {
                    $(".bucket-color").hide().animate();
                    $(".p-3-4").hide().animate();
                    $(".bucket-color2").hide().animate();
                }
            });



            $(".btn-collapse").click(function (e) {
                $(this).addClass('btn-expand');
                $(this).removeClass('btn-collapse');
                $($tableBody.find('.cls-' + result.priceImpacts[0].parentPartNo + '-sub')).hide();
            });
            $(".btn-expand .fa").on("click", function (e) {
                $(this).toggleClass("fa-chevron-circle-up");
            });
            $(".btn-collapse .fa").on("click", function (e) {
                $(this).toggleClass("fa-chevron-circle-down");

            });
            
            $(".btn-expandcost").click(function () {
                console.log('test');
                _partBucketViewModal.open({
                    buyerid: _buyer.text(),
                    supplierid: _supplier.text() && _supplier.text().split(' - ').length > 1 ? _supplier.text().split(' - ')[0] : null,
                    part: $(this).attr('data-rm-ppart'),
                    rm: $(this).attr('data-rm-prm'),
                    price: $(this).attr('data-rm-pbaseprice'),
                    type: $(this).attr('data-rm-ptype')                
                })
            });

        }

    }


    var getSubPartImpact = function (partno, plant) {
        if (_$docId && _$docId > 0) {
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getA3SubPartRMPriceImpact({
                    buyer: _$Buyer,
                    supplier: _$Supplier,
                    period: _$dateRange,
                    partno: partno,
                    a3Id: _$docId,
                    isGenerateA3: false
                })
                .done(function (result) {
                    loadSubPartDetails(result, partno, plant);
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        }
    }

    var refreshSubPartImpact = function (partno, plant) {
        if (_$dateRange && _$Buyer) {
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getSubPartRMPriceImpact({
                    buyer: _$Buyer,
                    buyerName: _$Buyer.buyerName,
                    supplier: _$Supplier,
                    supplierName: _$Supplier.supplierName,
                    period: _$dateRange,
                    partno: partno,
                    isGenerateA3: false,
                    plant: plant
                })
                .done(function (result) {
                    loadSubPartDetails(result, partno, plant);
                    $(".bucket-color").hide();
                    $('#EditableCheckboxRmbreakup').prop("checked", false);

                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        }
    }

    var loadSubPartDetails = function (result, partno, plant) {
        var _$RowContainer = $('.cls' + partno + '-' + plant);
        var $tableBody = _$RowContainer.closest('table');
        $($tableBody.find('.cls-' + result.priceImpacts[0].parentPartNo + '-sub')).remove();
        for (var i = 0; i < _$RowContainer.length; i++) {
            for (var rowIndex = result.priceImpacts.length - 1; rowIndex >= 0; rowIndex--) {
                var price = result.priceImpacts[rowIndex];
                var cls = (price["isParent"]) ? "bg-success" : "";
                var but = (price["isParent"]) ? '<button data-rm-ppart="' + price["partNo"] + '" class= "btn btn-primary btn-sm btn-expand"><i class=" p-0 m-0 fa fa-expand"></i>' + price["slNo"] + '</button>' : price["slNo"];
                var $tr = $('<tr class="cls-' + price['parentPartNo'] + '-sub cls' + price["partNo"] + '-sub' + '"></tr>').append(
                    /*$('<td>' + but + '</td>'),*/

                    $('<td>' + price["slNo"] +'</td>'),
                    $('<td>' + price["partNo"] + '</td>'),
                    $('<td>' + price["description"] + '</td>'),
                    $('<td></td>'),
                    $('<td></td>'),
                    $('<td></td>'),
                    $('<td>'  + price["rawMaterialGroup"] + '</td>'),
                    $('<td> ' + price["rawMaterialGrade"] +'</td>'),
                    $('<td>' + (price["currentCostPer"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["baseRMRate"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["rmSurchargeGradeDiff"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["secondaryProcessing"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["surfaceProtection"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["thickness"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["cuttingCost"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["moqVolume"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["transport"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["others"]).toFixed(2) + '</td>'),
                    $('<td>' + (price["revisedCostPer"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["baseRMRate"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["rmSurchargeGradeDiff"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["secondaryProcessing"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["surfaceProtection"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["thickness"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["cuttingCost"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["moqVolume"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["transport"]).toFixed(2) + '</td>'),
                    $('<td class="bucket-color">' + parseFloat(price["others"]).toFixed(2) + '</td>'),
                    $('<td>' + price["grossInputWeight"] + '</td>'),
                    $('<td>' + price["castingForgingWeight"] + '</td>'),
                    $('<td>' + price["finishedWeight"] + '</td>'),
                    $('<td>' + price["scrapRecovery"] + '</td>'),
                    $('<td>' + parseFloat(price["scrapRecoveryPercent"] * 100).toFixed(2) + '</td>'),
                    $('<td>' + price["scrapWeight"] + '</td>'),
                    $('<td>' + price["currentRMCost"] + '</td>'),
                    $('<td>' + price["revisedRMCost"] + '</td>'),
                    $('<td>' + price["otherCost"] + '</td>'),
                    $('<td class="danger1-rmact">' + parseFloat(price["currentExwPrice"]).toFixed(2)  + '</td>'),
                    $('<td class="danger2-rmact">' + parseFloat(price["revisedExwPrice"]).toFixed(2) + '</td>'),
                    $('<td>' + parseFloat(price["exwPriceChangeInCost"]).toFixed(2) + '</td>'),
                    $('<td>' + parseFloat(price["exwPriceChangeInPer"]).toFixed(2) + '</td>'),
                    $('<td>' + parseFloat(price["packagingCost"]).toFixed(2) + '</td>'),
                    $('<td>' + parseFloat(price["logisticsCost"]).toFixed(2) + '</td>'),
                    $('<td>' + parseFloat(price["currentFCAPrice"]).toFixed(2) + '</td>'),
                    $('<td>' + parseFloat(price["revisedFCAPrice"]).toFixed(2) + '</td>'),
                    $('<td>' + parseFloat(price["currentAVOB"]).toFixed(2) + '</td>'),
                    $('<td>' + parseFloat(price["revisedAVOB"]).toFixed(2) + '</td>'),

                    $('<td>' + price["sob"] + '</td>'),
                    $('<td>' + price["globusEPU"] + '</td>'),
                    $('<td class="danger1-rmact">' + price["rmImpact"] + '</td>'),
                    $('<td>' + price["rmReference"] + '</td>'),
                    $('<td class="bucket-color"></td>'),
                    $('<td class="bucket-color"></td>')




                );
                //$tableBody.append($tr);
                $(".bucket-color").hide();
                _$RowContainer.after($tr)
                
            }
        }
    }

    _$refreshRMPriceImpactButton.click(function () {
        //getImpactHasMixture();
        refreshRMPriceImpact();
    });

    _$updateRMCostConversionButton.click(function () {
        updateRMCostConversion();
    });

    //getHasMixture();
    refreshRMPriceImpact();

    abp.event.on('app.dashboardFilters.DateRangePicker.OnDateChange', function (_selectedDates) {
        _dtrange.text(_selectedDates.split('-')[0]);
        _$dateRange = _selectedDates;
        _widgetBase.runDelayed(function () { refreshRMPriceImpact() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnSupplierChange', function (_selectedSupplier) {
        _supplier.text(_selectedSupplier.supplierName);
        _$Supplier = _selectedSupplier.supplierId;
        _widgetBase.runDelayed(function () { refreshRMPriceImpact() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnBuyerChange', function (_selectedBuyer) {
        _buyer.text(_selectedBuyer.buyerName);
        _$Buyer = _selectedBuyer.buyerId;
        _widgetBase.runDelayed(function () { refreshRMPriceImpact() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnGradeChange', function (_selectedGrade) {
        _grade.text(_selectedGrade.gradeName);
        
        _$Grade = _selectedGrade.gradeId;
        _widgetBase.runDelayed(function () { refreshRMPriceImpact() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnGroupChange', function (_selectedGroup) {
        _spec.text(_selectedGroup.groupName);
        _$Group = _selectedGroup.groupId;
        _widgetBase.runDelayed(function () { refreshRMPriceImpact() });
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnPlantChange', function (_selectedPlant) {
        _plant.text(_selectedPlant.plantName);
        _$Plant = _selectedPlant.plantId;
        _widgetBase.runDelayed(function () { refreshRMPriceImpact() });
    });

    abp.event.on('app.dashboardA3Document.LoadA3Document', function (doc) {
        var DocId = doc.id;
        _$docId = doc.id;
        _supplier.text(doc.supplier);
        _buyer.text(doc.buyer);
        _widgetBase.runDelayed(function () { getA3RMPriceImpact(DocId) });
    });

    abp.event.on('app.createOrEditBaseRMRateModalSaved', function () {
        _widgetBase.runDelayed(function () { getA3RMPriceImpact() });
    });

});