app.controller("ChannelController", function ($scope, channelService, ngTableParams) {
    $scope.divChannelModification = false;
    GetAll();


    //To Get All Records  
    function GetAll() {
        
        var Data = channelService.getChan();
        Data.then(function (chan) {
            $scope.channels = chan.data;             
            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10           // count per page
            }, {
                total: chan.data.length, // length of data
                getData: function ($defer, params) {
                    $scope.channels = chan.data.slice((params.page() - 1) * params.count(), params.page() * params.count());
                    params.total(chan.data.length);
                    $defer.resolve($scope.channels);
                }
            });
           
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
    }

    $scope.Cancel = function () {
        $scope.ChannelID = "";
        $scope.ChannelDesc = "";
        $scope.ChannelPath = "";
        $scope.ChannelStatus = "";
        $scope.CreateBy = "";
        $scope.UpdateBy = "";
        $scope.divChannelModification = false;
    }

    $scope.add = function () {
        $scope.ChannelID = "";
        $scope.ChannelDesc = "";
        $scope.ChannelPath = "";
        $scope.ChannelStatus = "";
        $scope.CreateBy = "";
        $scope.UpdateBy = "";
        $scope.Operation = "Add";
        $scope.divChannelModification = true;
    }

    $scope.Save = function () {
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
                $scope.divChannelModification = false;
                GetAll();
                alert(messagefromController.data);
            }, function () {
                alert('Update Error');
            });
        }
        else {
            var getMSG = channelService.Add(Channel);
            getMSG.then(function (messagefromController) {
                //$scope.$apply(function () {
                //    GetAll();
                //    $scope.channels.push(Channel);                    
                //    $scope.tableParams.reload();
                //    $scope.ok();
                //});
                GetAll();
                //alert(messagefromController.data);
                //$scope.tableParams.reload();
                $scope.divChannelModification = false;
            }, function () {
                alert('Insert Error');
            });
        }
    }

    $scope.delete = function (channel) {
        var getMSG = channelService.Delete(channel.ChannelID);
        getMSG.then(function (messagefromController) {
            $scope['tableParams'] = { reload: function () { }, settings: function () { return {} } };
            GetAll();
            alert(messagefromController.data);
        }, function () {
            alert('Delete Error');
        });
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
            $scope.members = mem.data;
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
                alert(messagefromController.data);
                $scope.divMemberModification = false;
                $scope.divGrid = true;
            }, function () {
                alert('Update Error');
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
                    $scope.errormessage = "Insert member failed.";
                });
            } else {
                $scope.errormessage = "Confirm password mismatch.";
            }
        }
    }

    $scope.delete = function (member) {
        var getMSG = memberService.Delete(member.MemberID);
        getMSG.then(function (messagefromController) {
            GetMemberTypes();
            GetAllMember();
            alert(messagefromController.data);
        }, function () {
            alert('Delete Error');
        });
    }
});