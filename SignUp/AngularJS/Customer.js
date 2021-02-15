///<reference path="angular.js" />

var customerApp = angular.module('customerApp', ['LocalStorageModule', 'AuthApp']);

customerApp.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

customerApp.controller('customerController', ['$scope', 'customerService', function ($scope, customerService) {
    Clear();
    function Clear() {

        $scope.Customer = {};
        $scope.btnSave = 'Add Customer';
        $scope.Customer.CustomerId = 0;
        $scope.CustomerList = [];
        $scope.CustomerTempList = [];
        GetAllCustomers();
        LoadCustomer();
    }
    function LoadCustomer() {
        var customer = sessionStorage.getItem("Customer");
        if (customer != null) {
            $scope.btnSave = 'Update Customer';
            $scope.Customer = JSON.parse(sessionStorage.Customer);
        }
        sessionStorage.removeItem("Customer");
    }

    function GetAllCustomers() {
        customerService.GetAllCustomers().then(function (response) {
            $scope.CustomerList = response.data;
        }, function () {
            alert("Failed to get customer!");
        })
    }
    $scope.AddCustomer = function (customer) {
        if ($scope.Customer.CustomerId == 0) {
            if ($scope.Customer.CustomerAddress == undefined || $scope.Customer.CustomerAddress == "") {
                $scope.Customer.CustomerAddress = "Undefined";
            }
            $scope.Customer.Status = true;
            $scope.CustomerTempList.push(customer)
            ResetObject();
        }
        else {
            $scope.CustomerTempList.push(customer);
            $scope.btnSave = 'Add Customer';
            ResetObject();
        }
    }
    $scope.Remove = function (customer) {
        var index = $scope.CustomerTempList.indexOf(customer);
        $scope.CustomerTempList.splice(index, 1);
    }
    $scope.SaveCustomers = function () {
        var customerCount = $scope.CustomerTempList.length;
        angular.forEach($scope.CustomerTempList, function (data) {
            if (data.CustomerId == 0) {
                customerService.SaveCustomers(data).then(function (response) {
                    customerCount--;
                    if (customerCount == 0) {
                        alert("Customer Saved Successfully");
                        window.location.href = "/Customers/Index";
                    }

                }, function () {
                    alert("Error occured. Please try again.");

                });
            }
            else {
                customerService.UpdateCustomer(data).then(function (response) {
                    customerCount--;
                    if (customerCount == 0) {
                        alert("Customer Update Successfully");
                        window.location.href = "/Customers/Index";
                    }

                }, function () {
                    alert("Error occured. Please try again.");

                });
            }

        });


    }
    $scope.Delete = function (customer) {
        var r = confirm("Are you sure you want to delete?");
        if (r == true) {
            customerService.DeleteCustomer(customer).then(function (response) {
                alert("Customer Deleted Successfully");
                window.location.reload();
            }, function () {
                alert("Error occured. Please try again.");

            });
        } else {
            return;
        }
    }
    $scope.Edit = function (customer) {
        sessionStorage.setItem("Customer", JSON.stringify(customer));
        window.location.href = "/Customers/Create"
    }
    function ResetObject() {
        $scope.Customer = {};
        $scope.Customer.CustomerId = 0;
    }
    $scope.ResetObject = function () {
        ResetObject();
    }
    $scope.AddNew = function () {
        window.location.href = "/Customers/Create";
    }
    $scope.RedirectToList = function () {
        window.location.href = "/Customers/Index";
    }
}])


customerApp.factory('customerService', ['$http', function ($http) {

    var customerAppFactory = {};

    customerAppFactory.GetAllCustomers = function () {
        return $http.get('/api/Customers/AllCustomers')
    }
    customerAppFactory.SaveCustomers = function (Customer) {
        return $http.post('/api/Customers/CreateCustomer', Customer)
    };
    customerAppFactory.UpdateCustomer = function (Customer) {
        return $http.put('/api/Customers/UpdateCustomer/' + Customer.CustomerId, Customer)
    };
    customerAppFactory.DeleteCustomer = function (Customer) {
        return $http.delete('/api/Customers/DeleteCustomer/' + Customer.CustomerId)
    };

    return customerAppFactory;

}])