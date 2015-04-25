//------------------------------- UserController ----------------------------------------------------//
app.controller("UserController", function ($scope, userService, $filter) {
    $scope.divModification = false;
    $scope.divGrid = true;
    GetUsersList();
    //To Get All Records
    function GetUsersList() {
        var Data = userService.getUsers();
        Data.then(function (tbuser) {
            $scope.itemsByPage = 10;
            $scope.rowCollection = tbuser.data;
            $scope.displayedCollection = [].concat($scope.rowCollection);
        }, function () {
            alert('get data error');
        });
    }
    $scope.edit = function (tbuser) {
        $scope.errormessage = "";
        $scope.DealerID = tbuser.DealerID;
        $scope.UserName = tbuser.UserName;
        $scope.Password = tbuser.Password;
        $scope.RealName = tbuser.RealName;
        $scope.Email = tbuser.Email;
        $scope.Phone = tbuser.Phone;
        $scope.Address = tbuser.Address;
        $scope.Role = tbuser.Role;
        $scope.Status = tbuser.Status;
        $scope.Operation = "Update";
        $scope.divModification = true;
        $scope.showRepassword = false;
        $scope.divGrid = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.showRepassword = false;
        $scope.divGrid = true;
    }

    $scope.add = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.DealerID = "";
        $scope.UserName = "";
        $scope.Password = "";
        $scope.RealName = "";
        $scope.Email = "";
        $scope.Phone = "";
        $scope.Address = "";
        $scope.Role = "Dealer";
        $scope.Status = "Active";
        $scope.Operation = "New";
        $scope.divModification = true;
        $scope.showRepassword = true;
        $scope.divGrid = false;
    }

    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var tbuser = {
                DealerID: $scope.DealerID,
                UserName: $scope.UserName,
                Password: $scope.Password,
                RealName: $scope.RealName,
                Email: $scope.Email,
                Phone: $scope.Phone,
                Address: $scope.Address,
                Role: $scope.Role,
                Status: $scope.Status
            };
            var Operation = $scope.Operation;

            if (Operation == "Update") {
                tbuser.DealerID = $scope.DealerID;
                var getMSG = userService.update(tbuser);
                getMSG.then(function (messagefromController) {
                    if (messagefromController.data == "Success") {
                        GetUsersList();
                        $scope.divModification = false;
                        $scope.divGrid = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Update user failed.";
                });
            }
            else {
                var getMSG = userService.Add(tbuser);
                getMSG.then(function (messagefromController) {
                    if (messagefromController.data == "Success") {
                        GetUsersList();
                        $scope.divModification = false;
                        $scope.divGrid = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Add user failed.";
                });
            }
        }
    }

    $scope.delete = function (tbuser) {
        if (confirm('Please confirm to delete.')) {
            var getMSG = userService.Delete(tbuser.DealerID);
            getMSG.then(function (messagefromController) {
                GetUsersList();
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete user failed.";
            });
        }
    }
});

//----------------------- Package ------------------------------------------------//
app.controller("PackageController", function ($scope, packageService, $filter) {
    $scope.divModification = false;
    $scope.divGrid = true;
    GetPackagesList();
    //To Get All Records
    function GetPackagesList() {
        var Data = packageService.getPackages();
        Data.then(function (packages) {
            $scope.itemsByPage = 10;
            $scope.rowCollection = packages.data;
            $scope.displayedCollection = [].concat($scope.rowCollection);
        }, function () {
            alert('get data error');
        });
    }
    $scope.edit = function (Package) {
        $scope.errormessage = "";
        $scope.PackageID = Package.PackageID;
        $scope.PackageDesc = Package.PackageDesc;
        $scope.PackageStatus = Package.PackageStatus;
        $scope.Operation = "Update";
        $scope.divModification = true;
        $scope.divGrid = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.divGrid = true;
    }

    $scope.add = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.PackageID = "";
        $scope.PackageDesc = "";
        $scope.PackageStatus = "Active";
        $scope.Operation = "New";
        $scope.divModification = true;
        $scope.divGrid = false;
    }

    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var Package = {
                PackageID: $scope.PackageID,
                PackageDesc: $scope.PackageDesc,
                PackageStatus: $scope.PackageStatus
            };
            var Operation = $scope.Operation;

            if (Operation == "Update") {
                Package.PackageID = $scope.PackageID;
                var getMSG = packageService.update(Package);
                getMSG.then(function (messagefromController) {
                    if (messagefromController.data == "Success") {
                        GetPackagesList();
                        $scope.divModification = false;
                        $scope.divGrid = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Update package failed.";
                });
            }
            else {
                var getMSG = packageService.Add(Package);
                getMSG.then(function (messagefromController) {
                    if (messagefromController.data == "Success") {
                        GetPackagesList();
                        $scope.divModification = false;
                        $scope.divGrid = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Add package failed.";
                });
            }
        }
    }

    $scope.delete = function (Package) {
        if (confirm('Please confirm to delete.')) {
            var getMSG = packageService.Delete(Package.PackageID);
            getMSG.then(function (messagefromController) {
                GetPackagesList();
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete package failed.";
            });
        }
    }
});

