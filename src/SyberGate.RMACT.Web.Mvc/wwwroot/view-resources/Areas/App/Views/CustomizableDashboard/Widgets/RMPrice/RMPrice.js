$(function () {
    console.log("RMPrice");
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _yearService = abp.services.app.years;
    var _$rMPriceTable = $('#RMPriceTable');
    var _$Container = $('.RMPriceContainer');
    var _rmPriceHistoryTable = $('#RMPriceTable');
    var _$datecontainer = $(".pricedate-range-filter-container");
    var _$suppliercontainer = $(".price-supplier-filter-container");
    var _$yearcontainer = $(".price-year-filter-container");
    var _$teamcontainer = $(".price-team-filter-container");
    var _$priceselSupplier = _$suppliercontainer.find('#priceselSupplier');
    var _$priceselYear = _$yearcontainer.find('#priceselYear');
    var _$priceselTeams = _$teamcontainer.find('#priceselTeams');
    var _dtrange = $('#dtrange1');
    var _$dateRange;

    var teams = function () {
        _tenantDashboardService
            .getDepartments()
            .done(function (result) {
                _$priceselTeams.html('<option value=" ">All Team</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$priceselTeams.append('<option value="' + result[rowIndex] + '">' + result[rowIndex] + '</option')
                }
            })
    }
    teams();
    var supplier = function () {
        var department = _$priceselTeams.val();
        _tenantDashboardService
            .getSupplierfromDept(department)
            .done(function (result) {
                _$priceselSupplier.html('<option value=" ">All Supplier</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$priceselSupplier.append('<option value="' + result[rowIndex].supplierId + '">' + result[rowIndex].supplierName + " - " + result[rowIndex].supplierCode + '</option')
                }
            })
    }
    supplier();
    var year = function () {
        _tenantDashboardService
            .getYears({})
            .done(function (result) {
                _$priceselYear.html('<option value=" ">Select Year</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$priceselYear.append('<option value="' + rowIndex + '">' + result[rowIndex].name + '</option')
                }
                _$priceselYear.val("4")
            })
    }
    year();
    _$priceselTeams.change(function () {
        supplier();
    })
    var refreshRMPrice = function () {
        _$Supplier = _$priceselSupplier.val();
        _$year = _$priceselYear.find('option:selected').text();
        _$Department = _$priceselTeams.find('option:selected').text();
        abp.ui.setBusy(_$Container);
        _tenantDashboardService
            .getTotalSupplier({
                department: _$Department,
                supplier: _$Supplier,
                year: _$year
            })
            .done(function (result) {
                loadinput(result);
            })
            .always(function () {
                abp.ui.clearBusy(_$Container);
            });

    };

    var globalResult;
    var refreshavobbysupplier = function () {
        _$Supplier = _$priceselSupplier.val();
        _$year = _$priceselYear.find('option:selected').text();
        _$Department = _$priceselTeams.find('option:selected').text();
        abp.ui.setBusy(_$Container);
        _tenantDashboardService
            .getAVOBForSupplierandTeams({
                department: _$Department,
                supplier: _$Supplier,
                year: _$year
            })
            .done(function (result) {
                globalResult = result;
                loadinput(result);
                loadsupplieravob(result);
            })
            .always(function () {
                abp.ui.clearBusy(_$Container);
            });

    };
    var loadinput = function (result) {
        console.log('Rmprice');
        var totalbuyers = {};
        var totalsuppliers = {};
        var totalavob = 0;
        var totalparts = 0;
        for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
            var price = result[rowIndex];
            totalparts += price["totalParts"];
            totalbuyers[price["buyer"]] = true;
            totalsuppliers[price["supplier"]] = true;
            totalavob += price["totalAVOBBySupplier"];
            //$('#totalsupplierhandled').text(price["totalBuyers"]);
            $('#totalpartshandled').text(totalparts);
            $('#totalavob').text(totalavob.toFixed(2));
        }
        var uniqueBuyersCount = Object.keys(totalbuyers).length;
        console.log("Total unique buyers:", uniqueBuyersCount);
        $('#totalbuyerhandled').text(uniqueBuyersCount);
        var uniqueSupplierssCount = Object.keys(totalsuppliers).length;
        console.log("Total unique suppliers:", uniqueSupplierssCount);
        $('#totalsupplierhandled').text(uniqueSupplierssCount);
    }
    var loadsupplieravob = function (result) {
        console.log(globalResult)
        console.log('Rmprice');
        var $tableBody = $('#RMPrice_Content table tbody');
        $tableBody.html('');

        function closeDropdown(event) {
            if (!$(event.target).closest('.btn-group').length) {
                $('.dropdown-menu').removeClass('show');
            }
        }

        $(document).on('click', closeDropdown);

        for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
            var price = result[rowIndex];
            var $tr = $('<tr></tr>').append(
                //$('<td></td>').append(
                //    $('<div></div>').addClass('btn-group').append(
                //        $('<button></button>').addClass('btn btn-brand dropdown-toggle')
                //            .html('<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>')
                //            .css({
                //                'color': '#FFFFFF',
                //                'background-color': '#71180C',
                //                'border-color': '#71180C'
                //            })
                //            .click(function () {
                //                $(this).next('.dropdown-menu').toggleClass('show');
                //            }),
                //        $('<div></div>').addClass('dropdown-menu')
                //            .append(
                //                $('<li></li>').append($('<a></a>').addClass('dropdown-item view-trend').attr('href', '#').text('View Trend')),
                //                $('<li></li>').append($('<a></a>').addClass('dropdown-item view-impact').attr('href', '#').text('View Impact'))
                //            )
                //    )
                //),
                $('<td>' + price["department"] + '</td>'),
                $('<td>' + price["buyer"] + '</td>'),
                $('<td>' + price["supplier"] + '</td>'),
                $('<td>' + price["supplierCode"] + '</td>'),
                $('<td>' + price["totalParts"] + '</td>'),
                $('<td>' + price["totalAVOBBySupplier"] + '</td>')
            );
            $tableBody.append($tr);
        }
    };
    $(document).on('click', '.view-trend', function () {
        var supplierCode = $(this).closest('tr').find('td:eq(1)').text();
        var supplier = $(this).closest('tr').find('td:eq(2)').text();
        handleViewTrend(supplierCode, supplier);
    });

    $(document).on('click', '.view-impact', function () {
        var supplierCode = $(this).closest('tr').find('td:eq(1)').text();
        var supplier = $(this).closest('tr').find('td:eq(2)').text();
        handleViewImpact(supplierCode, supplier);
    });
    function handleViewTrend(supplierCode, supplier) {
        console.log('View Trend for Supplier Code: ' + supplierCode + ', Supplier: ' + supplier);
        $('#viewTrendModal').modal('show');
        refreshRMPriceTrend(supplierCode);
    }
    function handleViewImpact(supplierCode, supplier) {
        console.log('View Impact for Supplier Code: ' + supplierCode + ', Supplier: ' + supplier);
        $('#viewImpactModal').modal('show');
        refreshimpact(supplierCode);
    }
    var refreshimpact = function (supplier) {
        for (var rowIndex = 0; rowIndex < globalResult.priceTrends.length; rowIndex++) {
            var price = globalResult.priceTrends[rowIndex];
            if (supplier == price["supplierCode"]) {
                console.log(price["supplierId"])
                _$Supplier = price["supplierId"];
            }
        }
        
        _$Supplier = _$priceselSupplier.val();
        _$year = _$priceselYear.find('option:selected').text();
        abp.ui.setBusy(_$Container);
        _tenantDashboardService
            .getSupplierRMPriceImpact({
                buyer: _$Buyer,
                //buyerName: _buyer.text(),
                supplier: _$Supplier,
                //supplierName: _supplier.text() && _supplier.text().split(' - ').length > 1 ? _supplier.text().split(' - ')[0] : null,
                period: _$dateRange,
                isGenerateA3: false,
                //spec: _$Group,
                //grade: _$Grade,
                /*plant: _plant.text() && _plant.text().split(' - ').length > 1 ? _plant.text().split(' - ')[0] : null*/
               /* plant: concatenatedString*/

            })
            .done(function (result) {
                console.log(result)
                loadPriceImpact(result);
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            });
    }
    var loadPriceImpact = function (result) {
        console.log(globalResult)
        console.log('Rmprice');
        var $tableBody = $('#RMPriceimp_Content table tbody');
        $tableBody.html('');
        for (var rowIndex = 0; rowIndex < result.priceImpacts.length; rowIndex++) {
            var price = result.priceImpacts[rowIndex];
            var $tr = $('<tr></tr>').append(
                $('<td>' + price["slNo"] + '</td>'),
                $('<td>' + price["partNo"] + '</td>'),
                $('<td >' + price["description"] + '</td>'),
                $('<td>' + (price.isParentSubMixture ? " " : price["globusEPU"]) + '</td>'),
                $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["currentExwPrice"]).toFixed(2)) + '</td>'),
                $('<td>' + (price.isParentSubMixture ? " " : parseFloat(price["currentAVOB"]).toFixed(2)) + '</td>')
            );
            $tableBody.append($tr);
        }
    }
    var refreshRMPriceTrend = function (supplier) {
        console.log(_$dateRange)
        for (var rowIndex = 0; rowIndex < globalResult.priceTrends.length; rowIndex++) {
            var price = globalResult.priceTrends[rowIndex];
            if (supplier == price["supplierCode"]) {
                console.log(price["supplierId"])
                _$Supplier = price["supplierId"];
            }
        }
        _$Buyer = _$priceselBUyer.val();
        _$year = _$priceselYear.find('option:selected').text();
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getSupplierRMPriceTrend({
                    buyer: _$Buyer,
                    supplier: _$Supplier,
                    period: _$dateRange,
                    
                })
                .done(function (result) {
                    console.log(result)
                    loadRMPriceTrend(result);
                })
                .always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        
    };
    var loadRMPriceTrend = function (result) {

        console.log('RmpriceTrend');
        for (var i = 0; i < _$Container.length; i++) {
            var container = $(_$Container[i]);
            var $tableBody = container.find('#RMPricetr_Content table tbody');
            $tableBody.html('');
            for (var rowIndex = 0; rowIndex < result.priceTrends.length; rowIndex++) {
                var price = result.priceTrends[rowIndex];
                var $tr = $('<tr></tr>').append(
                    $('<td>' + price["mixtureGrade"] + '</td>'),
                    $('<td>' + price["rmGrade"] + '</td>')
                );
                $tableBody.append($tr);
            }
        }
    }
    $('#PriceSubmit').click(function () {
        //refreshRMPrice();
        refreshavobbysupplier();
    });
    $('#refresRMPriceButton').click(function () {
        //refreshRMPrice();
        refreshavobbysupplier();
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnDateChange', function (_selectedDates) {
        _dtrange.text(_selectedDates.split('-')[0]);
        _$dateRange = _selectedDates;
        
    });

})