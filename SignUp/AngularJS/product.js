///<reference path="angular.js" />
var productApp = angular.module('productApp', ['LocalStorageModule', 'AuthApp']);

productApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

productApp.controller('productController', ['$scope', 'productService', function ($scope, productService) {

    //$scope.init = function () {



    //productService.GetAllProduct().then(function (response) {
    //    $scope.productList = response.data;
    //}, function () {
    //    alert("Failed to get supplier!");
    //})

    //productService.GetCategory().then(function (response) {
    //    $scope.categoryList = response.data;
    //}, function () {
    //    alert("Failed to get supplier!");
    //})

    //$scope.ddlProductEntry = [];
    //$scope.products = {};
    // }

    //$scope.init();
    //Clear();
    //function Clear() {

    //    

    //}

    Clear();
    function Clear() {

        $scope.productObj = {};
        $scope.ddlCategory = [];
        $scope.btnSave = 'Add Category';
        $scope.selectedName = null;
        //$scope.Supplier.SupplierId = 0;
        $scope.CategoryList = [];
        $scope.CategoryTempList = [];
        $scope.AddproductList = [];
        $scope.productList = [];
        GetAllCategorys();
        GetAllProduct();
        //  GetProductSave();
        //LoadSupplier();


    }

    ClearProduct();
    function ClearProduct() {

        $scope.productObj = new Object();
    }

    $scope.addProduct = function () {

        $scope.productObj.CategoryId = $scope.ddlCategory.CategoryId;


        if ($scope.AddproductList.length) {
        }
        $scope.AddproductList.push($scope.productObj);
        ClearProduct();
    }
    $scope.removebtn = function (productObj) {
        var index = $scope.AddproductList.indexOf(productObj);
        $scope.AddproductList.splice(index, 1);
    }


    function GetAllCategorys() {
        productService.GetAllCategorys().then(function (response) {
            $scope.CategoryList = response.data;
        }, function () {
            alert("Failed to get Category!");
        })
    }

    function GetAllProduct() {
        productService.GetAllProduct().then(function (response) {
            $scope.productList = response.data;
        }, function () {
            alert("Failed to get Category!");
        })
    }

    $scope.ProductSave = function () {

        productService.PostCategory($scope.AddproductList).then(function (response) {
        }, function () {
            alert("Data Save Success !!!");
            window.location.reload();
        });
    }

    //$scope.Delete = function (productObj) {
    //    var r = confirm("Are you sure you want to delete?");
    //    if (r == true) {
    //        productService.DeleteCategory(productObj).then(function (response) {
    //            alert("Supplier Deleted Successfully");
    //            //window.location.reload();
    //        }, function () {
    //            alert("Error occured. Please try again.");

    //        });
    //    } else {
    //        return;
    //    }
    //}

    $scope.Delete = function (productObj) {

        productService.DeleteCategory(productObj).then(function (response) {
            alert("Product Deleted Successfully");
            window.location.reload();
        }, function () {
            alert("Error occured. Please try again.");

        });
    }

    $scope.editProductBtn = function (cid) {
        $scope.ddlCategory = { CategoryId: cid.CategoryId }
        $scope.productObj = cid;
    }

}])


productApp.factory('productService', ['$http', function ($http) {

    var productAppFactory = {};

    productAppFactory.GetAllProduct = function () {
        return $http.get('/api/Products/Get')
    }



    productAppFactory.GetAllCategorys = function () {
        return $http.get('/api/Products/GetCategorys')
    }
    productAppFactory.PostCategory = function (productObj) {
        return $http.post('/api/Products/PostCategory', productObj)
    }

    productAppFactory.DeleteCategory = function (productObj) {
        return $http.delete('/api/Products/DeleteCategorys/' + productObj)
    };


    return productAppFactory;



}])