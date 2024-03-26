$(function () {
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _$container = $(".rm-plant-filter-container");
    var _$rmPlantPickerInput = _$container.find("#rm-plant-filter");
    var _$selPlant = _$container.find('#selPlant');
    var _$Supplier;
    var _$Buyer;

    var refreshAllData = function () {
        //_tenantDashboardService.updatePlantSettings(_$selPlant.val());
        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnPlantChange', { plantId: _$selPlant.val(), plantName: _$selPlant.find('option:selected').text() });
    };

    var initData = function () {
        _tenantDashboardService
            .getPlant({
                buyer: _$Buyer,
                supplier: _$Supplier
            })
            .done(function (result) {
                _$selPlant.html('<option value="0">All Plant</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    _$selPlant.append('<option value="'+ result[rowIndex].id +'">'+'|'+ result[rowIndex].code +'|'+ ' - ' + result[rowIndex].description + '</option')
                }
                _$selPlant.val(0);
                abp.event.trigger('app.dashboardFilters.DateRangePicker.OnPlantChange', { plantId: _$selPlant.val(), plantName: _$selPlant.find('option:selected').text() });
                //_tenantDashboardService
                //    .getPlantSettings()
                //    .done(function (result) {
                //        _$selPlant.val(result);
                //        abp.event.trigger('app.dashboardFilters.DateRangePicker.OnPlantChange', { plantId: _$selPlant.val(), plantName: _$selPlant.find('option:selected').text() });
                //    });
            });
    };

    _$selPlant.change(function () {
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