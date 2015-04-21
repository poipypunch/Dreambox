app.controller("ChannelController", function ($scope, channelService, $filter, ngTableParams) {
    $scope.divChannelModification = false;
    $scope.divGrid = true;
    GetAll();
    //To Get All Records
    function GetAll() {
        var Data = channelService.getChan();
        Data.then(function (chan) {
            $scope.itemsByPage = 10;
            $scope.rowCollection = chan.data;
            $scope.displayedCollection = [].concat($scope.rowCollection);
        }, function () {
            alert('get data error');
        });
    }
    $scope.edit = function (channel) {
        $scope.ChannelID = channel.ChannelID;
        $scope.ChannelDesc = channel.ChannelDesc;
        $scope.ChannelPath = channel.ChannelPath;
        $scope.ChannelStatus = channel.ChannelStatus;
        $scope.CreateBy = channel.CreateBy;
        $scope.UpdateBy = channel.UpdateBy;
        $scope.Operation = "Update";
        $scope.divChannelModification = true;
        $scope.divGrid = false;
    }

    $scope.Cancel = function () {
        $scope.$broadcast('show-errors-reset');
        $scope.ChannelID = "";
        $scope.ChannelDesc = "";
        $scope.ChannelPath = "";
        $scope.ChannelStatus = "";
        $scope.CreateBy = "";
        $scope.UpdateBy = "";
        $scope.divChannelModification = false;
        $scope.divGrid = true;
    }

    $scope.add = function () {
        $scope.ChannelID = "";
        $scope.ChannelDesc = "";
        $scope.ChannelPath = "";
        $scope.ChannelStatus = "Active";
        $scope.CreateBy = "";
        $scope.UpdateBy = "";
        $scope.Operation = "Add";
        $scope.divChannelModification = true;
        $scope.divGrid = false;
    }

    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var Channel = {
                ChannelDesc: $scope.ChannelDesc,
                ChannelPath: $scope.ChannelPath,
                ChannelStatus: $scope.ChannelStatus,
                CreateBy: $scope.CreateBy,
                UpdateBy: $scope.UpdateBy
            };
            var Operation = $scope.Operation;

            if (Operation == "Update") {
                Channel.ChannelID = $scope.ChannelID;
                var getMSG = channelService.update(Channel);
                getMSG.then(function (messagefromController) {
                    if (messagefromController.data == "Success") {
                        GetAll();
                        $scope.divChannelModification = false;
                        $scope.divGrid = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Update channel failed.";
                });
            }
            else {
                var getMSG = channelService.Add(Channel);
                getMSG.then(function (messagefromController) {
                    if (messagefromController.data == "Success") {
                        GetAll();
                        $scope.divChannelModification = false;
                        $scope.divGrid = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Add channel failed.";
                });
            }
        }
    }

    $scope.delete = function (channel) {
        if (confirm('Please confirm to delete.')) {
            var getMSG = channelService.Delete(channel.ChannelID);
            getMSG.then(function (messagefromController) {
                GetAll();
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete channel failed.";
            });
        }
    }
});
app.controller("MemberController", function ($scope, memberService) {
    GetMemberTypes();
    GetAllMember();
    $scope.divGrid = true;
    //To Get All Records  
    function GetAllMember() {
        var Data = memberService.getMember();
        Data.then(function (mem) {
            //$scope.members = mem.data;
            $scope.itemsByPage = 10;
            $scope.rowCollection = mem.data;
            $scope.displayedCollection = [].concat($scope.rowCollection);
        }, function () {
            alert('Error');
        });
    }

    //To Get All Records  
    function GetMemberTypes() {
        var Data = memberService.getMemberType();
        Data.then(function (memtype) {
            $scope.membertypes = memtype.data;
        }, function () {
            alert('Error');
        });
    }

    $scope.SavePermission = function () {
        var Permission = {
            ID: $scope.selectedItem.ID,
            RoleID: $scope.selectedItemRole.ID
        };

        var getMSG = memberService.update(Permission);
        getMSG.then(function (messagefromController) {
            GetMemberTypes();
            GetAllRoles();
            GetAllMember();
            alert(messagefromController.data);
        }, function () {
            alert('Save Permission Error');
        });


    }

    $scope.edit = function (member) {
        $scope.MemberID = member.MemberID;
        $scope.UserName = member.UserName;
        $scope.Password = member.Password;
        $scope.MemberName = member.MemberName;
        $scope.Address = member.Address;
        $scope.Email = member.Email;
        $scope.Phone = member.Phone;
        $scope.MemberTypeID = member.MemberTypeID;
        $scope.DealerID = member.DealerID;
        $scope.CreateDate = member.CreateDate;
        $scope.UpdateDate = member.UpdateDate;
        $scope.CreateBy = member.CreateBy;
        $scope.UpdateBy = member.UpdateBy;
        $scope.Operation = "Update";
        $scope.divMemberModification = true;
        $scope.showRepassword = false;
        $scope.divGrid = false;
    }

    $scope.Cancel = function () {       
        $scope.divMemberModification = false;
        $scope.showRepassword = false;
        $scope.divGrid = true;
    }

    $scope.add = function () {
        $scope.MemberID = "";
        $scope.UserName = "";
        $scope.Password = "";
        $scope.RePassword = "";
        $scope.MemberName = "";
        $scope.Address = "";
        $scope.Email = "";
        $scope.Phone = "";
        $scope.MemberTypeID = "";
        $scope.DealerID = "";
        $scope.CreateDate = "";
        $scope.UpdateDate = "";
        $scope.CreateBy = "";
        $scope.UpdateBy = "";
        $scope.Operation = "Add";
        $scope.showRepassword = true;
        $scope.divMemberModification = true;
        $scope.divGrid = false;
    }

    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var Member = {
                MemberID: $scope.MemberID,
                UserName: $scope.UserName,
                MemberName: $scope.MemberName,
                Password: $scope.Password,
                RePassword: $scope.RePassword,
                Address: $scope.Address,
                Email: $scope.Email,
                Phone: $scope.Phone,
                MemberTypeID: $scope.MemberTypeID,
                DealerID: $scope.DealerID,
                CreateBy: $scope.CreateBy,
                UpdateBy: $scope.UpdateBy,
                CreateDate: $scope.CreateDate,
                UpdateDate: $scope.UpdateDate
            };
            var Operation = $scope.Operation;

            if (Operation == "Update") {
                Member.MemberID = $scope.MemberID;
                var getMSG = memberService.update(Member);
                getMSG.then(function (messagefromController) {
                    GetMemberTypes();
                    GetAllMember();
                    if (messagefromController.data == "Success") {
                        alert(messagefromController.data);
                        $scope.divMemberModification = false;
                        $scope.divGrid = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Update member failed.";
                });
            }
            else {
                var getMSG = memberService.Add(Member);
                if (Member.Password == Member.RePassword) {
                    getMSG.then(function (messagefromController) {
                        GetMemberTypes();
                        GetAllMember();
                        if (messagefromController.data == "Success") {
                            alert(messagefromController.data);
                            $scope.divMemberModification = false;
                            $scope.divGrid = true;
                        } else {
                            $scope.errormessage = messagefromController.data;
                        }
                    }, function () {
                        $scope.errormessage = "Add member failed.";
                    });
                } else {
                    $scope.errormessage = "Confirm password mismatch.";
                }
            }
        }
    }

    $scope.delete = function (member) {
        if (confirm('Please confirm to delete.')) {
            var getMSG = memberService.Delete(member.MemberID);
            getMSG.then(function (messagefromController) {
                GetMemberTypes();
                GetAllMember();
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete member failed.";
            });
        }
    }
});

