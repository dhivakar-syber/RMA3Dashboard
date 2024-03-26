(function() {
    app.modals.EditCommodityTreeModal = function () {

        var _modalManager;
        var _commodityTreeService = abp.services.app.commodityTrees;
        var _$form = null;

        this.init = function(modalManager) {
            _modalManager = modalManager;

            _$form = _modalManager.getModal().find('form[name=CommodityTreeForm]');
            _$form.validate({ ignore: "" });
        };

        this.save = function() {
            if (!_$form.valid()) {
                return;
            }

            var commodityTree = _$form.serializeFormToObject();

            _modalManager.setBusy(true);
            _commodityTreeService.updateCommodityTree(
                commodityTree
            ).done(function(result) {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                _modalManager.setResult(result);
            }).always(function() {
                _modalManager.setBusy(false);
            });
        };
    };
})();