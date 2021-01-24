///<reference path="angular.js" />
var supplierApp = angular.module('supplierApp', ['LocalStorageModule', 'AuthApp']);

supplierApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

supplierApp.controller('supplierController', ['$scope', 'supplierService', function ($scope, supplierService) {

    $scope.init = function () {
        supplierService.GetAllSuppliers().then(function (response) {
            $scope.SupplierList = response.data;
        }, function () {
            alert("Failed to get supplier!");
        })
    }

    $scope.init();


}])


supplierApp.factory('supplierService', ['$http', function ($http) {

    var supplierAppFactory = {};

    supplierAppFactory.GetAllSuppliers = function () {
        return $http.get('/api/Suppliers')
    }


    return supplierAppFactory;

}])