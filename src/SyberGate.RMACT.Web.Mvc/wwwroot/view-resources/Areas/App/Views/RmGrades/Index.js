(function () {
    $(function () {

        var _commodityTreeService = abp.services.app.commodityTrees;
        var _entityTypeFullName = 'Abp.RawMaterialGrade';

        var _permissions = {
            manageCommodityTree: abp.auth.hasPermission('Pages_Administration_RawMaterialGrades')
        };

        var _createModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RmGrades/CreateModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RmGrades/_CreateModal.js',
            modalClass: 'CreateCommodityTreeModal'
        });

        var _editModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/RmGrades/EditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/RmGrades/_EditModal.js',
            modalClass: 'EditCommodityTreeModal'
        });

        var _entityTypeHistoryModal = app.modals.EntityTypeHistoryModal.create();

        function entityHistoryIsEnabled() {
            return abp.custom.EntityHistory &&
                abp.custom.EntityHistory.IsEnabled &&
                _.filter(abp.custom.EntityHistory.EnabledEntities, function (entityType) {
                    return entityType === _entityTypeFullName;
                }).length === 1;
        }

        var commodityTree = {

            $tree: $('#CommodityEditTree'),

            $emptyInfo: $('#CommodityTreeEmptyInfo'),

            show: function () {
                commodityTree.$emptyInfo.hide();
                commodityTree.$tree.show();
            },

            hide: function () {
                commodityTree.$emptyInfo.show();
                commodityTree.$tree.hide();
            },

            commodityCount: 0,

            setCommodityCount: function (Count) {
                commodityTree.commodityCount = Count;
                if (Count) {
                    commodityTree.show();
                } else {
                    commodityTree.hide();
                }
            },

            refreshCommodityCount: function () {
                commodityTree.setCommodityCount(commodityTree.$tree.jstree('get_json').length);
            },

            selectedC: {
                id: null,
                displayName: null,
                code: null,
                isLeaf: null,

                set: function (cmoInTree) {
                    if (!cmoInTree) {
                        commodityTree.selectedC.id = null;
                        commodityTree.selectedC.displayName = null;
                        commodityTree.selectedC.code = null;
                        commodityTree.selectedC.isLeaf = false;
                    } else {
                        commodityTree.selectedC.id = cmoInTree.id;
                        commodityTree.selectedC.displayName = cmoInTree.original.displayName;
                        commodityTree.selectedC.code = cmoInTree.original.code;
                        commodityTree.selectedC.isLeaf = cmoInTree.original.isLeaf;
                    }
                }
            },

            contextMenu: function (node) {

                var items = {
                    editCmo: {
                        label: app.localize('Edit'),
                        icon: 'la la-pencil',
                        _disabled: !_permissions.manageCommodityTree,
                        action: function (data) {
                            var instance = $.jstree.reference(data.reference);

                            _editModal.open({
                                id: node.id
                            },
                                function (updatedC) {
                                    node.original.isLeaf = updatedC.isLeaf;
                                    node.original.displayName = updatedC.displayName;
                                    instance.rename_node(node, commodityTree.generateTextOnTree(updatedC));
                                });
                        }
                    },

                    addSubCmo: {
                        label: app.localize('AddRmGrades'),
                        icon: 'la la-plus',
                        _disabled: !_permissions.manageCommodityTree,
                        action: function () {
                            commodityTree.addCommodity(node.id);
                        }
                    },

                    'delete': {
                        label: app.localize("Delete"),
                        icon: 'la la-remove',
                        _disabled: !_permissions.manageCommodityTree,
                        action: function (data) {
                            var instance = $.jstree.reference(data.reference);

                            abp.message.confirm(
                                app.localize('CommodityDeleteWarningMessage', node.original.displayName),
                                app.localize('AreYouSure'),
                                function (isConfirmed) {
                                    if (isConfirmed) {
                                        _commodityTreeService.deleteCommodityTree({
                                            id: node.id
                                        }).done(function () {
                                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                                            instance.delete_node(node);
                                            commodityTree.refreshCommodityCount();
                                        }).fail(function (err) {
                                            setTimeout(function () { abp.message.error(err.message); }, 500);
                                        });
                                    }
                                }
                            );
                        }
                    }
                };

                if (entityHistoryIsEnabled()) {
                    items.history = {
                        label: app.localize('History'),
                        icon: 'la la-history',
                        _disabled: !_permissions.manageCommodityTree,
                        action: function () {
                            _entityTypeHistoryModal.open({
                                entityTypeFullName: _entityTypeFullName,
                                entityId: node.original.id,
                                entityTypeDescription: node.original.displayName,
                                entityIsLeaf: node.original.isLeaf
                            });
                        }
                    };
                }

                return items;
            },

            addCommodity: function (parentId) {
                var instance = $.jstree.reference(commodityTree.$tree);

                _createModal.open({
                    parentId: parentId
                }, function (newCmo) {
                    instance.create_node(
                        parentId ? instance.get_node(parentId) : '#',
                        {
                            id: newCmo.id,
                            parent: newCmo.parentId ? newCmo.parentId : '#',
                            code: newCmo.code,
                            displayName: newCmo.displayName,
                            isLeaf: newCmo.isLeaf,
                            memberCount: 0,
                            roleCount: 0,
                            text: commodityTree.generateTextOnTree(newCmo),
                            state: {
                                opened: true
                            }
                        });

                    commodityTree.refreshCommodityCount();
                });
            },

            generateTextOnTree: function (cmo) {
                var itemClass = (cmo.memberCount > 0 || cmo.roleCount) ? ' ou-text-has-members' : ' ou-text-no-members';
                return '<span title="' + cmo.code + '" class="ou-text text-dark' + itemClass + '" data-ou-id="' + cmo.id + '">' +
                    app.htmlUtils.htmlEncodeText(cmo.displayName) +
                    ' <i class="fa fa-caret-down text-muted"></i> ' +
                    ' <span style="font-size: .82em; ">' +
                    ' <span class="badge danger2-rmact ou-text-member-count ml-2">' + (cmo.isLeaf ? 'Leaf' : '') +
                    '</span></span></span>';
            },

            incrementMemberCount: function (ouId, incrementAmount) {
                var treeNode = commodityTree.$tree.jstree('get_node', ouId);
                treeNode.original.memberCount = treeNode.original.memberCount + incrementAmount;
                commodityTree.$tree.jstree('rename_node', treeNode, commodityTree.generateTextOnTree(treeNode.original));
            },

            incrementRoleCount: function (ouId, incrementAmount) {
                var treeNode = commodityTree.$tree.jstree('get_node', ouId);
                treeNode.original.roleCount = treeNode.original.roleCount + incrementAmount;
                commodityTree.$tree.jstree('rename_node', treeNode, commodityTree.generateTextOnTree(treeNode.original));
            },

            getTreeDataFromServer: function (callback) {
                _commodityTreeService.getCommodityTrees({}).done(function (result) {
                    var treeData = _.map(result.items, function (item) {
                        return {
                            id: item.id,
                            parent: item.parentId ? item.parentId : '#',
                            code: item.code,
                            displayName: item.displayName,
                            isLeaf: item.isLeaf,
                            text: commodityTree.generateTextOnTree(item),
                            state: {
                                opened: true
                            }
                        };
                    });

                    callback(treeData);
                });
            },

            init: function () {
                commodityTree.getTreeDataFromServer(function (treeData) {

                    commodityTree.setCommodityCount(treeData.length);

                    commodityTree.$tree
                        .on('changed.jstree', function (e, data) {
                            if (data.selected.length != 1) {
                                commodityTree.selectedC.set(null);
                            } else {
                                var selectedNode = data.instance.get_node(data.selected[0]);
                                commodityTree.selectedC.set(selectedNode);
                            }
                        })
                        .on('move_node.jstree', function (e, data) {

                            var parentNodeName = (!data.parent || data.parent == '#')
                                ? app.localize('Root')
                                : commodityTree.$tree.jstree('get_node', data.parent).original.displayName;

                            abp.message.confirm(
                                app.localize('CommodityTreeMoveConfirmMessage', data.node.original.displayName, parentNodeName),
                                app.localize('AreYouSure'),
                                function (isConfirmed) {
                                    if (isConfirmed) {
                                        _commodityTreeService.moveCommodityTree({
                                            id: data.node.id,
                                            newParentId: data.parent === '#' ? null : data.parent
                                        }).done(function () {
                                            abp.notify.success(app.localize('SuccessfullyMoved'));
                                            commodityTree.reload();
                                        }).fail(function (err) {
                                            commodityTree.$tree.jstree('refresh'); //rollback
                                            setTimeout(function () { abp.message.error(err.message); }, 500);
                                        });
                                    } else {
                                        commodityTree.$tree.jstree('refresh'); //rollback
                                    }
                                }
                            );
                        })
                        .jstree({
                            'core': {
                                data: treeData,
                                multiple: false,
                                check_callback: function (operation, node, node_parent, node_position, more) {
                                    return true;
                                }
                            },
                            types: {
                                "default": {
                                    "icon": "fa fa-folder kt--font-warning"
                                },
                                "file": {
                                    "icon": "fa fa-file  kt--font-warning"
                                }
                            },
                            contextmenu: {
                                items: commodityTree.contextMenu
                            },
                            sort: function (node1, node2) {
                                if (this.get_node(node2).original.displayName < this.get_node(node1).original.displayName) {
                                    return 1;
                                }

                                return -1;
                            },
                            plugins: [
                                'types',
                                'contextmenu',
                                'wholerow',
                                'sort',
                                'dnd'
                            ]
                        });

                    $('#AddRootCommodityButton').click(function (e) {
                        console.log("Add Root Commodity");
                        e.preventDefault();
                        commodityTree.addCommodity(null);
                    });

                    commodityTree.$tree.on('click', '.ou-text .fa-caret-down', function (e) {
                        e.preventDefault();

                        var ouId = $(this).closest('.ou-text').attr('data-ou-id');
                        setTimeout(function () {
                            commodityTree.$tree.jstree('show_contextmenu', ouId);
                        }, 100);
                    });
                });
            },

            reload: function () {
                commodityTree.getTreeDataFromServer(function (treeData) {
                    commodityTree.setCommodityCount(treeData.length);
                    commodityTree.$tree.jstree(true).settings.core.data = treeData;
                    commodityTree.$tree.jstree('refresh');
                });
            }
        };


        commodityTree.init();

        KTUtil.ready(function() {
            KTLayoutStretchedCard.init('ouCard');
            KTLayoutStretchedCard.init('ouMembersCard');
        });
        
    });
})();
