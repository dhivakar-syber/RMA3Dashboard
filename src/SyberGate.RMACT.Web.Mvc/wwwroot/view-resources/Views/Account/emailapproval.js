(function () {
    $(function () {
        console.log('emailapproval')
 $('#register-submit-btn').click(function () {
            var Remarks = $('.form-group textarea[name="Remarks"]').val();

            var _a3DocumentsService = abp.services.app.a3Documents;
            var _tenantDashboardService = abp.services.app.tenantDashboard;
            var a3IdValue = $('.form-group input[name="A3id"]').val();
            var Token = $('.form-group input[name="Token"]').val();
            var issequence = $('.form-group input[name="Sequence"]').val();
            var uid = $('.form-group input[name="Uid"]').val();

            _a3DocumentsService.emailApprove(
                a3IdValue,
                Token,
                issequence,
                Remarks,
                uid
            ).done(function () {
                abp.notify.success(app.localize('SuccessfullyUpdated'));
                window.close();
            });

     $(this).hide();

     $('#success-message').show();
        });
    });

    
    
})();
