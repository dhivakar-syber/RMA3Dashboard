(function ($) {
    app.modals.PartBucketViewModal = function () {
        var _partBucketService = abp.services.app.partBuckets;
        var _$baseRMHistoryTable = $('#PartBucketTable');
        var _$container = $('.PartBucketContainer');

        var loadData = function () {
            abp.ui.setBusy(_$container);
            _partBucketService.getProcessDetails({
                partNumber: $('#PartNoFilterId').val(),
                rawMaterial: $('#RMFilterId').val(),
                price: $('#PriceFilter').val(),
                buyer: $('#BuyerFilterId').val(),
                supplier: $('#SupplierFilterId').val(),
                isCurrentPrice: $('#IsCurrentPriceFilter').val() == "1" ? true : false
            }).done(function (result) {
                loadPartBucket(result);             
            }).always(function () {
                abp.ui.clearBusy(_$container);
            });
        }

        var loadPartBucket = function (result) {
            var $tableBody = _$container.find('#PartBucketTable table tbody');
            
            var $tr = $('<tr></tr>').append($('<td>' + result["price"] + '</td>' ), $('<td>' + result["basePrice"] + '</td>')) ;
            for (var rowIndex = 0; rowIndex < result.partBucketDetails.length; rowIndex++) {
                $tr.append('<td>' + result.partBucketDetails[rowIndex]["value"] + '</td>');
            }
            $tableBody.append($tr);
        };

        
        loadData();
        console.log('PartBucketjs');
    }
})(jQuery);