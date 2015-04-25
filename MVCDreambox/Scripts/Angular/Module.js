﻿var app = angular.module('MyApp', ['ngRoute', 'smart-table']);

app.config(['$routeProvider', function ($routeprovider) {
    $routeprovider.
      when('/Channel', {
          templateurl: '/Views/Channel/index.cshtml',
          controller: 'ChannelController'
      }).
      when('/Member', {
          templateurl: '/Views/Member/index.cshtml',
          controller: 'MemberController'
      }).
        when('/MemberType', {
            templateurl: '/Views/MemberType/index.cshtml',
            controller: 'MemberTypeController'
        }).
        when('/Package', {
            templateurl: '/Views/Package/index.cshtml',
            controller: 'PackgeController'
        }).
        when('/tbUser', {
            templateurl: '/Views/tbUser/index.cshtml',
            controller: 'UserController'
        }).
        when('/Category', {
            templateurl: '/Views/Category/index.cshtml',
            controller: 'CategoryController'
        }).
        when('/Payment', {
            templateurl: '/Views/Payment/index.cshtml',
            controller: 'PaymentController'
        }).
        when('/PackageMapping', {
            templateurl: '/Views/PackageMapping/index.cshtml',
            controller: 'PackageMappingController'
        }).
      otherwise({
          redirectto: '/Home'
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