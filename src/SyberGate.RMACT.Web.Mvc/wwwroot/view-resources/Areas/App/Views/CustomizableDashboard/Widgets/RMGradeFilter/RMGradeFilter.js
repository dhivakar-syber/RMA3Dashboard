$(function () {
    console.log('rmgradefilter')
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _$container = $(".rm-grade-filter-container");
    var _$selGrade = _$container.find('#selGrade');
    var _$Supplier;
    var _$Buyer;

    var refreshAllData = function () {
        _tenantDashboardService.updateGradeSettings(_$selGrade.val());

        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnGradeChange', { gradeId: _$selGrade.val(), gradeName: _$selGrade.find('option:selected').text() });
    };

    var initData = function () {
        _tenantDashboardService
            .getGrade({
                buyer: _$Buyer,
                supplier: _$Supplier
            })
            .done(function (result) {
                _$selGrade.html('<option value="0">All Applicable</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$selGrade.append('<option value="' + result[rowIndex].id + '">' + result[rowIndex].name + '</option')
                }
                _tenantDashboardService
                    .getGradeSettings()
                    .done(function (result) {
                        _$selGrade.val(result);
                        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnGradeChange', { gradeId: _$selGrade.val(), gradeName: _$selGrade.find('option:selected').text() });
                    });
            });
    };

    _$selGrade.change(function () {
        refreshAllData();
    });

    //initData();

    abp.event.on('app.dashboardFilters.DateRangePicker.OnSupplierChange', function (_selectedSupplier) {
        _$Supplier = _selectedSupplier.supplierId;
        initData(); 
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnBuyerChange', function (_selectedBuyer) {
        _$Buyer = _selectedBuyer.buyerId;
    });
});