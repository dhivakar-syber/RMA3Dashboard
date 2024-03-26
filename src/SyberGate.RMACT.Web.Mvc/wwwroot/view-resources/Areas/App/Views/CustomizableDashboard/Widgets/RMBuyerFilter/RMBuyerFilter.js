$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _$container = $(".rm-buyer-filter-container");
    var _$rmBuyerPickerInput = _$container.find("#rm-buyer-filter");
    var _$selBUyer = _$container.find('#selBUyer');

    var refreshAllData = function () {
        _tenantDashboardService.updateBuyerSettings(_$selBUyer.val());
        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnBuyerChange', { buyerId: _$selBUyer.val(), buyerName: _$selBUyer.find('option:selected').text() });
    };

    var initData = function () {
        _tenantDashboardService
            .getBuyer({})
            .done(function (result) {
                _$selBUyer.html('<option value="">Select Buyer</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$selBUyer.append('<option value="'+ result[rowIndex].id +'">' + result[rowIndex].name + '</option')
                }
                _tenantDashboardService
                    .getBuyerSettings()
                    .done(function (result) {
                        _$selBUyer.val(result);
                        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnBuyerChange', { buyerId: _$selBUyer.val(), buyerName: _$selBUyer.find('option:selected').text() });
                    });
            });
    };
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

    _$selBUyer.change(function () {
        refreshAllData();
    });

    initData();
});