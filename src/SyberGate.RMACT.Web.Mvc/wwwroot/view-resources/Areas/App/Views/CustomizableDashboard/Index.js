$(function () {

    console.log('index.js')
    var _applicationPrefix = "Mvc";
    var _dashboardCustomizationService = abp.services.app.dashboardCustomization;
    var _partsService = abp.services.app.parts;
    var _tenantDashboardService = abp.services.app.tenantDashboard;
    var _$Buyer;
    var _$Supplier;
    var _$Grade;
    var _$Spec;
    var _$Group;
    var _$Plant;
    var _buyer;
    var _supplier;
    var _group;
    var _grade;
    var _spec;
    var _plant;
    var _$dateRange;
    var _dtrange;
    var _userService = abp.services.app.user

    var _addWidgetModal = new app.ModalManager({
        viewUrl: abp.appPath + 'App/' + $('#DashboardName').val() + '/AddWidgetModal',
        scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/CustomizableDashboard/_AddWidgetModal.js',
        modalClass: 'AddWidgetModal'
    });

    var toggleToolbarButtonsVisibility = function (active) {
        if (active) {
            $(".deleteWidgetButton").removeClass("d-none");
            $(".div-dashboard-customization").removeClass("d-none");
        } else {
            $(".deleteWidgetButton").addClass("d-none");
            $(".div-dashboard-customization").addClass("d-none");
        }
    };

    var enableGrid = function () {

        console.log('test');
        toggleToolbarButtonsVisibility(true);
        $('.grid-stack').each(function () {
            var grid = $(this).data('gridstack');
            grid.enable();
        });
    };

    var disableGrid = function () {
        toggleToolbarButtonsVisibility(false);
        $('.grid-stack').each(function () {
            var grid = $(this).data('gridstack');
            grid.disable();
        });
    };

    $('.grid-stack').gridstack({
        alwaysShowResizeHandle: false,
        cellHeight: 'auto',
        animate: true
    });

    //$('.grid-stack').on('enable', function (e, items) {
    //    //console.log(test);
    //    var grid = $(this).data('gridstack');
    //    if ($('#RMPriceTrend_Content')) {
    //        grid.resize(
    //            $('.grid-stack-item')[0],
    //            $($('.grid-stack-item')[0]).attr('data-gs-width'),
    //            Math.ceil(($('#RMPriceTrend_Content').height() + 140 + grid.opts.verticalMargin) / (grid.cellHeight() + grid.opts.verticalMargin))
    //        );
    //    }

    //    if ($('RMPriceImpact_Content')) {
    //        grid.resize(
    //            $('.grid-stack-item')[1],
    //            $($('.grid-stack-item')[1]).attr('data-gs-width'),
    //            Math.ceil(($('#RMPriceImpact_Content').height() + 140 + grid.opts.verticalMargin) / (grid.cellHeight() + grid.opts.verticalMargin))
    //        );
    //    }
  
    //})

    var refreshPage = function () {
        location.reload();
    };

    var getCurrentPageId = function () {
        return $('#PagesDiv').find('.active')
            .find('input[name="PageId"]').val();
    };

    var getCurrentPageName = function () {
        return $('#PagesDiv').find('.active')
            .find('input[name="PageName"]').val();
    };

    var savePageData = function () {
        abp.ui.setBusy($("body"));

        var pageContent = [];
        var pages = $('#PagesDiv').find('.page');

        for (var j = 0; j < pages.length; j++) {
            var page = pages[j];
            var pageId = $(page).find('input[name="PageId"]').val();
            var pageName = $(page).find('input[name="PageName"]').val();
            var widgetStackItems = $(page).find('.grid-stack-item');
            var widgets = [];

            for (var i = 0; i < widgetStackItems.length; i++) {
                var widget = {};
                widget.widgetId = $(widgetStackItems[i]).attr('data-widget-id');
                widget.height = $(widgetStackItems[i]).attr('data-gs-height');
                widget.width = $(widgetStackItems[i]).attr('data-gs-width');
                widget.positionX = $(widgetStackItems[i]).attr('data-gs-x');
                widget.positionY = $(widgetStackItems[i]).attr('data-gs-y');
                widgets[i] = widget;
            }
            pageContent.push({
                id: pageId,
                name: pageName,
                widgets: widgets
            });
        }

        var filters = [];

        var filterDiv = $('#FiltersDiv');
        if (filterDiv) {
            var filtersStackItems = $(filterDiv).find('.grid-stack-item');

            for (var i = 0; i < filtersStackItems.length; i++) {
                var filter = {};
                filter.widgetFilterId = $(filtersStackItems[i]).attr('data-filter-id');
                filter.height = $(filtersStackItems[i]).attr('data-gs-height');
                filter.width = $(filtersStackItems[i]).attr('data-gs-width');
                filter.positionX = $(filtersStackItems[i]).attr('data-gs-x');
                filter.positionY = $(filtersStackItems[i]).attr('data-gs-y');
                filters[i] = filter;
            }
        }

        _dashboardCustomizationService
            .savePage({
                dashboardName: $('#DashboardName').val(),
                pages: pageContent,
                widgetFilters: filters,
                application: _applicationPrefix
            }).done(function (result) {
                abp.notify.success(app.localize('Saved'));
                $('#EditableCheckbox').prop('checked', false).trigger('change');
            }).always(function () {
                abp.ui.clearBusy($("body"));
            });
    };

    $('#AddWidgetButton').click(function () {
        _addWidgetModal.open({
            dashboardName: $('#DashboardName').val(),
            pageId: getCurrentPageId()
        });
    });

    abp.event.on('app.addWidgetModalSaved', function () {
        refreshPage();
    });

    $('#DeletePageButton').click(function () {
        var pageCount = $("#dashboardPageCount").val();
        var message = pageCount > 1
            ? app.localize('PageDeleteWarningMessage', getCurrentPageName())
            : app.localize('BackToDefaultPageWarningMessage', getCurrentPageName());

        abp.message.confirm(
            message,
            app.localize('AreYouSure'),
            function (isConfirmed) {
                if (isConfirmed) {
                    _dashboardCustomizationService
                        .deletePage({
                            dashboardName: $('#DashboardName').val(),
                            id: getCurrentPageId(),
                            application: _applicationPrefix
                        })
                        .done(function (result) {
                            refreshPage();
                        });
                }
            }
        );
    });

    $('#RenamePageSaveButton').on('click', function () {
        let newName = $('#RenamePageNameInput').val();
        newName = newName.trim();
        if (newName === '') {
            abp.notify.error(app.localize("PageNameCanNotBeEmpty"));
            return;
        }

        _dashboardCustomizationService
            .renamePage({
                dashboardName: $('#DashboardName').val(),
                id: getCurrentPageId(),
                name: newName,
                application: _applicationPrefix
            })
            .done(function (result) {
                abp.notify.success(app.localize('Renamed'));
                refreshPage();
            });
    });

    $('#AddPageSaveButton').on('click', function () {
        let newName = $('#PageNameInput').val();
        newName = newName.trim();
        if (newName.trim() === '') {
            abp.notify.error(app.localize("PageNameCanNotBeEmpty"));
            return;
        }

        _dashboardCustomizationService
            .addNewPage({
                dashboardName: $('#DashboardName').val(),
                name: newName,
                application: _applicationPrefix
            })
            .done(function (result) {
                abp.notify.success(app.localize('Saved'));
                refreshPage();
            });
    });

    $('#RenamePageDropdownMenuButton').on('click', function () {
        $('#RenamePageNameInput').attr('placeholder', getCurrentPageName());
        $('#RenamePageNameInput').val("");
    });

    $('#AddPageButtonDropdownMenuButton').on('click', function () {
        $('#PageNameInput').val("");
    });

    $('#EditableCheckbox').change(function () {
        if ($(this).is(":checked")) {
            enableGrid();
            $(this).closest(".switch").addClass("switch-primary");
        }
        else {
            disableGrid();
            $(this).closest(".switch").removeClass("switch-primary");
        }
    });

    $(document).on('click', '.deleteWidgetButton', function () {
        var stackItem = $(this).closest(".grid-stack-item");
        abp.message.confirm(
            app.localize('WidgetDeleteWarningMessage', stackItem.attr("data-widget-name"), getCurrentPageName()),
            app.localize('AreYouSure'),
            function (isConfirmed) {
                if (isConfirmed) {
                    stackItem.remove();
                }
            }
        );
    });

    $('#savePageButton').on('click', function () {
        savePageData();
    });

    function initialize() {
        $(".grid-stack-item-content").mCustomScrollbar({
            theme: "minimal-dark"
        });

        $('.grid-stack').each(function () {
            var grid = $(this).data('gridstack');
            grid.disable();
        });
    }
    $("#btn-config-a3").click(function () {

        console.log('config');

        $('#a3-config').val('');
        $('#a3-l4-config_Dept').html('<option value="0">SectionHead</option>');
        $('#a3-cp-config_Dept').html('<option value="0">CE</option>');
        $('#a3-fin-config_Dept').html('<option value="0">Finance</option>');
        $('#a3-comexp-config_Dept').html('<option value="0">Commodity</option>');

        var L4dropdown = $("#a3-l4-config-username");
        var cpdropdown = $("#a3-cp-config-username");
        var findropdown = $("#a3-fin-config-username");
        var comexpdropdown = $("#a3-comexp-config-username");

       
        
        
        _tenantDashboardService
            .getApprovalUserNames('SectionHead')
            .done(function (result) {
                //L4dropdown.html('<option value="0">selectusername</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    
                    L4dropdown.append('<option value="' + result[rowIndex].userName + '">' + result[rowIndex].userName + '</option')
                }


            });
        _tenantDashboardService
            .getApprovalUserNames('CE')
            .done(function (result) {
                //cpdropdown.html('<option value="0">selectusername</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    
                    cpdropdown.append('<option value="' + result[rowIndex].userName + '">' + result[rowIndex].userName + '</option')
                }


            });
        _tenantDashboardService
            .getApprovalUserNames('Finance')
            .done(function (result) {
                //findropdown.html('<option value="0">selectusername</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                   
                    findropdown.append('<option value="' + result[rowIndex].userName + '">' + result[rowIndex].userName + '</option')
                }


            });
        _tenantDashboardService
            .getApprovalUserNames('Commodity')
            .done(function (result) {
                //comexpdropdown.html('<option value="0">selectusername</option')
                for (var rowIndex = 0; rowIndex < result.length; rowIndex++) {
                    
                    comexpdropdown.append('<option value="' + result[rowIndex].userName + '">' + result[rowIndex].userName + '</option')
                }


            });

        

        

        _tenantDashboardService.getUserDetails().done(function (result) {

            var l4username = result.l4UserName;
            var cpusername = result.cpUserName;
            var finusername = result.finUserName;
            var comexpusername = result.commadityExpertUserName;

            $("#a3-l4-config-username").val(l4username);
            $("#a3-cp-config-username").val(cpusername);
            $("#a3-fin-config-username").val(finusername);   
            $("#a3-comexp-config-username").val(comexpusername);

            $("#a3-l4-config-email").val(result.l4EmailAddress);
            $("#a3-cp-config-email").val(result.cpEmailAddress);
            $("#a3-fin-config-email").val(result.finEmailAddress);
            $("#a3-comexp-config-email").val(result.commadityExpertEmailAddress);
            $('#a3-comexp-config-checkbox').prop('checked', result.sequenceCheckBox);
        });
       

       $('#configModal').show();
        $('#configModal').on('show.bs.modal', function () {
            _tenantDashboardService.getUserDetails().done(function (result) {
                var l4username = result.l4UserName;
                var cpusername = result.cpUserName;
                var finusername = result.finUserName;
                var comexpusername = result.commadityExpertUserName;

                
                $("#a3-l4-config-username").val(l4username);

                $("#a3-cp-config-username").val(cpusername);
                $("#a3-fin-config-username").val(finusername);
                $("#a3-comexp-config-username").val(comexpusername);

                $("#a3-l4-config-email").val(result.l4EmailAddress);
                $("#a3-cp-config-email").val(result.cpEmailAddress);
                $("#a3-fin-config-email").val(result.finEmailAddress);
                $("#a3-comexp-config-email").val(result.commadityExpertEmailAddress);
                $('#a3-comexp-config-checkbox').prop('checked', result.sequenceCheckBox);
            });
        });

        
    });


    
    
        
    $('#a3-l4-config-username').change(function () {
        var selectedValue = $(this).val();
        var dept = $('#a3-l4-config_Dept').find('option:selected').text();
        
        _tenantDashboardService
            .getApprovalUserEmailAddress('SectionHead',selectedValue).done(function (result) {
                $("#a3-l4-config-email").val(result);
            });
        

    });
    $('#a3-cp-config-username').change(function () {
        var selectedValue = $(this).val();
        var dept = $('#a3-cp-config_Dept').find('option:selected').text();

        _tenantDashboardService
            .getApprovalUserEmailAddress('CE',selectedValue).done(function (result) {
                $("#a3-cp-config-email").val(result);
            });


    });
    $('#a3-fin-config-username').change(function () {
        var selectedValue = $(this).val();
        var dept = $('#a3-fin-config_Dept').find('option:selected').text();

        _tenantDashboardService
            .getApprovalUserEmailAddress('Finance',selectedValue).done(function (result) {
                $("#a3-fin-config-email").val(result);
            });


    });
    $('#a3-comexp-config-username').change(function () {
        var selectedValue = $(this).val();
        var dept = $('#a3-comexp-config_Dept').find('option:selected').text();

        _tenantDashboardService
            .getApprovalUserEmailAddress('Commodity',selectedValue).done(function (result) {
                $("#a3-comexp-config-email").val(result);
            });


    });
    





    $('#configModal .save-button').click(function () {
        console.log('config')
        abp.ui.setBusy($("body"));
        var l4mail = $('#a3-l4-config-email').val(); 
        var l4username = $('#a3-l4-config-username').find('option:selected').text();
        var l4dept = $('#a3-l4-config_Dept').find('option:selected').text();
        var cpmail = $('#a3-cp-config-email').val();
        var cpusername = $('#a3-cp-config-username').find('option:selected').text();
        var cpdept = $('#a3-cp-config_Dept').find('option:selected').text();
        var finmail = $('#a3-fin-config-email').val();
        var finusername = $('#a3-fin-config-username').find('option:selected').text();
        var findept = $('#a3-fin-config_Dept').find('option:selected').text();
        var comexpemail = $('#a3-comexp-config-email').val();
        var comexpusername = $('#a3-comexp-config-username').find('option:selected').text();
        var comexpdept = $('#a3-comexp-config_Dept').find('option:selected').text();
        var comexpcheckbox = $('#a3-comexp-config-checkbox').prop('checked');
        _tenantDashboardService.configupdate(
            abp.session.userId,
            l4mail,l4username,l4dept,
            cpmail,cpusername,cpdept,
            finmail, finusername, findept,
            comexpemail, comexpusername, comexpdept, comexpcheckbox
        ).done(function () {
            abp.notify.info(app.localize('User Details Saved Successfully'));
            $('#configModal').hide();
            
        }).always(function () {
            abp.ui.clearBusy($("body"));
        });

    });

    $('#configModal .close-button').click(function () {
        console.log('config')
         $("#a3-l4-config-username").html('');
        $("#a3-cp-config-username").html('');
        $("#a3-fin-config-username").html('');
        $("#a3-comexp-config-username").html('');
        $("#a3-l4-config-username").html('');
        $("#a3-cp-config-username").html('');
        $("#a3-fin-config-username").html('');
        $("#a3-comexp-config-username").html('');
    
    });



    $('#ImportPartsToExcelTemplateButton').click(function () {
        abp.ui.setBusy($("body"));
        _partsService.getPartsTemplate({
            supplierId: _$Supplier,
            buyerId: _$Buyer,
            templatePath: abp.appPath + '/assets/SampleFiles/PartsUploadTemplate.xlsx'
        })
            .done(function (result) {
                app.downloadTempFile(result);
            })
            .always(function () {
                abp.ui.clearBusy($("body"));
            });
    });


    $('#partBucketTemplateDownloadButton').click(function () {
        const numbers = [];

        _plant.split("|").forEach((element, index) => {
            if (index % 2 !== 0) {
                numbers.push(element);
            }
        });
        const concatenatedString = numbers.join(',');
        abp.ui.setBusy($("body"));
        _tenantDashboardService.getPartBucketToExcel({
            buyer: _$Buyer,
            supplier: _$Supplier,
            period: _$dateRange,
            buyerName: _buyer,
            //supplierName: _supplier && _supplier.split(' - ').length > 1 ? _supplier.split(' - ')[0] : null,
            supplierName: _supplier,
            isGenerateA3: false,
            group: _$Group,
            groupName: _group,
            plant: concatenatedString,
            plantName: _plant,
            templatePath: abp.appPath + 'assets/SampleFiles/PartBuckets.xlsx'
        })
            .done(function (result) {
                app.downloadTempFile(result);
            })
            .always(function () {
                abp.ui.clearBusy($("body"));
            });
    });
   

    $('#ImportPartsFromExcelButton').fileupload({
        url: abp.appPath + 'App/Parts/ImportPartMasterFromExcel',
        dataType: 'json',
        maxFileSize: 1048576 * 100,
        dropZone: $('body'),
        done: function (e, response) {
            var jsonResult = response.result;
            if (jsonResult.success) {
                abp.notify.info(app.localize('UpdatePartsMasterProcessStarted'));
            } else {
                abp.notify.warn(app.localize('UpdatePartsMasterProcessFailed'));
            }
        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');

 $('#PartBucketUploadButton').fileupload({
        url: abp.appPath + 'App/PartBuckets/ImportFromExcel',
        dataType: 'json',
        maxFileSize: 1048576 * 100,
        dropZone: $('body'),
        done: function (e, response) {
            var jsonResult = response.result;
            if (jsonResult.success) {
                abp.notify.info(app.localize('UpdatePartBucketsProcessStarted'));
            } else {
                abp.notify.warn(app.localize('UpdatePartBucketsProcessFailed'));
            }
        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');



    $('#ImportGlobusDataFromExcelButton').fileupload({
        url: abp.appPath + 'App/Parts/ImportGlobusDataFromExcel',
        dataType: 'json',
        maxFileSize: 1048576 * 100,
        dropZone: $("body"),
        done: function (e, response) {
            var jsonResult = response.result;
            if (jsonResult.success) {
                abp.notify.info(app.localize('UpdateGlobusDatasMasterProcessStarted'));
            } else {
                abp.notify.warn(app.localize('UpdateGlobusDatasMasterProcessFailed'));
            }
        }
    }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');

    _tenantDashboardService
        .getBuyerSettings()
        .done(function (result) {
            _$Buyer = result;
        });

    _tenantDashboardService
        .getSupplierSettings()
        .done(function (result) {
            _$Supplier = result;
        });


    abp.event.on('app.dashboardFilters.DateRangePicker.OnDateChange', function (_selectedDates) {
        _dtrange = _selectedDates.split('-')[0];
        _$dateRange = _selectedDates;
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnSupplierChange', function (_selectedSupplier) {
        _supplier = _selectedSupplier.supplierName;
        _$Supplier = _selectedSupplier.supplierId;
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnBuyerChange', function (_selectedBuyer) {
        _buyer = _selectedBuyer.buyerName;
        _$Buyer = _selectedBuyer.buyerId;
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnGradeChange', function (_selectedGrade) {
        _grade = _selectedGrade.gradeName;
        _$Grade = _selectedGrade.gradeId;
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnGroupChange', function (_selectedGroup) {
        _group = _selectedGroup.groupName;
        _$Group = _selectedGroup.groupId;
    });
    abp.event.on('app.dashboardFilters.DateRangePicker.OnPlantChange', function (_selectedPlant) {
        _plant = _selectedPlant.plantName;
        _$Plant = _selectedPlant.plantId;
    });
    initialize();
});