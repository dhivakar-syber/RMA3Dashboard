$(function () {
    console.log("RMPriceHistory");
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _$Container = $('.RMPriceHistoryContainer');
    var _rmPriceHistoryTable = $('#RMPriceHistoryTable');
    var _$datecontainer = $(".historydate-range-filter-container");
    var _$buyercontainer = $(".rmhistory-buyer-filter-container");
    var _$suppliercontainer = $(".rmhistory-supplier-filter-container");
    var _$gradecontainer = $(".rmhistory-grade-filter-container");
    var _$speccontainer = $(".rmhistory-spec-filter-container");
    var _$graphContainer = $('.HistoryPriceTrendContainer');
    var _$dateRangePickerInput = _$datecontainer.find("#historydate-range-filter");
    var _$datePickerInput = _$datecontainer.find('#historydate-time-filter');
    var _$historyselBUyer = _$buyercontainer.find('#historyselBUyer');
    var _$historyselSupplier = _$suppliercontainer.find('#historyselSupplier');
    var _$historyselGrade = _$gradecontainer.find('#historyselGrade');
    var _$historyselSpec = _$speccontainer.find('#historyselSpec');
    var _$dateRangePickerOpenButton = _$datecontainer.find("#btnDateRangeFilterOpen");
    var _$dateFilterOpen = _$datecontainer.find("#btnDateFilterOpen");
    var _$Spec;
    var _$Grade;

    var fnsupplier = function () {
        var buyerId = _$historyselBUyer.val();
        console.log(buyerId)
            _tenantDashboardService
                .getSupplier(buyerId)
                .done(function (result) {
                    _$historyselSupplier.empty();
                    _$historyselSupplier.html('<option value="">Select Supplier</option')
                    for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                        _$historyselSupplier.append('<option value="' + result[rowIndex].supplierId + '">' + result[rowIndex].supplierName + " - " + result[rowIndex].supplierCode + '</option')
                    }
                    fngrade();
                    fnspec();
                })

    }
    var fnbuyer = function () {
        _tenantDashboardService
            .getBuyer({})
            .done(function (result) {
                _$historyselBUyer.html('<option value="0">Select Buyer</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$historyselBUyer.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    fnbuyer();
    var fngrade = function () {
        var _$Supplier = _$historyselSupplier.val();
        var _$Buyer = _$historyselBUyer.val();
        console.log(_$Supplier)
        console.log(_$Buyer)
        _tenantDashboardService
            .getGrade({
                buyer: _$Buyer,
                supplier: _$Supplier
            })
            .done(function (result) {
                _$historyselGrade.html('<option value="">All Applicable</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$historyselGrade.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
                fnspec();
            })

    }

    var fnspec = function () {
        var _$Supplier = _$historyselSupplier.val();
        var _$Buyer = _$historyselBUyer.val();
        var _$grade = _$historyselGrade.val();
        console.log(_$grade)
        _tenantDashboardService
            .getSpec({
                buyer: _$Buyer,
                supplier: _$Supplier,
                grade: _$grade
            })
            .done(function (result) {
                _$historyselSpec.html('<option value="">All Applicable</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$historyselSpec.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })

    }

    var getHasMixture = function () {
        var _$Buyer = _$historyselBUyer.val();
        var _$Supplier = _$historyselSupplier.val();
        _tenantDashboardService
            .getPartHasMixture({
                buyer: _$Buyer,
                supplier: _$Supplier,
            })
            .done(function (result) {
                _hasMixture = result.partHasMixture;
            });
    }

    var refreshRMPriceHistory = function () {
        var _$Buyer = _$historyselBUyer.val();
        var _$Supplier = _$historyselSupplier.val();
        var _$Spec = _$historyselSpec.val();
        var _$Grade = _$historyselGrade.val();
        //console.log(_$Spec)
        //console.log(_$Grade)
        //if ( _$Buyer && _$Supplier) {
            getHasMixture();
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getSupplierRMPriceHistory({
                    buyer: _$Buyer,
                    supplier: _$Supplier,
                    period:'09/20/2023'-'09/20/2023',
                    isGenerateA3: false,
                    spec: _$Spec,
                    grade: _$Grade,
                    plant: 'All Plant'
                })
                .done(function (result) {
                    loadRMPriceHistory(result);
                })
                .always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        //}
    };
    var loadRMPriceHistory = function (result) {
        for (var i = 0; i < _$Container.length; i++) {
            var container = $(_$Container[i]);
            var $tableBody = container.find('#RMPriceHistory_Content table tbody');
            $tableBody.html('');
            for (var rowIndex = 0; rowIndex < result.priceTrends.length; rowIndex++) {
                var price = result.priceTrends[rowIndex];
                var $tr = $('<tr></tr>').append(
                    $('<td>' + price["buyer"] + '</td>'),
                    $('<td>' + price["supplier"] + '</td>'),
                    $('<td>' + price["parentGrp"] + '</td>'), 
                    $('<td>' + price["rmGrade"] + '</td>'), 
                    $('<td>' + price["mixtureGrade"] + '</td>'),
                    $('<td>' + price["uom"] + '</td>'),
                    $('<td>' + price["setteledMY"] + '</td>'),
                    $('<td>' + parseFloat(price["revisedUR"]).toFixed(2) + '</td>'),
                    $('<td>' + parseFloat(price["scrapRevised"]).toFixed(2) + '</td>'),
                );
                $tableBody.append($tr);
            }
        }
    }

    refreshRMPriceHistory();

    $(".refreshRMPriceHistoryButton").click(function () {
        refreshRMPriceHistory();
    });

    _$historyselBUyer.change(function () {
        fnsupplier();
       // unitrategraph();
        //fngrade();
        //fnspec();
        refreshRMPriceHistory();

    })
    _$historyselSupplier.change(function () {
        fngrade();
        unitrategraph();
        scraprategraph();
        //price();
        //fnspec();
        refreshRMPriceHistory();

    })
    _$historyselGrade.change(function () {
        fnspec();
        unitrategraph();
        scraprategraph();
        refreshRMPriceHistory();

    })
    _$historyselSpec.change(function () {
        unitrategraph();
        scraprategraph();
        refreshRMPriceHistory();
    })

    var unitrategraph = function () {
        var _widgetBase = app.widgetBase.create();
        var _$Container = $('.PriceHistoryContainer');
        var PriceTrend = function (data) {
            var dayLabels = [];
            for (var d = 0; d < data.length; d++) {
                if (!data[d].hasOwnProperty('setteledMY')) {
                    continue;
                }
                dayLabels.push(data[d].setteledMY);
            }
            console.log(dayLabels);
            
            var unitRate = [];
            for (var r = 0; r < data.length; r++) {
                if (!data[r].hasOwnProperty('revisedUR')) {
                    continue;
                }

                unitRate.push(data[r].revisedUR);
            }
            console.log(unitRate);

            var rmSpec = [];
            for (var s = 0; s < data.length; s++) {
                if (!data[s].hasOwnProperty('mixtureGrade')) {
                    continue;
                }

                rmSpec.push(data[s].mixtureGrade);
            }
            console.log(rmSpec);

            function getRandomColor() {
                var letters = '0123456789ABCDEF';
                var color = '#';
                for (var i = 0; i < 6; i++) {
                    color += letters[Math.floor(Math.random() * 16)];
                }
                return color;
            }

            var dataset = {};
            for (var i = 0; i < rmSpec.length; i++) {
                
                var key = rmSpec[i];
                var value = {
                    date: dayLabels[i],
                    rate: unitRate[i]
                };
                if (dataset[key]) {
                    dataset[key].push(value);
                } else {
                    dataset[key] = [value];
                }
            }
            console.log(dataset);

            var formattedData = Object.entries(dataset).map(function (entry) {

                var key = entry[0];
                var value = entry[1];
                var color = getRandomColor();


                var obj = {
                    label: key,
                    data: value.map(function (point) {
                        return {
                            x: point.date,
                            y: point.rate
                        };
                    }),
                    backgroundColor: color,
                    borderColor: color,
                    fill:false
                };
                return obj;
            });
            console.log(formattedData);

            var day = dayLabels
            var unique = day.filter((name, index) => day.indexOf(name) === index);
            console.log(unique);

            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#m_chart_price_history');
                new Chart(chartContainer, {
                    type: 'line',
                    data: {
                        labels: unique,
                        datasets: formattedData 
                    },
                    options: {
                        plugins: {
                            legend: {
                                display: true, 
                                position: 'right', 
                                labels: {
                                    color: 'black', 
                                    font: {
                                        size: 14 
                                    }
                                }
                            }
                        },
                        tooltips: {
                            mode: 'nearest',
                            xPadding: 10,
                            yPadding: 10,
                            caretPadding: 10
                        },
                        legend: {
                            display: true
                        },
                        responsive: true,
                        maintainAspectRatio: false,
                        hover: {
                            mode: 'index'
                        },
                        scales: {
                            x: {
                                type: "time", 
                                time: {
                                    parser: "DD-MM-YYYY" 
                                }
                            },
                            y: {
                                beginAtZero: true,
                                title: {
                                    display: true,
                                    text: 'Unit Rate'
                                }  
                            }
                        },
                    }
                });
            }
        };

        var getPriceTrend = function () {
            var _$Buyer = _$historyselBUyer.val();
            var _$Supplier = _$historyselSupplier.val();
            var _$Spec = _$historyselSpec.val();
            var _$Grade = _$historyselGrade.val();
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getSupplierRMPriceHistory({
                    buyer: _$Buyer,
                    supplier: _$Supplier,
                    period: '08/28/2023' - '08/28/2023',
                    isGenerateA3: false,
                    spec: _$Spec,
                    grade: _$Grade,
                    plant: 'All Plant'
                })
                .done(function (result) {
                    PriceTrend(result.priceTrends);
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        };

        _widgetBase.runDelayed(getPriceTrend);

        $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
            _widgetBase.runDelayed(getPriceTrend);
        });
    }
   // unitrategraph();
    var scraprategraph = function () {
        var _widgetBase = app.widgetBase.create();
        var _$Container = $('.ScrapRateContainer');
        var PriceTrend = function (data) {
            var dayLabels = [];
            for (var d = 0; d < data.length; d++) {
                if (!data[d].hasOwnProperty('setteledMY')) {
                    continue;
                }
                dayLabels.push(data[d].setteledMY);
            }

            var scrapRate = [];
            for (var r = 0; r < data.length; r++) {
                if (!data[r].hasOwnProperty('scrapRevised')) {
                continue;
                }

            scrapRate.push(data[r].scrapRevised);
        }
        var rmSpec = [];
        for (var s = 0; s < data.length; s++) {
            if (!data[s].hasOwnProperty('mixtureGrade')) {
                continue;
            }

            rmSpec.push(data[s].mixtureGrade);
        }

        function getRandomColor() {
            var letters = '0123456789ABCDEF';
            var color = '#';
            for (var i = 0; i < 6; i++) {
                color += letters[Math.floor(Math.random() * 16)];
            }
            return color;
        }

        var dataset = {};
        for (var i = 0; i < rmSpec.length; i++) {

            var key = rmSpec[i];
            var value = {
                date: dayLabels[i],
                rate: scrapRate[i]
            };
            if (dataset[key]) {
                dataset[key].push(value);
            } else {
                dataset[key] = [value];
            }
        }
        console.log(dataset);

        var formattedData = Object.entries(dataset).map(function (entry) {

            var key = entry[0];
            var value = entry[1];
            var color = getRandomColor();


            var obj = {
                label: key,
                data: value.map(function (point) {

                    return {
                        x: point.date,
                        y: point.rate
                    };
                }),
                backgroundColor: color,
                borderColor: color,
                fill: false
            };
            return obj;
        });
        console.log(formattedData);

        var day = dayLabels
        var unique = day.filter((name, index) => day.indexOf(name) === index);
        console.log(unique);

        console.log(dayLabels);
        console.log(scrapRate);
        console.log(rmSpec);

        for (var i = 0; i < _$Container.length; i++) {
            var chartContainer = $(_$Container[i]).find('#m_chart_scrap_rate');
            new Chart(chartContainer, {
                type: 'line',
                data: {
                    labels: unique,
                    datasets: formattedData
                },
                options: {
                    tooltips: {
                        mode: 'nearest',
                        xPadding: 10,
                        yPadding: 10,
                        caretPadding: 10
                    },
                    legend: {
                        display: true
                    },
                    responsive: true,
                    maintainAspectRatio: false,
                    hover: {
                        mode: 'index'
                    },
                    scales: {
                        x: {
                            type: "time",
                            time: {
                                parser: "DD-MM-YYYY"
                            }
                        },
                        y: {
                            beginAtZero: true,
                            title: {
                                display: true,
                                text: 'Scrap Rate'
                            }
                        }
                    },
                }
            });
        }
    };

    var getPriceTrend = function () {
        var _$Buyer = _$historyselBUyer.val();
        var _$Supplier = _$historyselSupplier.val();
        var _$Spec = _$historyselSpec.val();
        var _$Grade = _$historyselGrade.val();
        abp.ui.setBusy(_$Container);
        _tenantDashboardService
            .getSupplierRMPriceHistory({
                buyer: _$Buyer,
                supplier: _$Supplier,
                period: '08/28/2023' - '08/28/2023',
                isGenerateA3: false,
                spec: _$Spec,
                grade: _$Grade,
                plant: 'All Plant'
            })
            .done(function (result) {
                PriceTrend(result.priceTrends);
            }).always(function () {
                abp.ui.clearBusy(_$Container);
            });
    };

    _widgetBase.runDelayed(getPriceTrend);

    $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
        _widgetBase.runDelayed(getPriceTrend);
    });
    }
})