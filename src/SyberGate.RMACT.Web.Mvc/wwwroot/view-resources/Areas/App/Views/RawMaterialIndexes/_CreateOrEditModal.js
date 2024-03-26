(function ($) {
    app.modals.CreateOrEditRawMaterialIndexModal = function () {

        var _rawMaterialIndexesService = abp.services.app.rawMaterialIndexes;

        var _modalManager;
        var _$rawMaterialIndexInformationForm = null;

		        var _RawMaterialIndexindexNameLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialIndexes/IndexNameLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialIndexes/_RawMaterialIndexIndexNameLookupTableModal.js',
            modalClass: 'IndexNameLookupTableModal'
        });        var _RawMaterialIndexyearLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialIndexes/YearLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialIndexes/_RawMaterialIndexYearLookupTableModal.js',
            modalClass: 'YearLookupTableModal'
        });        var _RawMaterialIndexrawMaterialGradeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialIndexes/RawMaterialGradeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialIndexes/_RawMaterialIndexRawMaterialGradeLookupTableModal.js',
            modalClass: 'RawMaterialGradeLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$rawMaterialIndexInformationForm = _modalManager.getModal().find('form[name=RawMaterialIndexInformationsForm]');
            _$rawMaterialIndexInformationForm.validate();
        };

		          $('#OpenIndexNameLookupTableButton').click(function () {

            var rawMaterialIndex = _$rawMaterialIndexInformationForm.serializeFormToObject();

            _RawMaterialIndexindexNameLookupTableModal.open({ id: rawMaterialIndex.indexNameId, displayName: rawMaterialIndex.indexNameName }, function (data) {
                _$rawMaterialIndexInformationForm.find('input[name=indexNameName]').val(data.displayName); 
                _$rawMaterialIndexInformationForm.find('input[name=indexNameId]').val(data.id); 
            });
        });
		
		$('#ClearIndexNameNameButton').click(function () {
                _$rawMaterialIndexInformationForm.find('input[name=indexNameName]').val(''); 
                _$rawMaterialIndexInformationForm.find('input[name=indexNameId]').val(''); 
        });
		
        $('#OpenYearLookupTableButton').click(function () {

            var rawMaterialIndex = _$rawMaterialIndexInformationForm.serializeFormToObject();

            _RawMaterialIndexyearLookupTableModal.open({ id: rawMaterialIndex.yearId, displayName: rawMaterialIndex.yearName }, function (data) {
                _$rawMaterialIndexInformationForm.find('input[name=yearName]').val(data.displayName); 
                _$rawMaterialIndexInformationForm.find('input[name=yearId]').val(data.id); 
            });
        });
		
		$('#ClearYearNameButton').click(function () {
                _$rawMaterialIndexInformationForm.find('input[name=yearName]').val(''); 
                _$rawMaterialIndexInformationForm.find('input[name=yearId]').val(''); 
        });
		
        $('#OpenRawMaterialGradeLookupTableButton').click(function () {

            var rawMaterialIndex = _$rawMaterialIndexInformationForm.serializeFormToObject();

            _RawMaterialIndexrawMaterialGradeLookupTableModal.open({ id: rawMaterialIndex.rawMaterialGradeId, displayName: rawMaterialIndex.rawMaterialGradeName }, function (data) {
                _$rawMaterialIndexInformationForm.find('input[name=rawMaterialGradeName]').val(data.displayName); 
                _$rawMaterialIndexInformationForm.find('input[name=rawMaterialGradeId]').val(data.id); 
            });
        });
		
		$('#ClearRawMaterialGradeNameButton').click(function () {
                _$rawMaterialIndexInformationForm.find('input[name=rawMaterialGradeName]').val(''); 
                _$rawMaterialIndexInformationForm.find('input[name=rawMaterialGradeId]').val(''); 
        });
		


        this.save = function () {
            if (!_$rawMaterialIndexInformationForm.valid()) {
                return;
            }
            if ($('#RawMaterialIndex_IndexNameId').prop('required') && $('#RawMaterialIndex_IndexNameId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('IndexName')));
                return;
            }
            if ($('#RawMaterialIndex_YearId').prop('required') && $('#RawMaterialIndex_YearId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Year')));
                return;
            }
            if ($('#RawMaterialIndex_RawMaterialGradeId').prop('required') && $('#RawMaterialIndex_RawMaterialGradeId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('RawMaterialGrade')));
                return;
            }

            var rawMaterialIndex = _$rawMaterialIndexInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _rawMaterialIndexesService.createOrEdit(
				rawMaterialIndex
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditRawMaterialIndexModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);