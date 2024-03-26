(function ($) {
    app.modals.CreateOrEditSupplierModal = function () {

        var _suppliersService = abp.services.app.suppliers;

        var _modalManager;
        var _$supplierInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$supplierInformationForm = _modalManager.getModal().find('form[name=SupplierInformationsForm]');
            _$supplierInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$supplierInformationForm.valid()) {
                return;
            }

            var supplier = _$supplierInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _suppliersService.createOrEdit(
				supplier
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditSupplierModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);