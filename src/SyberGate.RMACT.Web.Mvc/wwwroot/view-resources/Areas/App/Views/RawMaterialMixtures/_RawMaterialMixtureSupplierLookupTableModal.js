(function ($) {
    app.modals.SupplierLookupTableModal = function () {

        var _modalManager;

        var _rawMaterialMixturesService = abp.services.app.rawMaterialMixtures;
        var _$supplierTable = $('#SupplierTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$supplierTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _rawMaterialMixturesService.getAllSupplierForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#SupplierTableFilter').val()
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

        $('#SupplierTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getSupplier() {
            dataTable.ajax.reload();
        }

        $('#GetSupplierButton').click(function (e) {
            e.preventDefault();
            getSupplier();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getSupplier();
            }
        });

    };
})(jQuery);

