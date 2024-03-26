(function ($) {
    app.modals.BuyerLookupTableModal = function () {

        var _modalManager;

        var _baseRMRatesService = abp.services.app.baseRMRates;
        var _$buyerTable = $('#BuyerTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$buyerTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _baseRMRatesService.getAllBuyerForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#BuyerTableFilter').val()
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

        $('#BuyerTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getBuyer() {
            dataTable.ajax.reload();
        }

        $('#GetBuyerButton').click(function (e) {
            e.preventDefault();
            getBuyer();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getBuyer();
            }
        });

    };
})(jQuery);

