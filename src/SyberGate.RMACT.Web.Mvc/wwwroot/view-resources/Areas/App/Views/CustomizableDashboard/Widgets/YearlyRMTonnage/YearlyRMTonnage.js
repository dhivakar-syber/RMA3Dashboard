$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();
    var _$Container = $('.YearlyRMTonnageContainer');
    var _$groupcontainer = $(".yearlyrmt-group-filter-container");
    var _$yearcontainer = $(".yearlyrmt-year-filter-container");
    var _$yearlytselGroup = _$groupcontainer.find('#yearlytselGroup');
    var _$yearlytselYear = _$yearcontainer.find('#yearlytselYear');

    var yearlytgroup = function () {
        _tenantDashboardService
            .getgroup({})
            .done(function (result) {
                _$yearlytselGroup.html('<option value=" ">Select Group</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$yearlytselGroup.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    yearlytgroup();
    var yearlyt = function () {
        _tenantDashboardService
            .getYears({})
            .done(function (result) {
                _$yearlytselYear.html('<option value=" ">Select Year</option')
                for (var rowIndex = 1; rowIndex < result.length; rowIndex++) {
                    _$yearlytselYear.append('<option value="' + rowIndex + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    yearlyt();
    $('#yearlyrmtsubmit').click(function () {
        var yearValue = $('#yearlytselYear').val();
        var groupValue = $('#yearlytselGroup').val();
        if (yearValue !== " " && groupValue !== " ") {
            yearlyrmtgraph();
        } else if (yearValue === " " && groupValue === " ") {
            alert("Select both Group and Year");
        } else if (yearValue === " ") {
            alert("Select Year");
        } else if (groupValue === " ") {
            alert("Select Group");
        }
    });

    var yearlyrmtgraph = function () {
        var _widgetBase = app.widgetBase.create();
        var _$Container = $('.YearlyRMTonnageContainer');
        var PriceTrend = function (data) {
            console.log(data)
            var propertyValues = Object.values(data["0"]);
            var numericValues = propertyValues.filter(function (value) {
                return $.isNumeric(value);
            });

            console.log(numericValues);

            var data = {
                labels: [
                    'January',
                    'February',
                    'March',
                    'April',
                    'May',
                    'June',
                    'July',
                    'August',
                    'September',
                    'October',
                    'November',
                    'December'
                ],
                datasets: [{
                    type: 'bar',
                    label: 'Bar Dataset',
                    data: numericValues,
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)'
                }, {
                    type: 'line',
                    label: 'Line Dataset',
                    data: numericValues,
                    fill: false,
                    borderColor: 'rgb(54, 162, 235)'
                }]
            };

            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#m_chart_YearlyRM_tonnage');
                new Chart(chartContainer, {
                    type: 'bar',
                    data: data,
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });
            }
        };

        var getPriceTrend = function () {
            var _$Group = _$yearlytselGroup.val();
            var _$Year = _$yearlytselYear.find('option:selected').text();
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .gettonmonthly({
                    rMGroupId: _$Group,
                    year: _$Year
                })
                .done(function (result) {
                    PriceTrend(result);
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        };

        _widgetBase.runDelayed(getPriceTrend);

        $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
            _widgetBase.runDelayed(getPriceTrend);
        });
    }

    //var tonchart = function () {
    //    console.log("chart")
    //    var data = {
    //        labels: [
    //            'January',
    //            'February',
    //            'March',
    //            'April',
    //            'May',
    //            'June',
    //            'July'
    //        ],
    //        datasets: [{
    //            type: 'bar',
    //            label: 'Bar Dataset',
    //            data: [10, 20, 30, 40, 30, 50, 45],
    //            borderColor: 'rgb(255, 99, 132)',
    //            backgroundColor: 'rgba(255, 99, 132, 0.2)'
    //        }, {
    //            type: 'line',
    //            label: 'Line Dataset',
    //            data: [10, 20, 30, 40, 30, 50, 45],
    //            fill: false,
    //            borderColor: 'rgb(54, 162, 235)'
    //        }]
    //    };

    //    for (var i = 0; i < _$Container.length; i++) {
    //        var chartContainer = $(_$Container[i]).find('#m_chart_YearlyRM_tonnage');

    //        new Chart(chartContainer, {
    //            type: 'bar',
    //            data: data,
    //            options: {
    //                scales: {
    //                    y: {
    //                        beginAtZero: true
    //                    }
    //                }
    //            }
    //        });
    //    }
    //};
    //tonchart();
})