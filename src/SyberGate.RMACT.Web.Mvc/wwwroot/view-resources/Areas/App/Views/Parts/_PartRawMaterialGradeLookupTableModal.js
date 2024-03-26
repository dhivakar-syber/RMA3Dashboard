(function ($) {
    app.modals.RawMaterialGradeLookupTableModal = function () {

        var _modalManager;

        var _partsService = abp.services.app.parts;
        var _$rawMaterialGradeTable = $('#RawMaterialGradeTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$rawMaterialGradeTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _partsService.getAllRawMaterialGradeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#RawMaterialGradeTableFilter').val()
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

        $('#RawMaterialGradeTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getRawMaterialGrade() {
            dataTable.ajax.reload();
        }

        $('#GetRawMaterialGradeButton').click(function (e) {
            e.preventDefault();
            getRawMaterialGrade();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getRawMaterialGrade();
            }
        });

    };
})(jQuery);

