(function ($) {
    app.modals.RMGradeLookupTableModal = function () {

        var _modalManager;

        var _partsService = abp.services.app.parts;
        var _$rmGradeTable = $('#RMGradeTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$rmGradeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _partsService.getAllRMGradeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#RMGradeTableFilter').val()
                    };
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" + app.localize('Select') + "' /></div>"
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "displayName"
                }
            ]
        });

        $('#RMGradeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getRMGrade() {
            dataTable.ajax.reload();
        }

        $('#GetRMGradeButton').click(function (e) {
            e.preventDefault();
            getRMGrade();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getRMGrade();
            }
        });

    };
})(jQuery);

