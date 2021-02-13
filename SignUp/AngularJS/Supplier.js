///<reference path="angular.js" />
var supplierApp = angular.module('supplierApp', ['LocalStorageModule', 'AuthApp']);

supplierApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

supplierApp.controller('supplierController', ['$scope', 'supplierService', function ($scope, supplierService) {

    Clear();
    function Clear() {

        $scope.Supplier = {};
        $scope.btnSave = 'Add Supplier';
        $scope.addBtnDisable = false;
        $scope.Supplier.SupplierId = 0;
        $scope.SupplierList = [];
        $scope.SupplierTempList = [];
        GetAllSuppliers();
        LoadSupplier();
    }

    function LoadSupplier() {
        var supplier = sessionStorage.getItem("Supplier");
        if (supplier != null) {
            $scope.btnSave = 'Update Supplier';
            $scope.Supplier = JSON.parse(sessionStorage.Supplier);
        }
        sessionStorage.removeItem("Supplier");
    }

    function GetAllSuppliers() {
        supplierService.GetAllSuppliers().then(function (response) {
            $scope.SupplierList = response.data;
        }, function () {
            alert("Failed to get supplier!");
        })
    }
    function ResetObject() {
        $scope.Supplier = {};
        $scope.Supplier.SupplierId = 0;
    }
    $scope.Edit = function (supplier) {
        sessionStorage.setItem("Supplier", JSON.stringify(supplier));
        window.location.href = "/Suppliers/Create"
    }
    $scope.Delete = function (supplier) {
        var r = confirm("Are you sure you want to delete?");
        if (r == true) {
            supplierService.DeleteSupplier(supplier).then(function (response) {
                alert("Supplier Deleted Successfully");
                window.location.reload();
            }, function () {
                alert("Error occured. Please try again.");

            });
        } else {
            return;
        }
    }
    $scope.AddSupplier = function (supplier) {
        if ($scope.Supplier.SupplierId == 0) {
            $scope.Supplier.Status = true;
            $scope.SupplierTempList.push(supplier)
            ResetObject();
        }
        else {
            $scope.SupplierTempList.push(supplier);
            $scope.btnSave = 'Add Supplier';
            $scope.addBtnDisable = true;
            ResetObject();
        }
    }
    $scope.AddNew = function () {
        window.location.href = "/Suppliers/Create";
    }
    $scope.SaveSuppliers = function () {
        var supplierCount = $scope.SupplierTempList.length;
        angular.forEach($scope.SupplierTempList, function (data) {
            if (data.SupplierId == 0) {
                supplierService.SaveSuppliers(data).then(function (response) {
                    supplierCount--;
                    if (supplierCount == 0) {
                        alert("Supplier Saved Successfully");
                        window.location.href = "/Suppliers/Index";
                    }

                }, function () {
                    alert("Error occured. Please try again.");

                });
            }
            else {
                supplierService.UpdateSupplier(data).then(function (response) {
                    supplierCount--;
                    if (supplierCount == 0) {
                        alert("Supplier Update Successfully");
                        window.location.href = "/Suppliers/Index";
                    }

                }, function () {
                    alert("Error occured. Please try again.");

                });
            }

        });


    }

    $scope.ResetObject = function () {
        ResetObject();
    }
    $scope.RedirectToList = function () {
        window.location.href = "/Suppliers/Index";
    }
    $scope.Remove = function (supplier) {
        var index = $scope.SupplierTempList.indexOf(supplier);
        $scope.SupplierTempList.splice(index, 1);
    }


}])


supplierApp.factory('supplierService', ['$http', function ($http) {

    var supplierAppFactory = {};

    supplierAppFactory.GetAllSuppliers = function () {
        return $http.get('/api/Suppliers/AllSuppliers')
    }

    supplierAppFactory.SaveSuppliers = function (Supplier) {
        return $http.post('/api/Suppliers/CreateSupplier', Supplier)
    };
    supplierAppFactory.UpdateSupplier = function (Supplier) {
        return $http.put('/api/Suppliers/UpdateSupplier/' + Supplier.SupplierId, Supplier)
    };
    supplierAppFactory.DeleteSupplier = function (Supplier) {
        return $http.delete('/api/Suppliers/DeleteSupplier/' + Supplier.SupplierId)
    };

    return supplierAppFactory;

}])