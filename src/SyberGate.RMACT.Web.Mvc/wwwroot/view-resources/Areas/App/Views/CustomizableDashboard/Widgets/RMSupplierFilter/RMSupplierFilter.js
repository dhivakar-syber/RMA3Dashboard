$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _$container = $(".rm-supplier-filter-container");
    var _$rmSupplierPickerInput = _$container.find("#rm-supplier-filter");
    var _$selSupplier = _$container.find('#selSupplier');
    var _$buyer;
    var _widgetBase = app.widgetBase.create();

    var refreshAllData = function () {
        _tenantDashboardService.updateSupplierSettings(_$selSupplier.val());
        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnSupplierChange', { supplierId: _$selSupplier.val(), supplierName: _$selSupplier.find('option:selected').text() });
    };

    var initData = function () {
        console.log(_$buyer)
        if (_$buyer) {
            _tenantDashboardService
                .getSupplier(_$buyer)
                .done(function (result) {
                    _$selSupplier.html('<option value="">Select Supplier</option')
                    for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                        _$selSupplier.append('<option value="' + result[rowIndex].supplierId + '">' + result[rowIndex].supplierName + " - " + result[rowIndex].supplierCode + '</option')
                    }
                    _tenantDashboardService
                        .getSupplierSettings()
                        .done(function (result) {
                            _$selSupplier.val(result);
                            abp.event.trigger('app.dashboardFilters.DateRangePicker.OnSupplierChange', { supplierId: _$selSupplier.val(), supplierName: _$selSupplier.find('option:selected').text() });
                        });
                });
        }
    };


    abp.event.on('app.dashboardFilters.DateRangePicker.OnBuyerChange', function (_selectedBuyer) {
        _$buyer = _selectedBuyer.buyerId;
        _widgetBase.runDelayed(function () { initData() });
    });

    //_$dateRangePickerInput.val(_selectedDateRange.startDate.format("LL") + " - " + _selectedDateRange.endDate.format("LL"));
    /*
    _$dateRangePicker = _$dateRangePickerInput.daterangepicker(
        $.extend(true, app.createDateRangePickerOptions(), _startSettings), function (start, end, label) {
            _selectedDateRange.startDate = start;
            _selectedDateRange.endDate = end;
            refreshAllData();
        });

    _$dateRangePickerOpenButton.click(function () {
        _$dateRangePicker.data('daterangepicker').toggle();
    });
    */

    _$selSupplier.change(function () {
        refreshAllData();
    });

    initData();
});