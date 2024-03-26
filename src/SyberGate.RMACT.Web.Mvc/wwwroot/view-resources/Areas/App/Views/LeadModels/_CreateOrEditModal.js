(function ($) {
    app.modals.CreateOrEditLeadModelModal = function () {

        var _leadModelsService = abp.services.app.leadModels;

        var _modalManager;
        var _$leadModelInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$leadModelInformationForm = _modalManager.getModal().find('form[name=LeadModelInformationsForm]');
            _$leadModelInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$leadModelInformationForm.valid()) {
                return;
            }

            var leadModel = _$leadModelInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _leadModelsService.createOrEdit(
				leadModel
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditLeadModelModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);