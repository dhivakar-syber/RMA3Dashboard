$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();
    var _$Container = $('.RMComparisonContainer');
    var _$groupcontainer = $(".rmcomp-group-filter-container");
    var _$yearcontainer = $(".rmcomp-year-filter-container");
    var _$teamcontainer = $(".rmcomp-team-filter-container");
    var _$suppliercontainer = $(".rmcomp-supplier-filter-container");
    var _$buyercontainer = $(".rmcomp-buyer-filter-container");
    var _$gradecontainer = $(".rmcomp-grade-filter-container");
    var _$speccontainer = $(".rmcomp-spec-filter-container");
    var _$rmcompselGroup = _$groupcontainer.find('#rmcompselGroup');
    var _$rmcompselYear = _$yearcontainer.find('#rmcompselYear');
    var _$rmcompselTeams = _$teamcontainer.find('#rmcompselTeams');
    var _$rmcompselSupplier = _$suppliercontainer.find('#rmcompselSupplier');
    var _$rmcompselBUyer = _$buyercontainer.find('#rmcompselBUyer');
    var _$rmcompselGrade = _$gradecontainer.find('#rmcompselGrade');
    var _$rmcompselSpec = _$speccontainer.find('#rmcompselSpec');
    var Data;
    

    var compsupplier = function () {
        var buyerId = _$rmcompselBUyer.val();
        console.log(buyerId)
        _tenantDashboardService
            .getSupplier(buyerId)
            .done(function (result) {
                _$rmcompselSupplier.empty();
                _$rmcompselSupplier.html('<option value="">All Supplier</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$rmcompselSupplier.append('<option value="' + result[rowIndex].supplierId + '">' + result[rowIndex].supplierName + " - " + result[rowIndex].supplierCode + '</option')
                }
            })

    }
    var compbuyer = function () {
        department = _$rmcompselTeams.find('option:selected').text();
        _tenantDashboardService
            .getBuyersFromDepartment(department)
            .done(function (result) {
                _$rmcompselBUyer.html('<option value="0">All Buyer</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$rmcompselBUyer.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }

    var compteams = function () {
        _tenantDashboardService
            .getDepartments()
            .done(function (result) {
                _$rmcompselTeams.html('<option value=" ">All Team</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$rmcompselTeams.append('<option value="' + result[rowIndex] + '">' + result[rowIndex] + '</option')
                }
            })
    }
    compteams();

    var compyear = function () {
        _tenantDashboardService
            .getYears({})
            .done(function (result) {
                _$rmcompselYear.html('<option value=" ">Select Year</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$rmcompselYear.append('<option value="' + rowIndex + '">' + result[rowIndex].name + '</option')
                }
                
            })
    }
    compyear();

    var compgroup = function () {
        var _$Buyer = _$rmcompselBUyer.val();
        var _$Supplier = _$rmcompselSupplier.val();
        _tenantDashboardService
            .getgroup()
            .done(function (result) {
                _$rmcompselGroup.html('<option value=" ">Select Group</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$rmcompselGroup.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    compgroup();
    var compgrade = function () {
        var gradeId = _$rmcompselGroup.val();
        _tenantDashboardService
            .getallgrade(gradeId)
            .done(function (result) {
                _$rmcompselGrade.html('<option value=" ">Select Grade</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$rmcompselGrade.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    
    
    var compspec = function () {
        var groupId = _$rmcompselGroup.val();
        var gradeId = _$rmcompselGrade.val();
        _tenantDashboardService
            .getAllSpec(groupId, gradeId)
            .done(function (result) {
                _$rmcompselSpec.html('<option value=" ">Select Spec</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$rmcompselSpec.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
            })
    }
    
    _$rmcompselTeams.change(function () {
        compbuyer();
    })

    _$rmcompselBUyer.change(function () {
        compsupplier();
    })
    _$rmcompselSupplier.change(function () {
        compgroup();
    })
    _$rmcompselGroup.change(function () {
        compgrade();
        compspec();
    })
    _$rmcompselGrade.change(function () {
        compspec();
    })
    _$rmcompselSpec.change(function () {
        compchart(); 
    })
    $('#ShowAdvancedFiltersSpan').click(function () {
        $('#ShowAdvancedFiltersSpan').hide();
        $('#HideAdvancedFiltersSpan').show();
        $('#AdvacedAuditFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
        $('#HideAdvancedFiltersSpan').hide();
        $('#ShowAdvancedFiltersSpan').show();
        $('#AdvacedAuditFiltersArea').slideUp();
    });
    $('#rmcompsubmit').click(function () {
        var yearValue = $('#compselYear').val();
        var groupValue = $('#rmcompselSpec').val();
        if (yearValue !== " " && groupValue !== " ") {
            
        } else if (yearValue === " " && groupValue === " ") {
            alert("Select both Group and Year");
        } else if (yearValue === " ") {
            alert("Select Year");
        } else if (groupValue === " ") {
            alert("Select Spec");
        }
    });
    var compchart = function () {
        var _widgetBase = app.widgetBase.create();
        var _$Container = $('.RMComparisonContainer');
        var PriceTrend = function (data) {
            console.log(data);

            var labels = [
                'Jan',
                'Feb',
                'Mar',
                'Apr',
                'May',
                'Jun',
                'Jul',
                'Aug',
                'Sep',
                'Oct',
                'Nov',
                'Dec'
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
            datasets = [];
            data.forEach(function (buyer) {
                var buyerName = buyer.buyerName;
                var suppliername = buyer.suppliername;
                var spec = buyer.spec;
                var label = buyerName + '-' + suppliername;
                var color = getRandomColor();
                var values = [];

                for (var month in buyer) {
                    if (month !== "buyerName" && month !== "suppliername" && month !== "spec" && month !== "team" && month !== "weightedAverageEPU" && month !== "unitRateAverage") {
                        values.push(buyer[month]);
                    }
                }
                console.log(values)
                datasets.push({
                    name: label,
                    type: 'line',
                    data: values,
                    smooth: true,
                    lineStyle: {
                        width: 2,
                    },
                    itemStyle: {
                        color: color,
                    }
                });
            });
            console.log(datasets)
            var option = {
                xAxis: {
                    type: 'category',
                    data: labels,
                    axisLabel: {
                        interval: 0
                    },
                },
                yAxis: {
                    type: 'value',
                    axisLabel: {
                        formatter: '{value}'
                    }
                },
                legend: {
                    orient: 'horizontal',
                    right: 10,
                    top: 20,
                },
                series: datasets,
            };

            _$Container.each(function (index, container) {
                var chartContainer = $(container).find('#m_chart_RM_Comparison')[0];
                var myChart = echarts.init(chartContainer);
                myChart.clear(); 
                myChart.setOption(option);
            });
        };  

        var getPriceTrend = function () {
            var _$Buyer = _$rmcompselBUyer.val();
            var _$Group = _$rmcompselGroup.val();
            var _$Spec = _$rmcompselSpec.val();
            var _$Year = _$rmcompselYear.find('option:selected').text();
            var _$Department = _$rmcompselTeams.val();
            var _$Grade = _$rmcompselGrade.val();

            abp.ui.setBusy(_$Container);
            _tenantDashboardService
                .getrmforcompbybuyer({
                    buyerId: _$Buyer,
                    rMSpecId: _$Spec,
                    rMGroupId: _$Group,
                    rMGradeId:_$Grade,
                    year: _$Year,
                    department: _$Department
                })
                .done(function (result) {
                    Data = result;
                    PriceTrend(result);
                    loadPriceTrendtable(result);
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
        };

        _widgetBase.runDelayed(getPriceTrend);

        $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
            _widgetBase.runDelayed(getPriceTrend);
        });
    }
    var loadPriceTrendtable = function (result) {
        result.sort(function (a, b) {
            return (b.weightedAverageEPU * b.unitRateAverage) - (a.weightedAverageEPU * a.unitRateAverage);
        });

        for (var i = 0; i < _$Container.length; i++) {
            var container = $(_$Container[i]);
            var $tableBody = container.find('#BWAPrice_Content table tbody');
            $tableBody.html('');

            for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                var price = result[rowIndex];
                var impact = price["weightedAverageEPU"] * price["unitRateAverage"];
                var $tr = $('<tr></tr>').append(
                    $('<td style="text-align: left;">' + price["team"] + '</td>'),
                    $('<td>' + price["buyerName"] + '</td>'),
                    $('<td>' + price["suppliername"] + '</td>'),
                    $('<td>' + price["unitRateAverage"] + '</td>'),
                    $('<td>' + impact.toFixed(2) + '</td>')
                );
                $tableBody.append($tr);
            }
        }
    }

})