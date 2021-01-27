///<reference path="angular.js" />

var categoryApp = angular.module('categoryApp', ['LocalStorageModule', 'AuthApp']);

categoryApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

categoryApp.controller('categoryController', ['$scope', 'categoryService', function ($scope, categoryService) {

    $scope.init = function () {
        categoryService.GetAllCategorys().then(function (response) {
            $scope.CategoryList = response.data;
        }, function () {
            alert("Failed to get Category!");
        })
    }

    $scope.init();


}])


categoryApp.factory('categoryService', ['$http', function ($http) {

    var catagoryAppFactory = {};

    catagoryAppFactory.GetAllCategorys = function () {
        return $http.get('/api/Categorys')
    }


    return catagoryAppFactory;

}])