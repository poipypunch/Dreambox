var app = angular.module('MyApp', ['ngRoute', 'smart-table']);
app.config(['$routeProvider', function ($routeprovider) {
    var baseUrl = $("base").first().attr("href");
    $routeprovider.
        when('/Home', {
            templateurl: baseUrl+'Views/Home/index.cshtml',
            controller: 'HomeController'
        }).
      when('/Channel', {
          templateurl: baseUrl + 'Views/Channel/index.cshtml',
          controller: 'ChannelController'
      }).
      when('/Member', {
          templateurl: baseUrl + 'Views/Member/index.cshtml',
          controller: 'MemberController'
      }).
        when('/MemberType', {
            templateurl: baseUrl + 'Views/MemberType/index.cshtml',
            controller: 'MemberTypeController'
        }).
        when('/Package', {
            templateurl: baseUrl + 'Views/Package/index.cshtml',
            controller: 'PackgeController'
        }).
        when('/tbUser', {
            templateurl: baseUrl + 'Views/tbUser/index.cshtml',
            controller: 'UserController'
        }).
        when('/Category', {
            templateurl: baseUrl + 'Views/Category/index.cshtml',
            controller: 'CategoryController'
        }).
        when('/Payment', {
            templateurl: baseUrl + 'Views/Payment/index.cshtml',
            controller: 'PaymentController'
        }).
        when('/PackageMapping', {
            templateurl: baseUrl + 'Views/PackageMapping/index.cshtml',
            controller: 'PackageMappingController'
        }).
         when('/ContentManagement', {
             templateurl: baseUrl + 'Views/ContentManagement/index.cshtml',
             controller: 'ContentManagementController'
         }).
         when('/ChangePassword', {
             templateurl: baseUrl + 'Views/tbUser/ChangePassword.cshtml',
             controller: 'ChangePassowrdController'
         }).
         when('/MemberTypeMapping', {
             templateurl: baseUrl + 'Views/MemberTypeMapping/index.cshtml',
             controller: 'MemberTypeMappingController'
         }).
        when('/MemberSubscription', {
            templateurl: baseUrl + 'Views/MemberSubscription/index.cshtml',
            controller: 'MemberSubScribeController'
        }).
      otherwise({
          redirectto: baseUrl + 'Home'
      });
}]);
//app.config(['$httpProvider', function ($httpProvider) {
//    // Use x-www-form-urlencoded Content-Type
//    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
//    var param = function (obj) {
//        var query = '', name, value, fullSubName, subName, subValue, innerObj, i;

//        for (name in obj) {
//            value = obj[name];

//            if (value instanceof Array) {
//                for (i = 0; i < value.length; ++i) {
//                    subValue = value[i];
//                    fullSubName = name + '[' + i + ']';
//                    innerObj = {};
//                    innerObj[fullSubName] = subValue;
//                    query += param(innerObj) + '&';
//                }
//            }
//            else if (value instanceof Object) {
//                for (subName in value) {
//                    subValue = value[subName];
//                    fullSubName = name + '[' + subName + ']';
//                    innerObj = {};
//                    innerObj[fullSubName] = subValue;
//                    query += param(innerObj) + '&';
//                }
//            }
//            else if (value !== undefined && value !== null)
//                query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
//        }

//        return query.length ? query.substr(0, query.length - 1) : query;
//    };

//    // Override $http service's default transformRequest
//    $httpProvider.defaults.transformRequest = [function (data) {
//        return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
//    }];
//}]);