(function ($) {
    app.modals.CreateOrEditCommodityTreeModal = function () {

        var _commodityTreesService = abp.services.app.commodityTrees;

        var _modalManager;
        var _$commodityTreeInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$commodityTreeInformationForm = _modalManager.getModal().find('form[name=CommodityTreeInformationsForm]');
            _$commodityTreeInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$commodityTreeInformationForm.valid()) {
                return;
            }

            var commodityTree = _$commodityTreeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _commodityTreesService.createOrEdit(
				commodityTree
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditCommodityTreeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);