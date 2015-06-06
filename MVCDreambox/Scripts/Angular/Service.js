app.filter("ToJavaScriptDateToString", function () {
    return function (value) {
        // Your logic
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return results[1].toString();
        } else {
            return "";
        }
    }
});
app.filter("ToJavaScriptDate", function () {
    return function (value) {
        if (value != null) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            var dt = new Date(parseFloat(results[1]));
            return (dt.getFullYear() + "-" + ('0' + (dt.getMonth() + 1)).slice(-2) + "-" + ('0' + (dt.getDate())).slice(-2)).toString();
        } else {
            return "";
        }
    }
});

app.filter('strLimit', ['$filter', function ($filter) {
    return function (input, limit) {
        if (!input) return;
        if (input.length <= limit) {
            return input;
        }
        return $filter('limitTo')(input, limit) + '...';
    };
}]);
app.service("loginService", function ($http, ShareService) {
    //Login 
    var baseUrl = ShareService.GetBaseUrl();
    this.Login = function (UserName, Password) {
        var response = $http({
            method: "post",
            url: baseUrl + "tbUser/CheckUser",
            params: {
                UserID: UserName,
                Password: Password
            }
        });
        return response;
    }
});
app.service("changePassService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getUsers = function () {
        return $http.get(baseUrl + "tbUser/ChangePassword");
    };
    //Login 
    this.UpdatePassword = function (NewPassword) {
        var response = $http({
            method: "post",
            url: baseUrl + "tbUser/UpdatePassword",
            params: {
                Password: NewPassword
            }
        });
        return response;
    }
});
app.service("userService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getUsers = function () {
        return $http.get(baseUrl + "tbUser/GetUsersList");
    };

    //Save (Update)  
    this.update = function (tbuser) {
        var response = $http({
            method: "post",
            url: baseUrl + "tbUser/Update",
            data: JSON.stringify(tbuser),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (DealerID) {
        var response = $http({
            method: "post",
            url: baseUrl + "tbUser/Delete",
            params: {
                id: DealerID
            }
        });
        return response;
    }

    // reset password
    this.ResetPassword = function (DealerID) {
        var response = $http({
            method: "post",
            url: baseUrl + "tbUser/ResetPassword",
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
            url: baseUrl + "tbUser/Add",
            data: JSON.stringify(tbuser),
            dataType: "json"

        });
        return response;
    }

});

app.service("packageService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getPackages = function () {
        return $http.get(baseUrl + "Package/GetPackagesList");
    };

    //Save (Update)  
    this.update = function (Package) {
        var response = $http({
            method: "post",
            url: baseUrl + "Package/Update",
            data: JSON.stringify(Package),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (PackageID) {
        var response = $http({
            method: "post",
            url: baseUrl + "Package/Delete",
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
            url: baseUrl + "Package/Add",
            data: JSON.stringify(Package),
            dataType: "json"

        });
        return response;
    }

});

app.service("channelService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getChannels = function () {
        return $http.get(baseUrl + "Channel/GetChannelsList");
    };

    //Save (Update)  
    this.update = function (channel) {
        var response = $http({
            method: "post",
            url: baseUrl + "Channel/Update",
            data: JSON.stringify(channel),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (channelID) {
        var response = $http({
            method: "post",
            url: baseUrl + "Channel/Delete",
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
            url: baseUrl + "Channel/Add",
            data: JSON.stringify(channel),
            dataType: "json"

        });
        return response;
    }

});

app.service("packagemapService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getActivePackagesList = function () {
        return $http.get(baseUrl + "PackageMapping/GetActivePackageList");
    };
    this.getMappingChannelsList = function (PackageID) {
        return $http({
            url: baseUrl + "PackageMapping/GetMappingChannelList",
            method: "GET",
            params: { PackageID: PackageID }
        });
        //return $http.get("/PackageMapping/GetMappingChannelList/" + PackageID);
    };
    this.getActiveChannelsList = function (PackageID) {
        return $http({
            url: baseUrl + "PackageMapping/GetActiveChannelList",
            method: "GET",
            params: { PackageID: PackageID }
        });
    };
    //Delete 
    this.Delete = function (PackageID, ChannelID) {
        var response = $http({
            method: "post",
            url: baseUrl + "PackageMapping/Delete",
            params: {
                pid: PackageID,
                cid: ChannelID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (PackageID, Channels) {
        //var response = $http({
        //    method: "post",
        //    url: "/PackageMapping/Add",
        //    params: {
        //        pid: PackageID,
        //        channelids: Channels
        //    }
        //});
        var response = $http({
            method: "post",
            url: baseUrl + "PackageMapping/Add",
            data: { pid: PackageID, channelids: Channels },
            dataType: "json"
        });
        return response;
    }

});

app.service("packagePerService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getActiveUserList = function () {
        return $http.get(baseUrl + "PackagePermission/GetActiveUserList");
    };
    this.getMappingPackageList = function (DealerID) {
        return $http({
            url: baseUrl + "PackagePermission/GetMappingPackageList",
            method: "GET",
            params: { DealerID: DealerID }
        });
        //return $http.get("/PackageMapping/GetMappingChannelList/" + PackageID);
    };
    this.getActivePackagesList = function (DealerID) {
        return $http({
            url: baseUrl + "PackagePermission/GetActivePackageList",
            method: "GET",
            params: { DealerID: DealerID }
        });
    };
    //Delete 
    this.Delete = function (DealerID, PackageID) {
        var response = $http({
            method: "post",
            url: baseUrl + "PackagePermission/Delete",
            params: {
                uid: DealerID,
                pid: PackageID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (DealerID, packs) {
        var response = $http({
            method: "post",
            url: baseUrl + "PackagePermission/Add",
            data: { uid: DealerID, packids: packs },
            dataType: "json"
        });
        return response;
    }

});

app.service("memberService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getMembers = function () {
        return $http.get(baseUrl + "Member/GetAllMember");
    };

    this.getMemberTypes = function () {
        return $http.get(baseUrl + "Member/GetAllMemberTypes");
    };

    this.update = function (member) {
        var response = $http({
            method: "post",
            url: baseUrl + "Member/Update",
            data: JSON.stringify(member),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (memberID) {
        var response = $http({
            method: "post",
            url: baseUrl + "Member/Delete",
            params: {
                id: memberID
            }
        });
        return response;
    }

    // reset password
    this.ResetPassword = function (memberID) {
        var response = $http({
            method: "post",
            url: baseUrl + "Member/ResetPassword",
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
            url: baseUrl + "Member/Add",
            data: JSON.stringify(member),
            dataType: "json"

        });
        return response;
    }
});
app.service("memberTypeService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.GetMemberTypes = function () {
        return $http.get(baseUrl + "MemberType/GetMemberTypesList");
    };
    this.update = function (memtype) {
        var response = $http({
            method: "post",
            url: baseUrl + "MemberType/Update",
            data: JSON.stringify(memtype),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (membertypeid) {
        var response = $http({
            method: "post",
            url: baseUrl + "MemberType/Delete",
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
            url: baseUrl + "MemberType/Add",
            data: JSON.stringify(memtype),
            dataType: "json"

        });
        return response;
    }
});
//---------------- PaymentService --------------------------------//
app.service("paymentService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getPayments = function () {
        return $http.get(baseUrl + "Payment/GetPaymentsList");
    };
    //Delete 
    this.Delete = function (PaymentID) {
        var response = $http({
            method: "post",
            url: "/MVCDreambox/Payment/Delete",
            params: {
                id: PaymentID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (payment) {
        var response = $http({
            method: "post",
            url: baseUrl + "Payment/Add",
            data: JSON.stringify(payment),
            dataType: "json"
        });
        return response;
    }

});

app.service("categoryService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.GetCategorys = function () {
        return $http.get(baseUrl + "Category/GetCategoryTrees");
    };
    this.update = function (memtype) {
        var response = $http({
            method: "post",
            url: baseUrl + "Category/Update",
            data: JSON.stringify(memtype),
            dataType: "json"
        });
        return response;
    }

    //Delete 
    this.Delete = function (categoryid) {
        var response = $http({
            method: "post",
            url: baseUrl + "Category/Delete",
            params: {
                id: categoryid
            }
        });
        return response;
    }

    //Add 
    //this.Add = function (cate) {
    //    var response = $http({
    //        method: "post",
    //        url: "/Category/Add",
    //        data: JSON.stringify(cate),
    //        dataType: "json"
    //    });
    //    return response;
    //}
    this.Add = function (cate) {
        var response = $http({
            method: "post",
            url: baseUrl + "Category/Add",
            data: getModelAsFormData(cate),
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
            // data: { category: JSON.stringify(cate), Attachment: getModelAsFormData(Attachment) },
            dataType: "json"

        });
        return response;
    }

    var getModelAsFormData = function (data) {
        var dataAsFormData = new FormData();
        angular.forEach(data, function (value, key) {
            dataAsFormData.append(key, value);
        });
        return dataAsFormData;
    };
});

app.service("contentService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.GetCategorys = function () {
        return $http.get(baseUrl + "ContentManagement/GetCategoryList");
    };
    this.getMappingChannelsList = function (CategoryID) {
        return $http({
            url: baseUrl + "ContentManagement/GetMappingChannelList",
            method: "GET",
            params: { CategoryID: CategoryID }
        });
    };
    this.getActiveChannelsList = function (CategoryID) {
        return $http({
            url: baseUrl + "ContentManagement/GetActiveChannelList",
            method: "GET",
            params: { CategoryID: CategoryID }
        });
    };

    this.getMaxOrder = function (CategoryID) {
        return $http({
            url: baseUrl + "ContentManagement/MaxOrder",
            method: "GET",
            params: { cid: CategoryID }
        });
    };
    //Delete 
    this.Delete = function (CategoryID, ChannelID) {
        var response = $http({
            method: "post",
            url: baseUrl + "ContentManagement/Delete",
            params: {
                cid: CategoryID,
                chid: ChannelID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (CategoryID, Channels) {
        var response = $http({
            method: "post",
            url: baseUrl + "ContentManagement/Add",
            data: { cid: CategoryID, channelids: Channels },
            dataType: "json"
        });
        return response;
    }

    this.Save = function (CategoryID, ChannelID,ChannelOrder,orgOrder) {
        var response = $http({
            method: "post",
            url: baseUrl + "ContentManagement/ChangeOrder",
            data: { cid: CategoryID, channelids: ChannelID, channelorder: ChannelOrder, org: orgOrder },
            dataType: "json"
        });
        return response;
    }

});


app.service("membertypeMapService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getActiveMemberTypeList = function () {
        return $http.get(baseUrl + "MemberTypeMapping/GetActiveMemberTypeList");
    };
    this.getMappingCategoryList = function (MemberTypeID) {
        return $http({
            url: baseUrl + "MemberTypeMapping/GetMappingCategoryList",
            method: "GET",
            params: { MemberTypeID: MemberTypeID }
        });
    };
    this.getActiveCategorysList = function (MemberTypeID) {
        return $http({
            url: baseUrl + "MemberTypeMapping/GetActiveCategorysList",
            method: "GET",
            params: { MemberTypeID: MemberTypeID }
        });
    };
    //Delete 
    this.Delete = function (MemberTypeID, CategoryID) {
        var response = $http({
            method: "post",
            url: baseUrl + "MemberTypeMapping/Delete",
            params: {
                mid: MemberTypeID,
                cid: CategoryID
            }
        });
        return response;
    }

    //Add 
    this.Add = function (MemberTypeID, Categorys) {
        var response = $http({
            method: "post",
            url: baseUrl + "MemberTypeMapping/Add",
            data: { mid: MemberTypeID, categoryids: Categorys },
            dataType: "json"
        });
        return response;
    }
});

app.service("memberSubService", function ($http, ShareService) {
    var baseUrl = ShareService.GetBaseUrl();
    this.getSubScribeList = function () {
        return $http.get(baseUrl + "MemberSubscription/GetSubScribeList");
    };
    this.getActiveMemberList = function () {
        return $http.get(baseUrl + "MemberSubscription/GetActiveMemberList");
    };
    this.checkPayment = function (PaymentID) {
        return $http({
            url: baseUrl + "MemberSubscription/CheckPayment",
            method: "GET",
            params: { PaymentID: PaymentID }
        });
    };    
    //Add 
    this.Add = function (MemberID, PaymentID) {
        var response = $http({
            method: "post",
            url: baseUrl + "MemberSubscription/Add",
            data: { mid: MemberID, pid: PaymentID },
            dataType: "json"
        });
        return response;
    }
});

app.service("ShareService", function ($http) {   
    this.GetBaseUrl = function () {
        return $("base").first().attr("href");
    };
    this._makeTree = function (options) {
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

    this._queryTreeSort = function (options) {
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
});