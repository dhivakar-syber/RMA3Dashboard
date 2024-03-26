(function ($) {
    app.modals.CreateOrEditRMGroupModal = function () {

        var _rmGroupsService = abp.services.app.rMGroups;

        var _modalManager;
        var _$rmGroupInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$rmGroupInformationForm = _modalManager.getModal().find('form[name=RMGroupInformationsForm]');
            _$rmGroupInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$rmGroupInformationForm.valid()) {
                return;
            }

            var rmGroup = _$rmGroupInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _rmGroupsService.createOrEdit(
				rmGroup
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditRMGroupModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);