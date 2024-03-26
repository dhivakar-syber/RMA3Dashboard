(function ($) {
    app.modals.CreateOrEditApprovalUserModal = function () {

        var _approvalUsersService = abp.services.app.approvalUsers;

        var _modalManager;
        var _$approvalUserInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$approvalUserInformationForm = _modalManager.getModal().find('form[name=ApprovalUserInformationsForm]');
            _$approvalUserInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$approvalUserInformationForm.valid()) {
                return;
            }

            

            var approvalUser = _$approvalUserInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _approvalUsersService.createOrEdit(
				approvalUser
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditApprovalUserModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);