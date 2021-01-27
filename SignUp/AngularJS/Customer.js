///<reference path="angular.js" />

var customerApp = angular.module('customerApp', ['LocalStorageModule', 'AuthApp']);

customerApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

customerApp.controller('customerController', ['$scope', 'customerService', function ($scope, customerService) {

    $scope.init = function () {
        customerService.GetAllCustomers().then(function (response) {
            $scope.CustomerList = response.data;
        }, function () {
            alert("Failed to get Customer!");
        })
    }

    $scope.init();


}])


customerApp.factory('customerService', ['$http', function ($http) {

    var customerAppFactory = {};

    customerAppFactory.GetAllCustomers = function () {
        return $http.get('/api/Customers')
    }


    return customerAppFactory;

}])