//----------------------- ChannelController -----------------------------------------//
app.controller("ChannelController", function ($scope, channelService, $filter) {
    $scope.divModification = false;
    $scope.divGrid = true;
    GetChannelList();
    //To Get All Records
    function GetChannelList() {
        var Data = channelService.getChannels();
        Data.then(function (chan) {
            $scope.itemsByPage = 10;
            $scope.rowCollection = chan.data;
            $scope.displayedCollection = [].concat($scope.rowCollection);
        }, function () {
            alert('get data error');
        });
    }
    $scope.edit = function (channel) {
        $scope.errormessage = "";
        $scope.ChannelID = channel.ChannelID;
        $scope.ChannelDesc = channel.ChannelDesc;
        $scope.ChannelPath = channel.ChannelPath;
        $scope.ChannelStatus = channel.ChannelStatus;
        $scope.Operation = "Update";
        $scope.divModification = true;
        $scope.divGrid = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.divGrid = true;
    }

    $scope.add = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.ChannelID = "";
        $scope.ChannelDesc = "";
        $scope.ChannelPath = "";
        $scope.ChannelStatus = "Active";
        $scope.Operation = "New";
        $scope.divModification = true;
        $scope.divGrid = false;
    }

    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var Channel = {
                ChannelDesc: $scope.ChannelDesc,
                ChannelPath: $scope.ChannelPath,
                ChannelStatus: $scope.ChannelStatus
            };
            var Operation = $scope.Operation;

            if (Operation == "Update") {
                Channel.ChannelID = $scope.ChannelID;
                var getMSG = channelService.update(Channel);
                getMSG.then(function (messagefromController) {
                    if (messagefromController.data == "Success") {
                        GetChannelList();
                        $scope.divModification = false;
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
                        GetChannelList();
                        $scope.divModification = false;
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
                GetChannelList();
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete channel failed.";
            });
        }
    }
});

