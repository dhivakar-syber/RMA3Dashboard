(function ($) {
    app.modals.CreateOrEditIndexNameModal = function () {

        var _indexNamesService = abp.services.app.indexNames;

        var _modalManager;
        var _$indexNameInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$indexNameInformationForm = _modalManager.getModal().find('form[name=IndexNameInformationsForm]');
            _$indexNameInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$indexNameInformationForm.valid()) {
                return;
            }

            var indexName = _$indexNameInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _indexNamesService.createOrEdit(
				indexName
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditIndexNameModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);