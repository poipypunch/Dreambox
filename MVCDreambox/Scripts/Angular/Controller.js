//------------------------------- LoginController ----------------------------------------------------//
app.controller("LoginController", function ($scope, loginService, $location) {
    $scope.$broadcast('show-errors-reset');
    $scope.errormessage = "";
    $scope.UserName = "";
    $scope.Password = "";
    $scope.Login = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var getMSG = loginService.Login($scope.UserName, $scope.Password);
            getMSG.then(function (messagefromController) {
                if (messagefromController.data == "Success") {
                    window.location = "/Home/Index";
                } else {
                    $scope.errormessage = messagefromController.data;
                }
            }, function () {
                $scope.errormessage = "Login failed.";
            });
        }
    }
    $scope.Cancel = function () {
        $scope.$broadcast('show-errors-check-validity');
        $scope.errormessage = "";
        $scope.UserName = "";
        $scope.Password = "";
    }
});

//------------------------------- UserController ----------------------------------------------------//
app.controller("UserController", function ($scope, userService, $filter) {
    $scope.divModification = false;
    $scope.AddNew = true;
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
        //$scope.Password = tbuser.Password;
        $scope.RealName = tbuser.RealName;
        $scope.Email = tbuser.Email;
        $scope.Phone = tbuser.Phone;
        $scope.Address = tbuser.Address;
        $scope.Role = tbuser.Role;
        $scope.Status = tbuser.Status;
        $scope.Operation = "Update";
        $scope.divModification = true;
        //$scope.showRepassword = false;
        $scope.AddNew = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        //$scope.showRepassword = false;
        $scope.AddNew = true;
    }

    $scope.add = function () {
        $scope.$broadcast('show-errors-reset');
        $scope.errormessage = "";
        $scope.DealerID = "";
        $scope.UserName = "";
        //$scope.Password = "";
        $scope.RealName = "";
        $scope.Email = "";
        $scope.Phone = "";
        $scope.Address = "";
        $scope.Role = "Dealer";
        $scope.Status = "Active";
        $scope.Operation = "New";
        $scope.divModification = true;
        //$scope.showRepassword = true;
        $scope.AddNew = false;
    }

    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var tbuser = {
                DealerID: $scope.DealerID,
                UserName: $scope.UserName,
                //Password: $scope.Password,
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
                        $scope.AddNew = true;
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
                        $scope.AddNew = true;
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
        $scope.divModification = false;
        $scope.AddNew = true;
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
    $scope.AddNew = true;
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
    $scope.ToJavaScriptDateToString = function (value) {
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return results[1].toString();
        } else {
            return "";
        }
    }
    $scope.edit = function (Package) {
        $scope.errormessage = "";
        $scope.PackageID = Package.PackageID;
        $scope.PackageDesc = Package.PackageDesc;
        $scope.PackageStatus = Package.PackageStatus;
        $scope.Operation = "Update";
        $scope.divModification = true;
        $scope.AddNew = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.AddNew = true;
    }

    $scope.add = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.PackageID = "";
        $scope.PackageDesc = "";
        $scope.PackageStatus = "Active";
        $scope.Operation = "New";
        $scope.divModification = true;
        $scope.AddNew = false;
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
                        $scope.AddNew = true;
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
                        $scope.AddNew = true;
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
        $scope.divModification = false;
        $scope.AddNew = true;
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
    $scope.AddNew = true;
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
    $scope.ToJavaScriptDateToString = function (value) {
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return results[1].toString();
        } else {
            return "";
        }
    }
    $scope.edit = function (channel) {
        $scope.errormessage = "";
        $scope.ChannelID = channel.ChannelID;
        $scope.ChannelDesc = channel.ChannelDesc;
        $scope.ChannelPath = channel.ChannelPath;
        $scope.ChannelStatus = channel.ChannelStatus;
        $scope.Operation = "Update";
        $scope.divModification = true;
        $scope.AddNew = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.AddNew = true;
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
        $scope.AddNew = false;
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
                        $scope.AddNew = true;
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
                        $scope.AddNew = true;
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
        $scope.divModification = false;
        $scope.AddNew = true;
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
    $scope.ToJavaScriptDateToString = function (value) {
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return results[1].toString();
        } else {
            return "";
        }
    }
    $scope.selectRow = function (pack) {
        $scope.divMappingChannel = true;
        $scope.SelectItemChans = [];
        $scope.divActiveChannel = false;
        $scope.currentPackage = pack;
        GetMappingChannelsList($scope.currentPackage.PackageID);
    }
    $scope.addChannel = function () {
        $scope.SelectItemChans = [];
        $scope.divActiveChannel = true;
        GetActiveChannelsList($scope.currentPackage.PackageID);
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
            var getMSG = packagemapService.Add($scope.currentPackage.PackageID, $scope.SelectItemChans);
            getMSG.then(function (messagefromController) {
                if (messagefromController.data == "Success") {
                    GetMappingChannelsList($scope.currentPackage.PackageID);
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
        $scope.SelectItemChans = [];
        $scope.divActiveChannel = false;
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
//---------------------- PackagePermissionController -------------------------------//
app.controller("PackagePermissionController", function ($scope, packagePerService, $filter) {
    $scope.divMappingPackage = false;
    $scope.divActivePackage = false;
    $scope.SelectItemPacks = [];
    GetActiveUserList();
    function GetActiveUserList() {
        var Data = packagePerService.getActiveUserList();
        Data.then(function (pack) {
            $scope.itemsByPage = 10;
            $scope.rowCollection = pack.data;
            $scope.displayedCollection = [].concat($scope.rowCollection);
        }, function () {
            alert('get data error');
        });
    }
    function GetMappingPackagesList(DealerID) {
        var Data = packagePerService.getMappingPackageList(DealerID);
        Data.then(function (pack) {
            $scope.itemsmapByPage = 10;
            $scope.mapCollection = pack.data;
            $scope.displayedmapCollection = [].concat($scope.mapCollection);
        }, function () {
            alert('get data error');
        });
    }
    function GetActivePackagesList(DealerID) {
        var Data = packagePerService.getActivePackagesList(DealerID);
        Data.then(function (pack) {
            $scope.itemspackByPage = 10;
            $scope.packCollection = pack.data;
            $scope.displayedpackCollection = [].concat($scope.packCollection);
        }, function () {
            alert('get data error');
        });
    }
    $scope.ToJavaScriptDateToString = function (value) {
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return results[1].toString();
        } else {
            return "";
        }
    }
    $scope.selectRow = function (user) {
        $scope.divMappingPackage = true;
        $scope.SelectItemPacks = [];
        $scope.divActivePackage = false;
        $scope.currentDealer = user;
        GetMappingPackagesList($scope.currentDealer.DealerID);
    }
    $scope.addPackage = function () {
        $scope.SelectItemPacks = [];
        $scope.divActivePackage = true;
        GetActivePackagesList($scope.currentDealer.DealerID);
    }
    $scope.cancel = function () {
        $scope.SelectItemPacks = [];
        $scope.divActivePackage = false;
    }
    $scope.addSelect = function (pack) {
        if ($scope.SelectItemPacks.indexOf(pack.PackageID) != -1) return;
        $scope.SelectItemPacks.push(pack.PackageID);
    }

    $scope.add = function () {
        if ($scope.SelectItemPacks == null || $scope.SelectItemPacks.length <= 0) {
            $scope.errormessage = "Please select package.";
        } else {
            var getMSG = packagePerService.Add($scope.currentDealer.DealerID, $scope.SelectItemPacks);
            getMSG.then(function (messagefromController) {
                if (messagefromController.data == "Success") {
                    GetMappingPackagesList($scope.currentDealer.DealerID);
                    $scope.SelectItemPacks = [];
                    $scope.divActivePackage = false;
                } else {
                    alert(messagefromController.data);
                }
            }, function () {
                $scope.errormessage = "Mapping package failed.";
            });
        }
    }
    $scope.delete = function (pack) {
        $scope.SelectItemPacks = [];
        $scope.divActivePackage = false;
        if (confirm('Please confirm to delete.')) {
            var getMSG = packagePerService.Delete(pack.DealerID, pack.PackageID);
            getMSG.then(function (messagefromController) {
                //GetChannelList();
                GetMappingPackagesList(pack.DealerID);
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete package permission failed.";
            });
        }
    }
});
//---------------------- MemberTypeController -----------------------------------//
app.controller("MemberTypeController", function ($scope, memberTypeService) {
    $scope.divModification = false;
    $scope.AddNew = true;
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
    $scope.ToJavaScriptDateToString = function (value) {
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return results[1].toString();
        } else {
            return "";
        }
    }
    $scope.edit = function (memtype) {
        $scope.errormessage = "";
        $scope.MemberTypeID = memtype.MemberTypeID;
        $scope.MemberTypeDesc = memtype.MemberTypeDesc;
        $scope.Operation = "Update";
        $scope.divModification = true;
        $scope.AddNew = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.AddNew = true;
    }

    $scope.add = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.MemberTypeID = "";
        $scope.MemberTypeDesc = "";
        $scope.Operation = "New";
        $scope.divModification = true;
        $scope.AddNew = false;
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
                        $scope.AddNew = true;
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
                        $scope.AddNew = true;
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
        $scope.divModification = false;
        $scope.AddNew = true;
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
    $scope.AddNew = true;
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
    $scope.ToJavaScriptDateToString = function (value) {
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return results[1].toString();
        } else {
            return "";
        }
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
        //$scope.Password = member.Password;
        $scope.MemberName = member.MemberName;
        $scope.Address = member.Address;
        $scope.Email = member.Email;
        $scope.Phone = member.Phone;
        $scope.MemberTypeID = member.MemberTypeID;
        $scope.Operation = "Update";
        $scope.divModification = true;
        $scope.showRepassword = false;
        $scope.AddNew = false;
    }

    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        //$scope.showRepassword = false;
        $scope.AddNew = true;
    }

    $scope.add = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.MemberID = "";
        $scope.UserName = "";
        //$scope.Password = "";
        $scope.RePassword = "";
        $scope.MemberName = "";
        $scope.Address = "";
        $scope.Email = "";
        $scope.Phone = "";
        $scope.MemberTypeID = "";
        $scope.Operation = "New";
        $scope.showRepassword = true;
        $scope.divModification = true;
        $scope.AddNew = false;
    }

    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var Member = {
                MemberID: $scope.MemberID,
                UserName: $scope.UserName,
                MemberName: $scope.MemberName,
                //Password: $scope.Password,
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
                        $scope.AddNew = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Update member failed.";
                });
            }
            else {
                var getMSG = memberService.Add(Member);
                //if (Member.Password == Member.RePassword) {
                getMSG.then(function (messagefromController) {
                    GetMemberTypeList();
                    GetMemberList();
                    if (messagefromController.data == "Success") {
                        alert(messagefromController.data);
                        $scope.divModification = false;
                        $scope.AddNew = true;
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Add member failed.";
                });
                //} else {
                //    $scope.errormessage = "Confirm password mismatch.";
                //}
            }
        }
    }

    $scope.delete = function (member) {
        $scope.AddNew = true;
        $scope.divModification = false;
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
    $scope.AddNew = true;
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
    //$scope.ToJavaScriptDate = function (value) {
    //    var pattern = /Date\(([^)]+)\)/;
    //    var results = pattern.exec(value);
    //    var dt = new Date(parseFloat(results[1]));
    //    return dt.getFullYear() + "/" + (dt.getMonth() + 1) + "/" + dt.getDate();
    //}
    $scope.ToJavaScriptDateToString = function (value) {
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return results[1].toString();
        } else {
            return "";
        }
    }
    $scope.Cancel = function () {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = false;
        $scope.AddNew = true;
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
        $scope.AddNew = false;
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
                    $scope.AddNew = true;
                } else {
                    $scope.errormessage = messagefromController.data;
                }
            }, function () {
                $scope.errormessage = "Add payment failed.";
            });
        }
    }

    $scope.delete = function (payment) {
        $scope.AddNew = true;
        $scope.divModification = false;
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
app.controller("CategoryController", function ($scope, categoryService, $filter) {
    $scope.iscollapsed = true;
    $scope.divAdd = false;
    GetCategoryTreeList();
    function GetCategoryTreeList() {
        var Data = categoryService.GetCategorys();
        Data.then(function (pack) {
            $scope.data = _queryTreeSort(pack);
            $scope.categories = _makeTree($scope.data);
        }, function () {
            alert('get data error');
        });
    }
    $scope.selectNodeHead = function (category) {
        category.iscollapsed = !category.iscollapsed;
    }
    var _makeTree = function (options) {
        var children, e, id, o, pid, temp, _i, _len, _ref;
        id = options.id || "CategoryID";
        pid = options.parentid || "ParentID";
        children = options.categories || "categories";
        temp = {};
        o = [];
        _ref = options;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            e = _ref[_i];
            e[children] = [];
            temp[e[id]] = e;
            if (temp[e[pid]] != null) {
                temp[e[pid]][children].push(e);
            } else {
                o.push(e);
            }
        }
        return o;
    };

    var _queryTreeSort = function (options) {
        var cfi, e, i, id, o, pid, rfi, ri, thisid, _i, _j, _len, _len1, _ref, _ref1;
        id = options.id || "CategoryID";
        pid = options.parentid || "ParentID";
        ri = [];
        rfi = {};
        cfi = {};
        o = [];
        _ref = options.data;
        for (i = _i = 0, _len = _ref.length; _i < _len; i = ++_i) {
            e = _ref[i];
            rfi[e[id]] = i;
            if (cfi[e[pid]] == null) {
                cfi[e[pid]] = [];
            }
            cfi[e[pid]].push(options.data[i][id]);
        }
        _ref1 = options.data;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
            e = _ref1[_j];
            if (rfi[e[pid]] == null) {
                ri.push(e[id]);
            }
        }
        while (ri.length) {
            thisid = ri.splice(0, 1);
            o.push(options.data[rfi[thisid]]);
            if (cfi[thisid] != null) {
                ri = cfi[thisid].concat(ri);
            }
        }
        return o;
    };


    //  $scope.categories = [
    //{
    //    title: 'Computers',
    //    id: '1',
    //    categories: [
    //      {
    //          title: 'Laptops',
    //          categories: [
    //            {
    //                title: 'Ultrabooks'
    //            },
    //            {
    //                title: 'Macbooks'
    //            }
    //          ]
    //      },

    //      {
    //          title: 'Desktops',
    //          id: '2',
    //      },

    //      {
    //          title: 'Tablets',
    //          id: '3',
    //          categories: [
    //            {
    //                title: 'Apple',
    //                categories: [
    //                        { title: 'ttt' }
    //                        , { title: 'dfdfdfd' }
    //                ]
    //            },
    //            {
    //                title: 'Android'
    //                , id: '4'
    //            }
    //          ]
    //      }
    //    ]
    //},
    //{
    //    title: 'Printers',
    //    id: '5'
    //}
    //  ];

    $scope.SelectNode = function (cate, parent) {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divModification = true;
        $scope.currentNode = cate;
        $scope.currentParentNode = parent;
    }
    $scope.add = function () {
        $scope.Operation = "Add";
        $scope.divAdd = true;
        $scope.ParentID = $scope.currentNode.CategoryID;
        $scope.CategoryDesc = "";
    }
    $scope.edit = function () {
        $scope.Operation = "Update";
        $scope.divAdd = true;
        $scope.ParentID = $scope.currentNode.CategoryID
        $scope.CategoryDesc = $scope.currentNode.CategoryDesc;
        $scope.ImgPath = $scope.currentNode.ImgPath;
    }
    $scope.cancel = function (cate) {
        $scope.errormessage = "";
        $scope.$broadcast('show-errors-reset');
        $scope.divAdd = false;
    }
    $scope.Save = function () {
        $scope.$broadcast('show-errors-check-validity');
        if ($scope.appForm.$valid) {
            var cate = {
                CategoryID: "",
                CategoryDesc: $scope.CategoryDesc,
                ImgPath: $scope.ImgPath,
                ParentID: $scope.ParentID,
            };
            var Operation = $scope.Operation;
            if (Operation == "Update") {
                cate.CategoryID = $scope.CategoryID;
                var getMSG = categoryService.update(cate);
                getMSG.then(function (messagefromController) {
                    if (messagefromController.data == "Success") {
                        alert(messagefromController.data);
                        $scope.divAdd = false;
                        $scope.divModification = false;
                        parent_node = $scope.currentParentNode;
                        parent_node = parent_node || $scope.categories;
                        var index = parent_node.categories.indexOf($scope.currentNode);
                        if (index != -1) {
                            update_node = parent_node.categories[index];
                            update_node.CategoryDesc = $scope.CategoryDesc;
                        }
                    } else {
                        $scope.errormessage = messagefromController.data;
                    }
                }, function () {
                    $scope.errormessage = "Update category failed.";
                });
            }
            else {
                var getMSG = categoryService.Add(cate);
                getMSG.then(function (messagefromController) {
                    var str = [];
                    str = messagefromController.data.split('|');
                    if (str[0] == "Success") {
                        alert(str[0]);
                        $scope.divAdd = false;
                        $scope.divModification = false;
                        current_node = $scope.currentNode;
                        cate.CategoryID = str[1];
                        if (current_node.categories)
                        { current_node.categories.push(cate); }
                        else {
                            current_node.categories = cate;
                        }
                    } else {
                        $scope.errormessage = str[1];
                    }
                }, function () {
                    $scope.errormessage = "Add category failed.";
                });
            }
        }
    }
    $scope.delete = function () {
        if (confirm('Please confirm to delete.')) {
            var getMSG = categoryService.Delete($scope.currentNode.CategoryID);
            getMSG.then(function (messagefromController) {
                alert(messagefromController.data);
                parent_node = $scope.currentParentNode;
                parent_node = parent_node || $scope.categories;
                var index = parent_node.categories.indexOf($scope.currentNode);
                if (index != -1) {
                    parent_node.categories.splice(index, 1);
                }
            }, function () {
                $scope.errormessage = "Delete category failed.";
            });
        }
    }
});

