(function ($) {
    app.modals.CreateOrEditPartModal = function () {

        var _partsService = abp.services.app.parts;

        var _modalManager;
        var _$partInformationForm = null;

		        var _PartsupplierLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Parts/SupplierLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Parts/_PartSupplierLookupTableModal.js',
            modalClass: 'SupplierLookupTableModal'
        });        var _PartbuyerLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Parts/BuyerLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Parts/_PartBuyerLookupTableModal.js',
            modalClass: 'BuyerLookupTableModal'
        });        var _PartrmGroupLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Parts/RMGroupLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Parts/_PartRMGroupLookupTableModal.js',
            modalClass: 'RMGroupLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$partInformationForm = _modalManager.getModal().find('form[name=PartInformationsForm]');
            _$partInformationForm.validate();
        };

		          $('#OpenSupplierLookupTableButton').click(function () {

            var part = _$partInformationForm.serializeFormToObject();

            _PartsupplierLookupTableModal.open({ id: part.supplierId, displayName: part.supplierName }, function (data) {
                _$partInformationForm.find('input[name=supplierName]').val(data.displayName); 
                _$partInformationForm.find('input[name=supplierId]').val(data.id); 
            });
        });
		
		$('#ClearSupplierNameButton').click(function () {
                _$partInformationForm.find('input[name=supplierName]').val(''); 
                _$partInformationForm.find('input[name=supplierId]').val(''); 
        });
		
        $('#OpenBuyerLookupTableButton').click(function () {

            var part = _$partInformationForm.serializeFormToObject();

            _PartbuyerLookupTableModal.open({ id: part.buyerId, displayName: part.buyerName }, function (data) {
                _$partInformationForm.find('input[name=buyerName]').val(data.displayName); 
                _$partInformationForm.find('input[name=buyerId]').val(data.id); 
            });
        });
		
		$('#ClearBuyerNameButton').click(function () {
                _$partInformationForm.find('input[name=buyerName]').val(''); 
                _$partInformationForm.find('input[name=buyerId]').val(''); 
        });
		
        $('#OpenRMGroupLookupTableButton').click(function () {

            var part = _$partInformationForm.serializeFormToObject();

            _PartrmGroupLookupTableModal.open({ id: part.rmGroupId, displayName: part.rmGroupName }, function (data) {
                _$partInformationForm.find('input[name=rmGroupName]').val(data.displayName); 
                _$partInformationForm.find('input[name=rmGroupId]').val(data.id); 
            });
        });
		
		$('#ClearRMGroupNameButton').click(function () {
                _$partInformationForm.find('input[name=rmGroupName]').val(''); 
                _$partInformationForm.find('input[name=rmGroupId]').val(''); 
        });
		


        this.save = function () {
            if (!_$partInformationForm.valid()) {
                return;
            }
            if ($('#Part_SupplierId').prop('required') && $('#Part_SupplierId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Supplier')));
                return;
            }
            if ($('#Part_BuyerId').prop('required') && $('#Part_BuyerId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Buyer')));
                return;
            }
            if ($('#Part_RMGroupId').prop('required') && $('#Part_RMGroupId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('RMGroup')));
                return;
            }

            var part = _$partInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _partsService.createOrEdit(
				part
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditPartModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);