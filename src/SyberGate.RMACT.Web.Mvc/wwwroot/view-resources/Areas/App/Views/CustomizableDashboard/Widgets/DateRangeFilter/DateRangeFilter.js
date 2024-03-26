$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _$container = $(".date-range-filter-container");
    var _$dateRangePickerInput = _$container.find("#date-range-filter");
    var _$datePickerInput = _$container.find('#date-time-filter');
    var _$dateRangePickerOpenButton = _$container.find("#btnDateRangeFilterOpen");
    var _$dateFilterOpen = _$container.find("#btnDateFilterOpen");

    var _datePickerInput = _$datePickerInput.datetimepicker({
        locale: abp.localization.currentLanguage.name, //'en-gb'  abp.localization.currentLanguage.name
        format: 'L' //'DD/MM/YYYY' 'L'
    });

    var _startSettings = {
        startDate: moment().startOf('year'),
        endDate: moment().endOf("year"),
        opens: "bottom",
        rnages: {
            'This year': [moment().startOf('year'), moment().endOf('year')],
        }
    };

    var _selectedDateRange = {
        startDate: _startSettings.startDate,
        endDate: _startSettings.endDate
    };

    _$datePickerInput.val(moment().format("MM/DD/YYYY")) //"DD/MM/YYYY" MM/DD/YYYY

    _$dateFilterOpen.click(function () {
        _datePickerInput.data('DateTimePicker').toggle();
    });

    _$datePickerInput.datetimepicker().on('dp.change', function () {
        refreshAllData();
    });

    var refreshAllData = function () {
        var dtval = _$datePickerInput.val() + " - " + _$datePickerInput.val();
        _tenantDashboardService.updateDateRangeSettings(dtval);
        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnDateChange', dtval);
    };

    _$dateRangePickerInput.val(_selectedDateRange.startDate.format("LL") + " - " + _selectedDateRange.endDate.format("LL"));

    _$dateRangePicker = _$dateRangePickerInput.daterangepicker(
        $.extend(true, app.createDateRangePickerOptions(), _startSettings), function (start, end, label) {
            _selectedDateRange.startDate = start;
            _selectedDateRange.endDate = end;
            refreshAllData();
        });

    _$dateRangePickerOpenButton.click(function () {
        _$dateRangePicker.data('daterangepicker').toggle();
    });
    _$dateRangePickerInput.change(function () {
        refreshAllData();
    });
    _$datePickerInput.change(function () {
        refreshAllData();
    });

    refreshAllData();
});