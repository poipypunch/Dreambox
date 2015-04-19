var app = angular.module('MyApp', ['ngRoute', 'ngTable']);

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
      otherwise({
          redirectto: '/Channel'
      });
}]);