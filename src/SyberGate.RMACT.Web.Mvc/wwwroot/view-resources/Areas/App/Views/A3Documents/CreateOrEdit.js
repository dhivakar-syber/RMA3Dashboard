(function () {
    $(function () {
        var _a3DocumentsService = abp.services.app.a3Documents;

        var _$a3DocumentInformationForm = $('form[name=A3DocumentInformationsForm]');
        _$a3DocumentInformationForm.validate();

		
   
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });
      
	    

        function save() {
            if (!_$a3DocumentInformationForm.valid()) {
                return;
            }

            var a3Document = _$a3DocumentInformationForm.serializeFormToObject();
			
			 abp.ui.setBusy();
			 _a3DocumentsService.createOrEdit(
				a3Document
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               abp.event.trigger('app.createOrEditA3DocumentModalSaved');
               clearForm();
			 }).always(function () {
			    abp.ui.clearBusy();
			});
        };
        
        function clearForm(){
            _$a3DocumentInformationForm[0].reset();
        }

        _$a3DocumentInformationForm.on('submit', function(){
            save();
        });
    });
})();