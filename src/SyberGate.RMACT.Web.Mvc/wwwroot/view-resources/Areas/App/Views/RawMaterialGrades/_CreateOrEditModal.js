(function ($) {
    app.modals.CreateOrEditRawMaterialGradeModal = function () {

        var _rawMaterialGradesService = abp.services.app.rawMaterialGrades;

        var _modalManager;
        var _$rawMaterialGradeInformationForm = null;

		        var _RawMaterialGraderawMaterialGradeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RawMaterialGrades/RawMaterialGradeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RawMaterialGrades/_RawMaterialGradeRawMaterialGradeLookupTableModal.js',
            modalClass: 'RawMaterialGradeLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$rawMaterialGradeInformationForm = _modalManager.getModal().find('form[name=RawMaterialGradeInformationsForm]');
            _$rawMaterialGradeInformationForm.validate();
        };

		          $('#OpenRawMaterialGradeLookupTableButton').click(function () {

            var rawMaterialGrade = _$rawMaterialGradeInformationForm.serializeFormToObject();

            _RawMaterialGraderawMaterialGradeLookupTableModal.open({ id: rawMaterialGrade.rawMaterialGradeId, displayName: rawMaterialGrade.rawMaterialGradeName }, function (data) {
                _$rawMaterialGradeInformationForm.find('input[name=rawMaterialGradeName]').val(data.displayName); 
                _$rawMaterialGradeInformationForm.find('input[name=rawMaterialGradeId]').val(data.id); 
            });
        });
		
		$('#ClearRawMaterialGradeNameButton').click(function () {
                _$rawMaterialGradeInformationForm.find('input[name=rawMaterialGradeName]').val(''); 
                _$rawMaterialGradeInformationForm.find('input[name=rawMaterialGradeId]').val(''); 
        });
		
        $('#RawMaterialGrade_IsGroup').change(function () {
            if ($(this).is(':checked')) {
                $('#RawMaterialGrade_HasMixture').prop('checked', false);
            }
        });

        $('#RawMaterialGrade_HasMixture').change(function () {
            if ($(this).is(':checked')) {
                $('#RawMaterialGrade_IsGroup').prop('checked', false);
            }
        });

        this.save = function () {
            if (!_$rawMaterialGradeInformationForm.valid()) {
                return;
            }
            if ($('#RawMaterialGrade_RawMaterialGradeId').prop('required') && $('#RawMaterialGrade_RawMaterialGradeId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('RawMaterialGrade')));
                return;
            }

            var rawMaterialGrade = _$rawMaterialGradeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _rawMaterialGradesService.createOrEdit(
				rawMaterialGrade
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditRawMaterialGradeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);