//---------------------- PackageMappingController -------------------------------//
app.controller("PackageMappingController", function ($scope, packagemapService, $filter) {
    $scope.divMappingChannel = false;
    $scope.divActiveChannel = false;
    $scope.SelectItemChans = [];
    GetActivePackageList();
    //getMappingChannelsList();
    //getActiveChannelsList();
    //To Get All Records
    function GetActivePackageList() {
        var Data = packagemapService.getActivePackagesList();
        Data.then(function (pack) {
            $scope.itemsByPage = 10;
            $scope.rowCollection = pack.data;
            $scope.displayedCollection = [].concat($scope.rowCollection);
        }, function () {
            alert('get data error');
        });
    }
    function GetMappingChannelsList(PackageID) {
        var Data = packagemapService.getMappingChannelsList(PackageID);
        Data.then(function (pack) {
            $scope.itemsmapByPage = 10;
            $scope.mapCollection = pack.data;
            $scope.displayedmapCollection = [].concat($scope.mapCollection);
        }, function () {
            alert('get data error');
        });
    }
    function GetActiveChannelsList(PackageID) {
        var Data = packagemapService.getActiveChannelsList(PackageID);
        Data.then(function (pack) {
            $scope.itemschanByPage = 10;
            $scope.chanCollection = pack.data;
            $scope.displayedchanCollection = [].concat($scope.chanCollection);
        }, function () {
            alert('get data error');
        });
    }
    $scope.selectRow = function (pack) {
        $scope.divMappingChannel = true;
        $scope.SelectItemChans = [];
        $scope.divActiveChannel = false;
        $scope.SelectedPackageID = pack.PackageID;
        GetMappingChannelsList(pack.PackageID);
    }
    $scope.addChannel = function () {
        $scope.SelectItemChans = [];
        $scope.divActiveChannel = true;
        GetActiveChannelsList($scope.SelectedPackageID);
    }
    $scope.cancel = function () {
        $scope.SelectItemChans = [];
        $scope.divActiveChannel = false;
    }
    $scope.addSelect = function (chan) {
        if ($scope.SelectItemChans.indexOf(chan.ChannelID) != -1) return;
        $scope.SelectItemChans.push(chan.ChannelID);
    }

    $scope.add = function () {
        if ($scope.SelectItemChans == null || $scope.SelectItemChans.length <= 0) {
            $scope.errormessage = "Please select channel to map.";
        } else {
            var getMSG = packagemapService.Add($scope.SelectedPackageID, $scope.SelectItemChans);
            getMSG.then(function (messagefromController) {
                if (messagefromController.data == "Success") {
                    GetMappingChannelsList($scope.SelectedPackageID);
                    $scope.SelectItemChans = [];
                    $scope.divActiveChannel = false;
                } else {
                    alert(messagefromController.data);
                }
            }, function () {
                $scope.errormessage = "Mapping package failed.";
            });
        }
    }
    $scope.delete = function (pack) {
        if (confirm('Please confirm to delete.')) {
            var getMSG = packagemapService.Delete(pack.PackageID, pack.ChannelID);
            getMSG.then(function (messagefromController) {
                //GetChannelList();
                GetMappingChannelsList(pack.PackageID);
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete package mapping failed.";
            });
        }
    }
});
//---------------------- MemberTypeController -----------------------------------//
app.controller("MemberTypeController", function ($scope, memberTypeService) {
    $scope.divModification = false;
    $scope.divGrid = true;
    GetMemberTypesList();
    //To Get All Records  
    function GetMemberTypesList() {
        var Data = memberTypeService.GetMemberTypes();
        Data.then(function (mem) {
            $scope.itemsByPage = 10;
            $scope.rowCollection = mem.data;
            $scope.displayedCollection = [].concat($scope.rowCollection);
        }, function () {
            alert('Error');
        });
    }

    $scope.edit = function (memtype) {
        $scope.errormessage = "";
        $scope.MemberTypeID = memtype.MemberTypeID;
        $scope.MemberTypeDesc = memtype.MemberTypeDesc;
        $scope.Operation = "Update";
        $scope.divModification = true;
        $scope.divGrid = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.divGrid = true;
    }

    $scope.add = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.MemberTypeID = "";
        $scope.MemberTypeDesc = "";
        $scope.Operation = "New";
        $scope.divModification = true;
        $scope.divGrid = false;
    }

    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var memtype = {
                MemberTypeID: $scope.MemberTypeID,
                MemberTypeDesc: $scope.MemberTypeDesc
            };
            var Operation = $scope.Operation;

            if (Operation == "Update") {
                memtype.MemberTypeID = $scope.MemberTypeID;
                var getMSG = memberTypeService.update(memtype);
                getMSG.then(function (messagefromController) {
                    GetMemberTypesList();
                    if (messagefromController.data == "Success") {
                        alert(messagefromController.data);
                        $scope.divModification = false;
                        $scope.divGrid = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Update member type failed.";
                });
            }
            else {
                var getMSG = memberTypeService.Add(memtype);
                getMSG.then(function (messagefromController) {
                    GetMemberTypesList();
                    if (messagefromController.data == "Success") {
                        alert(messagefromController.data);
                        $scope.divModification = false;
                        $scope.divGrid = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Add member type failed.";
                });
            }
        }
    }

    $scope.delete = function (memtype) {
        if (confirm('Please confirm to delete.')) {
            var getMSG = memberTypeService.Delete(memtype.MemberTypeID);
            getMSG.then(function (messagefromController) {
                GetMemberTypesList();
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete member type failed.";
            });
        }
    }
});

