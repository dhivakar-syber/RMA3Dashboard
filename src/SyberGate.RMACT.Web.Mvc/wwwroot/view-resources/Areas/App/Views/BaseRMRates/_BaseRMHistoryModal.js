(function ($) {
    app.modals.BaseRMHistoryModal = function () {

        console.log('BaseRmRateHistory')
        var _baseRMRatesService = abp.services.app.baseRMRates;
        var _$baseRMHistoryTable = $('#BaseRMHistoryTable');
        var dataTable = _$baseRMHistoryTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _baseRMRatesService.getAll,
                inputFilter: function () {
                    //var dateString = "baseRMRate.settledDate";
                    //var date = new Date(dateString);
                    //var formattedDate = (date.getDate()) + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
                    return {
                        filter: $('#BaseRMRatesTableFilter').val(),
                        rMGroupNameFilter: $('#RMGroupNameFilterId').val(),
                        unitOfMeasurementCodeFilter: $('#UnitOfMeasurementCodeFilterId').val(),
                        yearNameFilter: $('#YearNameFilterId').val(),
                        buyerNameFilter: $('#BuyerNameFilterId').val(),
                        supplierNameFilter: $('#SupplierNameFilterId').val().split("-")[0]
                    };
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    data: "baseRMRate.unitRate",
                    name: "unitRate"
                },
                {
                    targets: 1,
                    data: "baseRMRate.scrapAmount",
                    name: "scrapAmount"
                },
                //{
                //    targets: 2,
                //    data: "baseRMRate.month",
                //    name: "month",
                //    render: function (month) {
                //        return app.localize('Enum_Months_' + month);
                //    }
                //},
                {
                    targets: 2,
                    data: "baseRMRate.settledDate",
                    name: "settledDate"
                },
                //{
                //    targets: 2,
                //    data: "formattedDate",
                //    name: "settledDate"
                //},

                {
                    targets: 3,
                    data: "baseRMRate.weightRatio",
                    name:"weightRatio"
                },
                {
                    targets: 4,
                    data: "baseRMRate.lossRatio",
                    name: "lossRatio"
                }
            ]

        });
        function getBaseRMRates() {
            dataTable.ajax.reload();
        }
        getBaseRMRates();
        console.log('basermmodel');
    }
})(jQuery);