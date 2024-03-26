(function ($) {
    app.modals.CreateOrEditUnitOfMeasurementModal = function () {

        var _unitOfMeasurementsService = abp.services.app.unitOfMeasurements;

        var _modalManager;
        var _$unitOfMeasurementInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$unitOfMeasurementInformationForm = _modalManager.getModal().find('form[name=UnitOfMeasurementInformationsForm]');
            _$unitOfMeasurementInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$unitOfMeasurementInformationForm.valid()) {
                return;
            }

            var unitOfMeasurement = _$unitOfMeasurementInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _unitOfMeasurementsService.createOrEdit(
				unitOfMeasurement
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditUnitOfMeasurementModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);