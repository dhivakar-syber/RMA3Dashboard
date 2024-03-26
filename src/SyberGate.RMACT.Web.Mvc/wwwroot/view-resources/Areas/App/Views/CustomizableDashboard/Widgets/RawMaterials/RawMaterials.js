$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _widgetBase = app.widgetBase.create();
    var _$Container = $('.RawMaterialContainer');

    //$('#addChartButton').click(function () {
    //    var label = $('#labelInput').val();
    //    var data = $('#dataInput').val().split(',').map(item => parseFloat(item.trim()));
    //    var borderColor = $('#colorInput').val();

    //    if (!label || data.length === 0 || borderColor === '') {
    //        alert('Please fill in all the fields.');
    //        return;
    //    }

    //    console.log('Label:', label);
    //    console.log('Data:', data);
    //    console.log('Border Color:', borderColor);

    //    $('#chartModal').modal('hide');
    //});

    //var initDailySales = function () {
    //    var rawmaterial = ['Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint',
    //        'Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint',
    //        'Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint',
    //        'Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint',
    //        'Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint',
    //        'Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint',
    //        'Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint',
    //        'Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint',
    //        'Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint',
    //        'Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint'];

    //    var month = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September',
    //        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September',
    //        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September',
    //        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September',
    //        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September',
    //        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September',
    //        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September',
    //        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September',
    //        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September',
    //        'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September']

    //    var rangevalue =
    //        ['4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442',
    //        '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205',
    //        '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556',
    //        '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070',
    //        '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016',
    //        '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33',
    //        '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912',
    //        '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664',
    //        '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603',
    //        '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507']


    //    function getRandomColor() {
    //        var letters = '0123456789ABCDEF';
    //        var color = '#';
    //        for (var i = 0; i < 6; i++) {
    //            color += letters[Math.floor(Math.random() * 16)];
    //        }
    //        return color;
    //    }

    //    var dataset = {};
    //    for (var i = 0; i < rawmaterial.length; i++) {

    //        var key = rawmaterial[i];
    //        var value = {
    //            date: month[i],
    //            rate: rangevalue[i]
    //        };
    //        if (dataset[key]) {
    //            dataset[key].push(value);
    //        } else {
    //            dataset[key] = [value];
    //        }
    //    }
    //    console.log(dataset);

    //    //var datasets = {};

    //    //for (var i = 0; i < rawmaterial.length; i++) {
    //    //    var rawmaterial = rawmaterial[i];
    //    //    //var month = month[i];
    //    //    //var rangevalue = rangevalue[i];

    //    //    //var data = [];

    //    //    //for (var j = 0; j < month.length; j++) {
    //    //    //    data.push({
    //    //    //        date: month[j],
    //    //    //        rate: rangevalue[j]
    //    //    //    });
    //    //    //}

    //    //    datasets
    //    //}

    //    //console.log(datasets);

    //    var formattedData = Object.entries(dataset).map(function (entry) {
    //        var key = entry[0];
    //        var value = entry[1];
    //        var color = getRandomColor();

    //        var obj = {
    //            label: key,
    //            data: value.map(function (point) {
    //                return {
    //                    x: point.date,
    //                    y: parseFloat(point.rate)
    //                };
    //            }),
    //            backgroundColor: color,
    //            borderColor: color,
    //            fill: false
    //        };
    //        return obj;
    //    });

    //    console.log(formattedData);


    //    //var chartData = {
    //    //    labels: rawmaterial,
    //    //    datasets: [{
    //    //        //label: 'Dataset 1',
    //    //        backgroundColor: ['rgb(255, 99, 132)', 'rgb(54, 162, 235)', 'rgb(255, 205, 86)', 'rgb(54, 150, 25)', 'rgb(128,0,0)', 'rgb(128,0,128)', 'rgb(255,165,0)', 'rgb(240,230,140)', 'rgb(152,251,152)', 'rgb(0,206,209)'],
    //    //        data: ['4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205'],
    //    //    }]
    //    //};

    ////    for (var i = 0; i < _$Container.length; i++) {
    ////        var chartContainer = $(_$Container[i]).find('#m_chart_RM_value');

    ////        new Chart(chartContainer, {
    ////            type: 'pie',
    ////            data: chartData,
    ////            options: {
    ////                title: {
    ////                    display: false
    ////                },
    ////                tooltips: {
    ////                    intersect: false,
    ////                    mode: 'nearest',
    ////                    xPadding: 10,
    ////                    yPadding: 10,
    ////                    caretPadding: 10
    ////                },
    ////                legend: {
    ////                    display: true
    ////                },
    ////                responsive: true,
    ////                maintainAspectRatio: false,
    ////                barRadius: 4,
    ////                scales: {
    ////                    xAxes: [{
    ////                        display: false,
    ////                        gridLines: false,
    ////                        stacked: true
    ////                    }],
    ////                    yAxes: [{
    ////                        display: false,
    ////                        stacked: true,
    ////                        gridLines: false
    ////                    }]
    ////                },
    ////                layout: {
    ////                    padding: {
    ////                        left: 0,
    ////                        right: 0,
    ////                        top: 0,
    ////                        bottom: 0
    ////                    }
    ////                }
    ////            }
    ////        });
    ////    }
    //};

    //initDailySales();

    ////var getDailySales = function () {
    ////    abp.ui.setBusy(_$Container);
    ////    _tenantDashboardService
    ////        .getDailySales()
    ////        .done(function (result) {
    ////           initDailySales(result.dailySales);
    ////        }).always(function () {
    ////            abp.ui.clearBusy(_$Container);
    ////        });
    ////};

    //_widgetBase.runDelayed(initDailySales);

    var fnline = function (label, data, containerId, borderColor) {
        var chartData = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
            datasets: [
                {
                    label: [label],
                    data: data,
                    fill: false,
                    borderColor: borderColor,
                    tension: 0.1
                }
            ]
        };

        for (var i = 0; i < _$Container.length; i++) {
            var chartContainer = $(_$Container[i]).find('#' + containerId);

            new Chart(chartContainer, {
                type: 'line',
                data: chartData,
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'top',
                            display: false
                        },
                        title: {
                            display: true,
                            text: 'Line Chart'
                        }
                    }
                },
            });
        }
    };

    fnline('Aluminum', ['4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442'], 'lineChart', 'rgb(255, 99, 132)');
    fnline('Copper', ['9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205'], 'lineChart1', 'rgb(54, 162, 235)');
    fnline('Flat Steel (Hot/Cold-Rolled)', ['1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556'], 'lineChart2', 'rgb(255, 205, 86)');
    fnline('Forgings', ['4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070'], 'lineChart3', 'rgb(54, 150, 25)');
    fnline('Iron Castings', ['7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016'], 'lineChart4', 'rgb(128,0,0)');
    fnline('Natural Rubber (Tires)', ['982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33'], 'lineChart5', 'rgb(128,0,128)');
    fnline('Other Material', ['1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912'], 'lineChart6', 'rgb(255,165,0)');
    fnline('Other Steel', ['6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664'], 'lineChart7', 'rgb(240,230,140)');
    fnline('PGMs(Platinum / Palladium)', ['5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603'], 'lineChart8', 'rgb(152,251,152)');
    fnline('Plastics/Paint', ['3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507'], 'lineChart9', 'rgb(0,206,209)');

    var chartCount = 11;

    function addChart() {
        console.log("chart");
        var chartDataInput = $("#dataInput").val();
        var label = $("#labelInput").val();
        var borderColor = $("#colorInput").val();

        var chartContainer = $("<div>").addClass("col-md-2");
        var canvas = $("<canvas>").addClass("chart").attr({
            id: "lineChart" + chartCount,
            width: 400,
            height: 400
        });

        chartContainer.append(canvas);
        $(".RawMaterialContainer").append(chartContainer);

        var chartData = chartDataInput.split(",").map(function (value) {
            return parseFloat(value.trim());
        });

        var ctx = canvas[0].getContext("2d");
        new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: label,
                        data: chartData,
                        fill: false,
                        borderColor: borderColor,
                        tension: 0.1
                    }
                ]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                        display: false
                    },
                    title: {
                        display: true,
                        text: 'Line Chart'
                    }
                }
            }
        });

        chartCount++;
    }
    $("#addChartButton").on("click", addChart);

    //document.getElementById('chartForm').addEventListener('submit', function (e) {
    //    e.preventDefault();

    //    var label = document.getElementById('labelInput').value;
    //    var dataInput = document.getElementById('dataInput').value;
    //    var borderColor = document.getElementById('colorInput').value;

    //    var data = dataInput.split(',').map(function (item) {
    //        return parseFloat(item.trim());
    //    });

    //    createChart(label, data, borderColor);
    //});

    //function createChart(label, data, borderColor) {
    //    var chartData = {
    //        labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
    //        datasets: [
    //            {
    //                label: [label],
    //                data: data,
    //                fill: false,
    //                borderColor: borderColor,
    //                tension: 0.1
    //            }
    //        ]
    //    };

    //    var chartContainer = document.getElementById('lineChart');
    //    new Chart(chartContainer, {
    //        type: 'line',
    //        data: chartData,
    //        options: {
    //            responsive: true,
    //            plugins: {
    //                legend: {
    //                    position: 'top',
    //                    display: false
    //                },
    //            },
    //            title: {
    //                display: true,
    //                text: 'Line Chart'
    //            }
    //        }
            
    //    });
    //}

    //_widgetBase.runDelayed(createChart);
});