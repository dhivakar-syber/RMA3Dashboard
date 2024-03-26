$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();
    var _$Container = $('.AVOBChartContainer');
    var _$yearcontainer = $(".avobchart-year-filter-container");
    var _$monthcontainer = $(".avobchart-month-filter-container");
    var _$monthcontainer2 = $(".avobchart-month2-filter-container");
    var _$plantcontainer = $(".avobchart-plant-filter-container");
    var _$avobchartselYear = _$yearcontainer.find('#avobchartselYear');
    var _$avobchartselMonth = _$monthcontainer.find('#avobchartselMonth');
    var _$avobchartselMonth2 = _$monthcontainer2.find('#avobchartselMonth2');
    var _$avobchartselPlant = _$plantcontainer.find('#avobchartselPlant');

    var avobchartyear = function () {
        _tenantDashboardService
            .getYears({})
            .done(function (result) {
                _$avobchartselYear.html('<option value="">Select Year</option>'); 
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$avobchartselYear.append('<option value="' + rowIndex + '">' + result[rowIndex].name + '</option>'); 
                }
                _$avobchartselYear.val("4");
            });
    }
    avobchartyear();

    var avobchartplant = function () {
        _tenantDashboardService
            .getPlants({})
            .done(function (result) {
                _$avobchartselPlant.html('<option value="0">All Plant</option>');
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$avobchartselPlant.append('<option value="' + result[rowIndex].code + '">' + '|' + result[rowIndex].code + '|' + ' - ' + result[rowIndex].description + '</option');
                }
                _$avobchartselPlant.val(0);
            });
    }
    avobchartplant();

    $('#AVOBChartSubmit').click(function () {
        var monthValue = $('#avobchartselMonth2').val();
        var monthValue2 = $('#avobchartselMonth').val();
        if (monthValue !== " ") {
            Promise.all([loadcurrentAVOB(), loadrevisedAVOB()])
                .then(function ([array1, array2]) {
                    var differenceArray = calculateDifference(array1, array2);

                    function calculateDifference(array1, array2) {
                        var differenceArray = [];
                        for (var i = 0; i < array1.length; i++) {
                            if (array1[i] === 0 || array2[i] === 0) {
                                differenceArray.push(0);
                            } else {
                                differenceArray.push(array1[i] - array2[i]);
                            }
                        }
                        return differenceArray;
                    }
                    var barchartInstance;

                    if (barchartInstance) {
                        barchartInstance.destroy();
                    }
                    var ctx = document.getElementById('m_chart_Delta').getContext('2d');

                    var labels = [
                        '1015R', '1215R', '1415R', '1217C', '1617R', '2823R', '3523R',
                        '1923C', '2823C', '2828C', '3528C', '5428TS', '5528T', '4228R', '3528CM'
                    ];

                    barchartInstance = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: labels,
                            datasets: [{
                                data: differenceArray,
                                backgroundColor: [
                                    'rgb(255, 99, 132)', 'rgb(54, 162, 235)', 'rgb(255, 205, 86)',
                                    'rgb(0, 103, 127)', 'rgb(255, 255, 64)', 'rgb(0, 86, 106)',
                                    'rgb(200, 200, 200)', 'rgb(0, 122, 147)', 'rgb(158, 158, 158)',
                                    'rgb(121, 174, 191)', 'rgb(80, 151, 171)', 'rgb(166, 202, 216)',
                                    'rgb(255, 0, 0)', 'rgb(230, 145, 35)', 'rgb(110, 160, 70)'
                                ],
                            }]
                        },
                        options: {
                            legend: {
                                display: true,
                            },
                            hover: {
                                mode: 'index'
                            }
                        }
                    });
                })

        }
        else {
            alert("For Comparison select another Month");
        }
    });
    $('#refreshAVOBchartButton').click(function () {
        loadcurrentAVOB();
    });

    var loadcurrentAVOB = function () {
        return new Promise(function (resolve, reject) {
            console.log("chart");

            var _widgetBase = app.widgetBase.create();
            var piechartInstance;

            var Piechartcurrent = function (data) {
                console.log(data);
                var propertyValues = Object.values(data[0]);
                var numericValues = propertyValues.filter(function (value) {
                    return $.isNumeric(value);
                });

                console.log(numericValues);

                var labels = [
                    '1015R',
                    '1215R',
                    '1415R',
                    '1217C',
                    '1617R',
                    '2823R',
                    '3523R',
                    '1923C',
                    '2823C',
                    '2828C',
                    '3528C',
                    '5428TS',
                    '5528T',
                    '4228R',
                    '3528CM'
                ];

                if (piechartInstance) {
                    piechartInstance.destroy();
                }

                for (var i = 0; i < _$Container.length; i++) {
                    var chartContainer = $(_$Container[i]).find('#m_chart_Current_AVOB');
                    piechartInstance = new Chart(chartContainer, {
                        type: 'pie',
                        data: {
                            datasets: [{
                                data: numericValues,
                                backgroundColor: [
                                    'rgb(255, 99, 132)',
                                    'rgb(54, 162, 235)',
                                    'rgb(255, 205, 86)',
                                    'rgb(0, 103, 127)',
                                    'rgb(255, 255, 64)',
                                    'rgb(0, 86, 106)',
                                    'rgb(200, 200, 200)',
                                    'rgb(0, 122, 147)',
                                    'rgb(158, 158, 158)',
                                    'rgb(121, 174, 191)',
                                    'rgb(80, 151, 171)',
                                    'rgb(166, 202, 216)',
                                    'rgb(255, 0, 0)',
                                    'rgb(230, 145, 35)',
                                    'rgb(110, 160, 70)'
                                ],
                            }],
                            labels: labels,
                            hoverOffset: 4,
                        },
                        options: {
                            plugins: {
                                legend: {
                                    display: true,
                                    position: 'right',
                                },
                            },
                            responsive: true,
                        },
                    });
                }
            };

            var getModelByPlant = function () {
                var _$Plant = _$avobchartselPlant.val();
                var _$Month = _$avobchartselMonth.find('option:selected').text();
                var _$Year = _$avobchartselYear.find('option:selected').text();
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .getModelForCompByPlant({
                        plant: _$Plant,
                        month: _$Month,
                        year: _$Year
                    })
                    .done(function (result) {
                        Piechartcurrent(result);
                        var propertyValues = Object.values(result[0]);
                        var numericValues = propertyValues.filter(function (value) {
                            return $.isNumeric(value);
                        });
                        resolve(numericValues);
                    })
                    .always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
            };

            _widgetBase.runDelayed(getModelByPlant);

            $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
                _widgetBase.runDelayed(getModelByPlant);
            });
        });
    };

    loadcurrentAVOB();

    var loadrevisedAVOB = function () {
        return new Promise(function (resolve, reject) {
            var _widgetBase = app.widgetBase.create();
            var _$Container = $('.AVOBChartContainer');
            var piechart2Instance;
            var Piechartrevised = function (data) {
                console.log(data);
                var propertyValues = Object.values(data["0"]);
                var revisednumericValues = propertyValues.filter(function (value) {
                    return $.isNumeric(value);
                });

                console.log(revisednumericValues);
                var labels = [
                    '1015R',
                    '1215R',
                    '1415R',
                    '1217C',
                    '1617R',
                    '2823R',
                    '3523R',
                    '1923C',
                    '2823C',
                    '2828C',
                    '3528C',
                    '5428TS',
                    '5528T',
                    '4228R',
                    '3528CM'
                ];

                if (piechart2Instance) {
                    piechart2Instance.destroy();
                }


                for (var i = 0; i < _$Container.length; i++) {
                    var chartContainer = $(_$Container[i]).find('#m_chart_Revised_AVOB');
                    piechart2Instance = new Chart(chartContainer, {
                        type: 'pie',
                        data: {
                            datasets: [{
                                data: revisednumericValues,
                                backgroundColor: [
                                    'rgb(255, 99, 132)',
                                    'rgb(54, 162, 235)',
                                    'rgb(255, 205, 86)',
                                    'rgb(0,103,127)',
                                    'rgb(255,255,64)',
                                    'rgb(0,86,106)',
                                    'rgb(200,200,200)',
                                    'rgb(0,122,147)',
                                    'rgb(158,158,158)',
                                    'rgb(121,174,191)',
                                    'rgb(80,151,171)',
                                    'rgb(166,202,216)',
                                    'rgb(255,0,0)',
                                    'rgb(230,145,35)',
                                    'rgb(110,160,70)'
                                ],
                            }],
                            labels: labels,
                            hoverOffset: 4,
                            hover: {
                                mode: 'index'
                            }
                        },
                    });
                }
            };

            var getModelbyPlantforcomp = function () {
                var _$Plant = _$avobchartselPlant.val();
                var _$Month = _$avobchartselMonth2.find('option:selected').text();
                var _$Year = _$avobchartselYear.find('option:selected').text();
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .getModelForCompByPlant({
                        plant: _$Plant,
                        month: _$Month,
                        year: _$Year
                    })
                    .done(function (result) {
                        Piechartrevised(result);
                        var propertyValues = Object.values(result["0"]);
                        var revisedNumericValues = propertyValues.filter(function (value) {
                            return $.isNumeric(value);
                        });
                        resolve(revisedNumericValues);
                    }).always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
            };

            _widgetBase.runDelayed(getModelbyPlantforcomp);

            $('#DashboardTabList a[data-toggle="pill"]').on('shown.bs.tab', function (e) {
                _widgetBase.runDelayed(getModelbyPlantforcomp);
            });
        });
    };
});