//-------------------------------- MemberController ---------------------------------------//
app.controller("MemberController", function ($scope, memberService) {
    $scope.divModification = false;
    $scope.divGrid = true;
    GetMemberTypeList();
    GetMemberList();
    //To Get All Records  
    function GetMemberList() {
        var Data = memberService.getMembers();
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
    function GetMemberTypeList() {
        var Data = memberService.getMemberTypes();
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
        $scope.errormessage = "";
        $scope.MemberID = member.MemberID;
        $scope.UserName = member.UserName;
        $scope.Password = member.Password;
        $scope.MemberName = member.MemberName;
        $scope.Address = member.Address;
        $scope.Email = member.Email;
        $scope.Phone = member.Phone;
        $scope.MemberTypeID = member.MemberTypeID;
        $scope.Operation = "Update";
        $scope.divModification = true;
        $scope.showRepassword = false;
        $scope.divGrid = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.showRepassword = false;
        $scope.divGrid = true;
    }

    $scope.add = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.MemberID = "";
        $scope.UserName = "";
        $scope.Password = "";
        $scope.RePassword = "";
        $scope.MemberName = "";
        $scope.Address = "";
        $scope.Email = "";
        $scope.Phone = "";
        $scope.MemberTypeID = "";
        $scope.Operation = "New";
        $scope.showRepassword = true;
        $scope.divModification = true;
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
                MemberTypeID: $scope.MemberTypeID
            };
            var Operation = $scope.Operation;

            if (Operation == "Update") {
                Member.MemberID = $scope.MemberID;
                var getMSG = memberService.update(Member);
                getMSG.then(function (messagefromController) {
                    GetMemberTypeList();
                    GetMemberList();
                    if (messagefromController.data == "Success") {
                        alert(messagefromController.data);
                        $scope.divModification = false;
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
                        GetMemberTypeList();
                        GetMemberList();
                        if (messagefromController.data == "Success") {
                            alert(messagefromController.data);
                            $scope.divModification = false;
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
                GetMemberTypeList();
                GetMemberList();
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete member failed.";
            });
        }
    }
});
//------------------------------- UserController ----------------------------------------------------//
app.controller("PaymentController", function ($scope, paymentService, $filter) {
    $scope.divGrid = true;
    $scope.divModification = false;
    GetPaymentsList();
    //To Get All Records
    function GetPaymentsList() {
        var Data = paymentService.getPayments();
        Data.then(function (payment) {
            $scope.itemsByPage = 10;
            $scope.rowCollection = payment.data;
            $scope.displayedCollection = [].concat($scope.rowCollection);
        }, function () {
            alert('get data error');
        });
    }
    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.divGrid = true;
    }

    $scope.add = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.PaymentName = "";
        $scope.PaymentCost = "";
        $scope.PaymentExpiryDate = "";
        $scope.PaymentTotalDay = "";
        $scope.Quantity = 1;
        $scope.Operation = "New";
        $scope.divModification = true;
        $scope.divGrid = false;
    }
    $scope.change = function (value) {
        alert('change');
    }

    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var payment = {
                PaymentName: $scope.PaymentName,
                PaymentTotalDay: $scope.PaymentTotalDay,
                PaymentCost: $scope.PaymentCost,
                PaymentExpiryDate: $scope.PaymentExpiryDate,
                Quantity: $scope.Quantity
            };
            var Operation = $scope.Operation;
            var getMSG = paymentService.Add(payment);
            getMSG.then(function (messagefromController) {
                if (messagefromController.data == "Success") {
                    GetPaymentsList();
                    $scope.divModification = false;
                    $scope.divGrid = true;
                } else {
                    $scope.errormessage = messagefromController.data;
                }
            }, function () {
                $scope.errormessage = "Add payment failed.";
            });
        }
    }

    $scope.delete = function (payment) {
        if (confirm('Please confirm to delete.')) {
            var getMSG = paymentService.Delete(payment.PaymentID);
            getMSG.then(function (messagefromController) {
                GetPaymentsList();
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete payment failed.";
            });
        }
    }
});
app.controller("CategoryController", function ($scope) {
    $scope.assets = [
         { assetId: 1, name: "parent 1", hasChildren: true },
         { assetId: 2, name: "parent 2", hasChildren: false }
    ];
    $scope.selected = { name: "child 111" };
    $scope.hierarchy = "1,11";
    $scope.loadChildren = function (nodeId) {
        return [
            { assetId: parseInt(nodeId + "1"), name: "child " + nodeId + "1", hasChildren: true },
            { assetId: parseInt(nodeId + "2"), name: "child " + nodeId + "2" }
        ];
    }
    $scope.$on("nodeSelected", function (event, node) {
        $scope.selected = node;
        $scope.$broadcast("selectNode", node);
    });
});






