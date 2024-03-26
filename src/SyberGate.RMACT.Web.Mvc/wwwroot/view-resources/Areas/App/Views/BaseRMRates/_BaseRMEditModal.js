(function ($) {
    app.modals.BaseRMEditModal = function () {
        var _baseRMRatesService = abp.services.app.baseRMRates;
        var _tenantDashboardService = abp.services.app.tenantDashboard;
        var _indexNameService = abp.services.app.indexNames;
        var _rawMaterialIndexes = abp.services.app.rawMaterialIndexes;
        var _yearsService = abp.services.app.years;
        var _$baseRMEditTable = $('#BaseRMEditTable');
        var _$Container = $('.RMPriceTrendContainer');
        var _$dateRange = $('#DateRangeFilter').val();
        var _$Buyer = $("#BuyerNameFilterId").val();
        var _$Supplier = $('#SupplierNameFilterId').val();
        var _$Group = $('#GroupFilterId').val();
        var _modalManager;
        var _indexNames = "";
        var _years = "";
        var _yearsList = [];
       
        var _yearMonth = "";
                this.init = function (modalManager) {
            _modalManager = modalManager;
        };
        
        loadData = function () {
            if (_$dateRange && _$Buyer && _$Supplier) {
                abp.ui.setBusy(_$Container);
                _tenantDashboardService
                    .getRMPriceForEdit({
                        buyer: _$Buyer,
                        supplier: _$Supplier,
                        period: _$dateRange,
                        group: _$Group
                    })
                    .done(function (result) {


                        for (var i = 0; i < _$Container.length; i++) {
                            var container = $(_$Container[i]);
                            var $tableBody = container.find('#divBaseRMEditTable table tbody');
                            $tableBody.html('');
                            for (var rowIndex = 0; rowIndex < result.priceTrends.length; rowIndex++) {
                                var price = result.priceTrends[rowIndex];
                                var fromMY = price["revFromPeriod"] == "" ? "s-s" : price["revFromPeriod"];
                                var toMY = price["revToPeriod"] == "" ? "s-s" : price["revToPeriod"];
                                var datetimeinputid = "date-time-filter" + rowIndex;
                                // $('<td><div class=""><button data-rm-trendsetid="' + price["setId"] + '" data-rm-trendid="' + price["id"] + '" class= "btn btn-primary btn-sm btn-reviserm"><i class=" p-0 m-0 fa fa-cog"></i></button><button data-rm-groupname="' + price["rmGrade"] + '" data-rm-historyid="' + price["rmGroupId"] + '" class= "btn btn-primary btn-sm btn-historyrm"><i class=" p-0 m-0 fa fa-history"></i></button></div></td> '), 
                                var $tr = $('<tr></tr>').append(
                                    $('<td><span data-rm-mixturegrade="' + price["mixtureGrade"] + '" class="rmGroup">' + price["mixtureGrade"] + '</span></td>' ),
                                    $('<td style="left: 0px; position: sticky;" class="bg-success p-3 dtfc-fixed-left" ><span data-rm-gradename="' + price["rmGrade"] +'" data-rm-gradeid ="' + price["rmGradeId"] + '" class="rmGrade">' + price["rmGrade"] + '</span></td>'),
                                    $('<td><span data-uom-id= "' + price["uomId"] + '" class="uom">' + price["uom"] + '</span></td>'),
                                    $('<td style="text-align: center;" class= "' + (price["setApproved"] ? 'note' : '') + '">' + price["setteledMY"] + '</td>'),
                                    $('<td style="text-align: right;" class="bg-success" >' + price["setteledUR"] + '</td>'),
                                    $('<td style="text-align: right;" class="bg-success">' + price["scrapSetteled"] + '</td>'),
                                    $('<td style="text-align: right;" class="bg-success" >' + price["setteledWRatio"] + '</td>'),
                                    $('<td style="text-align: right;" class="bg-success" >' + price["setteledLRatio"] + '</td>'),
                                    /*price["revApproved"] ? $('<td class= "note">' + price["revisedMY"] + '</td>') : $('<td><select style="width: 90px;float: left;" data-rm-month="' + price["revisedMY"] + '" class=" month form-control m-input m-input--square"></select><select style="width: 90px;float:left" data-rm-year="' + price["revisedMY"] + '" class=" year form-control m-input m-input--square  w-20"></select><i class="repeatyear pt-3 pl-1 m-0 fa fa-redo"></i> </td>'),*/
                                    price["revApproved"] ? $('<td class= "note">' + price["revisedMY"] + '</td>') : ('<td><div style="width:170px" id="DateTimePicker" class="input-group">' +
                                    '<span class="input-group-append"><i  class="repeatdate pt-3 pl-1 m-0 fa fa-arrow-circle-down"></i></span> ' +
                                    '<input datePicker type="datetime" name="DateTimePicker" id=' + datetimeinputid + ' class="form-control rm-date-time-filter date-picker">' +
                                    '<span class="input-group-append">' +
                                    '<button class="btn btn-primary btn-datetime-picker" type="button" data-input-id = ' + datetimeinputid +' id="btnDateFilterOpen">' +
                                    '<i class="fa fa-calendar-day p-0" style="color:white;" aria-hidden="true"></i>' +
                                    '</button>' +
                                    '</span>' +
                                    '</div></td>'),
                                    $('<td class="bg-success"><input data-rm-trendsetid="' + price["setId"] + '" data-rm-trendid="' + price["id"] + '" class="input-revisedUR form-control m-input m-input--square" data-rm-revisedUr="' + price["revisedUR"] + '" value ="' + price["revisedUR"] + '"></input></td>'),
                                    $('<td class="bg-success"><input data-rm-trendsetid="' + price["setId"] + '" data-rm-trendid="' + price["id"] + '" class="input-scrapRevised form-control m-input m-input--square" data-rm-scrapRevised="' + price["scrapRevised"] + '"  value ="' + price["scrapRevised"] + '"></input></td>'),
                                    $('<td class="bg-success"><input data-rm-trendsetid="' + price["setId"] + '" data-rm-trendid="' + price["id"] + '" class="input-wrRevised form-control m-input m-input--square" data-rm-wrRevised="' + price["revisedWRatio"] + '" value ="' + price["revisedWRatio"] + '"></input></td>'),
                                    $('<td class="bg-success"><input data-rm-trendsetid="' + price["setId"] + '" data-rm-trendid="' + price["id"] + '" class="input-lrRevised form-control m-input m-input--square" data-rm-lrRevised="' + price["revisedLRatio"] + '" value ="' + price["revisedLRatio"] + '"></input></td>'),
                                    $('<td><select style="width: 100px; float: left;" data-rm-indexname="' + price["revIndexName"] + '" class="indexname form-control m-input m-input--square"></select></td>'),
                                    $('<td><select style="width: 150px; float: left;" data-rm-fromMonthYear="' + fromMY + '" class="fromMY form-control m-input m-input--square"></select></td>'),
                                    $('<td><select style="width: 150px; float: left;" data-rm-toMonthYear="' + toMY + '" class="toMY form-control m-input m-input--square"></select></td>'),
                                    $('<td><input readonly=readonly data-rm-myval="' + price["revIndexValue"] + '" class="input-myval form-control m-input m-input--square" type="number" value="' + price["revIndexValue"] + '" ></input></td>')
                                );
                                $tableBody.append($tr);
                                loadMonthYear($tr);
                                loadIndexNames($tr);
                                loadMonthPlusYear($tr);
                            }
                            $(".repeatyear").click(function (e) {
                                console.log(e);
                                var year = $(this).parent().find('.year').val();
                                var month = $(this).parent().find('.month').val();

                                $(this).parent().parent().nextAll().find('.year').val(year);
                                $(this).parent().parent().nextAll().find('.month').val(month);
                            });
                            $(".repeatdate").click(function (e) {
                                console.log(e);
                                var dt = $(this).parent().parent().find('.rm-date-time-filter').val();

                                $(this).parent().parent().parent().parent().nextAll().find('.rm-date-time-filter').val(dt);
                            });

                            $('.btn-datetime-picker').on('click', function () {
                                var dtinputid = $(this).data('input-id');
                                var _$datePickerInput = $('#' + dtinputid);

                                var _datePickerInput = _$datePickerInput.datetimepicker({
                                    locale: abp.localization.currentLanguage.name,
                                    format: 'L'
                                });

                                _datePickerInput.data('DateTimePicker').toggle();
                            });

                            $(".input-revisedUR").blur(function (e) {
                                console.log(e);
                                var bevalue = parseFloat($(this).attr('data-rm-revisedUR'));
                                var aevalue = parseFloat($(this).val());
                                var id = $(this).attr('data-rm-trendid');

                                if (bevalue != aevalue) {
                                    $(this).addClass('bg-secondary')
                                }
                            });
                            $(".input-scrapRevised").blur(function (e) {
                                console.log(e);
                                var bevalue = parseFloat($(this).attr('data-rm-scrapRevised'));
                                var aevalue = parseFloat($(this).val());

                                if (bevalue != aevalue) {
                                    $(this).addClass('bg-secondary')
                                }
                            });

                            $(".btn-reviserm").click(function (e) {
                                console.log(e);
                                var id = $(this).attr('data-rm-trendid');
                                _createOrEditModal.open({ id: id, isrevision: true })
                            });
                            $(".btn-historyrm").click(function (e) {
                                console.log(e);
                                var id = $(this).attr('data-rm-historyid');
                                var rm = $(this).attr('data-rm-groupname');
                                _viewHistoryModal.open({ rmid: id, rm: rm, buyerid: _$Buyer, buyer: _buyer.text(), supplierid: _$Supplier, supplier: _supplier.text() })
                            });

                            $('.fromMY').change(function (e) {
                                calcIndexValue(this);
                            })

                            $('.toMY').change(function (e) {
                                calcIndexValue(this);
                            })

                            $('.indexname').change(function (e) {
                                calcIndexValue(this);
                            })
                        }
                    })
                    .always(function () {
                        abp.ui.clearBusy(_$Container);
                    });
            }
        }

        var getYears = function (callback) {
            _yearsService.getAll({}).done(function (result) {
                _years = '<option value=0>select</option>';
                for (var i = 0; i < result.items.length; i++) {
                    var y = result.items[i].year;
                    _yearsList.push({ id: y.id, year: y.name });
                    _years += '<option value=' + y.id + '>' + y.name + '</option>'
                }
                callback(getYearMonth);
                
            });
        }

        var calcIndexValue = function (elem) {
            var iname = $(elem).closest('tr').find('.indexname option:selected').text();
            var from = $(elem).closest('tr').find('.fromMY').val();
            var to = $(elem).closest('tr').find('.toMY').val();
            var rmgrade = $(elem).closest('tr').find('.toMY').attr("data-rm-gradename");

            if ((iname != 0) && (from != "s-s") && (to != "s-s")) {
                var that = elem;
                _rawMaterialIndexes.getAverageIndexValue({
                    fromFilter: from,
                    toFilter: to,
                    indexNameNameFilter: iname,
                    RawMaterialGradeNameFilter: rmgrade
                }).done(function (result) {
                    var rval = parseFloat(result).toFixed(2);
                    $(that).closest('tr').find('.input-myval').val(rval);
                });
            }
        }

        var getIndexNames = function (callback) {
            _indexNameService.getAll({}).done(function (result) {
                _indexNames = '<option value=0>Select</option>';
                for (var i = 0; i < result.items.length; i++) {
                    var y = result.items[i].indexName;
                    _indexNames += '<option value=' + y.id + '>' + y.name + '</option>'
                }
                callback(loadData);
            });
        }

        var getYearMonth = function (callback) {
            console.log('Get Year Month');
            _yearMonth = '<option value = s-s>Select</option>';
            for (var i = 0; i < _yearsList.length; i++) {
                var y = _yearsList[i];
                for (var j = 0; j < 12; j++)
                    _yearMonth += '<option value ='+ j +'-' + y.id + '>' + getMonth(j) + '-' + y.year + '</option>';
            }
            if (callback)
                callback();
        }

        var getMonth = function (monthid) {
            switch(monthid){
                case 0:
                    return 'Jan';
                case 1:
                    return 'Feb';
                case 2:
                    return 'Mar';
                case 3:
                    return 'Apr';
                case 4:
                    return 'May';
                case 5:
                    return 'Jun';
                case 6:
                    return 'Jul';
                case 7:
                    return 'Aug';
                case 8:
                    return 'Sep';
                case 9:
                    return 'Oct';
                case 10:
                    return 'Nov';
                case 11:
                    return 'Dec';
                default:
                    return 'Select';
            }
        }

        var loadIndexNames = function (row) {
            var indexNameCtl = row.find('select.indexname');
            var ctlval = indexNameCtl.attr('data-rm-indexname');

            $(indexNameCtl).empty().append(_indexNames);

            if (ctlval) {
                for (var row = 0; row < indexNameCtl.children().length; row++) {
                    var opt = indexNameCtl.children()[row];
                    if (opt.text.toUpperCase() == ctlval.toUpperCase())
                        $(opt).attr("selected", true)
                }
            }
        }

        var loadMonthYear = function(row) {
            var month = row.find('select.month');
            var year = row.find('select.year');

            var selMonth = month.attr('data-rm-month') && month.attr('data-rm-month').split("'")[0]
            var selYear = month.attr('data-rm-month') && month.attr('data-rm-month').split("'")[1]

            //$(month).empty().append('<option value="s">select</option><option value=0>Jan</option><option value=1>Feb</option><option value=2>Mar</option><option value=3>Apr</option><option value=4>May</option><option value=5>Jun</option><option value=6>Jul</option><option value=7>Aug</option><option value=8>Sep</option><option value=9>Oct</option><option value=10>Nov</option><option value=11>Dec</option>');
            
            //$(year).empty().append(_years);

            if (selMonth) {
                for (var row = 0; row < month.children().length; row++) {
                    var opt = month.children()[row];
                    if (opt.text.toUpperCase() == selMonth.toUpperCase())
                        $(opt).attr("selected", true)
                }
            }
            if (selYear)
                for (var row = 0; row < year.children().length; row++) {
                    var opt = year.children()[row];
                    if (opt.text.toUpperCase() == selYear.toUpperCase())
                        $(opt).attr("selected", true)
                }
        }

        var loadMonthPlusYear = function (row) {
            var ctrlTo = row.find('select.toMY');
            var ctrlFrom = row.find('select.fromMY');
            var ctrlToVal = (ctrlTo.attr('data-rm-toMonthYear') === 'undefined' ? 's-s' : ctrlTo.attr('data-rm-toMonthYear'));
            var ctrlFromVal = (ctrlFrom.attr('data-rm-fromMonthYear') == 'undefined' ? 's-s' : ctrlFrom.attr('data-rm-fromMonthYear'));

            $(ctrlTo).empty().append(_yearMonth);
            $(ctrlFrom).empty().append(_yearMonth);

            if (ctrlToVal)
                ctrlTo.val(ctrlToVal);

            if (ctrlFromVal)
                ctrlFrom.val(ctrlFromVal);
        }


        $('.save-button').click(function () {
            var rows = $('#divBaseRMEditTable').find('tr');
            for (var row = 1; row < rows.length; row++) {
                saverow(rows[row]);
            }
            _modalManager.close();
        });

        var saverow = function (row) {
            row = $(row);
            if (row.find('input.input-scrapRevised')) {
                var rscrap = row.find('input.input-scrapRevised').val();
                var rmGrade = row.find('span.rmGrade').attr('data-rm-gradeid');
                var uom = row.find('span.uom').attr('data-uom-id');
                var month = row.find('select.month').val();
                var year = row.find('select.year').val();

                var id = row.find('input.input-revisedUR').attr('data-rm-trendid');
                var value = row.find('input.input-revisedUR').val();
                var scrapPer = parseFloat(rscrap) * parseFloat(value) * 0.01;

                var indexName = row.find('.indexname option:selected').text();
                var fromPeriod = row.find('.fromMY').val();
                var toPeriod = row.find('.toMY').val();
                var indexValue = row.find('.input-myval').val();
                var wRatio = row.find('input.input-wrRevised').val();
                var lRatio = row.find('input.input-lrRevised').val();
                var SettledDate = row.find('td div#DateTimePicker input.date-picker').val();
                const date = new Date(SettledDate); // create a new Date object with the date you provided
                const formattedDate = `${date.getDate().toString().padStart(2, "0")}/${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getFullYear()}`; // format the date as "dd/MM/yyyy"



                indexName = indexName == "Select" ? "" : indexName;

                if (month == "s")
                    return;

                if (year == "0")
                    return;

                if (parseFloat(value) == 0)
                    return;

                var baseRMRate = {
                    id: id == 0 ? null : id,
                    unitRate: value,
                    scrapPercent: scrapPer,
                    scrapAmount: rscrap,
                    month: month,
                    yearId: year,
                    rmGroupId: rmGrade,
                    supplierId: _$Supplier,
                    buyerId: _$Buyer,
                    unitOfMeasurementId: uom,
                    indexName: indexName,
                    fromPeriod: fromPeriod,
                    toPeriod: toPeriod,
                    indexValue: indexValue,
                    weightRatio: wRatio,
                    LossRatio: lRatio,
                    SettledDate: formattedDate

                };

                abp.ui.setBusy(_$Container);
                _baseRMRatesService.createOrEdit(
                    baseRMRate
                ).done(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));
                    abp.event.trigger('app.createOrEditBaseRMRateModalSaved');
                }).always(function () {
                    abp.ui.clearBusy(_$Container);
                });
            }
        }
        getYears(getIndexNames);
    }
})(jQuery);