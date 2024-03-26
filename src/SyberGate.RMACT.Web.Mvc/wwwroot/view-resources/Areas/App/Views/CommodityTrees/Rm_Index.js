(function () {
    $(function () {
        var _rmIndexService = abp.services.app.rawMaterialIndexes;
        var _yearsService = abp.services.app.years;
        var _$Container = $('#RMPriceIndexContainer');
        var _year = $('#yearId');
        var _years = "";
        var selYear = moment().format("YYYY");

        $(_year).change(function () {
            lmeindexes.load();
            mmrindexes.load();
        });

        var loadYear = function () {
            $(_year).empty().append(_years);
            console.log('rmindexes');
            if (selYear)
                for (var row = 0; row < _year.children().length; row++) {
                    var opt = _year.children()[row];
                    if (opt.text.toUpperCase() == selYear.toUpperCase())
                        $(opt).attr("selected", true)
                }
        }

        var getYears = function () {
            _yearsService.getAll({}).done(function (result) {
                _years = '<option value=0>select</option>';
                for (var i = 0; i < result.items.length; i++) {
                    var y = result.items[i].year;
                    _years += '<option value=' + y.id + '>' + y.name + '</option>'
                }
                loadYear();
                lmeindexes.load();
            });
        }

        var lmeindexes = {
            $table: $('#LMETable'),
            $emptyInfo: $('#LMEEmptyInfo'),
            $selectedOuRightTitle: $('#SelectedOuRightTitle'),
            dataTable: null,

            showTable: function () {
                lmeindexes.$emptyInfo.hide();
                lmeindexes.$table.show();
                lmeindexes.$selectedOuRightTitle.text('LME Index').show();
            },

            hideTable: function () {
                lmeindexes.$table.hide();
                lmeindexes.$emptyInfo.show();
            },

            load: function () {
                lmeindexes.showTable();
                this.dataTable.ajax.reload();
                console.log('loaded');
            },

            init: function () {
                this.dataTable = lmeindexes.$table.find(".CommodityTree-LME-table").DataTable({
                    paging: false,
                    serverSide: true,
                    processing: true,
                    deferLoading: 0, //prevents table for ajax request on initialize
                    responsive: false,
                    listAction: {
                        ajaxFunction: _rmIndexService.getRMPriceIndexList,
                        inputFilter: function () {
                            return { yearNameFilter: $('#yearId option:selected').text(), indexNameNameFilter: 'LME'}
                        }
                    },
                    columnDefs: [
                        {
                            targets: 0,
                            orderable: false,
                            data: "rmGrade",
                            render: function (data) {
                                console.log(data);
                                return data;
                            }
                        },
                        {
                            targets: 1,
                            orderable: false,
                            data: "jan"
                        },
                        {
                            targets: 2,
                            orderable: false,
                            data: "feb"
                        },
                        {
                            targets: 3,
                            orderable: false,
                            data: "mar"
                        },
                        {
                            targets: 4,
                            orderable: false,
                            data: "apr"
                        },
                        {
                            targets: 5,
                            orderable: false,
                            data: "may"
                        },
                        {
                            targets: 6,
                            orderable: false,
                            data: "jun"
                        },
                        {
                            targets: 7,
                            orderable: false,
                            data: "jul"
                        },
                        {
                            targets: 8,
                            orderable: false,
                            data: "aug"
                        },
                        {
                            targets: 9,
                            orderable: false,
                            data: "sep"
                        },
                        {
                            targets: 10,
                            orderable: false,
                            data: "oct"
                        },
                        {
                            targets: 11,
                            orderable: false,
                            data: "nov"
                        },
                        {
                            targets: 12,
                            orderable: false,
                            data: "dec"
                        }

                    ]
                });
                $('#LMEHref').click(function (e) {
                    lmeindexes.load();
                });
                lmeindexes.hideTable();
            }
        };

        var mmrindexes = {
            $table: $('#MMRTable'),
            $emptyInfo: $('#MMREmptyInfo'),
            $selectedOuRightTitle: $('#SelectedOuRightTitle'),
            dataTable: null,

            showTable: function () {
                mmrindexes.$emptyInfo.hide();
                mmrindexes.$table.show();
                mmrindexes.$selectedOuRightTitle.text('MMR Index').show();
            },

            hideTable: function () {
                mmrindexes.$table.hide();
                mmrindexes.$emptyInfo.show();
            },

            load: function () {
                mmrindexes.showTable();
                this.dataTable.ajax.reload();
                console.log('loaded');
            },

            init: function () {
                this.dataTable = mmrindexes.$table.find(".CommodityTree-MMR-table").DataTable({
                    paging: false,
                    serverSide: true,
                    processing: true,
                    deferLoading: 0, //prevents table for ajax request on initialize
                    responsive: false,
                    listAction: {
                        ajaxFunction: _rmIndexService.getRMPriceIndexList,
                        inputFilter: function () {
                            return { yearNameFilter: $('#yearId option:selected').text(), indexNameNameFilter: 'MMR' }
                        }
                    },
                    columnDefs: [
                        {
                            targets: 0,
                            orderable: false,
                            data: "rmGrade",
                            render: function (data) {
                                console.log(data);
                                return data;
                            }
                        },
                        {
                            targets: 1,
                            orderable: false,
                            data: "jan"
                        },
                        {
                            targets: 2,
                            orderable: false,
                            data: "feb"
                        },
                        {
                            targets: 3,
                            orderable: false,
                            data: "mar"
                        },
                        {
                            targets: 4,
                            orderable: false,
                            data: "apr"
                        },
                        {
                            targets: 5,
                            orderable: false,
                            data: "may"
                        },
                        {
                            targets: 6,
                            orderable: false,
                            data: "jun"
                        },
                        {
                            targets: 7,
                            orderable: false,
                            data: "jul"
                        },
                        {
                            targets: 8,
                            orderable: false,
                            data: "aug"
                        },
                        {
                            targets: 9,
                            orderable: false,
                            data: "sep"
                        },
                        {
                            targets: 10,
                            orderable: false,
                            data: "oct"
                        },
                        {
                            targets: 11,
                            orderable: false,
                            data: "nov"
                        },
                        {
                            targets: 12,
                            orderable: false,
                            data: "dec"
                        }

                    ]
                });

                $('#MMRHref').click(function (e) {
                    mmrindexes.load();
                });
                mmrindexes.hideTable();
            }
        };

        var siamindexes = {
            $table: $('#SIAMTable'),
            $emptyInfo: $('#SIAMEmptyInfo'),
            $selectedOuRightTitle: $('#SelectedOuRightTitle'),
            dataTable: null,

            showTable: function () {
                siamindexes.$emptyInfo.hide();
                siamindexes.$table.show();
                siamindexes.$selectedOuRightTitle.text('SIAM Index').show();
            },

            hideTable: function () {
                siamindexes.$table.hide();
                siamindexes.$emptyInfo.show();
            },

            load: function () {
                siamindexes.showTable();
                this.dataTable.ajax.reload();
                console.log('loaded');
            },

            init: function () {
                this.dataTable = siamindexes.$table.find(".CommodityTree-SIAM-table").DataTable({
                    paging: false,
                    serverSide: true,
                    processing: true,
                    deferLoading: 0, //prevents table for ajax request on initialize
                    responsive: false,
                    listAction: {
                        ajaxFunction: _rmIndexService.getRMPriceIndexList,
                        inputFilter: function () {
                            return { yearNameFilter: $('#yearId option:selected').text(), indexNameNameFilter: 'SIAM' }
                        }
                    },
                    columnDefs: [
                        {
                            targets: 0,
                            orderable: false,
                            data: "rmGrade",
                            render: function (data) {
                                console.log(data);
                                return data;
                            }
                        },
                        {
                            targets: 1,
                            orderable: false,
                            data: "jan"
                        },
                        {
                            targets: 2,
                            orderable: false,
                            data: "feb"
                        },
                        {
                            targets: 3,
                            orderable: false,
                            data: "mar"
                        },
                        {
                            targets: 4,
                            orderable: false,
                            data: "apr"
                        },
                        {
                            targets: 5,
                            orderable: false,
                            data: "may"
                        },
                        {
                            targets: 6,
                            orderable: false,
                            data: "jun"
                        },
                        {
                            targets: 7,
                            orderable: false,
                            data: "jul"
                        },
                        {
                            targets: 8,
                            orderable: false,
                            data: "aug"
                        },
                        {
                            targets: 9,
                            orderable: false,
                            data: "sep"
                        },
                        {
                            targets: 10,
                            orderable: false,
                            data: "oct"
                        },
                        {
                            targets: 11,
                            orderable: false,
                            data: "nov"
                        },
                        {
                            targets: 12,
                            orderable: false,
                            data: "dec"
                        }

                    ]
                });

                $('#SIAMHref').click(function (e) {
                    siamindexes.load();
                });
                siamindexes.hideTable();
            }

        };

        getYears();
        lmeindexes.init();
        mmrindexes.init();
        siamindexes.init();

        KTUtil.ready(function () {
            KTLayoutStretchedCard.init('ouCard');
            KTLayoutStretchedCard.init('RMPriceIndexContainer');
        });

    });
})();