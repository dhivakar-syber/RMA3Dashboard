(function ($) {
    app.modals.CreateOrEditProcureDataModal = function () {

        var _procureDatasService = abp.services.app.procureDatas;

        var _modalManager;
        var _$procureDataInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$procureDataInformationForm = _modalManager.getModal().find('form[name=ProcureDataInformationsForm]');
            _$procureDataInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$procureDataInformationForm.valid()) {
                return;
            }

            var procureData = _$procureDataInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _procureDatasService.createOrEdit(
				procureData
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditProcureDataModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);