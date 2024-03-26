$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();
    var _$Container = $('.YearlyRMPriceContainer');
    var _$groupcontainer = $(".yearlyrmp-group-filter-container");
    var _$yearcontainer = $(".yearlyrmp-year-filter-container");
    var _$yearlyselGroup = _$groupcontainer.find('#yearlypselGroup');
    var _$yearlyselYear = _$yearcontainer.find('#yearlypselYear');

    var yearlygroup = function () {
        _tenantDashboardService
            .getgroup({})
            .done(function (result) {
                _$yearlyselGroup.html('<option value=" ">Select Group</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$yearlyselGroup.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    yearlygroup();
    var yearly = function () {
        _tenantDashboardService
            .getYears({})
            .done(function (result) {
                _$yearlyselYear.html('<option value=" ">Select Year</option')
                for (var rowIndex = 1; rowIndex < result.length; rowIndex++) {
                    _$yearlyselYear.append('<option value="' + rowIndex + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    yearly();
    $('#yearlyrmpsubmit').click(function () {
        var yearValue = $('#yearlypselYear').val();
        var groupValue = $('#yearlypselGroup').val();
        if (yearValue !== " " && groupValue !== " ") {
            yearlyrmpgraph();
        } else if (yearValue === " " && groupValue === " ") {
            alert("Select both Group and Year");
        } else if (yearValue === " ") {
            alert("Select Year");
        } else if (groupValue === " ") {
            alert("Select Group");
        }
    });

    var yearlyrmpgraph = function () {
        var _widgetBase = app.widgetBase.create();
        var _$Container = $('.YearlyRMPriceContainer');
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
                var chartContainer = $(_$Container[i]).find('#m_chart_YearlyRM_value');
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
            var _$Group = _$yearlyselGroup.val();
            var _$Year = _$yearlyselYear.find('option:selected').text();
            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getpricemonthly({
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
})