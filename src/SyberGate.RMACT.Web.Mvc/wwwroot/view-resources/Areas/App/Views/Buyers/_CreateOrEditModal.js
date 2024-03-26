(function ($) {
    app.modals.CreateOrEditBuyerModal = function () {

        var _buyersService = abp.services.app.buyers;

        var _modalManager;
        var _$buyerInformationForm = null;

		        var _BuyeruserLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Buyers/UserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Buyers/_BuyerUserLookupTableModal.js',
            modalClass: 'UserLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$buyerInformationForm = _modalManager.getModal().find('form[name=BuyerInformationsForm]');
            _$buyerInformationForm.validate();
        };

		          $('#OpenUserLookupTableButton').click(function () {

            var buyer = _$buyerInformationForm.serializeFormToObject();

            _BuyeruserLookupTableModal.open({ id: buyer.userId, displayName: buyer.userName }, function (data) {
                _$buyerInformationForm.find('input[name=userName]').val(data.displayName); 
                _$buyerInformationForm.find('input[name=userId]').val(data.id); 
            });
        });
		
		$('#ClearUserNameButton').click(function () {
                _$buyerInformationForm.find('input[name=userName]').val(''); 
                _$buyerInformationForm.find('input[name=userId]').val(''); 
        });
		


        this.save = function () {
            if (!_$buyerInformationForm.valid()) {
                return;
            }
            if ($('#Buyer_UserId').prop('required') && $('#Buyer_UserId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('User')));
                return;
            }

            var buyer = _$buyerInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _buyersService.createOrEdit(
				buyer
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditBuyerModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);