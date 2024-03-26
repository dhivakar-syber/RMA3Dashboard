$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();
    var _$buyercontainer = $(".chart-buyer-filter-container");
    var _$buyercontainer1 = $(".chart-buyer-filter-container");
    var _$suppliercontainer = $(".chart-supplier-filter-container");
    var _$yearcontainer = $(".chart-year-filter-container");
    var _$monthcontainer = $(".chart-month-filter-container");
    var _$monthcontainer2 = $(".chart-month2-filter-container");
    var _$groupcontainer = $(".chart-group-filter-container");
    var _$teamcontainer = $(".chart-team-filter-container");
    var _$plantcontainer = $(".chart-plant-filter-container");
    var _$chartselBUyer = _$buyercontainer.find('#chartselBUyer');
    var _$chartselBUyer = _$buyercontainer1.find('#chartselBUyer');
    var _$chartselTeams = _$teamcontainer.find('#chartselTeams');
    var _$chartselSupplier = _$suppliercontainer.find('#chartselSupplier');
    var _$chartselYear = _$yearcontainer.find('#chartselYear');
    var _$chartselMonth = _$monthcontainer.find('#chartselMonth');
    var _$chartselMonth2 = _$monthcontainer2.find('#chartselMonth2');
    var _$chartselGroup = _$groupcontainer.find('#chartselGroup');
    var _$chartselPlant = _$plantcontainer.find('#chartselPlant');
    var _$Container = $('.ChartContainer');
    var _$CompContainer = $('.ChartContainer');
    var groupArray = [47, 68, 69, 70, 71, 72, 73, 79];
    var group = ["Castings", "Aluminium Base", "Aluminium Alloys", "Copper", "Cold-rolled steel", "Hot-rolled steel", "Forging Alloys", "Natural Rubber"];
    var data = [];
    var grp = [];
    for (var i = 0; i < group.length; i++) {
        grp.push({ group: group[i] });
    }
    console.log(grp)

    //var chartbuyerfromsupplier = function () {
    //    var supplierId = _$chartselSupplier.val();
    //    _tenantDashboardService
    //        .getBuyersFromSupplier(supplierId)
    //        .done(function (result) {
    //            _$chartselBUyer1.html('<option value="0">Select Buyer</option')
    //            for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
    //                _$chartselBUyer1.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
    //            }
    //        })
    //}
    //chartbuyerfromsupplier();

    var chartsupplier = function () {
        var buyerId = _$chartselBUyer.val();
        console.log(buyerId)
        _tenantDashboardService
            .getSupplier(buyerId)
            .done(function (result) {
                _$chartselSupplier.empty();
                _$chartselSupplier.html('<option value="">Select Supplier</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$chartselSupplier.append('<option value="' + result[rowIndex].supplierId + '">' + result[rowIndex].supplierName + " - " + result[rowIndex].supplierCode + '</option')
                }
            })

    }
    var chartbuyer = function () {
        department = _$chartselTeams.find('option:selected').text(); 
        _tenantDashboardService
            .getBuyersFromDepartment(department)
            .done(function (result) {
                _$chartselBUyer.html('<option value="0">Select Buyer</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$chartselBUyer.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }

    var chartteams = function () {
        _tenantDashboardService
            .getDepartments()
            .done(function (result) {
                _$chartselTeams.html('<option value=" ">Select Team</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$chartselTeams.append('<option value="' + result[rowIndex] + '">' + result[rowIndex] + '</option')
                }
            })
    }
    chartteams();

    var chartyear = function () {
        _tenantDashboardService
            .getYears({})
            .done(function (result) {
                _$chartselYear.html('<option value=" ">Select Year</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$chartselYear.append('<option value="' + rowIndex + '">' + result[rowIndex].name + '</option')
                }
                _$chartselYear.val("4");
            })
    }
    chartyear();

    var chartgroup = function () {
        _tenantDashboardService
            .getgroup({})
            .done(function (result) {
                _$chartselGroup.html('<option value=" ">Select Group</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$chartselGroup.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
                _$chartselGroup.val("68");
            })
    }
    chartgroup();

    _$chartselTeams.change(function () {
        chartbuyer();
    })

    _$chartselBUyer.change(function () {
        chartsupplier();
    })

    $('#ChartSubmit').click(function () {
        var yearValue = $('#chartselYear').val();
        var monthValue = $('#chartselMonth').val();
        if (yearValue !== " " && monthValue !== " ") {
            loadvaluechart();
            loadtonnagechart();
        }
        loadcompchart();
        loadactualgraph();
        loadapprovedgraph();
        loadrejectedgraph();
        dEpartment();

    });

    $('#refreshchartButton').click(function () {
        var yearValue = $('#chartselYear').val();
        var monthValue = $('#chartselMonth').val();
        if (yearValue !== " " && monthValue !== " ") {
            loadvaluechart();
            loadtonnagechart();
        }
        loadcompchart();
        loadactualgraph();
        loadapprovedgraph();
        loadrejectedgraph();
        dEpartment();

    });
    $('#refreshComparisonchartButton').click(function () {
        loadcompchart();
    });
    $('#refreshRMvaluechartButton').click(function () {
        var yearValue = $('#chartselYear').val();
        var monthValue = $('#chartselMonth').val();
        if (yearValue !== " " && monthValue !== " ") {
            loadvaluechart();
        }
    });
    $('#refreshRMDocumentchartButton').click(function () {
        loadactualgraph();
        loadapprovedgraph();
        loadrejectedgraph();
        dEpartment();
    });

    $('#refreshRMtonnagechartButton').click(function () {
        var yearValue = $('#chartselYear').val();
        var monthValue = $('#chartselMonth').val();
        if (yearValue !== " " && monthValue !== " ") {
            loadtonnagechart();
        }
    });

    var loadcompchart = async function () {
        return new Promise(function (resolve, reject) {
            var _widgetBase = app.widgetBase.create();
            var _$Container = $('.ChartContainer');
            var chartInstance;

            var bardata = function (data) {
                console.log(data);

                var labels = [
                    'January', 'February', 'March', 'April', 'May', 'June',
                    'July', 'August', 'September', 'October', 'November', 'December'
                ];
                var datasets = [];

                function getRandomColor() {
                    var letters = '0123456789ABCDEF';
                    var color = '#';
                    for (var i = 0; i < 6; i++) {
                        color += letters[Math.floor(Math.random() * 16)];
                    }
                    return color;
                }

                $.each(data, function (index, buyer) {
                    var buyerName = buyer.buyerName;
                    var suppliername = buyer.suppliername;
                    var spec = buyer.spec;
                    var label = `${buyerName} - ${suppliername} - ${spec}`;
                    var color = getRandomColor();
                    var values = [];

                    $.each(buyer, function (month, value) {
                        if (month !== "buyerName" && month !== "suppliername" && month !== "spec") {
                            values.push(value);
                        }
                    });

                    datasets.push({
                        label: label,
                        data: values,
                        backgroundColor: color,
                        borderColor: color,
                        fill: false
                    });
                    console.log(datasets);
                });

                if (chartInstance) {
                    chartInstance.destroy();
                }

                chartInstance = new Chart($(_$Container[0]).find('#m_chart_RM_Comparison'), {
                    type: 'line',
                    data: {
                        labels: labels,
                        datasets: datasets,
                    },
                    options: {
                        scales: {
                            x: {
                                grid: {
                                    display: false
                                }
                            },
                            y: {
                                grid: {
                                    display: false
                                },
                                beginAtZero: true,
                            },
                        },
                        legend: {
                            display: false,
                        },
                        hover: {
                            mode: 'index'
                        }
                    }
                });
            };

            var getPricebysupplier = function () {
                var _$Buyer = _$chartselBUyer.val();
                var _$Group = _$chartselGroup.val();
                var _$Year = _$chartselYear.find('option:selected').text();
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .getrmforcompbybuyer({
                        buyerId: _$Buyer,
                        rMGroupId: _$Group,
                        year: _$Year
                    })
                    .done(function (result) {
                        bardata(result);
                    }).always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
            };

            _widgetBase.runDelayed(getPricebysupplier);

            $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
                _widgetBase.runDelayed(getPricebysupplier);
            });

            resolve();
        });
    };


    var loadvaluechart = async function () {
        return new Promise(function (resolve, reject) {
            console.log("chart")
            var _widgetBase = app.widgetBase.create();
            var _$Container = $('.ChartContainer');
            var piechartInstance;
            var PriceValue = function (data) {
                var percentage = [];
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
                //for (var i = 0; i < data.length; i++) {
                //    if (!data[r].hasOwnProperty('parentGroup')) {
                //        continue;
                //    }

                //    ParentGroup.push(data[i].parentGroup);
                //}
                //for (var r = 0; r < data.length; r++) {
                //    if (!data[r].hasOwnProperty('totalAverage')) {
                //        continue;
                //    }

                //    percentage.push(data[r].totalAverage);
                //}
                var Group = [
                    "Castings",
                    "Aluminum Base",
                    "Aluminum Alloys",
                    "Copper",
                    "Cold-rolled Steel",
                    "Hot-rolled Steel",
                    "Forging Alloys",
                    "Natural Rubber"
                ];

                for (var i = 0; i < Group.length; i++) {
                    var found = false;
                    for (var r = 0; r < data.length; r++) {
                        if (/*data[r].hasOwnProperty('parentGroup') && */data[r].parentGroup === Group[i]) {
                            percentage.push(data[r].totalAverage);
                            found = true;
                            break;
                        }
                    }
                    if (!found) {
                        percentage.push(0);
                    }
                }
                console.log(percentage)
                
                if (piechartInstance) {
                    piechartInstance.destroy();
                }

                for (var i = 0; i < _$Container.length; i++) {
                    var chartContainer = $(_$Container[i]).find('#m_chart_RM_value');
                    piechartInstance = new Chart(chartContainer, {
                        type: 'pie',
                        data: {
                            datasets: [{
                                data: percentage,
                                backgroundColor: ['rgb(0,103,127)', 'rgb(255,255,64)', 'rgb(0,86,106)', 'rgb(200,200,200)', 'rgb(0,122,147)', 'rgb(158,158,158)', 'rgb(121,174,191)', 'rgb(80,151,171)', 'rgb(166,202,216)'],

                            }],
                            labels: labels,
                        },
                        options: {
                            plugins: {
                                datalabels: {
                                    color: 'white',
                                    display: true
                                }
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
                            hover: {
                                mode: 'index'
                            },
                        }
                    });
                }
            };

            var getPriceValue = function () {
                var _$Buyer = _$chartselBUyer.val();
                var _$Supplier = _$chartselSupplier.val();
                var _$Year = _$chartselYear.find('option:selected').text();
                var _$Month = _$chartselMonth.find('option:selected').text();
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .gettotalrate({
                        buyerId: _$Buyer,
                        supplierId: _$Supplier,
                        year: _$Year,
                        month: _$Month
                    })
                    .done(function (result) {
                        console.log(result)
                        PriceValue(result);
                    }).always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
            }

            _widgetBase.runDelayed(getPriceValue);

            $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
                _widgetBase.runDelayed(getPriceValue);
            });
            resolve();
        });
    };
    
    var loadactualgraph = async function () {
        return new Promise(function (resolve, reject) {
            var _widgetBase = app.widgetBase.create();
            var _$Container = $('.ChartContainer');
            var barchartInstance;
            var documentactual = function (data) {
                var department = [];
                for (var d = 0; d < data.length; d++) {
                    if (!data[d].hasOwnProperty('department')) {
                        continue;
                    }
                    department.push(data[d].department);
                }
                console.log(department);

                var quarters = ['Qtr1', 'Qtr2', 'Qtr3', 'Qtr4'];
                var quarterColors = ['orange', 'blue', 'green', 'rgb(0,103,127)'];

                var actualData = [];

                for (var i = 0; i < data.length; i++) {
                    var rowData = [];
                    for (var j = 1; j <= 4; j++) {
                        var total = data[i]["q" + j + "_Total"];
                        rowData.push(total);
                    }
                    actualData.push(rowData);
                }

                console.log(actualData);

                var datasets = [];
                for (var i = 0; i < quarters.length; i++) {
                    var label = quarters[i];
                    var data = [];
                    for (var j = 0; j < department.length; j++) {
                        var row = actualData[j];
                        var actual = row[i];
                        data.push(actual);
                    }
                    datasets.push({
                        label: label + " Actual",
                        data: data,
                        backgroundColor: quarterColors[i],
                        stack: 'stack' + i
                    });
                }
                console.log(datasets)

                if (barchartInstance) {
                    barchartInstance.destroy();
                }


                for (var i = 0; i < _$Container.length; i++) {
                    var chartContainer = $(_$Container[i]).find('#m_chart_actual');
                    barchartInstance = new Chart(chartContainer, {
                        type: 'horizontalBar',
                        data: {
                            labels: department,
                            datasets: datasets
                        },
                        options: {
                            title: {
                                display: false
                            },
                            legend: {
                                display: true
                            },
                            responsive: true,
                            maintainAspectRatio: false,
                            barThickness: 20,
                            scales: {
                                xAxes: [{
                                    stacked: false
                                }],
                                yAxes: [{
                                    stacked: false
                                }]
                            },
                            hover: {
                                mode: 'index'
                            },
                            layout: {
                                padding: {
                                    left: 0,
                                    right: 0,
                                    top: 0,
                                    bottom: 0
                                }
                            }
                        }
                    });
                }

            };

            var getactualdocument = function () {
                var _$Buyer = _$chartselBUyer.val();
                var _$Department = _$chartselTeams.val();
                var _$Year = _$chartselYear.find('option:selected').text();
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .getDepartmentQuarterSummary({
                        buyerId: _$Buyer,
                        department: _$Department,
                        year: _$Year
                    })
                    .done(function (result) {
                        documentactual(result);
                    }).always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
            };

            _widgetBase.runDelayed(getactualdocument);

            $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
                _widgetBase.runDelayed(getactualdocument);
            });
            resolve();
        });
    }



    var loadapprovedgraph = async function () {
        return new Promise(function (resolve, reject) {
            var _widgetBase = app.widgetBase.create();
            var _$Container = $('.ChartContainer');
            var barchart2Instance;
            var documentapproved = function (data) {
                var department = [];
                for (var d = 0; d < data.length; d++) {
                    if (!data[d].hasOwnProperty('department')) {
                        continue;
                    }
                    department.push(data[d].department);
                }
                console.log(department);

                var quarters = ['Qtr1', 'Qtr2', 'Qtr3', 'Qtr4'];
                var quarterColors = ['orange', 'blue', 'green', 'rgb(0,103,127)'];

                var approvedData = [];

                for (var i = 0; i < data.length; i++) {
                    var rowData = [];
                    for (var j = 1; j <= 4; j++) {
                        var approved = data[i]["q" + j + "_Approved"];
                        rowData.push(approved);
                    }
                    approvedData.push(rowData);
                }

                console.log(approvedData);

                var datasets = [];
                for (var i = 0; i < quarters.length; i++) {
                    var label = quarters[i];
                    var data2 = [];
                    for (var j = 0; j < department.length; j++) {
                        var row = approvedData[j];
                        var approved = row[i];
                        data2.push(approved);
                    }
                    datasets.push({
                        label: label + " Approved",
                        data: data2,
                        backgroundColor: quarterColors[i],
                        stack: 'stack' + i
                    });
                }

                if (barchart2Instance) {
                    barchart2Instance.destroy();
                }


                for (var i = 0; i < _$Container.length; i++) {
                    var chartContainer = $(_$Container[i]).find('#m_chart_Approved');
                    barchart2Instance = new Chart(chartContainer, {
                        type: 'horizontalBar',
                        data: {
                            labels: department,
                            datasets: datasets
                        },
                        options: {
                            title: {
                                display: false
                            },
                            legend: {
                                display: true
                            },
                            responsive: true,
                            maintainAspectRatio: false,
                            barThickness: 20,
                            scales: {
                                xAxes: [{
                                    stacked: false
                                }],
                                yAxes: [{
                                    stacked: false
                                }]
                            },
                            hover: {
                                mode: 'index'
                            },
                            layout: {
                                padding: {
                                    left: 0,
                                    right: 0,
                                    top: 0,
                                    bottom: 0
                                }
                            }
                        }
                    });
                }

            };

            var getapproveddocument = function () {
                var _$Buyer = _$chartselBUyer.val();
                var _$Department = _$chartselTeams.val();
                var _$Year = _$chartselYear.find('option:selected').text();
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .getDepartmentQuarterSummary({
                        buyerId: _$Buyer,
                        department: _$Department,
                        year: _$Year
                    })
                    .done(function (result) {
                        documentapproved(result);
                    }).always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
            };

            _widgetBase.runDelayed(getapproveddocument);

            $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
                _widgetBase.runDelayed(getapproveddocument);
            });
            resolve();
        });
    }



    var loadrejectedgraph = async function () {
        return new Promise(function (resolve, reject) {
            var _widgetBase = app.widgetBase.create();
            var _$Container = $('.ChartContainer');
            var barchart3Instance;
            var documentrejected = function (data) {
                var department = [];
                for (var d = 0; d < data.length; d++) {
                    if (!data[d].hasOwnProperty('department')) {
                        continue;
                    }
                    department.push(data[d].department);
                }
                console.log(department);

                var quarters = ['Qtr1', 'Qtr2', 'Qtr3', 'Qtr4'];
                var quarterColors = ['orange', 'blue', 'green', 'rgb(0,103,127)'];

                var rejectedData = [];

                for (var i = 0; i < data.length; i++) {
                    var rowData = [];
                    for (var j = 1; j <= 4; j++) {
                        var pending = data[i]["q" + j + "_Pending"];
                        rowData.push(pending);
                    }
                    rejectedData.push(rowData);
                }

                console.log(rejectedData);

                var datasets = [];
                for (var i = 0; i < quarters.length; i++) {
                    var label = quarters[i];
                    var data3 = [];
                    for (var j = 0; j < department.length; j++) {
                        var row = rejectedData[j];
                        var pending = row[i];
                        data3.push(pending);
                    }
                    datasets.push({
                        label: label + " Pending",
                        data: data3,
                        backgroundColor: quarterColors[i],
                        stack: 'stack' + i
                    });
                }

                if (barchart3Instance) {
                    barchart3Instance.destroy();
                }

                for (var i = 0; i < _$Container.length; i++) {
                    var chartContainer = $(_$Container[i]).find('#m_chart_Pending');
                    barchart3Instance = new Chart(chartContainer, {
                        type: 'horizontalBar',
                        data: {
                            labels: department,
                            datasets: datasets
                        },
                        options: {
                            title: {
                                display: false
                            },
                            legend: {
                                display: true
                            },
                            responsive: true,
                            maintainAspectRatio: false,
                            barThickness: 20,
                            scales: {
                                xAxes: [{
                                    stacked: false
                                }],
                                yAxes: [{
                                    stacked: false
                                }]
                            },
                            hover: {
                                mode: 'index'
                            },
                            layout: {
                                padding: {
                                    left: 0,
                                    right: 0,
                                    top: 0,
                                    bottom: 0
                                }
                            },
                            hover: {
                                mode: 'index'
                            }
                        }
                    });
                }

            };

            var getrejecteddocument = function () {
                var _$Buyer = _$chartselBUyer.val();
                var _$Department = _$chartselTeams.val();
                var _$Year = _$chartselYear.find('option:selected').text();
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .getDepartmentQuarterSummary({
                        buyerId: _$Buyer,
                        department: _$Department,
                        year: _$Year
                    })
                    .done(function (result) {
                        documentrejected(result);
                    }).always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
            };

            _widgetBase.runDelayed(getrejecteddocument);

            $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
                _widgetBase.runDelayed(getrejecteddocument);
            });
            resolve();
        });
    }

    var loadtonnagechart = async function () {
        return new Promise(function (resolve, reject) {
            console.log("chart")
            var _widgetBase = app.widgetBase.create();
            var _$Container = $('.ChartContainer');
            var piechart2Instance;
            var tonnageValue = function (data) {
                var Ton = [];
                //for (var r = 0; r < data.length; r++) {
                //    if (!data[r].hasOwnProperty('totalTon')) {
                //        continue;
                //    }

                //    Ton.push(data[r].totalTon);
                //}
                var Group = [
                    "Castings",
                    "Aluminum Base",
                    "Aluminum Alloys",
                    "Copper",
                    "Cold-rolled Steel",
                    "Hot-rolled Steel",
                    "Forging Alloys",
                    "Natural Rubber"
                ];
                for (var i = 0; i < Group.length; i++) {
                    var found = false;
                    for (var r = 0; r < data.length; r++) {
                        if (/*data[r].hasOwnProperty('parentGroup') &&*/ data[r].parentGroup === Group[i]) {
                            Ton.push(data[r].totalTon);
                            found = true;
                            break;
                        }
                    }
                    if (!found) {
                        Ton.push(0);
                    }
                }
                console.log(Ton)
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

                if (piechart2Instance) {
                    piechart2Instance.destroy();
                }


                for (var i = 0; i < _$Container.length; i++) {
                    var chartContainer = $(_$Container[i]).find('#m_chart_RM_tonnage');
                    piechart2Instance = new Chart(chartContainer, {
                        type: 'pie',
                        data: {
                            datasets: [{
                                data: Ton,
                                backgroundColor: ['rgb(0,103,127)', 'rgb(255,255,64)', 'rgb(0,86,106)', 'rgb(200,200,200)', 'rgb(0,122,147)', 'rgb(158,158,158)', 'rgb(121,174,191)', 'rgb(80,151,171)', 'rgb(166,202,216)'],

                            }],
                            labels: labels,
                        },
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                }
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
                            hover: {
                                mode: 'index'
                            },
                        }
                    });
                }
            };

            var gettonnage = function () {
                var _$Buyer = _$chartselBUyer.val();
                var _$Supplier = _$chartselSupplier.val();
                var _$Year = _$chartselYear.find('option:selected').text();
                var _$Month = _$chartselMonth.find('option:selected').text();
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .gettotalton({
                        buyerId: _$Buyer,
                        supplierId: _$Supplier,
                        year: _$Year,
                        month: _$Month
                    })
                    .done(function (result) {
                        console.log(result);
                        tonnageValue(result);
                    })
                    .always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
            };

            _widgetBase.runDelayed(gettonnage);

            $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
                _widgetBase.runDelayed(gettonnage);
            });
            resolve();
        });
    };
    
    Promise.resolve()
        .then(loadcompchart)
        .then(loadvaluechart)
        .then(loadactualgraph)
        .then(loadapprovedgraph)
        .then(loadrejectedgraph)
        .then(loadtonnagechart)

    var dEpartment = function () {
        var _$Buyer = _$chartselBUyer.val();
        var _$Department = _$chartselTeams.val();
        var _$Year = _$chartselYear.find('option:selected').text();
        abp.ui.setBusy(_$Container);
        _tenantDashboardService
            .getDepartmentQuarterSummary({
                buyerId: _$Buyer,
                department: _$Department,
                year: _$Year
            })
            .done(function (result) {
                loaddoc(result);
            })
            .always(function () {
                abp.ui.clearBusy(_$Container);
            });
        //}
    };
    var loaddoc = function (result) {
        for (var i = 0; i < _$Container.length; i++) {
            var container = $(_$Container[i]);
            var $tableBody = container.find('#Team_Content table tbody');
            $tableBody.html('');
            for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                //var grid = $('.grid-stack').data('gridstack');
                //grid.resize(
                //    $('.grid-stack-item')[0],
                //    $($('.grid-stack-item')[0]).attr('data-gs-width'),
                //    Math.ceil(($('#Team_Content').height() + 140 + grid.opts.verticalMargin) / (grid.cellHeight() + grid.opts.verticalMargin))
                //);
                var team = result[rowIndex];
                var $tr = $('<tr></tr>').append(
                    $('<td>' + team["department"] + '</td>'),
                    $('<td>' + team["q1_Total"] + '</td>'),
                    $('<td>' + team["q1_Approved"] + '</td>'),
                    $('<td>' + team["q1_Pending"] + '</td>'),
                    $('<td>' + team["q2_Total"] + '</td>'),
                    $('<td>' + team["q2_Approved"] + '</td>'),
                    $('<td>' + team["q2_Pending"] + '</td>'),
                    $('<td>' + team["q3_Total"] + '</td>'),
                    $('<td>' + team["q3_Approved"] + '</td>'),
                    $('<td>' + team["q3_Pending"] + '</td>'),
                    $('<td>' + team["q4_Total"] + '</td>'),
                    $('<td>' + team["q4_Approved"] + '</td>'),
                    $('<td>' + team["q4_Pending"] + '</td>'),
                );
                $tableBody.append($tr);
                //var grid = $('.grid-stack').data('gridstack');
                //grid.resize(
                //    $('.grid-stack-item')[0],
                //    $($('.grid-stack-item')[0]).attr('data-gs-width'),
                //    Math.ceil(($('#Team_Content').height() + 140 + grid.opts.verticalMargin) / (grid.cellHeight() + grid.opts.verticalMargin))
                //);
            }
        }
    }

    dEpartment();
    function addHoverEffect(chartContainer) {
        var originalTransform = chartContainer.css('transform');

        chartContainer.hover(
            function () {
                chartContainer.css('transform', 'scale(1.2)');
            },
            function () {
                chartContainer.css('transform', originalTransform);
            }
        );
    }

    $(document).ready(function () {
        addHoverEffect($('.card1'));
        addHoverEffect($('.card2'));
        addHoverEffect($('.card3'));
        addHoverEffect($('.card4'));
    });

    //function addClickEffect(chartContainer) {
    //    var originalTransform = chartContainer.css('transform');

    //    chartContainer.on('click', function () {
    //        // Toggle the scale on each click
    //        var currentTransform = chartContainer.css('transform');
    //        var newTransform = (currentTransform === originalTransform) ? 'scale(1.2)' : originalTransform;

    //        chartContainer.css('transform', newTransform);
    //    });
    //}

    //$(document).ready(function () {
    //    addClickEffect($('.card1'));
    //    addClickEffect($('.card2'));
    //    addClickEffect($('.card3'));
    //    addClickEffect($('.card4'));
    //});
    //var refreshtotalrate = function () {
    //    var _$Buyer = _$chartselBUyer.val();
    //    var _$Supplier = _$chartselSupplier.val();
    //    var _$Year = _$chartselYear.find('option:selected').text();
    //    var _$Month = _$chartselMonth.find('option:selected').text();
    //    _tenantDashboardService
    //        .gettotalrate({
    //            buyerId: _$Buyer,
    //            supplierId: _$Supplier,
    //            year: _$Year,
    //            month: _$Month,
    //            //rMGroupId: group
    //        })
    //        .done(function (result) {
    //            console.log(result)
    //        })
    //        .always(function () {
    //            abp.ui.clearBusy(_$Container);
    //        });

    //}
    //refreshtotalrate();
    //var CEContribution = function () {
    //    var Contribution = function (data) {
    //        var cpNames = [];
    //        var percentage = [];
    //        //for (var r = 0; r < data.length; r++) {
    //        //    if (!data[r].hasOwnProperty('totalAverage')) {
    //        //        continue;
    //        //    }

    //        //    cpNames.push(data[r].totalAverage);
    //        //}
    //        percentage.push(0);
    //        for (var r = 0; r < data.length; r++) {
    //            if (!data[r].hasOwnProperty('totalAverage')) {
    //                continue;
    //            }

    //            percentage.push(data[r].totalAverage);
    //        }
    //        console.log("CP Names:", cpNames);
    //        console.log("Percentage:", percentage);
    //        var labels = [
    //                "Castings",
    //                "Aluminium Base",
    //                "Aluminium Alloys",
    //                "Copper",
    //                "Cold-rolled steel",
    //                "Hot-rolled steel",
    //                "Forging Alloys",
    //                "Natural Rubber"
    //            ];

    //        var datavalue = {
    //            labels: labels,
    //            datasets: [{
    //                label: '% of Contribution by Cost Engineer',
    //                data: percentage,
    //                backgroundColor: ['rgb(0,103,127)', 'rgb(255,255,64)', 'rgb(0,86,106)', 'rgb(200,200,200)', 'rgb(0,122,147)', 'rgb(158,158,158)', 'rgb(121,174,191)', 'rgb(80,151,171)', 'rgb(166,202,216)']
    //            }]
    //        };

    //        // Configuration options
    //        var options = {
    //            responsive: true,
    //            maintainAspectRatio: false
    //        };

    //        // Create the doughnut chart
    //        var ctx = document.getElementById('m_chart_RM_value')
    //        var myDoughnutChart = new Chart(ctx, {
    //            type: 'pie',
    //            data: datavalue,
    //            options: options
    //        });
    //    };
    //    var getContributiondata = function () {
    //        var _$Buyer = _$chartselBUyer.val();
    //        var _$Supplier = _$chartselSupplier.val();
    //        var _$Year = _$chartselYear.find('option:selected').text();
    //        var _$Month = _$chartselMonth.find('option:selected').text();
    //        _tenantDashboardService
    //            .gettotalrate({
    //                buyerId: _$Buyer,
    //                supplierId: _$Supplier,
    //                year: _$Year,
    //                month: _$Month
    //            })
    //            .done(function (result) {
    //                console.log(result)
    //                Contribution(result);
    //            }).always(function () {
    //                //abp.ui.clearBusy(_$Container);
    //            });
    //    };
    //    getContributiondata();
    //}
});