//---------------------- ContentManagementController -------------------------------//
app.controller("ContentManagementController", function ($scope, contentService, $filter) {
    $scope.iscollapsed = true;
    $scope.divMappingChannel = false;
    $scope.divActiveChannel = false;
    $scope.SelectItemChans = [];
    GetCategoryTreeList();

    function GetCategoryTreeList() {
        var Data = contentService.GetCategorys();
        Data.then(function (cates) {
            $scope.data = _queryTreeSort(cates);
            $scope.categories = _makeTree($scope.data);
        }, function () {
            alert('get data error');
        });
    }

    function GetMappingChannelsList(CategoryID) {
        var Data = contentService.getMappingChannelsList(CategoryID);
        Data.then(function (pack) {
            $scope.itemsmapByPage = 10;
            $scope.mapCollection = pack.data;
            $scope.displayedmapCollection = [].concat($scope.mapCollection);
        }, function () {
            alert('get data error');
        });
    }

    function GetActiveChannelsList(CategoryID) {
        var Data = contentService.getActiveChannelsList(CategoryID);
        Data.then(function (pack) {
            $scope.itemschanByPage = 10;
            $scope.chanCollection = pack.data;
            $scope.displayedchanCollection = [].concat($scope.chanCollection);
        }, function () {
            alert('get data error');
        });
    }

    $scope.selectNodeHead = function (category) {
        category.iscollapsed = !category.iscollapsed;
    }
    $scope.SelectNode = function (cate, parentnode) {
        if (!(cate.categories != null && cate.categories.length > 0)) {
            $scope.divActiveChannel = false;
            $scope.SelectItemChans = [];
            $scope.errormessage = "";
            $scope.currentNode = cate;
            $scope.currentParentNode = parentnode;
            GetMappingChannelsList($scope.currentNode.CategoryID)
            $scope.divMappingChannel = true;
        }
    }
    var _makeTree = function (options) {
        var children, e, id, o, pid, temp, _i, _len, _ref;
        id = options.id || "CategoryID";
        pid = options.parentid || "ParentID";
        children = options.categories || "categories";
        temp = {};
        o = [];
        _ref = options;
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
            e = _ref[_i];
            e[children] = [];
            temp[e[id]] = e;
            if (temp[e[pid]] != null) {
                temp[e[pid]][children].push(e);
            } else {
                o.push(e);
            }
        }
        return o;
    };

    var _queryTreeSort = function (options) {
        var cfi, e, i, id, o, pid, rfi, ri, thisid, _i, _j, _len, _len1, _ref, _ref1;
        id = options.id || "CategoryID";
        pid = options.parentid || "ParentID";
        ri = [];
        rfi = {};
        cfi = {};
        o = [];
        _ref = options.data;
        for (i = _i = 0, _len = _ref.length; _i < _len; i = ++_i) {
            e = _ref[i];
            rfi[e[id]] = i;
            if (cfi[e[pid]] == null) {
                cfi[e[pid]] = [];
            }
            cfi[e[pid]].push(options.data[i][id]);
        }
        _ref1 = options.data;
        for (_j = 0, _len1 = _ref1.length; _j < _len1; _j++) {
            e = _ref1[_j];
            if (rfi[e[pid]] == null) {
                ri.push(e[id]);
            }
        }
        while (ri.length) {
            thisid = ri.splice(0, 1);
            o.push(options.data[rfi[thisid]]);
            if (cfi[thisid] != null) {
                ri = cfi[thisid].concat(ri);
            }
        }
        return o;
    };
    $scope.ToJavaScriptDateToString = function (value) {
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return results[1].toString();
        } else {
            return "";
        }
    }
    $scope.addChannel = function () {
        $scope.SelectItemChans = [];
        $scope.divActiveChannel = true;
        GetActiveChannelsList($scope.currentNode.CategoryID);
    }
    $scope.cancel = function () {
        $scope.SelectItemChans = [];
        $scope.divActiveChannel = false;
    }

    $scope.add = function () {
        if ($scope.SelectItemChans == null || $scope.SelectItemChans.length <= 0) {
            $scope.errormessage = "Please select channel to map.";
        } else {
            var getMSG = contentService.Add($scope.currentNode.CategoryID, $scope.SelectItemChans);
            getMSG.then(function (messagefromController) {
                if (messagefromController.data == "Success") {
                    GetMappingChannelsList($scope.currentNode.CategoryID);
                    $scope.SelectItemChans = [];
                    $scope.divActiveChannel = false;
                } else {
                    alert(messagefromController.data);
                }
            }, function () {
                $scope.errormessage = "Mapping failed.";
            });
        }
    }
    $scope.delete = function (cate) {
        $scope.SelectItemChans = [];
        $scope.divActiveChannel = false;
        if (confirm('Please confirm to delete.')) {
            var getMSG = contentService.Delete(cate.CategoryID, cate.ChannelID);
            getMSG.then(function (messagefromController) {
                GetMappingChannelsList(cate.CategoryID);
                alert(messagefromController.data);
            }, function () {
                $scope.errormessage = "Delete failed.";
            });
        }
    }
});






