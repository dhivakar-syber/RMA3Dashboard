$(function () {
    console.log('rmgroupfilter')
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _$container = $(".rm-group-filter-container");
    var _$selGroup = _$container.find('#selGroup');
    var _$Supplier;
    var _$Buyer;
    var _$grade;

    var refreshAllData = function () {
        _tenantDashboardService.updateGroupSettings(_$selGroup.val());
        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnGroupChange', { groupId: _$selGroup.val(), groupName: _$selGroup.find('option:selected').text() });
    };

    var initData = function () {
        _tenantDashboardService
            .getSpec({
                buyer: _$Buyer,
                supplier: _$Supplier,
                grade: _$grade
            })
            .done(function (result) {
                _$selGroup.html('<option value="0">All Applicable</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$selGroup.append('<option value="'+ result[rowIndex].id +'">' + result[rowIndex].name + '</option')
                }
                _tenantDashboardService
                    .getGroupSettings()
                    .done(function (result) {
                        _$selGroup.val(result);
                        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnGroupChange', { groupId: _$selGroup.val(), groupName: _$selGroup.find('option:selected').text() });
                    });
            });
    };

    _$selGroup.change(function () {
        refreshAllData();
    });

    //initData();

    abp.event.on('app.dashboardFilters.DateRangePicker.OnSupplierChange', function (_selectedSupplier) {
        _$Supplier = _selectedSupplier.supplierId;
        
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnBuyerChange', function (_selectedBuyer) {
        _$Buyer = _selectedBuyer.buyerId;
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnGradeChange', function (_selectedGrade) {
        _$grade = _selectedGrade.gradeId;
        initData();
    });
});