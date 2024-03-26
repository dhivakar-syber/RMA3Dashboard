$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();
    var _$Container = $('.RMTonnageContainer');
    var _$teamcontainer = $(".rmtonnage-team-filter-container");
    var _$buyercontainer = $(".rmtonnage-buyer-filter-container");
    var _$suppliercontainer = $(".rmtonnage-supplier-filter-container");
    var _$yearcontainer = $(".rmtonnage-year-filter-container");
    var _$monthcontainer = $(".rmtonnage-month-filter-container");
    var _$gradecontainer = $(".rmtonnage-grade-filter-container");
    var _$groupcontainer = $(".rmtonnage-group-filter-container");
    var _$tonnageselTeams = _$teamcontainer.find('#tonnageselTeams');
    var _$tonnageselBUyer = _$buyercontainer.find('#tonnageselBUyer');
    var _$tonnageselSupplier = _$suppliercontainer.find('#tonnageselSupplier');
    var _$tonnageselYear = _$yearcontainer.find('#tonnageselYear');
    var _$tonnageselMonth = _$monthcontainer.find('#tonnageselMonth');
    var _$tonnageselGrade = _$gradecontainer.find('#tonnageselGrade');
    var _$tonnageselGroup = _$groupcontainer.find('#tonnageselGroup');
    var _$Buyer = _$tonnageselBUyer.val();
    var _$Supplier = _$tonnageselSupplier.val();
    var _$Year = _$tonnageselYear.find('option:selected').text();
    var _$Month = _$tonnageselMonth.find('option:selected').text();
    var groupArray = [47, 68, 69, 70, 71, 72, 73, 79];
    var group = ["Castings", "Aluminium Base", "Aluminium Alloys", "Copper", "Cold-rolled steel", "Hot-rolled steel", "Forging Alloys", "Natural Rubber"];
    var data = [];
    var grp = [];
    for (var i = 0; i < group.length; i++) {
        grp.push({ group: group[i] });
    }
    console.log(grp)
    var deptchanged = false;
    var yearchanged = false;
    var monthchanged = false;
    var tonsupplier = function () {
        var buyerId = _$tonnageselBUyer.val();
        console.log(buyerId)
        _tenantDashboardService
            .getSupplier(buyerId)
            .done(function (result) {
                _$tonnageselSupplier.empty();
                _$tonnageselSupplier.html('<option value="">All Supplier</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$tonnageselSupplier.append('<option value="' + result[rowIndex].supplierId + '">' + result[rowIndex].supplierName + " - " + result[rowIndex].supplierCode + '</option')
                }
            })

    }
    var tonbuyer = function () {
        department = _$rmcompselTeams.find('option:selected').text();
        _tenantDashboardService
            .getBuyersFromDepartment(department)
            .done(function (result) {
                _$tonnageselBUyer.html('<option value="0">All Buyer</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$tonnageselBUyer.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }

    var tonteams = function () {
        _tenantDashboardService
            .getDepartments()
            .done(function (result) {
                _$tonnageselTeams.html('<option value=" ">All Team</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$tonnageselTeams.append('<option value="' + result[rowIndex] + '">' + result[rowIndex] + '</option')
                }
            })
    }
    tonteams();
    var tongrade = function () {
        var gradeId = _$tonnageselGroup.val();
        _tenantDashboardService
            .getallgrade(gradeId)
            .done(function (result) {
                _$tonnageselGrade.html('<option value=" ">Select Grade</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$tonnageselGrade.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    var tongroup = function () {
        _tenantDashboardService
            .getgroup({})
            .done(function (result) {
                _$tonnageselGroup.html('<option value=" ">Select Group</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$tonnageselGroup.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    tongroup();

    var tonyear = function () {
        _tenantDashboardService
            .getYears({})
            .done(function (result) {
                _$tonnageselYear.html('<option value=" ">Select Year</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$tonnageselYear.append('<option value="' + rowIndex + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    tonyear();

    //var rmtonnage = function () {
    //    abp.ui.setBusy(_$Container);
    //    console.log("rmtonnage")
    //    var requestsDone = new Array(groupArray.length).fill(false);
    //    for (var i = 0; i < groupArray.length; i++) {
    //        makeRequest(groupArray[i], i, data, requestsDone)
    //    }
    //};

    //var makeRequest = function (group, rowIndex, data, requestsDone) {
    //    var _$Buyer = _$tonnageselBUyer.val();
    //    var _$Supplier = _$tonnageselSupplier.val();
    //    var _$Year = _$tonnageselYear.find('option:selected').text();  
    //    var _$Month = _$tonnageselMonth.find('option:selected').text();
    //    _tenantDashboardService
    //        .gettotalton({
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
    //                loadrmtonnage(data);
    //            }
    //        })

    //}
    //var loadrmtonnage = function (data) {
    //    console.log(data)
    //    for (var i = 0; i < _$Container.length; i++) {
    //        var container = $(_$Container[i]);
    //        var $tableBody = container.find('#RMtonnage_Content table tbody');
    //        $tableBody.html('');
    //        //var $rows = $tableBody.find('tr');

    //        for (var rowIndex = 0; rowIndex < data.length; rowIndex++) {
    //            var group = grp[rowIndex].group;
    //            var tonnage = data[rowIndex];
    //            var $tr = $('<tr></tr>').append(
    //                $('<td>' + group + '</td>'),
    //                //$('<td>' + parseFloat(tonnage[0].grossInputWeightAverage).toFixed(2) + '</td>'),
    //                //$('<td>' + parseFloat(tonnage[0].epuAverage).toFixed(2) + '</td>'),
    //                //$('<td>' + parseFloat(tonnage[0].sobAverage).toFixed(2) + '</td>'),
    //                $('<td>' + parseFloat(tonnage[0].totalAverage).toFixed(2) + '</td>'),
    //            );
    //            $tableBody.append($tr);
    //            //var $tr = $rows.eq(rowIndex);

    //            //$tr.find('td:eq(1)').text(parseFloat(tonnage[0].grossInputWeightAverage).toFixed(2));
    //            //$tr.find('td:eq(2)').text(parseFloat(tonnage[0].epuAverage).toFixed(2));
    //            //$tr.find('td:eq(3)').text(parseFloat(tonnage[0].sobAverage).toFixed(2));
    //            //$tr.find('td:eq(4)').text(parseFloat(tonnage[0].totalAverage).toFixed(2));
    //        }
    //    }
    //};

    //rmtonnage();

    $('#tonnagesubmit').click(function () {
        var buyer = _$tonnageselBUyer.val();
        var department = _$tonnageselTeams.val();
        var spec = _$tonnageselGrade.val();
        if (spec !== null) {
            tonnagechartbygrade()
        }
        if (deptchanged == true || yearchanged == true || monthchanged == true) {
            tonnagechart();
        }
        deptchanged = false;
        yearchanged = false;
        monthchanged = false;
    });
    $('#refresRMTonnageButton').click(function () {
        tonnagechart();
    });

    _$tonnageselTeams.change(function () {
        tonbuyer();
        deptchanged = true;
    })
    _$tonnageselBUyer.change(function () {
        tonsupplier();
    })
    _$tonnageselGroup.change(function () {
        tongrade();
    })
    _$tonnageselYear.change(function () {
        tonnagechart();
        //yearchanged = true;
    })
    _$tonnageselMonth.change(function () {
        monthchanged = true;
    })
    _$tonnageselGrade.change(function () {
        tonnagechartbygrade();
    })
    //_$tonnageselSupplier.change(function () {
    //    fyear();
    //})

    var tonnagechart = function () {
        console.log("chart")
        var _widgetBase = app.widgetBase.create();
        var _$Container = $('.RMTonnageContainer');
        var _$Chart = $('.groupchart-container')
        var piechart2Instance;
        //var tonnageValue = function (data) {
        //    console.log(data)
        //    var label = [];
        //    var Ton = [];
        //    var Value = [];
        //    //for (var r = 0; r < data.length; r++) {
        //    //    if (!data[r].hasOwnProperty('totalTon')) {
        //    //        continue;
        //    //    }

        //    //    Ton.push(data[r].totalTon);
        //    //}
        //    var Group = [
        //        "Castings",
        //        "Aluminum Base",
        //        "Aluminum Alloys",
        //        "Copper",
        //        "Cold-rolled Steel",
        //        "Hot-rolled Steel",
        //        "Forging Alloys",
        //        "Natural Rubber"
        //    ];
        //    for (var i = 0; i < Group.length; i++) {
        //        var found = false;
        //        for (var r = 0; r < data.length; r++) {
        //            if (/*data[r].hasOwnProperty('parentGroup') &&*/ data[r].parentGroup === Group[i]) {
        //                Ton.push(data[r].totalTon);
        //                var valueInCrore = (data[r].totalValue).toFixed(4) + " crore";
        //                Value.push(valueInCrore);
        //                found = true;
        //                break;
        //            }
        //        }
        //        if (!found) {
        //            Ton.push(0);
        //        }
        //    }
        //    console.log(Ton)

        //    var labels = [
        //        "Castings",
        //        "Aluminium Base",
        //        "Aluminium Alloys",
        //        "Copper",
        //        "Cold-rolled steel",
        //        "Hot-rolled steel",
        //        "Forging Alloys",
        //        "Natural Rubber"
        //    ];
        //    $.each(data, function (index, buyer) {
        //        var spec = buyer.parentGroup;
        //        var ton = buyer.totalTon
        //        var value = (buyer.totalValue).toFixed(4) + " crore";
        //        label.push(`${spec}  Total Ton :${ton}  Total Value :${value}`);
        //    })
        //    console.log(label)

        //    if (piechart2Instance) {
        //        piechart2Instance.destroy();
        //    }
        //    console.log(labels)

        //    for (var i = 0; i < _$Container.length; i++) {
        //        var chartContainer = $(_$Container[i]).find('#m_chart_RM_tonnage');
        //        new Chart(chartContainer, {
        //            type: 'pie',
        //            data: {
        //                datasets: [{
        //                    data: Ton,
        //                    backgroundColor: ['rgb(0,103,127)', 'rgb(255,255,64)', 'rgb(0,86,106)', 'rgb(200,200,200)', 'rgb(0,122,147)', 'rgb(158,158,158)', 'rgb(121,174,191)', 'rgb(80,151,171)', 'rgb(166,202,216)'],

        //                }],
        //                labels: labels,
        //            },
        //            options: {
        //                plugins: {
        //                    legend: {
        //                        display: true,
        //                    }
        //                },
        //                tooltips: {
        //                    xPadding: 10,
        //                    yPadding: 10,
        //                    caretPadding: 10
        //                },
        //                legend: {
        //                    display: true,
        //                    //position: 'right',
        //                    //labels: {
        //                    //    usePointStyle: true,
        //                    //    //fontColor: 'blue', // Change legend text color
        //                    //    fontSize: 10 // Change legend text size
        //                    //}
        //                },
        //                responsive: true,
        //                maintainAspectRatio: false,
        //            }
        //        });
        //    }
        //};
        //var tonnageValue = function (data) {
        //    console.log(data)
        //    var label = [];
        //    var Ton = [];
        //    var Value = [];
        //    var Group = [
        //        "Castings",
        //        "Aluminum Base",
        //        "Aluminum Alloys",
        //        "Copper",
        //        "Cold-rolled Steel",
        //        "Hot-rolled Steel",
        //        "Forging Alloys",
        //        "Natural Rubber"
        //    ];
        //    for (var i = 0; i < Group.length; i++) {
        //        var found = false;
        //        for (var r = 0; r < data.length; r++) {
        //            if (data[r].parentGroup === Group[i]) {
        //                Ton.push(data[r].totalTon);
        //                var valueInCrore = (data[r].totalValue).toFixed(4) + " crore";
        //                Value.push(valueInCrore);
        //                found = true;
        //                break;
        //            }
        //        }
        //        if (!found) {
        //            Ton.push(0);
        //        }
        //    }
        //    console.log(Ton)

        //    var labels = [
        //        "Castings",
        //        "Aluminium Base",
        //        "Aluminium Alloys",
        //        "Copper",
        //        "Cold-rolled steel",
        //        "Hot-rolled steel",
        //        "Forging Alloys",
        //        "Natural Rubber"
        //    ];
        //    data.forEach(function (buyer) {
        //        var spec = buyer.parentGroup;
        //        var ton = buyer.totalTon;
        //        var value = (buyer.totalValue).toFixed(4) + " crore";
        //        label.push(`${spec}  Total Ton :${ton}  Total Value :${value}`);
        //    })
        //    console.log(label)

        //    // Assuming _$Container is defined somewhere in your code
        //    for (var i = 0; i < _$Container.length; i++) {
        //        var chartContainer = $(_$Container[i]).find('#m_chart_RM_tonnage');

        //        // Prepare data for ECharts
        //        var chartData = [];
        //        labels.forEach(function (label, index) {
        //            chartData.push({
        //                value: Ton[index],
        //                name: label
        //            });
        //        });

        //        // Initialize ECharts instance
        //        var myChart = echarts.init(chartContainer[0]);

        //        // Set options for the pie chart
        //        var options = {
        //            tooltip: {
        //                trigger: 'item',
        //                formatter: "{a} <br/>{b}: {c} ({d}%)"
        //            },
        //            legend: {
        //                orient: 'vertical',
        //                left: 10,
        //                data: labels
        //            },
        //            series: [
        //                {
        //                    name: 'Tonnage',
        //                    type: 'pie',
        //                    radius: ['50%', '70%'],
        //                    avoidLabelOverlap: false,
        //                    label: {
        //                        show: false,
        //                        position: 'center'
        //                    },
        //                    emphasis: {
        //                        label: {
        //                            show: true,
        //                            fontSize: '30',
        //                            fontWeight: 'bold'
        //                        }
        //                    },
        //                    labelLine: {
        //                        show: false
        //                    },
        //                    data: chartData
        //                }
        //            ]
        //        };

        //        // Set options and render the chart
        //        myChart.setOption(options);
        //    }
        //};
        var tonnageValue = function (data) {
            console.log(data);
            var labels = [];
            var Impact = [];

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
                    if (data[r].parentGroup === Group[i]) {
                        labels.push(Group[i]);
                        Impact.push(data[r].totalTon);
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    Impact.push(0);
                }
            }
            console.log(Impact);

            var result = [];
            for (var i = 0; i < Group.length; i++) {
                if (Impact[i] !== 0) {
                    result.push({ value: Impact[i], name: Group[i] });
                }
            }
            result.sort(function (a, b) {
                return b.value - a.value;
            });

            console.log(result);

            var chartDom = document.getElementById('m_chart_RM_tonnage');
            var myChart = echarts.init(chartDom);

            var option = {
                title: {
                    text: 'RM Tonnage By Group', 
                    left: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: '{b}: {c} ({d}%)'
                },
                series: [{
                    type: 'pie',
                    radius: '50%',
                    data: result,
                    label: {
                        show: true,
                        formatter: '{b}: {c} ({d}%)'
                    },
                    //    [
                    //    { value: 1048, name: 'Search Engine' },
                    //    { value: 735, name: 'Direct' },
                    //    { value: 580, name: 'Email' },
                    //    { value: 484, name: 'Union Ads' },
                    //    { value: 300, name: 'Video Ads' }
                    //],
                    emphasis: {
                        itemStyle: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }]
            };

            myChart.setOption(option);
        }


        var gettonnageTrend = function () {
            var _$Buyer = _$tonnageselBUyer.val();
            var _$Supplier = _$tonnageselSupplier.val();
            var _$Department = _$tonnageselTeams.val();
            var _$Year = _$tonnageselYear.find('option:selected').text();
            var _$Month = _$tonnageselMonth.find('option:selected').text();
            abp.ui.setBusy(_$Chart);
            _tenantDashboardService
                .gettotalton({
                    buyerId: _$Buyer,
                    department: _$Department,
                    supplierId: _$Supplier,
                    year: _$Year,
                    month: _$Month
                })
                .done(function (result) {
                    console.log(result);
                    tonnageValue(result);
                    loadtonnagevalue(result);
                })
                .always(function () {
                    abp.ui.clearBusy(_$Chart);
                });
        };
        _widgetBase.runDelayed(gettonnageTrend);

        $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
            _widgetBase.runDelayed(gettonnageTrend);
        });
    };
    var loadtonnagevalue = function (result) {
        console.log('tonnage');
        result.sort(function (a, b) {
            return b.totalTon - a.totalTon;
        });
        for (var i = 0; i < _$Container.length; i++) {
            var container = $(_$Container[i]);
            var $tableBody = container.find('#group_Content table tbody');
            $tableBody.html('');
            for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                var impact = result[rowIndex];
                if (impact["parentGroup"] !== '') {
                    var $tr = $('<tr></tr>').append(
                        $('<td style="text-align: left;">' + impact["parentGroup"] + '</td>'),
                        $('<td style="text-align: right;">' + Math.round(impact["totalTon"]) + '</td>'),
                        $('<td style="text-align: right;">' + impact["totalValue"].toFixed(3) + '</td>'),
                    );
                    $tableBody.append($tr);
                }
            }
        }
    }
    var tonnagechartbygrade = function () {
        console.log("chart")
        var _widgetBase = app.widgetBase.create();
        var _$Container = $('.RMTonnageContainer');
        var _$Chart1 = $('.gradechart-container')
        var piechart2Instance;
        //var tonnageValuebygrdae = function (data) {
        //    console.log(data)
        //    var label = [];
        //    var specArray = [];
        //    var totalTonByGradeArray = [];
        //    var totalValueByGradeArray = [];
        //    for (var d = 0; d < data.length; d++) {
        //        if (!data[d].hasOwnProperty('spec')) {
        //            continue;
        //        }
        //        specArray.push(data[d].spec);
        //    }

        //    for (var d = 0; d < data.length; d++) {
        //        if (!data[d].hasOwnProperty('totalTonByGrade')) {
        //            continue;
        //        }
        //        totalTonByGradeArray.push(data[d].totalTonByGrade);
        //    }
        //    console.log(totalTonByGradeArray)

        //    for (var d = 0; d < data.length; d++) {
        //        if (!data[d].hasOwnProperty('totalValueByGrade')) {
        //            continue;
        //        }
        //        var valueInCrore = (data[d].totalValueByGrade).toFixed(4) + " crore";
        //        totalValueByGradeArray.push(valueInCrore);
        //    }
        //    console.log(totalValueByGradeArray);
        //    $.each(data, function (index, buyer) {
        //        var spec = buyer.spec;
        //        var ton = buyer.totalTonByGrade
        //        var value = (buyer.totalValueByGrade).toFixed(4) + " crore";
        //        label.push( `${spec}  Total Ton :${ton}  Total Value :${value}`);
        //    })
        //    console.log(label)

        //    if (piechart2Instance) {
        //        piechart2Instance.destroy();
        //    }
        //    console.log(specArray)

        //    for (var i = 0; i < _$Container.length; i++) {
        //        var chartContainer = $(_$Container[i]).find('#m_chart_RM_tonnagebygrade');
        //        new Chart(chartContainer, {
        //            type: 'pie',
        //            data: {
        //                datasets: [{
        //                    data: totalTonByGradeArray,
        //                    backgroundColor: ['rgb(0,103,127)', 'rgb(255,255,64)', 'rgb(0,86,106)', 'rgb(200,200,200)', 'rgb(0,122,147)', 'rgb(158,158,158)', 'rgb(121,174,191)', 'rgb(80,151,171)', 'rgb(166,202,216)',
        //                        'rgb(157,34,53)', 'rgb(255,198,0)', 'rgb(0,155,119)', 'rgb(152, 115, 172)', 'rgb(112,128,144)', 'rgb(0,123,167)', 'rgb(100,50,32)','rgb(72, 60, 50)'],
        //                }],
        //                labels: specArray,
        //            },
        //            options: {
        //                plugins: {
        //                    legend: {
        //                        display: true,
        //                    }
        //                },
        //                tooltips: {
        //                    xPadding: 10,
        //                    yPadding: 10,
        //                    caretPadding: 10
        //                },
        //                legend: {
        //                    display: true,
        //                    //position: 'right',
        //                    //labels: {
        //                    //    usePointStyle: true,
        //                    //    //fontColor: 'blue', // Change legend text color
        //                    //    fontSize: 10 // Change legend text size
        //                    //}
        //                },
        //                responsive: true,
        //                maintainAspectRatio: false,
        //            }
        //        });
        //    }
        //};
        //var tonnageValuebygrdae = function (data) {
        //    console.log(data);
        //    var label = [];
        //    var specArray = [];
        //    var totalTonByGradeArray = [];
        //    var totalValueByGradeArray = [];

        //    // Extracting spec data
        //    for (var d = 0; d < data.length; d++) {
        //        if (data[d].hasOwnProperty('spec')) {
        //            specArray.push(data[d].spec);
        //        }
        //    }

        //    // Extracting totalTonByGrade data
        //    for (var d = 0; d < data.length; d++) {
        //        if (data[d].hasOwnProperty('totalTonByGrade')) {
        //            totalTonByGradeArray.push(data[d].totalTonByGrade);
        //        }
        //    }
        //    console.log(totalTonByGradeArray);

        //    // Extracting totalValueByGrade data
        //    for (var d = 0; d < data.length; d++) {
        //        if (data[d].hasOwnProperty('totalValueByGrade')) {
        //            var valueInCrore = (data[d].totalValueByGrade).toFixed(4) + " crore";
        //            totalValueByGradeArray.push(valueInCrore);
        //        }
        //    }
        //    console.log(totalValueByGradeArray);

        //    // Creating labels
        //    data.forEach(function (buyer) {
        //        var spec = buyer.spec;
        //        var ton = buyer.totalTonByGrade;
        //        var value = (buyer.totalValueByGrade).toFixed(4) + " crore";
        //        label.push(`${spec}  Total Ton :${ton}  Total Value :${value}`);
        //    });
        //    console.log(label);

        //    // Destroy existing chart instance if exists
        //    if (piechart2Instance) {
        //        piechart2Instance.dispose();
        //    }
        //    console.log(specArray);

        //    // Assuming _$Container is defined somewhere in your code
        //    for (var i = 0; i < _$Container.length; i++) {
        //        var chartContainer = $(_$Container[i]).find('#m_chart_RM_tonnagebygrade');

        //        // Prepare data for ECharts
        //        var chartData = [];
        //        specArray.forEach(function (spec, index) {
        //            chartData.push({
        //                value: totalTonByGradeArray[index],
        //                name: spec
        //            });
        //        });

        //        // Initialize ECharts instance
        //        var myChart = echarts.init(chartContainer[0]);

        //        // Set options for the pie chart
        //        var options = {
        //            tooltip: {
        //                trigger: 'item',
        //                formatter: "{a} <br/>{b}: {c} ({d}%)"
        //            },
        //            legend: {
        //                orient: 'vertical',
        //                left: 10,
        //                data: specArray
        //            },
        //            series: [
        //                {
        //                    name: 'Tonnage by Grade',
        //                    type: 'pie',
        //                    radius: ['50%', '70%'],
        //                    avoidLabelOverlap: false,
        //                    label: {
        //                        show: false,
        //                        position: 'center'
        //                    },
        //                    emphasis: {
        //                        label: {
        //                            show: true,
        //                            fontSize: '30',
        //                            fontWeight: 'bold'
        //                        }
        //                    },
        //                    labelLine: {
        //                        show: false
        //                    },
        //                    data: chartData
        //                }
        //            ]
        //        };

        //        // Set options and render the chart
        //        myChart.setOption(options);
        //    }
        //};
        //var tonnageValuebygrdae = function (data) {
        //    console.log(data);
        //    var label = [];
        //    var specArray = [];
        //    var totalTonByGradeArray = [];
        //    var totalValueByGradeArray = [];

        //    // Extracting spec data
        //    for (var d = 0; d < data.length; d++) {
        //        if (data[d].hasOwnProperty('spec')) {
        //            specArray.push(data[d].spec);
        //        }
        //    }

        //    // Extracting totalTonByGrade data
        //    for (var d = 0; d < data.length; d++) {
        //        if (data[d].hasOwnProperty('totalTonByGrade')) {
        //            totalTonByGradeArray.push(data[d].totalTonByGrade);
        //        }
        //    }
        //    console.log(totalTonByGradeArray);

        //    // Extracting totalValueByGrade data
        //    for (var d = 0; d < data.length; d++) {
        //        if (data[d].hasOwnProperty('totalValueByGrade')) {
        //            var valueInCrore = (data[d].totalValueByGrade).toFixed(4) + " crore";
        //            totalValueByGradeArray.push(valueInCrore);
        //        }
        //    }
        //    console.log(totalValueByGradeArray);

        //    // Creating labels
        //    data.forEach(function (buyer) {
        //        var spec = buyer.spec;
        //        var ton = buyer.totalTonByGrade;
        //        var value = (buyer.totalValueByGrade).toFixed(4) + " crore";
        //        label.push(`${spec}  Total Ton :${ton}  Total Value :${value}`);
        //    });
        //    console.log(label);

        //    // Destroy existing chart instance if exists
        //    if (piechart2Instance) {
        //        piechart2Instance.dispose();
        //    }
        //    console.log(specArray);

        //    // Assuming _$Container is defined somewhere in your code
        //    for (var i = 0; i < _$Container.length; i++) {
        //        var chartContainer = $(_$Container[i]).find('#m_chart_RM_tonnagebygrade');

        //        // Prepare data for ECharts
        //        var chartData = [];
        //        specArray.forEach(function (spec, index) {
        //            chartData.push({
        //                value: totalTonByGradeArray[index],
        //                name: spec
        //            });
        //        });

        //        // Initialize ECharts instance
        //        var myChart = echarts.init(chartContainer[0]);

        //        // Set options for the pie chart
        //        var options = {
        //            tooltip: {
        //                trigger: 'item',
        //                formatter: "{a} <br/>{b}: {c} ({d}%)"
        //            },
        //            legend: {
        //                orient: 'vertical',
        //                left: 10,
        //                data: specArray
        //            },
        //            series: [
        //                {
        //                    name: 'Tonnage by Grade',
        //                    type: 'pie',
        //                    //radius: ['50%', '70%'],
        //                    avoidLabelOverlap: false,
        //                    label: {
        //                        show: false,
        //                        position: 'center'
        //                    },
        //                    emphasis: {
        //                        label: {
        //                            show: true,
        //                            fontSize: '30',
        //                            fontWeight: 'bold'
        //                        }
        //                    },
        //                    labelLine: {
        //                        show: true, // Show label line
        //                        length: 10, // Length of label line
        //                        length2: 10 // Length of the second section of label line
        //                    },
        //                    data: chartData
        //                }
        //            ]
        //        };

        //        // Set options and render the chart
        //        myChart.setOption(options);
        //    }
        //};
        var tonnageValuebygrdae = function (data) {
            console.log(data);
            var labels = [];
            var Impact = [];

            for (var d = 0; d < data.length; d++) {
                if (data[d].hasOwnProperty('spec')) {
                    labels.push(data[d].spec);
                }
            }
            console.log(labels);

            for (var d = 0; d < data.length; d++) {
                if (data[d].hasOwnProperty('totalTonByGrade')) {
                    Impact.push(data[d].totalTonByGrade);
                }
            }
            console.log(Impact);

            var result = [];
            for (var i = 0; i < labels.length; i++) {
                result.push({ value: Impact[i], name: labels[i] });
            }
            result.sort(function (a, b) {
                return b.value - a.value;
            });
            console.log(result);

            var chartDom = document.getElementById('m_chart_RM_tonnage');
            var myChart = echarts.init(chartDom);

            var option = {
                title: {
                    text: 'RM Tonnage By Grade',
                    left: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: '{b}: {c} ({d}%)'
                },
                series: [{
                    type: 'pie',
                    radius: '50%',
                    data: result,
                    label: {
                        show: true,
                        formatter: '{b}: {c} ({d}%)' 
                    },
                    //    [
                    //    { value: 1048, name: 'Search Engine' },
                    //    { value: 735, name: 'Direct' },
                    //    { value: 580, name: 'Email' },
                    //    { value: 484, name: 'Union Ads' },
                    //    { value: 300, name: 'Video Ads' }
                    //],
                    emphasis: {
                        itemStyle: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }]
            };

            myChart.setOption(option);
        }

        var gettonnageTrend = function () {
            var _$Buyer = _$tonnageselBUyer.val();
            var _$Supplier = _$tonnageselSupplier.val();
            var _$Department = _$tonnageselTeams.val();
            var _$Year = _$tonnageselYear.find('option:selected').text();
            var _$Month = _$tonnageselMonth.find('option:selected').text();
            var _$Grade = _$tonnageselGrade.val();
            abp.ui.setBusy(_$Chart1);
            _tenantDashboardService
                .gettotaltonbygroup({
                    buyerId: _$Buyer,
                    department: _$Department,
                    supplierId: _$Supplier,
                    rMGradeId: _$Grade,
                    year: _$Year,
                    month: _$Month
                })
                .done(function (result) {
                    console.log(result);
                    tonnageValuebygrdae(result);
                    loadtonnagevaluebygrade(result);
                })
                .always(function () {
                    abp.ui.clearBusy(_$Chart1);
                });
        };
        _widgetBase.runDelayed(gettonnageTrend);

        $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
            _widgetBase.runDelayed(gettonnageTrend);
        });
    };
    var loadtonnagevaluebygrade = function (result) {
        console.log('tonnage');
        result.sort(function (a, b) {
            return b.totalTonByGrade - a.totalTonByGrade;
        });
        for (var i = 0; i < _$Container.length; i++) {
            var container = $(_$Container[i]);
            var $tableBody = container.find('#group_Content table tbody');
            $tableBody.html('');
            for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                var impact = result[rowIndex];
                var $tr = $('<tr></tr>').append(
                    $('<td style="text-align: left;">' + impact["spec"] + '</td>'),
                    $('<td style="text-align: right;">' + Math.round(impact["totalTonByGrade"]) + '</td>'),
                    $('<td style="text-align: right;">' + impact["totalValueByGrade"].toFixed(3) + '</td>'),
                );
                $tableBody.append($tr);
            }
        }
    }
});