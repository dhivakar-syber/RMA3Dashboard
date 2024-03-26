(function() {
    app.modals.CreateCommodityTreeModal = function () {

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
            _commodityTreeService.createCommodityTree(
                commodityTree
            ).done(function(result) {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.setResult(result);
                _modalManager.close();
            }).always(function() {
                _modalManager.setBusy(false);
            });
        };
    };
})();