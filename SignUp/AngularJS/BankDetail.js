///<reference path="angular.js" />

var bankDetailApp = angular.module('bankDetailApp', ['LocalStorageModule', 'AuthApp']);

bankDetailApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

bankDetailApp.controller('bankDetailController', ['$scope', 'bankDetailService', function ($scope, bankDetailService) {

    $scope.init = function () {
        bankDetailService.GetAllBankDetails().then(function (response) {
            $scope.BankDetailList = response.data;
        }, function () {
            alert("Failed to get Bank Detail!");
        })
    }

    $scope.init();


}])


bankDetailApp.factory('bankDetailService', ['$http', function ($http) {

    var bankDetailAppFactory = {};

    bankDetailAppFactory.GetAllBankDetails = function () {
        return $http.get('/api/BankDetails')
    }


    return bankDetailAppFactory;

}])