(function ($) {
    app.modals.CreateOrEditRawMaterialMixtureModal = function () {

        var _rawMaterialMixturesService = abp.services.app.rawMaterialMixtures;

        var _modalManager;
        var _$rawMaterialMixtureInformationForm = null;

		        var _RawMaterialMixturermGroupLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialMixtures/RMGroupLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialMixtures/_RawMaterialMixtureRMGroupLookupTableModal.js',
            modalClass: 'RMGroupLookupTableModal'
        });        var _RawMaterialMixturerawMaterialGradeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialMixtures/RawMaterialGradeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialMixtures/_RawMaterialMixtureRawMaterialGradeLookupTableModal.js',
            modalClass: 'RawMaterialGradeLookupTableModal'
        });        var _RawMaterialMixturebuyerLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialMixtures/BuyerLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialMixtures/_RawMaterialMixtureBuyerLookupTableModal.js',
            modalClass: 'BuyerLookupTableModal'
        });        var _RawMaterialMixturesupplierLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialMixtures/SupplierLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialMixtures/_RawMaterialMixtureSupplierLookupTableModal.js',
            modalClass: 'SupplierLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _rawMaterialMixturesService.getDefault({}).done(function (result) {
                console.log('test');
                //_$rawMaterialMixtureInformationForm.find('input[name=rmGroupName]').val(result.rmGroupName);
                //_$rawMaterialMixtureInformationForm.find('input[name=rmGroupId]').val(result.rmGroupId);
                _$rawMaterialMixtureInformationForm.find('input[name=buyerName]').val(result.buyerName);
                _$rawMaterialMixtureInformationForm.find('input[name=buyerId]').val(result.buyerId);
                _$rawMaterialMixtureInformationForm.find('input[name=supplierName]').val(result.supplierName);
                _$rawMaterialMixtureInformationForm.find('input[name=supplierId]').val(result.supplierId);
            });

            _$rawMaterialMixtureInformationForm = _modalManager.getModal().find('form[name=RawMaterialMixtureInformationsForm]');
            _$rawMaterialMixtureInformationForm.validate();
        };

		          $('#OpenRMGroupLookupTableButton').click(function () {

            var rawMaterialMixture = _$rawMaterialMixtureInformationForm.serializeFormToObject();

            _RawMaterialMixturermGroupLookupTableModal.open({ id: rawMaterialMixture.rmGroupId, displayName: rawMaterialMixture.rmGroupName }, function (data) {
                _$rawMaterialMixtureInformationForm.find('input[name=rmGroupName]').val(data.displayName); 
                _$rawMaterialMixtureInformationForm.find('input[name=rmGroupId]').val(data.id); 
            });
        });
		
		$('#ClearRMGroupNameButton').click(function () {
                _$rawMaterialMixtureInformationForm.find('input[name=rmGroupName]').val(''); 
                _$rawMaterialMixtureInformationForm.find('input[name=rmGroupId]').val(''); 
        });
		
        $('#OpenRawMaterialGradeLookupTableButton').click(function () {

            var rawMaterialMixture = _$rawMaterialMixtureInformationForm.serializeFormToObject();

            _RawMaterialMixturerawMaterialGradeLookupTableModal.open({ id: rawMaterialMixture.rawMaterialGradeId, displayName: rawMaterialMixture.rawMaterialGradeName }, function (data) {
                _$rawMaterialMixtureInformationForm.find('input[name=rawMaterialGradeName]').val(data.displayName); 
                _$rawMaterialMixtureInformationForm.find('input[name=rawMaterialGradeId]').val(data.id); 
            });
        });
		
		$('#ClearRawMaterialGradeNameButton').click(function () {
                _$rawMaterialMixtureInformationForm.find('input[name=rawMaterialGradeName]').val(''); 
                _$rawMaterialMixtureInformationForm.find('input[name=rawMaterialGradeId]').val(''); 
        });
		
        $('#OpenBuyerLookupTableButton').click(function () {

            var rawMaterialMixture = _$rawMaterialMixtureInformationForm.serializeFormToObject();

            _RawMaterialMixturebuyerLookupTableModal.open({ id: rawMaterialMixture.buyerId, displayName: rawMaterialMixture.buyerName }, function (data) {
                _$rawMaterialMixtureInformationForm.find('input[name=buyerName]').val(data.displayName); 
                _$rawMaterialMixtureInformationForm.find('input[name=buyerId]').val(data.id); 
            });
        });
		
		$('#ClearBuyerNameButton').click(function () {
                _$rawMaterialMixtureInformationForm.find('input[name=buyerName]').val(''); 
                _$rawMaterialMixtureInformationForm.find('input[name=buyerId]').val(''); 
        });
		
        $('#OpenSupplierLookupTableButton').click(function () {

            var rawMaterialMixture = _$rawMaterialMixtureInformationForm.serializeFormToObject();

            _RawMaterialMixturesupplierLookupTableModal.open({ id: rawMaterialMixture.supplierId, displayName: rawMaterialMixture.supplierName }, function (data) {
                _$rawMaterialMixtureInformationForm.find('input[name=supplierName]').val(data.displayName); 
                _$rawMaterialMixtureInformationForm.find('input[name=supplierId]').val(data.id); 
            });
        });
		
		$('#ClearSupplierNameButton').click(function () {
                _$rawMaterialMixtureInformationForm.find('input[name=supplierName]').val(''); 
                _$rawMaterialMixtureInformationForm.find('input[name=supplierId]').val(''); 
        });
		


        this.save = function () {
            if (!_$rawMaterialMixtureInformationForm.valid()) {
                return;
            }
            if ($('#RawMaterialMixture_RMGroupId').prop('required') && $('#RawMaterialMixture_RMGroupId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('RMGroup')));
                return;
            }
            if ($('#RawMaterialMixture_RawMaterialGradeId').prop('required') && $('#RawMaterialMixture_RawMaterialGradeId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('RawMaterialGrade')));
                return;
            }
            if ($('#RawMaterialMixture_BuyerId').prop('required') && $('#RawMaterialMixture_BuyerId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Buyer')));
                return;
            }
            if ($('#RawMaterialMixture_SupplierId').prop('required') && $('#RawMaterialMixture_SupplierId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Supplier')));
                return;
            }

            var rawMaterialMixture = _$rawMaterialMixtureInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _rawMaterialMixturesService.createOrEdit(
				rawMaterialMixture
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditRawMaterialMixtureModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);