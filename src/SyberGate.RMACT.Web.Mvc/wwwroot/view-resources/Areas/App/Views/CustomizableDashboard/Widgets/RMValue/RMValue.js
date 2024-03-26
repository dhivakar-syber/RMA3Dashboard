$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();
    var _$Container = $('.RMValueContainer');
    var _$buyercontainer = $(".rmprice-buyer-filter-container");
    var _$suppliercontainer = $(".rmprice-supplier-filter-container");
    var _$yearcontainer = $(".rmprice-year-filter-container");
    var _$monthcontainer = $(".rmprice-month-filter-container");
    var _$gradecontainer = $(".rmprice-grade-filter-container");
    var _$groupcontainer = $(".rmprice-group-filter-container");
    var _$castingcontainer = $(".rmprice-casting-filter-container");
    var _$alu1container = $(".rmprice-alu1-filter-container");
    var _$alu2container = $(".rmprice-alu2-filter-container");
    var _$coppercontainer = $(".rmprice-copper-filter-container");
    var _$crscontainer = $(".rmprice-crs-filter-container");
    var _$hrscontainer = $(".rmprice-hrs-filter-container");
    var _$forgingcontainer = $(".rmprice-forging-filter-container");
    var _$nrcontainer = $(".rmprice-nr-filter-container");
    var _$priceselBUyer = _$buyercontainer.find('#priceselBUyer');
    var _$priceselSupplier = _$suppliercontainer.find('#priceselSupplier');
    var _$priceselYear = _$yearcontainer.find('#priceselYear');
    var _$priceselMonth = _$monthcontainer.find('#priceselMonth');
    var _$priceselGrade = _$gradecontainer.find('#priceselGrade');
    var _$priceselGroup = _$groupcontainer.find('#priceselGroup');
    var _$priceselCasting = _$castingcontainer.find('#priceselCasting');
    var _$priceselalu1 = _$alu1container.find('#priceselalu1');
    var _$priceselalu2 = _$alu2container.find('#priceselalu2');
    var _$priceselCopper = _$coppercontainer.find('#priceselCopper');
    var _$priceselCrs = _$crscontainer.find('#priceselCrs');
    var _$priceselHrs = _$hrscontainer.find('#priceselHrs');
    var _$priceselForging = _$forgingcontainer.find('#priceselForging');
    var _$priceselNR = _$nrcontainer.find('#priceselNR');
    var _$Buyer = _$priceselBUyer.val();
    var _$Supplier = _$priceselSupplier.val();
    var _$Year = _$priceselYear.find('option:selected').text();  
    var _$Month = _$priceselMonth.find('option:selected').text(); 
    var groupArray = [47, 68, 69, 70, 71, 72, 73, 79];
    var group = ["Castings", "Aluminium Base", "Aluminium Alloys", "Copper", "Cold-rolled steel", "Hot-rolled steel", "Forging Alloys", "Natural Rubber"];
    var data = [];
    var grp = [];
    for (var i = 0; i < group.length; i++) {
        grp.push({ group: group[i] }); 
    }
    console.log(grp)
    //var gr=[]
    //_tenantDashboardService
    //    .getgroup()
    //    .done(function (result) {
    //        for (var d = 0; d < result.length; d++) {
    //            if (!result[d].hasOwnProperty('name')) {
    //                continue;
    //            }
    //            gr.push(result[d].name);
    //        }
    //    },
    //        console.log(gr));

    var fsupplier = function () {
        var buyerId = _$priceselBUyer.val();
        console.log(buyerId)
        _tenantDashboardService
            .getSupplier(buyerId)
            .done(function (result) {
                _$priceselSupplier.empty();
                _$priceselSupplier.html('<option value="">Select Supplier</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$priceselSupplier.append('<option value="' + result[rowIndex].supplierId + '">' + result[rowIndex].supplierName + " - " + result[rowIndex].supplierCode + '</option')
                }
            })

    }
    var fbuyer = function () {
        _tenantDashboardService
            .getBuyer({})
            .done(function (result) {
                _$priceselBUyer.html('<option value="0">Select Buyer</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$priceselBUyer.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    fbuyer();

    var fyear = function () {
        _tenantDashboardService
            .getYears({})
            .done(function (result) {
                _$priceselYear.html('<option value=" ">Select Year</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$priceselYear.append('<option value="' + rowIndex + '">' + result[rowIndex].name + '</option')
                }
                _$priceselYear.val("4");
            })
    }
    fyear();

    //var RMPrice = function () {
    //    abp.ui.setBusy(_$Container);
    //    console.log("rmvalue")
    //    var requestsDone = new Array(groupArray.length).fill(false);
    //    for (var i = 0; i < groupArray.length; i++) {
    //        makeRequest(groupArray[i], i, data, requestsDone)
    //    }

    //};

    //var makeRequest = function (group, rowIndex, data, requestsDone) {
    //    var _$Buyer = _$priceselBUyer.val();
    //    var _$Supplier = _$priceselSupplier.val();
    //    var _$Year = _$priceselYear.find('option:selected').text();  
    //    var _$Month = _$priceselMonth.find('option:selected').text();
    //    _tenantDashboardService
    //        .gettotalrate({
    //            buyerId: _$Buyer,
    //            supplierId: _$Supplier,
    //            year: _$Year,
    //            month: _$Month,
    //            rMGroupId: group
    //        })
    //        .done(function (result) {
    //            data[rowIndex] = result;

    //            requestsDone[rowIndex] = true;

    //            if (requestsDone.every(Boolean)) {
    //                abp.ui.clearBusy(_$Container);
    //                loadRMPrice(data);
    //            }

    //        })

    //}
    //var loadRMPrice = function (data) {
    //    console.log(data)
    //    for (var i = 0; i < _$Container.length; i++) {
    //        var container = $(_$Container[i]);
    //        var $tableBody = container.find('#RMPrice_Content table tbody');
    //        $tableBody.html('');
           
    //        for (var rowIndex = 0; rowIndex < data.length; rowIndex++) {
    //            var group = grp[rowIndex].group;
    //            var price = data[rowIndex];
    //            var $tr = $('<tr></tr>').append(
    //                $('<td>' + group + '</td>'),
    //                //$('<td>' + parseFloat(price[0].grossInputWeightAverage).toFixed(2) + '</td>'),
    //                //$('<td>' + parseFloat(price[0].epuAverage).toFixed(2) + '</td>'),
    //                //$('<td>' + parseFloat(price[0].unitRateAverage).toFixed(2) + '</td>'),
    //                //$('<td>' + parseFloat(price[0].sobAverage).toFixed(2) + '</td>'),
    //                $('<td>' + parseFloat(price[0].totalAverage).toFixed(2) + '</td>'),
    //            );
    //            $tableBody.append($tr);
                
    //        }
    //    }
    //};

    //RMPrice();
    $('#historysubmit').click(function () {
        var yearValue = $('#priceselYear').val();
        var monthValue = $('#priceselMonth').val();
        if (yearValue !== " " && monthValue !== " ") {
            RMPrice();
            valuechart();
        } else if (yearValue === " " && monthValue === " ") {
            alert("Select both Year and Month");
        } else if (yearValue === " ") {
            alert("Select Year");
        } else if (monthValue === " ") {
            alert("Select Month");
        }
    });
    $('#refresRMPriceButton').click(function () {
        //RMPrice();
        valuechart();
    });

    _$priceselBUyer.change(function () {
        fsupplier();
        fyear();
        //RMPrice();
    })

    //_$priceselSupplier.change(function () {
    //    fyear();
   //})

    var valuechart = function () {
        console.log("chart")
        var _widgetBase = app.widgetBase.create();
        var _$Container = $('.RMValueContainer');
        var PriceValue = function (data) {
            console.log(data)
            var totalAverageArray = $.map(data, function (item) {
                return item[0].totalAverage;
            });
            console.log(totalAverageArray);

            var labels = [
                "Castings",
                "Aluminium Base",
                "Aluminium Alloys",
                "Copper",
                "Cold-rolled steel",
                "Hot-rolled steel",
                "Forging Alloys",
                "Natural Rubber"
            ];

            console.log(labels)

            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#m_chart_RM_value');
                new Chart(chartContainer, {
                    type: 'pie',
                    data: {
                        datasets: [{
                            data: totalAverageArray,
                            backgroundColor: ['rgb(255, 99, 132)', 'rgb(54, 162, 235)', 'rgb(255, 205, 86)', 'rgb(54, 150, 25)', 'rgb(128,0,0)', 'rgb(128,0,128)', 'rgb(255,165,0)', 'rgb(240,230,140)', 'rgb(152,251,152)', 'rgb(0,206,209)'],

                        }],
                        labels: labels,
                    },
                    options: {
                        plugins: {
                            datalabels: {
                                display: true,  
                                color: 'black',  
                                font: {
                                    weight: 'bold',  
                                },
                            },
                        },
                        tooltips: {
                            xPadding: 10,
                            yPadding: 10,
                            caretPadding: 10
                        },
                        legend: {
                            display: true
                        },
                        responsive: true,
                        maintainAspectRatio: false,
                    }
                });
            }
        };

        var getPriceTrend = function () {

            console.log("rmvalue")
            var requestsDone = new Array(groupArray.length).fill(false);
            for (var i = 0; i < groupArray.length; i++) {
                makeRequest(groupArray[i], i, data, requestsDone)
            }

            //PriceValue(data);

        };

        var makeRequest = function (group, rowIndex, data, requestsDone) {
            var _$Buyer = _$priceselBUyer.val();
            var _$Supplier = _$priceselSupplier.val();
            var _$Year = _$priceselYear.find('option:selected').text();  
            var _$Month = _$priceselMonth.find('option:selected').text();
            _tenantDashboardService
                .gettotalrate({
                    buyerId: _$Buyer,
                    supplierId: _$Supplier,
                    year: _$Year,
                    month: _$Month,
                    rMGroupId: group
                })
                .done(function (result) {
                    data[rowIndex] = result;

                    requestsDone[rowIndex] = true;

                    if (requestsDone.every(Boolean)) {
                        PriceValue(data);
                    }

                })

        }

        _widgetBase.runDelayed(getPriceTrend);

        $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
            _widgetBase.runDelayed(getPriceTrend);
        });
    };
    valuechart();
});