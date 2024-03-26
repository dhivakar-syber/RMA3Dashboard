(function () {
    $(function () {
        console.log('emailapproval')
        var _a3DocumentsService = abp.services.app.a3Documents;

        function getA3Documents() {
            dataTable.ajax.reload();
        }


        var a3IdValue = $('.form-group input[name="A3id"]').val();


        function deleteA3Document() {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _a3DocumentsService.approve({
                            id: a3IdValue
                        }).done(function () {
                            getA3Documents(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        $('.register-submit-btn').click(function (e) {
            deleteA3Document();
        });

    });
})();
