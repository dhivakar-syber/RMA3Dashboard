(function ($) {
    app.modals.CreateOrEditYearModal = function () {

        var _yearsService = abp.services.app.years;

        var _modalManager;
        var _$yearInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$yearInformationForm = _modalManager.getModal().find('form[name=YearInformationsForm]');
            _$yearInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$yearInformationForm.valid()) {
                return;
            }

            var year = _$yearInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _yearsService.createOrEdit(
				year
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditYearModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);