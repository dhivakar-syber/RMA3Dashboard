(function ($) {
    app.modals.CreateOrEditPartBucketModal = function () {

        var _partBucketsService = abp.services.app.partBuckets;

        var _modalManager;
        var _$partBucketInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$partBucketInformationForm = _modalManager.getModal().find('form[name=PartBucketInformationsForm]');
            _$partBucketInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$partBucketInformationForm.valid()) {
                return;
            }

            var partBucket = _$partBucketInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _partBucketsService.createOrEdit(
				partBucket
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditPartBucketModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);