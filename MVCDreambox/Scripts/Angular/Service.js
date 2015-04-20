app.service("channelService", function ($http) {
    this.getChan = function () {
        return $http.get("/Channel/GetAllChannels");
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
    this.getMember = function () {
        return $http.get("/Member/GetAllMember");
    };

    this.getMemberType = function () {
        return $http.get("/Member/GetAllMemberTypes");
    };
    //this.getRole = function () {
    //    return $http.get("/Member/GetAllRoles");
    //};

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

    //Save Permission  
    this.updateRole = function (member) {
        var response = $http({
            method: "post",
            url: "/Member/UpdateMember",
            data: JSON.stringify(member),
            dataType: "json"
        });
        return response;
    }
});