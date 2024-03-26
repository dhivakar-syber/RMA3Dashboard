(function () {
    $(function () {
        console.log('Approvaluser')
        var _$approvalUsersTable = $('#ApprovalUsersTable');
        var _approvalUsersService = abp.services.app.approvalUsers;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.ApprovalUsers.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.ApprovalUsers.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.ApprovalUsers.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/ApprovalUsers/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/ApprovalUsers/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditApprovalUserModal'
                });
                   

		 var _viewApprovalUserModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/ApprovalUsers/ViewapprovalUserModal',
            modalClass: 'ViewApprovalUserModal'
        });

		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }
        
        var getMaxDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT23:59:59Z"); 
        }

        var dataTable = _$approvalUsersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _approvalUsersService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ApprovalUsersTableFilter').val(),
					userNameFilter: $('#UserNameFilterId').val(),
					departmentFilter: $('#DepartmentFilterId').val(),
					emailFilter: $('#EmailFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    width: 120,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
						{
                                text: app.localize('View'),
                                iconStyle: 'far fa-eye mr-2',
                                action: function (data) {
                                    _viewApprovalUserModal.open({ id: data.record.approvalUser.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.approvalUser.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteApprovalUser(data.record.approvalUser);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "approvalUser.userName",
						 name: "userName"   
					},
					{
						targets: 3,
						 data: "approvalUser.department",
						 name: "department"   
					},
					{
						targets: 4,
						 data: "approvalUser.email",
						 name: "email"   
					}
            ]
        });

        function getApprovalUsers() {
            dataTable.ajax.reload();
        }

        function deleteApprovalUser(approvalUser) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _approvalUsersService.delete({
                            id: approvalUser.id
                        }).done(function () {
                            getApprovalUsers(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        $('#ImportApprovalUsersFromExcelButton').fileupload({
            url: abp.appPath + 'App/ApprovalUsers/ImportFromExcel',
            dataType: 'json',
            maxFileSize: 1048576 * 100,
            dropZone: $('#LeadModelsTable'),
            done: function (e, response) {
                var jsonResult = response.result;
                if (jsonResult.success) {
                    abp.notify.info(app.localize('UpdateApprovalUsersMasterProcessStarted'));
                } else {
                    abp.notify.warn(app.localize('UpdateApprovalUsersMasterProcessFailed'));
                }
            }
        }).prop('disabled', !$.support.fileInput)
            .parent().addClass($.support.fileInput ? undefined : 'disabled');



		$('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

        $('#CreateNewApprovalUserButton').click(function () {
            _createOrEditModal.open();
        });        

		

        abp.event.on('app.createOrEditApprovalUserModalSaved', function () {
            getApprovalUsers();
        });

		$('#GetApprovalUsersButton').click(function (e) {
            e.preventDefault();
            getApprovalUsers();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getApprovalUsers();
		  }
		});
		
		
		
    });
})();
