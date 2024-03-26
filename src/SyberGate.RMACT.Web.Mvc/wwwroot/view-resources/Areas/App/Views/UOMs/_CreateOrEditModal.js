(function ($) {
    app.modals.CreateOrEditUOMModal = function () {

        var _uoMsService = abp.services.app.uoMs;

        var _modalManager;
        var _$uomInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$uomInformationForm = _modalManager.getModal().find('form[name=UOMInformationsForm]');
            _$uomInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$uomInformationForm.valid()) {
                return;
            }

            var uom = _$uomInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _uoMsService.createOrEdit(
				uom
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditUOMModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);