(function ($) {
    app.modals.RMGroupLookupTableModal = function () {

        var _modalManager;

        var _rawMaterialMixturesService = abp.services.app.rawMaterialMixtures;
        var _$rmGroupTable = $('#RMGroupTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$rmGroupTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _rawMaterialMixturesService.getAllRMGroupForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#RMGroupTableFilter').val()
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

        $('#RMGroupTable tbody').on('click', '[id*=selectbtn]', function () {
            console.log('ytr');
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
          /*  _rawMaterialMixturesService.getName(data.displayName);*/


        });

        function getRMGroup() {
            dataTable.ajax.reload();
        }

        $('#GetRMGroupButton').click(function (e) {
            e.preventDefault();
            getRMGroup();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getRMGroup();
            }
        });

    };
})(jQuery);

