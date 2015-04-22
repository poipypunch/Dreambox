﻿app.service("userService", function ($http) {
    this.getUsers = function () {
        return $http.get("/tbUser/GetUsersList");
    };

    //Save (Update)  
    this.update = function (tbuser) {
        var response = $http({
            method: "post",
            url: "/tbUser/Update",
            data: JSON.stringify(tbuser),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (DealerID) {
        var response = $http({
            method: "post",
            url: "/tbUser/Delete",
            params: {
                id: DealerID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (tbuser) {
        var response = $http({
            method: "post",
            url: "/tbUser/Add",
            data: JSON.stringify(tbuser),
            dataType: "json"

        });
        return response;
    }

});

app.service("packageService", function ($http) {
    this.getPackages = function () {
        return $http.get("/Package/GetPackagesList");
    };

    //Save (Update)  
    this.update = function (Package) {
        var response = $http({
            method: "post",
            url: "/Package/Update",
            data: JSON.stringify(Package),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (PackageID) {
        var response = $http({
            method: "post",
            url: "/Package/Delete",
            params: {
                id: PackageID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (Package) {
        var response = $http({
            method: "post",
            url: "/Package/Add",
            data: JSON.stringify(Package),
            dataType: "json"

        });
        return response;
    }

});

app.service("channelService", function ($http) {
    this.getChannels = function () {
        return $http.get("/Channel/GetChannelsList");
    };

    //Save (Update)  
    this.update = function (channel) {
        var response = $http({
            method: "post",
            url: "/Channel/Update",
            data: JSON.stringify(channel),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (channelID) {
        var response = $http({
            method: "post",
            url: "/Channel/Delete",
            params: {
                id: channelID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (channel) {
        var response = $http({
            method: "post",
            url: "/Channel/Add",
            data: JSON.stringify(channel),
            dataType: "json"

        });
        return response;
    }

});

app.service("memberService", function ($http) {
    this.getMembers = function () {
        return $http.get("/Member/GetAllMember");
    };

    this.getMemberTypes = function () {
        return $http.get("/Member/GetAllMemberTypes");
    };

    this.update = function (member) {
        var response = $http({
            method: "post",
            url: "/Member/Update",
            data: JSON.stringify(member),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (memberID) {
        var response = $http({
            method: "post",
            url: "/Member/Delete",
            params: {
                id: memberID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (member) {
        var response = $http({
            method: "post",
            url: "/Member/Add",
            data: JSON.stringify(member),
            dataType: "json"

        });
        return response;
    }    
});
app.service("memberTypeService", function ($http) {
    this.GetMemberTypes = function () {
        return $http.get("/MemberType/GetMemberTypesList");
    };
    this.update = function (memtype) {
        var response = $http({
            method: "post",
            url: "/MemberType/Update",
            data: JSON.stringify(memtype),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (membertypeid) {
        var response = $http({
            method: "post",
            url: "/MemberType/Delete",
            params: {
                id: membertypeid
            }
        });
        return response;
    }

    //Add 
    this.Add = function (memtype) {
        var response = $http({
            method: "post",
            url: "/MemberType/Add",
            data: JSON.stringify(memtype),
            dataType: "json"

        });
        return response;
    }    
});