app.directive('csSelect', function () {
    return {
        require: '^stTable',
        template: '<input type="checkbox"/>',
        scope: {
            row: '=csSelect'
        },
        link: function (scope, element, attr, ctrl) {

            element.bind('change', function (evt) {
                scope.$apply(function () {
                    ctrl.select(scope.row, 'multiple');
                });
            });

            scope.$watch('row.isSelected', function (newValue, oldValue) {
                if (newValue === true) {
                    element.parent().addClass('st-selected');
                } else {
                    element.parent().removeClass('st-selected');
                }
            });
        }
    };
});
app.directive('showErrors', function ($timeout, showErrorsConfig) {
    var getShowSuccess, linkFn;
    getShowSuccess = function (options) {
        var showSuccess;
        showSuccess = showErrorsConfig.showSuccess;
        if (options && options.showSuccess != null) {
            showSuccess = options.showSuccess;
        }
        return showSuccess;
    };
    linkFn = function (scope, el, attrs, formCtrl) {
        var blurred, inputEl, inputName, inputNgEl, options, showSuccess, toggleClasses;
        blurred = false;
        options = scope.$eval(attrs.showErrors);
        showSuccess = getShowSuccess(options);
        inputEl = el[0].querySelector('[name]');
        inputNgEl = angular.element(inputEl);
        inputName = inputNgEl.attr('name');
        if (!inputName) {
            throw 'show-errors element has no child input elements with a \'name\' attribute';
        }
        inputNgEl.bind('blur', function () {
            blurred = true;
            return toggleClasses(formCtrl[inputName].$invalid);
        });
        scope.$watch(function () {
            return formCtrl[inputName] && formCtrl[inputName].$invalid;
        }, function (invalid) {
            if (!blurred) {
                return;
            }
            return toggleClasses(invalid);
        });
        scope.$on('show-errors-check-validity', function () {
            return toggleClasses(formCtrl[inputName].$invalid);
        });
        scope.$on('show-errors-reset', function () {
            return $timeout(function () {
                el.removeClass('has-error');
                el.removeClass('has-success');
                return blurred = false;
            }, 0, false);
        });
        return toggleClasses = function (invalid) {
            el.toggleClass('has-error', invalid);
            if (showSuccess) {
                return el.toggleClass('has-success', !invalid);
            }
        };
    };
    return {
        restrict: 'A',
        require: '^form',
        compile: function (elem, attrs) {
            if (!elem.hasClass('form-group')) {
                throw 'show-errors element does not have the \'form-group\' class';
            }
            return linkFn;
        }
    };
}
  );

app.provider('showErrorsConfig', function () {
    var _showSuccess;
    _showSuccess = false;
    this.showSuccess = function (showSuccess) {
        return _showSuccess = showSuccess;
    };
    this.$get = function () {
        return { showSuccess: _showSuccess };
    };
});

