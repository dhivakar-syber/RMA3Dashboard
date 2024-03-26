(function ($) {
    app.modals.CreateOrEditPlantModal = function () {

        var _plantsService = abp.services.app.plants;

        var _modalManager;
        var _$plantInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$plantInformationForm = _modalManager.getModal().find('form[name=PlantInformationsForm]');
            _$plantInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$plantInformationForm.valid()) {
                return;
            }

            var plant = _$plantInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _plantsService.createOrEdit(
				plant
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditPlantModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);