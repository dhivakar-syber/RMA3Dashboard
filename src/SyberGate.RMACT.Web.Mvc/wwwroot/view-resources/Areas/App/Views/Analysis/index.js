(function () {
    $(function () {
        console.log("chart");
        var _$Container = $('.container');
        console.log("chart");
        var fnpie = function () {
            const data = {
                labels: ['Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint'],
                datasets: [
                    {
                        label: ['Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint'],
                        data: ['4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205'],
                        backgroundColor: ['rgb(255, 99, 132)', 'rgb(54, 162, 235)', 'rgb(255, 205, 86)', 'rgb(54, 150, 25)', 'rgb(128,0,0)', 'rgb(128,0,128)', 'rgb(255,165,0)', 'rgb(240,230,140)', 'rgb(152,251,152)', 'rgb(0,206,209)'],
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#pieChart');

                new Chart(chartContainer, {
                    type: 'pie',
                    data: data,
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top',
                            },
                            title: {
                                display: true,
                                text: ' Pie Chart'
                            }
                        }
                    },
                });
            }
        }
        fnpie();

        var fnpie1 = function () {
            const data = {
                labels: ['Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint'],
                datasets: [
                    {
                        label: ['Aluminum', 'Copper', 'Flat Steel (Hot/Cold-Rolled)', 'Forgings', 'Iron Castings', 'Natural Rubber (Tires)', 'Other Material', 'Other Steel', 'PGMs (Platinum/Palladium)', 'Plastics/Paint'],
                        data: ['63497.94', '13972.5', '20566.2', '61136.12', '77492.8', '15323.22', '36655.3', '83815.49', '81754.42', '58315.95'],
                        backgroundColor: ['rgb(255, 99, 132)', 'rgb(54, 162, 235)', 'rgb(255, 205, 86)', 'rgb(54, 150, 25)', 'rgb(128,0,0)', 'rgb(128,0,128)', 'rgb(255,165,0)', 'rgb(240,230,140)', 'rgb(152,251,152)', 'rgb(0,206,209)'],
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#pieChart1');

                new Chart(chartContainer, {
                    type: 'pie',
                    data: data,
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top',
                            },
                            title: {
                                display: true,
                                text: ' Pie Chart'
                            }
                        }
                    },
                });
            }
        }
        fnpie1();

        //function generateLineChart(label, data, borderColor) {
        //    const chartData = {
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

        //    for (var i = 0; i < _$Container.length; i++) {
        //        var chartContainer = $(_$Container[i]).find('.chart').eq(label);

        //        new Chart(chartContainer, {
        //            type: 'line',
        //            data: chartData,
        //            options: {
        //                responsive: true,
        //                plugins: {
        //                    legend: {
        //                        position: 'top',
        //                        display: false
        //                    },
        //                    title: {
        //                        display: true,
        //                        text: 'Line Chart'
        //                    }
        //                }
        //            },
        //        });
        //    }
        //}

        //generateLineChart('Aluminum', ['4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442'], 'rgb(255, 99, 132)');
        //generateLineChart('Copper', ['9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205'], 'rgb(54, 162, 235)');
        //generateLineChart('Flat Steel (Hot/Cold-Rolled)', ['1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556'], 'rgb(255, 205, 86)');
        //generateLineChart('Forgings', ['4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070'], 'rgb(54, 150, 25)');
        //generateLineChart('Iron Castings', ['7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016'], 'rgb(128,0,0)');
        //generateLineChart('Natural Rubber (Tires)', ['982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33'], 'rgb(128,0,128)');
        //generateLineChart('Other Material', ['1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912'], 'rgb(255,165,0)');
        //generateLineChart('Other Steel', ['6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664'], 'rgb(240,230,140)');
        //generateLineChart('PGMs (Platinum/Palladium)', ['5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603'], 'rgb(152,251,152)');
        //generateLineChart('Plastics/Paint', ['3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507'], 'rgb(0,206,209)');



        var fnline = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['Aluminum'],
                        data: ['4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442'],
                        fill: false,
                        borderColor: 'rgb(255, 99, 132)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline();

        var fnline1 = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['Copper'],
                        data: ['9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205'],
                        fill: false,
                        borderColor: 'rgb(54, 162, 235)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart1');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline1();

        var fnline2 = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['Flat Steel (Hot/Cold-Rolled)'],
                        data: ['1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556'],
                        fill: false,
                        borderColor: 'rgb(255, 205, 86)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart2');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline2();

        var fnline3 = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['Forgings'],
                        data: ['4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070'],
                        fill: false,
                        borderColor: 'rgb(54, 150, 25)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart3');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline3();

        var fnline4 = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['Iron Castings'],
                        data: ['7771752.912', '982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016'],
                        fill: false,
                        borderColor: 'rgb(128,0,0)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart4');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline4();

        var fnline5 = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['Natural Rubber (Tires)'],
                        data: ['982524.8664', '1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33'],
                        fill: false,
                        borderColor: 'rgb(128,0,128)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart5');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline5();

        var fnline6 = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['Other Material'],
                        data: ['1778148.603', '6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912'],
                        fill: false,
                        borderColor: 'rgb(255,165,0)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart6');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline6();

        var fnline7 = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['Other Steel'],
                        data: ['6825095.3507', '5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664'],
                        fill: false,
                        borderColor: 'rgb(240,230,140)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart7');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline7();

        var fnline8 = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['PGMs (Platinum/Palladium)'],
                        data: ['5723626.9442', '3871595.9205', '4914740.556', '9110070', '1453619.016', '4753333.33', '7771752.912', '982524.8664', '1778148.603'],
                        fill: false,
                        borderColor: 'rgb(152,251,152)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart8');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline8();

        var fnline9 = function () {
            const data = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September'],
                datasets: [
                    {
                        label: ['Plastics/Paint'],
                        data: ['3871595.9205', '4914740.556', '9110070', '1453619.016','4753333.33', '7771752.912', '982524.8664', '1778148.603', '6825095.3507'],
                        fill: false,
                        borderColor: 'rgb(0,206,209)',
                        tension: 0.1
                    }
                ]
            };
            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#lineChart9');

                new Chart(chartContainer, {
                    type: 'line',
                    data: data,
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
        }
        fnline9();

        var fnstacked = function () {

            var teams = ['TT/GI1-1', 'TT/GI1-2', 'TT/GI1-4', 'TT/GI1-5', 'TT/GI4-1', 'TT/GI4-2', 'TT/GI4-3', 'TT/GI4-4', 'TT/GI4-5', 'TT/GI4-6', 'TT/GI4-7', 'TT/GI-3'];
            var quarters = ['Qtr1', 'Qtr2', 'Qtr3', 'Qtr4'];

            var teamData = [
                [20, 12, 8, 25, 8, 17, 18, 11, 7, 13, 12, 1],
                [15, 11, 4, 22, 13, 9, 14, 6, 8, 22, 12, 10],
                [25, 19, 6, 26, 15, 11, 12, 8, 4, 17, 13, 4],
                [22, 17, 5, 17, 9, 8, 16, 7, 9, 21, 15, 6],
                [10, 6, 4, 15, 11, 4, 15, 9, 6, 9, 5, 4],
                [15, 10, 5, 18, 14, 4, 18, 13, 5, 17, 15, 2],
                [20, 16, 4, 24, 16, 8, 20, 15, 5, 14, 10, 4],
                [25, 18, 7, 16, 6, 10, 24, 19, 5, 18, 6, 12],
                [28, 23, 5, 10, 4, 6, 22, 13, 9, 25, 15, 10],
                [30, 26, 4, 12, 7, 5, 10, 4, 6, 12, 9, 3],
                [32, 24, 8, 15, 5, 10, 20, 11, 9, 24, 11, 13],
                [15, 11, 4, 22, 15, 7, 15, 6, 9, 21, 13, 8]
            ];

            var datasets = [];
            for (var i = 0; i < quarters.length; i++) {
                var label = quarters[i];
                var data = [];
                var data2 = [];
                var data3 = [];
                for (var j = 0; j < teams.length; j++) {
                    var row = teamData[j];
                    var actual = row[i * 3];
                    var approved = row[i * 3 + 1];
                    var pending = row[i * 3 + 2];
                    data.push(actual);
                    data2.push(approved);
                    data3.push(pending);
                }
                datasets.push({
                    label: label + " Actual",
                    data: data,
                    backgroundColor: "blue",
                    stack: 'stack' + i
                });
                datasets.push({
                    label: label + " Approved",
                    data: data2,
                    backgroundColor: "green",
                    stack: 'stack' + i
                });
                datasets.push({
                    label: label + " Pending",
                    data: data3,
                    backgroundColor: "red",
                    stack: 'stack' + i
                });
            }

            console.log(datasets);

            for (var i = 0; i < _$Container.length; i++) {
                var chartContainer = $(_$Container[i]).find('#stackedChart');

                new Chart(chartContainer, {
                    type: 'bar',
                    data: {
                        labels: teams,
                        datasets: datasets
                    },
                    options: {
                        responsive: true,
                        scales: {
                            xAxes: [{
                                stacked: true
                            }],
                            yAxes: [{
                                stacked: true
                            }]
                        }
                    },
                });
            }
        }
        fnstacked();
    });
})();