(function ($) {
    app.modals.CreateOrEditPartModelMatrixModal = function () {

        var _partModelMatrixesService = abp.services.app.partModelMatrixes;

        var _modalManager;
        var _$partModelMatrixInformationForm = null;

		        var _PartModelMatrixleadModelLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/PartModelMatrixes/LeadModelLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/PartModelMatrixes/_PartModelMatrixLeadModelLookupTableModal.js',
            modalClass: 'LeadModelLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$partModelMatrixInformationForm = _modalManager.getModal().find('form[name=PartModelMatrixInformationsForm]');
            _$partModelMatrixInformationForm.validate();
        };

		          $('#OpenLeadModelLookupTableButton').click(function () {

            var partModelMatrix = _$partModelMatrixInformationForm.serializeFormToObject();

            _PartModelMatrixleadModelLookupTableModal.open({ id: partModelMatrix.leadModelId, displayName: partModelMatrix.leadModelName }, function (data) {
                _$partModelMatrixInformationForm.find('input[name=leadModelName]').val(data.displayName); 
                _$partModelMatrixInformationForm.find('input[name=leadModelId]').val(data.id); 
            });
        });
		
		$('#ClearLeadModelNameButton').click(function () {
                _$partModelMatrixInformationForm.find('input[name=leadModelName]').val(''); 
                _$partModelMatrixInformationForm.find('input[name=leadModelId]').val(''); 
        });
		


        this.save = function () {
            if (!_$partModelMatrixInformationForm.valid()) {
                return;
            }
            if ($('#PartModelMatrix_LeadModelId').prop('required') && $('#PartModelMatrix_LeadModelId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('LeadModel')));
                return;
            }

            var partModelMatrix = _$partModelMatrixInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _partModelMatrixesService.createOrEdit(
				partModelMatrix
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditPartModelMatrixModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);