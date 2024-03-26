(function ($) {
    app.modals.CreateOrEditBaseRMRateModal = function () {
        console.log('testBM')
        var _baseRMRatesService = abp.services.app.baseRMRates;

        var _modalManager;
        var _$baseRMRateInformationForm = null;

		        var _BaseRMRatermGroupLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/BaseRMRates/RMGroupLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BaseRMRates/_BaseRMRateRMGroupLookupTableModal.js',
            modalClass: 'RMGroupLookupTableModal'
        });        var _BaseRMRateunitOfMeasurementLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/BaseRMRates/UnitOfMeasurementLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BaseRMRates/_BaseRMRateUnitOfMeasurementLookupTableModal.js',
            modalClass: 'UnitOfMeasurementLookupTableModal'
        });        var _BaseRMRateyearLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/BaseRMRates/YearLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BaseRMRates/_BaseRMRateYearLookupTableModal.js',
            modalClass: 'YearLookupTableModal'
        });        var _BaseRMRatebuyerLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/BaseRMRates/BuyerLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BaseRMRates/_BaseRMRateBuyerLookupTableModal.js',
            modalClass: 'BuyerLookupTableModal'
        });        var _BaseRMRatesupplierLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/BaseRMRates/SupplierLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/BaseRMRates/_BaseRMRateSupplierLookupTableModal.js',
            modalClass: 'SupplierLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$baseRMRateInformationForm = _modalManager.getModal().find('form[name=BaseRMRateInformationsForm]');
            _$baseRMRateInformationForm.validate();
        };

		          $('#OpenRMGroupLookupTableButton').click(function () {

            var baseRMRate = _$baseRMRateInformationForm.serializeFormToObject();

            _BaseRMRatermGroupLookupTableModal.open({ id: baseRMRate.rmGroupId, displayName: baseRMRate.rmGroupName }, function (data) {
                _$baseRMRateInformationForm.find('input[name=rmGroupName]').val(data.displayName); 
                _$baseRMRateInformationForm.find('input[name=rmGroupId]').val(data.id); 
            });
        });
		
		$('#ClearRMGroupNameButton').click(function () {
                _$baseRMRateInformationForm.find('input[name=rmGroupName]').val(''); 
                _$baseRMRateInformationForm.find('input[name=rmGroupId]').val(''); 
        });
		
        $('#OpenUnitOfMeasurementLookupTableButton').click(function () {

            var baseRMRate = _$baseRMRateInformationForm.serializeFormToObject();

            _BaseRMRateunitOfMeasurementLookupTableModal.open({ id: baseRMRate.unitOfMeasurementId, displayName: baseRMRate.unitOfMeasurementCode }, function (data) {
                _$baseRMRateInformationForm.find('input[name=unitOfMeasurementCode]').val(data.displayName); 
                _$baseRMRateInformationForm.find('input[name=unitOfMeasurementId]').val(data.id); 
            });
        });
		
		$('#ClearUnitOfMeasurementCodeButton').click(function () {
                _$baseRMRateInformationForm.find('input[name=unitOfMeasurementCode]').val(''); 
                _$baseRMRateInformationForm.find('input[name=unitOfMeasurementId]').val(''); 
        });
		
        $('#OpenYearLookupTableButton').click(function () {

            var baseRMRate = _$baseRMRateInformationForm.serializeFormToObject();

            _BaseRMRateyearLookupTableModal.open({ id: baseRMRate.yearId, displayName: baseRMRate.yearName }, function (data) {
                _$baseRMRateInformationForm.find('input[name=yearName]').val(data.displayName); 
                _$baseRMRateInformationForm.find('input[name=yearId]').val(data.id); 
            });
        });
		
		$('#ClearYearNameButton').click(function () {
                _$baseRMRateInformationForm.find('input[name=yearName]').val(''); 
                _$baseRMRateInformationForm.find('input[name=yearId]').val(''); 
        });
		
        $('#OpenBuyerLookupTableButton').click(function () {

            var baseRMRate = _$baseRMRateInformationForm.serializeFormToObject();

            _BaseRMRatebuyerLookupTableModal.open({ id: baseRMRate.buyerId, displayName: baseRMRate.buyerName }, function (data) {
                _$baseRMRateInformationForm.find('input[name=buyerName]').val(data.displayName); 
                _$baseRMRateInformationForm.find('input[name=buyerId]').val(data.id); 
            });
        });
		
		$('#ClearBuyerNameButton').click(function () {
                _$baseRMRateInformationForm.find('input[name=buyerName]').val(''); 
                _$baseRMRateInformationForm.find('input[name=buyerId]').val(''); 
        });
		
        $('#OpenSupplierLookupTableButton').click(function () {

            var baseRMRate = _$baseRMRateInformationForm.serializeFormToObject();

            _BaseRMRatesupplierLookupTableModal.open({ id: baseRMRate.supplierId, displayName: baseRMRate.supplierName }, function (data) {
                _$baseRMRateInformationForm.find('input[name=supplierName]').val(data.displayName); 
                _$baseRMRateInformationForm.find('input[name=supplierId]').val(data.id); 
            });
        });
		
		$('#ClearSupplierNameButton').click(function () {
                _$baseRMRateInformationForm.find('input[name=supplierName]').val(''); 
                _$baseRMRateInformationForm.find('input[name=supplierId]').val(''); 
        });

        $('#BaseRMRate_UnitRate').change(function (e) {
            console.log(e);
            var urate = $(e.currentTarget).val();
            var per = $('#BaseRMRate_ScrapPercent').val();
            if ($.isNumeric(per) && $.isNumeric(urate)) {
                var amt = eval(urate) * eval(per) * 0.01;
                $('#BaseRMRate_ScrapAmount').val(amt)
            }
        });


		
        $('#BaseRMRate_ScrapPercent').change(function (e) {
            console.log(e);
            var per = $(e.currentTarget).val();
            var urate = $('#BaseRMRate_UnitRate').val();
            if ($.isNumeric(per) && $.isNumeric(urate)) {
                var amt = eval(urate) * eval(per) * 0.01;
                $('#BaseRMRate_ScrapAmount').val(amt)
            }
        });

        $('#BaseRMRate_ScrapAmount').change(function (e) {
            $('#BaseRMRate_ScrapPercent').val(0);
        });

        this.save = function () {
            console.log('testBasermRate')
            if (!_$baseRMRateInformationForm.valid()) {
                return;
            }
            if ($('#BaseRMRate_RMGroupId').prop('required') && $('#BaseRMRate_RMGroupId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('RMGroup')));
                return;
            }
            if ($('#BaseRMRate_UnitOfMeasurementId').prop('required') && $('#BaseRMRate_UnitOfMeasurementId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('UnitOfMeasurement')));
                return;
            }
            if ($('#BaseRMRate_YearId').prop('required') && $('#BaseRMRate_YearId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Year')));
                return;
            }
            if ($('#BaseRMRate_BuyerId').prop('required') && $('#BaseRMRate_BuyerId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Buyer')));
                return;
            }
            if ($('#BaseRMRate_SupplierId').prop('required') && $('#BaseRMRate_SupplierId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Supplier')));
                return;
            }
           

            var baseRMRate = _$baseRMRateInformationForm.serializeFormToObject();
			    
			 _modalManager.setBusy(true);
			 _baseRMRatesService.createOrEdit(
				baseRMRate
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditBaseRMRateModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);