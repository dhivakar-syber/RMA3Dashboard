$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();
    var _$buyercontainer = $(".team-buyer-filter-container");
    var _$teamcontainer = $(".team-filter-container");
    var _$yearcontainer = $(".team-year-filter-container");
    var _$teamselBUyer = _$buyercontainer.find('#teamselBUyer');
    var _$teamselTeams = _$teamcontainer.find('#teamselTeams');
    var _$teamselYears = _$yearcontainer.find('#teamselyear');
    var _$Container = $('.TeamsContainer');
    var tbuyer = function () {
        department = _$teamselTeams.find('option:selected').text();
        _tenantDashboardService
            .getBuyersFromDepartment(department)
            .done(function (result) {
                _$teamselBUyer.html('<option value="0">Select Buyer</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$teamselBUyer.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }

    var teams = function () {
        _tenantDashboardService
            .getDepartments()
            .done(function (result) {
                _$teamselTeams.html('<option value=" ">All Team</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$teamselTeams.append('<option value="' + result[rowIndex] + '">' + result[rowIndex] + '</option')
                }
            })
    }
    teams();
    var years = function () {

        _tenantDashboardService
            .getYears({})
            .done(function (result) {
                _$teamselYears.html('<option value=" ">Select Year</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$teamselYears.append('<option value="' + rowIndex + '">' + result[rowIndex].name + '</option')
                }
                _$teamselYears.val("4");
            })
    }
    years();

    

    var dEpartment = function () {
        var _$Buyer = _$teamselBUyer.val();
        var _$Department = _$teamselTeams.val();
        var _$Year = _$teamselYears.find('option:selected').text();
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
            var $tableBody = container.find('#Teams_Content table tbody');
            $tableBody.html('');
            for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                var team = result[rowIndex];
                var q1_Pending_Percentage = team["q1_Total"] !== 0 ? Math.round((team["q1_Pending"] / team["q1_Total"]) * 100) : 0;
                var q2_Pending_Percentage = team["q2_Total"] !== 0 ? Math.round((team["q2_Pending"] / team["q2_Total"]) * 100) : 0;
                var q3_Pending_Percentage = team["q3_Total"] !== 0 ? Math.round((team["q3_Pending"] / team["q3_Total"]) * 100) : 0;
                var q4_Pending_Percentage = team["q4_Total"] !== 0 ? Math.round((team["q4_Pending"] / team["q4_Total"]) * 100) : 0;

                var $tr = $('<tr></tr>').append(
                    $('<td>' + team["department"] + '</td>'),
                    $('<td>' + team["q1_Total"] + '</td>'),
                    $('<td>' + team["q1_Approved"] + '</td>'),
                    $('<td>' + team["q1_Pending"] + '</td>'),
                    $('<td>' + q1_Pending_Percentage+'%' + '</td>'),
                    $('<td>' + team["q2_Total"] + '</td>'),
                    $('<td>' + team["q2_Approved"] + '</td>'),
                    $('<td>' + team["q2_Pending"] + '</td>'),
                    $('<td>' + q2_Pending_Percentage + '%' + '</td>'),
                    $('<td>' + team["q3_Total"] + '</td>'),
                    $('<td>' + team["q3_Approved"] + '</td>'),
                    $('<td>' + team["q3_Pending"] + '</td>'),
                    $('<td>' + q3_Pending_Percentage + '%' + '</td>'),
                    $('<td>' + team["q4_Total"] + '</td>'),
                    $('<td>' + team["q4_Approved"] + '</td>'),
                    $('<td>' + team["q4_Pending"] + '</td>'),
                    $('<td>' + q4_Pending_Percentage + '%' + '</td>'),
                );
                $tableBody.append($tr);
            }
        }
    }

    dEpartment();

    $('#teamsubmit').click(function () {
        dEpartment();
        //teamgraph();
    });
    _$teamselTeams.change(function () {
        tbuyer();
    })

    $('#refresTeamButton').click(function () {
        dEpartment();
        //teamgraph();
    });
    //var teamgraph = function () {
    //    var _widgetBase = app.widgetBase.create();
    //    var _$Container = $('.TeamsContainer');
    //    var PriceTrend = function (data) {
    //        var department = [];
    //        for (var d = 0; d < data.length; d++) {
    //            if (!data[d].hasOwnProperty('department')) {
    //                continue;
    //            }
    //            department.push(data[d].department);
    //        }
    //        console.log(department);

    //        var quarters = ['Qtr1', 'Qtr2', 'Qtr3', 'Qtr4'];

    //        var actualData = [];

    //        for (var i = 0; i < data.length; i++) {
    //            var rowData = [];
    //            for (var j = 1; j <= 4; j++) {
    //                var total = data[i]["q" + j + "_Total"];
    //                var approved = data[i]["q" + j + "_Approved"];
    //                var pending = data[i]["q" + j + "_Pending"];
    //                rowData.push(total);
    //                rowData.push(approved);
    //                rowData.push(pending);
    //            }
    //            actualData.push(rowData);
    //        }

    //        console.log(actualData);

    //        var datasets = [];
    //        for (var i = 0; i < quarters.length; i++) {
    //            var label = quarters[i];
    //            var data = [];
    //            var data2 = [];
    //            var data3 = [];
    //            for (var j = 0; j < department.length; j++) {
    //                var row = actualData[j];
    //                var actual = row[i * 3];
    //                var approved = row[i * 3 + 1];
    //                var pending = row[i * 3 + 2];
    //                data.push(actual);
    //                data2.push(approved);
    //                data3.push(pending);
    //            }
    //            datasets.push({
    //                label: label + " Actual",
    //                data: data,
    //                backgroundColor: "blue",
    //                stack: 'stack' + i
    //            });
    //            datasets.push({
    //                label: label + " Approved",
    //                data: data2,
    //                backgroundColor: "green",
    //                stack: 'stack' + i
    //            });
    //            datasets.push({
    //                label: label + " Pending",
    //                data: data3,
    //                backgroundColor: "red",
    //                stack: 'stack' + i
    //            });
    //        }
    //        console.log(datasets)

    //        for (var i = 0; i < _$Container.length; i++) {
    //            var chartContainer = $(_$Container[i]).find('#m_chart_Teams');
    //            new Chart(chartContainer, {
    //                type: 'bar',  
    //                data: {
    //                    labels: department,
    //                    datasets: datasets
    //                },
    //                options: {
    //                    title: {
    //                        display: false
    //                    },
    //                    legend: {
    //                        display: true
    //                    },
    //                    responsive: true,
    //                    maintainAspectRatio: false,
    //                    barThickness: 20,   
    //                    scales: {
    //                        xAxes: [{
    //                            stacked: true
    //                        }],
    //                        yAxes: [{  
    //                            stacked: true
    //                        }]
    //                    },
    //                    layout: {
    //                        padding: {
    //                            left: 0,
    //                            right: 0,
    //                            top: 0,
    //                            bottom: 0
    //                        }
    //                    }
    //                }
    //            });
    //        }

    //    };

    //    var getPriceTrend = function () {
    //        var _$Buyer = _$teamselBUyer.val();
    //        var _$Department = _$teamselTeams.val();
    //        abp.ui.setBusy(_$Container);
    //        _tenantDashboardService
    //            .getDepartmentQuarterSummary({
    //                buyerId: _$Buyer,
    //                department: _$Department
    //            })
    //            .done(function (result) {
    //                PriceTrend(result);
    //            }).always(function () {
    //                abp.ui.clearBusy(_$Container);
    //            });
    //    };

    //    _widgetBase.runDelayed(getPriceTrend);

    //    $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
    //        _widgetBase.runDelayed(getPriceTrend);
    //    });
    //}
    //teamgraph();
});